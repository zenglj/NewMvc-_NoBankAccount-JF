using SelfhelpOrderMgr.Model;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NPOI.XSSF.UserModel; // for XSSF (.xlsx) - Office 2007 and later
using NPOI.HSSF.UserModel; // for HSSF (.xls) - Office 97 and earlier
using System.IO;
using NPOI.SS.UserModel;

using Spire.Xls;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    /// <summary>
    /// Doc文件转换成PDF格式
    /// </summary>
    public static class DocxToPdfConvertHelper
    {
        public static ResultInfo ConvertWordToPdf(string docSourceFile, string pdfTargetFile)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                // 加载docx文件
                Spire.Doc.Document document = new Spire.Doc.Document();
                document.LoadFromFile(docSourceFile);

                // 将文档保存为PDF格式
                document.SaveToFile(pdfTargetFile, Spire.Doc.FileFormat.PDF);
                rs.Flag = true;
                rs.ReMsg = "OK|转换成功";
                rs.DataInfo = pdfTargetFile;
                return rs;
            }
            catch (Exception ex)
            {
                rs.Flag = true;
                rs.ReMsg = "Err|" + ex.Message;
                return rs;
            }

        }




        public static ResultInfo ConvertExcelToPdf(string excelFilePath, string pdfFilePath)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                // 加载Excel文件
                IWorkbook workbook;
                using (FileStream file = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }

                // 使用Spire.XLS进行PDF转换
                Workbook book = new Workbook();
                book.LoadFromFile(excelFilePath);

                // 保存为PDF
                book.SaveToFile(pdfFilePath, Spire.Xls.FileFormat.PDF);

                rs.Flag = true;
                rs.ReMsg = "OK|转换成功";
                rs.DataInfo = pdfFilePath;
                return rs;
            }
            catch (Exception ex)
            {

                rs.Flag = true;
                rs.ReMsg = "Err|" + ex.Message;
                return rs;
            }
            
        }


    }

}