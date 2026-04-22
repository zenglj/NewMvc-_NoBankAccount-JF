using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class JfDengjiController : LoginController
    {
        BaseDapperBLL _bll = new BaseDapperBLL();
        // GET: DamagesMgr
        public ActionResult Index()
        {

            ViewData["levelNames"]=_bll.QueryList<T_JF_GoodsLevel>("UseType=1").OrderByDescending(o=>o.CompletionRate).ToList();

            ViewData["workTypes"] = _bll.QueryList<T_JF_DictCode>("TypeName='工作类型'").OrderBy(o => o.Id).ToList();
            ViewData["workResults"] = _bll.QueryList<T_JF_DictCode>("TypeName='评议结果'").OrderBy(o => o.Id).ToList();

            return View();
        }

        #region =======积分等级设定Start===========

        /// <summary>
        /// 获取基本类型的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetBaseTypeList(string strJsonWhere = "", int page = 1, int rows = 10)
        {
            var list = _bll.GetPageList<T_JF_DengjiType>("Id", strJsonWhere, page, rows, " isDelete=0");
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));

        }

        /// <summary>
        /// 保存基本类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BaseTypeSave(T_JF_DengjiType model)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_JF_DengjiType>(model.Id);
                    oldModel.TypeFlag = model.TypeFlag;
                    oldModel.TypeName = model.TypeName;
                    oldModel.CompletionRate = model.CompletionRate;
                    oldModel.WorkResult = model.WorkResult;
                    oldModel.LevelName = model.LevelName;
                    oldModel.JfUseMaxPoints = model.JfUseMaxPoints;
                    oldModel.MonthCount = model.MonthCount;
                    oldModel.UseType = model.UseType;
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

                    var oldModel = _bll.GetModel<T_JF_DengjiType>(id);
                    //oldModel.ModBy = base.loginUserName;//操作员
                    //oldModel.ModifyDate = DateTime.Now;
                    //oldModel.isDelete = true;
                    //_bll.Update(oldModel);
                    _bll.Delete<T_JF_DengjiType>(id);
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

        #endregion =======积分等级基本类型 End===========


        #region =======商品等级类型设定Start===========
        public ActionResult GoodLevelIndex()
        {
            return View();
        }

        

        /// <summary>
        /// 获取基本类型的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetGoodsLevelList(string strJsonWhere = "", int page = 1, int rows = 10)
        {
            var list = _bll.GetPageList<T_JF_GoodsLevel>("Id", strJsonWhere, page, rows);
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));

        }

        /// <summary>
        /// 保存基本类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GoodsLevelSave(T_JF_GoodsLevel model)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_JF_GoodsLevel>(model.Id);
                    oldModel.LevelName = model.LevelName;
                    oldModel.CompletionRate = model.CompletionRate;
                    oldModel.UseType = model.UseType;
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
        public ActionResult GoodsLevelDelete(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                if (id > 0)
                {

                    var oldModel = _bll.GetModel<T_JF_GoodsLevel>(id);
                    _bll.Delete<T_JF_GoodsLevel>(id);
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

        #endregion =======商品等级类型 End===========


        #region ==========完成率数据导入模块Start==============

        /// <summary>
        /// 完成率首页
        /// </summary>
        /// <returns></returns>
        public ActionResult RequestRecIndex(int id = 1)
        {
            ViewData["id"] = id;
            return View();
        }


        /// <summary>
        /// 获取完成率的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecordList(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_JF_Completion, T_JF_Completion_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取完成率的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecordJson(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_JF_Completion, T_JF_Completion_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            return Json(list);
        }

        /// <summary>
        /// 根据Id获取完成率记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetRequestRecords(int id)
        {
            var model = _bll.GetModel<T_JF_Completion>(id);
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
                var model = _bll.QueryList<T_JF_Completion>("select *from T_JF_Completion where FCode=@FCrimeCode and isDelete=0 and Flag<1 ", new { FCrimeCode = fcode }).FirstOrDefault();
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
        /// 保存完成率申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult RequestRecSave(T_JF_Completion model)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                List<T_AREA> areas = new T_AREABLL().GetModelList("");
                var jfDictCodes = _bll.GetModelList<T_JF_DictCode, T_JF_DictCode>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeName = "工作类型" }));
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_JF_Completion>(model.Id);
                    oldModel.FCode = model.FCode;
                    oldModel.FName = model.FName;
                    oldModel.FAreaCode = model.FAreaCode;
                    oldModel.FAreaName = areas.Where(o => o.FCode == model.FAreaCode).FirstOrDefault()?.FName; 
                    oldModel.WorkType = model.WorkType;
                    oldModel.WorkTypeName = jfDictCodes.Where(o=>o.FCode== model.WorkType).FirstOrDefault()?.FName;
                    oldModel.OutputValue = model.OutputValue;
                    oldModel.CompletionRate = model.CompletionRate;
                    oldModel.YearMonth = model.YearMonth;
                    oldModel.Remark = model.Remark;
                    oldModel.ModBy = base.loginUserName;//操作员
                    oldModel.ModifyDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
                    model.FAreaName = areas.Where(o => o.FCode == model.FAreaCode).FirstOrDefault()?.FName;
                    model.WorkTypeName = jfDictCodes.Where(o=>o.FCode== model.WorkType).FirstOrDefault()?.FName;
                    model.CrtBy = base.loginUserName;//操作员
                    model.CreateDate = DateTime.Now;
                    model.PcNo = "0";
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
        /// 删除完成率申请记录
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
                    var oldModel = _bll.GetModel<T_JF_Completion>(id);
                    var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCode);
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
                        //_bll.Update(oldModel);
                        _bll.Delete<T_JF_Completion>(oldModel.Id);

                        if (oldModel.Flag == 1)
                        {
                            criminal.CompletionRate = 0;
                            criminal.PointsDate = new DateTime(1900, 1, 1);
                            _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { CompletionRate = 0, PointsDate = DateTime.Today } ), "FCode='" + criminal.FCode + "'", false);
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
        /// 审核完成率申请记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RequestRecAudit(int id, int flag, string auditText)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
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
                var oldModel = _bll.GetModel<T_JF_Completion>(id);
                if (oldModel.Flag > 0)
                {
                    rs.Flag = false;
                    rs.ReMsg = "Err|无需重新审核";
                    return (rs);
                }
                var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCode);
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

                    var _levels= _bll.QueryList<T_JF_GoodsLevel>(" UseType=1");
                    T_JF_GoodsLevel userLevel = null;
                    if (oldModel.CompletionRate > 0)
                    {
                        userLevel=_levels.Where(x => x.CompletionRate <= oldModel.CompletionRate).OrderByDescending(o => o.CompletionRate).FirstOrDefault();
                    }
                    else
                    {
                        var _jfdj=_bll.GetModelList<T_JF_DengjiType>( Newtonsoft.Json.JsonConvert.SerializeObject( new { UseType=1 ,TypeFlag=oldModel.WorkType,WorkResult=oldModel.WorkResult })).FirstOrDefault();
                        userLevel = _levels.Where(x => x.LevelName == _jfdj.LevelName).FirstOrDefault();
                    }

                    oldModel.LevelName = userLevel.LevelName;
                    _bll.Update(oldModel);

                    if (flag == 1)
                    {
                        if (oldModel.CompletionRate > 0)
                        {
                            criminal.CompletionRate = oldModel.CompletionRate;
                        }
                        else
                        {
                            criminal.CompletionRate = userLevel.CompletionRate;
                        }                        
                        criminal.PointsDate = DateTime.Now;
                        criminal.WorkType = oldModel.WorkType;
                    }
                    else
                    {
                        criminal.CompletionRate = 0;
                        criminal.PointsDate = DateTime.Now;
                        criminal.WorkType = oldModel.WorkType;
                    }
                    _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { CompletionRate = oldModel.CompletionRate, PointsDate = DateTime.Today,WorkType= oldModel.WorkType}), "FCode='" + criminal.FCode + "'", false);


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

                var errList = new List<JF_Comploetion_ErrModel>();
                var idarry = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(ids);
                foreach (var id in idarry)
                {
                    rs = AuditRecordInfo(id, flag, auditText);
                    if (rs.Flag == false)
                    {
                        var _m = _bll.GetModel<T_JF_Completion>(id);
                        errList.Add(new JF_Comploetion_ErrModel()
                        {
                            FCode = _m.FCode,
                            FName = _m.FName,
                            FAreaName=_m.FAreaName,
                            WorkTypeName = _m.WorkTypeName,
                            OutputValue=_m.OutputValue,
                            CompletionRate = _m.CompletionRate,
                            YearMonth = _m.YearMonth,
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
            var list = _bll.GetPageList<T_JF_Completion, T_JF_Completion_Search>("Id", strJsonWhere, 1, 10000, " isDelete=0");
            if (list.rows.Count <= 0)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

            }
            string strFileName = "完成率导入记录" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            string fullName = Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(list.rows, "完成率导入记录", fullName);
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
                //完成率导入的Excel的模版
                //编号、姓名、队别、工作类型、月份、产值、完成率、备注

                IWorkbook workbook = null;
                try
                {
                    workbook = new XSSFWorkbook(file.InputStream); // 2007版本  
                }
                catch
                {
                    workbook = new HSSFWorkbook(file.InputStream); // 2003版本  
                }

                //var workbook = new XSSFWorkbook(file.InputStream);
                var worksheet = workbook.GetSheetAt(0);
                var rows = worksheet.LastRowNum; // 跳过标题行

                var dataList = new List<T_JF_Completion>();

                var errList = new List<JF_Comploetion_ErrModel>();

                string pcno="N"+DateTime.Now.ToString("yyyyMMddHHmmssf");

                var jfDictCodes = _bll.QueryList<T_JF_DictCode>("");
                var workTypes=jfDictCodes.Where(x => x.TypeName == "工作类型").ToList();
                var workResults = jfDictCodes.Where(x => x.TypeName == "评议结果").ToList();
                var areas = _bll.QueryList<T_AREA>("select * from t_area");
                for (int i = 1; i <= rows; i++)
                {
                    NPOI.SS.UserModel.IRow row = worksheet.GetRow(i);
                    string _errInfo = "";
                    var _baseType = workTypes.Where(o=>o.FName== row.GetCell(3).StringCellValue).FirstOrDefault();
                    if (_baseType == null)
                    {
                        _errInfo = "积分工作类型不存在";

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
                    //评议等级
                    if (!string.IsNullOrWhiteSpace(row.GetCell(7).StringCellValue))
                    {
                        var _workResult = workResults.Where(o =>o.FName== row.GetCell(3).StringCellValue ).FirstOrDefault();
                        if (_baseType == null)
                        {
                            _errInfo = "评议等级不存在";

                        }
                    }
                    

                    //if (!(row.GetCell(3).NumericCellValue >= 6 && row.GetCell(3).NumericCellValue <= 60))
                    //{
                    //    _errInfo = $"申请周期只能是半年到5年";
                    //}

                    var _ls = _bll.GetModelList<T_JF_Completion>(Newtonsoft.Json.JsonConvert.SerializeObject(new { FCode = row.GetCell(0).ToString(),YearMonth= row.GetCell(4).ToString(), IsDelete = false }));
                    if (_ls.Count > 0)
                    {
                        _errInfo = $"已经有申请记录，不能重复导入，如要调整需要删除了，再申请";
                    }

                    if (_errInfo != "")
                    {
                        errList.Add(new JF_Comploetion_ErrModel()
                        {
                            PcNo = pcno,
                            FCode = row.GetCell(0).ToString(),
                            FName = row.GetCell(1).StringCellValue,
                            FAreaName = row.GetCell(2).StringCellValue,
                            WorkTypeName = row.GetCell(3).StringCellValue,
                            YearMonth = row.GetCell(4).ToString(),
                            OutputValue = row.GetCell(5) == null ? 0 : decimal.Parse(row.GetCell(5).NumericCellValue.ToString()),
                            CompletionRate = row.GetCell(6) ==null ? 0 : decimal.Parse(row.GetCell(6).NumericCellValue.ToString()),
                            WorkResult = row.GetCell(7) == null ? "" : row.GetCell(7).ToString(),
                            Remark = row.GetCell(8)==null?"": row.GetCell(8).ToString(),
                            ErrInfo = _errInfo
                        });
                        continue;
                    }

                    var user = new T_JF_Completion()
                    {
                        FCode = row.GetCell(0).ToString(),
                        FName = row.GetCell(1).StringCellValue,
                        FAreaName = row.GetCell(2).StringCellValue,
                        FAreaCode = _criminal.FAreaCode,
                        WorkType= _baseType.FCode,//工作类型编码
                        WorkTypeName=row.GetCell(3).StringCellValue,
                        YearMonth = row.GetCell(4).ToString(),
                        OutputValue = row.GetCell(5)==null ?0: decimal.Parse(row.GetCell(5).NumericCellValue.ToString()),
                        CompletionRate = row.GetCell(6)==null ?0: decimal.Parse(row.GetCell(6).NumericCellValue.ToString()),
                        WorkResult = row.GetCell(7) == null ? "" : row.GetCell(7).ToString(),
                        Remark = row.GetCell(8) == null ? "" : row.GetCell(8).ToString(),
                        Flag=0,
                        CrtBy = base.loginUserName,//操作员
                        CreateDate = DateTime.Now,
                        IsDelete = false,
                        PcNo= pcno
                    };
                    dataList.Add(user);

                    errList.Add(new JF_Comploetion_ErrModel()
                    {
                        PcNo = pcno,
                        FCode = row.GetCell(0).ToString(),
                        FName = row.GetCell(1).StringCellValue,
                        FAreaName = row.GetCell(2).StringCellValue,
                        WorkTypeName = row.GetCell(3).StringCellValue,
                        YearMonth = row.GetCell(4).ToString(),
                        OutputValue = row.GetCell(5) == null ? 0 : decimal.Parse(row.GetCell(5).NumericCellValue.ToString()),
                        CompletionRate = row.GetCell(6) == null ? 0 : decimal.Parse(row.GetCell(6).NumericCellValue.ToString()),
                        WorkResult = row.GetCell(7) == null ? "" : row.GetCell(7).ToString(),
                        Remark = row.GetCell(8) == null ? "" : row.GetCell(8).ToString(),
                        ErrInfo = "导入成功"
                    });
                }

                _bll.Insert<T_JF_Completion>(dataList);


                string strFileName = "Excel完成率导入结果" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                string fullName = Server.MapPath("~/Upload/" + strFileName);
                ExcelRender.RenderListToExcel(errList, "Excel完成率导入结果", fullName);
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




        #region ==========扣分数据导入模块Start==============

        /// <summary>
        /// 扣分首页
        /// </summary>
        /// <returns></returns>
        public ActionResult KoufenRecIndex(int id = 1)
        {
            ViewData["id"] = id;
            return View();
        }


        /// <summary>
        /// 获取扣分的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetKoufenRecordList(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_JF_KouFen, T_JF_KouFen_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取完成率的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetKoufenRecordJson(string strJsonWhere = "", int page = 1, int rows = 10)
        {

            var list = _bll.GetPageList<T_JF_KouFen, T_JF_KouFen_Search>("Id", strJsonWhere, page, rows, " isDelete=0");
            return Json(list);
        }

        /// <summary>
        /// 根据Id获取扣分记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetKoufenRecords(int id)
        {
            var model = _bll.GetModel<T_JF_Completion>(id);
            return Json(model);
        }


        /// <summary>
        /// 请求罪犯本人现有的申请记录
        /// </summary>
        /// <param name="fcode"></param>
        /// <returns></returns>
        public ActionResult GetKoufenNowRecord(string fcode)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.QueryList<T_JF_KouFen>("select *from T_JF_KouFen where FCrimeCode=@FCrimeCode and isDelete=0 ", new { FCrimeCode = fcode }).FirstOrDefault();
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


        public ActionResult GetKoufenCurrMonthVaule(string fcode)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var year = DateTime.Today.AddMonths(-1).Year;
                var month = DateTime.Today.AddMonths(-1).Month;
                var score = _bll.QueryList<T_JF_KouFen>("select * from T_JF_KouFen where FCode=@FCode and CreateDate>=@CreateDate and isDelete=0 ", new { FCode = fcode , CreateDate =new DateTime(year,month,1)}).Sum(o=>o.ScoreValue);
                
                if (score != null)
                {
                    rs.Flag = true;
                    rs.ReMsg = "OK|成功";
                    rs.DataInfo = score;
                }
                else
                {
                    rs.ReMsg = "Err|没有申请记录";
                    rs.DataInfo = 0;
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
        /// 保存扣分申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult KoufenRecSave(T_JF_KouFen model)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                List<T_AREA> areas = new T_AREABLL().GetModelList("");
                var jfDictCodes = _bll.GetModelList<T_JF_DictCode, T_JF_DictCode>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeName = "工作类型" }));
                if (model.Id > 0)
                {
                    var oldModel = _bll.GetModel<T_JF_KouFen>(model.Id);
                    oldModel.FCode = model.FCode;
                    oldModel.FName = model.FName;
                    oldModel.FAreaCode = model.FAreaCode;
                    oldModel.FAreaName = areas.Where(o => o.FCode == model.FAreaCode).FirstOrDefault()?.FName;
                    oldModel.Memo = model.Memo;
                    oldModel.ScoreValue = model.ScoreValue;
                    oldModel.Remark = model.Remark;
                    oldModel.ModBy = base.loginUserName;//操作员
                    oldModel.ModifyDate = DateTime.Now;
                    _bll.Update(oldModel);
                }
                else
                {
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
        /// 删除扣分申请记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult KoufenRecDelete(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {

                if (id > 0)
                {
                    var oldModel = _bll.GetModel<T_JF_KouFen>(id);
                    var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCode);
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

                        //if (oldModel.Flag == 1)
                        //{
                        //    criminal.CompletionRate = 0;
                        //    criminal.PointsDate = new DateTime(1900, 1, 1);
                        //    _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { CompletionRate = 0, PointsDate = DateTime.Today }), "FCode='" + criminal.FCode + "'", false);
                        //}
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

        ///// <summary>
        ///// 审核完成率申请记录
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult RequestRecAudit(int id, int flag, string auditText)
        //{
        //    ResultInfo rs = new ResultInfo();
        //    try
        //    {
        //        rs = AuditRecordInfo(id, flag, auditText);
        //        return Json(rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.ReMsg = "Err|" + ex.Message;
        //        return Json(rs);
        //    }
        //}

        ///// <summary>
        ///// 审核记录
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="flag"></param>
        ///// <param name="auditText"></param>
        ///// <returns></returns>
        //private ResultInfo AuditRecordInfo(int id, int flag, string auditText)
        //{
        //    ResultInfo rs = new ResultInfo();
        //    if (id > 0)
        //    {
        //        var oldModel = _bll.GetModel<T_JF_Completion>(id);
        //        if (oldModel.Flag > 0)
        //        {
        //            rs.Flag = false;
        //            rs.ReMsg = "Err|无需重新审核";
        //            return (rs);
        //        }
        //        var criminal = _bll.QueryModel<T_Criminal>("FCode", oldModel.FCode);
        //        //判断用户是否具有删除的管理权限
        //        var areas = _bll.QueryList<T_Czy_area>("select *from T_Czy_area where fflag=2 and fcode=@fcode and fareacode=@fareacode",
        //            new
        //            {
        //                fcode = base.loginUserCode
        //            ,
        //                fareacode = criminal.FAreaCode
        //            });
        //        if (areas == null || areas.Count <= 0)
        //        {
        //            rs.Flag = false;
        //            rs.ReMsg = "Err|操作员没有该用户的管理权限";
        //            return (rs);
        //        }

        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            oldModel.ModBy = base.loginUserName;//操作员
        //            oldModel.ModifyDate = DateTime.Now;
        //            oldModel.Flag = flag;
        //            oldModel.AuditText = auditText;

        //            var _levels = _bll.QueryList<T_JF_GoodsLevel>(" UseType=1");
        //            T_JF_GoodsLevel userLevel = null;
        //            if (oldModel.CompletionRate > 0)
        //            {
        //                userLevel = _levels.Where(x => x.CompletionRate <= oldModel.CompletionRate).OrderByDescending(o => o.CompletionRate).FirstOrDefault();
        //            }
        //            else
        //            {
        //                var _jfdj = _bll.GetModelList<T_JF_DengjiType>(Newtonsoft.Json.JsonConvert.SerializeObject(new { UseType = 1, TypeFlag = oldModel.WorkType, WorkResult = oldModel.WorkResult })).FirstOrDefault();
        //                userLevel = _levels.Where(x => x.LevelName == _jfdj.LevelName).FirstOrDefault();
        //            }

        //            oldModel.LevelName = userLevel.LevelName;
        //            _bll.Update(oldModel);

        //            if (flag == 1)
        //            {
        //                if (oldModel.CompletionRate > 0)
        //                {
        //                    criminal.CompletionRate = oldModel.CompletionRate;
        //                }
        //                else
        //                {
        //                    criminal.CompletionRate = userLevel.CompletionRate;
        //                }
        //                criminal.PointsDate = DateTime.Now;
        //                criminal.WorkType = oldModel.WorkType;
        //            }
        //            else
        //            {
        //                criminal.CompletionRate = 0;
        //                criminal.PointsDate = DateTime.Now;
        //                criminal.WorkType = oldModel.WorkType;
        //            }
        //            _bll.Update(criminal, Newtonsoft.Json.JsonConvert.SerializeObject(new { CompletionRate = oldModel.CompletionRate, PointsDate = DateTime.Today, WorkType = oldModel.WorkType }), "FCode='" + criminal.FCode + "'", false);


        //            ts.Complete();
        //        }

        //        rs.Flag = true;
        //        rs.ReMsg = "OK|审核通过";
        //        return (rs);
        //    }
        //    else
        //    {
        //        rs.Flag = false;
        //        rs.ReMsg = "Err|编号必须大于0";
        //        return (rs);
        //    }
        //}
        //public ActionResult RequestRecBatchAudit(string ids, int flag, string auditText)
        //{
        //    ResultInfo rs = new ResultInfo();
        //    try
        //    {

        //        var errList = new List<JF_Comploetion_ErrModel>();
        //        var idarry = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(ids);
        //        foreach (var id in idarry)
        //        {
        //            rs = AuditRecordInfo(id, flag, auditText);
        //            if (rs.Flag == false)
        //            {
        //                var _m = _bll.GetModel<T_JF_Completion>(id);
        //                errList.Add(new JF_Comploetion_ErrModel()
        //                {
        //                    FCode = _m.FCode,
        //                    FName = _m.FName,
        //                    FAreaName = _m.FAreaName,
        //                    WorkTypeName = _m.WorkTypeName,
        //                    OutputValue = _m.OutputValue,
        //                    CompletionRate = _m.CompletionRate,
        //                    YearMonth = _m.YearMonth,
        //                    Remark = _m.Remark,
        //                    ErrInfo = rs.ReMsg
        //                });
        //            }

        //        }
        //        if (errList.Count > 0)
        //        {
        //            string strFileName = "批量审核结果" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        //            string fullName = Server.MapPath("~/Upload/" + strFileName);
        //            ExcelRender.RenderListToExcel(errList, "Excel批量审核结果", fullName);
        //            rs.Flag = false;
        //            rs.DataInfo = strFileName;
        //            rs.ReMsg = $"Err|有条{errList.Count}失败";
        //        }
        //        else
        //        {
        //            rs.Flag = true;
        //            rs.DataInfo = "";
        //            rs.ReMsg = "OK|成功";
        //        }

        //        return Json(rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.ReMsg = "Err|" + ex.Message;
        //        return Json(rs);
        //    }
        //}
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult DoKoufenExcelOut(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            var list = _bll.GetPageList<T_JF_KouFen, T_JF_KouFen_Search>("Id", strJsonWhere, 1, 10000, " isDelete=0");
            if (list.rows.Count <= 0)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));

            }
            string strFileName = "扣分导入记录" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            string fullName = Server.MapPath("~/Upload/" + strFileName);
            ExcelRender.RenderListToExcel(list.rows, "扣分导入记录", fullName);
            rs.Flag = true;
            rs.DataInfo = strFileName;
            rs.ReMsg = "OK|成功";
            return Json(rs);
            //return File(ms.ToArray(), "application/vnd.ms-excel", "赔偿金申请记录" + DateTime.Today.ToString("yyyyMMdd")+".xls");

        }



        [HttpPost]
        public ActionResult KoufenImportExcel()
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
                //完成率导入的Excel的模版
                //编号、姓名、队别、工作类型、月份、产值、完成率、备注

                IWorkbook workbook = null;
                try
                {
                    workbook = new XSSFWorkbook(file.InputStream); // 2007版本  
                }
                catch
                {
                    workbook = new HSSFWorkbook(file.InputStream); // 2003版本  
                }
                //var workbook = new XSSFWorkbook(file.InputStream);
                var worksheet = workbook.GetSheetAt(0);
                var rows = worksheet.LastRowNum; // 跳过标题行

                var dataList = new List<T_JF_KouFen>();

                var errList = new List<JF_Koufen_ErrModel>();
                string pcno = "KF" + DateTime.Now.ToString("yyyyMMddHHmmssf");

                var areas = _bll.QueryList<T_AREA>("select * from t_area");
                for (int i = 1; i <= rows; i++)
                {
                    NPOI.SS.UserModel.IRow row = worksheet.GetRow(i);
                    string _errInfo = "";
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

                    if (_errInfo != "")
                    {
                        errList.Add(new JF_Koufen_ErrModel()
                        {
                            PcNo= pcno,
                            FCode = row.GetCell(0).ToString(),
                            FName = row.GetCell(1).StringCellValue,
                            FAreaName = row.GetCell(2).StringCellValue,
                            Memo = row.GetCell(3).StringCellValue,
                            ScoreValue = row.GetCell(4)==null?0: decimal.Parse(row.GetCell(4).NumericCellValue.ToString()),
                            Remark = row.GetCell(5) == null ? "" : row.GetCell(5).ToString(),
                            ErrInfo = _errInfo
                        });
                        continue;
                    }

                    var user = new T_JF_KouFen()
                    {
                        PcNo = pcno,
                        FCode = row.GetCell(0).ToString(),
                        FName = row.GetCell(1).StringCellValue,
                        FAreaName = row.GetCell(2).StringCellValue,
                        FAreaCode = _criminal.FAreaCode,
                        Memo = row.GetCell(3).StringCellValue,
                        ScoreValue = row.GetCell(4) == null ? 0 : decimal.Parse(row.GetCell(4).NumericCellValue.ToString()),
                        Remark = row.GetCell(5) == null ? "" : row.GetCell(8).ToString(),
                        CrtBy = base.loginUserName,//操作员
                        CreateDate = DateTime.Now,
                        IsDelete = false,
                    };
                    dataList.Add(user);

                    errList.Add(new JF_Koufen_ErrModel()
                    {
                        PcNo = pcno,
                        FCode = row.GetCell(0).ToString(),
                        FName = row.GetCell(1).StringCellValue,
                        FAreaName = row.GetCell(2).StringCellValue,
                        Memo = row.GetCell(3).StringCellValue,
                        ScoreValue = row.GetCell(4)==null? 0:decimal.Parse(row.GetCell(4).NumericCellValue.ToString()),
                        Remark = row.GetCell(5) == null ? "" : row.GetCell(5).ToString(),
                        ErrInfo = "导入成功"
                    });
                }

                _bll.Insert<T_JF_KouFen>(dataList);


                string strFileName = "Excel扣分导入结果" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                string fullName = Server.MapPath("~/Upload/" + strFileName);
                ExcelRender.RenderListToExcel(errList, "Excel扣分导入结果", fullName);
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



        #endregion =====扣分数据模块End============



        #region ========赔偿金自助申请Start================
        public ActionResult SelfKoufenIndex()
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

        public ActionResult GetWorkTypes()
        {
            List<T_JF_DictCode> list = _bll.QueryList<T_JF_DictCode>("select * from T_JF_DictCode where TypeName=@TypeName", new { TypeName = "工作类型"});
            var ls = list.Select(o => new { FCode = o.Id.ToString(), FName = o.FName }).ToList();

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(ls));
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