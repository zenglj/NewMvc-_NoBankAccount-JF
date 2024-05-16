using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    //[MyLogActionFilterAttribute]
    public class SelfPaymentController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string ip = "";
        //
        // GET: /SelfPayment/
        public ActionResult Index()
        {

            string strMode = "0";
            T_SHO_ManagerSet LoginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            if (LoginMode != null)
            {
                strMode = LoginMode.MgrValue.ToString();
            }
            ViewData["LoginMode"] = strMode;

            string LoginFlag = Request["LoginFlag"];
            string managerCardNo = Request["managerCardNo"];
            string UserName = Request["UserName"];
            if (string.IsNullOrEmpty(LoginFlag) == true)
            {
                return View("Login");
            }
            else if ("LoginOK122342124121123131231122" != LoginFlag)
            {
                return View("Login");
            }

            ViewData["FManagerCard"] = managerCardNo;
            ViewData["UserName"] = UserName;
            ViewData["fcrimecode"] = Request["fcrimecode"]; 
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

            
            return View();
        }

        //获取自助扣款的摘要
        public ActionResult getKK_Remark()
        {
            string keyMode = Request["KeyModeValue"];
            List<string> ss = new List<string>();
            List<T_SHO_ManagerSet> msets = new T_SHO_ManagerSetBLL().GetModelList("KeyName like 'ZuzhuKoukuanLeixinZaiyao%' and KeyMode='"+ keyMode +"'");
            if (msets.Count()>0 )
            {
                if (msets[0].MgrValue == "1")
                {
                    ss = msets[0].MgrName.Split((char)44).ToList<string>();
                    return Content("OK|" + jss.Serialize(ss));
                }
            }
            return Content("Err|无相应的类型");
        }
        public ActionResult Login()
        {

            string strMode = "0";
            T_SHO_ManagerSet LoginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            if (LoginMode != null)
            {
                strMode = LoginMode.MgrValue.ToString();
            }
            ViewData["LoginMode"] = strMode;
            return View();
        }
        //验证系统登录
        public ActionResult LoginCheck()
        {
            string managerCardNo = Request["managerCardNo"];
            List<T_CZY> users = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            string status = "Error|无效的管理卡";
            if (users.Count > 0)
            {
                T_CZY user = users[0];
                if (string.IsNullOrEmpty(user.FManagerCard) == false)
                {
                    ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                    Log4NetHelper.logger.Info("自助扣款登录,操作员：" + user.FName + "管理卡号:" + user.FManagerCard + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");

                    status = "OK|登录成功|" + user.FUserChinaName;
                }
            }
            return Content(status);
        }

        //查找用户信息
        public ActionResult QueryUserInfo()
        {
            string fcardCode = Request["FCardCode"];
            string FManagerCard = Request["FManagerCard"];
            string UserName = Request["UserName"];
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            //string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string status = "Error|查询失败";
            
            
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if(czys.Count==0)
            {
                return Content("Error|无效的管理卡，请联系统民警");
            }

            string fcrimeCode = Request["fcrimecode"];
            if (string.IsNullOrWhiteSpace(fcrimeCode))
            {
                if (fcardCode.Length != 10 || fcardCode.Length != 11)
                {
                    return Content(status);
                }

                List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode.ToString() + "'");
                if (cards.Count == 0)
                {
                    status = "Error|该卡找不对应人员信息";
                    return Content(status);
                }
                fcrimeCode = cards[0].fcrimecode;
            }
            
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, 1);

            List<T_Czy_area> criminals = new T_Czy_areaBLL().GetModelList("FFlag=2 and FCode='" + czys[0].FCode + "' and FAreaCode='"+ criminal.FAreaCode +"' ");
            if(criminals.Count==0)
            {
                return Content("Error|能不能跨队别扣款，该卡所属：" + criminal.FAreaName + ",您没有管理权限，请与该队别民警联系，谢谢");
            }
            
            //获取查询到的近三个月的存款记录

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList(100, "flag=0 and fcrimecode='" + criminal.FCode + "' and crtdate>='" + DateTime.Now.AddMonths(-3).ToShortDateString() + "'and crtdate<'" + DateTime.Now.AddDays(1).ToShortDateString() + "'", " CrtDate desc");
            rtnQueryUserInfo userinfo = new rtnQueryUserInfo();
            userinfo.UserInfo = criminal;
            userinfo.vcrds = vcrds;
            
            status = jss.Serialize(userinfo);
            status = "There|" + status;
            return Content(status);
        }

        public ActionResult btnSubmitKK(int id=1)
        {
            id = 2;//设为是扣款的
            string FCrimeCode = Request["FCrimeCode"];
            string FIcCardCode = Request["FIcCardCode"];
            string FManagerCard = Request["FManagerCard"];
            string KKMoney = Request["KKMoney"];
            string DType = Request["DType"];
            string remark = Request["Remark"];
            #region 验证信息完整性
            if (string.IsNullOrEmpty(FManagerCard))
            {
                return Content("Err|管理卡不能为空");
            }
            if (string.IsNullOrEmpty(DType))
            {
                return Content("Err|扣款类型不能为空");
            }
            if (string.IsNullOrEmpty(FIcCardCode))
            {
                return Content("Err|扣款的IC卡号不能为空");
            }
            if (string.IsNullOrEmpty(FCrimeCode))
            {
                return Content("Err|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(KKMoney))
            {
                return Content("Err|扣款金额不能为空");
            } 
            #endregion

            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if(czys.Count==0)
            {
                return Content("Err|无效管理卡，用户不存在");
            }
            string strFCode = FCrimeCode;
            string strFName = Request["FName"];
            string strDType = DType;
            string strFMoney = KKMoney;
            string strApply = czys[0].FUserChinaName;
            string strRemark = "自助扣款";
            if (string.IsNullOrEmpty(remark) == false)
            {
                strRemark = remark;
            }

            string strLoginName = czys[0].FName;
            T_Criminal criminal = new T_CriminalBLL().GetModel(strFCode);
            if (criminal.flimitamt == null)
            {
                criminal.flimitamt = 0;
            }
            if (criminal.flimitflag == null)
            {
                criminal.flimitamt = 0;
            }
            if (criminal == null)
            {
                return Content("Err|用户不存在");
            }
            if (criminal.fflag == 1)
            {
                return Content("Err|用户已经离监");
            }
            //flag 是存扣款的标志，1是存款，-1是扣款
            List<T_Savetype> savetypes = new List<T_Savetype>();
            int savePayFlag = 1;
            if (id == 1)
            {
                savetypes = new T_SavetypeBLL().GetModelList("typeflag=0 and FCode=" + strDType);
                savePayFlag = 1;
            }
            else if (id == 2)
            {
                T_Criminal_card card = new T_Criminal_cardBLL().GetModel(strFCode);
                if (card.AmountA + card.AmountB - (criminal.flimitamt - criminal.flimitflag) < Convert.ToDecimal(strFMoney))
                {
                    if (card.AmountA + card.AmountB > Convert.ToDecimal(strFMoney))
                    {
                        return Content("Err|账户余额不足,有冻结金额:" + criminal.flimitamt.ToString() + "，不可用");
                    }
                    return Content("Err|账户余额不足");
                }

                savetypes = new T_SavetypeBLL().GetModelList("typeflag=1 and FCode=" + strDType);
                savePayFlag = -1;
            }
            else
            {
                return Content("Err|你传的是错误的参数");
            }

            //增加判断自助扣款是否是审核，MgrValue 为1要，为0不要
            int auditFlag = 0;
            T_SHO_ManagerSet kkAuditMset = new T_SHO_ManagerSetBLL().GetModel("YibanQukouKuanShenhe_Flag");
            if (kkAuditMset != null)
            {
                if (kkAuditMset.MgrValue == "1")
                {
                    auditFlag = -1;//如果需要审核则T_Vcrd把这个BankFlag标志设为-1
                }
            }
            //根据上面获取auditFlag的值，MgrValue 为1要，为0不要
            List<T_Vcrd> vcrd = new T_VcrdBLL().UserCunKouKuan(strFCode, savePayFlag, Convert.ToDecimal(strFMoney), savetypes[0], strLoginName, strRemark, strApply, "", auditFlag);

            //strFCode;
            List<T_UserInfoExt> users = new T_CriminalBLL().GetUserInfo("FCode='" + strFCode + "'");

            Log4NetHelper.logger.Info("自助扣款，操作员=" + czys[0].FName + "(" + czys[0].FUserChinaName + ")|管理卡号=" + FManagerCard + "|用户编号=" + criminal.FCode + "|姓名:" + criminal.FName + "|扣款金额=" + strFMoney + "|扣款类型=" + savetypes[0].fname + "|操作时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");

            return Content("OK|" + jss.Serialize(vcrd) + "|" + jss.Serialize(users[0]));
        }
	}
}