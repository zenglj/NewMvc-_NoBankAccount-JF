using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class BankAtmMgrController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        BankAtmService bll = new BankAtmService();
        //
        // GET: /BankAtmMgr/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBankDealList(string strJsonWhere, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { Id = 0 };
                strJsonWhere = jss.Serialize(sWhere);
            }

            PageResult<T_Bank_AtmDealList> list = bll.GetPageList<T_Bank_AtmDealList, T_Bank_AtmDealList_Search>("Id", strJsonWhere, page, rows);

            EasyUiPageResult<T_Bank_AtmDealList> rs = new EasyUiPageResult<T_Bank_AtmDealList>()
            {
                total = list.total,
                rows = list.rows
            };
            return Content(jss.Serialize(rs));
        }

        public ActionResult PrintDetailReport(string strJsonWhere)
        {

            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                return Content("Err|添加不能为空");
            }
            List<T_Bank_AtmDealList> res = bll.GetModelList<T_Bank_AtmDealList, T_Bank_AtmDealList_Search>(strJsonWhere, "Id asc", 10000);

            //ViewData["res"] = res.Where(o => (o.TranStatus == 2 || o.TranStatus == 1)).ToList();
            ViewData["res"] = res;

            CustomTimeArea customTimeArea = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomTimeArea>(strJsonWhere);

            ViewData["customTimeArea"] = customTimeArea;

            return View();
        }

        public ActionResult ExcelOutPrint(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                //return Content("Err|添加不能为空");
                rs.ReMsg = "Err|添加不能为空";
                return Json(rs);
            }
            List<T_Bank_AtmDealList> res = bll.GetModelList<T_Bank_AtmDealList, T_Bank_AtmDealList_Search>(strJsonWhere, "Id asc", 10000);

            string fileName = "AtmDetail" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string strFileName = Server.MapPath("~/Upload/" + fileName);
            ExcelRender.RenderListToExcel<T_Bank_AtmDealList>(res, "ATM机的记录", strFileName);

            rs.Flag = true;
            rs.ReMsg = "OK|导出成功";
            rs.DataInfo = fileName;
            return Json(rs);
        }

        public ActionResult GetMachineBalance()
        {
            string strJsonWhere="{}";
            int page = 1;
            int rows = 10;
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { Id = 0 };
                strJsonWhere = jss.Serialize(sWhere);
            }

            var rs = new ResultInfo() { 
                Flag=false,
                ReMsg="未处理",
                DataInfo=null
            };
            PageResult<T_Bank_AtmMachine> list = bll.GetPageList<T_Bank_AtmMachine, T_Bank_AtmMachine>("Id", strJsonWhere, page, rows);
            if (list.total == 0)
            {
                rs.ReMsg = "Err|找不到相应的记录";
            }
            else
            {
                rs.Flag = true;
                rs.ReMsg = "OK|成功";
                rs.DataInfo = list.rows[0];
            }
            return Json((rs),JsonRequestBehavior.AllowGet);
        }
	}
}