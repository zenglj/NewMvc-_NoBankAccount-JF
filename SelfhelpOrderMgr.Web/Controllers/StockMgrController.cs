using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Dto;
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
using NPOI.HSSF.UserModel;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class StockMgrController : LoginController
    {


        private string vsaAction = "";
        BaseDapperBLL _bll = new BaseDapperBLL();
        // GET: DamagesMgr

        int _SaleTypeId = 61;

        #region ==========福费缴汇款记录模块Start==============

        /// <summary>
        /// 库存管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int id = 1)
        {
            ViewData["id"] = id;
            ViewData["GetStokType"] = GetStokType(id);
            _SaleTypeId = id;


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



        #region 库存数量管理
        public ActionResult StockQtyIndex()
        {
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
        #endregion
    }
}