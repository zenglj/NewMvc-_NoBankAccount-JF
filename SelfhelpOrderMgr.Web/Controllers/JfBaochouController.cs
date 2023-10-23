using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class JfBaochouController : Controller
    {
        private string strWhere = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        private  BaochouMgrService _baochouService = new BaochouMgrService();
        // GET: JfBaochou
        public ActionResult Index(string typecode="")
        {
            ViewBag.TypeCode = typecode;
            return View();
        }

        #region 积分参数设定
        public ActionResult GetParamInfo(string typecode = "")
        {
            if (!string.IsNullOrWhiteSpace(typecode))
            {
                var whr = new { TypeCode = typecode };
                strWhere = JsonConvert.SerializeObject(whr);
            }

            List<T_BC_ManagerParam> list = _baochouService.GetModelList<T_BC_ManagerParam>(strWhere, "TypeCode asc,FCode asc", 1000);
            return Content(jss.Serialize(list));
        }


        [MyLogActionFilterAttribute]
        public ActionResult SaveParamInfoList()//保存队别列表
        {
            //Request.("UTF-8");
            //取编辑数据 这里获取到的是json字符串

            string deleted = Request["deleted"];

            string inserted = Request["inserted"];

            string updated = Request["updated"];


            if (inserted != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(inserted);
                if (ja.Count > 0)
                {
                    List<T_BC_ManagerParam> models = SetParamListInfo(ja);

                    return Content("OK|" + jss.Serialize(models));
                }
            }

            if (updated != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(updated);
                if (ja.Count > 0)
                {
                    List<T_BC_ManagerParam> models = SetParamListInfo(ja);
                    return Content("OK保存成功！");
                }
            }

            return Content("");
        }

        private List<T_BC_ManagerParam> SetParamListInfo(JArray ja)
        {
            T_BC_ManagerParam model = new T_BC_ManagerParam();
            foreach (JObject o in ja)
            {
                model = new T_BC_ManagerParam();
                model.Id = Convert.ToInt32(o["Id"].ToString());
                model.TypeCode = o["TypeCode"].ToString();
                model.TypeName = o["TypeName"].ToString();
                model.FCode = o["FCode"].ToString();
                model.FName = o["FName"].ToString();
                model.MgrValue = o["MgrValue"].ToString();
                model.RatioValue = o["RatioValue"].ToString();
                model.AreaStart = o["AreaStart"].ToString();
                model.AreaEnd = o["AreaEnd"].ToString();
                model.Remark = o["Remark"].ToString();

                T_BC_ManagerParam m = _baochouService.GetModel<T_BC_ManagerParam>(model.Id);
                if (m != null)
                {
                    bool b = _baochouService.Update(model);
                }
                else
                {
                    _baochouService.Insert(model);
                }
            }
            List<T_BC_ManagerParam> models = _baochouService.GetModelList<T_BC_ManagerParam>(strWhere, "TypeCode asc,FCode asc", 1000);
            return models;
        }


        /// <summary>
        /// 删除参数信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult DeleleParamInfo(int id)
        {
            ResultInfo result = new ResultInfo();
            if (Session["loginUserAdmin"].ToString() != "1")
            {
                result.ReMsg = "Err|您不是管理员，无权删除";
                return Json(result);
            }
            try
            {
                if (_baochouService.Delete<T_BC_ManagerParam>(id))
                {
                    result.Flag = true;
                    result.ReMsg = "OK|删除成功";
                }
                else
                {
                    result.ReMsg = "Err|删除失败";
                }
            }
            catch (Exception ex)
            {
                result.ReMsg = "Err|" + ex.Message;
            }

            return Json(result);

        }
        #endregion

        #region 中队报酬分配
        public ActionResult BaochouMgr(int id = 1)
        {
            string sql = "select TransType as fcode,TypeName as fname from [T_Bank_TransType]";
            List<T_CommonTypeTab> _ts = new CommTableInfoBLL().GetList<T_CommonTypeTab>(sql, null).ToList();

            List<T_AREA> areas = _baochouService.QueryList<T_AREA>("Select * from T_Area", null).ToList();
            ViewData["types"] = _ts;
            ViewData["id"] = id;

            ViewData["areas"] = areas;
            return View();
        }

        [HttpPost]
        public JsonResult GetSearchList(string strJsonWhere, string orderField = " id asc ", int page = 1, int rows = 10)
        {
            PageResult<T_BC_BaochouMain> rs = _baochouService.GetPageList<T_BC_BaochouMain, T_BC_BaochouMain_Search>(orderField, strJsonWhere, page, rows);
            return Json(rs);

            //return Content(jss.Serialize(rs));
        }

        [HttpPost]
        public JsonResult GetDetailList(string orderId, string orderField = " id asc ", int page = 1, int rows = 10)
        {
            string strJsonWhere = "";
            if (string.IsNullOrWhiteSpace(orderId))
            {
                strJsonWhere = JsonConvert.SerializeObject(new { OrderId = "JFN000000" });
            }
            else
            {
                strJsonWhere = JsonConvert.SerializeObject(new { OrderId = orderId });
            }
            PageResult<T_BC_BaochouDetail> rs = _baochouService.GetPageList<T_BC_BaochouDetail, T_BC_BaochouDetail>(orderField, strJsonWhere, page, rows);
            return Json(rs);

            //return Content(jss.Serialize(rs));
        }


        //导入Excel文件
        public ActionResult ImportExcelDetail(string orderId, string Remark)
        {
            List<T_AREA> areas = _baochouService.QueryList<T_AREA>("select * from t_area");
            ResultInfo result = new ResultInfo();
            string strLoginName = Session["loginUserName"].ToString();

            //分监区的主单
            List<T_BC_BaochouDetail> mainLs = new List<T_BC_BaochouDetail>();
            if (Request.Files.Count == 0)
            {
                result.ReMsg = "Err|导入失败，服务器没有接收到Excel文件";
                return Json(result);

            }

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/Upload/" + fname);
                f.SaveAs(savePath);
                //context.Response.Write("Success!");
                DateTime sdt;
                //DateTime edt;
                //TimeSpan tspan;
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    sdt = DateTime.Now;
                    //XSSFWorkbook workbook = new XSSFWorkbook(stream);
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
                    //int ErrNums = 0;
                    if (rows < 1)
                    {
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {

                        #region 标准劳酬Excel格式  ：编号0	姓名1	工作岗位2	工作等级3	完成率4	工作评议5	排名6	出勤天数7	值星天数8	值星评议9	扣罚金额10	备注11
                        for (int i = 1; i <= rows; i++)
                        {
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
                            string fcrimeCode = "";
                            if (iType == 0)
                            {
                                fcrimeCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                            }
                            else
                            {
                                fcrimeCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }
                            string fcrimeName = row.GetCell(1).StringCellValue;//姓名 列名 以下类似

                            string workPostCode = "";  //工作岗位
                            workPostCode = row.GetCell(2).StringCellValue;//姓名 列名 以下类似


                            decimal workLevel = 0;  //工作等级
                            try
                            {
                                workLevel = Convert.ToDecimal(row.GetCell(3).NumericCellValue);
                            }
                            catch
                            {

                            }



                            decimal completeRatio = 0;  //完成率
                            try
                            {
                                completeRatio = Convert.ToDecimal(row.GetCell(4).NumericCellValue);
                            }
                            catch
                            {

                            }

                            string workCommentCode = "";  //工作评议
                            try
                            {
                                workCommentCode = row.GetCell(5).StringCellValue;
                            }
                            catch
                            {

                            }

                            decimal ranking = 0;  //排名
                            try
                            {
                                ranking = Convert.ToDecimal(row.GetCell(6).NumericCellValue);
                            }
                            catch { }

                            decimal workDays = 0;  //出勤天数
                            try
                            {
                                workDays = Convert.ToDecimal(row.GetCell(7).NumericCellValue);
                            }
                            catch { }

                            decimal dutyDays = 0;  //值星天数
                            try
                            {
                                dutyDays = Convert.ToDecimal(row.GetCell(8).NumericCellValue);
                            }
                            catch { }


                            string dutyCommentsCode = "";  //值星评议
                            try
                            {
                                dutyCommentsCode = row.GetCell(9).StringCellValue;
                            }
                            catch { }

                            

                            decimal deductAmount = 0;  //扣罚金额
                            try
                            {
                                deductAmount = Convert.ToDecimal(row.GetCell(10).NumericCellValue);
                            }
                            catch { }
                            string remark = "";  //备注
                            try
                            {
                                remark = row.GetCell(11).StringCellValue;
                            }
                            catch { }


                            //编号0	姓名1	工作岗位2	工作等级3	完成率4	工作评议5	排名6	出勤天数7	
                            //值星天数8	值星评议9	扣罚金额10	备注11
                            var smodel = new T_BC_BaochouDetail()
                            {
                                OrderId = orderId,
                                FCrimeCode = fcrimeCode,
                                FCrimeName = fcrimeName,
                                WorkPostCode=workPostCode,
                                WorkLevel = workLevel == 0 ? "" : workLevel.ToString(),
                                CompleteRatio = completeRatio,
                                WorkCommentCode = workCommentCode,
                                Ranking = Convert.ToInt32(ranking),
                                WorkDays = workDays,
                                DutyDays = dutyDays,
                                DutyCommentsCode = dutyCommentsCode,
                                DeductAmount = deductAmount,
                                CreateDate = DateTime.Now,
                                Remark = remark
                            };
                            mainLs.Add(smodel);
                        }
                        #endregion


                    }
                }
            }


            string strJson = JsonConvert.SerializeObject(new { OrderId = orderId });
            T_BC_BaochouMain _main = _baochouService.QueryModel<T_BC_BaochouMain>("OrderId", orderId);

            //验证主单
            if (_main==null)
            {
                result.ReMsg = "Err|主单不存在！";
                return Json(result);
            }

            if (_main.OrderStatus >= 2)
            {
                result.ReMsg = "Err|明细已经提交不能修改！";
                return Json(result);
            }

            //计算完成率
            for (int i = 0; i < mainLs.Count; i++)
            {
                //绩效积分
                if (mainLs[i].CompleteRatio > 0)
                {
                    mainLs[i].PerformanceMark = _baochouService.GetJishuGangWeiJixiaoGongfen(mainLs[i].CompleteRatio);
                }
                //出勤积分
                if (mainLs[i].WorkDays > 0)
                {
                    mainLs[i].WorkMark = mainLs[i].WorkDays * _baochouService.chuqinRation;
                }

                //值星积分
                if (mainLs[i].DutyCommentsCode!="")
                {
                    mainLs[i].DutyDays = mainLs[i].DutyDays * Convert.ToDecimal(_baochouService.dutyComments.Where(o => o.FName.Equals(mainLs[i].DutyCommentsCode)).FirstOrDefault().MgrValue);
                }

            }

            //计算生产辅助岗位的工作绩效
            decimal jishuAvgMark = mainLs.Where(o => o.CompleteRatio > 0).Average(o => o.PerformanceMark);
            decimal shengchanFuzhuZongfen = jishuAvgMark*mainLs.Where(o => o.WorkPostCode.Equals("B")).Count()
                * Convert.ToDecimal( _baochouService.workPost.Where(o=>o.FCode.Equals("B")).FirstOrDefault().MgrValue);

            decimal yibanGangweiZongfen = jishuAvgMark * mainLs.Where(o => o.WorkPostCode.Equals("C")).Count()
                * Convert.ToDecimal(_baochouService.workPost.Where(o => o.FCode.Equals("C")).FirstOrDefault().MgrValue);

            //计算生产辅助、一般岗位总系数
            decimal shengchanRatio = 0;
            decimal yibanRatio = 0;
            
            for (int i = 0; i < mainLs.Count; i++)
            {
                if (mainLs[i].WorkPostCode == "B")
                {
                    shengchanRatio = shengchanRatio + Convert.ToDecimal(mainLs[i].WorkLevel)
                        * Convert.ToDecimal(_baochouService.workComments.Where(o => o.FName == mainLs[i].WorkCommentCode).FirstOrDefault().MgrValue);

                }
                if (mainLs[i].WorkPostCode == "C")
                {
                    yibanRatio = yibanRatio + Convert.ToDecimal(_baochouService.workComments.Where(o => o.FName == mainLs[i].WorkCommentCode).FirstOrDefault().MgrValue);
                }
            }

            //计算生产辅助、一般岗位个人的绩效积分
            for (int i = 0; i < mainLs.Count; i++)
            {
                if (mainLs[i].WorkPostCode == "B")
                {
                    mainLs[i].PerformanceMark = shengchanFuzhuZongfen/shengchanRatio * Convert.ToDecimal(  mainLs[i].WorkLevel)
                        * Convert.ToDecimal( _baochouService.workComments.Where(o=>o.FName== mainLs[i].WorkCommentCode).FirstOrDefault().MgrValue);
                }

                if (mainLs[i].WorkPostCode == "C")
                {
                    mainLs[i].PerformanceMark = yibanGangweiZongfen/ yibanRatio * Convert.ToDecimal(_baochouService.workComments.Where(o => o.FName == mainLs[i].WorkCommentCode).FirstOrDefault().MgrValue);
                }
            }

            

            //计算队别总积分
            decimal areaSumMark = mainLs.Sum(o => o.PerformanceMark + o.WorkMark + o.DutyMark);
            //计算队别平均系数
            decimal areaSumBaseAmount = _baochouService.QueryModel<T_BC_BaochouMain>("OrderId", orderId).BaseAmount;
            decimal areaAvgRatio = areaSumBaseAmount / areaSumMark;

            //计算排名
            mainLs = mainLs.OrderByDescending(o => o.PerformanceMark+o.WorkMark+o.DutyMark).ToList();

            for (int i = 0; i < mainLs.Count; i++)
            {
                mainLs[i].Ranking = i + 1;//计算排名
                mainLs[i].BaseAmount = areaAvgRatio* (mainLs[i].PerformanceMark+ mainLs[i].WorkMark+ mainLs[i].DutyMark);//计算排名
            }






            #region 写入到数据库中
            using (TransactionScope ts = new TransactionScope())
            {
                if (!_baochouService.Insert(mainLs))
                {
                    result.ReMsg = "Err|保存失败！";
                }
                else
                {
                    result.ReMsg = "OK|保存成功！";
                    result.Flag = true;
                    result.DataInfo = mainLs;
                    ts.Complete();
                }
                
                return Json(result);
            }
            #endregion

        }


        #endregion




    }
}