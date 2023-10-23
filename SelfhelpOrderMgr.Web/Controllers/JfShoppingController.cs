using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class JfShoppingController : Controller
    {
        
        JifenMgrService _jifenMgrService = new JifenMgrService();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        private int loginSaleId = 1;
        string strLoginUserName = "";
        // GET: JfShopping
        public ActionResult Index(int id = 1)//默认1是超市消费
        {

            int saleTypeId = id;
            loginSaleId = id;
            //如果销售类型处理于关闭判断则不能开账
            T_SHO_SaleType saletype = new T_SHO_SaleTypeBLL().GetModel(saleTypeId);
            if (saletype.ShoppingFlag == 0)
            {
                return View("StopShoppingNotice");
            }

            //是否启用销售日期，如果启用销售日期，则要判断购买日是否在配置列表中
            T_SHO_ManagerSet mgrset = new T_SHO_ManagerSetBLL().GetModel("SaleDayEnableFlag");
            if (mgrset != null)
            {
                if (mgrset.MgrValue == "1")
                {
                    //如果今天没有在列表里，就说明不能消费，则转到停止消费页面
                    int saledayFlag = new T_SHO_SaleDayListBLL().SaleDayExists(saleTypeId, DateTime.Today);

                    switch (saledayFlag)
                    {
                        case 0:
                            return Redirect("/Shopping/StopShoppingNotice/" + id);
                        case 1:
                            break;
                        case -1:
                            return Redirect("/Shopping/NoAtShoppingTime/" + id);
                    }
                }
            }


            T_SHO_ManagerSet saleTimeSet = new T_SHO_ManagerSetBLL().GetModel("SaleTimeAreaFlag");
            if (saleTimeSet != null)
            {
                if (saleTimeSet.KeyMode == 1)
                {
                    string startTime = saleTimeSet.MgrValue.Substring(0, 5);
                    string endTime = saleTimeSet.MgrValue.Substring(6, 5);
                    if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + startTime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + endTime))
                    {
                        return Redirect("/Shopping/NoAtShoppingTime");
                    }
                }
            }



            ViewData["ptype"] = saletype.PType;
            ViewData["saleTypeId"] = saleTypeId;
            //List<T_GoodsType> types = _jifenMgrService.GetModelList<T_GoodsType>(jss.Serialize(new { SaleTypeId= id, UseType=1 }), "Id asc", 100);
            List<T_GoodsType> types = _jifenMgrService.QueryList<T_GoodsType>("(SaleTypeId='"+id+"' and UseType =1) or UseType=2");

            ViewData["types"] = types;
            string strTypes = "";

            if (types.Count > 0)
            {
                var f = from s in types
                        select s.Fcode;
                strTypes = "'" + string.Join("','", f.ToArray()) + "'";
            }

            List<T_Goods> goods = new List<T_Goods>();
            if (strTypes != "")
            {
                goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("Active='Y' and GTYPE in(" + strTypes + ")");
            }
            else
            {
                goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("Active='Y' and GTYPE =''");
            }
            ViewData["goods"] = goods;

            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("XiaoPiaoStyle");
            ViewData["mset"] = mset;

            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["loginMode"] = loginMode.MgrValue;

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

        //获取用户IC卡信息
        public ActionResult GetCardInfo()
        {
            string userCode = Request["userCode"];
            string userPwd = Request["userPwd"];
            if (string.IsNullOrEmpty(userCode))
            {
                return Content("Err|用户名或是密码有错");
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                userPwd = "";
            }
            List<T_ICCARD_LIST> cardInfos = new T_ICCARD_LISTBLL().GetModelList("FCrimeCode='" + userCode + "' and isnull(FPWD,'')='" + userPwd + "' and FFlag in(1,2)");
            if (cardInfos.Count == 1)
            {
                if (cardInfos[0].FFlag == 2)
                {
                    return Content("Err|抱歉,您的IC卡已经挂失了");
                }
                return Content("OK|" + cardInfos[0].CardCode.ToString());
            }
            return Content("Err|用户名或是密码有错");
        }

        //增加订单的主单
        public ActionResult AddOrder()
        {

            T_SHO_ManagerSet saleTimeSet = new T_SHO_ManagerSetBLL().GetModel("SaleTimeAreaFlag");
            if (saleTimeSet != null)
            {
                if (saleTimeSet.KeyMode == 1)
                {
                    string startTime = saleTimeSet.MgrValue.Substring(0, 5);
                    string endTime = saleTimeSet.MgrValue.Substring(6, 5);
                    if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + startTime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + endTime))
                    {
                        return Content("Error|请在{" + saleTimeSet.MgrValue + "}这个时间段内购物。");
                    }
                }
            }
            string idTimeArea = new T_SHO_SaleDayListBLL().SaleDayTimeArea(loginSaleId, DateTime.Today);
            if (idTimeArea.Length == 11)
            {
                string stime = idTimeArea.Substring(0, 5);
                string etime = idTimeArea.Substring(6, 5);
                if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + stime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + etime))
                {
                    return Content("Error|请在{" + idTimeArea + "}这个时间段内购物。");
                }
            }


            string saleTypeId = Request["saleTypeId"];
            if (string.IsNullOrEmpty(saleTypeId) == true)
            {
                saleTypeId = "1";//如果没有传递相关信息过来，则默认为1超市消费.
            }
            if (saleTypeId == "1")
            {

            }
            string fcardCode = Request["FCardCode"];
            string checkflag = Request["checkFlag"];
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;


            T_SHO_SaleType stype = new T_SHO_SaleTypeBLL().GetModel(Convert.ToInt32(saleTypeId));

            if (stype.ShoppingFlag == 0)//判断是否已经关闭消费了
            {
                return Content("Error|本模块已经停止消费下单，请下个月再来");
            }

            //string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string status = "Error|申请失败";
            if (string.IsNullOrEmpty(fcardCode))
            {
                return Content(status);
            }
            if (fcardCode.Length != 10)
            {
                return Content(status);
            }
            List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode.ToString() + "'");

            #region 验证IC卡状态
            if (cards.Count == 0)
            {
                status = "Error|该卡找不对应人员信息";
                return Content(status);
            }
            switch (cards[0].cardflaga)
            {
                case 4:
                    {
                        return Content("Error|用户已经离监，IC卡已经停用");
                    }
                case 3:
                    {
                        return Content("Error|IC卡已作废，不能用");
                    }
                case 2:
                    {
                        return Content("Error|IC卡已挂失，不能用");
                    }
                default:
                    break;
            }

            #endregion

            #region 验证银行卡是允许未注册消费
            T_SHO_ManagerSet mbankRegFlag = new T_SHO_ManagerSetBLL().GetModel("BankRegFlag");
            if (mbankRegFlag != null)
            {
                if (mbankRegFlag.MgrValue == "1")
                {
                    if (cards[0].RegFlag == null)
                    {
                        return Content("Error|银行卡未开通过，不能用消费");
                    }
                    if (cards[0].RegFlag == 0)
                    {
                        return Content("Error|银行卡未开通过，不能用消费");
                    }
                }
            }
            else
            {
                if (cards[0].RegFlag == null)
                {
                    return Content("Error|银行卡未开通过，不能用消费");
                }
                if (cards[0].RegFlag == 0)
                {
                    return Content("Error|银行卡未开通过，不能用消费");
                }
            }
            #endregion


            string fcrimeCode = cards[0].fcrimecode;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, Convert.ToInt32(saleTypeId));
            if (criminal.ErrInfo != "")
            {
                return Content("Error|" + criminal.ErrInfo + "，请与管理人员联系");
            }

            #region 判断是队别是否已经关账停止消费了
            try
            {
                //超市消费
                //if (criminal.SaleCloseFlag == 1)
                //{
                //    return Content("Error|您所在队别已经停止消费下单了，请下个月再来购买");
                //}

                //积分消费
                T_AREA tarea = new T_AREABLL().GetModelList($"FCode ='{criminal.FAreaCode}'").FirstOrDefault();
                if (tarea.JiFenCloseFlag == 1)
                {
                    return Content("Error|您所在队别已经停止【积分消费】了，请下个月再来购买");
                }
            }
            catch
            {

            }
            #endregion


            #region 判断是否在指定的消费日之内
            T_SHO_ManagerSet saleDaySet = new T_SHO_ManagerSetBLL().GetModel("SaleDaySettingFlag");
            if (saleDaySet != null)
            {
                if (saleDaySet.MgrValue == "1")
                {
                    try
                    {
                        string saleDaySql = @"select  convert(datetime, substring( convert(varchar(20),getdate(),102),1,8)+startDay),
                    convert(datetime, substring( convert(varchar(20),getdate(),102),1,8)+EndDay +' 23:59:00')
                    ,* from t_Sho_saledayList a , t_area b where a.flag=1
                                        and (b.fid in(
                                        select id from t_area where fid in(
                                        select id from t_area where fcode=a.fareaCode
                                        ))
                                         or b.id in (select id from t_area where fid in(
                                        select id from t_area where fcode=a.fareaCode
                                        )) or b.fcode=a.fareaCode)
                                        and b.fcode='" + criminal.FAreaCode + "' and a.Ptype=" + saleTypeId + "  order by a.LevelId desc ";

                        DataTable dt = new CommTableInfoBLL().GetDataTable(saleDaySql);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                bool dayInSettingDaiesFlag = false;
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (DateTime.Now >= Convert.ToDateTime(row[0]) && DateTime.Now < Convert.ToDateTime(row[1]))
                                    {
                                        dayInSettingDaiesFlag = true;
                                    }
                                }
                                if (dayInSettingDaiesFlag == false)
                                {
                                    return Content("Error|今天不是您队消费下单日期，请在本队的下单日来购买，谢谢");
                                }

                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }

            #endregion



            //if (criminal.AmountAmoney < 0)
            //{
            //    if (criminal.dongjieMoney + criminal.AmountBmoney >= 0)
            //    {
            //        return Content("Error|该账户有冻结" + criminal.dongjieMoney.ToString() + " 元，余额不足，不能消费，请与管理人员联系");
            //    }
            //    return Content("Error|该账户A账出现负数不能消费，请与管理人员联系");
            //}

            //if (criminal.AmountBmoney < 0)
            //{
            //    if (criminal.dongjieMoney + criminal.AmountBmoney >= 0)
            //    {
            //        return Content("Error|该账户有冻结" + criminal.dongjieMoney.ToString() + " 元，余额不足，不能消费，请与管理人员联系");
            //    }
            //    return Content("Error|该账户B账出现负数不能消费，请与管理人员联系");

            //}

            //if (criminal.AmountCmoney < 0)
            //{
            //    return Content("Error|该账户C账出现负数不能消费，请与管理人员联系");
            //}

            //if (criminal.CanUseMoneyA < 0)
            //{
            //    return Content("Error|A账户已经超过本月最大可消费额度了，请与管理人员联系");
            //}
            //if (criminal.CanUseMoneyB < 0)
            //{
            //    return Content("Error|B账户已经超过本月最大可消费额度了，请与管理人员联系");
            //}


            if (criminal.AccPoints <= 0)
            {
                return Content("Error|账户已经积分小于等于0不能消费");
            }


            //验证是否在该管理卡权限范围内的人员
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel(ip);

            //取得用户登录名称
            string loginUserName = GetLoginUserName(mset);

            if (string.IsNullOrEmpty(loginUserName))
            {
                #region 消费人员卡，是否可以在本机消费管理卡
                T_SHO_ManagerSet mcardFlag = new T_SHO_ManagerSetBLL().GetModel("CheckManagerCardFlag");


                if (mcardFlag != null)
                {
                    if (mcardFlag.MgrValue == "1")
                    {
                        if (mset == null)
                        {
                            return Content("Error|该机没有管理卡开通不能消费");
                        }
                        List<T_Criminal> cnls = new T_CriminalBLL().GetModelList(@" fareacode in (select fareacode from t_czy_area a,t_czy b where a.fcode=b.fcode and a.fflag=2 and b.fmanagerCard='" + mset.MgrValue + "' ) and fcode='" + criminal.FCode + "'");
                        if (cnls.Count == 0)
                        {
                            return Content("Error|对不起，您不能在本机购物，请回到" + criminal.FAreaName + "购物，谢谢");
                        }
                    }
                }
                #endregion

            }
            else
            {
                List<T_Criminal> cnlsByCookie = new T_CriminalBLL().GetModelList(@" fareacode in (select fareacode from t_czy_area a,t_czy b where a.fcode=b.fcode and a.fflag=2 and (b.fname='" + loginUserName + "' or b.fcode='" + loginUserName + "') ) and fcode='" + criminal.FCode + "'");
                if (cnlsByCookie.Count == 0)
                {
                    return Content("Error|对不起，您不能在本机购物，请回到" + criminal.FAreaName + "购物，谢谢");
                }
            }


            //查询是有未提交的订单
            if (checkflag == "1")
            {
                List<T_SHO_Order> orders = new T_SHO_OrderBLL().GetModelList(" fcrimecode='" + fcrimeCode + "' and (flag=0 or (flag=1 and isnull(InvoiceNo,'')='')) and PType='" + stype.PType + "' and crtdate>='" + DateTime.Today.ToShortDateString() + "' and CrtDate<'" + DateTime.Now.ToString() + "'");
                if (orders.Count > 0)
                {
                    status = "";
                    foreach (T_SHO_Order order in orders)
                    {
                        if (order.Flag == 1 && order.InvoiceNo == "")//如果Flag=1且InvoiceNo是空的，就将它改为Flag=0
                        {
                            order.Flag = 0;
                            new T_SHO_OrderBLL().Update(order);
                        }
                        List<T_SHO_OrderDTL> dtls = new T_SHO_OrderDTLBLL().GetModelList("OrderId='" + order.OrderID + "'");
                        if (dtls.Count > 0)
                        {
                            status = GetOrderListInfo(status, criminal, orders);//获取订单列表信息
                        }
                        else
                        {
                            new T_SHO_OrderBLL().Delete(order.OrderID);
                        }
                    }
                    if (status == "")
                    {//显示并创建订单信息
                        status = DisplayAddOrder(fcrimeCode, ip, status, criminal, saleTypeId, loginUserName);
                    }
                }
                else
                {//显示并创建订单信息
                    status = DisplayAddOrder(fcrimeCode, ip, status, criminal, saleTypeId, loginUserName);
                }
            }
            else
            {
                //显示并创建订单信息
                status = DisplayAddOrder(fcrimeCode, ip, status, criminal, saleTypeId, loginUserName);
            }
            return Content(status);
        }

        private string GetLoginUserName(T_SHO_ManagerSet mset)
        {
            HttpCookie cookie = Request.Cookies["loginUserName"];
            string loginUserName = "";
            if (cookie != null)
            {
                loginUserName = cookie.Value;
            }
            if (string.IsNullOrEmpty(loginUserName))
            {
                try
                {
                    loginUserName = Session["loginUserName"].ToString();
                }
                catch
                {

                }

            }
            return loginUserName;
        }

        private static string GetOrderListInfo(string status, T_Criminal criminal, List<T_SHO_Order> orders)
        {
            //status = "There|" + orders[0].OrderID.ToString() + "|" + criminal.FName + "|" + criminal.CyName + "|" + (criminal.NoXiaofeimoney - (orders[0].FAmount-orders[0].FreeAmount)).ToString()+"|"+ criminal.OkUseAllMoney.ToString()+"|" + orders[0].FAmount.ToString();
            decimal yue = criminal.NoXiaofeimoney - (orders[0].FAmount - orders[0].FreeAmount);
            criminal.NoXiaofeimoney = yue;
            rtnStatus rts = new rtnStatus();
            rts.orderId = orders[0].OrderID;
            rts.FName = criminal.FName;
            rts.cyName = criminal.CyName;
            rts.FAreaName = criminal.FAreaName;
            rts.FCrimeCode = criminal.FCode;
            rts.NoXiaofeimoney = criminal.NoXiaofeimoney;
            rts.OkUseAllMoney = criminal.OkUseAllMoney;
            rts.orderMoney = orders[0].FAmount;
            rts.AccPoints = criminal.AccPoints;
            rts.XiaoFeiPoints = criminal.XiaoFeiPoints;
            JavaScriptSerializer css = new JavaScriptSerializer();

            List<T_SHO_OrderDTL> details = new T_SHO_OrderDTLBLL().GetModelList("OrderId='" + orders[0].OrderID.ToString() + "'");

            rts.lists = details;
            //status = status + "|" + strDtl ;
            status = css.Serialize(rts);
            status = "There|" + status;
            return status;
        }

        private static string DisplayAddOrder(string fcrimeCode, string ip, string status, T_Criminal criminal, string saleTypeId, string loginUserName)
        {
            //删除该犯人的所有临时订单信息
            new T_SHO_OrderBLL().DeleteOrderInfoByFCrimecode(fcrimeCode, saleTypeId);

            //T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode);
            T_SHO_SaleType saletype = new T_SHO_SaleTypeBLL().GetModel(Convert.ToInt32(saleTypeId));
            string crtby = "";
            string cc = ".";
            string[] ips = ip.Split(cc.ToCharArray());
            //string ipLastCode = string.Format("000", ips[3]);
            string ipLastCode = "001";
            if (ips.Length > 3)
            {
                ipLastCode = "000" + ips[3];
                ipLastCode = ipLastCode.Substring(ipLastCode.Length - 3);
            }
            crtby = "IP_" + ipLastCode + "号机";

            T_SHO_ManagerSet mOpenMode = new T_SHO_ManagerSetBLL().GetModel("OpenMode");
            if (mOpenMode.KeyMode == 1)
            {
                if (string.IsNullOrEmpty(loginUserName))
                {
                    T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel(ip);
                    List<T_CZY> mcardCzys = new T_CZYBLL().GetModelList("FManagerCard='" + mset.MgrValue + "'");
                    if (mcardCzys.Count > 0)
                    {
                        crtby = mcardCzys[0].FName + "_" + crtby;
                    }
                }
                else
                {
                    crtby = loginUserName + "_" + crtby;
                }

            }


            T_SHO_Order model = new T_SHO_Order();
            model.FCrimecode = fcrimeCode;
            model.FCriminal = criminal.FName;
            model.CrtDate = DateTime.Now;
            model.Flag = 0;
            model.FAmount = 0;
            model.PType = saletype.PType;
            model.InvoiceNo = "";
            model.IPAddr = ip;
            model.FreeAmount = 0;
            model.RoomNO = "";
            model.CrtBy = crtby;
            int inumber = new T_SHO_OrderBLL().Add(model);
            if (inumber > 0)
            {
                //status = "OK|" + inumber.ToString() + "|" + criminal.FName + "|" + criminal.CyName + "|" + criminal.NoXiaofeimoney.ToString()+"|"+criminal.OkUseAllMoney.ToString()+"|0.00";
                rtnStatus rts = new rtnStatus();
                rts.orderId = inumber;
                rts.FName = criminal.FName;
                rts.cyName = criminal.CyName;
                rts.NoXiaofeimoney = criminal.NoXiaofeimoney;
                rts.OkUseAllMoney = criminal.OkUseAllMoney;
                rts.Xiaofeimoney = criminal.Xiaofeimoney;
                rts.orderMoney = 0;
                rts.FAreaName = criminal.FAreaName;
                rts.FCrimeCode = criminal.FCode;

                rts.AccPoints = criminal.AccPoints;
                rts.XiaoFeiPoints = criminal.XiaoFeiPoints;
                JavaScriptSerializer css = new JavaScriptSerializer();
                status = css.Serialize(rts);
                status = "OK|" + status;

            }
            return status;
        }


        public ActionResult SearchGoodsInfo()
        {
            string status = "Error|没有相应的商品";
            string gtxm = Request["Gtxm"];
            string saleTypeId = Request["saleTypeId"];
            string ptype = "";
            //List<T_GoodsType> types = _jifenMgrService.GetModelList<T_GoodsType>(jss.Serialize(new { SaleTypeId = saleTypeId, UseType = 1 }), "Id asc", 100); 
            List<T_GoodsType> types = _jifenMgrService.QueryList<T_GoodsType>("(SaleTypeId='"+ saleTypeId + "' and UseType=1) or UseType=2");

            if (types.Count > 0)
            {
                //foreach(T_GoodsType type in types)
                //{
                //    if (ptype == "") 
                //    {
                //        ptype = "'" + type.Fcode + "'";
                //    }
                //    else
                //    {
                //        ptype = ptype+",'" + type.Fcode + "'";
                //    }
                //}
                //Linq方式实现
                var t = from item in types
                        select item.Fcode;
                ptype = "'" + string.Join("','", t.ToArray()) + "'";


                List<T_Goods> goods = new T_GoodsBLL().GetModelList("Gtxm='" + gtxm + "' and GType in (" + ptype + ")");
                if (goods.Count > 0)
                {
                    if (goods[0].ACTIVE == "N")
                    {
                        status = "Error|抱谦，该商品已经下架了";
                    }
                    else
                    {
                        status = "OK|" + jss.Serialize(goods[0]);
                    }
                }
                else
                {
                    status = "Error|没有查询到您要的商品";
                    if (!string.IsNullOrEmpty(gtxm))
                    {
                        goods = new T_GoodsBLL().GetModelList("SPShortCode='" + gtxm + "' and GType in (" + ptype + ")");
                        if (goods.Count > 0)
                        {
                            //JavaScriptSerializer jss = new JavaScriptSerializer();
                            if (goods[0].ACTIVE == "N")
                            {
                                status = "Error|抱谦，该商品已经下架了";
                            }
                            else
                            {
                                status = "OK|" + jss.Serialize(goods[0]);
                            }
                        }
                    }
                }

            }
            else
            {
                status = "Error|您现在操作消费类型不存在";
            }

            return Content(status);
        }

        //增加订单明细
        public ActionResult AddOrderDetail()
        {
            string orderId = Request["OrderId"];
            string gtxm = Request["GTXM"];
            string gcount = Request["GCount"];
            string goodRemark = Request["goodRemark"];//商品的品味、尺码等信息
            if (string.IsNullOrEmpty(goodRemark) == true)//如果提交没有说明则为空
            {
                goodRemark = "";
            }
            if (string.IsNullOrEmpty(orderId) == true)
            {
                return Content("Error|订单号不能为空");
            }
            string status = "Error|添加失败";
            //获取商品信息
            T_Goods good = new T_GoodsBLL().GetModel(gtxm);
            DataTable dt = new CommTableInfoBLL().GetDataTable(@"select *from t_goodstype c,t_goods d 
                    where  c.fcode=d.gtype and c.UseType in(1,2)  and (d.gtxm='" + gtxm + "' or spShortCode='" + gtxm + "')");
            if (dt.Rows.Count <= 0)
            {
                return Content("Error|您录入的商品信息不存在");
            }

            if (good == null)
            { //查店内码
                List<T_Goods> goods = new T_GoodsBLL().GetModelList("[SPShortCode]='" + gtxm + "'");
                if (goods.Count == 1)
                {
                    good = goods[0];
                    gtxm = good.GTXM;
                }
            }
            if (good.ACTIVE != "Y")
            {
                return Content("Error|该商品已经下架了，不能购买");
            }
            if (good != null)
            {

                if (Convert.ToDecimal(gcount) > Convert.ToDecimal(good.Xgsl)) //判断是否超过最大限购数量
                {
                    status = "Error|你超过了最大购买数量，" + good.Xgsl.ToString() + "个！";
                    return Content(status);
                }


                //判断本月是否超过最大限购数量
                T_SHO_Order order = new T_SHO_OrderBLL().GetModel(Convert.ToInt32(orderId));
                decimal monthGcount = new T_SHO_OrderBLL().GetMonthBuyCount(gtxm, order.FCrimecode);
                decimal buyGcount = Convert.ToDecimal(monthGcount) + Convert.ToDecimal(gcount);
                string strSD = "月";
                switch (good.XgMode)
                {
                    case 0:
                        {
                            strSD = "日";
                        }
                        break;
                    case 1:
                        {
                            strSD = "周";
                        }
                        break;
                    case 2:
                        {
                            strSD = "月";
                        }
                        break;
                    case 3:
                        {
                            strSD = "季度";
                        }
                        break;
                    case 4:
                        {
                            strSD = "年";
                        }
                        break;
                    default:
                        break;

                }
                if (buyGcount > Convert.ToDecimal(good.Xgsl))
                {
                    status = "Error|你本" + strSD + "已经购买了" + monthGcount.ToString() + "个，再购就超过了最大购买数量，" + good.Xgsl.ToString() + "个！";
                    return Content(status);
                }


                T_GoodsType gt = _jifenMgrService.GetModelFirst<T_GoodsType>(jss.Serialize(new { Fcode = good.GTYPE })); 
                if (gt == null)
                {
                    return Content("Error|您所选的商品类别为空");
                }

                //判断是否开启类别限购模式

                T_SHO_ManagerSet leibieXgMode = new T_SHO_ManagerSetBLL().GetModel("GoodsType_MaxBuyCount");

                string leibieXgFlag = "0";
                if (leibieXgMode != null)
                {
                    leibieXgFlag = leibieXgMode.MgrValue;
                }
                if (leibieXgFlag == "1")
                {
                    //判断本月是否超过本【类别】的最大限购数量
                    decimal typeBuyCount = new T_SHO_OrderBLL().GetTypeBuyCount(gt.CtrlMode, good.GTYPE, good.GTXM, order.FCrimecode);
                    decimal typeSumCount = typeBuyCount + Convert.ToDecimal(gcount);

                    //类型的限购数据必须大于0，否则等于0认为不限购
                    if (typeSumCount > Convert.ToDecimal(gt.MaxBuyCount) && gt.MaxBuyCount > 0)
                    {
                        status = "Error|你本月【" + gt.Fname + "】已经购买了" + typeBuyCount.ToString() + "个，再购就超过了最大购买数量，" + gt.MaxBuyCount.ToString() + "个！";
                        return Content(status);
                    }
                }


                T_SHO_OrderDTL model = new T_SHO_OrderDTL();
                model.OrderID = Convert.ToInt32(orderId);
                model.GCode = good.GCODE;
                model.GTXM = gtxm;
                model.GName = good.GNAME;
                model.GPrice = (decimal)good.GDJ;
                model.GCount = Convert.ToDecimal(gcount);
                model.GAmount = (decimal)good.GDJ * Convert.ToDecimal(gcount);
                model.Flag = 0;
                model.Remark = goodRemark;//保存商品的品味、尺码等信息
                model.SPShortCode = good.SPShortCode;
                if (good.Ffreeflag == 1)//设定商品是否是非限额的
                {
                    model.FreeFlag = (int)good.Ffreeflag;
                }
                else
                {
                    model.FreeFlag = 0;
                }

                //T_SHO_Order order = new T_SHO_OrderBLL().GetModel(model.OrderID);
                List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + order.PType + "'");
                T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(order.FCrimecode, saleTypes[0].Id);
                if (criminal.ErrInfo != "")
                {
                    return Content("Error|" + criminal.ErrInfo + "，请与管理人员联系");
                }


                T_SHO_ManagerSet xgMode = new T_SHO_ManagerSetBLL().GetModel("XianGouMode");

                string XgFlag = "0";
                if (xgMode != null)
                {
                    XgFlag = xgMode.MgrValue;
                }

                if (XgFlag == "1")
                {
                    //判断是否超过单品队别本月最大限购数量
                    List<T_CZY> czys;
                    string loginUserName = "";
                    HttpCookie cookie = Request.Cookies["loginUserName"];
                    if (cookie != null)
                    {
                        loginUserName = cookie.Value;
                    }
                    if (loginUserName == "")
                    {
                        string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                        T_SHO_ManagerSet msetIp = new T_SHO_ManagerSetBLL().GetModel(ip);
                        strLoginUserName = msetIp.MgrValue;
                        czys = new T_CZYBLL().GetModelList("FManagerCard='" + strLoginUserName + "' ");

                    }
                    else
                    {
                        czys = new T_CZYBLL().GetModelList("FName='" + loginUserName + "' ");
                    }

                    if (czys.Count <= 0)
                    {
                        status = "Error|你的机器未登录，不能消费！";
                        return Content(status);
                    }

                    List<T_AREA> areas = new T_AREABLL().GetModelList("FName='" + czys[0].FUserArea + "'");
                    if (areas.Count <= 0)
                    {
                        status = "Error|您的登录名未分配指定的使用队别，不能消费！";
                        return Content(status);
                    }

                    T_SHO_AreaGoodMaxCount areaMaxModel = new T_SHO_AreaGoodMaxCountBLL().GetModel(areas[0].FCode, good.GTXM);

                    if (!new T_SHO_AreaGoodMaxCountBLL().GetLoginNameBuyCountByGtxm(czys[0].FName, Convert.ToDecimal(gcount), good.GTXM, areas[0].FCode))
                    {
                        status = "Error|你所在队别已经超过了本月最大购买数量！";
                        return Content(status);
                    }
                }
                else
                {
                    //判断是否超过单品队别本月最大限购数量
                    T_SHO_AreaGoodMaxCount areaMaxModel = new T_SHO_AreaGoodMaxCountBLL().GetModel(criminal.FAreaCode, good.GTXM);

                    if (!new T_SHO_AreaGoodMaxCountBLL().GetAreaBuyCountByGtxm(criminal.FCode, Convert.ToDecimal(gcount), good.GTXM, criminal.FAreaCode))
                    {
                        status = "Error|你所在队别已经超过了本月最大购买数量！";
                        return Content(status);
                    }
                }

                // 验证库存量
                T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("YanZhenKCL");
                if (mset.KeyMode == 1)
                {
                    List<T_GOODSSTOCKMAIN> gstocks = new T_GOODSSTOCKMAINBLL().GetModelList("GCode='" + good.GCODE + "'");
                    if (gstocks == null)
                    {
                        status = "Error|当前库存量为0，您本次购买量超过最大库存了！";
                        return Content(status);
                    }
                    //验证库存量是否足够卖,当前要买的数量+未结算的数量要不大于库存数量
                    if (new T_SHO_OrderDTLBLL().AddDetailCheckStock(buyGcount, good.GCODE))
                    {
                        status = "Error|当前库存量为" + gstocks[0].BALANCE.ToString() + "，您本次购买量超过最大库存了！";
                        return Content(status);
                    }
                }

                #region 判断是否在属于特购商品
                //判断是否在属于特购商品，如果是就要验证金额是有够扣
                int typeflag = _jifenMgrService.GetModelFirst<T_GoodsType>(jss.Serialize(new { Fcode=good.GTYPE})).FTZSP_TypeFlag;
                if (typeflag == 1)
                {
                    model.FTZSP_TypeFlag = 1;//标记该商品是特种消费商品
                    if (order.FTZSP_Money + (good.GDJ * Convert.ToDecimal(gcount)) > criminal.FTZSP_CanUseMoney)
                    {
                        status = "Error|您购买特购商品金额不能超过本月最大金额：" + criminal.FTZSP_CanUseMoney + "！";
                        return Content(status);
                    }
                }
                else
                {
                    model.FTZSP_TypeFlag = 0;//标记该商品不是特种消费商品
                }
                #endregion


                string strFreeFlag = "0";
                if (good.Ffreeflag == 1)//如果是非限制商品
                {
                    strFreeFlag = "1";
                    if (criminal.OkUseAllMoney >= (order.FAmount + model.GAmount))
                    {
                        //执行订单明细记录插入
                        status = DoAddOrderDetail(status, model, strFreeFlag);
                    }
                    else
                    {
                        status = "Error|账户当前总余额不足";

                    }
                }
                else
                {
                    strFreeFlag = "0";
                    status = "Error|可消费积分不足";
                    //if (criminal.NoXiaofeimoney >= (order.FAmount - order.FreeAmount + model.GAmount))//看下钱是否够扣

                    if (criminal.AccPoints >= (order.FAmount - order.FreeAmount + model.GAmount))//看下钱是否够扣
                    {
                        //如果金额可消费金额有够，判断总的可用金额够吗
                        if (criminal.AccPoints >= (order.FAmount + model.GAmount))//看下钱是否够扣
                        {
                            //执行订单明细记录插入
                            status = DoAddOrderDetail(status, model, strFreeFlag);
                        }
                    }
                    //else
                    //{
                    //    status = "Error|可消费余额不足";
                    //}
                }
            }
            return Content(status);
        }

        private static string DoAddOrderDetail(string status, T_SHO_OrderDTL model, string strFreeFlag)
        {
            //2016-10-29日 曾林进 取消单步执行模式，改采用Dapper事务模式
            //int i = new T_SHO_OrderDTLBLL().Add(model);
            //if (i > 0)
            //{
            //    if (new T_SHO_OrderBLL().UpdateMoney((int)model.OrderID, (decimal)model.GAmount, strFreeFlag))
            //    {
            //        JavaScriptSerializer jss = new JavaScriptSerializer();
            //        T_SHO_OrderDTL dtl=new T_SHO_OrderDTLBLL().GetModel(i);
            //        status = "OK|" + i.ToString() + "|" + model.GAmount.ToString() + "|" + strFreeFlag+"|"+ jss.Serialize(dtl) ;
            //    }
            //    else
            //    {
            //        status = "Error|更新主单金额失败";
            //    }
            //}
            //return status;

            //2016-10-29日 曾林进 采用事务方式进行，解决之前明细有写，主单金额没有更新问题
            int i = new T_SHO_OrderDTLBLL().AddDetailAndUpdateMain(model, strFreeFlag);
            if (i > 0)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                T_SHO_OrderDTL dtl = new T_SHO_OrderDTLBLL().GetModel(i);
                status = "OK|" + i.ToString() + "|" + model.GAmount.ToString() + "|" + strFreeFlag + "|" + jss.Serialize(dtl);

            }
            else
            {
                status = "Error|更新主单金额失败";
            }
            return status;
        }

        //获取订单明细记录
        public ActionResult GetOrderDetails()
        {
            string orderid = Request["orderId"];
            if (string.IsNullOrEmpty(orderid) != true)
            {
                List<T_SHO_OrderDTL> dtls = new T_SHO_OrderDTLBLL().GetModelList("OrderId='" + orderid + "'");
                return Content("OK|" + jss.Serialize(dtls));
            }
            return Content("Err|没有记录");
        }
        public ActionResult OrderDetail()
        {
            string orderId = Request["OrderId"];

            List<T_SHO_OrderDTL> types = new T_SHO_OrderDTLBLL().GetModelList("");
            ViewData["types"] = types;
            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("");
            ViewData["goods"] = goods;
            return View();
        }

        //删除一条订单明细记录
        public ActionResult DelOrderDetail()
        {
            string orderId = Request["OrderId"];
            string id = Request["Id"];
            T_SHO_Order myorder = new T_SHO_OrderBLL().GetModel(Convert.ToInt32(orderId));
            if (myorder.Flag >= 1)
            {
                return Content("Error|该定单已经提交结算不能删除明细记录");
            }
            if (new T_SHO_OrderBLL().DelOrderDetailAndUpdateMoney(Convert.ToInt32(orderId), Convert.ToInt32(id)))
            {
                T_SHO_Order order = new T_SHO_OrderBLL().GetModel(Convert.ToInt32(orderId));
                List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + order.PType + "'");
                T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(order.FCrimecode, saleTypes[0].Id);
                if (criminal.ErrInfo != "")
                {
                    return Content("Error|" + criminal.ErrInfo + "，请与管理人员联系");
                }
                //decimal yue = criminal.NoXiaofeimoney - (order.FAmount - order.FreeAmount);
                //criminal.NoXiaofeimoney = yue;

                string status = "";
                List<T_SHO_Order> orders = new T_SHO_OrderBLL().GetModelList(" OrderId='" + orderId + "'");
                status = GetOrderListInfo(status, criminal, orders);//获取订单列表信息
                return Content(status);
            }
            else
            {
                return Content("Error|删除明细记录失败");
            }

        }
        public ActionResult SubmitOrder()//提交订单
        {
            string orderId = Request["OrderId"];
            string fcrimecode = Request["FCrimeCode"];
            string crtby = Request["crtby"];
            string userRoomNo = Request["userRoomNo"];

            if (string.IsNullOrEmpty(orderId) == true)
            {
                return Content("Err|订单号不能为空。");
            }
            if (string.IsNullOrEmpty(fcrimecode) == true)
            {
                return Content("Err|用户编号不能为空。");
            }

            if (string.IsNullOrEmpty(userRoomNo) == true)
            {
                return Content("Err|房间号不能为空。");
            }

            if (new T_InvoiceBLL().Exists(Convert.ToInt32(orderId)))
            {
                return Content("Error|该订单号已经结算过了，不能再重复结算（提交提示）");
            }


            userRoomNo = Convert.ToInt32(userRoomNo).ToString(); //改为一位数
            string ipaddr = System.Web.HttpContext.Current.Request.UserHostAddress;

            //验证是否在该管理卡权限范围内的人员
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel(ipaddr);

            //取得用户登录名称
            string loginUserName = GetLoginUserName(mset);


            string cc = ".";
            string[] ips = ipaddr.Split(cc.ToCharArray());
            //string ipLastCode = string.Format("000", ips[3]);
            string ipLastCode = "001";
            if (ips.Length > 3)
            {
                ipLastCode = "000" + ips[3];
                ipLastCode = ipLastCode.Substring(ipLastCode.Length - 3);
            }
            crtby = "IP_" + ipLastCode + "号机";
            T_SHO_Order orderModel = new T_SHO_OrderBLL().GetModel(Convert.ToInt32(orderId));
            //如果订单操作员有存在就直接用订单的操作员
            if (string.IsNullOrEmpty(orderModel.CrtBy) == false)
            {
                crtby = orderModel.CrtBy;
            }

            //增加验证商品是否队别数量限购产品，如果有则要验证是否超过数量
            #region 增加验证商品是否队别数量限购产品，如果有则要验证是否超过数量

            /*
             *2018-06-08 修改人：曾林进
             *修改原因：判断是否超过最大限购数量时，采用单个商品循环太慢了
             *修改方法：首先判断商品中是否有限购商品，没有就跳过
             *           接来采用批量查询判断的方法
             */

            //T_Criminal crl = new T_CriminalBLL().GetModel(fcrimecode);
            //List<T_SHO_OrderDTL> orderDetails = new T_SHO_OrderDTLBLL().GetModelList("OrderId='" + orderId + "'");
            //foreach (T_SHO_OrderDTL orderDetail in orderDetails)
            //{
            //    if (!new T_SHO_AreaGoodMaxCountBLL().GetLoginNameBuyCountByGtxm(crtby.Substring(0,crtby.IndexOf("_")), orderDetail.GCount, orderDetail.GTXM, crl.FAreaCode))
            //    {
            //        return Content("Err|【" + orderDetail.GName + "】超过本月队别最大购买数量，不能提交。");
            //    }
            //} 

            //修改后，改成批量验证

            if (!new T_SHO_AreaGoodMaxCountBLL().SubmitCheckLoginNameBuyGoodCountStatus(crtby.Substring(0, crtby.IndexOf("_")), Convert.ToInt32(orderId)))
            {
                return Content("Err|【有的商品超过本月队别最大购买数量】，不能提交。");
            }

            #endregion

            if (orderModel == null)
            {
                return Content("Err|该订单号不存在。");
            }

            if (orderModel.FAmount == 0)
            {
                return Content("Err|该订单没有任何商品信息，不能提交。");
            }

            if (orderModel.Flag == 1)
            {
                return Content("Err|该订单已经在提交中，不必重复提交。");
            }
            else if (orderModel.Flag >= 2)
            {
                return Content("Err|该订单已经结算，不能重复结算。");
            }
            orderModel.Flag = 1;
            if (new T_SHO_OrderBLL().Update(orderModel))//更订单状态为1“提交中”
            {
                string status = new T_SHO_OrderBLL().SubmitJFOrder(Convert.ToInt32(orderId), crtby, ipLastCode, fcrimecode, userRoomNo);
                if (status == "OK|结算成功。")
                {
                    rtnPaySubmitInfo<T_JF_Invoice,T_JF_InvoiceDTL> rtns = new rtnPaySubmitInfo<T_JF_Invoice, T_JF_InvoiceDTL>();

                    T_JF_Invoice invoice = _jifenMgrService.GetModelFirst<T_JF_Invoice>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId }));
                    List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + invoice.PType + "'");
                    T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode, saleTypes[0].Id);
                    List<T_JF_InvoiceDTL> details = _jifenMgrService.GetModelList<T_JF_InvoiceDTL>(Newtonsoft.Json.JsonConvert.SerializeObject(new { INVOICENO = invoice.InvoiceNo }) ,"Id asc",100);


                    rtns.details = details;
                    rtns.invoice = invoice;
                    rtns.criminal = criminal;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    return Content(status + "|" + jss.Serialize(rtns));
                }
                else
                {
                    //先将状态改回0
                    orderModel.Flag = 0;
                    new T_SHO_OrderBLL().Update(orderModel);
                    return Content(status);
                }
            }
            else
            {
                return Content("Err|更新订单状态为提交时失败。");
            }
        }

        public ActionResult GetGoodsStockNumber()//获取库存数量及商品属性
        {
            string gtxm = Request["GTXM"];
            T_Goods goods = new T_GoodsBLL().GetModel(gtxm);
            if (goods == null)
            {
                return Content("Error|对不起，商品信息不存在!");
            }

            //获取商品的属性
            List<T_SHO_GoodsForAttr> attrs = new T_SHO_GoodsForAttrBLL().GetModelList("GCODE='" + goods.GCODE + "'", "AttrInfo");
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //获取商品库存
            List<T_GOODSSTOCKMAIN> list = new T_GOODSSTOCKMAINBLL().GetModelList(" gcode='" + goods.GCODE + "'");

            //Web.Config里是否需要检测库存
            //string checkStock = ConfigurationManager.ConnectionStrings["checkStock"].ConnectionString;
            string checkStock = new T_SHO_ManagerSetBLL().GetModel("YanZhenKCL").KeyMode.ToString();
            if (checkStock == "1")
            {
                if (list.Count > 0)
                {
                    return Content("OK|" + list[0].BALANCE.ToString() + "|" + jss.Serialize(attrs));
                }
                else
                {
                    return Content("OK|0|" + jss.Serialize(attrs));
                }
            }
            else
            {
                return Content("OK|99999|" + jss.Serialize(attrs));
            }
        }

        public ActionResult GetPrintXiaoPiaoFlag()//打印小票标志
        {
            try
            {
                T_SHO_ManagerSet PrintXiaoPiao = new T_SHO_ManagerSetBLL().GetModel("PrintXiaoPiao");
                return Content(PrintXiaoPiao.MgrValue);
            }
            catch
            {
                return Content("Err|获取失败");
            }
        }

        public ActionResult StopShoppingNotice()
        {
            return View();
        }

        public ActionResult NoAtShoppingTime(int id = 1)
        {
            string saleTimeArea = "00:00-00:00";
            T_SHO_ManagerSet saleTimeSet = new T_SHO_ManagerSetBLL().GetModel("SaleTimeAreaFlag");
            if (saleTimeSet != null)
            {
                if (saleTimeSet.KeyMode == 1)
                {
                    saleTimeArea = saleTimeSet.MgrValue;
                }
            }
            string saleIdTimeArea = new T_SHO_SaleDayListBLL().SaleDayTimeArea(id, DateTime.Today);
            if (saleIdTimeArea.Length == 11)
            {
                saleTimeArea = saleIdTimeArea;
            }
            ViewData["saleTimeArea"] = saleTimeArea;
            return View();
        }






    }
}