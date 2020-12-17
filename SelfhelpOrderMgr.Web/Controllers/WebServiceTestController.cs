using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class WebServiceTestController : Controller
    {
        //
        // GET: /WebServiceTest/
        public ActionResult Index()
        {
            return View();
        }

        //字符串方法
        public ActionResult WsPostString()
        {
            string Action = Request["Action"];
            //调用示例：
            Hashtable ht = new Hashtable();  //Hashtable 为webservice所需要的参数集
            string data;
            data = "3501005222";
            ht.Add("unitCode", "FJJXYY06");
            ht.Add("action", Action);
            ht.Add("data", data);
            XmlDocument xx = WebServiceCaller.QuerySoapWebService("http://localhost:7149/HospitalWS.asmx", "TranPrisonInfoForBank", ht, "");
            //MessageBox.Show(xx.OuterXml);
            return Content(xx.OuterXml);
        }

        //对象的方法
        public ActionResult WsPostObject()
        {
            string Action = Request["Action"];
            //调用示例：
            Hashtable ht = new Hashtable();  //Hashtable 为webservice所需要的参数集
            
            T_Invoice invoice = new T_InvoiceBLL().GetModel("INV170400721001");
            object data = new InvoiceEntity { 
                InvoiceNo=invoice.InvoiceNo,
                CardCode=invoice.CardCode,
                FCrimeCode=invoice.FCrimeCode,
                FCriminal=invoice.FCriminal,
                Amount=invoice.Amount,
                OrderDate=invoice.OrderDate,
                PayDate=invoice.PayDate,
                //PType=invoice.PType,
                PType = "建新住院消费",
                Flag=invoice.Flag,
                Remark=invoice.Remark,
                ServAmount=invoice.ServAmount,
                Crtby=invoice.Crtby,
                Fsn=invoice.fsn,
                FAreaCode=invoice.FAreaCode,
                FAreaName=invoice.FAreaName,
                Frealareacode=invoice.Frealareacode,
                FrealAreaName=invoice.FrealAreaName,
                AmountA=invoice.Amount,
                AmountB=invoice.AmountB,
                FreeAmountA=invoice.FreeAmountA,
                FreeAmountB=invoice.FreeAmountB,
                CardType=invoice.CardType,
                TypeFlag=invoice.TypeFlag,
                Fifoflag=invoice.Fifoflag,
                Checkflag=invoice.Checkflag,
                RoomNo=invoice.RoomNo,
                OrderId=invoice.OrderId,

                OrderStatus=0
            };
             
            
            

            ht.Add("unitCode", "33332222");
            ht.Add("action", Action);
            ht.Add("data", data);
            XmlDocument xx = WebServiceCaller.QuerySoapWebService("http://localhost:7149/HospitalWS.asmx", "TranPrisonInfoForBank", ht, "");
            //MessageBox.Show(xx.OuterXml);
            return Content(xx.InnerXml);
        }

        
	}
}