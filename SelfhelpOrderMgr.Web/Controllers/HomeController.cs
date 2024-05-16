
using MeetASRConsoleApp.Models;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class HomeController : BaseMenuController
    {
        private BaseDapperBLL _baseDapperBLL = new BaseDapperBLL();
        public ActionResult Index()
        {
            T_SHO_ManagerSet cyMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByCy");
            T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByArea");

            ViewData["cyFlag"] = cyMset != null ? cyMset.KeyMode.ToString() : "0";
            ViewData["areaFlag"] = areaMset != null ? areaMset.KeyMode.ToString() : "1";


            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["loginMode"] = loginMode.MgrValue;
            return View();
        }

        public ActionResult GoodsView()
        {
            List<T_GoodsType> types = (List<T_GoodsType>)new T_GoodsTypeBLL().GetListOfIEnumerable("");
            ViewData["types"] = types;
            //string firstType="";
            //foreach(T_GoodsType type in types)
            //{
            //    if(type.LevelNo==2)
            //    {
            //        firstType = type.Fcode;
            //        break;
            //    }
            //}

            //List<T_GoodsType> mytypes = new List<T_GoodsType>();
            //foreach (T_GoodsType type in types)
            //{
            //    if (type.LevelNo == 1)
            //    {
            //        mytypes.Add(type);
            //        foreach (T_GoodsType stype in types)
            //        {
            //            if (stype.LevelNo == 2 && stype.FTypeCode==type.Fcode)
            //            {
            //                mytypes.Add(stype);
            //            }
            //        }
            //    }
            //}
            //ViewData["types"] = mytypes;
            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("Active='Y'");
            ViewData["goods"] = goods;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult test1()
        {
            return View();
        }

        public ActionResult Login()
        {
            string id="0";
            T_SHO_ManagerSet LoginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            if (LoginMode!=null)
            {
                id = LoginMode.MgrValue.ToString();
            }
            ViewData["LoginMode"] = id;
            ViewData["managerCardNo"] =Request["managerCardNo"];

            //List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("Flag=0 and Bankflag=1 and CAmount>0 and SendDate<'" + DateTime.Today.ToString() + "'");
            //ViewData["vcrdCount"] = vcrds.Count;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select isnull(count(1),0) fcount from t_Vcrd where Flag=0 and Bankflag=1 and CAmount>0 and SendDate<'" + DateTime.Today.ToString() + "'");
            if (dt == null)
            {
                ViewData["vcrdCount"] = 0;
            }
            if (dt.Rows.Count > 0)
            {
                ViewData["vcrdCount"] = dt.Rows[0][0];
            }

            

            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        //验证系统登录
        //[MyLogActionFilterAttribute]
        public ActionResult LoginCheck()
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string managerCardNo = Request["managerCardNo"];
            List<T_CZY> users = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            string status = "Error|无效的管理卡";
            if (users.Count > 0)
            {
                string strCookieLogin = "";
                T_SHO_ManagerSet checkUserLoginModeMgr = new T_SHO_ManagerSetBLL().GetModel("CheckLoginSeccionOrCookie");
                if (checkUserLoginModeMgr != null)
                {
                    if (checkUserLoginModeMgr.MgrValue == "1")
                    {
                        //创建Cookie
                        HttpCookie cookie = new HttpCookie("loginUserName", users[0].FName);
                        cookie.Expires = DateTime.Now.AddHours(4);
                        //写入Cookie
                        Response.Cookies.Set(cookie);
                        strCookieLogin = "COOKIE";
                        status = "OK|开启Cookie登录模式成功";
                    }
                   
                }

                //如果不是Cookie模式登录，再采用Seccion方式登录
                if (string.IsNullOrEmpty(strCookieLogin))
                {
                    T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("OpenMode");
                    T_CZY user = users[0];
                    if (string.IsNullOrEmpty(user.FManagerCard) == false)
                    {
                        if (mgr.KeyMode == 1)
                        {
                            //HttpCookie cookie = new HttpCookie("person_Users");
                            //cookie.Values.Add("userLoginCard", user.FManagerCard);
                            //cookie.Expires = DateTime.Now.AddHours(10);                        
                            //System.Web.HttpContext.Current.Response.AppendCookie(cookie);


                            T_SHO_ManagerSet oldmgr = new T_SHO_ManagerSetBLL().GetModel(ip);
                            bool bfalse;
                            if (oldmgr != null)
                            {
                                oldmgr.StartTime = DateTime.Now;
                                oldmgr.MgrValue = managerCardNo;
                                bfalse = new T_SHO_ManagerSetBLL().Update(oldmgr);
                            }
                            else
                            {
                                T_SHO_ManagerSet newmgr = new T_SHO_ManagerSet();
                                newmgr.KeyName = ip;
                                newmgr.KeyMode = 1;
                                newmgr.StartTime = DateTime.Now;
                                newmgr.MgrName = "分别开启";
                                newmgr.MgrValue = user.FManagerCard;
                                newmgr.Remark = "";
                                new T_SHO_ManagerSetBLL().Add(newmgr);
                            }

                            status = "OK|开启分别消费模式成功";
                        }
                        else if (mgr.KeyMode == 2)
                        {
                            mgr.StartTime = DateTime.Now;
                            if (new T_SHO_ManagerSetBLL().Update(mgr))
                            {
                                status = "OK|开启集中消费模式成功";
                            }
                            else
                            {
                                status = "Error|开启集中消费模式成功";
                            }
                        }
                    }
                }
                T_SysOperationLog log = new T_SysOperationLog()
                {
                    ControlName = "Home",
                    ActionName = "LoginCheck",
                    CrtDate = DateTime.Now,
                    Remark = "消费开启登录",
                    ReqJson = managerCardNo,
                    RtnJson = status +",IP:"+ip,
                    UserCode = users[0].FName
                };
                new BaseDapperBLL().Insert<T_SysOperationLog>(log);
            }
            Log4NetHelper.logger.Info("消费登录开启,操作员：" + managerCardNo + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + ",登录结果：" + status);

            

            return Content(status);
        }//消费登录开启，管理卡模式

        //[MyLogActionFilterAttribute]
        public ActionResult UserSignInCheck()
        {
            //string managerCardNo = Request["managerCardNo"];
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string userName = Request["userName"];
            string userPwd = Request["userPwd"];
            if(string.IsNullOrEmpty(userName))
            {
                return Content("Error|用户名不能为空");
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                return Content("Error|密码不能为空");
            }
            List<T_CZY> users = new T_CZYBLL().GetModelList("(FName='" + userName + "' or FCode='" + userName + "') and FPWD='" + userPwd + "'");
            string status = "Error|用户名或是密码信息不正确";
            if (users.Count > 0)
            {
                string strCookieLogin = "";
                T_SHO_ManagerSet checkUserLoginModeMgr = new T_SHO_ManagerSetBLL().GetModel("CheckLoginSeccionOrCookie");
                if (checkUserLoginModeMgr != null)
                {
                    if (checkUserLoginModeMgr.MgrValue == "1")
                    {
                        //创建Cookie
                        HttpCookie cookie = new HttpCookie("loginUserName", userName);
                        cookie.Expires = DateTime.Now.AddHours(4);
                        //写入Cookie
                        Response.Cookies.Set(cookie);
                        strCookieLogin = "COOKIE";
                        status = "OK|开启Cookie登录模式成功";
                    }
                }

                //如果不是Cookie模式登录，再采用Seccion方式登录
                if (string.IsNullOrEmpty(strCookieLogin))
                {
                    T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("OpenMode");
                    T_CZY user = users[0];
                    if (string.IsNullOrEmpty(user.FManagerCard) == false)
                    {
                        if (mgr.KeyMode == 1)//分别开启
                        {
                            T_SHO_ManagerSet oldmgr = new T_SHO_ManagerSetBLL().GetModel(ip);
                            bool bfalse;
                            if (oldmgr != null)
                            {
                                oldmgr.StartTime = DateTime.Now;
                                oldmgr.MgrValue = user.FManagerCard;
                                bfalse = new T_SHO_ManagerSetBLL().Update(oldmgr);
                            }
                            else
                            {
                                T_SHO_ManagerSet newmgr = new T_SHO_ManagerSet();
                                newmgr.KeyName = ip;
                                newmgr.KeyMode = 1;
                                newmgr.StartTime = DateTime.Now;
                                newmgr.MgrName = "分别开启";
                                newmgr.MgrValue = user.FManagerCard;
                                newmgr.Remark = "";
                                new T_SHO_ManagerSetBLL().Add(newmgr);
                            }

                            status = "OK|开启分别消费模式成功";
                        }
                        else if (mgr.KeyMode == 2)//集中开启
                        {
                            mgr.StartTime = DateTime.Now;
                            if (new T_SHO_ManagerSetBLL().Update(mgr))
                            {
                                status = "OK|开启集中消费模式成功";
                            }
                            else
                            {
                                status = "Error|开启集中消费模式成功";
                            }
                        }

                        Session["loginUserName"] = userName; ;
                        
                    }
                }
                T_SysOperationLog log = new T_SysOperationLog()
                {
                    ControlName = "Home",
                    ActionName = "UserSingCheck",
                    CrtDate = DateTime.Now,
                    Remark = "消费开启登录,IP:"+ ip,
                    ReqJson = userName,
                    RtnJson = status,
                    UserCode = userName
                };
                new BaseDapperBLL().Insert<T_SysOperationLog>(log);
            }
            Log4NetHelper.logger.Info("消费登录开启,操作员：" + userName + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + ",登录结果：" + status);

            return Content(status);
        }//消费登录，用户名密码方式

        public ActionResult GetExitSystemPwd()
        {
            try
            { 
            T_SHO_ManagerSet ExitSystemPwd = new T_SHO_ManagerSetBLL().GetModel("ExitSystemPwd");
            return Content("OK|" + ExitSystemPwd.MgrValue);
            }
            catch {
                return Content("Err|获取失败");
            }
            
        }

        public ActionResult PrintCkeck(int id=100)//打印小票
        {
            string SaleMode = Request["SaleMode"];//销售模式
            if (string.IsNullOrEmpty(SaleMode))
            {
                SaleMode = "";
            }
            ViewData["SaleMode"] = SaleMode;
            ViewData["id"] = id;

            ViewData["managerCardNo"] = Request["managerCardNo"];

            string strMode = "0";
            T_SHO_ManagerSet LoginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            if (LoginMode != null)
            {
                strMode = LoginMode.MgrValue.ToString();
            }
            ViewData["LoginMode"] = strMode;



            return View("PrintCkeck");
        }
        //[MyLogActionFilterAttribute]
        public ActionResult PrintCkeckLogin()//打印小票登录验证
        {
            string managerCardNo = Request["managerCardNo"];
            string status = MgrCardLogin(managerCardNo);
            return Content(status);
        }

        /// <summary>
        /// 管理卡登录
        /// </summary>
        /// <param name="managerCardNo"></param>
        /// <returns></returns>
        private string MgrCardLogin(string managerCardNo)
        {
            List<T_CZY> users = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            string status = "Error|无效的管理卡";
            if (users.Count > 0)
            {
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                Log4NetHelper.logger.Info("小票打印管理卡方式登录,操作员：" + managerCardNo + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");

                T_CZY user = users[0];

                //设定登录Cookie记录
                SetLoginCookieInfo(user);
                T_SysOperationLog log = new T_SysOperationLog()
                {
                    ControlName = "Home",
                    ActionName = "PrintCkeckSignIn",
                    CrtDate = DateTime.Now,
                    Remark = "消费查询登录" + ",IP:" + ip,
                    ReqJson = managerCardNo,
                    RtnJson = "OK|登录成功",
                    UserCode = user.FName
                };
                new BaseDapperBLL().Insert<T_SysOperationLog>(log);
                status = "OK|登录成功";
            }

            return status;
        }

        //[MyLogActionFilterAttribute]
        public ActionResult PrintCkeckSignIn()//打印小票用户密码登录验证
        {
            string userName = Request["userName"];
            string userPwd = Request["userPwd"];
            if(string.IsNullOrEmpty(userName))
            {
                return Content("Error|用户名不能为空");
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                return Content("Error|密码不能为空");
            }

            List<T_CZY> users = new T_CZYBLL().GetModelList("(FName='" + userName + "' or FCode='" + userName + "') and FPWD='" + userPwd + "'");
            string status = "Error|用户名和密码不正确";
            if (users.Count == 1)
            {
                T_CZY user = users[0];

                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                Log4NetHelper.logger.Info("小票打印用户名方式登录,操作员：" + userName + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");

                //设定登录Cookie记录
                SetLoginCookieInfo(user);
                T_SysOperationLog log = new T_SysOperationLog()
                {
                    ControlName = "Home",
                    ActionName = "PrintCkeckSignIn",
                    CrtDate = DateTime.Now,
                    Remark = "消费查询登录" + ",IP:" + ip,
                    ReqJson = "操作员：" + userName,
                    RtnJson = "OK",
                    UserCode = userName
                };
                new BaseDapperBLL().Insert<T_SysOperationLog>(log);
                status = "OK|" + users[0].FManagerCard.ToString();
            }
            return Content(status);
        }

        private void SetLoginCookieInfo(T_CZY user)
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
        }

        public ActionResult PrintXiaoPiao()//开始打印小票
        {
            string strMode = "0";
            T_SHO_ManagerSet LoginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            if (LoginMode != null)
            {
                strMode = LoginMode.MgrValue.ToString();
            }
            ViewData["LoginMode"] = strMode;

            

            string printFlag = Request["printFlag"];
            string managerCardNo = Request["managerCardNo"];

            //如果是人脸，则模拟管理登录
            if (LoginMode.MgrValue == "2")
            {
                this.MgrCardLogin(managerCardNo);
            }

            if (string.IsNullOrEmpty(printFlag)==true)
            {
                return View("PrintCkeck");
            }
            else if ("LoginOK122342124121123131231122" != printFlag)
            {
                return View("PrintCkeck");
            }
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            if (string.IsNullOrEmpty(startTime) == true)
            {
                startTime = DateTime.Today.ToShortDateString();
            }
            if (string.IsNullOrEmpty(endTime) == true)
            {
                endTime = DateTime.Now.ToShortDateString();
                endTime = endTime + " " + DateTime.Now.ToShortTimeString();
            }
            List<T_Invoice> invoices = new T_InvoiceBLL().GetModelList(10, "OrderDate>='" + startTime + "' and OrderDate<'" + endTime + "' and Flag=1 and FAreaCode in(select a.fareacode from t_czy_area a,t_czy b where a.fcode=b.fcode and a.fflag=2 and b.FManagerCard='" + managerCardNo + "')", "InvoiceNo");
            ViewData["invoices"] = invoices;

            string invoiceNos = "";
            foreach(T_Invoice invoice in invoices)
            {
                if(invoiceNos=="")
                {
                    invoiceNos = invoice.InvoiceNo;
                }
                else
                {
                    invoiceNos =invoiceNos +"|"+ invoice.InvoiceNo;
                }
            }
            ViewData["invoiceNos"] = invoiceNos;
            List<T_AREA> areas = new T_AREABLL().GetModelList(" FCode in(select a.fareacode from t_czy_area a,t_czy b where a.fcode=b.fcode and a.fflag=2 and b.FManagerCard='" + managerCardNo + "')");
            ViewData["areas"] = areas;

            //List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1");
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1 and not(PType like '%积分%')");
            ViewData["saleTypes"] = saleTypes;

            List<T_GoodsType> goodsTypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["goodsTypes"] = goodsTypes;

            ViewData["managerCardNo"] = managerCardNo;

            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("XiaoPiaoStyle");
            ViewData["mset"] = mset;


            T_SHO_ManagerSet printPlusVer = new T_SHO_ManagerSetBLL().GetModel("PrintPlusVer");
            if (printPlusVer == null || printPlusVer.MgrValue == "0")
            {
                ViewData["PrintPlusVer"] = "0";
            }
            else
            {
                ViewData["PrintPlusVer"] = "1";
            }


            return View();

        }

        //更新打印小票次数
        public ActionResult UpdatePrintCount()
        {
            string strInvoices = Request["invoices"];
            char ee = (char)124;
            string[] invoices = strInvoices.Split(ee);
            string myStrInvoices = "'" + strInvoices.Replace("|", "','") + "'";
            if (new T_InvoiceBLL().UpdatePrintCount(myStrInvoices))
            {
                return Content("OK|更新打印次数成功");
            }
            else
            {
                return Content("OK|更新打印次数成功");
            }

        }
        public ActionResult GetSearchInvoices()
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            string FDate = Request["FDate"];
            string managerCardNo = Request["managerCardNo"];
            string FSaleType = Request["FSaleType"];

            if (string.IsNullOrEmpty(FSaleType))
            {
                return Content("Err|消费类不能为空，请选择一个消费类型");
            }
            if (string.IsNullOrEmpty(managerCardNo))
            {
                return Content("Err|管理卡号不能为空，请联系民警");
            }
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            if (czys.Count <= 0)
            {
                return Content("Err|管理卡不存在，请更换一张有效的管理卡");
            }
            DateTime sDate;
            try
            {
                sDate = Convert.ToDateTime(FDate);
            }
            catch
            {
                sDate = DateTime.Today;
            }
            
            if (string.IsNullOrEmpty(startTime) == true)
            {
                startTime = "0";
            }
            if (string.IsNullOrEmpty(endTime) == true)
            {
                endTime = "23";
            }
            string strWhere = "OrderDate>='" + sDate.Year.ToString() + "-" + sDate.Month.ToString() + "-" + sDate.Day.ToString() +
                " " + startTime + ":00:00" + "' and OrderDate<'" + sDate.Year.ToString() + "-" + sDate.Month.ToString() + "-" + sDate.Day.ToString() +
                " " + endTime + ":59:59" + "'";
            if (string.IsNullOrEmpty(areaName) == false)
            {
                strWhere = strWhere + " and FAreaName='" + areaName + "'";
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and FCriminal like'%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strWhere = strWhere + " and FCrimeCode='" + FCode + "'";
            }
            if (string.IsNullOrEmpty(FSaleType) == false)
            {
                strWhere = strWhere + " and TypeFlag=" + FSaleType + "";
            }
            strWhere = strWhere + " and FCrimeCode in( select c.fcode from t_czy_area a,t_czy b,t_criminal c where a.fcode=b.fcode and c.fareaCode=a.fareacode and a.fflag=2 and b.FManagerCard='" + managerCardNo + "' )";

            

            List<T_Invoice> invoices = new T_InvoiceBLL().GetModelList(1000, strWhere + " and Flag=1", "FAreaCode,RoomNo,FCriminal");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Content("OK|"+jss.Serialize(invoices));
        }
        public ActionResult GetInvoices()
        {
            try
            {
                string strInvoices = Request["invoices"];
                char ee = (char)124;
                string[] invoices = strInvoices.Split(ee);
                List<PrintInvoices> rtnInvs = new List<PrintInvoices>();
                if (invoices.Length > 0)
                {
                    for (int i = 0; i < invoices.Length; i++)
                    {
                        PrintInvoices rtnInv = new PrintInvoices();
                        rtnInv.invoice = new T_InvoiceBLL().GetModel(invoices[i]);
                        T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");
                        if (mgr.MgrValue == "1")
                        {
                            rtnInv.details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoices[i] + "'", 1);
                        }
                        else
                        {
                            rtnInv.details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoices[i] + "'");
                        }
                        rtnInv.criminal = new T_CriminalBLL().GetCriminalXE_info(rtnInv.invoice.FCrimeCode, 1);
                        //2022年改为到账户清单中统计余额
                        T_Criminal_card crimeBalance = new T_CriminalBLL().GetInvoiceBalance(rtnInv.invoice.FCrimeCode, rtnInv.invoice.InvoiceNo);
                        rtnInv.criminal.AmountAmoney = crimeBalance.AmountA;
                        rtnInv.criminal.AmountBmoney = crimeBalance.AmountB;
                        rtnInv.criminal.AmountCmoney = crimeBalance.AmountC;

                        rtnInvs.Add(rtnInv);
                    }
                }
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(rtnInvs));
            }
            catch (Exception ex)
            {
                return Content("Err|"+ ex.Message);
                throw;
            }
            
        }

        //取消订单按订单号
        [MyLogActionFilterAttribute]
        public ActionResult CancelOrder(string InvoiceNo, string managerCardNo)
        {
            string rtnInfo = "Err|订单取消失败";
            //string InvoiceNo = Request["InvoiceNo"];
            //string managerCardNo = Request["managerCardNo"];
            if (string.IsNullOrEmpty(managerCardNo)==true)
            {
                return Content("Err|没有管理卡不能取消订单");
            }
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            if(czys.Count<=0)
            {
                return Content("Err|管理卡不存在，请更换一张有效的管理卡");
            }
            
            string crtUser ="";
            if (string.IsNullOrEmpty(czys[0].FUserChinaName) == true)
            {
                crtUser = czys[0].FName;
            }
            else
            {
                crtUser = czys[0].FUserChinaName;
            }
            if(string.IsNullOrEmpty(InvoiceNo)==false)
            {
                //查询订单状态                
                T_Invoice invoice = new T_InvoiceBLL().GetModel(InvoiceNo);

                //判断是管理卡是否有权限改
                List<T_Czy_area> czrareas = new T_Czy_areaBLL().GetModelList("FCode ='" + czys[0].FCode + "' and FFlag=2 and FAreaCode in( select FareaCode from t_criminal where fcode='" + invoice.FCrimeCode + "' )");
                if (czrareas.Count == 0)
                {
                    return Content("Err|您没有该犯人的管理权限，请与本队别民警联系，谢谢");
                }

                //增加必须是本人才能取消订单  by zenglj 2017-05-16
                //判断是不是原来的操作员，不是则不能取消
                //if (czys[0].FName != invoice.Crtby && czys[0].FPRIVATE==0)
                //{
                //    return Content("Err|您没有取消该订单的管理权限，请与本监区民警联系，谢谢");

                //}
                

                if(invoice.Checkflag==0)
                {
                    if (invoice.Flag==0)
                    {
                        return Content( "Err|不用取消，该订单已经被取消了");
                    }
                    List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("OrigId='"+ InvoiceNo +"'");
                    try
                    {
                        if (vcrds[0].CrtDate < DateTime.Today)
                        {
                            T_SHO_ManagerSet mgrSetCancel = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCancelOrderFlag");
                            if (mgrSetCancel == null || mgrSetCancel.MgrValue=="0")
                            {
                                rtnInfo = "Err|对不起，当天就不能取消或撤单";
                                return Content(rtnInfo);
                            }
                        }
                        switch(vcrds[0].BankFlag)
                        {
                            case 0:
                                {
                                    rtnInfo = "Err|不能取消，订单已经配货了";
                                    T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCheckFlag");
                                    if (mgrSet != null)
                                    {
                                        if (Convert.ToInt32(mgrSet.MgrValue) == vcrds[0].CheckFlag)
                                        {
                                            rtnInfo = StartCancelOrder(rtnInfo, InvoiceNo, crtUser);
                                        }
                                    }
                                }break;
                            case 1:
                                {
                                    rtnInfo = "Err|不能取消订单已发送到银行扣款";
                                } break;
                            case 2:
                                {
                                    rtnInfo = "Err|不能取消订单,已在银行扣款成功了";
                                } break;
                            case -1:
                                {
                                    rtnInfo = "Err|不能取消订单,已在银行扣款,但未成功";
                                    
                                } break;
                            default:
                                rtnInfo = "Err|不能取消，订单已扣款";
                                break;
                        }
                    }
                    catch
                    {
                        rtnInfo = "Err|订单失败,程序执行时错误";
                    }

                }
                else
                {
                    rtnInfo = "Err|订单已经配货了不能取消";
                }

            }

            return Content(rtnInfo);
        }

        //执行取消订单动作
        private static string StartCancelOrder(string rtnInfo, string InvoiceNo, string crtUser)
        {
            //开始取消定单
            if (new T_InvoiceBLL().CancelOrderById(InvoiceNo, crtUser))
            {
                rtnInfo = "OK|订单成功取消了";
            }
            else
            {
                rtnInfo = "Err|取消失败，执行取消过程失败";
            }
            return rtnInfo;
        }

        //修改订单，取消下单回到未下单状态 ChangeOrder
        [MyLogActionFilterAttribute]
        public ActionResult ChangeOrder(string InvoiceNo,string managerCardNo)
        {
            string rtnInfo = "Err|订单退回改修失败";
            //string InvoiceNo = Request["InvoiceNo"];
            //string managerCardNo = Request["managerCardNo"];
            if (string.IsNullOrEmpty(managerCardNo) == true)
            {
                return Content("Err|没有管理卡不能改单");
            }
            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            if (czys.Count <= 0)
            {
                return Content("Err|管理卡不存在，请更换一张有效的管理卡");
            }

            string crtUser = "";
            if (string.IsNullOrEmpty(czys[0].FUserChinaName) == true)
            {
                crtUser = czys[0].FName;
            }
            else
            {
                crtUser = czys[0].FUserChinaName;
            }
            if (string.IsNullOrEmpty(InvoiceNo) == false)
            {
                //查询订单状态                
                T_Invoice invoice = new T_InvoiceBLL().GetModel(InvoiceNo);

                //判断是管理卡是否有权限改
                List<T_Czy_area> czrareas = new T_Czy_areaBLL().GetModelList("FCode ='" + czys[0].FCode + "' and FFlag=2 and FAreaCode in( select FareaCode from t_criminal where fcode='" + invoice.FCrimeCode + "' )");
                if (czrareas.Count==0)
                {
                    return Content("Err|您没有该犯人的管理权限，请与本队别民警联系，谢谢");
                }

                //增加必须是本人才能取消订单  by zenglj 2017-05-16
                //判断是不是原来的操作员，不是则不能取消
                if (czys[0].FName != invoice.Crtby && czys[0].FPRIVATE == 0)
                {
                    return Content("Err|您没有取消该订单的管理权限，请与本队别民警联系，谢谢");

                }

                if (invoice.Checkflag == 0)
                {
                    if (invoice.Flag == 0)
                    {
                        return Content("Err|不用改，该订单已经被取消了");
                    }
                    List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("OrigId='" + InvoiceNo + "'");
                    try
                    {
                        if (vcrds[0].CrtDate < DateTime.Today)
                        {
                            T_SHO_ManagerSet mgrSetCancel = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCancelOrderFlag");
                            if (mgrSetCancel == null || mgrSetCancel.MgrValue == "0")
                            {
                                rtnInfo = "Err|对不起，当天就不能取消或撤单";
                                return Content(rtnInfo);
                            }
                        }
                        switch (vcrds[0].BankFlag)
                        {
                            case 0://需要区分下两种情况
                                {
                                    rtnInfo = "Err|不能改单，订单已经配货了";
                                    T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCheckFlag");
                                    if (mgrSet!=null)
                                    {
                                        if (Convert.ToInt32(mgrSet.MgrValue) == vcrds[0].CheckFlag)
                                        {
                                            rtnInfo = StartChangeOrder(rtnInfo, InvoiceNo, crtUser);
                                        }
                                    }                                    
                                } break;
                            case 1:
                                {
                                    rtnInfo = "Err|不能改单，订单已发送到银行扣款";
                                } break;
                            case 2:
                                {
                                    rtnInfo = "Err|不能改单,已在银行扣款成功了";
                                } break;
                            case -1:
                                {
                                    rtnInfo = "Err|不能改单,已在银行扣款,但未成功";
                                    

                                } break;
                            default:
                                rtnInfo = "Err|不能改单，订单已扣款";
                                break;
                        }
                    }
                    catch
                    {
                        rtnInfo = "Err|订单失败,程序执行时错误";
                    }

                }
                else
                {
                    rtnInfo = "Err|订单已经配货了不能取消";
                }

            }

            return Content(rtnInfo);
        }

        //执行修改订单动作
        private static string StartChangeOrder(string rtnInfo, string InvoiceNo, string crtUser)
        {
            //开始改单定单
            if (new T_InvoiceBLL().ChangeOrderById(InvoiceNo, crtUser))
            {
                rtnInfo = "OK|订单成功改单退回，现在可以去修改下单内容了";
            }
            else
            {
                rtnInfo = "Err|改单失败，执行改单过程失败";
            }
            return rtnInfo;
        }

        //消费统计查询
        public ActionResult XiaoFeiTongji()
        {
            string tjStartDate = Request["tjStartDate"];
            string tjEndDate = Request["tjEndDate"];
            string tjFAreaName = Request["tjFAreaName"];
            string managerCardNo = Request["managerCardNo"];
            string tjSPShortCode = Request["tjSPShortCode"];
            string tjRoomNo = Request["tjRoomNo"];
            //string strFsn = Request["FBidExcel"];
            string roomNoFlag = Request["roomNoFlag"];

            string tjFSaleType = Request["tjFSaleType"];

            List<T_CZY> czys = new T_CZYBLL().GetModelList("FManagerCard='" + managerCardNo + "'");
            if(czys.Count==0)
            {
                return Content("Err|管理卡不能为空");
            }
            
            if (string.IsNullOrEmpty(tjFSaleType))
            {
                return Content("Err|消费类不能为空，请选择一个消费类型");
            }
            
            if (string.IsNullOrEmpty(tjStartDate))
            {
                return Content("Err|开始日期不能为空");
            }
            if (string.IsNullOrEmpty(tjEndDate))
            {
                return Content("Err|结束日期不能为空");
            }
            //if (string.IsNullOrEmpty(tjFAreaName))
            //{
            //    return Content("Err|统计队别不能为空");
            //}

            tjStartDate = tjStartDate.Substring(6, 4) + "-" + tjStartDate.Substring(0, 2) + "-" + tjStartDate.Substring(3, 2);
            tjEndDate = tjEndDate.Substring(6, 4) + "-" + tjEndDate.Substring(0, 2) + "-" + tjEndDate.Substring(3, 2);

            if (string.IsNullOrEmpty(roomNoFlag))
            {
                roomNoFlag = "0";
            }
            
            StringBuilder strSql = new StringBuilder();

            if (string.IsNullOrEmpty(tjFAreaName) == false)
            {                
                
                if (roomNoFlag == "0")
                {
                    strSql.Append(@"select b.FAreaName FAreaName,'' RoomNo,a.SPShortCode SPShortCode
                        ,a.gname GName,'' Remark
                        ,a.gtxm GTXM,sum(a.qty) FCount,sum(a.amount) FMoney 
                        from t_invoicedtl a,t_invoice b
                                        where a.invoiceno=b.invoiceno and a.flag=1
                        and a.OrderDate>='" + tjStartDate +" 00:00:00' and a.OrderDate<'"+ tjEndDate +@" 23:59:00'
                        and b.FAreaCode='"+ tjFAreaName +@"'");
                    if (string.IsNullOrEmpty(tjSPShortCode) == false)
                    {
                        strSql.Append(" and a.SPShortCode='" + tjSPShortCode + "'");
                    }
                    if (string.IsNullOrEmpty(tjRoomNo) == false)
                    {
                        strSql.Append(" and b.RoomNo='" + tjRoomNo + "'");
                    }
                    if (string.IsNullOrEmpty(tjFSaleType) == false)
                    {
                        strSql.Append(" and b.TypeFlag=" + tjFSaleType + "");
                    }                    
                    strSql.Append(" group by b.FAreaName,a.SPShortCode,a.gname,a.gtxm");
                }
                else
                {
                    strSql.Append(@"select b.FAreaName FAreaName,isnull(b.roomNo,'') RoomNo
                        ,a.SPShortCode SPShortCode,a.gname GName
                        ,isnull(a.Remark,'') Remark,a.gtxm GTXM
                        ,sum(a.qty) FCount,sum(a.amount) FMoney 
                        from t_invoicedtl a,t_invoice b
                                        where a.invoiceno=b.invoiceno and a.flag=1
                        and a.OrderDate>='" + tjStartDate + " 00:00:00' and a.OrderDate<'" + tjEndDate + @" 23:59:00'
                        and b.FAreaCode='" + tjFAreaName + @"'");
                    if (string.IsNullOrEmpty(tjSPShortCode)==false)
                    {
                        strSql.Append(" and a.SPShortCode='" + tjSPShortCode + "'");
                    }
                    if (string.IsNullOrEmpty(tjRoomNo) == false)
                    {
                        strSql.Append(" and b.RoomNo='" + tjRoomNo + "'");
                    }
                    if (string.IsNullOrEmpty(tjFSaleType) == false)
                    {
                        strSql.Append(" and b.TypeFlag=" + tjFSaleType + "");
                    }
                    if (string.IsNullOrEmpty(czys[0].FCode))
                    {
                        strSql.Append(" and b.FAreaCode in(select fareacode from t_czy_area where fflag=2 and fcode='" + czys[0].FCode + "')");
                    }
                    strSql.Append("group by b.FAreaName,isnull(b.roomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                }

                //return Content("Err|556654不能为空|" + strSql.ToString());
                
            }
            else
            {   //如果不选监区
                strSql.Append(@"select '' FAreaName,'' RoomNo,a.SPShortCode SPShortCode
                    ,a.gname GName,isnull(a.Remark,'') Remark
                    ,a.gtxm GTXM,sum(a.qty) FCount,sum(a.amount) FMoney 
                    from t_invoicedtl a,t_invoice b");
                strSql.Append(@"  where a.invoiceno=b.invoiceno and a.flag=1");
                strSql.Append(@" and a.OrderDate>='" + tjStartDate + " 00:00:00' and a.OrderDate<'" + tjEndDate + @" 23:59:00'");
                if (string.IsNullOrEmpty(tjSPShortCode) == false)
                {
                    strSql.Append(" and a.SPShortCode='" + tjSPShortCode + "'");
                }
                if (string.IsNullOrEmpty(tjFSaleType) == false)
                {
                    strSql.Append(" and b.TypeFlag=" + tjFSaleType + "");
                }
                strSql.Append(@" group by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                strSql.Append(@" order by a.SPShortCode");
            }
            List<PeihuoDanPrintList> phds = new CommTableInfoBLL().GetListData(strSql.ToString());
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Content("OK|" + roomNoFlag + "|" + jss.Serialize(phds));
            
        }

        //消费明细查询
        public ActionResult xfMingxiSearch()
        {
            string mxStartDate = Request["mxStartDate"];
            string mxEndDate = Request["mxEndDate"];
            string mxFAreaName = Request["mxFAreaName"];
            string managerCardNo = Request["managerCardNo"];
            string mxSPShortCode = Request["mxSPShortCode"];
            //string strFsn = Request["FBidExcel"];
            string mxFGoodsType = Request["mxFGoodsType"];

            string mxFCrimeCode = Request["mxFCrimeCode"];
            string mxFSaleType = Request["mxFSaleType"];
            string mxFAreas="sfewreree";
            if (string.IsNullOrEmpty(managerCardNo))
            {
                return Content("Err|没有刷管理卡，不能查询");
            }
            else
            {
                List<T_Czy_area> areas = new T_Czy_areaBLL().GetModelList("FFlag=2 and FCode in(select fcode from t_czy where FManagerCard='" + managerCardNo + "')");
                if(areas.Count>0)
                {
                    mxFAreas = "";
                    foreach (T_Czy_area item in areas)
                    {
                        if(mxFAreas=="")
                        {
                            mxFAreas = "'" + item.fareacode + "'";
                        }
                        else
                        {
                            mxFAreas = mxFAreas+",'" + item.fareacode + "'";
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(mxFSaleType))
            {
                return Content("Err|消费类不能为空，请选择一个消费类型");
            }

            if (string.IsNullOrEmpty(mxStartDate))
            {
                return Content("Err|开始日期不能为空");
            }
            if (string.IsNullOrEmpty(mxEndDate))
            {
                return Content("Err|结束日期不能为空");
            }

            mxStartDate = mxStartDate.Substring(6, 4) + "-" + mxStartDate.Substring(0, 2) + "-" + mxStartDate.Substring(3, 2);
            mxEndDate = mxEndDate.Substring(6, 4) + "-" + mxEndDate.Substring(0, 2) + "-" + mxEndDate.Substring(3, 2);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.FAreaName,b.RoomNo,b.FCriminal,a.SPShortCode,a.GName,isnull(a.Remark,'') Remark,sum(a.qty) Qty,sum(a.Amount) Amount ");
            strSql.Append(" from t_invoicedtl a,t_invoice b ");
            strSql.Append(" where a.invoiceno=b.invoiceno and b.flag=1");
            if (string.IsNullOrEmpty(mxStartDate)==false && string.IsNullOrEmpty(mxEndDate)==false)
            {
                strSql.Append(" and b.OrderDate>='" + mxStartDate + " 00:00:00' and b.OrderDate<'" + mxEndDate + " 23:59:00'");
            }
            if (string.IsNullOrEmpty(mxFSaleType)==false)
            {
                strSql.Append(" and b.TypeFlag=" + mxFSaleType +"");
            }
            if (string.IsNullOrEmpty(mxFAreaName) == false)
            {
                strSql.Append(" and b.FAreaCode='" + mxFAreaName + "'");
            }

            if (string.IsNullOrEmpty(mxSPShortCode) == false)
            {
                strSql.Append(" and a.SPShortCode='" + mxSPShortCode + "'");
            }

            if (string.IsNullOrEmpty(mxFCrimeCode) == false)
            {
                strSql.Append(" and a.FCrimeCode='" + mxFCrimeCode + "'");
            }

            if (string.IsNullOrEmpty(mxFAreas) == false)
            {
                strSql.Append(" and b.FAreaCode in(" + mxFAreas + ")");
            }
            if (string.IsNullOrEmpty(mxFGoodsType)==false)
            {
                strSql.Append(" and a.SPShortCode in(select spshortCode from t_goods where gtype='" + mxFGoodsType + "')");
            }
            //strSql.Append("");
            strSql.Append(" group by b.FAreaName,b.RoomNo,b.FCriminal,a.SPShortCode,a.GName,isnull(a.Remark,'')");
            StringBuilder strCountSql = new StringBuilder();
            strCountSql.Append("select isnull(Count(*),0) fcount from (");
            strCountSql.Append(strSql.ToString());
            strCountSql.Append(" ) k");

            DataTable dt = new CommTableInfoBLL().GetDataTable(strCountSql.ToString());
            if(Convert.ToInt32( dt.Rows[0][0])>3000)
            {
                return Content("Err|您所查询的数据条数过多，请增加相应的查询条件");
            }

            strSql.Append(" order by b.FAreaName,b.RoomNo,b.FCriminal,a.SPShortCode,a.GName,isnull(a.Remark,'')");

            List<xfMingxi> xfmxs = new CommTableInfoBLL().GetXfMingxi(strSql.ToString());
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Content("OK|" + jss.Serialize(xfmxs));
        }

        //导出消费明细到Excel
        public ActionResult SearchXFMingxiExcelOut()
        {
            string mxStartDate = Request["mxStartDate"];
            string mxEndDate = Request["mxEndDate"];
            string mxFAreaName = Request["mxFAreaName"];
            string managerCardNo = Request["managerCardNo"];
            string mxSPShortCode = Request["mxSPShortCode"];
            //string strFsn = Request["FBidExcel"];
            string mxFGoodsType = Request["mxFGoodsType"];

            string mxFCrimeCode = Request["mxFCrimeCode"];
            string mxFSaleType = Request["mxFSaleType"];
            string mxFAreas = "sfewreree";
            if (string.IsNullOrEmpty(managerCardNo))
            {
                return Content("Err|没有刷管理卡，不能查询");
            }
            else
            {
                List<T_Czy_area> areas = new T_Czy_areaBLL().GetModelList("FFlag=2 and FCode in(select fcode from t_czy where FManagerCard='" + managerCardNo + "')");
                if (areas.Count > 0)
                {
                    mxFAreas = "";
                    foreach (T_Czy_area item in areas)
                    {
                        if (mxFAreas == "")
                        {
                            mxFAreas = "'" + item.fareacode + "'";
                        }
                        else
                        {
                            mxFAreas = mxFAreas + ",'" + item.fareacode + "'";
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(mxFSaleType))
            {
                return Content("Err|消费类不能为空，请选择一个消费类型");
            }

            if (string.IsNullOrEmpty(mxStartDate))
            {
                return Content("Err|开始日期不能为空");
            }
            if (string.IsNullOrEmpty(mxEndDate))
            {
                return Content("Err|结束日期不能为空");
            }

            mxStartDate = mxStartDate.Substring(6, 4) + "-" + mxStartDate.Substring(0, 2) + "-" + mxStartDate.Substring(3, 2);
            mxEndDate = mxEndDate.Substring(6, 4) + "-" + mxEndDate.Substring(0, 2) + "-" + mxEndDate.Substring(3, 2);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.FAreaName 队别,b.RoomNo 房号,b.FCriminal 姓名,a.fcrimecode 编号,c.BankAccNo 账号,a.SPShortCode 简码,a.GName 品名,isnull(a.Remark,'') 规格,d.SendDate 扣款日期,d.BankFlag 扣款状态,sum(a.qty) 数量,sum(a.Amount) 金额 ");
            strSql.Append(" from t_invoicedtl a,t_invoice b ");
            strSql.Append(" ,t_criminal_card c,(select distinct origid,bankflag,senddate from t_vcrd) d ");
            strSql.Append(" where a.invoiceno=b.invoiceno and b.flag=1");
            strSql.Append(" and a.invoiceno=d.origid and a.fcrimecode=c.fcrimecode ");
            if (string.IsNullOrEmpty(mxStartDate) == false && string.IsNullOrEmpty(mxEndDate) == false)
            {
                strSql.Append(" and b.OrderDate>='" + mxStartDate + " 00:00:00' and b.OrderDate<'" + mxEndDate + " 23:59:00'");
            }
            if (string.IsNullOrEmpty(mxFSaleType) == false)
            {
                strSql.Append(" and b.TypeFlag=" + mxFSaleType + "");
            }
            if (string.IsNullOrEmpty(mxFAreaName) == false)
            {
                strSql.Append(" and b.FAreaCode='" + mxFAreaName + "'");
            }

            if (string.IsNullOrEmpty(mxSPShortCode) == false)
            {
                strSql.Append(" and a.SPShortCode='" + mxSPShortCode + "'");
            }

            if (string.IsNullOrEmpty(mxFCrimeCode) == false)
            {
                strSql.Append(" and a.FCrimeCode='" + mxFCrimeCode + "'");
            }

            if (string.IsNullOrEmpty(mxFAreas) == false)
            {
                strSql.Append(" and b.FAreaCode in(" + mxFAreas + ")");
            }
            if (string.IsNullOrEmpty(mxFGoodsType) == false)
            {
                strSql.Append(" and a.SPShortCode in(select spshortCode from t_goods where gtype='" + mxFGoodsType + "')");
            }
            //strSql.Append("");
            strSql.Append(" group by b.FAreaName,b.RoomNo,b.FCriminal,a.SPShortCode,a.GName,isnull(a.Remark,''),a.fcrimecode,c.BankAccNo,d.SendDate,d.BankFlag");
            StringBuilder strCountSql = new StringBuilder();
            strCountSql.Append("select isnull(Count(*),0) fcount from (");
            strCountSql.Append(strSql.ToString());
            strCountSql.Append(" ) k");

            DataTable dt = new CommTableInfoBLL().GetDataTable(strCountSql.ToString());
            if (Convert.ToInt32(dt.Rows[0][0]) > 8000)
            {
                return Content("Err|您所查询的数据条数过多，请增加相应的查询条件");
            }

            strSql.Append(" order by b.FAreaName,b.RoomNo,b.FCriminal,a.SPShortCode,a.GName,isnull(a.Remark,''),a.fcrimecode,c.BankAccNo,d.SendDate,d.BankFlag");


            DataTable excelDT = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8("xfMingXi_List.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(excelDT, strFileName);
            return Content("OK|" + "xfMingXi_List.xls");
            
        }


        

        //public ActionResult CheckFace()
        public ActionResult CheckFace(string fcrimecode, string imageSrc, string faceMode = "0001", int loginCheck = 1)
        {

            //string fcrimecode = Request["smFCrimeCode"];
            //string faceMode = Request["faceMode"];
            //string strloginCheck = Request["loginCheck"];
            //string faceImage = Request["faceImage"];
            //string fcrimecode = Request["fcrimecode"];


            //string imageSrc

            //int loginCheck = 1;
            //if (!string.IsNullOrWhiteSpace(strloginCheck))
            //{
            //    loginCheck = Convert.ToInt32(strloginCheck);
            //}


            ResultInfo rs = new ResultInfo();

            if (string.IsNullOrWhiteSpace(faceMode))
            {
                faceMode = "0001";
            }
            string stringdata = "";

            //string imageSrc = Request["imageSrc"];  //改为参数提取
            if (string.IsNullOrWhiteSpace(imageSrc))
            {
                return Content("Err|图片不能为空");
            }

            T_Criminal _criminal = null;
            if (!string.IsNullOrWhiteSpace(fcrimecode))
            {
                _criminal = new BaseDapperBLL().QueryModel<T_Criminal>("fcode", fcrimecode);
                if (_criminal == null || _criminal.fflag == 1)
                {
                    return Content("Err|编号不存在或已离监");
                }

            }
            int typeFlag = 0;
            if (loginCheck == 2)
            {
                typeFlag = 1;
            }
            string strIpAddr = IpAddressHelper.GetHostAddress();
            var iplist=_baseDapperBLL.QueryList<T_Area_MacIpAddr>("select * from T_Area_MacIpAddr where IpAddr=@IpAddr", new { IpAddr = strIpAddr });
            string strArea = string.Join<string>(",", iplist.Select(o=>o.FAreaCode).ToList().ToArray());
            rs= FaceCheckService.SendAndCheckFace(fcrimecode,  imageSrc, faceMode,  _criminal, typeFlag,strArea);


            return Json(rs);
        }

        


        #region 人脸管理
        /// <summary>
        /// 注册人脸
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterFace(int typeFlag = 0)
        {
            if (Request.Files.Count > 0)
            {
                List<PhotoEntity> photoList = new List<PhotoEntity>();
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase f = Request.Files[i];
                        string fname = f.FileName;
                        /* startIndex */
                        int index = fname.LastIndexOf("\\") + 1;
                        /* length */
                        int len = fname.Length - index;
                        fname = fname.Substring(index, len);
                        string extName = FileNameHelper.GetFileExtName(fname);

                        if (extName == "png" || extName == "PNG" || extName == "jpg" || extName == "JPG" || extName == "jepg" || extName == "JEPG")
                        {
                            string savePath = Server.MapPath("~/Upload/" + fname);
                            MemoryStream ms = new MemoryStream();
                            f.InputStream.CopyTo(ms);
                            byte[] arr = new byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(arr, 0, (int)ms.Length);
                            ms.Close();
                            String strbaser64 = Convert.ToBase64String(arr);

                            var _pho = new PhotoEntity()
                            {
                                photoName = fname,
                                photoBase64Data = strbaser64,
                                TypeFlag = typeFlag
                            };
                            photoList.Add(_pho);
                        }
                    }

                }

                string strSendPhotoInfo = "0002" + Newtonsoft.Json.JsonConvert.SerializeObject(photoList);//增加报文头0002
                var _sendRs = SocketHelper.SendInfo(strSendPhotoInfo, "127.0.0.1", 7788);
                if (_sendRs.Flag)
                {
                    return Content("OK|" + Convert.ToString(_sendRs.DataInfo));
                }
                else
                {
                    return Content("Err|" + _sendRs.ReMsg);
                }

            }
            return Content("Err|没有接收到文件信息");
        }

        public ActionResult WebCamTest3(string callback = "/Shopping/index/1?", int loginCheck=0)
        {
            ViewData["callback"] = callback;
            ViewData["loginCheck"] = loginCheck;
            ViewData["FUserCode"] =Request["FUserCode"];
            ViewData["FaceMode"] = Request["FaceMode"];

            return View();
        }



        public ActionResult MulitImageUpload()
        {
            return View();
        }

        public ActionResult SpeechTest()
        {
            return View();
        }

        public ActionResult AsrTest()
        {
            return View();
        }
        public ActionResult AsrStart(string strJson)
        {
            AsrResponeEntity asrInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<AsrResponeEntity>(strJson);

            var _message = Newtonsoft.Json.JsonConvert.DeserializeObject<AsrMessage>(asrInfo.msgBody.message);
            var lattices = _message.lattices;

            StringBuilder strOne = new StringBuilder();
            foreach (var item in lattices)
            {
                strOne.Append(item.onebest +"<br>");
            }
            

            return Content(strOne.ToString());
        }



        public ActionResult AsrFileInport()//语音文上传
        {
            string respone = "Err|未处理";
            string strReSaveFlag = Request["reSaveFlag"];
            string bizId = Request["bizId"];
            string uuid = Request["uuid"];

            


            if (Request.Files.Count > 0)
            {
                //string strFBid = Request["excelBid"];

                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/Upload/" + fname);
                f.SaveAs(savePath);
                //context.Response.Write("Success!");
                DateTime sdt;
                //DateTime edt;
                //TimeSpan tspan;
                respone = "OK|上传成功";
                return Content(respone);
            }
            
            return Content($"Err|bizId:{bizId},uuid:{uuid},导入失败，服务器没有接收到Excel文件");
        }

        #endregion


        #region 打印测试

        public ActionResult PrintTest()
        {
            return View();
        }

        #endregion

    }
}