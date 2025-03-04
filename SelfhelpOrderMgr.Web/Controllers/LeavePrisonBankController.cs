using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class LeavePrisonBankController : Controller
    {
        BaseDapperBLLExtend _bll = new BaseDapperBLLExtend();
        // GET: LeavePrisonBank
        public ActionResult Index()
        {
            string LoginFlag = Request["LoginFlag"];
            string managerCardNo = Request["managerCardNo"];
            string UserName = Request["UserName"];

            ViewData["fcrimecode"] = Request["fcrimecode"];
            ViewData["FManagerCard"] = managerCardNo;
            ViewData["UserName"] = UserName;

            List<T_Savetype> saveTypes = new T_SavetypeBLL().GetModelList("typeflag=1 and zzkk_flag=1");
            ViewData["saveTypes"] = saveTypes;

            T_SHO_ManagerSet softNumerKeyBoard = new T_SHO_ManagerSetBLL().GetModel("SoftNumerKeyBoard");
            if (softNumerKeyBoard == null)
            {
                ViewData["softNumerKeyBoard"] = "0";
            }
            else
            {
                ViewData["softNumerKeyBoard"] = softNumerKeyBoard.MgrValue;
            }

            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["loginMode"] = loginMode.MgrValue;

            return View();
        }

        public ActionResult GetBankCardInfo()
        {

            //if (Session["loginUserName"] == null)
            //{
            //    rs.ReMsg = "Err|请先刷管理卡登录";
            //    return Json(rs);
            //}
            return View();
        }

        /// <summary>
        /// 查找用于管理卡下的用户
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="fmanagerCard"></param>
        /// <returns></returns>
        public ActionResult GetManagerUserInfo(string fcrimecode, string fmanagerCard)
        {
            ResultInfo rs = new ResultInfo();

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
            }
            if (Session["loginUserName"] == null)
            {
                rs.ReMsg = "Err|请先刷管理卡登录";
                return Json(rs);
            }
            
            if(!(fcrimecode.Length==10 || fcrimecode.Length == 11))
            {
                rs.ReMsg = "Err|狱号位数长度不正确";
                return Json(rs) ;
            }

            if (!(fmanagerCard.Length == 10 || fmanagerCard.Length == 11))
            {
                rs.ReMsg = "Err|管理卡号长度不正确";
                return Json(rs);
            }

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode, 1);
            if (criminal == null)
            {
                rs.ReMsg = "Err|狱号不存在";
                return Json(rs);
            }

            if (!_bll.CheckManagerCardAreaPower(fmanagerCard, criminal.FAreaCode))
            {
                rs.ReMsg = "Err|管理卡没有用户的管理权限";
                return Json(rs);
            }

            ReadUserCardInfo(fcrimecode, rs, criminal);
            return Json(rs);
        }


        /// <summary>
        /// 获取用户信息 根据用户名和密码
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult UserPwdLogin(string fname,string pwd)
        {
            ResultInfo rs = new ResultInfo();

            
            T_Criminal_card card = _bll.QueryModel<T_Criminal_card>("FCrimeCode", fname);
            if (card == null)
            {
                rs.ReMsg = "Err|卡号无效";
                return Json(rs);
            }
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(card.fcrimecode, 1);
            if (criminal == null)
            {
                rs.ReMsg = "Err|狱号不存在";
                return Json(rs);
            }

            ReadUserCardInfo(card.fcrimecode, rs, criminal);
            return Json(rs);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public ActionResult GetUserInfo(string cardno)
        {
            ResultInfo rs = new ResultInfo();

            if (!(cardno.Length == 10 || cardno.Length == 11))
            {
                rs.ReMsg = "Err|卡号位数长度不正确";
                return Json(rs);
            }
            T_Criminal_card card = _bll.QueryModel<T_Criminal_card>("CardCodeA", cardno);
            if (card == null)
            {
                rs.ReMsg = "Err|卡号无效";
                return Json(rs);
            }
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(card.fcrimecode, 1);
            if (criminal == null)
            {
                rs.ReMsg = "Err|狱号不存在";
                return Json(rs);
            }

            ReadUserCardInfo(card.fcrimecode, rs, criminal);
            return Json(rs);
        }

        private void ReadUserCardInfo(string fcrimecode, ResultInfo rs, T_Criminal criminal)
        {
            T_Criminal_OutBankAccount outbank = _bll.QueryModel<T_Criminal_OutBankAccount>("FCrimecode", fcrimecode);
            RecvBankCardInfo recvBankCardInfo = new RecvBankCardInfo();
            recvBankCardInfo.criminal = criminal;
            recvBankCardInfo.recvBankAccount = outbank;
            recvBankCardInfo.paymentRecord =_bll.QueryModel<T_Bank_PaymentRecord>("FCrimeCode",fcrimecode,"Id desc");
            //验证是否有未结清的数据
            recvBankCardInfo.jieqingFlag= _bll.QueryList<T_Vcrd>("select * from t_Vcrd where flag=0 and typeflag not in(5,6) and isnull(bankflag,0)<2 and FCrimeCode=@FCrimeCode", new { FCrimeCode= fcrimecode }).Count==0;

            rs.DataInfo = recvBankCardInfo;
            rs.Flag = true;
            if (outbank != null)
            {
                if (outbank.CheckFlag == 1)
                {
                    rs.ReMsg = "OK|您已提交申请，请耐心等待余额同步情况，一小时后在查询";
                }
                else if (outbank.CheckFlag == 2)
                {
                    rs.ReMsg = "OK|系统后台正在同步最新余额，请耐心等待返回结果，一小时后在查询";
                }
                else if (outbank.CheckFlag == 3)
                {
                    rs.ReMsg = $"OK|已同步最新余额为：{criminal.AmountA + criminal.AmountB + criminal.AmountC} 元，可以开始结算";
                }
                else if (outbank.CheckFlag == 4)
                {
                    rs.ReMsg = "OK|您已经签字结算成功，请到接转账结果区，查询转账结果";
                }
                else
                {
                    rs.ReMsg = "OK|成功";
                }
            }

        }

        /// <summary>
        /// 设置银行卡信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ActionResult SetBankInfo(ReqBankInfo req)
        {
            ResultInfo rs = new ResultInfo();
            if (Session["loginUserName"] == null)
            {
                rs.ReMsg = "Err|请先刷管理卡登录";
                return Json(rs);
            }
            string loginName = Session["loginUserName"].ToString();
            if (!(req.FCrimeCode.Length==10 || req.FCrimeCode.Length == 11))
            {
                rs.ReMsg = "Err|用户编号不正确";
                return Json(rs);
            }
            try
            {
                T_Criminal_OutBankAccount bankAccount = _bll.QueryModel<T_Criminal_OutBankAccount>("FCrimecode", req.FCrimeCode);
                if (bankAccount == null)
                {
                    bankAccount = new T_Criminal_OutBankAccount()
                    {
                        FCrimecode = req.FCrimeCode,
                        OutBankCard = req.OutBankCard,
                        OutBankRemark = req.OutBankRemark,
                        BankUserName = req.BankUserName,
                        OpeningBank = req.OpeningBank,
                        BankCNAPS = req.BankCNAPS,
                        CrtBy = loginName,
                        CrtDate = DateTime.Now,
                        CheckDate = Convert.ToDateTime("2000-01-01"),
                        ModifyTime = Convert.ToDateTime("2000-01-01")
                };
                    _bll.Insert<T_Criminal_OutBankAccount>(bankAccount);
                }
                else
                {
                    if (bankAccount.CheckFlag == 1)
                    {
                        rs.ReMsg = "Err|该银行账号用户已经确认了，不能再修改";
                        return Json(rs);
                    }
                    bankAccount.FCrimecode = req.FCrimeCode;
                    bankAccount.OutBankCard = req.OutBankCard;
                    bankAccount.OutBankRemark = req.OutBankRemark;
                    bankAccount.BankUserName = req.BankUserName;
                    bankAccount.OpeningBank = req.OpeningBank;
                    bankAccount.BankCNAPS = req.BankCNAPS;
                    bankAccount.ModifyBy = loginName;
                    bankAccount.ModifyTime = DateTime.Now;
                    if (bankAccount.CheckDate < Convert.ToDateTime("2000-01-01"))
                    {
                        bankAccount.CheckDate = Convert.ToDateTime("2000-01-01");
                    }
                    _bll.Update<T_Criminal_OutBankAccount>(bankAccount);
                }

                rs.Flag = true;
                rs.ReMsg = "OK|保存成功";
                return Json(rs);
            }
            catch (Exception e)
            {
                rs.ReMsg = "Err|"+e.Message;
                return Json(rs);
            }
            
        }



        /// <summary>
        /// 管理卡登录
        /// </summary>
        /// <param name="FManagerCard"></param>
        /// <returns></returns>
        public ActionResult CardLogin(string FManagerCard)
        {
            ResultInfo rs = new ResultInfo();
            if(!(FManagerCard.Length==10 || FManagerCard.Length == 11))
            {
                rs.ReMsg = "Err|管理卡号位数不正确";
                return Json(rs);
            }
            T_CZY user = _bll.QueryModel<T_CZY>("FManagerCard", FManagerCard);
            if (user == null)
            {
                rs.ReMsg = "Err|管理卡号不存在";
                return Json(rs);
            }
            try
            {
                string strCookieLogin = "";
                T_SHO_ManagerSet checkUserLoginModeMgr = new T_SHO_ManagerSetBLL().GetModel("CheckLoginSeccionOrCookie");

                if (checkUserLoginModeMgr != null)
                {
                    if (checkUserLoginModeMgr.MgrValue == "1")
                    {
                        //创建Cookie
                        HttpCookie cookie = new HttpCookie("loginUserName", user.FName);
                        cookie.Expires = DateTime.Now.AddHours(4);
                        //写入Cookie
                        Response.Cookies.Set(cookie);

                        HttpCookie cookieCode = new HttpCookie("loginUserCode", user.FCode);
                        cookieCode.Expires = DateTime.Now.AddHours(4);
                        //写入Cookie
                        Response.Cookies.Set(cookieCode);

                        strCookieLogin = "COOKIE";
                        Session["loginUserAdmin"] = user.FPRIVATE;
                        Session["loginUserCode"] = user.FCode;
                        Session["loginUserName"] = user.FName;
                    }
                }
                //如果不是Cookie就用Seccion
                if (string.IsNullOrEmpty(strCookieLogin))
                {
                    //Session["loginUserLevelId"] = user.FRole.LevelId;
                    Session["loginUserAdmin"] = user.FPRIVATE;
                    Session["loginUserCode"] = user.FCode;
                    Session["loginUserName"] = user.FName;
                }


                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //记录日志
                T_SysOperationLog log = new T_SysOperationLog()
                {
                    ControlName = "LeavePrisonBank",
                    ActionName = "CardLogin",
                    CrtDate = DateTime.Now,
                    Remark = "离监银行收集登录,IP:" + ip,
                    ReqJson = FManagerCard,
                    RtnJson = "OK|验证成功",
                    UserCode = user.FName
                };
                new BaseDapperBLL().Insert<T_SysOperationLog>(log);


                rs.Flag = true;
                rs.ReMsg = "OK|登录成功";
                rs.DataInfo = FManagerCard;
                return Json(rs);
            }
            catch (Exception e)
            {

                rs.ReMsg = "Err|"+e.Message;
                return Json(rs);
            }

        }

        /// <summary>
        /// 确认银行卡转账信息
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <returns></returns>
        public ActionResult CheckBankCard(string fcrimecode,string icCardCode, string selPayMode,string image)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                int imgLen = image.Length;
                if (image.Length < 3000)
                {
                    rs.ReMsg = "请正确签名。。。。";
                    return Json(rs);
                }
                var imgRs = Base64ToImageHelper.Base64StringToImage(image, Server.MapPath("~/Content/ImageQianMing/qm_" + fcrimecode + ".png"));

                if (string.IsNullOrWhiteSpace(fcrimecode) || string.IsNullOrWhiteSpace(icCardCode))
                {
                    rs.ReMsg = "Err|狱政编号和管理卡号不能为空";
                    return Json(rs);
                }

                if (string.IsNullOrWhiteSpace(selPayMode))
                {
                    rs.ReMsg = "Err|付款模式不能为空";
                    return Json(rs);
                }

                T_Criminal criminal = _bll.QueryModel<T_Criminal>("FCode", fcrimecode);
                if (criminal == null || criminal.fflag == 1)
                {
                    rs.ReMsg = "Err|人员不存在或已离监不能重复结算";
                    return Json(rs);
                }

                T_Criminal_card card = _bll.QueryModel<T_Criminal_card>("FCrimeCode", fcrimecode);
                if (card == null || card.cardcodea != icCardCode)
                {
                    rs.ReMsg = "Err|当前使用的不是有效的IC卡，请与民警联系";
                    return Json(rs);
                }

                T_Criminal_OutBankAccount outBank = _bll.QueryModel<T_Criminal_OutBankAccount>("fcrimecode", fcrimecode);
                bool changeMode = true;//有记录
                if (outBank == null)
                {
                    changeMode = false;
                }
                if (outBank == null && Convert.ToInt32(selPayMode) == 2)
                {
                    rs.ReMsg = "Err|找不到相应的银行账号信息";
                    return Json(rs);
                }
                if (Convert.ToInt32(selPayMode) == 0 && outBank == null)
                {
                    outBank = new T_Criminal_OutBankAccount();
                    outBank.FCrimecode = fcrimecode;
                    outBank.CrtBy = criminal.FName + "(自助办理)";
                    outBank.CrtDate = DateTime.Now;
                    outBank.ModifyTime = DateTime.Now;
                    outBank.BankUserName = "柜台领款";
                    outBank.OutBankCard = "";
                    outBank.OutBankRemark = "";
                }
                if (Convert.ToInt32(selPayMode) == 1 && outBank == null)
                {
                    outBank = new T_Criminal_OutBankAccount();
                    outBank.FCrimecode = fcrimecode;
                    outBank.CrtBy = criminal.FName + "(自助办理)";
                    outBank.CrtDate = DateTime.Now;
                    outBank.ModifyTime = DateTime.Now;
                    outBank.BankUserName = "ATM取款";
                    outBank.OutBankCard = "";
                    outBank.OutBankRemark = "";
                }
                outBank.CheckFlag = 4;
                outBank.CheckDate = DateTime.Now;
                outBank.CheckCard = icCardCode;
                outBank.PayMode = Convert.ToInt32(selPayMode);
                outBank.QianMing = image;

                string rtnReustl = "OK|该犯之前就结算过了";
                using (TransactionScope ts = new TransactionScope())
                {

                    if (changeMode)
                    {
                        _bll.Update<T_Criminal_OutBankAccount>(outBank);

                    }
                    else
                    {
                        _bll.Insert<T_Criminal_OutBankAccount>(outBank);

                    }

                    /*直接离监结算
                     1、判断IC是否正确有效
                     2、是否已经结算过，如果结算过就不能重复结算
                     3、验证出监日期
                     */
                    switch (Convert.ToInt32(selPayMode))
                    {
                        case 0://网点支取
                            {
                                //=====2022-05-16 zenglj 应武夷山监狱要求现金结算也要到PaymentRecord表进行审核
                                //rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProcedure(FCode, LoginUserName);
                                rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProc_NoBankCard(fcrimecode, criminal.FName + "(自助)", Convert.ToInt32(selPayMode),"自助");

                                //网点支取，现金支付
                            }
                            break;
                        case 1://ATM机现金结算
                            {
                                rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProc_NoBankCard(fcrimecode, criminal.FName + "(自助)", Convert.ToInt32(selPayMode), "自助");

                                if(rtnReustl== "OK|Success")
                                {
                                    T_Bank_PaymentRecord t = new BaseDapperBLL().GetModelFirst<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(Newtonsoft.Json.JsonConvert.SerializeObject(new { FCrimeCode = fcrimecode,PayMode=1 }));
                                    if (string.IsNullOrWhiteSpace(t.WithdrawalPassword))
                                    {
                                        //生成取款密码
                                        Random r1 = new Random();

                                        int rowNo = r1.Next(100000, 999999);
                                        t.WithdrawalPassword = rowNo.ToString();
                                        new BaseDapperBLL().Update<T_Bank_PaymentRecord>(t);

                                    }
                                }
                                

                                //ATM机现金结算
                            }
                            break;
                        case 2://转账支付
                            {
                                //否则采用正常结算模式
                                rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProc_NoBankCard(fcrimecode, criminal.FName + "(自助)", Convert.ToInt32(selPayMode), "自助");
                                //转账支付
                            }
                            break;
                        case 5://放弃领款
                            {
                                rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProcedure(fcrimecode, criminal.FName + "(自助)");
                                new CommTableInfoBLL().ExecSql($"update T_balancelist set PayMode=5 where fcrimecode='{fcrimecode}'");
                            }
                            break;
                        case 3://只做挂失
                            {
                                rtnReustl = new T_TempLeavePrisonBLL().SetLossAndInsertBankProve(fcrimecode);
                            }
                            break;

                        default:
                            {
                                //return Content("Err|未定义的动作方式【" + selPayMode + "】");
                                rs.Flag = false;
                                rs.ReMsg = "Err|未定义的动作方式【" + selPayMode + "】";
                                return Json(rs);
                            }
                    }

                    ts.Complete();
                }

                rs.Flag = true;
                rs.ReMsg = rtnReustl;
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|失败_{ex.Message}";
                return Json(rs);
            }
        }


        /// <summary>
        /// 余额查询申请
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="icCardCode"></param>
        /// <param name="selPayMode"></param>
        /// <returns></returns>
        public ActionResult BalanceQuery(string fcrimecode,string icCardCode,string selPayMode)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (string.IsNullOrWhiteSpace(fcrimecode))
                {
                    rs.ReMsg = "狱号不能为空";
                    return Json(rs);
                }
                if (string.IsNullOrWhiteSpace(icCardCode))
                {
                    rs.ReMsg = "确认的IC卡号不能为空";
                    return Json(rs);
                }
                if (string.IsNullOrWhiteSpace(selPayMode))
                {
                    rs.ReMsg = "请选择一个付款模式";
                    return Json(rs);
                }

                T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode,1);
                if (criminal.fflag != 1)
                {
                    _bll.Delete<T_Criminal_OutBankAccount>("FCrimeCode", fcrimecode);
                }
                T_Criminal_OutBankAccount outBank = _bll.QueryModel<T_Criminal_OutBankAccount>("fcrimecode", fcrimecode);
                bool changeMode = true;//有记录
                if (outBank == null)
                {
                    changeMode = false;
                }
                if (outBank != null && outBank.CheckFlag>=1)
                {

                    RecvBankCardInfo recvBankCardInfo = new RecvBankCardInfo();
                    recvBankCardInfo.criminal = criminal;
                    recvBankCardInfo.recvBankAccount = outBank;
                    //recvBankCardInfo.card = _bll.QueryModel<T_Criminal_card>("FCrimeCode", fcrimecode, "fcrimecode desc");
                    rs.DataInfo = recvBankCardInfo;

                    if (outBank.CheckFlag == 1)
                    {
                        rs.ReMsg = "OK|您已提交申请，请耐心等待余额同步情况，一小时后在查询";
                    }
                    else if (outBank.CheckFlag == 2)
                    {
                        rs.ReMsg = "OK|系统后台正在同步最新余额，请耐心等待返回结果，一小时后在查询";
                    }
                    else if (outBank.CheckFlag == 3)
                    {
                        rs.ReMsg = $"OK|已同步最新余额为：{criminal.AmountA+ criminal.AmountB+ criminal.AmountC} 元，可以开始结算";
                    }
                    else if (outBank.CheckFlag == 4)
                    {
                        rs.ReMsg = "OK|已经签字结算成功，请到接转账结果区，查询转账结果";
                    }
                    else
                    {
                        rs.ReMsg = "OK|成功";
                    }

                    

                    rs.DataInfo = outBank;
                    rs.Flag = true;

                    return Json(rs);
                }


                if (outBank == null && Convert.ToInt32(selPayMode) == 2)
                {
                    rs.ReMsg = "Err|找不到相应的银行账号信息";
                    return Json(rs);
                }
                if (Convert.ToInt32(selPayMode) == 0 && outBank == null)
                {
                    outBank = new T_Criminal_OutBankAccount();
                    outBank.FCrimecode = fcrimecode;
                    outBank.CrtBy = criminal.FName + "(自助办理)";
                    outBank.CrtDate = DateTime.Now;
                    outBank.ModifyTime = DateTime.Now;
                    outBank.BankUserName = "柜台领款";
                    outBank.OutBankCard = "";
                    outBank.OutBankRemark = "";
                }
                if (Convert.ToInt32(selPayMode) == 1 && outBank == null)
                {
                    outBank = new T_Criminal_OutBankAccount();
                    outBank.FCrimecode = fcrimecode;
                    outBank.CrtBy = criminal.FName + "(自助办理)";
                    outBank.CrtDate = DateTime.Now;
                    outBank.ModifyTime = DateTime.Now;
                    outBank.BankUserName = "ATM取款";
                    outBank.OutBankCard = "";
                    outBank.OutBankRemark = "";
                }
                outBank.CheckFlag = 3;
                outBank.CheckDate = DateTime.Now;
                outBank.CheckCard = icCardCode;
                outBank.PayMode = Convert.ToInt32(selPayMode);

                if (changeMode==false)
                {
                    _bll.Insert<T_Criminal_OutBankAccount>(outBank);

                }
                else
                {
                    _bll.Update<T_Criminal_OutBankAccount>(outBank);

                }

                rs.Flag = true;
                rs.ReMsg = "OK|已提交申请，现在请进行电子签名以完成正式结算";
                rs.DataInfo = outBank;
                return Json(rs);

            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return Json(rs);
            }
            

        }

        public ActionResult QianMing(string submitInfo)
        {
            string strJson = submitInfo;
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<T_Criminal_OutBankAccount>(strJson);
            ViewData["submitInfo"] = model;

            ViewData["cardInfo"] =new T_CriminalBLL().GetCriminalXE_info(model.FCrimecode,1);
            return View();
        }



        /// <summary>
        /// 离监支付状态查询
        /// </summary>
        /// <param name="iccardNo"></param>
        /// <returns></returns>
        public ActionResult ResultQuery(string iccardNo)
        {

            return View();
        }


        public ActionResult Test()
        {
            return View();
        }
    }
}