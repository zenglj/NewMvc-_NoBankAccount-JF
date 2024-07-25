using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [MyLogActionFilterAttribute]
    public class BaseInfoMgrController : BaseController
    {
        //基础Dapper类
        BaseDapperBLL _baseDapperBLL = new BaseDapperBLL();
        // GET: /BaseInfoMgr/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index()
        {
            return View();
        }

        #region 监区管理
        //监区管理
        public ActionResult AreaMgr()
        {
            //商品类别
            //商品类别
            List<T_GoodsType> goodtypes = _baseDapperBLL.GetModelList<T_GoodsType>("", "Id asc", 200);
            ViewData["goodtypes"] = goodtypes;

            //消费类型
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1");
            ViewData["saleTypes"] = saleTypes;

            return View();
        }
        public ActionResult GetAreaMgr()
        {
            //商品类别
            //商品类别
            List<T_AREA> areas = new T_AREABLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(areas));
        }

        public ActionResult DeleleArea()//删除队别
        {
            string strRes = "Err|删除失败";
            string strFcode = Request["FCode"];
            if (new T_AREABLL().Delete(strFcode))
            {
                strRes = "OK|删除成功";
            }
            Log4NetHelper.logger.Warn($"操作人员:{Session["loginUserName"].ToString()}|删除队别，编号：{strFcode}");

            return Content(strRes);
        }

        public ActionResult SaveAreaList()//保存队别列表
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
                    List<T_AREA> models = SetAreaInfo(ja, "Add");

                    Log4NetHelper.logger.Info($"操作人员:{Session["loginUserName"].ToString()}|新增队别，编号：{ string.Join(",", models.Select(o => o.FName).ToArray())}");

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
                    List<T_AREA> models = SetAreaInfo(ja, "Update");
                    Log4NetHelper.logger.Warn($"操作人员:{Session["loginUserName"].ToString()}|修改队别，编号：{ string.Join(",", models.Select(o => o.FName).ToArray())}");

                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_AREA> SetAreaInfo(JArray ja, string action)
        {
            T_AREA model = new T_AREA();
            foreach (JObject o in ja)
            {
                model = new T_AREA();
                model.FCode = o["FCode"].ToString();
                model.FName = o["FName"].ToString();
                model.ID = o["ID"].ToString();
                if (string.IsNullOrEmpty(o["FID"].ToString()))
                {
                    model.FID = null;
                }
                else
                {
                    model.FID = o["FID"].ToString();
                }

                model.URL = o["URL"].ToString();
                if (o["FTZSP_Money"].ToString() == "")//设定监区特种商品的金额
                {
                    model.FTZSP_Money = 0;
                }
                else
                {
                    model.FTZSP_Money = Convert.ToDecimal(o["FTZSP_Money"].ToString());
                }

                if (o["SaleCloseFlag"].ToString() == "")//设定监区许可消费开关
                {
                    model.SaleCloseFlag = 0;
                }
                else
                {
                    model.SaleCloseFlag = Convert.ToInt32(o["SaleCloseFlag"].ToString());
                }

                if (o["JiFenCloseFlag"].ToString() == "")//设定监区许可消费开关
                {
                    model.JiFenCloseFlag = 0;
                }
                else
                {
                    model.JiFenCloseFlag = Convert.ToInt32(o["JiFenCloseFlag"].ToString());
                }


                T_AREA m = new T_AREABLL().GetModel(model.FCode);
                //if (m != null)
                //{
                //    bool b = new T_AREABLL().Update(model);
                //}
                //else
                //{
                //    new T_AREABLL().Add(model);
                //}

                if (action == "Update")
                {
                    bool b = new T_AREABLL().Update(model);
                }
                else
                {
                    new T_AREABLL().Add(model);
                }
            }
            List<T_AREA> models = new T_AREABLL().GetModelList("");
            return models;
        }

        //全部开启或是关闭监区的消费功能
        public ActionResult ChangeAallSaleCloseFlag()
        {
            string strRes = "Err|更新失败";
            string flag = Request["flag"];
            if (string.IsNullOrEmpty(flag))
            {
                flag = "0";
            }
            string strSql = "update t_Area set SaleCloseFlag='" + flag + "'";
            if (new CommTableInfoBLL().ExecSql(strSql) > 0)
            {
                strRes = "OK|更新成功";
            }

            return Content(strRes);
        }

        #endregion

        #region 供应商管理
        //供应商管理
        public ActionResult SupplyerMgr()
        {
            return View();
        }

        //获取供应商信息
        public ActionResult GetSupplyerMgr()
        {
            List<T_Supplyer> supplyers = new T_SupplyerBLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(supplyers));
        }

        public ActionResult DeleleSupplyer()//删除供应商
        {
            string strRes = "Err|删除失败";
            string strFcode = Request["FCode"];
            if (new T_SupplyerBLL().Delete(strFcode))
            {
                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }


        public ActionResult SaveSupplyerList()//保存队别列表
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
                    List<T_Supplyer> models = SetSupplyerInfo(ja);

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
                    List<T_Supplyer> models = SetSupplyerInfo(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_Supplyer> SetSupplyerInfo(JArray ja)
        {
            T_Supplyer model = new T_Supplyer();
            foreach (JObject o in ja)
            {
                model = new T_Supplyer();
                model.scode = o["scode"].ToString();
                model.sName = o["sName"].ToString();
                model.sAddress = o["sAddress"].ToString();
                model.sTel = o["sTel"].ToString();
                model.sFax = o["sFax"].ToString();
                model.sAtten = o["sAtten"].ToString();
                model.sAccountNo = o["sAccountNo"].ToString();
                model.sBank = o["sBank"].ToString();

                T_Supplyer m = new T_SupplyerBLL().GetModel(model.scode);
                if (m != null)
                {
                    bool b = new T_SupplyerBLL().Update(model);
                }
                else
                {
                    new T_SupplyerBLL().Add(model);
                }
            }
            List<T_Supplyer> models = new T_SupplyerBLL().GetModelList("");
            return models;
        }
        #endregion

        #region 系统参数管理
        //====================系统参数管理=========================
        //系统参数管理
        public ActionResult ManagerSetMgr()
        {
            return View();
        }

        //获取系统参数信息
        public ActionResult GetManagerSetMgr()
        {
            List<T_SHO_ManagerSet> supplyers = new T_SHO_ManagerSetBLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            foreach (T_SHO_ManagerSet supplyer in supplyers)
            {
                supplyer.StrDateTime = supplyer.StartTime.ToString();
            }


            return Content(jss.Serialize(supplyers));
        }

        public ActionResult DeleleManagerSet()//删除参数
        {
            string strRes = "Err|删除失败";
            string strFcode = Request["KeyName"];
            if (new T_SupplyerBLL().Delete(strFcode))
            {
                Log4NetHelper.logger.Warn($"操作人员:{Session["loginUserName"].ToString()}|删除系统参数，KeyName：{strFcode}");

                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }


        public ActionResult SaveManagerSetList()//保存队别列表
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
                    List<T_SHO_ManagerSet> models = SetManagerSetInfo(ja);

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
                    List<T_SHO_ManagerSet> models = SetManagerSetInfo(ja);
                    Log4NetHelper.logger.Warn($"操作人员:{Session["loginUserName"].ToString()}|修改系统参数，KeyName：{string.Join(",", models.Select(o => o.KeyName).ToArray())}");

                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_SHO_ManagerSet> SetManagerSetInfo(JArray ja)
        {
            T_SHO_ManagerSet model = new T_SHO_ManagerSet();
            foreach (JObject o in ja)
            {
                model = new T_SHO_ManagerSet();
                model.KeyName = o["KeyName"].ToString();
                model.KeyMode = Convert.ToInt32(o["KeyMode"].ToString());
                model.MgrName = o["MgrName"].ToString();
                model.MgrValue = o["MgrValue"].ToString();
                if (!string.IsNullOrEmpty(o["StrDateTime"].ToString()))
                {
                    model.StartTime = Convert.ToDateTime(o["StrDateTime"].ToString());
                }
                else
                {
                    model.StartTime = DateTime.Today;
                }
                model.Remark = o["Remark"].ToString();

                if (model.KeyName == "InterestRate")
                {
                    if (Convert.ToDouble(model.MgrValue) > 0.003)
                    {
                        throw new Exception("银行活动利息不能超过千分之三(0.003)");
                    }
                }

                T_SHO_ManagerSet m = new T_SHO_ManagerSetBLL().GetModel(model.KeyName);
                if (m != null)
                {
                    bool b = new T_SHO_ManagerSetBLL().Update(model);
                }
                else
                {
                    new T_SHO_ManagerSetBLL().Add(model);
                }
            }
            List<T_SHO_ManagerSet> models = new T_SHO_ManagerSetBLL().GetModelList("");
            return models;
        }

        #endregion

        #region 存取款类型管理
        //=================存取款类型==========================
        //存取款类型管理
        public ActionResult SaveTypeMgr(int id = 0)
        {
            ViewData["UseTypeId"] = id;
            return View();
        }

        //获取存取款类型
        public ActionResult GetSaveTypeMgr(int id = 0)
        {
            List<T_Savetype> supplyers = new T_SavetypeBLL().GetModelList("UseType='" + id.ToString() + "'");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();



            return Content(jss.Serialize(supplyers));
        }

        public ActionResult DeleleSaveType()//删除存取款类型
        {
            string strRes = "Err|删除失败";
            string strFcode = Request["fcode"];
            string typeflag = Request["typeflag"];
            int fcode, flag;
            try
            {
                fcode = Convert.ToInt32(strFcode);
                flag = Convert.ToInt32(typeflag);
            }
            catch
            {
                return Content("Err|您传入的参数不正确");
            }
            if (new T_SavetypeBLL().Delete(fcode, flag))
            {
                Log4NetHelper.logger.Warn($"操作人员:{Session["loginUserName"].ToString()}|删除存取款类型,fcode：{fcode}");

                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }


        public ActionResult SaveSubSaveTypeList()//保存队别列表
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
                    List<T_Savetype> models = SetSaveTypeInfo(ja);

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
                    List<T_Savetype> models = SetSaveTypeInfo(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_Savetype> SetSaveTypeInfo(JArray ja)
        {
            T_Savetype model = new T_Savetype();
            foreach (JObject o in ja)
            {
                model = new T_Savetype();
                model.fcode = Convert.ToInt32(o["fcode"].ToString());
                model.fname = o["fname"].ToString();
                model.typeflag = Convert.ToInt32(o["typeflag"].ToString());

                if (string.IsNullOrEmpty(o["PLXE_Flag"].ToString()))
                {
                    model.PLXE_Flag = 0;
                }
                else
                {
                    model.PLXE_Flag = Convert.ToInt32(o["PLXE_Flag"].ToString());
                }

                if (string.IsNullOrEmpty(o["ZZKK_Flag"].ToString()))
                {
                    model.ZZKK_Flag = 0;
                }
                else
                {
                    model.ZZKK_Flag = Convert.ToInt32(o["ZZKK_Flag"].ToString());
                }

                if (string.IsNullOrEmpty(o["AccType"].ToString()))
                {
                    model.AccType = 0;
                }
                else
                {
                    model.AccType = Convert.ToInt32(o["AccType"].ToString());
                }

                if (string.IsNullOrEmpty(o["FuShuFlag"].ToString()))
                {
                    model.FuShuFlag = 0;
                }
                else
                {
                    model.FuShuFlag = Convert.ToInt32(o["FuShuFlag"].ToString());
                }

                if (string.IsNullOrEmpty(o["UseType"].ToString()))
                {
                    model.UseType = 0;
                }
                else
                {
                    model.UseType = Convert.ToInt32(o["UseType"].ToString());
                }

                List<T_Savetype> ls = new T_SavetypeBLL().GetModelList("fcode=" + model.fcode + " and typeflag=" + model.typeflag.ToString());
                if (ls.Count > 0)
                {
                    bool b = new T_SavetypeBLL().Update(model);
                }
                else
                {
                    new T_SavetypeBLL().Add(model);
                }
            }
            //List<T_Savetype> models = new T_SavetypeBLL().GetModelList("UseType='"+ model.UseType +"'");
            List<T_Savetype> models = new BaseDapperBLL().QueryList<T_Savetype>("select * from T_Savetype where UseType=@UseType",new { UseType =model.UseType});

            return models;
        }

        #endregion



        #region 消费的起始日期管理
        //=================消费的起始管理==========================
        //消费的起始管理
        public ActionResult SaleDayListMgr()
        {
            return View();
        }

        //消费的起始管理
        public ActionResult GetSaleDayListMgr()
        {
            List<T_SHO_SaleDayList> saleDayLists = new T_SHO_SaleDayListBLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(saleDayLists));
        }

        //获取所有销售类别
        public ActionResult GetSystemSaleType()
        {
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1");

            List<comType> comtypes = new List<comType>();

            foreach (T_SHO_SaleType a in saleTypes)
            {
                comtypes.Add(
                     new comType()
                     {
                         text = a.PType,
                         value = a.Id.ToString(),
                         typeFlag = a.TypeFlagId.ToString()
                     }
                    );
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(comtypes));
        }

        //获取所有队别，用于列表选择
        public ActionResult GetAreaListInfo()
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("");

            List<comType> comtypes = new List<comType>();

            foreach (T_AREA a in areas)
            {
                comtypes.Add(
                     new comType()
                     {
                         text = a.FName,
                         value = a.FCode.ToString()
                     }
                    );
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(comtypes));
        }


        public ActionResult DeleleSaleDayList()//删除消费日期
        {
            string strRes = "Err|删除失败";
            string Seqno = Request["Seqno"];
            int seqno;
            try
            {
                seqno = Convert.ToInt32(Seqno);
            }
            catch
            {
                return Content("Err|您传入的参数不正确");
            }
            if (new T_SHO_SaleDayListBLL().Delete(seqno))
            {
                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }


        public ActionResult Save_SaleDayList()//保存队别列表
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
                    List<T_SHO_SaleDayList> models = SetSaleDayListInfo(ja);

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
                    List<T_SHO_SaleDayList> models = SetSaleDayListInfo(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_SHO_SaleDayList> SetSaleDayListInfo(JArray ja)
        {
            T_SHO_SaleDayList model = new T_SHO_SaleDayList();
            foreach (JObject o in ja)
            {
                model = new T_SHO_SaleDayList();
                model.Seqno = Convert.ToInt32(o["Seqno"].ToString());
                model.SaleTypeId = Convert.ToInt32(o["SaleTypeId"].ToString());
                model.PType = o["PType"].ToString();
                model.FAreaCode = o["FAreaCode"].ToString();
                model.StartDay = o["StartDay"].ToString();
                model.EndDay = o["EndDay"].ToString();
                if (string.IsNullOrEmpty(o["Flag"].ToString()))
                {
                    model.Flag = 0;
                }
                else
                {
                    model.Flag = Convert.ToInt32(o["Flag"].ToString());
                }
                if (string.IsNullOrEmpty(o["LevelId"].ToString()))
                {
                    model.LevelId = 0;
                }
                else
                {
                    model.LevelId = Convert.ToInt32(o["LevelId"].ToString());
                }

                model.Remark = o["Remark"].ToString();


                List<T_SHO_SaleDayList> ls = new T_SHO_SaleDayListBLL().GetModelList("Seqno=" + model.Seqno.ToString());
                if (ls.Count > 0)
                {
                    bool b = new T_SHO_SaleDayListBLL().Update(model);
                }
                else
                {
                    new T_SHO_SaleDayListBLL().Add(model);
                }
            }
            List<T_SHO_SaleDayList> models = new T_SHO_SaleDayListBLL().GetModelList("");
            return models;
        }

        #endregion


        #region 传统节假日期管理
        //=================传统节假日期管理==========================
        //传统节假日期管理
        public ActionResult JiaRiMgr()
        {
            return View();
        }

        //获取传统节假日期管理
        public ActionResult GetChinaFestivalListMgr()
        {
            List<T_CY_ChinaFestival> saleDayLists = new T_CY_ChinaFestivalBLL().GetModelList("");

            foreach (T_CY_ChinaFestival m in saleDayLists)
            {
                m.FTextDate = m.FDate.ToString();
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(saleDayLists));
        }



        public ActionResult DeleleChinaFestival()//删除假日信息
        {
            string strRes = "Err|删除失败";
            string ID = Request["id"];
            int id;
            try
            {
                id = Convert.ToInt32(ID);
            }
            catch
            {
                return Content("Err|您传入的参数不正确");
            }

            var cf = new T_CY_ChinaFestivalBLL().GetModel(id);
            if (new T_CY_ChinaFestivalBLL().Delete(id))
            {
                Log4NetHelper.logger.Warn($"操作人员:{Session["loginUserName"].ToString()}|删除假日信息,fcode：{cf.Festival_Name}");

                strRes = "OK|删除成功";
            }
            else
            {
                strRes = "Err|没有找到相应的记录";
            }

            return Content(strRes);
        }


        public ActionResult Save_ChinaFestivalList()//保存队别列表
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
                    List<T_CY_ChinaFestival> models = SetChinaFestivalInfo(ja);

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
                    List<T_CY_ChinaFestival> models = SetChinaFestivalInfo(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_CY_ChinaFestival> SetChinaFestivalInfo(JArray ja)
        {
            T_CY_ChinaFestival model = new T_CY_ChinaFestival();
            foreach (JObject o in ja)
            {
                model = new T_CY_ChinaFestival();
                if ("" != o["id"].ToString())
                {
                    model.id = Convert.ToInt32(o["id"].ToString());
                }
                model.FName = o["FName"].ToString();
                //model.FDate = Convert.ToDateTime( o["FDate"].ToString());
                model.FDate = Convert.ToDateTime(o["FTextDate"].ToString());
                model.Festival_Name = o["Festival_Name"].ToString();
                model.Remark = o["Remark"].ToString();



                List<T_CY_ChinaFestival> ls = new T_CY_ChinaFestivalBLL().GetModelList("id=" + model.id.ToString());
                if (ls.Count > 0)
                {
                    bool b = new T_CY_ChinaFestivalBLL().Update(model);
                }
                else
                {
                    new T_CY_ChinaFestivalBLL().Add(model);
                }
            }
            List<T_CY_ChinaFestival> models = new T_CY_ChinaFestivalBLL().GetModelList("");
            return models;
        }

        #endregion


    }

    public class comType
    {
        public string value { get; set; }
        public string text { get; set; }
        public string typeFlag { get; set; }
    }
}