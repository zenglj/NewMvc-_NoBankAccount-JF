using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class DepositListController : Controller
    {

        JavaScriptSerializer jss = new JavaScriptSerializer();
        //
        // GET: /DepositList/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DepositList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DepositList/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DepositList/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                string id = collection["Id"];
                string userName = collection["userName"];
                string password = collection["password"];

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DepositList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DepositList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public JsonResult GetSearchList(string strJsonWhere, string orderField = " id asc ", int page = 1, int rows = 10)
        {
            //if (string.IsNullOrWhiteSpace(orderField))
            //{
            //    orderField=" id asc ";
            //}
            //ActionResultInfo rs = new ActionResultInfo();
            //try
            //{
                //rs.flag = true;
                //rs.msg = "查询成功";
                //rs.resultData = new T_Bank_DepositListBLL().GetPageList<T_Bank_DepositList>(orderField, strJsonWhere, pageIndex, pageSize);
            //}
            //catch(Exception e)
            //{
            //    rs.flag = false;
            //    rs.msg = e.Message.ToString();
            //    rs.resultData = null;
            //}
            //return Json(rs);

            PageResult<T_Bank_DepositList> rs = new T_Bank_DepositListBLL().GetPageList<T_Bank_DepositList,T_Bank_DepositList_Search>(orderField, strJsonWhere, page, rows);
            return Json(rs);
            //return Content(jss.Serialize(rs));
        }

        //
        // GET: /DepositList/Delete/5
        public JsonResult Delete(int id)
        {
            ActionResultInfo rs = new ActionResultInfo();
            if(new T_Bank_DepositListBLL().Delete<T_Bank_DepositList>(id))
            {
                rs.flag = true;
                rs.msg = "删除成功";
            }else
            {
                rs.flag = false;
                rs.msg = "删除失败";
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /DepositList/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
