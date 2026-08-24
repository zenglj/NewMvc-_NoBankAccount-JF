using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class YiyuanController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private BaseDapperBLL _baseDapperBll = new BaseDapperBLL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCardInfo()
        {
            string fcardCode = Request["FCardCode"];
            if (string.IsNullOrEmpty(fcardCode))
            {
                return Content("Error|卡号不能为空");
            }

            List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode + "'");
            if (cards.Count == 0)
            {
                return Content("Error|该卡找不到对应人员信息");
            }

            var card = cards[0];
            switch (card.cardflaga)
            {
                case 4:
                    return Content("Error|用户已经离监，IC卡已停用");
                case 3:
                    return Content("Error|IC卡已作废，不能使用");
                case 2:
                    return Content("Error|IC卡已挂失，不能使用");
            }

            string fcrimeCode = card.fcrimecode;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, 1);
            if (criminal.ErrInfo != "")
            {
                return Content("Error|" + criminal.ErrInfo);
            }

            var result = new
            {
                FCrimeCode = criminal.FCode,
                FName = criminal.FName,
                FAreaName = criminal.FAreaName,
                CyName = criminal.CyName,
                FCYCode = criminal.FCYCode,
                FAreaCode = criminal.FAreaCode,
                NoXiaofeimoney = criminal.NoXiaofeimoney,
                OkUseAllMoney = criminal.OkUseAllMoney,
                Xiaofeimoney = criminal.Xiaofeimoney,
                AmountAmoney = criminal.AmountAmoney,
                AmountBmoney = criminal.AmountBmoney,
                AmountCmoney = criminal.AmountCmoney
            };

            return Content("OK|" + jss.Serialize(result));
        }

        [HttpPost]
        public ActionResult GetDrugInfo()
        {
            string drugCode = Request["DrugCode"];
            if (string.IsNullOrEmpty(drugCode))
            {
                return Content("Error|药品简码不能为空");
            }

            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("SPShortCode='" + drugCode + "' and Active='Y' and GType in(select FCode from T_GoodsType a,T_SHO_SaleType b where a.SaleTypeId=b.ID and b.PType='医院消费')");

            if (goods.Count == 0)
            {
                goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("GTXM='" + drugCode + "' and Active='Y'  and GType in(select FCode from T_GoodsType a,T_SHO_SaleType b where a.SaleTypeId=b.ID and b.PType='医院消费')");
            }

            if (goods.Count == 0)
            {
                return Content("Error|未找到该药品信息");
            }

            if (goods.Count > 1)
            {
                var drugs = goods.Select(g => new
                {
                    g.GCODE,
                    g.GNAME,
                    g.GDJ,
                    g.GUnit,
                    g.GStandard,
                    g.GTXM,
                    g.SPShortCode,
                    g.GBalance,
                    g.GTYPE,
                    g.Ffreeflag,
                    g.Xgsl
                }).ToList();
                return Content("MULTI|" + jss.Serialize(drugs));
            }

            var good = goods[0];
            if (good.ACTIVE == "N")
            {
                return Content("Error|该药品已下架");
            }
            var kcMset = new T_SHO_ManagerSetBLL().GetModel("YanZhenKCL");
            var drugInfo = new
            {
                GCODE = good.GCODE,
                GNAME = good.GNAME,
                GDJ = good.GDJ,
                GUnit = good.GUnit,
                GStandard =  good.GStandard,
                GTXM = good.GTXM,
                SPShortCode = good.SPShortCode,
                GBalance = kcMset.KeyMode == 0 ? 9999 : good.GBalance,
                GTYPE = good.GTYPE,
                Ffreeflag = good.Ffreeflag,
                Xgsl = good.Xgsl
            };

            return Content("OK|" + jss.Serialize(drugInfo));
        }

        [HttpPost]
        public ActionResult SubmitPrescription()
        {
            string fcrimecode = Request["FCrimeCode"];
            string doctorName = Request["DoctorName"];
            string diagnosis = Request["Diagnosis"];
            string drugListJson = Request["DrugList"];

            if (string.IsNullOrEmpty(fcrimecode))
            {
                return Content("Error|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(drugListJson))
            {
                return Content("Error|处方药品不能为空");
            }

            List<PrescriptionDrug> drugList = jss.Deserialize<List<PrescriptionDrug>>(drugListJson);
            if (drugList == null || drugList.Count == 0)
            {
                return Content("Error|处方药品列表为空");
            }

            decimal totalAmount = drugList.Sum(d => d.Amount);

            if (totalAmount <= 0)
            {
                return Content("Error|处方金额必须大于0");
            }

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode, 1);
            if (criminal == null || criminal.FCode == null)
            {
                return Content("Error|未找到用户信息");
            }

            if (criminal.NoXiaofeimoney < totalAmount)
            {
                return Content("Error|用户可消费金额不足，当前可用余额：" + criminal.NoXiaofeimoney + "元，处方金额：" + totalAmount + "元");
            }

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string loginUserName = "";
            try
            {
                if (Session["loginUserName"] != null)
                {
                    loginUserName = Session["loginUserName"].ToString();
                }
            }
            catch
            {
                loginUserName = "Yiyuan";
            }


            string cc = ".";
            string[] ips = ip.Split(cc.ToCharArray());
            //string ipLastCode = string.Format("000", ips[3]);
            string ipLastCode = "001";
            if (ips.Length > 3)
            {
                ipLastCode = "000" + ips[3];
                ipLastCode = ipLastCode.Substring(ipLastCode.Length - 3);
            }

            string crtby = loginUserName + "_" +ipLastCode + "号机";
            string prescriptionNo = "RX" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999);

            // 1. 创建订单主单 T_SHO_Order
            T_SHO_Order orderModel = new T_SHO_Order();
            orderModel.FCrimecode = fcrimecode;
            orderModel.FCriminal = criminal.FName;
            orderModel.CrtDate = DateTime.Now;
            orderModel.Flag = 0; // 0-草稿/未提交, 1-已提交
            orderModel.FAmount = totalAmount;
            orderModel.PType = "医院消费"; // 医院/处方类型
            orderModel.InvoiceNo = "";
            orderModel.IPAddr = ip;
            orderModel.FreeAmount = totalAmount;
            orderModel.RoomNO = "";
            orderModel.CrtBy = crtby;
            orderModel.FTZSP_Money = 0;

            try
            {
                int orderId = new T_SHO_OrderBLL().Add(orderModel);
                if (orderId <= 0)
                {
                    return Content("Error|创建处方主单失败");
                }

                // 2. 循环写入订单明细 T_SHO_OrderDTL
                T_SHO_OrderDTLBLL dtlBll = new T_SHO_OrderDTLBLL();
                foreach (PrescriptionDrug drug in drugList)
                {
                    T_SHO_OrderDTL dtl = new T_SHO_OrderDTL();
                    dtl.OrderID = orderId;
                    dtl.GCode = drug.GCODE;
                    dtl.GTXM = drug.GCODE;
                    dtl.GName = drug.GNAME;
                    dtl.GCount = drug.Quantity;
                    dtl.GPrice = drug.GDJ;
                    dtl.GAmount = drug.Amount;
                    dtl.Flag = 0;
                    dtl.FreeFlag = 0;
                    dtl.Remark = (drug.Usage ?? "") + " 诊断:" + (diagnosis ?? "");
                    dtl.SPShortCode = drug.SPShortCode;
                    dtl.FTZSP_TypeFlag = 0;
                    dtl.WareHouseCode = "";

                    int dtlId = dtlBll.Add(dtl);
                    if (dtlId <= 0)
                    {
                        // 如果某条明细失败，尝试回滚主单
                        try
                        {
                            new T_SHO_OrderBLL().Delete(orderId);
                        }
                        catch { }
                        return Content("Error|写入药品【" + drug.GNAME + "】明细失败");
                    }
                }

                // 3. 更新主单Flag=1（已确认/提交）
                orderModel.OrderID = orderId;
                orderModel.Flag = 1;
                new T_SHO_OrderBLL().Update(orderModel);

                
                string status = new T_SHO_OrderBLL().SubmitOrder(Convert.ToInt32(orderId), crtby, ipLastCode, fcrimecode, "");

                if (status != "OK|结算成功。")
                {

                    _baseDapperBll.ExecuteSql(@"delete from T_Sho_Order where OrderId=@orderId;
                        delete from T_Sho_OrderDTL where OrderId=@orderId;"
                        , new { orderId=orderId});

                    return Content("Error|写入提交结算时失败：" + status);

                }
            }
            catch (Exception ex)
            {
                return Content("Error|写入数据库异常：" + ex.Message);
            }

            var result = new
            {
                PrescriptionNo = prescriptionNo,
                FCrimeCode = fcrimecode,
                FName = criminal.FName,
                FAreaName = criminal.FAreaName,
                CyName = criminal.CyName,
                DoctorName = doctorName,
                Diagnosis = diagnosis,
                TotalAmount = totalAmount,
                DrugCount = drugList.Count,
                SubmitTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Message = "处方开具成功，已自动扣款" + totalAmount.ToString("0.00") + "元，主单已写入T_SHO_Order，明细已写入T_SHO_OrderDTL"
            };

            return Content("OK|" + jss.Serialize(result));
        }

        [HttpPost]
        public ActionResult GetCriminalInfoByCode()
        {
            string fcrimeCode = Request["FCrimeCode"];
            if (string.IsNullOrEmpty(fcrimeCode))
            {
                return Content("Error|用户编号不能为空");
            }

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, 1);
            if (criminal.ErrInfo != "")
            {
                return Content("Error|" + criminal.ErrInfo);
            }

            var result = new
            {
                FCrimeCode = criminal.FCode,
                FName = criminal.FName,
                FAreaName = criminal.FAreaName,
                CyName = criminal.CyName,
                FCYCode = criminal.FCYCode,
                NoXiaofeimoney = criminal.NoXiaofeimoney,
                OkUseAllMoney = criminal.OkUseAllMoney
            };

            return Content("OK|" + jss.Serialize(result));
        }
    }

    public class PrescriptionDrug
    {
        public string GCODE { get; set; }
        public string GNAME { get; set; }
        public string SPShortCode { get; set; }
        public decimal GDJ { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string GUnit { get; set; }
        public string GStandard { get; set; }
        public string Usage { get; set; }
    }
}