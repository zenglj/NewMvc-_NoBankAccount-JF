using SelfhelpOrderMgr.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfhelpOrderMgr.Model;
using System.Transactions;
using SelfhelpOrderMgr.Web.Models;
using SelfhelpOrderMgr.Web.Filters;
using System.IO;
using NPOI.XSSF.UserModel;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class DamagesMgrController : LoginController
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
        public ActionResult GetBaseTypeList(string strJsonWhere = "", int page = 1, int rows = 10)
        {
            var list = _bll.GetPageList<T_PCJ_BaseType>("Id", strJsonWhere, page, rows, " isDelete=0");
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));

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
                    oldModel.ModBy = base.loginUserName;//操作员
                    oldModel.ModifyDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
                    model.CrtBy = base.loginUserName;//操作员
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
                    var ls = _bll.GetModelList<T_PCJ_RequestRecord>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeId = id, IsDelete = false }));
                    if (ls.Count > 0)
                    {
                        rs.ReMsg = "ERR|请先删除该类型下的申请记录";
                        return Json(rs);
                    }
                    var oldModel = _bll.GetModel<T_PCJ_BaseType>(id);
                    //oldModel.ModBy = base.loginUserName;//操作员
                    //oldModel.ModifyDate = DateTime.Now;
                    //oldModel.isDelete = true;
                    //_bll.Update(oldModel);
                    _bll.Delete<T_PCJ_BaseType>(id);
                    rs.Flag = true;
                    rs.ReMsg = "OK|删除成功";
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
        public ActionResult RequestRecIndex(int id = 1)
        {
            ViewData["id"] = id;
            return View();
        }


        /// <summary>
        /// 获取赔偿金的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecordList(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_PCJ_RequestRecord, T_PCJ_RequestRecord_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取赔偿金的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecordJson(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_PCJ_RequestRecord, T_PCJ_RequestRecord_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            return Json(list);
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
        /// 请求罪犯本人现有的申请记录
        /// </summary>
        /// <param name="fcode"></param>
        /// <returns></returns>
        public ActionResult GetRequestNowRecord(string fcode)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.QueryList<T_PCJ_RequestRecord>("select *from T_PCJ_RequestRecord where FCrimeCode=@FCrimeCode and isDelete=0 and Flag<2 and EndTime>getdate()", new { FCrimeCode = fcode }).FirstOrDefault();
                if (model != null)
                {
                    rs.Flag = true;
                    rs.ReMsg = "OK|成功";
                    rs.DataInfo = model;
                }
                else
                {
                    rs.ReMsg = "Err|没有申请记录";
                }

                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
                throw;
            }

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
                    oldModel.FAreaName = areas.Where(o => o.FCode == model.FAreaCode).FirstOrDefault()?.FName;
                    oldModel.UseMonths = model.UseMonths;
                    oldModel.EndTime = model.EndTime;
                    oldModel.Remark = model.Remark;
                    oldModel.ModBy = base.loginUserName;//操作员
                    oldModel.ModifyDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
                    model.TypeName = _bll.GetModelList<T_PCJ_BaseType>("").Where(o => o.Id == model.TypeId).FirstOrDefault()?.TypeName;
                    model.FAreaName = areas.Where(o => o.FCode == model.FAreaCode).FirstOrDefault()?.FName;
                    model.CrtBy = base.loginUserName;//操作员
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
                        new
                        {
                            fcode = base.loginUserCode
                        ,
                            fareacode = criminal.FAreaCode
                        });
                    if (areas == null || areas.Count <= 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|操作员没有该用户的删除管理权限";
                        return Json(rs);
                    }
                    using (TransactionScope ts = new TransactionScope())
                    {
                        oldModel.ModBy = base.loginUserName;//操作员
                        oldModel.ModifyDate = DateTime.Now;
                        oldModel.IsDelete = true;
                        _bll.Update(oldModel);

                        if (oldModel.Flag == 1)
                        {
                            criminal.DamagesFlag = 0;
                            criminal.DamagesControlMoney = 0;
                            criminal.DamagesRetentionRate = 0;
                            criminal.DamagesEndDate = new DateTime(1900, 1, 1);
                            _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { DamagesFlag = criminal.DamagesFlag, DamagesControlMoney = criminal.DamagesFlag, DamagesRetentionRate = criminal.DamagesRetentionRate }), "FCode='" + criminal.FCode + "'", false);
                        }
                        ts.Complete();
                    }

                    rs.Flag = true;
                    rs.ReMsg = "OK|删除成功";
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
        public ActionResult RequestRecAudit(int id, int flag, string auditText)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                //if (id > 0)
                //{
                //    var oldModel = _bll.GetModel<T_PCJ_RequestRecord>(id);
                //    if (oldModel.Flag > 0)
                //    {
                //        rs.Flag = false;
                //        rs.ReMsg = "Err|无需重新审核";
                //        return Json(rs);
                //    }
                //    var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCrimeCode);
                //    //判断用户是否具有删除的管理权限
                //    var areas = _bll.QueryList<T_Czy_area>("select *from T_Czy_area where fflag=2 and fcode=@fcode and fareacode=@fareacode",
                //        new
                //        {
                //            fcode = base.loginUserCode
                //        ,
                //            fareacode = criminal.FAreaCode
                //        });
                //    if (areas == null || areas.Count <= 0)
                //    {
                //        rs.Flag = false;
                //        rs.ReMsg = "Err|操作员没有该用户的管理权限";
                //        return Json(rs);
                //    }

                //    using (TransactionScope ts = new TransactionScope())
                //    {
                //        oldModel.ModBy = base.loginUserName;//操作员
                //        oldModel.ModifyDate = DateTime.Now;
                //        oldModel.Flag = flag;
                //        oldModel.AuditText = auditText;
                //        _bll.Update(oldModel);

                //        if (flag == 1)
                //        {
                //            criminal.DamagesFlag = 1;
                //            criminal.DamagesControlMoney = _bll.GetModel<T_PCJ_BaseType>(oldModel.TypeId).MaxUseMoney;
                //            criminal.DamagesRetentionRate = _bll.GetModel<T_PCJ_BaseType>(oldModel.TypeId).RetentionRate;
                //            //还有一个有效期
                //            criminal.DamagesEndDate = oldModel.EndTime;
                //        }
                //        else
                //        {
                //            criminal.DamagesFlag = 0;
                //            criminal.DamagesControlMoney = 0;
                //            criminal.DamagesRetentionRate = 0;
                //            criminal.DamagesEndDate = new DateTime(1900, 1, 1);
                //        }
                //        _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { DamagesFlag = criminal.DamagesFlag, DamagesControlMoney = criminal.DamagesFlag, DamagesRetentionRate = criminal.DamagesRetentionRate, DamagesEndDate = criminal.DamagesEndDate }), "FCode='" + criminal.FCode + "'", false);


                //        ts.Complete();
                //    }

                //    rs.Flag = true;
                //    rs.ReMsg = "OK|审核通过";
                //    return Json(rs);
                //}
                //else
                //{
                //    rs.Flag = false;
                //    rs.ReMsg = "Err|编号必须大于0";
                //    return Json(rs);
                //}
                rs = AuditRecordInfo(id, flag, auditText);
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
            }
        }

        /// <summary>
        /// 审核记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <param name="auditText"></param>
        /// <returns></returns>
        private ResultInfo AuditRecordInfo(int id, int flag, string auditText)
        {
            ResultInfo rs = new ResultInfo();
            if (id > 0)
            {
                var oldModel = _bll.GetModel<T_PCJ_RequestRecord>(id);
                if (oldModel.Flag > 0)
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|无需重新审核";
                    return (rs);
                }
                var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCrimeCode);
                //判断用户是否具有删除的管理权限
                var areas = _bll.QueryList<T_Czy_area>("select *from T_Czy_area where fflag=2 and fcode=@fcode and fareacode=@fareacode",
                    new
                    {
                        fcode = base.loginUserCode
                    ,
                        fareacode = criminal.FAreaCode
                    });
                if (areas == null || areas.Count <= 0)
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|操作员没有该用户的管理权限";
                    return (rs);
                }

                using (TransactionScope ts = new TransactionScope())
                {
                    oldModel.ModBy = base.loginUserName;//操作员
                    oldModel.ModifyDate = DateTime.Now;
                    oldModel.Flag = flag;
                    oldModel.AuditText = auditText;
                    _bll.Update(oldModel);

                    if (flag == 1)
                    {
                        criminal.DamagesFlag = 1;
                        criminal.DamagesControlMoney = _bll.GetModel<T_PCJ_BaseType>(oldModel.TypeId).MaxUseMoney;
                        criminal.DamagesRetentionRate = _bll.GetModel<T_PCJ_BaseType>(oldModel.TypeId).RetentionRate;
                        //还有一个有效期
                        criminal.DamagesEndDate = oldModel.EndTime;
                    }
                    else
                    {
                        criminal.DamagesFlag = 0;
                        criminal.DamagesControlMoney = 0;
                        criminal.DamagesRetentionRate = 0;
                        criminal.DamagesEndDate = new DateTime(1900, 1, 1);
                    }
                    _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { DamagesFlag = criminal.DamagesFlag, DamagesControlMoney = criminal.DamagesFlag, DamagesRetentionRate = criminal.DamagesRetentionRate, DamagesEndDate = criminal.DamagesEndDate }), "FCode='" + criminal.FCode + "'", false);


                    ts.Complete();
                }

                rs.Flag = true;
                rs.ReMsg = "OK|审核通过";
                return (rs);
            }
            else
            {
                rs.Flag = false;
                rs.ReMsg = "Err|编号必须大于0";
                return (rs);
            }
        }
        public ActionResult RequestRecBatchAudit(string ids, int flag, string auditText)
        {
            ResultInfo rs = new ResultInfo();
            try
            {

                var errList = new List<Pcj_ErrModel>();
                var idarry = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(ids);
                foreach (var id in idarry)
                {
                    rs = AuditRecordInfo(id, flag, auditText);
                    if (rs.Flag == false)
                    {
                        var _m = _bll.GetModel<T_PCJ_RequestRecord>(id);
                        errList.Add(new Pcj_ErrModel()
                        {
                            FCode = _m.FCrimeCode,
                            FName = _m.FCrimeName,
                            TypeName = _m.TypeName,
                            UseMonths = _m.UseMonths,
                            Remark = _m.Remark,
                            ErrInfo = rs.ReMsg
                        });
                    }
                    
                }
                if (errList.Count > 0)
                {
                    string strFileName = "批量审核结果" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                    string fullName = Server.MapPath("~/Upload/" + strFileName);
                    ExcelRender.RenderListToExcel(errList, "Excel批量审核结果", fullName);
                    rs.Flag = false;
                    rs.DataInfo = strFileName;
                    rs.ReMsg = $"Err|有条{errList.Count}失败";
                }
                else
                {
                    rs.Flag = true;
                    rs.DataInfo = "";
                    rs.ReMsg = "OK|成功";
                }
                
                return Json(rs);
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
            string fullName = Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(list.rows, "赔偿金申请记录", fullName);
            rs.Flag = true;
            rs.DataInfo = strFileName;
            rs.ReMsg = "OK|成功";
            return Json(rs);
            //return File(ms.ToArray(), "application/vnd.ms-excel", "赔偿金申请记录" + DateTime.Today.ToString("yyyyMMdd")+".xls");

        }



        [HttpPost]
        public ActionResult ImportExcel()
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                //object f = Request.Form.Files["file"];
                HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase; ;
                if (file == null || file.ContentLength == 0)
                {
                    return Json(new { success = false, message = "未选择文件" });
                }
                //var dataList = ParseExcel(file.InputStream);

                var workbook = new XSSFWorkbook(file.InputStream);
                var worksheet = workbook.GetSheetAt(0);
                var rows = worksheet.LastRowNum; // 跳过标题行

                var dataList = new List<T_PCJ_RequestRecord>();

                var errList = new List<Pcj_ErrModel>();

                var areas = _bll.QueryList<T_AREA>("select * from t_area");
                for (int i = 1; i <= rows; i++)
                {
                    NPOI.SS.UserModel.IRow row = worksheet.GetRow(i);
                    string _errInfo = "";
                    var _baseType = _bll.GetModelFirst<T_PCJ_BaseType>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeName = row.GetCell(2).StringCellValue }));
                    if (_baseType == null)
                    {
                        _errInfo = "赔偿金消费类型不存在";

                    }
                    var _criminal = _bll.QueryModel<T_Criminal>("FCode", row.GetCell(0).ToString());
                    if (_criminal == null)
                    {
                        _errInfo = "罪犯编号不存在";
                    }
                    else
                    {
                        if (_criminal.FName != row.GetCell(1).StringCellValue)
                        {
                            _errInfo = $"罪犯姓名不正确,系统是{_criminal.FName}";
                        }
                        if (_criminal.fflag == 1)
                        {
                            _errInfo = $"罪犯已离监";
                        }
                    }

                    if (!(row.GetCell(3).NumericCellValue >= 6 && row.GetCell(3).NumericCellValue <= 60))
                    {
                        _errInfo = $"申请周期只能是半年到5年";
                    }

                    var _ls = _bll.GetModelList<T_PCJ_RequestRecord>(Newtonsoft.Json.JsonConvert.SerializeObject(new { FCrimeCode = row.GetCell(0).ToString(), IsDelete = false }));
                    if (_ls.Count > 0)
                    {
                        _errInfo = $"已经有申请记录，不能重复申请，如要调整需要删除了，再申请";
                    }

                    if (_errInfo != "")
                    {
                        errList.Add(new Pcj_ErrModel()
                        {
                            FCode = row.GetCell(0).ToString(),
                            FName = row.GetCell(1).StringCellValue,
                            TypeName = row.GetCell(2).StringCellValue,
                            UseMonths = int.Parse(row.GetCell(3).NumericCellValue.ToString()),
                            Remark = row.GetCell(4)==null?"": row.GetCell(4).ToString(),
                            ErrInfo = _errInfo
                        });
                        continue;
                    }

                    var user = new T_PCJ_RequestRecord()
                    {
                        FCrimeCode = row.GetCell(0).ToString(),
                        FCrimeName = row.GetCell(1).StringCellValue,
                        TypeName = row.GetCell(2).StringCellValue,
                        TypeId = _baseType.Id,
                        UseMonths = int.Parse(row.GetCell(3).NumericCellValue.ToString()),
                        Remark = row.GetCell(4) == null ? "" : row.GetCell(4).ToString(),
                        FAreaCode=_criminal.FAreaCode,
                        FAreaName = areas.Where(o => o.FCode == _criminal.FAreaCode).FirstOrDefault()?.FName,
                        CrtBy = base.loginUserName,//操作员
                        CreateDate = DateTime.Now,
                        IsDelete = false,
                        EndTime = DateTime.Today.AddMonths(int.Parse(row.GetCell(3).NumericCellValue.ToString()))

                    };
                    dataList.Add(user);

                    errList.Add(new Pcj_ErrModel()
                    {
                        FCode = row.GetCell(0).ToString(),
                        FName = row.GetCell(1).StringCellValue,
                        TypeName = row.GetCell(2).StringCellValue,
                        UseMonths = int.Parse(row.GetCell(3).NumericCellValue.ToString()),
                        Remark = row.GetCell(4).StringCellValue,
                        ErrInfo = "导入成功"
                    });
                }

                _bll.Insert<T_PCJ_RequestRecord>(dataList);


                string strFileName = "Excel赔偿金申请结果" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                string fullName = Server.MapPath("~/Upload/" + strFileName);
                ExcelRender.RenderListToExcel(errList, "Excel赔偿金申请结果", fullName);
                rs.Flag = true;
                rs.DataInfo = strFileName;
                rs.ReMsg = "OK|成功";
                return Json(rs);
                //return Json(new { success = true, data = errList });
            }
            catch (Exception ex)
            {
                rs.Flag = false;
                rs.ReMsg = "Err|" + ex.Message;
                return Json(rs);
                //return Json(new { success = false, message = ex.Message });
            }
        }



        #endregion =====赔偿金模块End============




        #region ========赔偿金自助申请Start================
        public ActionResult SelfServiceIndex()
        {
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("loginMode");
            ViewData["LoginMode"] = mset.MgrValue;
            ViewData["baseTypes"] = GetPcjBaseTypes();
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
            var crim = _bll.QueryModel<T_Criminal>("FCode", fcrimecode);
            if (crim == null || crim.fflag == 1)
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
            //List<T_PCJ_BaseType> list = _bll.QueryList<T_PCJ_BaseType>("select * from T_PCJ_BaseType where isDelete=@isDelete", new { isDelete =0});
            //var ls = list.Select(o => new { FCode = o.Id.ToString(), FName = o.TypeName }).ToList();

            var ls = GetPcjBaseTypes();
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(ls));
        }
        private List<T_AREA> GetPcjBaseTypes()
        {
            List<T_PCJ_BaseType> list = _bll.QueryList<T_PCJ_BaseType>("select * from T_PCJ_BaseType where isDelete=@isDelete", new { isDelete = 0 });
            var ls = list.Select(o => new T_AREA() { FCode = o.Id.ToString(), FName = o.TypeName }).ToList();
            return ls;
        }

        #region ======赔偿金扣款管理 Start============
        public ActionResult DamagesPayIndex()
        {

            return View();
        }



        /// <summary>
        /// 获取赔偿金取款记录的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetDamagesPayVcrds(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", strJsonWhere, page, rows, $" AccType=4 and CAmount>0 and flag=0");
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));

        }



        /// <summary>
        /// 获取Json赔偿金取款记录的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetDamagesPayVcrdsJson(string strJsonWhere = "", int page = 1, int rows = 10)
        {
            var list = _bll.GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", strJsonWhere, page, rows, $" AccType=4 and CAmount>0 and flag=0");

            return Json(list);
        }

        /// <summary>
        /// 根据Id获取赔偿金取款记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetDamagesVcrdRecord(int id)
        {
            var model = _bll.QueryModel<T_Vcrd>("seqno", id.ToString());
            return Json(model);
        }



        /// <summary>
        /// 保存赔偿金取款记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DamagesPayVcrdSave(T_Vcrd model)
        {

            ResultInfo rs = new ResultInfo();
            try
            {

                var criminal = _bll.QueryModel<T_Criminal>("FCode", model.FCrimeCode);


                if (criminal == null || criminal.fflag == 1)
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|用户不存在或已经离监，无法保存";
                    return Json(rs);
                }

                var _card = _bll.QueryModel<T_Criminal_card>("FCrimeCode", model.FCrimeCode);
                if (_card == null || _card.AmountD < model.CAmount)
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|用户未制卡或可用余额不足";
                    return Json(rs);
                }
                List<T_AREA> areas = new T_AREABLL().GetModelList("");
                T_Savetype _saveType = _bll.QueryList<T_Savetype>("select * from t_saveType where TypeFlag=1 and FCode=@FCode", new { FCode = model.TypeFlag }).FirstOrDefault();
                var _criminal = _bll.QueryList<T_Criminal>("select * from T_Criminal where fcode=@fcode", new { fcode = model.FCrimeCode }).FirstOrDefault();
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_Vcrd>(model.Id);
                    oldModel.TypeFlag = model.TypeFlag;
                    oldModel.DType = _saveType.fname;
                    oldModel.FCriminal = _criminal.FName;
                    oldModel.FAreaCode = _criminal.FAreaCode;
                    oldModel.FAreaName = areas.Where(o => o.FCode == _criminal.FAreaCode).FirstOrDefault()?.FName;
                    oldModel.DAmount = 0;
                    oldModel.CAmount = model.CAmount;
                    oldModel.Remark = model.Remark;
                    oldModel.CrtBy = base.loginUserName;//操作员
                    oldModel.CrtDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
                    var dist = new Dictionary<string, string>();
                    dist.Add("SEQTYPE", "INV");
                    dist.Add("YM", "1");


                    var VouNo = _bll.ExecuteProcByOutput("CREATESEQNO", dist, "SEQNO");
                    model.Vouno = "VouPcj" + VouNo;
                    model.DType = _saveType.fname;
                    //model.TypeFlag = model.TypeFlag;
                    model.FCriminal = _criminal.FName;
                    model.FAreaCode = _criminal.FAreaCode;
                    model.FAreaName = areas.Where(o => o.FCode == _criminal.FAreaCode).FirstOrDefault()?.FName;
                    model.DAmount = 0;
                    //model.CAmount = model.CAmount;
                    model.AccType = 4;
                    //model.Remark = model.Remark;
                    model.CrtBy = base.loginUserName;//操作员
                    model.CrtDate = DateTime.Now;
                    model.CheckBy = "";//base.loginUserName;
                    model.UDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);
                    model.DelBy = "";
                    model.Frealareacode = "";
                    model.FrealAreaName = "";
                    model.PType = "";
                    model.CardCode = _card.cardcodea;
                    model.SubTypeFlag = 0;
                    model.OrigId = "";
                    model.BankFlag = 0;
                    model.SendDate = new DateTime(1900, 01, 01); ;
                    model.CheckBy = "";
                    model.CheckFlag = 0;
                    model.CheckDate = new DateTime(1900, 01, 01); ;
                    model.DelDate = new DateTime(1900, 01, 01);
                    model.RcvDate = new DateTime(1900, 01, 01);
                    model.bankRcvFlag = 0;
                    model.Depositer = "";
                    model.Remark = null == model.Remark ? "" : model.Remark;
                    new T_VcrdBLL().Add(model);
                    //_bll.Insert<T_Vcrd>(model);
                    _bll.ExecuteSql("update t_Criminal_Card set AmountD=AmountD-@changeAmount where FCrimeCode=@FCrimeCode"
                        , new { changeAmount = model.CAmount, FCrimeCode = model.FCrimeCode });
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
        /// 删除赔偿金取款Vcrd记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DamagesPayVcrdDelete(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {

                if (id > 0)
                {
                    var oldModel = _bll.QueryList<T_Vcrd>("select * from t_Vcrd where seqno=@seqno", new { seqno = id }).FirstOrDefault();

                    if (oldModel == null || oldModel.Flag == 1)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|记录不存在或已经被删除了";
                        return Json(rs);
                    }

                    var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCrimeCode);

                    if (criminal == null || criminal.fflag == 1)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|用户不存在或已经离监了";
                        return Json(rs);
                    }

                    //判断用户是否具有删除的管理权限
                    var areas = _bll.QueryList<T_Czy_area>("select *from T_Czy_area where fflag=2 and fcode=@fcode and fareacode=@fareacode",
                        new
                        {
                            fcode = base.loginUserCode
                        ,
                            fareacode = criminal.FAreaCode
                        });
                    if (areas == null || areas.Count <= 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "Err|操作员没有该用户的删除管理权限";
                        return Json(rs);
                    }
                    oldModel.DelBy = base.loginUserName;//操作员
                    oldModel.DelDate = DateTime.Now;
                    oldModel.Remark = "用户手动删除扣款记录";
                    oldModel.Flag = 1;
                    //_bll.Update(oldModel);
                    var dist = new Dictionary<string, string>();
                    dist.Add("Flag", oldModel.Flag.ToString());
                    dist.Add("DelBy", oldModel.DelBy);
                    dist.Add("DelDate", oldModel.DelDate.ToString());
                    dist.Add("Remark", oldModel.Remark);


                    _bll.UpdatePartInfo<T_Vcrd>(dist, $" seqno={id}");
                    _bll.ExecuteSql("update t_Criminal_Card set AmountD=AmountD+@changeAmount where FCrimeCode=@FCrimeCode"
                        , new { changeAmount = oldModel.CAmount, FCrimeCode = oldModel.FCrimeCode });

                    rs.Flag = true;
                    rs.ReMsg = "OK|删除成功";
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
        public ActionResult DoDamagesExcelOut(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            var list = _bll.GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", strJsonWhere, 1, 10000, $" AccType=4 and CAmount>0 and Flag=0");
            if (list.rows.Count <= 0)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

            }
            var ls = list.rows.Select(o => new DamagesPayVcrdEntity()
            {
                seqno = o.seqno,
                Vouno = o.Vouno,
                FCrimeCode = o.FCrimeCode,
                FCriminal = o.FCriminal,
                CrtDate = o.CrtDate,
                DType = o.DType,
                CAmount = o.CAmount,
                Remark = o.Remark,
                BankFlag = o.BankFlag,
                Depositer = o.Depositer,
                Flag = o.Flag
            }).ToList();
            string strFileName = "赔偿金取款记录" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            string fullName = Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(ls, "赔偿金取款记录", fullName);
            rs.Flag = true;
            rs.DataInfo = strFileName;
            rs.ReMsg = "OK|成功";
            return Json(rs);
            //return File(ms.ToArray(), "application/vnd.ms-excel", "赔偿金申请记录" + DateTime.Today.ToString("yyyyMMdd")+".xls");

        }

        /// <summary>
        /// 赔偿金扣款类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSaveTypes()
        {
            List<T_Savetype> list = _bll.QueryList<T_Savetype>("select * from T_Savetype where TypeFlag=1 and AccType=4", new { isDelete = 0 });
            var ls = list.Select(o => new T_AREA() { FCode = o.fcode.ToString(), FName = o.fname }).ToList();
            return Json(ls);
        }

        #endregion ======赔偿金扣款管理 End============
    }
}