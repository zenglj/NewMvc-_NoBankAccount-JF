using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class RetentionAmountController : BaseController
    {
        
        // GET: RetentionAmount
        public ActionResult Index(int id=0)
        {
            ViewData["id"] = id;
            var czyLs = new T_CZYBLL().GetModelList("");
            Dictionary<string, string> czys = new Dictionary<string, string>();
            foreach (var item in czyLs)
            {
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

            PageResult<T_Bank_RetentionAmount> list = _baseDapperBll.GetPageList<T_Bank_RetentionAmount, T_Bank_RetentionAmount_Search>("Id", strJsonWhere, page, rows);

            EasyUiPageResult<T_Bank_RetentionAmount> rs = new EasyUiPageResult<T_Bank_RetentionAmount>()
            {
                total = list.total,
                rows = list.rows,
                sum = list.rows.Sum(o => o.Amount)
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

            var model=new T_CriminalBLL().GetModel(fcode);
            if (model == null)
            {
                rs.ReMsg = "Err|编号不存在";
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
                T_Bank_RetentionAmount model = Newtonsoft.Json.JsonConvert.DeserializeObject<T_Bank_RetentionAmount>(strJsonWhere);
                if (model.Id == 0)
                {
                    if (!string.IsNullOrWhiteSpace(model.FCrimeCode))
                    {
                        var _crim = new T_CriminalBLL().GetModel(model.FCrimeCode);
                        if (model.FName!= _crim?.FName)
                        {
                            rs.ReMsg = "Err|用户的狱号和姓名不一致";
                            return Json(rs);
                        }
                        var _area = new T_AREABLL().GetModel(_crim.FAreaCode);
                        model.FAreaCode = _area?.FCode;
                        model.FAreaName = _area?.FName;
                    }
                    model.OrderStatus = 0;
                    model.CrtDate = DateTime.Now;
                    model.CrtBy = base.loginUserName;
                    
                    model = _baseDapperBll.Insert<T_Bank_RetentionAmount>(model);
                    rs.Flag = true;
                    rs.ReMsg = "OK|新增成功";
                    rs.DataInfo = model;
                }
                else
                {
                    T_Bank_RetentionAmount _m= _baseDapperBll.GetModel<T_Bank_RetentionAmount>(model.Id);
                    _m.ModifyDate = DateTime.Now;
                    _m.ModifyBy = base.loginUserName;
                    _m.OrderStatus = model.OrderStatus;
                    _m.ResultDesc = model.ResultDesc;
                    _m.Remark = model.Remark;
                    if (_baseDapperBll.Update<T_Bank_RetentionAmount>(_m))
                    {
                        rs.Flag = true;
                        rs.ReMsg = "OK|修改成功";
                        rs.DataInfo = _m;
                    }
                    Log4NetHelper.logger.Error("Err|修改滞留款成功|" + jss.Serialize(_m));

                }
            }
            catch (Exception e)
            {
                rs.Flag = true;
                rs.ReMsg = $"Err|{e.Message}";
                Log4NetHelper.logger.Error("Err|保存滞留款异常|" + e.Message);
            }

            return Json(rs);
        }



        public ActionResult DeleteMainRec(int id)
        {
            ResultInfo rs = new ResultInfo();
            if (id == 0)
            {
                rs.ReMsg = "Err|主单号不能为0";
                return Json(rs);
            }
            T_Bank_RetentionAmount _m = _baseDapperBll.GetModel<T_Bank_RetentionAmount>(id);

            if (_m == null)
            {
                rs.ReMsg = "Err|主单号不存在";
                return Json(rs);
            }

            if (_m.OrderStatus >= 1)
            {
                rs.ReMsg = "Err|主单号已审核过，不能删除";
                return Json(rs);
            }


            if (_baseDapperBll.Delete<T_Bank_RetentionAmount>(id))
            {
                rs.Flag = true;
                rs.DataInfo = _m;
                rs.ReMsg = "OK|删除成功";
                return Json(rs);
            }
            rs.ReMsg = "Err|删除处理失败";
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
            List<T_Bank_RetentionAmount> res = _baseDapperBll.GetModelList<T_Bank_RetentionAmount, T_Bank_RetentionAmount_Search>(strJsonWhere, "Id asc", 10000);

            //ViewData["res"] = res.Where(o => (o.TranStatus == 2 || o.TranStatus == 1)).ToList();
            ViewData["res"] = res;

            CustomTimeArea customTimeArea = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomTimeArea>(strJsonWhere);

            ViewData["customTimeArea"] = customTimeArea;

            return View();
        }


    }
}