using SelfhelpOrderMgr.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfhelpOrderMgr.Model;
using System.Transactions;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class DamagesMgrController : Controller
    {
        BaseDapperBLL _bll = new BaseDapperBLL();
        // GET: DamagesMgr
        public ActionResult Index()
        {
            return View();
        }

        #region =======赔偿金基本类型Start===========

        /// <summary>
        /// 获取基本类型的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetBaseTypeList(string strJsonWhere="",int page=1,int rows=10)
        {
            var list = _bll.GetPageList<T_PCJ_BaseType>("Id", strJsonWhere, page, rows, " isDelete=0");
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
        }

        /// <summary>
        /// 保存基本类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BaseTypeSave(T_PCJ_BaseType model)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_PCJ_BaseType>(model.Id);
                    oldModel.TypeName = model.TypeName;
                    oldModel.MaxUseMoney = model.MaxUseMoney;
                    oldModel.RetentionRate = model.RetentionRate;
                    oldModel.Remark = model.Remark;
                    oldModel.ModBy = "test";
                    oldModel.ModifyDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
                    model.CrtBy = "test";
                    model.CreateDate = DateTime.Now;
                    _bll.Insert(model);
                }

                rs.Flag = true;
                rs.ReMsg = "OK|保存成功";
                rs.DataInfo = model;
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
            }
            
        }

        /// <summary>
        /// 根据id删除基本类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BaseTypeDelete(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (id > 0)
                {
                    var oldModel = _bll.GetModel<T_PCJ_BaseType>(id);
                    oldModel.ModBy = "test";
                    oldModel.ModifyDate = DateTime.Now;
                    oldModel.isDelete = true;
                    _bll.Update(oldModel);
                    rs.Flag = true;
                    rs.ReMsg = "OK|保存成功";
                    return Json(rs);
                }
                else
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|编号必须大于0";
                    return Json(rs);
                }

                
            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
            }
        }

        #endregion =======赔偿金基本类型 End===========

        #region ==========赔偿金模块Start==============

        /// <summary>
        /// 赔偿金首页
        /// </summary>
        /// <returns></returns>
        public ActionResult RequestRecIndex()
        {
            return View();
        }


        /// <summary>
        /// 获取赔偿金的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecordList(string strJsonWhere="", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_PCJ_RequestRecord, T_PCJ_RequestRecord_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
        }


        /// <summary>
        /// 根据Id获取赔偿金记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecords(int id)
        {
            var model = _bll.GetModel<T_PCJ_RequestRecord>(id);
            return Json(model);
        }

        /// <summary>
        /// 保存赔偿金申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult RequestRecSave(T_PCJ_RequestRecord model)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                List<T_AREA> areas = new T_AREABLL().GetModelList("");
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_PCJ_RequestRecord>(model.Id);
                    oldModel.TypeId = model.TypeId;
                    oldModel.TypeName = _bll.GetModelList<T_PCJ_BaseType>("").Where(o => o.Id == model.TypeId).FirstOrDefault()?.TypeName;
                    oldModel.FAreaCode = model.FAreaCode;
                    oldModel.FAreaName = areas.Where(o=>o.FCode==model.FAreaCode).FirstOrDefault()?.FName;
                    oldModel.UseMonths = model.UseMonths;
                    oldModel.EndTime = model.EndTime;
                    oldModel.Remark = model.Remark;
                    oldModel.ModBy = "test";//base.loginUserName;
                    oldModel.ModifyDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
                    model.TypeName = _bll.GetModelList<T_PCJ_BaseType>("").Where(o => o.Id == model.TypeId).FirstOrDefault()?.TypeName;
                    model.FAreaName = areas.Where(o => o.FCode == model.FAreaCode).FirstOrDefault()?.FName;
                    model.CrtBy = "test";//base.loginUserName;
                    model.CreateDate = DateTime.Now;
                    _bll.Insert(model);
                }

                rs.Flag = true;
                rs.ReMsg = "OK|保存成功";
                rs.DataInfo = model;
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
            }

        }

        /// <summary>
        /// 删除赔偿金申请记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RequestRecDelete(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                
                if (id > 0)
                {
                    var oldModel = _bll.GetModel<T_PCJ_RequestRecord>(id);
                    var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCrimeCode);
                    //判断用户是否具有删除的管理权限
                    var areas = _bll.QueryList<T_Czy_area>("select *from T_Czy_area where fflag=2 and fcode=@fcode and fareacode=@fareacode",
                        new { fcode = "102"//base.loginUserCode
                        , fareacode = criminal.FAreaCode });
                    if(areas==null || areas.Count <= 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|操作员没有该用户的删除管理权限";
                        return Json(rs);
                    }
                    oldModel.ModBy = "test";// base.loginUserName;
                    oldModel.ModifyDate = DateTime.Now;
                    oldModel.IsDelete = true;
                    _bll.Update(oldModel);
                    rs.Flag = true;
                    rs.ReMsg = "OK|保存成功";
                    return Json(rs);
                }
                else
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|编号必须大于0";
                    return Json(rs);
                }


            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
            }
        }

        /// <summary>
        /// 审核赔偿金申请记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RequestRecAudit(int id,int flag,string auditText)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (id > 0)
                {
                    var oldModel = _bll.GetModel<T_PCJ_RequestRecord>(id);
                    if (oldModel.Flag >0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|无需重新审核";
                        return Json(rs);
                    }
                    var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCrimeCode);
                    //判断用户是否具有删除的管理权限
                    var areas = _bll.QueryList<T_Czy_area>("select *from T_Czy_area where fflag=2 and fcode=@fcode and fareacode=@fareacode", 
                        new { fcode = "102"// base.loginUserCode
                        , fareacode = criminal.FAreaCode });
                    if (areas == null || areas.Count <= 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|操作员没有该用户的管理权限";
                        return Json(rs);
                    }

                    using (TransactionScope ts = new TransactionScope())
                    {
                        oldModel.ModBy = "test";// base.loginUserName;
                        oldModel.ModifyDate = DateTime.Now;
                        oldModel.Flag = flag;
                        oldModel.AuditText = auditText;
                        _bll.Update(oldModel);

                        if (flag == 1)
                        { 
                            criminal.DamagesFlag = 1; 
                        }
                        else
                        {
                            criminal.DamagesFlag = 0;
                        }
                        _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject( new { DamagesFlag=1}),"FCode='"+criminal.FCode+"'",false);
                        ts.Complete();
                    }
                    
                    rs.Flag = true;
                    rs.ReMsg = "OK|审核通过";
                    return Json(rs);
                }
                else
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|编号必须大于0";
                    return Json(rs);
                }


            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
            }
        }
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult DoExcelOut(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            var list = _bll.GetPageList<T_PCJ_RequestRecord, T_PCJ_RequestRecord_Search>("Id", strJsonWhere, 1, 10000, " isDelete=0");
            if (list.rows.Count <= 0)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

            }
            string strFileName = "赔偿金申请记录" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            string fullName=Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(list.rows, "赔偿金申请记录", fullName);
            rs.Flag = true;
            rs.DataInfo = strFileName;
            rs.ReMsg = "OK|成功";
            return Json(rs);
            //return File(ms.ToArray(), "application/vnd.ms-excel", "赔偿金申请记录" + DateTime.Today.ToString("yyyyMMdd")+".xls");

        }

        #endregion =====赔偿金模块End============




        #region ========赔偿金自助申请Start================

        public ActionResult SelfRequestIndex()
        {
            return View();
        }

        #endregion ========赔偿金自助申请End==================

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
            var crim = _bll.QueryModel<T_Criminal>("FCode",fcrimecode);
            if(crim==null || crim.fflag == 1)
            {
                rs.Flag = false;
                rs.ReMsg = "Err|编号不存在或离监";

            }
            else
            {
                rs.Flag = true;
                rs.ReMsg = "OK|成功";
                rs.DataInfo = crim;
            }
            return Json(rs);
        }

        public ActionResult GetBaseTypes()
        {
            List<T_PCJ_BaseType> list = _bll.QueryList<T_PCJ_BaseType>("select * from T_PCJ_BaseType where isDelete=@isDelete", new { isDelete =0});
            var ls = list.Select(o => new { FCode = o.Id.ToString(), FName = o.TypeName }).ToList();
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(ls));
        }
    }
}