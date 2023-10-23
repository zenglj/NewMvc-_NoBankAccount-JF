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
    public class JfAssignMainController : Controller
    {
        private string strWhere = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        private BaochouMgrService _baochouService = new BaochouMgrService();
        
        public JfAssignMainController()
        {
            
        }
        // GET: JfAssignMain
        public ActionResult Index(int id=1)
        {
            string sql = "select TransType as fcode,TypeName as fname from [T_Bank_TransType]";
            List<T_CommonTypeTab> _ts = new CommTableInfoBLL().GetList<T_CommonTypeTab>(sql, null).ToList();

            List<T_AREA> areas = _baochouService.QueryList<T_AREA>("Select * from T_Area", null).ToList();
            ViewData["types"] = _ts;
            ViewData["id"] = id;

            ViewData["areas"] = areas;
            ViewData["Shuilv"] = _baochouService.shuilv;
            return View();
        }

        #region 监狱到分监区分配


        /// <summary>
        /// 查询监狱总单
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="orderField"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetSearchList(string strJsonWhere, string orderField = " id asc ", int page = 1, int rows = 10)
        {
            PageResult<T_BC_BaochouSum> rs = _baochouService.GetPageList<T_BC_BaochouSum, T_BC_BaochouSum_Search>(orderField, strJsonWhere, page, rows);
            return Json(rs);
        }

        public ActionResult GetDetailList(string yearMonth,int page=1,int rows=10)
        {
            string strJsonWhere = JsonConvert.SerializeObject(new { YearMonth = yearMonth });
            PageResult<T_BC_BaochouMain> rs = _baochouService.GetPageList<T_BC_BaochouMain, T_BC_BaochouMain_Search>("Id asc", strJsonWhere, page, rows);
            return Json(rs);
        }


        /// <summary>
        /// 新增监狱总单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddModel(string YearMonth,string Remark)
        {
            List<T_AREA> areas = _baochouService.QueryList<T_AREA>("select * from t_area");
            ResultInfo result = new ResultInfo();
            string strLoginName = Session["loginUserName"].ToString();

            //分监区的主单
            List<T_BC_BaochouMain> mainLs = new List<T_BC_BaochouMain>();
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

                        #region 标准劳酬Excel格式  ：编号、姓名、金额、备注
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
                            string strYM = "";
                            if (iType == 0)
                            {
                                strYM = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                            }
                            else
                            {
                                strYM = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }
                            string fareaName = row.GetCell(1).StringCellValue;//监区名称 列名 以下类似

                            decimal completeMoney = 0;  //完成金额
                            try
                            {
                                completeMoney = Convert.ToDecimal(row.GetCell(2).NumericCellValue);
                            }
                            catch
                            {

                            }

                            decimal workDay = 0;  //出勤天数
                            try
                            {
                                workDay = Convert.ToDecimal(row.GetCell(3).NumericCellValue);
                            }
                            catch
                            {

                            }

                            decimal examinePersonNum = 0;  //考核人数
                            try
                            {
                                examinePersonNum = Convert.ToDecimal(row.GetCell(4).NumericCellValue);
                            }
                            catch
                            {

                            }

                            string subRemark = "";  //开始日期
                            try
                            {
                                subRemark = Convert.ToString(row.GetCell(5).StringCellValue);
                            }
                            catch { }

                            string strOrderId = _baochouService.CreateOrderId("JFN");
                            var smodel = new T_BC_BaochouMain()
                            {
                                OrderId=strOrderId,
                                FAreaName=fareaName,
                                FAreaCode= areas.Where(o=>o.FName==fareaName).FirstOrDefault().FCode,
                                Remark =subRemark,
                                CompleteMoney=completeMoney,
                                WorkDay=workDay,
                                ExaminePersonNum=examinePersonNum,
                                YearMonth= strYM,
                                CreateDate=DateTime.Now,
                                CreateBy=strLoginName,
                                AuditBy=""
                            };
                            mainLs.Add(smodel);
                        }
                        #endregion


                    }
                }
            }


            string strJson = JsonConvert.SerializeObject(new { YearMonth = YearMonth });
            PageResult<T_BC_BaochouSum> rs = _baochouService.GetPageList<T_BC_BaochouSum, T_BC_BaochouSum_Search>("Id asc", strJson, 1, 10);
            if (rs.rows.Count > 0)
            {
                result.ReMsg = "Err|该月已经发放过了，不能重复发放！";
                return Json(result);
            }

            var query=mainLs.Where(o => o.YearMonth != YearMonth).ToList();
            if (query.Count>0)
            {
                result.ReMsg = $"Err|{query[0].FAreaName}:月份不正确，不能重复发放！";
                return Json(result);
            }


            //按总额设置排名
            mainLs=mainLs.OrderByDescending(o => o.CompleteMoney).ToList();

            for (int i = 0; i < mainLs.Count; i++)
            {
                mainLs[i].Ranking = i + 1;
            }


            T_BC_BaochouSum model = new T_BC_BaochouSum() { 
                YearMonth=YearMonth,
                Remark=Remark,
                CreateDate=DateTime.Now,
                CreateBy=strLoginName,
                CompleteMoney=mainLs.Sum(o=>o.CompleteMoney),
                WorkDay=mainLs.Sum(o=>o.WorkDay),
                ExaminePersonNum= mainLs.Sum(o => o.ExaminePersonNum)
            };





            #region 写入到数据库中
            using (TransactionScope ts = new TransactionScope())
            {
                _baochouService.Insert(mainLs);
                var m = _baochouService.Insert(model);
                ts.Complete();

                if (m == null)
                {
                    result.ReMsg = "Err|保存失败！";
                }
                else
                {
                    result.ReMsg = "OK|保存成功！";
                    result.Flag = true;
                    result.DataInfo = m;
                }
                return Json(result);
            }
            #endregion

        }


        
        public ActionResult EditModel(T_BC_BaochouSum model)
        {
            ResultInfo result = new ResultInfo();
            string strJson = JsonConvert.SerializeObject(new { YearMonth = model.YearMonth });
            T_BC_BaochouSum qmodel = _baochouService.GetModel<T_BC_BaochouSum>(model.Id);
            if (qmodel == null)
            {
                result.ReMsg = "Err|该主单不存在！";
                return Json(result);
            }
            if (!_baochouService.Update(model))
            {
                result.ReMsg = "Err|修改失败！";
            }
            else
            {
                result.ReMsg = "OK|修改成功！";
                result.Flag = true;
                result.DataInfo = model;
            }
            return Json(result);
        }
        /// <summary>
        /// 编辑监狱主单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveAssignResult(int Id, decimal BaseAmount,decimal ExtAmount)
        {
            ResultInfo result = new ResultInfo();
            T_BC_BaochouSum model = _baochouService.GetModel<T_BC_BaochouSum>(Id);
            if (model == null)
            {
                result.ReMsg = "Err|该主单不存在！";
                return Json(result);
            }
            model.BaseAmount = BaseAmount;
            model.ExtAmount = ExtAmount;
            if (!_baochouService.Update(model))
            {
                result.ReMsg = "Err|分配失败！";
            }
            else
            {

                List<T_BC_BaochouMain> ls = _baochouService.GetModelList<T_BC_BaochouMain>(JsonConvert.SerializeObject(new { YearMonth = model.YearMonth }), "Id asc", 100);
                var ctrparm = _baochouService.GetModelList<T_BC_ManagerParam>(JsonConvert.SerializeObject(new { TypeCode = "4A" }), "Id asc", 100);
                for (int i = 0; i < ls.Count; i++)
                {
                    //基本报酬
                    decimal avgValue = ls[i].CompleteMoney / ls[i].WorkDay;
                    foreach (var item in ctrparm)
                    {
                        if(avgValue>= Convert.ToDecimal(item.AreaStart) && avgValue< Convert.ToDecimal(item.AreaEnd))
                        {
                                              //完成总额           除以   税率                乘以  提成点数
                            ls[i].BaseAmount = ls[i].CompleteMoney /(1+_baochouService.shuilv) * Convert.ToDecimal(item.MgrValue)/100;
                            ls[i].TichengBilv = Convert.ToDecimal(item.MgrValue);

                        }
                    }

                    //专项报酬
                    ls[i].ExtAmount = model.ExtAmount * _baochouService.dictRanking[ls[i].Ranking] / 100;

                }
                _baochouService.Update(ls);
                decimal useBaseAmount = ls.Sum(o => o.BaseAmount);
                decimal useExtAmount = ls.Sum(o => o.ExtAmount);

                result.ReMsg = $"OK|分配成功！基本劳酬:{Math.Round( useBaseAmount,2)}，专项劳酬{useExtAmount}";
                result.Flag = true;
                result.DataInfo = model;
            }
            return Json(result);
        }




        [MyLogActionFilterAttribute]
        public ActionResult SaveAreaMainInfoList()//保存队别分配结果
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
                    List<T_BC_BaochouMain> models = SetMainListInfo(ja);

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
                    List<T_BC_BaochouMain> models = SetMainListInfo(ja);
                    return Content("OK保存成功！");
                }
            }

            return Content("");
        }

        private List<T_BC_BaochouMain> SetMainListInfo(JArray ja)
        {
            T_BC_BaochouMain model = new T_BC_BaochouMain();

            foreach (JObject o in ja)
            {
                model = new T_BC_BaochouMain();
                model.Id = Convert.ToInt32(o["Id"].ToString());
                model.TichengBilv = Convert.ToDecimal( o["TichengBilv"].ToString());
                model.CompleteMoney = Convert.ToDecimal(o["CompleteMoney"].ToString());
                model.YearMonth = o["YearMonth"].ToString();

                T_BC_BaochouMain m = _baochouService.GetModel<T_BC_BaochouMain>(model.Id);
                if (m != null)
                {
                    model.BaseAmount = model.CompleteMoney / (1 + _baochouService.shuilv) * model.TichengBilv / 100;
                    bool b = _baochouService.Update<T_BC_BaochouMain>(model,JsonConvert.SerializeObject(new { Id=model.Id, TichengBilv =model.TichengBilv,BaseAmount=model.BaseAmount}));
                }
            }
            List<T_BC_BaochouMain> list = _baochouService.GetModelList<T_BC_BaochouMain>(JsonConvert.SerializeObject(new { YearMonth=model.YearMonth}),"Id asc",1000);
            return list;
        }


        #endregion
    }
}