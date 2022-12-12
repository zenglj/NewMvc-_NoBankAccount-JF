using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    public class ComAppController : Controller
    {
        //
        // GET: /ComApp/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string ip = "";
        public ActionResult Index()
        {
            string LoginFlag = Request["LoginFlag"];
            string managerCardNo = Request["managerCardNo"];
            string UserName = Request["UserName"];
            //if (string.IsNullOrEmpty(LoginFlag) == true)
            //{
            //    return View("Login");
            //}
            //else if ("LoginOK122342124121123131231122" != LoginFlag)
            //{
            //    return View("Login");
            //}

            ViewData["FManagerCard"] = managerCardNo;
            ViewData["UserName"] = UserName;

            //List<T_AREA> areas = new T_AREABLL().GetModelList("");
            List<T_AREA> areas = null;
            if (string.IsNullOrEmpty(managerCardNo)==false)
            {
                areas = new T_AREABLL().GetModelList(@" fcode in(
                select fareaCode from t_Czy_Area where fflag=2 and fcode in(
                select fcode from t_czy where FManagerCard ='" + managerCardNo + "'))");
            }
            

            ViewData["areas"] = areas;

            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["LoginMode"] = mset.MgrValue;
            return View();
        }

        public ActionResult areaLogin()
        {
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["LoginMode"] = mset.MgrValue;

            return View();
        }
        
        public ActionResult LoginCheck()
        {
            string LoginFlag = Request["LoginFlag"];
            string managerCardNo = Request["managerCardNo"];
            string UserName = Request["UserName"];
            if (string.IsNullOrEmpty(managerCardNo))
            {
                return Content("Err|管理卡号不能为空");
            }

            List<T_AREA> areas = new T_AREABLL().GetModelList(@" fcode in(
                select fareaCode from t_Czy_Area where fflag=2 and fcode in(
                select fcode from t_czy where FManagerCard ='"+managerCardNo+"'))");

            return Content("OK|"+ jss.Serialize(areas));
        }

        public ActionResult QueryUserInfo()
        {
            string fcardCode = Request["FCardCode"];
            string FManagerCard = Request["FManagerCard"];
            string UserName = Request["UserName"];
            ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            //string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string status = "Error|查询失败";
            if (fcardCode.Length != 10)
            {
                return Content(status);
            }
            if (FManagerCard.Substring(0, 2) == "OK")
            {
                FManagerCard = FManagerCard.Substring(3);
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

            

            //获取查询到的近三个月的存款记录

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList(100, "flag=0 and fcrimecode='" + criminal.FCode + "' and crtdate>='" + DateTime.Now.AddMonths(-3).ToShortDateString() + "'and crtdate<'" + DateTime.Now.AddDays(1).ToShortDateString() + "'", " CrtDate desc");
            rtnQueryUserInfo userinfo = new rtnQueryUserInfo();
            userinfo.UserInfo = criminal;
            userinfo.vcrds = vcrds;

            status = jss.Serialize(userinfo);
            status = "There|" + status;
            return Content(status);
        }

        //按狱号查询人员信息
        public ActionResult QueryUserInfoByFCrimeCode()
        {
            string userCodeId = Request["userCodeId"];
            string FManagerCard = Request["FManagerCard"];
            string UserName = Request["UserName"];
            ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            //string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string status = "Error|查询失败";

            //FManagerCard = FManagerCard.Substring(3);
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if (czys.Count == 0)
            {
                return Content("Error|无效的管理卡，请联系统民警");
            }

            if (string.IsNullOrEmpty(userCodeId)==true)
            {
                status = "Error|狱号不能为空";
                return Content(status);
            }
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(userCodeId, 1);



            //获取查询到的近三个月的存款记录

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList(100, "flag=0 and fcrimecode='" + criminal.FCode + "' and crtdate>='" + DateTime.Now.AddMonths(-3).ToShortDateString() + "'and crtdate<'" + DateTime.Now.AddDays(1).ToShortDateString() + "'", " CrtDate desc");
            rtnQueryUserInfo userinfo = new rtnQueryUserInfo();
            userinfo.UserInfo = criminal;
            userinfo.vcrds = vcrds;

            status = jss.Serialize(userinfo);
            status = "There|" + status;
            return Content(status);
        }
        //队别调整
        public ActionResult btnSubmitKK(int id = 1)
        {
            id = 2;//设为是扣款的
            string FCrimeCode = Request["FCrimeCode"];
            string FIcCardCode = Request["FIcCardCode"];
            string FManagerCard = Request["FManagerCard"];            
            string DType = Request["DType"];
            #region 验证信息完整性
            if (string.IsNullOrEmpty(FManagerCard))
            {
                return Content("Err|管理卡不能为空");
            }
            if (FManagerCard.Substring(0,2) =="OK")
            {
                FManagerCard = FManagerCard.Substring(3);
            }
            if (string.IsNullOrEmpty(DType))
            {
                return Content("Err|队别不能为空");
            }
            if (string.IsNullOrEmpty(FIcCardCode))
            {
                return Content("Err|扣款的IC卡号不能为空");
            }
            if (string.IsNullOrEmpty(FCrimeCode))
            {
                return Content("Err|用户编号不能为空");
            }
            
            #endregion

            string status = "Error|查询失败";
            if (FIcCardCode.Length != 10)
            {
                return Content(status);
            }
            if (FManagerCard.Substring(0, 2) == "OK")
            {
                FManagerCard = FManagerCard.Substring(3);
            }
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if (czys.Count == 0)
            {
                return Content("Err|无效管理卡，用户不存在");
            }
            string strFCode = FCrimeCode;
            string strFName = Request["FName"];
            string strDType = DType;           
            string strLoginName = czys[0].FName;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(strFCode,1);
            T_AREA oldArea = new T_AREABLL().GetModel(criminal.FAreaCode);
            if (criminal == null)
            {
                return Content("Err|用户不存在");
            }
            if (criminal.fflag == 1)
            {
                return Content("Err|用户已经离监");
            }

            T_AREA newArea = new T_AREABLL().GetModel(strDType);
            if (newArea == null)
            {
                return Content("Err|您选择的新队别不存在");
            }

            StringBuilder strSql = new StringBuilder();

            //strSql.Append(@"insert into t_area_change(fcrimecode,fcriminal,fareacode,fareaname,fnewareacode,fnewareaname,crtby,crtdate,amount,dvouno,cvouno) ");
            strSql.Append(@"INSERT INTO [T_Criminal_ChangeList]
                        ([FCode],[FName],[ChangeType],[ChangeTypeName],[OldCode],[OldName],[NewCode],[NewName]
                        ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                        ,[Remark])
                             VALUES
                        ('" + criminal.FCode + "','" + criminal.FName + "','2','队别','" + criminal.FAreaCode + "','" + criminal.FAreaName + "','" + newArea.FCode + "','" + newArea.FName + @"'
                        ,'前台自助调队','" + czys[0].FName + "','" + DateTime.Now.ToString() + "','自动审核','系统','" + DateTime.Now.ToString() + "','变更不用审核',9,'')");
            strSql.Append(@" select a.fcode,a.fname,a.FAreaCode,b.FName FAreaName,'" + strDType + "','" + newArea.FName + "','" + strLoginName + @"',getdate(),0,'',''");
            strSql.Append(@" from t_Criminal a,T_Area b where a.fareaCode=b.FCode and a.FCode='" + strFCode + "';");
            strSql.Append(@" update t_criminal set fareacode='" + strDType + "' where fcode='" + strFCode + "';");

             T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByArea");
             if (areaMset != null)
             {
                 if (areaMset.MgrValue == "1")
                 {
                     strSql = new StringBuilder();
                     strSql.Append(@"INSERT INTO [T_Criminal_ChangeList]
                        ([FCode],[FName],[ChangeType],[ChangeTypeName],[OldCode],[OldName],[NewCode],[NewName]
                        ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                        ,[Remark])
                             VALUES
                        ('" + criminal.FCode + "','" + criminal.FName + "','2','队别','" + criminal.FAreaCode + "','" + criminal.FAreaName + "','" + newArea.FCode + "','" + newArea.FName + @"'
                        ,'前台自助调队','" + czys[0].FName + "','" + DateTime.Now.ToString() + "','','',null,'',0,'')");
                 }
             }

            int s=new CommTableInfoBLL().ExecSql(strSql.ToString());
            Log4NetHelper.logger.Info("自助调队，操作员=" + czys[0].FName + "(" + czys[0].FUserChinaName + ")|管理卡号=" + FManagerCard + "|用户编号=" + criminal.FCode + "|姓名:" + criminal.FName + "|新队别=" + newArea.FName + "|原队别=" + criminal.FAreaName + "|操作时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");

            if (areaMset != null)
            {
                if (areaMset.MgrValue == "1")
                {
                    if (s >= 1)
                    {
                        return Content("OK|调队申请提交成功，请联系管理科室审批");
                    }
                    else
                    {
                        return Content("OK|调队申请失败");
                    }
                }
            }
            if(s>=2)
            {
                return Content("OK|调队成功请重新刷卡验证");
            }
            else
            {
                return Content("OK|调队失败");
            }
        }

        public ActionResult ChuyiChange()
        {

            string LoginFlag = Request["LoginFlag"];
            string managerCardNo = Request["managerCardNo"];
            string UserName = Request["UserName"];
            //if (string.IsNullOrEmpty(LoginFlag) == true)
            //{
            //    return View("Login");
            //}
            //else if ("LoginOK122342124121123131231122" != LoginFlag)
            //{
            //    return View("Login");
            //}

            

            List<T_AREA> areas = new T_AREABLL().GetModelList("");

            List<T_CY_TYPE> cies = new T_CY_TYPEBLL().GetModelList("FName<>'' and isnull(flag,0)=1");

            ViewData["FManagerCard"] = managerCardNo;
            ViewData["UserName"] = UserName;
            ViewData["cies"] = cies;
            return View();
        }

        public ActionResult btnSubmitChange(int id = 1)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            T_SHO_ManagerSet cyPowerSet = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByCyPowerIP");
            if(cyPowerSet!=null && cyPowerSet.KeyMode == 1)
            {
                bool ipAuthFlag = false;
                string[] ips = cyPowerSet.MgrValue.Split((char)59);//以分号";"进行分割
                foreach (var item in ips)
                {
                    if(item== ip)
                    {
                        ipAuthFlag = true;
                        break;
                    }
                }
                if(ipAuthFlag==false)
                {
                    return Content("Err|您的机器没有授权，不能变更处遇");
                }
                
            }

            id = 2;//设为是扣款的
            string FCrimeCode = Request["FCrimeCode"];
            string FIcCardCode = Request["FIcCardCode"];
            string FManagerCard = Request["FManagerCard"];
            string DType = Request["DType"];
            #region 验证信息完整性
            if (string.IsNullOrEmpty(FManagerCard))
            {
                return Content("Err|管理卡不能为空");
            }

            string status = "Error|查询失败";
            if (FIcCardCode.Length != 10)
            {
                return Content(status);
            }
            if (FManagerCard.Substring(0, 2) == "OK")
            {
                FManagerCard = FManagerCard.Substring(3);
            }

            if (string.IsNullOrEmpty(DType))
            {
                return Content("Err|队别不能为空");
            }
            if (string.IsNullOrEmpty(FIcCardCode))
            {
                return Content("Err|扣款的IC卡号不能为空");
            }
            if (string.IsNullOrEmpty(FCrimeCode))
            {
                return Content("Err|用户编号不能为空");
            }

            #endregion

            
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + FManagerCard + "'");
            if (czys.Count == 0)
            {
                return Content("Err|无效管理卡，用户不存在");
            }
            string strFCode = FCrimeCode;
            string strFName = Request["FName"];
            string strDType = DType;
            string strLoginName = czys[0].FName;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(strFCode, 1);
            T_CY_TYPE oldCy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
            if (criminal == null)
            {
                return Content("Err|用户不存在");
            }
            if (criminal.fflag == 1)
            {
                return Content("Err|用户已经离监");
            }

            T_CY_TYPE newCy = new T_CY_TYPEBLL().GetModel(strDType);
            if (newCy == null)
            {
                return Content("Err|您选择的新处遇不存在");
            }

            StringBuilder strSql = new StringBuilder();

            //strSql.Append(@"insert into t_area_change(fcrimecode,fcriminal,fareacode,fareaname,fnewareacode,fnewareaname,crtby,crtdate,amount,dvouno,cvouno) ");
            strSql.Append(@"INSERT INTO [T_Criminal_ChangeList]
                        ([FCode],[FName],[ChangeType],[ChangeTypeName],[OldCode],[OldName],[NewCode],[NewName]
                        ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                        ,[Remark])
                             VALUES
                        ('" + criminal.FCode + "','" + criminal.FName + "','1','处遇','" + oldCy.FCode + "','" + oldCy.FName + "','" + newCy.FCode + "','" + newCy.FName + @"'
                        ,'前台自助处遇变更','" + czys[0].FName + "','" + DateTime.Now.ToString() + "','自动审核','系统','" + DateTime.Now.ToString() + "','变更不用审核',9,'')");
            strSql.Append(@" select a.fcode,a.fname,a.FAreaCode,b.FName FAreaName,'" + strDType + "','" + newCy.FName + "','" + strLoginName + @"',getdate(),0,'',''");
            strSql.Append(@" from t_Criminal a,T_Area b where a.fareaCode=b.FCode and a.FCode='" + strFCode + "';");
            strSql.Append(@" update t_criminal set FCyCode='" + strDType + "' where fcode='" + strFCode + "';");

            T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByCy");
            if (areaMset != null)
            {
                if (areaMset.MgrValue == "1")
                {
                    strSql = new StringBuilder();
                    strSql.Append(@"INSERT INTO [T_Criminal_ChangeList]
                        ([FCode],[FName],[ChangeType],[ChangeTypeName],[OldCode],[OldName],[NewCode],[NewName]
                        ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                        ,[Remark])
                             VALUES
                        ('" + criminal.FCode + "','" + criminal.FName + "','1','处遇','" + oldCy.FCode + "','" + oldCy.FName + "','" + newCy.FCode + "','" + newCy.FName + @"'
                        ,'前台自助处遇变更','" + czys[0].FName + "','" + DateTime.Now.ToString() + "','','',null,'',0,'')");
                }
            }

            int s = new CommTableInfoBLL().ExecSql(strSql.ToString());
            Log4NetHelper.logger.Info("自助处遇变更，操作员=" + czys[0].FName + "(" + czys[0].FUserChinaName + ")|管理卡号=" + FManagerCard + "|用户编号=" + criminal.FCode + "|姓名:" + criminal.FName + "|新队别=" + newCy.FName + "|原队别=" + oldCy.FName + "|操作时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");

            if (areaMset != null)
            {
                if (areaMset.MgrValue == "1")
                {
                    if (s >= 1)
                    {
                        return Content("OK|变更申请提交成功，请联系管理科室审批");
                    }
                    else
                    {
                        return Content("OK|变更申请失败");
                    }
                }
            }
            if (s >= 2)
            {
                return Content("OK|变更成功请重新刷卡验证");
            }
            else
            {
                return Content("OK|变更失败");
            }
        }


	}
}