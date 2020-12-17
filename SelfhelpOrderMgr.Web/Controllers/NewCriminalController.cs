using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class NewCriminalController : LoginController
    {
        //
        // GET: /NewCriminal/

        JavaScriptSerializer jss = new JavaScriptSerializer();

        string fczy = "Admin";


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change()
        {
            return View();
        }
        #region 调价管理
        public ActionResult ChangePriceManager()
        {
            return View();
        }
        #endregion

        #region 人员调度管理
        public ActionResult PrisonManager()
        {
            return View();
        }
        #endregion

        public ActionResult PrisonManagerList()
        {
            return View();
        }

        #region =========================队别信息管理====================================
        public ActionResult GetAreaInfoStr()
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("");
            return Content(jss.Serialize(areas));
        }

        private string GetAreaSearchCondition()
        {
            string strWhere = "";
            string loginUserCode = Session["loginUserCode"].ToString();
            string userAreaCode = "select FAreaCode From t_czy_area where fcode='" + loginUserCode + "' and fflag=2";
            strWhere = " (FCode in(" + userAreaCode + ") or FCode in( select fid from t_Area where fcode in( " + userAreaCode + ") ))";
            return strWhere;
        }
        public JsonResult GetAreaInfo()
        {
            string strWhere = GetAreaSearchCondition();
            List<T_AREA> areas = new T_AREABLL().GetModelList(strWhere);
            return Json(areas);
        }
        //保存队别信息
        public ActionResult SaveAreaInfo()
        {
            string FCode = Request["FCode"];
            string FName = Request["FName"];
            string fmeetdate = Request["fmeetdate"];
            string ID = Request["ID"];
            string FID = Request["FID"];
            string addFlag = "|Update";
            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|ID号不能为空");
            }
            if (string.IsNullOrEmpty(FName))
            {
                return Content("Err|名称不能为空");
            }
            if (string.IsNullOrEmpty(ID))
            {
                return Content("Err|ID号不能为空");
            }
            if (ID != "0")
            {
                if (string.IsNullOrEmpty(ID))
                {
                    return Content("Err|FID号（上级单位）不能为空");
                }
            }
            T_AREA model = new T_AREABLL().GetModel(FCode);
            if (model == null)
            {
                model = new T_AREA();
                model.FCode = FCode;
                model.FName = FName;
                
                model.ID = ID;
                if (FID == "")
                {
                    model.FID = null;
                }
                else
                {
                    model.FID = FID;
                }
                model.URL = "";
                new T_AREABLL().Add(model);
                addFlag = "|Add";
            }
            else
            {
                model = new T_AREA();
                model.FCode = FCode;
                model.FName = FName;
                

                model.ID = ID;
                if (FID == "")
                {
                    model.FID = null;
                }
                else
                {
                    model.FID = FID;
                }
                model.URL = "";
                new T_AREABLL().Update(model);
            }
            return Content("OK|保存成功" + addFlag);

        }

        //删除队别信息
        public ActionResult DeleteAreaInfo()
        {
            string result = "";
            string FCode = Request["FCode"];
            string FName = Request["FName"];
            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|用户ID号不能为空");
            }
            if (string.IsNullOrEmpty(FName))
            {
                return Content("Err|用户名称不能为空");
            }
            if (new T_AREABLL().Delete(FCode))
            {
                return Content("OK|删除成功" + result);
            }
            else
            {
                return Content("Err|删除失败" + result);
            }
        }
        #endregion



        #region =======================人员（犯人Criminal）管理===============================
        public JsonResult GetCriminalInfo()
        {
            List<T_Criminal> criminals = GetCriminalInfoBySearch();
            return Json(criminals);
        }

        public ActionResult GetCriminalInfoStr()
        {
            List<T_Criminal> criminals = GetCriminalInfoBySearch();
            return Content(jss.Serialize(criminals));
        }
        private List<T_Criminal> GetCriminalInfoBySearch()
        {
            string schFCode = Request["schFCode"];
            string schFName = Request["schFName"];
            string schFAreaCode = Request["schFAreaCode"];
            string schFCYCode = Request["schFCYCode"];
            string schFFlag = Request["schFFlag"];

            //int top = 500;
            string LoginUserName = new T_CZYBLL().GetModelList("FCode='" + Session["loginUserCode"].ToString() + "'")[0].FName;

            StringBuilder strWhere = new StringBuilder();
            if (string.IsNullOrEmpty(schFCode) == false)
            {
                if (strWhere.ToString() != "")
                {
                    strWhere.Append(" and FCode= '" + schFCode + "'");
                }
                else
                {
                    strWhere.Append(" FCode= '" + schFCode + "'");
                }
            }
            if (string.IsNullOrEmpty(schFName) == false)
            {
                if (strWhere.ToString() != "")
                {
                    strWhere.Append(" and FName like  '%" + schFName + "%'");
                }
                else
                {
                    strWhere.Append(" FName like  '%" + schFName + "%'");
                }
            }
            if (string.IsNullOrEmpty(schFAreaCode) == false)
            {
                T_AREA area = new T_AREABLL().GetModel(schFAreaCode);

                if (strWhere.ToString() != "")
                {
                    strWhere.Append("  and FAreaCode in (select fcode from t_Area where fcode='" + schFAreaCode + "' or fid='" + schFAreaCode + "')");
                }
                else
                {
                    strWhere.Append(" FAreaCode in (select fcode from t_Area where fcode='" + schFAreaCode + "' or fid='" + schFAreaCode + "')");
                }
            }
            if (string.IsNullOrEmpty(schFCYCode) == false)
            {
                if (strWhere.ToString() != "")
                {
                    strWhere.Append(" and FCYCode= '" + schFCYCode + "'");
                }
                else
                {
                    strWhere.Append(" FCYCode= '" + schFCYCode + "'");
                }
            }
            if (string.IsNullOrEmpty(schFFlag) == false)
            {
                if (strWhere.ToString() != "")
                {
                    strWhere.Append(" and isnull(FFlag,0) = " + schFFlag + "");
                }
                else
                {
                    strWhere.Append(" isnull(FFlag,0)= " + schFFlag + "");
                }
            }
            if (string.IsNullOrEmpty(LoginUserName) == false)
            {
                if (strWhere.ToString() != "")
                {
                    strWhere.Append(" and FAreaCode in (select fareacode from t_czy_area,t_czy where t_czy.fcode=t_czy_area.fcode  and t_czy_area.fflag='2' and t_czy.fname='" + LoginUserName + "')");
                }
                else
                {
                    strWhere.Append(" FAreaCode in (select fareacode from t_czy_area,t_czy where t_czy.fcode=t_czy_area.fcode  and t_czy_area.fflag='2' and t_czy.fname='" + LoginUserName + "')");
                }
            }

            List<T_Criminal> criminals = new T_CriminalBLL().GetModelList(strWhere.ToString());
            return criminals;
        }

        //罪名信息
        public JsonResult GetCrimeInfo()
        {
            List<T_CRIME> crimes = new T_CRIMEBLL().GetModelList("");
            return Json(crimes);
        }
        public ActionResult GetCrimeInfoStr()
        {
            List<T_CRIME> crimes = new T_CRIMEBLL().GetModelList("");
            return Content(jss.Serialize(crimes));
        }

        //保存会见类型信息
        public ActionResult SaveCriminalInfo()
        {
            string FCode = Request["FCode"];
            string FName = Request["FName"];
            string FSex = Request["FSex"];
            string FIdenNo = Request["FIdenNo"];
            string FAge = Request["FAge"];
            string FAddr = Request["FAddr"];
            string FCrimeCode = Request["FCrimeCode"];
            string FCYCode = Request["FCYCode"];
            string FTerm = Request["FTerm"];
            string FInDate = Request["FInDate"];
            string FOuDate = Request["FOuDate"];
            string FAreaCode = Request["FAreaCode"];
            string fflag = Request["fflag"];
            string FDesc = Request["FDesc"];
            string FStatus = Request["FStatus"];
            int iStatus = 0;
            if (string.IsNullOrEmpty(FStatus) == false)
            {
                iStatus = Convert.ToInt32(FStatus);
            }

            fczy = Session["loginUserName"].ToString();

            string addFlag = "|Update";
            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|ID号不能为空");
            }
            if (string.IsNullOrEmpty(FName))
            {
                return Content("Err|名称不能为空");
            }
            if (string.IsNullOrEmpty(FSex))
            {
                return Content("Err|性别不能为空");
            }
            if (string.IsNullOrEmpty(FAreaCode))
            {
                return Content("Err|所在队别不能为空");
            }
            if (string.IsNullOrEmpty(FCYCode))
            {
                return Content("Err|处遇类型不能为空");
            }

            if (string.IsNullOrEmpty(FInDate))
            {
                FInDate = "1900-01-01";
            }
            if (string.IsNullOrEmpty(FOuDate))
            {
                FOuDate = "1900-01-01";
            }
            if (string.IsNullOrEmpty(fflag))
            {
                fflag = "0";
            }


            T_Criminal model = new T_CriminalBLL().GetModel(FCode);
            if (model == null)
            {
                model = new T_Criminal()
                {
                    FCode = FCode,
                    FName = FName,
                    FSex = FSex,
                    FIdenNo = FIdenNo,
                    FAge = 0,
                    FAddr = FAddr,
                    FAreaCode = FAreaCode,
                    FCYCode = FCYCode,
                    FDesc = FDesc,
                    fflag = Convert.ToInt32(fflag),
                    FInDate = Convert.ToDateTime(FInDate),
                    FOuDate = Convert.ToDateTime(FOuDate),
                    FCrimeCode = FCrimeCode,
                    FTerm = FTerm,
                    //fczy = fczy,
                    flimitamt = 0,
                    flimitflag = 0,
                    amount = 0,
                    FAddr_tmp = "",
                    //frealareacode = "",
                    FStatus = iStatus,
                    FStatus2 = 0,
                    FSubArea = ""
                };
                new T_CriminalBLL().Add(model);
                addFlag = "|Add";
            }
            else
            {
                model = new T_Criminal()
                {
                    FCode = FCode,
                    FName = FName,
                    FSex = FSex,
                    FIdenNo = FIdenNo,
                    FAge = 0,
                    FAddr = FAddr,
                    FAreaCode = FAreaCode,
                    FCYCode = FCYCode,
                    FDesc = FDesc,
                    fflag = Convert.ToInt32(fflag),
                    FInDate = Convert.ToDateTime(FInDate),
                    FOuDate = Convert.ToDateTime(FOuDate),
                    FCrimeCode = FCrimeCode,
                    FTerm = FTerm,
                    //fczy = fczy,
                    flimitamt = 0,
                    flimitflag = 0,
                    amount = 0,
                    FAddr_tmp = "",
                    //frealareacode = "",
                    FStatus = iStatus,
                    FStatus2 = 0,
                    FSubArea = ""
                };
                new T_CriminalBLL().Update(model);
            }
            return Content("OK|保存成功" + addFlag + "|" + jss.Serialize(model));

        }

        //删除犯人信息
        public ActionResult DeleteCriminalInfo()
        {
            string result = "";
            string FCode = Request["FCode"];
            string FName = Request["FName"];
            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|用户ID号不能为空");
            }
            if (string.IsNullOrEmpty(FName))
            {
                return Content("Err|用户名称不能为空");
            }
            if (new T_CriminalBLL().Delete(FCode))
            {
                return Content("OK|删除成功" + result);
            }
            else
            {
                return Content("Err|删除失败" + result);
            }
        }

        public ActionResult ExcelKeyCriminalInport()//Excel重点犯人标志导入
        {

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
                string savePath = Server.MapPath("~/Meet/Upload/" + fname);
                f.SaveAs(savePath);

                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(stream);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                    int rows = sheet.LastRowNum;
                    int ErrNums = 0, SuccNums = 0;
                    string listFCode = "";
                    string ErrInfo = "";
                    if (rows < 1)
                    {
                        return Content("Excel表为空表,无数据!");
                    }
                    else
                    {
                        for (int i = 1; i <= rows; i++)
                        {
                            T_Criminal model = new T_Criminal();
                            try
                            {
                                SetRowDataToModel(sheet, i, ref model);
                                if (model == null)
                                {
                                    continue;
                                }
                                int j = new CommTableInfoBLL().ExecSql("Update T_Criminal Set FStatus='" + model.FStatus.ToString() + "' where FCode='" + model.FCode + "' and FName='" + model.FName + "'");
                                if (j > 0)
                                {
                                    if (listFCode == "")
                                    {
                                        listFCode = "'" + model.FCode + "'";
                                    }
                                    else
                                    {
                                        listFCode = listFCode + ",'" + model.FCode + "'";
                                    }
                                    SuccNums++;
                                }
                                else
                                {
                                    ErrNums++;
                                }

                            }
                            catch
                            {
                                ErrNums++;
                            }
                        }
                        //获得主单的信息
                        if (listFCode != "")
                        {
                            List<T_Criminal> openInfos = new T_CriminalBLL().GetModelList(" FCode in(" + listFCode + ")");
                            return Content("OK|" + jss.Serialize(openInfos) + "|导入完成，失败记录：" + ErrNums.ToString() + "条，成功：" + SuccNums.ToString() + " 条,失败情况:" + ErrInfo);
                        }
                        else
                        {
                            return Content("Err|没有数据");
                        }

                    }
                }
            }
            return Content("Err|未知错误");
        }

        //把Excel行数据重点犯人标志数据转成Model
        private void SetRowDataToModel(NPOI.SS.UserModel.ISheet sheet, int i, ref T_Criminal tmpmodel)
        {
            NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
            string fcode = "";
            try
            {
                fcode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
            }
            catch
            {
                fcode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
            }

            T_Criminal model = new T_CriminalBLL().GetModel(fcode);
            if (model != null)
            {
                model.FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似
                string strStatus = "";
                try
                {
                    strStatus = row.GetCell(2).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                    strStatus = strStatus.Replace(" ", "");
                    strStatus = strStatus.Replace("　", "");
                    if (strStatus == "重点")
                    {
                        model.FStatus = 1;
                    }
                    else if (strStatus == "艾感")
                    {
                        model.FStatus = 2;
                    }
                    else
                    {
                        model.FStatus = 0;
                    }
                }
                catch
                {
                    strStatus = "";
                    model.FStatus = 0;
                }
                tmpmodel = model;
            }

        }


        #endregion

        #region 消费时间控制
        public ActionResult ShoppingTimeManager()
        {
            return View();
        }
        #endregion

        #region 调拨管理
        public ActionResult TransferManager()
        {
            return View();
        }
        #endregion

        #region 调拨管理
        public ActionResult CYChangeManager()
        {
            return View();
        }
        #endregion

    }
}