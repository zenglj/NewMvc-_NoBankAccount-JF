using Newtonsoft.Json;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    /// <summary>
    /// 银行充值记录管理（基于 T_Bank_Recharge 表）
    /// </summary>
    [MyLogActionFilterAttribute]
    public class RechargeController : BaseController
    {
        BaseDapperBLL _bll = new BaseDapperBLL();

        #region ==========银行充值记录模块 Start==============

        /// <summary>
        /// 充值记录首页
        /// </summary>
        /// <param name="id">权限Id</param>
        /// <returns></returns>
        public ActionResult Index(int id = 1)
        {
            ViewData["id"] = id;
            return View();
        }

        /// <summary>
        /// 获取充值记录的分页数据
        /// </summary>
        /// <param name="strJsonWhere">查询条件 JSON</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public ActionResult GetRechargeList(string strJsonWhere = "", int page = 1, int rows = 10)
        {
            var list = _bll.GetPageList<T_Bank_Recharge, T_Bank_Recharge_Search>("Id", strJsonWhere, page, rows);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取充值记录的分页数据（返回 Json）
        /// </summary>
        /// <param name="strJsonWhere">查询条件 JSON</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public ActionResult GetRechargeJson(string strJsonWhere = "", int page = 1, int rows = 10)
        {
            var list = _bll.GetPageList<T_Bank_Recharge, T_Bank_Recharge_Search>("Id", strJsonWhere, page, rows);
            return Json(list);
        }

        /// <summary>
        /// 根据 Id 获取单条充值记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetRechargeRecord(int id)
        {
            var model = _bll.GetModel<T_Bank_Recharge>(id);
            return Json(model);
        }

        /// <summary>
        /// Excel 导出
        /// </summary>
        /// <param name="strJsonWhere">查询条件 JSON</param>
        /// <returns></returns>
        public ActionResult DoRechargeExcelOut(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            var list = _bll.GetPageList<T_Bank_Recharge, T_Bank_Recharge_Search>("Id", strJsonWhere, 1, 10000);
            if (list.rows.Count <= 0)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));
            }
            string strFileName = "银行充值记录" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            string fullName = Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(list.rows, "银行充值记录", fullName);
            rs.Flag = true;
            rs.DataInfo = strFileName;
            rs.ReMsg = "OK|成功";
            return Json(rs);
        }

        #endregion =====银行充值记录模块 End============
    }
}
