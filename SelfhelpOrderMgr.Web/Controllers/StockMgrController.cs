using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Dto;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    public class StockMgrController : LoginController
    {


        private string vsaAction = "";
        BaseDapperBLL _bll = new BaseDapperBLL();
        // GET: DamagesMgr

        int _SaleTypeId = 61;

        #region ==========出入库管理模块Start==============

        /// <summary>
        /// 库存管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int id = 1)
        {
            ViewData["id"] = id;
            ViewData["GetStokType"] = GetStokType(id);
            //_SaleTypeId = id;


            return View();
        }


        /// <summary>
        /// 获取库存主单的分页记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRecordList(StockQueryDto dto, int page = 1, int rows = 10)
        {

            //T_Stock_Search wherDto = TinyMapper.Map<T_Stock_Search>(dto);
            var list = _bll.QueryPageListByDto<T_Stock, StockQueryDto>("T_Stock", dto, page, rows,"Id desc", " StockFlag in (select FCode from T_CommonTypeTab where FType='KCLX' and FRemark in ('入库','出库') )");

            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }


        /// <summary>
        /// 根据明细主单查询明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult GetStockDetailJson( StockDetailQueryDto dto)
        {

            var list = _bll.QueryListByDto<T_StockDTL, StockDetailQueryDto>("T_StockDTL", dto);
            return new CustomJsonResult { Data=list} ;
        }

        //审核库存主单记录
        public ActionResult AuditStock(StockAuditQueryDto dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.QueryListByDto<T_Stock, StockAuditQueryDto>("T_Stock", dto).FirstOrDefault();
                if (model == null || model.CheckFlag >= 1)
                {
                    rs.ReMsg = "库存主单不存在或主单已审核过!";
                    return new CustomJsonResult { Data = rs };
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    model.CheckFlag = 1;
                    model.CheckBy = base.loginUserName;
                    model.CheckDt = DateTime.Now;
                    _bll.Update<T_Stock>(model);
                    
                    _bll.UpdatePartInfo<T_StockDTL>(new { Flag=2}, " StockId=@StockId", new { StockId = model.StockId });

                    rs.Flag = true;
                    rs.ReMsg = "审核成功!";
                    rs.DataInfo = model;
                    scope.Complete();
                    return new CustomJsonResult { Data = rs };
                }
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return new CustomJsonResult { Data = rs };
            }
            
            
        }

        //撤销审核库存主单记录
        public ActionResult UnAuditStock(StockAuditQueryDto dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.QueryListByDto<T_Stock, StockAuditQueryDto>("T_Stock", dto).FirstOrDefault();
                if (model == null || model.CheckFlag == 0)
                {
                    rs.ReMsg = "库存主单不存在；或主单未审核，不用撤审!";
                    return new CustomJsonResult { Data = rs };
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    model.CheckFlag = 0;
                    model.CheckBy = base.loginUserName;
                    model.CheckDt = DateTime.Now;
                    _bll.Update<T_Stock>(model);

                    _bll.UpdatePartInfo<T_StockDTL>(new { Flag=0}, " StockId=@StockId", new { StockId = model.StockId });

                    rs.Flag = true;
                    rs.ReMsg = "撤销审核成功!";
                    rs.DataInfo = model;
                    scope.Complete();
                    return new CustomJsonResult { Data = rs };
                }
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return new CustomJsonResult { Data = rs };
            }


        }

        //删除库存主单记录
        public ActionResult DeleteStock(StockDeleteDto dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.QueryListByDto<T_Stock, StockDeleteDto>("T_Stock", dto).FirstOrDefault();
                if (model == null || model.CheckFlag == 1)
                {
                    rs.ReMsg = "库存主单不存在；或主单已审核，不能删除!";
                    return new CustomJsonResult { Data = rs };
                }
                using (TransactionScope scope = new TransactionScope())
                {

                    _bll.Delete<T_StockDTL>("StockId", model.StockId);
                    _bll.Delete<T_Stock>(model.Id);

                    rs.Flag = true;
                    rs.ReMsg = "删除成功!";
                    rs.DataInfo = model;
                    scope.Complete();
                    return new CustomJsonResult { Data = rs };
                }
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return new CustomJsonResult { Data = rs };
            }


        }
        

        public ActionResult SaveStockInfo(StockAddPostDto dto)
        {
            ResultInfo rs = new ResultInfo();

            bool isAdd = false;
            //StockAddPostDto dto = new StockAddPostDto();
            var _stock = _bll.QueryModel<T_Stock>("StockId", dto.stock.StockId);
            var comStockType = _bll.QueryListByTableName<T_CommonTypeTab>(" FType='KCLX' and FName=@FName", new { FName = dto.stock.StockType }).FirstOrDefault();
            if (_stock == null)
            {
                isAdd = true;
                _stock = TinyMapper.Map<T_Stock>(dto.stock);
                _stock.CrtBy = base.loginUserName;
                _stock.CrtDt = DateTime.Now;
                _stock.Flag = 0;
                _stock.CheckBy = "";
                _stock.CheckFlag = 0;
                _stock.InvoiceNo = "";
                _stock.Stockflag = Convert.ToInt32(comStockType.FCode);
                _stock.InOutFlag = comStockType.FRemark == "入库" ? 1 : -1;
            }

            if (_stock.CheckFlag >= 1)
            {
                rs.Flag = false;
                rs.ReMsg = "该单据已审核，不能修改!";
                return Json(rs);
            }

            var _goods = _bll.GetModelList<T_Goods>("");

            List<T_StockDTL> _details = new List<T_StockDTL>();
            if (dto.details != null && dto.details.Count > 0)
            {
                _details = TinyMapper.Map<List<T_StockDTL>>(dto.details);
                _details.ForEach(o =>
                {
                    o.Flag = 0;
                    o.GCode = _goods.Where(o1 => o1.GTXM == o.GTXM).FirstOrDefault().GCODE;
                    o.GName = _goods.Where(o1 => o1.GTXM == o.GTXM).FirstOrDefault().GNAME;
                    o.InOutFlag = comStockType.FRemark == "入库" ? 1 : -1;
                    o.StockFlag = Convert.ToInt32(comStockType.FCode);
                    o.StockId = _stock.StockId;
                    o.WareHouseCode = "";
                });
            }

            rs= StartAddStockInfo(rs, isAdd, ref _stock, _details);

            return new CustomJsonResult { Data = rs };
        }



        private ResultInfo StartAddStockInfo(ResultInfo rs, bool isAdd, ref T_Stock _stock, List<T_StockDTL> _details)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //删除原有明细数据
                    _bll.Delete<T_StockDTL>("StockId", _stock.StockId);

                    //保存主单数据
                    if (isAdd)
                    {
                        _stock = _bll.Insert(_stock);

                    }
                    else
                    {
                        _bll.Update(_stock);
                    }

                    if (_details != null && _details.Count > 0)
                    {
                        _bll.Insert<T_StockDTL>(_details);
                    }


                    rs.Flag = true;
                    rs.ReMsg = "OK|保存成功!";
                    rs.DataInfo = new
                    {
                        stock = _stock,
                        details = _bll.QueryListByDto<T_StockDTL, StockDetailQueryDto>
                                ("T_StockDTL", new StockDetailQueryDto { StockId = _stock.StockId })
                            
                    };

                    scope.Complete();
                    return rs;
                }
                catch (Exception ex)
                {
                    rs.ReMsg = ex.Message;
                    return (rs);
                }

            }
        }

        public ActionResult GetProductList(string q)
        {
            
            var list = _bll.QueryList<T_Goods>($" GType in(select FCode from t_goodsType where SaleTypeId={_SaleTypeId})").Select(o => new { 
                GTXM= o.GTXM,
                GName=o.GNAME,
                GCode=o.GCODE,
                GDJG=o.GDJ});

            list = list.Where(o => o.GTXM.Contains(q)).ToList();
            return Json(list);
        }

        private List<T_CommonTypeTab> GetStokType(int inoutFlag = 1)
        {
            string fremark = "入库";
            if (inoutFlag == -1)
            {
                fremark = "出库";
            }
            var list = _bll.QueryListByTableName<T_CommonTypeTab>(" FType='KCLX' and FRemark=@FRemark", new { FRemark= fremark });
            return list;
            
        }







        // 这个参数名 'excelFile' 必须和前端 formData.append 的第一个参数一致
        [HttpPost]
        public ActionResult ImportExcel(StockImportExcelDto dto)
        {
            ResultInfo rs = new ResultInfo();
            // 1. 检查文件是否为空
            if (Request.Files.Count>0 && Request.Files[0].ContentLength > 0)
            {
                try
                {
                    HttpPostedFileBase excelFile = Request.Files[0];
                    bool isAdd = false;
                    //StockAddPostDto dto = new StockAddPostDto();
                    var _stock = _bll.QueryModel<T_Stock>("StockId", dto.StockId);
                    var comStockType = _bll.QueryListByTableName<T_CommonTypeTab>(" FType='KCLX' and FName=@FName", new { FName = dto.StockType }).FirstOrDefault();
                    if (_stock == null)
                    {
                        isAdd = true;
                        _stock = TinyMapper.Map<T_Stock>(dto);
                        _stock.CrtBy = base.loginUserName;
                        _stock.CrtDt = DateTime.Now;
                        _stock.Flag = 0;
                        _stock.CheckBy = "";
                        _stock.CheckFlag = 0;
                        _stock.InvoiceNo = "";
                        _stock.Stockflag = Convert.ToInt32(comStockType.FCode);
                        _stock.InOutFlag = comStockType.FRemark == "入库" ? 1 : -1;
                    }

                    if (_stock.CheckFlag >= 1)
                    {
                        rs.Flag = false;
                        rs.ReMsg = "该单据已审核，不能修改!";
                        return Json(rs);
                    }

                    var _goods = _bll.GetModelList<T_Goods>("");

                    // 2. 读取Excel文件内容
                    // 这里以 NPOI 为例，你需要根据自己的库进行调整
                    List<StockDetailAddDto> dataList = new List<StockDetailAddDto>();

                    // 使用 NPOI 读取 Excel 流
                    IWorkbook workbook;
                    if (excelFile.FileName.EndsWith(".xls"))
                    {
                        workbook = new HSSFWorkbook(excelFile.InputStream); // Excel 97-2003
                    }
                    else
                    {
                        workbook = new XSSFWorkbook(excelFile.InputStream); // Excel 2007+
                    }

                    ISheet sheet = workbook.GetSheetAt(0); // 获取第一个工作表

                    // 遍历行，解析数据 (从第二行开始，假设第一行是表头)
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            // 根据你的Excel列顺序获取数据
                            var item = new StockDetailAddDto
                            {
                                Id = 0,
                                GCode="",
                                GName = row.GetCell(0)?.ToString(),
                                GTXM = row.GetCell(1)?.ToString(),
                                GDJ = decimal.Parse(row.GetCell(2)?.ToString() ?? "0"),
                                GCount = int.Parse(row.GetCell(3)?.ToString() ?? "0"),
                                ProductDate= row.GetCell(4).DateCellValue,
                                Remark= row.GetCell(5)?.ToString() ?? ""
                            };
                            dataList.Add(item);
                        }
                    }

                    string _goodsNameErrInfo = "";
                    dataList.ForEach(o =>
                    {
                        var _tmpGName=_goods.Where(o1 => o1.GTXM == o.GTXM).FirstOrDefault()?.GNAME;
                        if (o.GName != _goods.Where(o1 => o1.GTXM == o.GTXM).FirstOrDefault()?.GNAME)
                        {
                            _goodsNameErrInfo= _goodsNameErrInfo+$"[{o.GName}]商品名称不匹配，"+ _tmpGName + "与数据库不符";
                        }
                    });

                    if (!string.IsNullOrEmpty(_goodsNameErrInfo))
                    {
                        rs.Flag = false;
                        rs.ReMsg ="Err|"+ _goodsNameErrInfo;
                        return Json(rs);
                    }


                    List<T_StockDTL> _details = new List<T_StockDTL>();
                    if (dataList != null && dataList.Count > 0)
                    {
                        _details = TinyMapper.Map<List<T_StockDTL>>(dataList);
                        _details.ForEach(o =>
                        {
                            o.Flag = 0;
                            o.GCode = _goods.Where(o1 => o1.GTXM == o.GTXM).FirstOrDefault().GCODE;
                            o.GName = _goods.Where(o1 => o1.GTXM == o.GTXM).FirstOrDefault().GNAME;
                            o.InOutFlag = comStockType.FRemark == "入库" ? 1 : -1;
                            o.StockFlag = Convert.ToInt32(comStockType.FCode);
                            o.StockId = _stock.StockId;
                            o.WareHouseCode = "";
                        });
                    }

                    rs= StartAddStockInfo(rs, isAdd, ref _stock, _details);
                    return new CustomJsonResult { Data = rs };

                }
                catch (Exception ex)
                {
                    // 记录异常日志
                    // Logger.Error(ex);
                    rs.Flag = false;
                    rs.ReMsg = "处理文件时发生错误：" + ex.Message;
                    return Json(rs);
                }
            }
            else
            {
                rs.Flag = false;
                rs.ReMsg = "未选择文件或文件为空。";
                return Json(rs);
            }
        }


        
        /// <summary>
        /// 打印库存单据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PrintStockBill(int id)
        {
            var _model = _bll.GetModel<T_Stock>(id);
            ViewBag.StockTaking = _model;
            var details = _bll.QueryListByTableName<T_StockDTL>("StockId=@StockId", new { StockId = _model.StockId });
            ViewBag.Details = details;
            return View();
        }


        #endregion =====出入库管理模块End============

        #region 库存数量管理
        public ActionResult StockQtyIndex()
        {

            var saleTypes=_bll.GetModelList<T_SHO_SaleType>("");
            var goodTypes = _bll.GetModelList<T_GoodsType>("");
            ViewBag.SaleTypes = saleTypes;
            ViewBag.GoodTypes = goodTypes;
            return View();
        }


        /// <summary>
        /// 库存数量列表查询
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetStockQtyList(StockQtyQueryDto dto, int page = 1, int rows = 10)
        {

            //T_Stock_Search wherDto = TinyMapper.Map<T_Stock_Search>(dto);
            var list = _bll.QueryPageListByDto<ViewGoodStockQty, StockQtyQueryDto>("ViewGoodStockQty", dto, page, rows, "Id desc", "");

            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取商品类型下拉列表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetGoodTypes(int id=0)
        {
            var list = new List<T_GoodsType>();
            if (id == 0)
            {
                list = _bll.GetModelList<T_GoodsType>("");
            }
            else
            {
                list = _bll.GetModelList<T_GoodsType>(Newtonsoft.Json.JsonConvert.SerializeObject(new { SaleTypeId=id }));
            }
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }


        /// <summary>
        /// 导出库存数量
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult ExcelOutStockBalance(StockQtyQueryDto dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var list = _bll.QueryPageListByDto<ViewGoodStockQty, StockQtyQueryDto>("ViewGoodStockQty", dto, 1, 1000, "Id desc", "");

                if (list.rows.Count <= 0)
                {
                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(rs));
                }
                string strFileName = "库存清单" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                string fullName = Server.MapPath("~/Upload/" + strFileName);
                ExcelRender.RenderListToExcel(list.rows, "库存清单", fullName);
                rs.Flag = true;
                rs.DataInfo = strFileName;
                rs.ReMsg = "OK|成功";
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg="处理文件时发生错误：" + ex.Message;
                return Json(rs);
            }

            

        }

        #endregion


        #region 库存盘点

        public ActionResult StockTaking()
        {
            var cangkus = _bll.GetModelList<T_CommonTypeTab>(JsonConvert.SerializeObject(new { FType = "CangK" }));
            ViewBag.Cangkus = cangkus;
            var goodTypes = _bll.GetModelList<T_GoodsType>("");            
            ViewBag.GoodTypes = goodTypes;
            return View();
        }

        /// <summary>
        /// 库存单盘点列表查询
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetStockTakingList(StockTakingQueryDto dto, int page = 1, int rows = 10)
        {
            var list = _bll.QueryPageListByDto<T_StockTaking, StockTakingQueryDto>("T_StockTaking", dto, page, rows,"Id desc", "");

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 添加库存盘点单信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult AddStockTaking(T_StockTaking dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var maxModel = _bll.QueryListByTableName<T_StockTaking>(
                    " CrtDate>=@StartDate and CrtDate<@EndDate ",
                    new
                    {
                        StartDate = DateTime.Today,
                        EndDate = DateTime.Today.AddDays(1)
                    }).OrderByDescending(o=>o.CrtDate).FirstOrDefault();

                string subNo = "0001";
                if (maxModel != null)
                {
                    subNo = (Convert.ToInt32(maxModel.StockTakingNo
                        .Replace($"PD{DateTime.Now.ToString("yyMMdd")}", "")) + 1)
                        .ToString("0000");
                }



                dto.StockTakingNo = $"PD{DateTime.Now.ToString("yyMMdd")}{subNo}";
                dto.CrtDate = DateTime.Now;
                dto.CheckFlag = 0;

                dto = _bll.Insert(dto);

                rs.Flag = true;
                rs.ReMsg = "盘点单创建成功";
                rs.DataInfo = dto;
                return new CustomJsonResult { Data = rs };
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return Json(rs);
            }            
            
        }



        /// <summary>
        /// 获取盘点商品库存信息列表查询
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetGoodStockQty(StockTakingQueryQtyDto dto, int page = 1, int rows = 10)
        {

            //T_Stock_Search wherDto = TinyMapper.Map<T_Stock_Search>(dto);
            var list = _bll.QueryPageListByDto<ViewGoodStockQty, StockTakingQueryQtyDto>("ViewGoodStockQty", dto, page, rows, "Id desc", "");

            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 添加库存盘点单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult AddStockTakingDetai(int id, List<ViewGoodStockQty> dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                //T_Stock_Search wherDto = TinyMapper.Map<T_Stock_Search>(dto);
                var model = _bll.GetModel<T_StockTaking>(id);
                if (model == null || model.CheckFlag >= 1)
                {
                    rs.ReMsg = "盘点单不存在或已审核，不能添加商品";
                    return Json(rs);
                }
                var list = TinyMapper.Map<List<T_StockTakingDetail>>(dto);
                var _pdMain = _bll.GetModel<T_StockTaking>(id);
                var oldList = _bll.GetModelList<T_StockTakingDetail>(JsonConvert.SerializeObject(new { StockTakingNo = _pdMain.StockTakingNo }));
                List<string> gtxms = new List<string>();
                if (oldList.Count > 0)
                {
                    gtxms = oldList.Select(o => o.GTXM).ToList();
                }
                //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list.rows));
                var newList = list.Where(o => !gtxms.Contains(o.GTXM)).ToList();
                newList.ForEach(o => { 
                        o.StockTakingNo = model.StockTakingNo;
                        o.WareHouseCode = model.WareHouseCode;
                });
                if (newList.Count > 0)
                {
                    _bll.Insert(newList);
                }
                oldList = _bll.GetModelList<T_StockTakingDetail>(JsonConvert.SerializeObject(new { StockTakingNo = _pdMain.StockTakingNo }));
                var rsList = oldList.Where(o => newList.Select(p => p.GTXM).ToList().Contains(o.GTXM)).ToList();

                rs.Flag = true;
                rs.ReMsg = "添加成功";
                rs.DataInfo = rsList;
                return new CustomJsonResult { Data = rs };
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err{ex.Message}";
                return Json(rs);
            }
            

            
        }

        public ActionResult GetStockTakingDetaiById(string StockTakingNo)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var list=new List<T_StockTakingDetail>();
                if (!string.IsNullOrWhiteSpace(StockTakingNo))
                {
                    list = _bll.GetModelList<T_StockTakingDetail>(JsonConvert.SerializeObject(new { StockTakingNo = StockTakingNo }));
                }

                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err{ex.Message}";
                return Json(rs);
            }



        }


        /// <summary>
        /// 保存库存盘点单信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult SaveStockTakingDetail(List<T_StockTakingDetail> dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var stock = _bll.QueryModel<T_StockTaking>("StockTakingNo", dto[0].StockTakingNo);
                if (stock.CheckFlag >= 1)
                {
                    rs.ReMsg = "盘点单已审核，不能修改";
                    return Json(rs);
                }
                dto.ForEach(o => { o.DiffCount = o.RealCount-o.Balance; });
                _bll.Update(dto);

                rs.ReMsg = $"OK|保存成功";
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err{ex.Message}";
                return Json(rs);
            }

        }

        /// <summary>
        /// 删除库存盘点单信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ActionResult DeleteStockTakingDetail(List<T_StockTakingDetail> dto)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.GetModelFirst<T_StockTaking>(JsonConvert.SerializeObject(new{ StockTakingNo= dto[0].StockTakingNo}));
                if (model.CheckFlag >= 1){
                    rs.ReMsg = "盘点单已审核，不能删除";
                    return Json(rs);
                }

                List<int> ids= dto.Select(o => o.Id).ToList();
                _bll.Delete<T_StockTakingDetail>(ids);
                var list = _bll.GetModelList<T_StockTakingDetail>(JsonConvert.SerializeObject(new { StockTakingNo = dto[0].StockTakingNo }));

                rs.Flag=true;
                rs.DataInfo = list;
                rs.ReMsg = $"OK|删除成功";
                return new CustomJsonResult { Data=rs };
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err{ex.Message}";
                return Json(rs);
            }
        }


        /// <summary>
        /// 审核库存盘点单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AuditStockTaking(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var _model = _bll.GetModel<T_StockTaking>(id);
                if (_model == null)
                {
                    rs.ReMsg = "盘点单不存在";
                    return Json(rs);
                }
                if (_model.CheckFlag >= 1)
                {
                    rs.ReMsg = "盘点单已审核，不能重复操作";
                    return Json(rs);
                }
                _model.CheckFlag = 1;
                if (_bll.Update(_model))
                {
                    rs.Flag = true;
                    rs.ReMsg = "盘点单审核成功";
                    rs.DataInfo = _model;
                }
                else
                {
                    rs.Flag = false;
                    rs.ReMsg = "审核失败";
                }
                return Json(rs);
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return Json(rs);
            }

        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UnAuditStockTaking(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.GetModel<T_StockTaking>(id);
                if (model==null || model.CheckFlag != 1)
                {
                    rs.ReMsg = "盘点单状态不正确，不能撤销审核";
                    return Json(rs);
                }
                model.CheckFlag = 0;
                _bll.Update<T_StockTaking>(model);

                rs.Flag = true;
                rs.DataInfo = null;
                rs.ReMsg = $"OK|撤销审核成功";
                return new CustomJsonResult { Data = rs };
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err{ex.Message}";
                return Json(rs);
            }
        }

        /// <summary>
        /// 生成盈亏调整
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AdjustStockTaking(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var model = _bll.GetModel<T_StockTaking>(id);
                if (model == null || model.CheckFlag != 1)
                {
                    rs.ReMsg = "盘点单状态不正确，生成盈亏调整";
                    return Json(rs);
                }
                var details = _bll.GetModelList<T_StockTakingDetail>(JsonConvert.SerializeObject(new { StockTakingNo = model.StockTakingNo }));

                var addDetails= details.Where(o => o.DiffCount > 0).ToList();
                var subDetails = details.Where(o => o.DiffCount < 0).ToList();

                var _goods=_bll.GetModelList<T_Goods>("");
                var _goodsStockMain = _bll.QueryListByTableName<T_GOODSSTOCKMAIN>("","");

                using (var ts = new TransactionScope())
                {
                    //添加盘盈入库单
                    if (addDetails.Count > 0)
                    {
                        string stockId = GetStockPanDianId();

                        var stockAdd = new T_Stock
                        {
                            StockId = stockId,
                            CrtDt = DateTime.Now,
                            InOutDate = DateTime.Now,
                            Flag = -1,
                            Stockflag = 109,
                            InOutFlag = 1,
                            StockType = "盘盈入库",
                            CrtBy = base.loginUserName,
                            CheckFlag = 1,
                            CheckBy = base.loginUserName,
                            CheckDt = DateTime.Now,
                            WareHouseCode = model.WareHouseCode,
                            Remark = "",
                            InvoiceNo = model.StockTakingNo
                        };
                        _bll.Insert<T_Stock>(stockAdd);

                        var stockAddDetails = addDetails.Select(o => new T_StockDTL
                        {
                            StockId = stockAdd.StockId,
                            GCode = o.GCode,
                            GName = o.GName,
                            GTXM = o.GTXM,
                            GCount = o.DiffCount ?? 0,
                            GDJ = _goods.Where(g => g.GTXM == o.GTXM).FirstOrDefault().GDJ,
                            Flag = 1,
                            StockFlag = 109,
                            InOutFlag = 1,
                            Remark = "",
                            ProductDate = DateTime.Today,
                            WareHouseCode = o.WareHouseCode
                        }).ToList();

                        _bll.Insert<T_StockDTL>(stockAddDetails);

                        var stockAddQtys = stockAddDetails.Select(o=>new PartChangeDto()
                        {
                            Id= _goodsStockMain.Where(g=>g.GCODE==o.GCode).FirstOrDefault().SEQNO,
                            ChangeValue=o.GCount
                        }).ToList();

                        _bll.UpdatePartValueInfo<T_GOODSSTOCKMAIN>("Balance","seqno", stockAddQtys);

                        addDetails.ForEach(o =>
                        {
                            o.StockId = stockId;
                        });
                        _bll.Update<T_StockTakingDetail>(addDetails);
                    }

                    //添加盘亏出库单
                    if (subDetails.Count > 0)
                    {
                        string stockSubId = GetStockPanDianId();

                        var stockSub = new T_Stock
                        {
                            StockId = stockSubId,
                            CrtDt = DateTime.Now,
                            InOutDate = DateTime.Now,
                            Flag = -1,
                            Stockflag = 119,
                            InOutFlag = -1,
                            StockType = "盘亏出库",
                            CrtBy = base.loginUserName,
                            CheckFlag = 1,
                            CheckBy = base.loginUserName,
                            CheckDt = DateTime.Now,
                            WareHouseCode = model.WareHouseCode,
                            Remark = "",
                            InvoiceNo = model.StockTakingNo
                        };
                        _bll.Insert<T_Stock>(stockSub);

                        var stockSubDetails = subDetails.Select(o => new T_StockDTL
                        {
                            StockId = stockSub.StockId,
                            GCode = o.GCode,
                            GName = o.GName,
                            GTXM = o.GTXM,
                            GCount = -(o.DiffCount ?? 0),
                            GDJ = _goods.Where(g => g.GTXM == o.GTXM).FirstOrDefault().GDJ,
                            Flag = 1,
                            StockFlag = 119,
                            InOutFlag = -1,
                            Remark = "",
                            ProductDate = DateTime.Today,
                            WareHouseCode = o.WareHouseCode
                        }).ToList();

                        _bll.Insert<T_StockDTL>(stockSubDetails);

                        var stockSubQtys = stockSubDetails.Select(o => new PartChangeDto()
                        {
                            Id = _goodsStockMain.Where(g => g.GCODE == o.GCode).FirstOrDefault().SEQNO,
                            ChangeValue = -o.GCount
                        }).ToList();

                        _bll.UpdatePartValueInfo<T_GOODSSTOCKMAIN>("Balance","seqno", stockSubQtys);
                        
                        //更新盘点明细的对应的库存单编号
                        subDetails.ForEach(o =>
                        {
                            o.StockId = stockSubId;
                        });
                        _bll.Update<T_StockTakingDetail>(subDetails);
                    }

                    //更新盘点单的状态
                    model.CheckFlag = 2;
                    _bll.Update<T_StockTaking>(model);

                    rs.Flag = true;
                    rs.DataInfo = model;
                    rs.ReMsg = $"OK|生成盈亏调整成功";
                    ts.Complete();
                    return new CustomJsonResult { Data = rs };
                }
            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err{ex.Message}";
                return Json(rs);
            }
        }

        /// <summary>
        /// 生成盘盈盘亏单编号
        /// </summary>
        /// <returns></returns>
        private string GetStockPanDianId()
        {
            var maxModel = _bll.QueryListByTableName<T_Stock>(
                                    " CrtDt>=@StartDate and CrtDt<@EndDate and StockId like 'SPD%'",
                                new
                                {
                                    StartDate = DateTime.Today,
                                    EndDate = DateTime.Today.AddDays(1)
                                }).OrderByDescending(o => o.StockId).FirstOrDefault();

            string subNo = "0001";
            if (maxModel != null)
            {
                subNo = (Convert.ToInt32(maxModel.StockId
                    .Replace($"SPD{DateTime.Now.ToString("yyMMdd")}", "")) + 1)
                    .ToString("0000");
            }

            string stockId = $"SPD{DateTime.Now.ToString("yyMMdd")}{subNo}";
            return stockId;
        }


        
        /// <summary>
        /// 删除盘点单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteStockTaking(int id)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var _model = _bll.GetModel<T_StockTaking>(id);
                if (_model == null)
                {
                    rs.ReMsg = "盘点单不存在";
                    return Json(rs);
                }
                if (_model.CheckFlag >= 1)
                {
                    rs.ReMsg = "盘点单已审核，不能删除";
                    return Json(rs);
                }
                using(TransactionScope ts = new TransactionScope())
                {
                    _bll.Delete<T_StockTaking>(id);
                    _bll.Delete<T_StockTakingDetail>("StockTakingNo", _model.StockTakingNo);
                    rs.Flag = true;
                    rs.ReMsg = "OK|删除成功";
                    rs.DataInfo = null;
                    ts.Complete();
                    return Json(rs);
                }

            }
            catch (Exception ex)
            {
                rs.ReMsg = $"Err|{ex.Message}";
                return Json(rs);
            }

        }

        /// <summary>
        /// 打印盘点单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PrintStockTaking(int id)
        {
            var _model = _bll.GetModel<T_StockTaking>(id);
            ViewBag.StockTaking = _model;
            var details = _bll.QueryListByTableName<T_StockTakingDetail>("StockTakingNo=@StockTakingNo",new { StockTakingNo = _model.StockTakingNo });
            ViewBag.Details = details;
            return View();
        }

        #endregion
    }
}