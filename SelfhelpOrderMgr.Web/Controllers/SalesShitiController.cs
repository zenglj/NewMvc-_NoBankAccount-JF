using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class SalesShitiController : Controller
    {
        BaseDapperBLL _baseDapperBLL=new BaseDapperBLL();
        string strIpAddr = "自助" + GetIpAddressLastCode(System.Web.HttpContext.Current.Request.UserHostAddress);
        private static string _dengjiMgrFlag = new T_SHO_ManagerSetBLL().GetModel("ShangpinDengjiKongzhi")?.MgrValue;
        private static List<T_JF_GoodsLevel> _goodsDjs = new JifenMgrService().GetModelList<T_JF_GoodsLevel>("");
        private static JifenMgrService _jifenMgrService = new JifenMgrService();
        private int _saleTypeId = 61;
        //
        // GET: /Sales/

        public ActionResult Index(int saleTypeId=61)
        {


            return View();
        }

        public ActionResult CardIndex(int saleTypeId = 61)
        {
            //ViewData["callback"] = callback;
            //ViewData["loginCheck"] = loginCheck;
            ViewData["FUserCode"] = Request["FUserCode"];
            ViewData["FaceMode"] = Request["FaceMode"];
            _saleTypeId = saleTypeId;

            return View();
        }

        public ActionResult FaceIndex(int saleTypeId = 61)
        {
            //ViewData["callback"] = callback;
            //ViewData["loginCheck"] = loginCheck;
            ViewData["FUserCode"] = Request["FUserCode"];
            ViewData["FaceMode"] = Request["FaceMode"];
            _saleTypeId = saleTypeId;

            return View();
        }


        private static string GetIpAddressLastCode(string ip)
        {
            string cc = ".";
            string[] ips = ip.Split(cc.ToCharArray());
            //string ipLastCode = string.Format("000", ips[3]);
            string ipLastCode = "001";
            if (ips.Length > 3)
            {
                ipLastCode = "000" + ips[3];
                ipLastCode = ipLastCode.Substring(ipLastCode.Length - 3);
            }

            return ipLastCode;
        }
        public ActionResult FaceShopping(string fcrimecode,string loginUrl, int id = 1)//超市
        {
            //int saleTypeId = id;


            T_SHO_SaleType saletype = new T_SHO_SaleTypeBLL().GetModel(_saleTypeId);

            if (saletype.ShoppingFlag == 0)
            {
                return Redirect("/SalesShiti/StopShoppingNotice");
            }


            //是否启用销售日期，如果启用销售日期，则要判断购买日是否在配置列表中
            T_SHO_ManagerSet mgrset = new T_SHO_ManagerSetBLL().GetModel("SaleDayEnableFlag");
            if (mgrset != null)
            {
                if (mgrset.MgrValue == "1")
                {
                    //如果今天没有在列表里，就说明不能消费，则转到停止消费页面
                    int saledayFlag = new T_SHO_SaleDayListBLL().SaleDayExists(_saleTypeId, DateTime.Today);
                    switch (saledayFlag)
                    {
                        case 0:
                            return Redirect("/SalesShiti/StopShoppingNotice/" + id);
                        case 1:
                            break;
                        case -1:
                            return Redirect("/SalesShiti/NoAtShoppingTime/" + id);
                    }
                    //if (!saledayFlag)
                    //{
                    //    return Redirect("/Shopping/StopShoppingNotice");
                    //}

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
                        return Redirect("/SalesShiti/NoAtShoppingTime");
                    }
                }
            }


            ViewData["id"] = id;
            ViewData["ptype"] = saletype.PType;
            ViewData["saleTypeId"] = _saleTypeId;

            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("XiaoPiaoStyle");
            ViewData["mset"] = mset;

            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["loginMode"] = loginMode.MgrValue;

            //防止修改编号来实现购物
            //ViewData["fcrimecode"] = Request["fcrimecode"];//人脸传过来的编号
            string fcode = "";

            
            string[] parts = fcrimecode.Split('|');
            if (parts.Length > 1 && MD5ProcessHelper.GetMD5(parts[0]) != parts[1])
            {
                return Redirect("/SalesShiti/Index");
            }
            fcode = parts[0];

            ViewData["fcrimecode"] = fcode;




            T_SHO_ManagerSet printPlusVer = new T_SHO_ManagerSetBLL().GetModel("PrintPlusVer");
            if (printPlusVer == null || printPlusVer.MgrValue == "0")
            {
                ViewData["PrintPlusVer"] = "0";
            }
            else
            {
                ViewData["PrintPlusVer"] = "1";
            }

            var user = new T_CriminalBLL().GetCriminalXE_info(fcode, _saleTypeId);

            //开始创建订单
            var rs = AddOrder(_saleTypeId, user.CardCode, "1");
            

            
            //ViewBag.Model = user;
            ViewData["Model"] = rs;
            ViewData["loginUrl"] = loginUrl;
            
            return View();
        }
        public ActionResult Medicine()//医药
        {
            return View();
        }
        public ActionResult AddDetaiGood(string orderId,string gtxm,int gcount,string goodRemark="")
        {
            ResultInfo rs = new ResultInfo();

            var _orderModel = _baseDapperBLL.QueryList<T_SHO_Order>("select * from T_SHO_Order where orderId=@orderId and Flag=0", new { orderId = orderId }).FirstOrDefault();
            if(_orderModel==null)
            {
                rs.ReMsg = "Error|订单不存在";
                return Json(rs);
            }

            var _saleType = _baseDapperBLL.QueryList<T_SHO_SaleType>("select * from T_SHO_SaleType where PType=@PType", new { PType = _orderModel.PType }).FirstOrDefault();

            T_Goods good = _baseDapperBLL.QueryList<T_Goods>("select * from t_goods where gtxm=@gtxm and gtype in(select fcode from T_GoodsType where saletypeid=@SaleTypeId)", new { gtxm = gtxm , SaleTypeId = _saleType.Id }).FirstOrDefault();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (good != null)
            {
                if (good.ACTIVE == "N")
                {
                    rs.ReMsg="Error|抱谦，该商品已经下架了";
                    return Json( rs);
                }
            }
            else
            {
                rs.ReMsg = "Error|商品信息不存在";
                return Json(rs);
            }


            if (Convert.ToDecimal(gcount) > Convert.ToDecimal(good.Xgsl)) //判断是否超过最大限购数量
            {
                rs.ReMsg = "Error|你超过了最大购买数量，" + good.Xgsl.ToString() + "个！";
                return Json(rs);
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
                rs.ReMsg = "Error|你本" + strSD + "已经购买了" + monthGcount.ToString() + "个，再购就超过了最大购买数量，" + good.Xgsl.ToString() + "个！";
                return Json(rs);
            }


            T_GoodsType gt = _baseDapperBLL.GetModelFirst<T_GoodsType>(jss.Serialize(new { Fcode = good.GTYPE }));
            if (gt == null)
            {
                rs.ReMsg="Error|您所选的商品类别为空";
                return Json(rs);
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
                    rs.ReMsg = "Error|你本月【" + gt.Fname + "】已经购买了" + typeBuyCount.ToString() + "个，再购就超过了最大购买数量，" + gt.MaxBuyCount.ToString() + "个！";
                    return Json(rs);
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

            #region 判断余额是否足够

            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + order.PType + "'");
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(order.FCrimecode, saleTypes[0].Id);

            string strFreeFlag = "0";
            if (good.Ffreeflag == 1)//如果是非限制商品
            {
                strFreeFlag = "1";
                if (criminal.OkUseAllMoney < (order.FAmount + model.GAmount))
                {
                    rs.ReMsg = "Error|账户当前总余额不足！";
                    return Json(rs);
                }
            }
            else
            {
                strFreeFlag = "0";
                if (criminal.NoXiaofeimoney < (order.FAmount - order.FreeAmount + model.GAmount))//看下钱是否够扣
                {
                    rs.ReMsg = "Error|可消费余额不足！";
                    return Json(rs);
                }

            }

            #endregion

            bool isSuccess = false;
            using (TransactionScope ts = new TransactionScope())
            {
                int _modelId = new T_SHO_OrderDTLBLL().Add(model);
                var _upMoney = _baseDapperBLL.ExecuteSql(@"update T_SHO_Order set FAmount=b.famount,FreeAmount=b.freeAmount,FTZSP_Money=b.FTZSP_Money
                    from T_SHO_Order a,(select orderId,isnull(sum(GAmount),0) as FAmount,isnull(sum(FreeFlag*GAmount),0) as FreeAmount
                    ,isnull(sum(FTZSP_TypeFlag*GAmount),0) as FTZSP_Money from T_SHO_OrderDTL where OrderID=@orderId group by OrderID) b where a.OrderID=b.OrderID
                    ", new { orderId = model.OrderID });

                isSuccess = true;
                ts.Complete();

            }


            if (isSuccess==false)
            {
                rs.ReMsg = "Error|添加商品失败！";
                return Json(rs);
            }

            var _goodList = _baseDapperBLL.QueryList<T_Goods>("select * from T_Goods where active='Y'");

            var _detail =_baseDapperBLL.QueryList<T_SHO_OrderDTL>("select * from T_SHO_OrderDTL where orderId=@orderId"
                , new { orderId = orderId });

            rs.Flag=true;
            rs.DataInfo = _detail.Select(item=>new {
                ID=item.ID,
                src= _goodList.Where(o=>o.GTXM==item.GTXM).Select(o=>o.src).FirstOrDefault(),
                GTXM=item.GTXM,
                GName=item.GName,
                GPrice=item.GPrice,
                GCount=item.GCount,
                GAmount=item.GAmount,
                Remark=item.Remark,
            }).ToList();
            rs.ReMsg = "Success|添加商品成功！";


            return Json(rs);
        }

        public ActionResult DelDetaiGood(string orderId, int id)
        {
            ResultInfo rs = new ResultInfo();

            var _orderModel = _baseDapperBLL.QueryList<T_SHO_Order>("select * from T_SHO_Order where orderId=@orderId and Flag=0", new { orderId = orderId }).FirstOrDefault();
            if (_orderModel == null)
            {
                rs.ReMsg = "Error|订单不存在或状态不正确";
                return Json(rs);
            }
            
            bool isSuccess = false;
            using (TransactionScope ts = new TransactionScope())
            {
                var _delCount = _baseDapperBLL.ExecuteSql("delete from T_SHO_OrderDTL where orderId=@orderId and Id=@Id", new { orderId = orderId, Id = id });
                var _upMoney = _baseDapperBLL.ExecuteSql(@"update T_SHO_Order set FAmount=b.famount,FreeAmount=b.freeAmount,FTZSP_Money=b.FTZSP_Money
                    from T_SHO_Order a,(select orderId,isnull(sum(GAmount),0) as FAmount,isnull(sum(FreeFlag*GAmount),0) as FreeAmount
                    ,isnull(sum(FTZSP_TypeFlag*GAmount),0) as FTZSP_Money from T_SHO_OrderDTL where OrderID=@orderId group by OrderID) b where a.OrderID=b.OrderID
                    ", new { orderId = orderId });

                isSuccess=true;
                ts.Complete();
                
            }

            if (isSuccess == false)
            {
                rs.ReMsg = "Error|删除商品失败！";
                return Json(rs);
            }
            var _goodList = _baseDapperBLL.QueryList<T_Goods>("select * from T_Goods where active='Y'");

                var _detail = _baseDapperBLL.QueryList<T_SHO_OrderDTL>("select * from T_SHO_OrderDTL where orderId=@orderId"
                    , new { orderId = orderId });

                rs.Flag = true;
                rs.DataInfo = _detail.Select(item => new {
                    ID = item.ID,
                    src = _goodList.Where(o => o.GTXM == item.GTXM).Select(o => o.src).FirstOrDefault(),
                    GTXM = item.GTXM,
                    GName = item.GName,
                    GPrice = item.GPrice,
                    GCount = item.GCount,
                    GAmount = item.GAmount,
                    Remark = item.Remark,
                }).ToList();
                rs.ReMsg = "Success|删除商品成功！";
                return Json(rs);
        }



        #region 人脸识别

        /// <summary>
        /// 人脸识别接口调用方法
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="imageSrc"></param>
        /// <param name="faceMode"></param>
        /// <param name="loginCheck"></param>
        /// <returns></returns>
        public ActionResult CheckFace(string fcrimecode, string imageSrc, string faceMode = "0001", int loginCheck = 1)
        {
            
            ResultInfo rs = new ResultInfo();

            if (string.IsNullOrWhiteSpace(faceMode))
            {
                faceMode = "0001";
            }
            string stringdata = "";

            //string imageSrc = Request["imageSrc"];  //改为参数提取
            if (string.IsNullOrWhiteSpace(imageSrc))
            {
                rs.ReMsg = "Err|图片不能为空";
                return Json(rs);
            }

            T_Criminal _criminal = null;
            if (!string.IsNullOrWhiteSpace(fcrimecode))
            {
                _criminal = new BaseDapperBLL().QueryModel<T_Criminal>("fcode", fcrimecode);
                if (_criminal == null || _criminal.fflag == 1)
                {
                    rs.ReMsg = "Err|编号不存在或已离监";
                    return Json(rs);
                }

            }
            int typeFlag = 0;
            if (loginCheck == 2)
            {
                typeFlag = 1;
            }
            string strIpAddr = IpAddressHelper.GetHostAddress();
            var iplist = _baseDapperBLL.QueryList<T_Area_MacIpAddr>("select * from T_Area_MacIpAddr where IpAddr=@IpAddr", new { IpAddr = strIpAddr });
            string strArea = string.Join<string>(",", iplist.Select(o => o.FAreaCode).ToList().ToArray());
            rs = FaceCheckService.SendAndCheckFace(fcrimecode, imageSrc, faceMode, _criminal, typeFlag, strArea);
            
            //rs = new ResultInfo() { Flag = true, DataInfo = new FaceCheckResult() { UserCode = "35010000003", UserName="测试用户" } };
            //rs.ReMsg = "识别成功";

            T_Criminal criminal;
            string userCode = "";
            if (rs.Flag == true)
            {

                FaceCheckResult faceResult = (FaceCheckResult)rs.DataInfo;

                //userCode = faceResult.UserCode;
                userCode = faceResult.UserCode.Split('|')[0];
                faceResult.url = "/SalesShiti/FaceShopping";
                //faceResult.UserCode = $"{faceResult.UserCode}|{MD5ProcessHelper.GetMD5(faceResult.UserCode)}";

                rs.DataInfo = faceResult;

                //获取人员信息
                criminal = new T_CriminalBLL().GetCriminalXE_info(userCode, _saleTypeId);
                if (criminal == null || criminal.fflag == 1)
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|此人不存在或已离监";
                    return Json(rs);
                }


            }
            return Json(rs);
        }


        #endregion


        #region IC卡识别

        public ActionResult CheckICCard(string cardCode, int checkFlag = 0)
        {

            ResultInfo rs = new ResultInfo();

            var card=_baseDapperBLL.QueryModel<T_Criminal_card>("CardCodeA", cardCode);
            if (card==null || card.cardflaga!=1)
            {
                rs.ReMsg = "Err|卡号不存在或已离监";
                return Json(rs);
            }

            T_Criminal criminal = _baseDapperBLL.QueryModel<T_Criminal>("FCode", card.fcrimecode);
            if (criminal == null || criminal.fflag != 0)
            {
                rs.ReMsg = "Err|用户不存在或已离监";
                return Json(rs);
            }

            //string strIpAddr = IpAddressHelper.GetHostAddress();
            //var iplist = _baseDapperBLL.QueryList<T_Area_MacIpAddr>("select * from T_Area_MacIpAddr where IpAddr=@IpAddr", new { IpAddr = strIpAddr });
            //string strArea = string.Join<string>(",", iplist.Select(o => o.FAreaCode).ToList().ToArray());

            FaceCheckResult faceResult = new FaceCheckResult()
            { 
                UserCode = $"{criminal.FCode}|{MD5ProcessHelper.GetMD5(criminal.FCode)}",
                UserName = criminal.FName,
                url = "/SalesShiti/FaceShopping"
            };

            rs.Flag = true;
            rs.ReMsg = "识别成功";
            rs.DataInfo = faceResult;

            return Json(rs);
        }


        #endregion

        private ResultInfo AddOrder(int saleTypeId,string fcardCode, string checkflag="1")
        {
            //string fcardCode = Request["FCardCode"];
            //string checkflag = Request["checkFlag"];
            ResultInfo rs=new ResultInfo();

            T_SHO_ManagerSet saleTimeSet = new T_SHO_ManagerSetBLL().GetModel("SaleTimeAreaFlag");
            if (saleTimeSet != null)
            {
                if (saleTimeSet.KeyMode == 1)
                {
                    string startTime = saleTimeSet.MgrValue.Substring(0, 5);
                    string endTime = saleTimeSet.MgrValue.Substring(6, 5);
                    if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + startTime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + endTime))
                    {
                        rs.ReMsg = "Error|请在{" + saleTimeSet.MgrValue + "}这个时间段内购物。";
                        return rs;
                    }
                }
            }
            string idTimeArea = new T_SHO_SaleDayListBLL().SaleDayTimeArea(_saleTypeId, DateTime.Today);
            if (idTimeArea.Length == 11)
            {
                string stime = idTimeArea.Substring(0, 5);
                string etime = idTimeArea.Substring(6, 5);
                if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + stime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + etime))
                {
                    rs.ReMsg = "Error|请在{" + idTimeArea + "}这个时间段内购物。";
                    return rs;
                }
            }

            
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;


            T_SHO_SaleType stype = new T_SHO_SaleTypeBLL().GetModel(_saleTypeId);

            if (stype.ShoppingFlag == 0)//判断是否已经关闭消费了
            {
                rs.ReMsg = "Error|本模块已经停止消费下单，请下个月再来";
                return rs;
            }

            //string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            string status = "Error|申请失败";
            if (string.IsNullOrEmpty(fcardCode))
            {
                rs.ReMsg = status;
                return rs;
            }
            if (!(fcardCode.Length == 10 || fcardCode.Length == 11))
            {
                rs.ReMsg = status;
                return rs;
            }
            List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode.ToString() + "'");

            #region 验证IC卡状态
            if (cards.Count == 0)
            {
                status = "Error|该卡找不对应人员信息";
                rs.ReMsg = status;
                return rs;
            }
            switch (cards[0].cardflaga)
            {
                case 4:
                    {
                        rs.ReMsg = "Error|用户已经离监，IC卡已经停用";
                        return rs;
                    }
                case 3:
                    {
                        rs.ReMsg = "Error|IC卡已作废，不能用";
                        return rs;
                    }
                case 2:
                    {
                        rs.ReMsg = "Error|IC卡已挂失，不能用";
                        return rs;
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
                        rs.ReMsg = "Error|银行卡未开通过，不能用消费";
                        return rs;
                    }
                    if (cards[0].RegFlag == 0)
                    {
                        rs.ReMsg = "Error|银行卡未开通过，不能用消费";
                        return rs;
                    }
                }
            }
            else
            {
                if (cards[0].RegFlag == null)
                {
                    rs.ReMsg = "Error|银行卡未开通过，不能用消费";
                    return rs;
                }
                if (cards[0].RegFlag == 0)
                {
                    rs.ReMsg = "Error|银行卡未开通过，不能用消费";
                    return rs;
                }
            }
            #endregion


            string fcrimeCode = cards[0].fcrimecode;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, Convert.ToInt32(saleTypeId));
            if (criminal.ErrInfo != "")
            {
                rs.ReMsg = "Error|" + criminal.ErrInfo + "，请与管理人员联系";
                return rs;
            }

            #region 判断是队别是否已经关账停止消费了
            try
            {
                if (criminal.SaleCloseFlag == 1)
                {
                    rs.ReMsg = "Error|您所在队别已经停止消费下单了，请下个月再来购买";
                    return rs;
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
                                    rs.ReMsg = "Error|今天不是您队消费下单日期，请在本队的下单日来购买，谢谢";
                                    return rs;
                                }

                            }
                            else
                            {
                                rs.ReMsg = "Error|本模块未设置你队的购买日期，请与管理员联系";
                                return rs;
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }

            #endregion



            if (criminal.AmountAmoney < 0)
            {
                if (criminal.dongjieMoney + criminal.AmountBmoney >= 0)
                {
                    rs.ReMsg = "Error|该账户有冻结" + criminal.dongjieMoney.ToString() + " 元，余额不足，不能消费，请与管理人员联系";
                }
                else
                {
                    rs.ReMsg = "Error|该账户A账出现负数不能消费，请与管理人员联系";
                }
                
                return rs;
            }

            if (criminal.AmountBmoney < 0)
            {
                if (criminal.dongjieMoney + criminal.AmountBmoney >= 0)
                {
                    rs.ReMsg = "Error|该账户有冻结" + criminal.dongjieMoney.ToString() + " 元，余额不足，不能消费，请与管理人员联系";

                }
                else
                {
                    rs.ReMsg = "Error|该账户B账出现负数不能消费，请与管理人员联系";
                }

                return rs;

            }

            if (criminal.AmountCmoney < 0)
            {
                rs.ReMsg = "Error|该账户C账出现负数不能消费，请与管理人员联系";
                return rs;
            }

            if (criminal.CanUseMoneyA < 0)
            {
                rs.ReMsg = "Error|A账户已经超过本月最大可消费额度了，请与管理人员联系";
                return rs;
            }
            if (criminal.CanUseMoneyB < 0)
            {
                rs.ReMsg = "Error|B账户已经超过本月最大可消费额度了，请与管理人员联系";
                return rs;
            }


            //验证是否在该管理卡权限范围内的人员
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel(ip);

            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            //取得用户登录名称
            string loginUserName = "";
            if (loginMode.MgrValue == "2")
            {
                loginUserName = strIpAddr;
            }
            else
            {
                loginUserName = GetLoginUserName(mset);
            }



            //查询是有未提交的订单
            if (checkflag == "1")
            {
                List<T_SHO_Order> orders = new T_SHO_OrderBLL().GetModelList(" fcrimecode='" + fcrimeCode + "' and (flag=0 or (flag=1 and isnull(InvoiceNo,'')='')) and isnull(InvoiceNo,'')='' and PType='" + stype.PType + "' and crtdate>='" + DateTime.Today.ToShortDateString() + "' and CrtDate<'" + DateTime.Now.ToString() + "'");
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
                            rs = GetOrderListInfo(status, criminal, orders, Convert.ToInt32(saleTypeId));//获取订单列表信息
                            
                        }
                        else
                        {
                            new T_SHO_OrderBLL().Delete(order.OrderID);
                        }
                    }
                    if (rs.Flag == true && rs.ReMsg == "OK|Three")
                    {//显示并创建订单信息
                        return rs; //获取订单列表信息 
                    }
                    else 
                    {
                        rs = DisplayAddOrder(fcrimeCode, ip, status, criminal, _saleTypeId.ToString(), loginUserName);
                    }

                }
                else
                {//显示并创建订单信息
                    rs = DisplayAddOrder(fcrimeCode, ip, status, criminal, _saleTypeId.ToString(), loginUserName);
                }
            }
            else
            {
                //显示并创建订单信息
                rs = DisplayAddOrder(fcrimeCode, ip, status, criminal, _saleTypeId.ToString(), loginUserName);
            }
            return rs;
        }

        public ActionResult SubmitOrder(string orderId, string fcrimecode)//提交订单
        {
            ResultInfo rs = new ResultInfo();
            string crtby = "";
            string userRoomNo = "";

            if (string.IsNullOrEmpty(orderId) == true)
            {                
                rs.ReMsg = "Err|订单号不能为空。";
                return Json(rs);
            }
            if (string.IsNullOrEmpty(fcrimecode) == true)
            {
                rs.ReMsg = "Err|用户编号不能为空。";
                return Json(rs);
            }

            if (new T_InvoiceBLL().Exists(Convert.ToInt32(orderId)))
            {
                rs.ReMsg = "Error|该订单号已经结算过了，不能再重复结算（提交提示）";
                return Json(rs);
            }


            //userRoomNo = Convert.ToInt32(userRoomNo).ToString(); //改为一位数

            userRoomNo = ""; //改为一位数

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
            //crtby = "IP_" + ipLastCode + "号机";
            T_SHO_Order orderModel = new T_SHO_OrderBLL().GetModel(Convert.ToInt32(orderId));
            ////如果订单操作员有存在就直接用订单的操作员
            //if (string.IsNullOrEmpty(orderModel.CrtBy) == false)
            //{
            //    crtby = orderModel.CrtBy;
            //}

            crtby = "出监机_" + ipLastCode;


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
                rs.ReMsg = "Err|【有的商品超过本月队别最大购买数量】，不能提交。";
                return Json(rs);
            }

            #endregion

            if (orderModel == null)
            {
                rs.ReMsg = "Err|该订单号不存在。";
                return Json(rs);
            }

            if (orderModel.FAmount == 0)
            {
                rs.ReMsg = "Err|该订单没有任何商品信息，不能提交。";
                return Json(rs);
            }

            if (orderModel.Flag == 1)
            {
                rs.ReMsg = "Err|该订单已经在提交中，不必重复提交。";
                return Json(rs);
            }
            else if (orderModel.Flag >= 2)
            {
                rs.ReMsg = "Err|该订单已经结算，不能重复结算。";
                return Json(rs);
            }
            orderModel.Flag = 1;
            if (new T_SHO_OrderBLL().Update(orderModel))//更订单状态为1“提交中”
            {
                string status = new T_SHO_OrderBLL().SubmitOrder(Convert.ToInt32(orderId), crtby, ipLastCode, fcrimecode, userRoomNo);
                if (status == "OK|结算成功。")
                {
                    rtnPaySubmitInfo<T_Invoice, T_InvoiceDTL> rtns = new rtnPaySubmitInfo<T_Invoice, T_InvoiceDTL>();

                    T_Invoice invoice = new T_InvoiceBLL().GetModelList("OrderId='" + orderId + "'")[0];
                    List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + invoice.PType + "'");
                    T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode, saleTypes[0].Id);
                    List<T_InvoiceDTL> details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoice.InvoiceNo + "'");

                    rtns.details = details;
                    rtns.invoice = invoice;
                    rtns.criminal = criminal;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    //return Content(status + "|" + jss.Serialize(rtns));
                    rs.Flag=true;
                    rs.ReMsg = "OK|结算成功。";
                    rs.DataInfo = rtns;
                    return Json(rs);
                }
                else
                {
                    //先将状态改回0
                    orderModel.Flag = 0;
                    new T_SHO_OrderBLL().Update(orderModel);
                    rs.ReMsg = status;
                    return Json(rs);
                }
            }
            else
            {
                rs.ReMsg = "Err|更新订单状态为提交时失败。";
                return Json(rs);
            }
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
                    if (Session["loginUserName"] != null)
                    {
                        loginUserName = Session["loginUserName"].ToString();
                    }
                    else
                    {
                        loginUserName = strIpAddr;
                    }

                }
                catch
                {
                    loginUserName = strIpAddr;
                }

            }
            return loginUserName;
        }


        private static ResultInfo GetOrderListInfo(string status, T_Criminal criminal, List<T_SHO_Order> orders, int saleTypeId)
        {
            ResultInfo rs = new ResultInfo();
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
            JavaScriptSerializer css = new JavaScriptSerializer();

            List<T_SHO_OrderDTL> details = new T_SHO_OrderDTLBLL().GetModelList("OrderId='" + orders[0].OrderID.ToString() + "'");

            var goodLists = _jifenMgrService.QueryList<T_Goods>("select * from t_goods where active='Y';");

            rts.lists = details.Select(d => new T_SHO_OrderDTL()
            {   ID=d.ID,
                Flag=d.Flag, 
                FreeFlag=d.FreeFlag,
                OrderID=d.OrderID,
                FTZSP_TypeFlag=d.FTZSP_TypeFlag,
                GPrice=d.GPrice,
                GCount=d.GCount,
                GAmount=d.GAmount,
                GCode=d.GCode,
                GName=d.GName,
                GTXM=d.GTXM,
                SPShortCode=d.SPShortCode,
                WareHouseCode=d.WareHouseCode,
                src= goodLists.Where(o=>o.GTXM==d.GTXM).FirstOrDefault()?.src,
            }
            ).ToList();


            //增加商品类型信息===Start===================
            rts.dengjiMgrFlag = _dengjiMgrFlag;
            if (_dengjiMgrFlag == "1")
            {
                decimal criminalKoufen = GetCurrMonthKoufen(criminal.FCode);
                var goods = _jifenMgrService.QueryList<T_Goods>("select a.* from t_goods a,T_JF_GoodsLevel b,T_GoodsType c where a.ACTIVE='Y' and a.LevelName=b.LevelName and a.GType=c.FCode and c.saleTypeId=@saleTypeId and b.CompletionRate>=@CompletionRate ", new { CompletionRate = criminalKoufen, saleTypeId = saleTypeId });
                var gtypes = _jifenMgrService.QueryList<T_GoodsType>("select * from t_goodstype where UseType=0 and saleTypeId=@saleTypeId and FCode in @fcodes", new { saleTypeId = saleTypeId, fcodes = goods.Select(g => g.GTYPE).Distinct().ToArray() });

                rts.goods = goods;
                rts.gtypes = gtypes;
            }

            //增加商品类型信息===End===================

            //status = css.Serialize(rts);
            //status = "There|" + status;
            

            rs.Flag = true; 
            rs.ReMsg= "OK|Three";

            rs.DataInfo = rts;
            return rs;
        }

        private static ResultInfo DisplayAddOrder(string fcrimeCode, string ip, string status, T_Criminal criminal, string saleTypeId, string loginUserName)
        {
            ResultInfo rs = new ResultInfo();
            //删除该犯人的所有临时订单信息
            new T_SHO_OrderBLL().DeleteOrderInfoByFCrimecode(fcrimeCode, saleTypeId);

            //T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode);
            T_SHO_SaleType saletype = new T_SHO_SaleTypeBLL().GetModel(Convert.ToInt32(saleTypeId));
            string crtby = "";
            string ipLastCode = GetIpAddressLastCode(ip);
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



                //增加商品类型信息===Start===================
                rts.dengjiMgrFlag = _dengjiMgrFlag;
                if (_dengjiMgrFlag == "1")
                {
                    decimal criminalKoufen = GetCurrMonthKoufen(criminal.FCode);
                    //====zenglj 2026-03-10 增加完成率部分的积分转换===Start===============================
                    decimal jifen = CompletionRateToJifen(criminal.CompletionRate);
                    if (criminalKoufen < jifen)
                    {
                        criminalKoufen = jifen;
                    }
                    //====zenglj 2026-03-10 增加完成率部分的积分转换===End===============================

                    var goods = _jifenMgrService.QueryList<T_Goods>("select a.* from t_goods a,T_JF_GoodsLevel b,T_GoodsType c where a.ACTIVE='Y' and a.LevelName=b.LevelName and a.GType=c.FCode and c.saleTypeId=@saleTypeId and b.CompletionRate>=@CompletionRate ", new { CompletionRate = criminalKoufen, saleTypeId = saleTypeId });

                    var gtypes = _jifenMgrService.QueryList<T_GoodsType>("select * from t_goodstype where UseType=0 and saleTypeId=@saleTypeId and FCode in @fcodes", new { saleTypeId = saleTypeId, fcodes = goods.Select(g => g.GTYPE).Distinct().ToArray() });
                    rts.goods = goods;
                    rts.gtypes = gtypes;
                }
                //增加商品类型信息===End===================

                JavaScriptSerializer css = new JavaScriptSerializer();
                status = css.Serialize(rts);
                status = "OK|" + status;

                rs.Flag = true;
                rs.DataInfo = rts;
                rs.ReMsg = "OK|成功";
            }
            else
            {
                rs.ReMsg = "Err|创建订单失败!";
            }
            return rs;
        }


        /// <summary>
        /// 计算扣分的值
        /// </summary>
        /// <param name="fcode"></param>
        /// <returns></returns>
        private static decimal GetCurrMonthKoufen(string fcode)
        {
            var year = DateTime.Today.AddMonths(-1).Year;
            var month = DateTime.Today.AddMonths(-1).Month;
            var ls = _jifenMgrService.QueryList<T_JF_KouFen>("select * from t_JF_KouFen where IsDelete=0 and CreateDate>=@CreateDate and FCode=@FCode"
                , new { CreateDate = new DateTime(year, month, 1), FCode = fcode });
            if (ls.Count <= 0)
            {
                return 0;
            }
            return ls.Sum(o => o.ScoreValue);
        }

        /// <summary>
        /// 根据完成率获取积分比例转换值。
        /// </summary>
        /// <param name="completionRate"></param>
        /// <returns></returns>
        private static decimal CompletionRateToJifen(decimal completionRate)
        {
            decimal jifen = 0;
            var comRateLevel = _goodsDjs.Where(o => o.UseType == 1 && o.CompletionRate <= completionRate)
                .OrderByDescending(p => p.CompletionRate).FirstOrDefault();
            if (comRateLevel != null)
            {
                switch (comRateLevel.LevelName)
                {
                    case "A":
                        {
                            jifen = _goodsDjs.Where(o => o.UseType == 0 && o.LevelName == "一级").First().CompletionRate;
                        }
                        break;
                    case "B":
                        {
                            jifen = _goodsDjs.Where(o => o.UseType == 0 && o.LevelName == "二级").First().CompletionRate;
                        }
                        break;
                    case "C":
                        {
                            jifen = _goodsDjs.Where(o => o.UseType == 0 && o.LevelName == "三级").First().CompletionRate;
                        }
                        break;
                    default:
                        {
                            jifen = _goodsDjs.Where(o => o.UseType == 0 && o.LevelName == "四级").First().CompletionRate;
                            break;
                        }
                }

            }
            else
            {
                //默认只能购买四级积分商品，所以直接返回四级积分商品的积分比例
                var row = _goodsDjs.Where(o => o.UseType == 0 && o.LevelName == "四级").FirstOrDefault();
                jifen = row == null ? 0 : row.CompletionRate;
            }

            return jifen;
        }



        #region 停止购物

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

        #endregion
    }
}