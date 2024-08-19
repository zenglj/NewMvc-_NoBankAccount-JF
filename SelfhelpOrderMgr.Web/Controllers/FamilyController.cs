using NPOI.HSSF.UserModel;
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
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class FamilyController : BaseController
    {

        // GET: RetentionAmount
        public ActionResult Index(int id = 0)
        {
            ViewData["id"] = id;
            var czyLs = new T_CZYBLL().GetModelList("");
            Dictionary<string, string> czys = new Dictionary<string, string>();
            foreach (var item in czyLs)
            {
                Debug.WriteLine(item.FName);
                czys.Add(item.FName, item.FName);
            }
            ViewData["czys"] = czys;


            var areals = new T_AREABLL().GetModelList("");
            Dictionary<string, string> areas = new Dictionary<string, string>();
            foreach (var item in areals)
            {
                areas.Add(item.FCode, item.FName);
            }
            ViewData["areas"] = areas;

            return View();
        }

        public ActionResult GetMainDataGridList(string strJsonWhere, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { Id = 0 };
                strJsonWhere = base.jss.Serialize(sWhere);
            }

            PageResult<ViewFamilyInfo> list = _baseDapperBll.GetPageList<ViewFamilyInfo, ViewFamilyInfo_Search>("Id", strJsonWhere, page, rows, " FAreaCode in( select FAreaCode from t_czy_area where fflag=2 and fcode='"+ base.loginUserCode +"')");

            EasyUiPageResult<ViewFamilyInfo> rs = new EasyUiPageResult<ViewFamilyInfo>()
            {
                total = list.total,
                rows = list.rows,
                //sum = list.rows.Sum(o => o.Amount)
            };
            return Content(jss.Serialize(rs));
        }

        /// <summary>
        /// 获取人员信息姓名
        /// </summary>
        /// <param name="fcode"></param>
        /// <returns></returns>
        public ActionResult GetCriminalInfo(string fcode)
        {
            ResultInfo rs = new ResultInfo();
            if (string.IsNullOrWhiteSpace(fcode))
            {
                rs.ReMsg = "Err|编号不能为空";
                return Json(rs);
            }

            var model = new T_CriminalBLL().GetModel(fcode);
            if (model == null)
            {
                rs.ReMsg = "Err|编号不存在";
                return Json(rs);
            }

            var ls = _baseDapperBll.QueryList<T_Czy_area>("select *from t_czy_Area where fcode=@FCode and FAreaCode=@FAreaCode and FFlag=2", new { FCode = base.loginUserCode ,FAreaCode=model.FAreaCode});
            if (ls == null || ls.Count<=0)
            {
                rs.ReMsg = "Err|您没有该用户的队别管理权限";
                return Json(rs);
            }

            rs.Flag = true;
            rs.ReMsg = "OK|查询成功";
            rs.DataInfo = model.FName;
            return Json(rs);
        }


        /// <summary>
        /// 增加主单记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult SaveMainRec(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                rs.ReMsg = "Err|记录信息不能为空";
                return Json(rs);
            }

            try
            {
                T_Criminal_Family model = Newtonsoft.Json.JsonConvert.DeserializeObject<T_Criminal_Family>(strJsonWhere);

                if (model.FIdenNo.Length == 18)
                {
                    var errInfo = IdentityCardHelper.CheckIdenCard(model.FSex, model.FIdenNo);
                    if (errInfo != "")
                    {
                        rs.Flag = false;
                        rs.ReMsg = errInfo;
                        return Json(rs);
                    }

                }

                if (model.Id == 0)
                {
                    if (!string.IsNullOrWhiteSpace(model.FCrimeCode))
                    {
                        var _crim = new T_CriminalBLL().GetModel(model.FCrimeCode);
                        //if (model.FName != _crim?.FName)
                        //{
                        //    rs.ReMsg = "Err|用户的狱号和姓名不一致";
                        //    return Json(rs);
                        //}
                        //var _area = new T_AREABLL().GetModel(_crim.FAreaCode);
                        //model.FAreaCode = _area?.FCode;
                        //model.FAreaName = _area?.FName;
                    }
                    //model.OrderStatus = 0;
                    model.CrtDate = DateTime.Now;
                    model.CrtBy = base.loginUserName;

                    model = _baseDapperBll.Insert<T_Criminal_Family>(model);
                    rs.Flag = true;
                    rs.ReMsg = "OK|新增成功";
                    rs.DataInfo = model;
                }
                else
                {
                    T_Criminal_Family _m = _baseDapperBll.GetModel<T_Criminal_Family>(model.Id);
                    _m.ModDate = DateTime.Now;
                    _m.ModBy = base.loginUserName;
                    _m.FamilyName = model.FamilyName;
                    _m.FSex = model.FSex;
                    _m.FIdenNo = model.FIdenNo;
                    _m.Relation = model.Relation;

                    _m.PhoneNum = model.PhoneNum;
                    _m.UserAuthCode = model.UserAuthCode;
                    _m.FAddress = model.FAddress;
                    _m.BankCard = model.BankCard;
                    _m.OpeningBank = model.OpeningBank;
                    _m.Remark = model.Remark;
                    if (_baseDapperBll.Update<T_Criminal_Family>(_m))
                    {
                        rs.Flag = true;
                        rs.ReMsg = "OK|修改成功";
                        rs.DataInfo = _m;
                    }
                    Log4NetHelper.logger.Error("Err|修改成功|" + jss.Serialize(_m));

                }
            }
            catch (Exception e)
            {
                rs.Flag = true;
                rs.ReMsg = $"Err|{e.Message}";
                Log4NetHelper.logger.Error("Err|保存异常|" + e.Message);
            }

            return Json(rs);
        }


        /// <summary>
        /// 验证身份证编号是否存在
        /// </summary>
        /// <param name="fidenno"></param>
        /// <returns></returns>
        public ActionResult CheckIdennoExists(string fidenno)
        {
            ResultInfo rs = new ResultInfo();
            if (string.IsNullOrEmpty(fidenno))
            {
                rs.ReMsg = "Err|记录信息不能为空";
                return Json(rs);
            }

            try
            {
                T_Criminal_Family model = _baseDapperBll.QueryModel<T_Criminal_Family>("FIdenNo", fidenno);
                if(model==null || model.FIdenNo != fidenno)
                {
                    rs.ReMsg = "Err|身份证编号不存在";
                    return Json(rs);
                }
                else
                {
                    rs.Flag = true;
                    rs.ReMsg = "OK|身份证编号已存在";
                    return Json(rs);
                }
                
            }
            catch (Exception e)
            {
                rs.Flag = true;
                rs.ReMsg = $"Err|{e.Message}";
                Log4NetHelper.logger.Error("Err|处理异常|" + e.Message);
            }

            return Json(rs);
        }

        public ActionResult DeleteMainRec(int id)
        {
            ResultInfo rs = new ResultInfo();
            if (id == 0)
            {
                rs.ReMsg = "Err|记录不能为0";
                return Json(rs);
            }
            T_Criminal_Family _m = _baseDapperBll.GetModel<T_Criminal_Family>(id);

            if (_m == null)
            {
                rs.ReMsg = "Err|记录不存在";
                return Json(rs);
            }



            if (_baseDapperBll.Delete<T_Criminal_Family>(id))
            {
                rs.Flag = true;
                rs.DataInfo = _m;
                rs.ReMsg = "OK|删除成功";
                return Json(rs);
            }
            rs.ReMsg = "Err|删除处理失败";
            return Json(rs);
        }


        public ActionResult ExcelInport()
        {
            ResultInfo rs = new ResultInfo();
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string savePath = SavePostExcelFile(f);
                //string savePath = CommonQueryService.SavePostExcelFile(f);
                //导入耗时计算
                DateTime sdt;
                DateTime edt;
                TimeSpan tspan;
                //IWorkbook workbook = null;
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    sdt = DateTime.Now;

                    //HSSFWorkbook workbook = new HSSFWorkbook(stream);// 2003版本 
                    //XSSFWorkbook workbook = new XSSFWorkbook(stream);// 2007版本
                    IWorkbook workbook = null;
                    try
                    {
                        workbook = new XSSFWorkbook(stream); // 2007版本  
                    }
                    catch
                    {
                        workbook = new HSSFWorkbook(stream); // 2003版本  
                    }

                    //HSSFSheet sheet = workbook.GetSheetAt(0);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                    //NPOI.SS.UserModel.Sheet
                    int rows = sheet.LastRowNum;
                    int ErrNums = 0;
                    if (rows < 1)
                    {
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        DataTable dtUserAdd;
                        DataRow drTemp = null;
                        int modeId = 0;


                        List<T_Criminal_Family> fls = new List<T_Criminal_Family>();
                        List<T_Criminal_Family> errfls = new List<T_Criminal_Family>();

                        //获取用户的队别的管理权限
                        var arrAreacodes = _baseDapperBll.QueryList<T_Czy_area>("select * from T_Czy_area where fflag=2 and fcode=@FCode", new { FCode = base.loginUserCode })
                                    .Select(o=>o.fareacode).ToArray<string>();

                        //白名单数量限制
                        T_SHO_ManagerSet wfamilySet = _baseDapperBll.QueryModel<T_SHO_ManagerSet>("KeyName", "WhiteFamilyCount");

                        for (int i = 1; i <= rows; i++)
                        {
                            T_Criminal_Family _family = new T_Criminal_Family();
                            NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                            //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                            NPOI.SS.UserModel.CellType iType = 0;
                            try
                            {
                                iType = row.GetCell(0).CellType;
                            }
                            catch
                            {
                                break;
                            }
                            string FCrimeCode = "";
                            if (iType == 0)
                            {
                                FCrimeCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                            }
                            else
                            {
                                FCrimeCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }
                            FCrimeCode = FCrimeCode.Trim();
                            string FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似
                            FName = FName.Trim();
                            CellType? _celltype = row.GetCell(2)?.CellType;
                            string FIdenNo = _celltype.ToString() == "Numeric" ? row.GetCell(2)?.NumericCellValue.ToString() : row.GetCell(2)?.StringCellValue;//身份证
                            //string FIdenNo = row.GetCell(2).StringCellValue;//家属身份证号
                            string FamilyName = row.GetCell(3).StringCellValue;//家属姓名
                            FamilyName = FamilyName.Trim();
                            string FSex = "男";  //队别
                            try
                            {
                                FSex = Convert.ToString(row.GetCell(4).StringCellValue).Trim();//家属性别
                            }
                            catch { }
                            string Relation = row.GetCell(5)?.StringCellValue.Trim();//家属关系
                            _celltype = row.GetCell(6)?.CellType;
                            string PhoneNum = _celltype.ToString() == "Numeric" ? row.GetCell(6)?.NumericCellValue.ToString() : row.GetCell(6)?.StringCellValue;//手机号码
                            PhoneNum = PhoneNum?.Trim();
                            string FAddress = row.GetCell(7)?.StringCellValue;//家庭地址
                            _celltype = row.GetCell(8)?.CellType;
                            string BankCard = _celltype.ToString() == "Numeric" ? row.GetCell(8)?.NumericCellValue.ToString() : row.GetCell(8)?.StringCellValue;//银行卡号
                            string OpeningBank = row.GetCell(9)?.StringCellValue;//开户行
                            string FRemark = row.GetCell(10)?.StringCellValue; //备注

                            _family.FCrimeCode = FCrimeCode;
                            _family.FamilyName = FamilyName;
                            _family.FIdenNo = FIdenNo;
                            _family.FSex = FSex;
                            _family.Relation = Relation;
                            _family.PhoneNum = PhoneNum;
                            _family.BankCard = BankCard;
                            _family.OpeningBank = OpeningBank;
                            _family.UserAuthCode = "";
                            _family.CrtBy = loginUserName;
                            _family.CrtDate = DateTime.Now;
                            _family.Remark = FName;


                            try
                            {//如果金额有
                                if (string.IsNullOrWhiteSpace(FCrimeCode))
                                {
                                    continue;
                                }
                                var _criminal = new T_CriminalBLL().GetModel(FCrimeCode);
                                if (_criminal == null)
                                {
                                    //rs.DataInfo = $"编号:{FCrimeCode}不存在";
                                    //return Json(rs);
                                    _family.OpeningBank = FName;
                                    _family.Remark = $"编号:{FCrimeCode}不存在";
                                    errfls.Add(_family);

                                }
                                else if (!arrAreacodes.Contains(_criminal.FAreaCode))
                                {
                                    _family.Remark = $"编号:{FCrimeCode},姓名:{FName},和您没有该犯的【队别管理权限】 不一致";
                                    _family.OpeningBank = FName;
                                    errfls.Add(_family);
                                }
                                else if (_criminal != null && _criminal.FName != FName.Trim())
                                {
                                    //rs.DataInfo = $"编号:{FCrimeCode},姓名:{FName},和系统的【{_criminal.FName}】 不一致";
                                    //return Json(rs);
                                    _family.Remark = $"编号:{FCrimeCode},姓名:{FName},和系统的【{_criminal.FName}】 不一致";
                                    _family.OpeningBank = FName;
                                    errfls.Add(_family);
                                }
                                else if ((FIdenNo.Length >= 16 && FIdenNo.Length != 18) || FIdenNo.Length < 6)
                                {
                                    //rs.DataInfo = $"编号:{FCrimeCode},姓名:{FName},身份证【{FIdenNo}】 长度不正确";
                                    //return Json(rs);
                                    _family.OpeningBank = FName;
                                    _family.Remark = $"编号:{FCrimeCode},姓名:{FName},身份证【{FIdenNo}】 长度不正确";
                                    errfls.Add(_family);
                                }
                                else if (string.IsNullOrWhiteSpace(PhoneNum) == false && PhoneNum.Length != 11)
                                {
                                    //rs.DataInfo = $"编号:{FCrimeCode},姓名:{FName},手机【{PhoneNum}】 长度不正确";
                                    //return Json(rs);
                                    _family.OpeningBank = FName;
                                    _family.Remark = $"编号:{FCrimeCode},姓名:{FName},手机【{PhoneNum}】 长度不正确";
                                    errfls.Add(_family);
                                }
                                else if (FIdenNo.Length == 18 && IdentityCardHelper.CheckIdenCard(FSex, FIdenNo) != "")
                                {
                                    var errInfo = IdentityCardHelper.CheckIdenCard(FSex, FIdenNo);
                                    _family.OpeningBank = FName;
                                    _family.Remark = $"编号:{FCrimeCode},姓名:{FName},身份证【{errInfo}】 不正确";
                                    errfls.Add(_family);
                                }
                                else if (FCrimeCode != "" && (FIdenNo.Length >=8 && FIdenNo.Length <= 14 || FIdenNo.Length ==18))
                                {
                                    var userFamilies = _baseDapperBll.GetModelList<T_Criminal_Family>(
                                        Newtonsoft.Json.JsonConvert.SerializeObject( new { FCrimeCode =FCrimeCode} ));

                                    
                                    //判断是否超过白名单的限制数量
                                    if(wfamilySet!=null && wfamilySet.KeyMode == 1 && userFamilies.Count+1>Convert.ToInt32(wfamilySet.MgrValue))
                                    {
                                        _family.OpeningBank = FName;
                                        _family.Remark = $"编号:{FCrimeCode},姓名:{FName},超过系统白名单人数{wfamilySet.MgrValue}";
                                        errfls.Add(_family);
                                    }
                                    else
                                    {
                                        fls.Add(_family);
                                    }
                                    

                                    //Log4NetHelper.logger.Info("Excel导入,操作员：" + Session["loginUserName"].ToString() + ",修改一个用户信息，ID=" + FCode + ",用户名为：" + FName + ",处遇为：" + FCyName + ",队别名为：" + FAreaName + "");
                                }
                                else
                                {
                                    _family.OpeningBank = FName;
                                    _family.Remark = $"编号为空或身份证号长度不正确";
                                    errfls.Add(_family);
                                }

                            }
                            catch
                            { }


                        }
                        List<T_Criminal_Family> chongfuList = new List<T_Criminal_Family>();
                        foreach (var item in fls)
                        {
                            var _ds = fls.Where(o => o.FIdenNo == item.FIdenNo).ToList();
                            if (_ds.Count > 1)
                            {
                                chongfuList.Add(item);
                                item.OpeningBank = item.Remark;
                                item.Remark = $"编号:{item.FCrimeCode},姓名:{item.FamilyName},身份证号【{item.FIdenNo}】 身份证号重复";
                                errfls.Add(item);

                                //rs.Flag = false;
                                //rs.ReMsg = $"编号:{item.FCrimeCode},姓名:{item.FamilyName},身份证号【{item.FIdenNo}】 身份证号重复";
                                //return Json(rs);
                            }
                        }

                        foreach (var item in chongfuList)
                        {
                            fls.Remove(item);
                        }


                        //1.获取系统数据中所有白名单的记录
                        //2.筛选已经存在的记录
                        var familyAll = _baseDapperBll.GetModelList<T_Criminal_Family>("");
                        var idennoArray = familyAll.Select(o => o.FIdenNo).ToArray();
                        var idKuChongfuList=fls.Where(o => idennoArray.Contains(o.FIdenNo)).ToList();
                        

                        foreach (var item in idKuChongfuList)
                        {
                            fls.Remove(item);
                            item.OpeningBank = item.Remark;
                            item.Remark = $"编号:{item.FCrimeCode},姓名:{item.FamilyName},身份证号【{item.FIdenNo}】 身份证号已经存在，请人工审核是否许可";
                            errfls.Add(item);
                        }

                        #region 写入到数据库中


                        try
                        {
                            foreach (var item in fls)
                            {
                                var _family = _baseDapperBll.GetModelFirst<T_Criminal_Family>(Newtonsoft.Json.JsonConvert.SerializeObject(new { FCrimeCode = item.FCrimeCode, FIdenNo = item.FIdenNo }));
                                if (_family == null)
                                {
                                    _baseDapperBll.Insert(item);
                                }
                                else
                                {
                                    _family.FCrimeCode = item.FCrimeCode;
                                    _family.FamilyName = item.FamilyName;
                                    _family.FIdenNo = item.FIdenNo;
                                    _family.FSex = item.FSex;
                                    _family.Relation = item.Relation;
                                    _baseDapperBll.Update(item);
                                }
                            }
                            string errFileName = $"ErrFamily{DateTime.Now.ToString("yyyyyMMddHHmmss")}.xls";
                            string errFullFileName = Server.MapPath($"~/Upload/{errFileName}");

                            DataTable dt = new DataTable();
                            dt.Columns.Add(new DataColumn("罪犯编号", typeof(string)));
                            dt.Columns.Add(new DataColumn("罪犯姓名", typeof(string)));
                            dt.Columns.Add(new DataColumn("家属姓名", typeof(string)));
                            dt.Columns.Add(new DataColumn("身份证号", typeof(string)));
                            dt.Columns.Add(new DataColumn("性别", typeof(string)));
                            dt.Columns.Add(new DataColumn("关系", typeof(string)));
                            dt.Columns.Add(new DataColumn("手机号码", typeof(string)));
                            dt.Columns.Add(new DataColumn("错误原因", typeof(string)));


                            foreach (var item in errfls)
                            {
                                drTemp = dt.NewRow();
                                drTemp["罪犯编号"] = item.FCrimeCode;
                                drTemp["罪犯姓名"] = item.OpeningBank;//罪犯姓名
                                drTemp["家属姓名"] = item.FamilyName;
                                drTemp["身份证号"] = item.FIdenNo;
                                drTemp["性别"] = item.FSex;
                                drTemp["关系"] = item.Relation;
                                drTemp["手机号码"] = item.PhoneNum;
                                drTemp["错误原因"] = item.Remark;
                                dt.Rows.Add(drTemp);
                            }


                            if (errfls.Count > 0)
                            {
                                ExcelRender.RenderToExcel(dt, errFullFileName);
                                rs.Flag = false;
                                rs.ReMsg = $"Err|Excel导入成功,成功{fls.Count}条,失败{errfls.Count}条";
                                rs.DataInfo = errFileName;
                                return Json(rs);
                            }
                            rs.Flag = true;
                            rs.ReMsg = $"OK|Excel导入成功{fls.Count}条";
                            return Json(rs);
                        }
                        catch (Exception e)
                        {
                            rs.Flag = false;
                            rs.ReMsg = $"Err|" + e.ToString();
                            return Json(rs);
                        }

                        #endregion


                    }
                }
            }
            return Json(rs);
        }

        /// <summary>
        /// 打印明细报表
        /// </summary>
        /// <param name="strWhereJson">JSON条件</param>
        /// <returns></returns>
        public ActionResult PrintDetailReport(string strJsonWhere)
        {

            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                return Content("Err|添加不能为空");
            }
            List<ViewFamilyInfo> res = _baseDapperBll.GetModelList<ViewFamilyInfo, ViewFamilyInfo_Search>(strJsonWhere, "Id asc", 10000);

            //ViewData["res"] = res.Where(o => (o.TranStatus == 2 || o.TranStatus == 1)).ToList();
            ViewData["res"] = res;

            CustomTimeArea customTimeArea = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomTimeArea>(strJsonWhere);

            ViewData["customTimeArea"] = customTimeArea;

            return View();
        }


        /// <summary>
        /// 打印身份证记录重复的明细报表
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult PrintRepeatDetailReport(string strJsonWhere)
        {

            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                return Content("Err|添加不能为空");
            }
            List<ViewFamilyInfo> res = _baseDapperBll.GetModelList<ViewFamilyInfo, ViewFamilyInfo_Search>(strJsonWhere, "Id asc", 10000
                , @" FIdenNo in(
                    select FIdenNo from T_Criminal_Family group by FIdenNo
                    having count(1)>=2)");

            //ViewData["res"] = res.Where(o => (o.TranStatus == 2 || o.TranStatus == 1)).ToList();
            ViewData["res"] = res;

            CustomTimeArea customTimeArea = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomTimeArea>(strJsonWhere);

            ViewData["customTimeArea"] = customTimeArea;

            return View();
        }


        public ActionResult ExcelDetailReport(string strJsonWhere)
        {
            ResultInfo rs = new ResultInfo();
            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                //return Content("Err|添加不能为空");
                rs.ReMsg = "Err|添加不能为空";
                return Json(rs);
            }
            List<ViewFamilyInfo> res = _baseDapperBll.GetModelList<ViewFamilyInfo, ViewFamilyInfo_Search>(strJsonWhere, "Id asc", 10000);

            string fileName = "familyInfo" + DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";
            string strFileName = Server.MapPath("~/Upload/" + fileName);
            ExcelRender.RenderListToExcel<ViewFamilyInfo>(res, "家属白名单", strFileName);

            rs.Flag = true;
            rs.ReMsg = "OK|导出成功";
            rs.DataInfo = fileName;
            return Json(rs);

        }
    }
}