using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class BankVitualCardController : BaseController
    {

        private readonly static string vsaUrlBase = ConfigurationManager.ConnectionStrings["vsaUrlBase"].ConnectionString;
        private readonly static string mainAcct = ConfigurationManager.ConnectionStrings["mainacct"].ConnectionString;

        private string vsaAction = "";
        //private static readonly string vsaURL ="http://47.104.101.88:18080/api/vsa/batch-open";
        BaseDapperBLL _bll = new BaseDapperBLL();
        // GET: DamagesMgr



        #region ==========福费缴汇款记录模块Start==============

        /// <summary>
        /// 福费缴汇款记录首页
        /// </summary>
        /// <returns></returns>
        public ActionResult FfjRecIndex(int id = 1)
        {
            ViewData["id"] = id;
            return View();
        }


        /// <summary>
        /// 获取福费缴的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetFfjRecordList(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_Bank_Recharge, T_Bank_Recharge_Search>("Id", strJsonWhere, page, rows);
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取福费缴的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetFfjRecordJson(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_Bank_Recharge, T_Bank_Recharge_Search>("Id", strJsonWhere, page, rows);
            return Json(list);
        }

        /// <summary>
        /// 根据Id获取福费缴记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetFfjRecords(int id)
        {
            var model = _bll.GetModel<T_Bank_Recharge>(id);
            return Json(model);
        }


        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult DoFfjExcelOut(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            var list = _bll.GetPageList<T_Bank_Recharge, T_Bank_Recharge_Search>("Id", strJsonWhere, 1, 10000);
            if (list.rows.Count <= 0)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

            }
            string strFileName = "福费缴汇款记录" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            string fullName = Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(list.rows, "福费缴汇款记录", fullName);
            rs.Flag = true;
            rs.DataInfo = strFileName;
            rs.ReMsg = "OK|成功";
            return Json(rs);
            //return File(ms.ToArray(), "application/vnd.ms-excel", "赔偿金申请记录" + DateTime.Today.ToString("yyyyMMdd")+".xls");

        }



        #endregion =====福费缴汇款记录模块End============




        /// <summary>
        /// 获取队别信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAreas()
        {
            //队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("");
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(areas));
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <returns></returns>
        public ActionResult GetUserName(string fcrimecode)
        {
            ResultInfo rs = new ResultInfo();
            //队别
            var crim = _bll.QueryModel<T_Criminal>("FCode", fcrimecode);
            if (crim == null)
            {
                rs.Flag = false;
                rs.ReMsg = "Err|编号不存在";

            }
            else if (crim.fflag == 1)
            {
                rs.Flag = true;
                rs.ReMsg = "OK|成功,但已经离监了";
                rs.DataInfo = crim;

            }
            else
            {
                rs.Flag = true;
                rs.ReMsg = "OK|成功";
                rs.DataInfo = crim;
            }
            return Json(rs);
        }


    }
}