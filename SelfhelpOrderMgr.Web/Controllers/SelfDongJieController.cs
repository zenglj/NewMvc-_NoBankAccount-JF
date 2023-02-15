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
    public class SelfDongJieController : Controller
    {
        //自助冻结犯人的金额
        // GET: /SelfDongJie/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string ip = "";
        public ActionResult Index()
        {
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
            if (fcardCode.Length != 10)
            {
                return Content(status);
            }

            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if (czys.Count == 0)
            {
                return Content("Error|无效的管理卡，请联系统民警");
            }
            List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode.ToString() + "'");
            if (cards.Count == 0)
            {
                status = "Error|该卡找不对应人员信息";
                return Content(status);
            }
            string fcrimeCode = cards[0].fcrimecode;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, 1);

            List<T_Czy_area> criminals = new T_Czy_areaBLL().GetModelList("FFlag=2 and FCode='" + czys[0].FCode + "' and FAreaCode='" + criminal.FAreaCode + "' ");
            if (criminals.Count == 0)
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

        public ActionResult btnSubmitKK(int id = 1)
        {
            id = 2;//设为是扣款的
            string FCrimeCode = Request["FCrimeCode"];
            string FIcCardCode = Request["FIcCardCode"];
            string FManagerCard = Request["FManagerCard"];
            string KKMoney = Request["KKMoney"];
            string DType = Request["DType"];
            #region 验证信息完整性
            if (string.IsNullOrEmpty(FManagerCard))
            {
                return Content("Err|管理卡不能为空");
            }

            if (string.IsNullOrEmpty(FIcCardCode))
            {
                return Content("Err|冻结的IC卡号不能为空");
            }
            if (string.IsNullOrEmpty(FCrimeCode))
            {
                return Content("Err|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(KKMoney))
            {
                return Content("Err|冻结金额不能为空");
            }
            #endregion

            if (Convert.ToDecimal(KKMoney) < 10)
            {
                return Content("Err|冻结金额不能小于10元，要完全解除冻结须与供应站联系，谢谢");
            }
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if (czys.Count == 0)
            {
                return Content("Err|无效管理卡，用户不存在");
            }
            string strFCode = FCrimeCode;
            string strFName = Request["FName"];
            string strDType = DType;
            string strFMoney = KKMoney;
            string strApply = czys[0].FUserChinaName;
            string strRemark = "自助扣款";
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
            
            int r=new CommTableInfoBLL().ExecSql("update t_Criminal set flimitflag=1,flimitamt=" + strFMoney + " where isnull(fflag,0)=0 and fcode='" + strFCode + "'");

            if (r>0)
            {
                List<T_UserInfoExt> users = new T_CriminalBLL().GetUserInfo("FCode='" + strFCode + "'");
                if (Convert.ToDecimal(KKMoney) < criminal.flimitamt)
                {
                    Log4NetHelper.logger.Warn("冻结金额(变少)，操作员=" + czys[0].FName + "(" + czys[0].FUserChinaName + ")|管理卡号=" + FManagerCard +"|用户编号=" + criminal.FCode + "|姓名:" + criminal.FName + "|冻结金额=" + strFMoney + "|原冻结金额=" + criminal.flimitamt.ToString() + "|操作时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");
                }
                else
                {
                    Log4NetHelper.logger.Info("冻结金额，操作员=" + czys[0].FName + "(" + czys[0].FUserChinaName + ")|管理卡号=" + FManagerCard + "|用户编号=" + criminal.FCode + "|姓名:" + criminal.FName + "|冻结金额=" + strFMoney + "|原冻结金额=" + criminal.flimitamt.ToString() + "|操作时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");
                }
                return Content("OK|" + strFMoney + "|" + jss.Serialize(users[0]));
            }
            else
            {
                return Content("Err|冻结失败");
            }

        }


        #region 出监账号录入界面

        public ActionResult PayeeLogin()
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
        
        public ActionResult PayeeIndex()
        {
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

        #endregion
        


	}
}