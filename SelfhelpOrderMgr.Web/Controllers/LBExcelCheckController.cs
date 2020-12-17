using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class LBExcelCheckController : BaseController
    {
        //
        // GET: /LBExcelCheck/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExcelFileCheck()
        {
            string action = Request["action"];
            string LoginUserCode = Session["loginUserCode"].ToString();
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            
            switch (action)
            {
                case "ExcelFileCheck"://Excel文件导入检验
                    {
                        if (Request.Files.Count > 0)
                        {
                            //创建一个PC单号
                            string strFBid = new T_BONUSBLL().CreateOrderId( "CHK");//生成PC号

                            HttpPostedFileBase f = Request.Files[0];
                            string fname = f.FileName;
                            /* startIndex */
                            int index = fname.LastIndexOf("\\") + 1;
                            /* length */
                            int len = fname.Length - index;
                            fname = fname.Substring(index, len);
                            /* save to server */
                            string savePath =Server.MapPath("~/Upload/" + fname);
                            f.SaveAs(savePath);
                            //context.Response.Write("Success!");

                            using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                            {
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
                                int ErrNums = 0;
                                int OKNums = 0;
                                if (rows < 1)
                                {
                                    return Content("<script>alert('Excel表为空表,无数据!')</script>");
                                }
                                else
                                {
                                    for (int i = 1; i <= rows; i++)
                                    {
                                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                                        //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                                        NPOI.SS.UserModel.CellType iType = row.GetCell(0).CellType;
                                        string FCode = "";
                                        if (iType == 0)
                                        {
                                            FCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                                        }
                                        else
                                        {
                                            FCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                                        }
                                        string FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似
                                        string FMoney = "0";  //金额
                                        try
                                        {
                                            FMoney = Convert.ToString(row.GetCell(2).NumericCellValue);
                                        }
                                        catch { }

                                        string FRemark = "";  //开始日期
                                        try
                                        {
                                            FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                                        }
                                        catch { }

                                        string okInfo;
                                        int strFlag;
                                        //检测并增加一条记录（劳动报酬）
                                        string strDoResult = CheckAndAddRecord(strLoginName, LoginUserCode, FCode, FName, FMoney, strFBid, out okInfo, out strFlag);

                                        if (strDoResult != "")
                                        {
                                            T_ImportList importList = new T_ImportList();
                                            importList.ImportType = 8;
                                            importList.fcrimecode = FCode;
                                            importList.fname = FName;
                                            importList.Amount = Convert.ToDecimal(FMoney);
                                            importList.Crtdt = DateTime.Now;
                                            importList.CrtBy = strLoginName;
                                            importList.Remark = strDoResult;
                                            importList.pc = strFBid;
                                            importList.notes = "";
                                            //插入失败记录
                                            new T_ImportListBLL().Add(importList);
                                            ErrNums++;
                                        }
                                        else
                                        {
                                            OKNums++;
                                        }
                                    }
                                    if (ErrNums > 0)
                                    {
                                        List<T_ImportList> list = (List<T_ImportList>)new T_ImportListBLL().GetModelList(" pc='" + strFBid + "'");

                                        JavaScriptSerializer jss = new JavaScriptSerializer();
                                        if (list == null)
                                        {
                                            list = new List<T_ImportList>();
                                            T_ImportList m1 = new T_ImportList();
                                            list.Add(m1);
                                        }
                                        string ss = jss.Serialize(list);

                                        return Content("Error|" + strFBid + "|成功:" + OKNums.ToString() + "条,失败：" + ErrNums.ToString() + "条|" + ss);
                                    }
                                    else
                                    {
                                        return Content("OK|" + OKNums.ToString() + "条，全部通过!");
                                    }
                                }
                            }
                        }

                    } break;
                case "ErrorListOutport":
                    {
                        string strFBid = Request["excelBid"];

                        DataTable dt = new T_BONUSBLL().GetErrListDataTable(strFBid);
                        string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_CheckExcelErrList.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, strFileName);
                        return Content ("OK|" + strFBid + "_CheckExcelErrList.xls");


                    } 
                case "LoadErrorListByPcNo":
                    {
                        string strFBid = Request["excelBid"];
                        List<T_ImportList> list = (List<T_ImportList>)new T_ImportListBLL().GetModelList( " pc='" + strFBid + "'");

                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        if (list == null)
                        {
                            list = new List<T_ImportList>();
                            T_ImportList m1 = new T_ImportList();
                            list.Add(m1);
                        }
                        return Content(jss.Serialize(list));

                    }
                default:
                    break;
            }
            return Content("无效的参数");
        }


        private static string CheckAndAddRecord(string LoginUserName, string LoginUserCode, string strFCode, string strFName, string strFMoney, string strFBid, out string okInfo, out int strFlag)
        {
            string strRusult = "";//返回处理结果信息
            decimal cpctMoney = 0;//留存金额
            okInfo = "";//成功的信息
            T_BONUSDTL model = new T_BONUSDTL();

            //犯人信息不正确或是无权管理
            //List<T_CrimeList> criminals = (List<T_CrimeList>)new T_CriminalBLL().GetCrimeBySearch(dbname, strFCode, strFName, "", LoginUserCode);
            List<T_Criminal> criminals = (List<T_Criminal>)new T_CriminalBLL().GetCrimeBySearch(strFCode, "", "", LoginUserCode);
            strFlag = 0;
            if (criminals.Count <= 0)
            {
                strFlag = 1;//犯人信息不正确或是无权管理
                strRusult = "编号不存在或是无权管理";
            }
            else
            {
                T_Criminal criminal = criminals[0];
                if (criminal.fflag == 1)
                {
                    strFlag = 2;//犯人已经离监结算了
                    strRusult = "犯人已经离监结算了";
                }
                else if (criminal.FName != strFName)
                {
                    strFlag = 6;//姓名与编号不对一致
                    strRusult = "姓名出错：" + strFCode + "对应的姓名是" + criminal.FName + ",而不是" + strFName;
                }
                else
                {
                    //获取处遇信息
                    T_CY_TYPE cy = new T_CY_TYPEBLL().GetModelList("FCode='" + criminal.FCYCode + "'")[0];
                    cpctMoney = Convert.ToDecimal(strFMoney) * (decimal)cy.cpct / 100;

                    //获取队别信息
                    T_AREA area = new T_AREABLL().GetModel(criminal.FAreaCode);

                    //获取卡号信息
                    List<T_ICCARD_LIST> cards = new T_ICCARD_LISTBLL().GetModelList("FCrimeCode='"+ strFCode +"' and FFlag=1");

                    if (cards.Count == 0)
                    {
                        strFlag = 5;//未办理IC卡
                        strRusult = "该犯人未办理IC卡";
                    }

                    //获得系统最大奖金发放次数
                    T_SETTINGS sModel = new T_SETTINGSBLL().GetModelList("TYPE=606")[0];

                    string strUdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                    string strWhere = " UDate='" + strUdate + "' and FCrimeCode='" + strFCode + "'";
                    int userCount = new T_BONUSBLL().GetSendCountByBid(strFCode, strUdate);
                    if (Convert.ToInt32(sModel.VALUE) <= userCount)
                    {
                        strFlag = 7;//未办理IC卡
                        strRusult = strFCode + "," + strFName + ",超过每月最大发放次数:" + sModel.VALUE;
                    }

                }
            }
            return strRusult;
        }

	}
}