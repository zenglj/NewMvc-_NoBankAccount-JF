using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using SelfhelpOrderMgr.Web.Controllers;
using System.Collections.Generic;

/// <summary>
/// 使用NPOI操作Excel，无需Office COM组件
/// Created By 囧月 http://lwme.cnblogs.com
/// 部分代码取自http://msdn.microsoft.com/zh-tw/ee818993.aspx
/// NPOI是POI的.NET移植版本，目前稳定版本中仅支持对xls文件（Excel 97-2003）文件格式的读写
/// NPOI官方网站http://npoi.codeplex.com/
/// </summary>
public class ExcelRender
{
    /// <summary>
    /// 根据Excel列类型获取列的值
    /// </summary>
    /// <param name="cell">Excel列</param>
    /// <returns></returns>
    private static string GetCellValue(ICell cell)
    {
        if (cell == null)
            return string.Empty;
        switch (cell.CellType)
        {
            case CellType.Blank:
                return string.Empty;
            case CellType.Boolean:
                return cell.BooleanCellValue.ToString();
            case CellType.Error:
                return cell.ErrorCellValue.ToString();
            case CellType.Numeric:
            case CellType.Unknown:
            default:
                return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Formula:
                try
                {
                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                    e.EvaluateInCell(cell);
                    return cell.ToString();
                }
                catch
                {
                    return cell.NumericCellValue.ToString();
                }
        }
    }

    /// <summary>
    /// 自动设置Excel列宽
    /// </summary>
    /// <param name="sheet">Excel表</param>
    private static void AutoSizeColumns(ISheet sheet)
    {
        if (sheet.PhysicalNumberOfRows > 0)
        {
            IRow headerRow = sheet.GetRow(0);

            for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }
    }

    /// <summary>
    /// 保存Excel文档流到文件
    /// </summary>
    /// <param name="ms">Excel文档流</param>
    /// <param name="fileName">文件名</param>
    private static void SaveToFile(MemoryStream ms, string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();

            data = null;
        }
    }

    /// <summary>
    /// 输出文件到浏览器
    /// </summary>
    /// <param name="ms">Excel文档流</param>
    /// <param name="context">HTTP上下文</param>
    /// <param name="fileName">文件名</param>
    private static void RenderToBrowser(MemoryStream ms, HttpContext context, string fileName)
    {
        if (context.Request.Browser.Browser == "IE")
            fileName = HttpUtility.UrlEncode(fileName);
        context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
        context.Response.BinaryWrite(ms.ToArray());
    }

    /// <summary>
    /// DataReader转换成Excel文档流
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static MemoryStream RenderToExcel(IDataReader reader)
    {
        MemoryStream ms = new MemoryStream();

        using (reader)
        {//HSSFWorkbook
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);
            int cellCount = reader.FieldCount;

            ICellStyle headStyle = workbook.CreateCellStyle();
            StyleHeadRow(workbook, headStyle);

            ICellStyle infoStyle = workbook.CreateCellStyle();
            StyleInfoRow(workbook, infoStyle);

            // handling header.
            for (int i = 0; i < cellCount; i++)
            {
                headerRow.CreateCell(i).SetCellValue(reader.GetName(i));
                headerRow.GetCell(i).CellStyle = headStyle;
            }

            // handling value.
            int rowIndex = 1;
            while (reader.Read())
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                for (int i = 0; i < cellCount; i++)
                {
                    dataRow.CreateCell(i).SetCellValue(reader[i].ToString());
                    dataRow.GetCell(i).CellStyle = infoStyle;
                }

                rowIndex++;
            }

            AutoSizeColumns(sheet);

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
        }
        return ms;
    }

    //Excel数据行样式
    private static void StyleInfoRow(IWorkbook workbook, ICellStyle infoStyle,bool dataFlag=false)
    {
        infoStyle.Alignment = HorizontalAlignment.Center;
        infoStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont infoFont = workbook.CreateFont();
        infoFont.FontHeightInPoints = 10;
        //infoFont.Boldweight = 700;
        infoStyle.SetFont(infoFont);

        infoStyle.WrapText = true;//自动换行
        if (dataFlag == true)
        {
            NPOI.HSSF.UserModel.HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            infoStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
        }
        
        // 设置边框
        infoStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


        //infoStyle.BorderBottom = CellBorderType.THIN; //下边框
        //infoStyle.BorderLeft = CellBorderType.THIN;//左边框
        //infoStyle.BorderTop = CellBorderType.THIN;//上边框
        //infoStyle.BorderRight = CellBorderType.THIN;//右边框
    }

    
    private static void StyleInfoRow(IWorkbook workbook, ICellStyle infoStyle,int aligmentStyle)
    {
        StyleInfoRow(workbook, infoStyle);
        switch (aligmentStyle)
        {
            case 0: 
                { infoStyle.Alignment = HorizontalAlignment.Left; 
                } break;
            case 2: 
                { infoStyle.Alignment = HorizontalAlignment.Right; 
                } break;
        }
    }

    private static void StyleDateRow(IWorkbook workbook, ICellStyle infoStyle)
    {
        infoStyle.Alignment = HorizontalAlignment.Center;
        infoStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont infoFont = workbook.CreateFont();
        infoFont.FontHeightInPoints = 10;
        //infoFont.Boldweight = 700;
        infoStyle.SetFont(infoFont);

        infoStyle.WrapText = true;//自动换行

        NPOI.HSSF.UserModel.HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
        infoStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

        // 设置边框
        infoStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


        //infoStyle.BorderBottom = CellBorderType.THIN; //下边框
        //infoStyle.BorderLeft = CellBorderType.THIN;//左边框
        //infoStyle.BorderTop = CellBorderType.THIN;//上边框
        //infoStyle.BorderRight = CellBorderType.THIN;//右边框
    }

    private static void StyleDecimalRow(IWorkbook workbook, ICellStyle infoStyle)
    {        
        infoStyle.Alignment = HorizontalAlignment.Center;
        infoStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont infoFont = workbook.CreateFont();
        infoFont.FontHeightInPoints = 10;
        //infoFont.Boldweight = 700;
        infoStyle.SetFont(infoFont);

        infoStyle.WrapText = true;//自动换行

        NPOI.HSSF.UserModel.HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
        infoStyle.DataFormat = format.GetFormat("0.00;[Red]-0.00");

        // 设置边框
        infoStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        infoStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

    }

    //Excel列头行样式
    private static void StyleHeadRow(IWorkbook workbook, ICellStyle headStyle)
    {
        headStyle.Alignment = HorizontalAlignment.Center;
        headStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont font = workbook.CreateFont();
        font.FontHeightInPoints = 12;
        font.Boldweight = 700;
        headStyle.SetFont(font);

        headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

        //headStyle.BorderBottom = CellBorderType.THIN; //下边框
        //headStyle.BorderLeft = CellBorderType.THIN;//左边框
        //headStyle.BorderTop = CellBorderType.THIN;//上边框
        //headStyle.BorderRight = CellBorderType.THIN;//右边框

    }

    //Excel标题行样式
    private static void StyleTitleRow(IWorkbook workbook, ICellStyle titleStyle)
    {
        titleStyle.Alignment = HorizontalAlignment.Center;
        titleStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont titleFont = workbook.CreateFont();
        titleFont.FontHeightInPoints = 20;
        titleFont.Boldweight = 700;
        titleFont.FontName = "黑体";
        titleStyle.SetFont(titleFont);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="table"></param>
    /// <param name="titleName">标题</param>
    /// <param name="mul_lan">多栏</param>
    /// <returns></returns>
    public static MemoryStream RenderSumToExcel(DataTable table, string titleName, bool mul_lan = false, string strCountTime = "")
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            //bool mul_lan = false;//多栏导出标志

            int rowIndex = 1;//设定初始行
            //生成Excel 表格标题名称首行
            if (mul_lan == false)
            {
                CreateExcelTitle(table, titleName, workbook, sheet, strCountTime);
                //生成Excel明细数据函数
                CreateExcelDetail(table, workbook, sheet, rowIndex, strCountTime);
            }
            else
            {
                CreateExcelTitle_TowCol(table, titleName, workbook, sheet, strCountTime);
                //生成Excel明细数据函数
                CreateExcelDetail_TwoCol(table, workbook, sheet, rowIndex);
            }


            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

        }
        return ms;
    }

    /// <summary>
    /// 返回Excel数据
    /// </summary>
    /// <param name="table">DataTabe数据表</param>
    /// <param name="titleName">文件首行标题名</param>
    /// <param name="sumFieldNo">汇总列号，从0开始</param>
    /// <returns></returns>
    public static MemoryStream RenderSumToExcel(DataTable table, string titleName, int sumFieldNo, bool mul_lan = false, string strCountTime = "")
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            

            int rowIndex = 1;//设定初始行

            ////生成Excel 表格标题名称首行
            //CreateExcelTitle(table, titleName, workbook, sheet);
            ////生成Excel明细数据函数
            //CreateExcelDetail(table, workbook, sheet, rowIndex);
            if (strCountTime != "")
            {
                rowIndex = 2;//设定初始行 +加一行
            }
            if (mul_lan == false)
            {
                //生成Excel 表格标题名称首行
                CreateExcelTitle(table, titleName, workbook, sheet, strCountTime);
                //生成Excel明细数据函数
                CreateExcelDetail(table, workbook, sheet, rowIndex, strCountTime);
            }
            else
            {
                CreateExcelTitle_TowCol(table, titleName, workbook, sheet, strCountTime);
                //生成Excel明细数据函数
                CreateExcelDetail_TwoCol(table, workbook, sheet, rowIndex);
            }

            //指定列汇总
            rowIndex = CreateExcelFieldSum(table, workbook, sumFieldNo, sheet, rowIndex, mul_lan);


            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

        }
        return ms;
    }

    //分组统计方式导出Excel
    public static MemoryStream RenderSumToExcel(DataTable table, string titleName, int sumFieldNo, int groupField, bool mul_lan = false)
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            //生成Excel 表格标题名称首行
            CreateExcelTitle(table, titleName, workbook, sheet);

            //列头标题行样式
            ICellStyle headStyle = workbook.CreateCellStyle();
            StyleHeadRow(workbook, headStyle);
            //Excel明细行样试
            ICellStyle infoStyle = workbook.CreateCellStyle();
            StyleInfoRow(workbook, infoStyle);


            int rowIndex = 1;//设定初始行

            IRow headerRow = sheet.CreateRow(1);
            //序号行
            headerRow.CreateCell(0).SetCellValue("序号");//If Caption not set, returns the ColumnName value
            headerRow.GetCell(0).CellStyle = headStyle;
            // handling header.
            foreach (DataColumn column in table.Columns)
            {
                headerRow.CreateCell(column.Ordinal+1).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
                headerRow.GetCell(column.Ordinal+1).CellStyle = headStyle;
            }

            // handling value.
            rowIndex++;
            decimal sumMoney = 0;
            decimal groupMoney = 0;
            string groupName = "";
            for (int rowid=0;rowid< table.Rows.Count;rowid++)
            {
                DataRow row = table.Rows[rowid];
                #region 分组统计
                if (groupName != row[groupField].ToString())
                {
                    if (rowIndex > 2)
                    {
                        InsertGroupRow(table, sumFieldNo, groupField, sheet, ref rowIndex, ref groupMoney, ref groupName, row[groupField].ToString());
                    }
                    else
                    {
                        groupName = row[groupField].ToString();
                    }
                }
                #endregion

                IRow dataRow = sheet.CreateRow(rowIndex);
                //行号
                dataRow.CreateCell(0).SetCellValue(rowid+1);
                dataRow.GetCell(0).CellStyle = infoStyle;
                foreach (DataColumn column in table.Columns)
                {
                    dataRow.CreateCell(column.Ordinal+1).SetCellValue(row[column].ToString());
                    dataRow.GetCell(column.Ordinal+1).CellStyle = infoStyle;
                }
                sumMoney = sumMoney + Convert.ToDecimal(row[sumFieldNo].ToString());
                groupMoney = groupMoney + Convert.ToDecimal(row[sumFieldNo].ToString());

                rowIndex++;

            }
            InsertGroupRow(table, sumFieldNo, groupField, sheet, ref rowIndex, ref groupMoney, ref groupName, "未尾行");

            IRow footerRow = sheet.CreateRow(rowIndex);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (sumFieldNo == i)
                {
                    footerRow.CreateCell(i+1).SetCellValue(sumMoney.ToString());
                }
                else if (sumFieldNo - 1 == i)
                {
                    footerRow.CreateCell(i+1).SetCellValue("合计:");
                }
                else
                {
                    footerRow.CreateCell(i+1).SetCellValue("");
                }

            }
            //AutoSizeColumns(sheet);
            sheet.PrintSetup.Scale = 100;
            //sheet.PrintSetup.PaperSize = 9;//A4纸张打印
            sheet.PrintSetup.PaperSize = (short)PaperSize.A4;//A4纸张打印


            sheet.PrintSetup.Copies = 3;
            sheet.PrintSetup.NoColor = true;
            sheet.PrintSetup.Landscape = false;//表示纵向
            sheet.PrintSetup.PaperSize = (short)PaperSize.A4;

            sheet.PrintSetup.FitHeight = 2;
            sheet.PrintSetup.FitWidth = 3;
            sheet.IsPrintGridlines = true;

            //是否自适应界面
            sheet.FitToPage = false;
            //设置打印标题
            //workbook.SetRepeatingRowsAndColumns(0, 0, 5, 0, 5);
            //workbook.SetRepeatingRowsAndColumns(0, 0, 5, 0, 1);
            SetExcelPrintPageAndMargin(sheet);

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

        }
        return ms;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strFileName">文件的名称</param>
    /// <param name="table">数据表</param>
    /// <param name="titleName">标题名称</param>
    /// <param name="groupFieldNo">分组的字段</param>
    /// <param name="sumFields">要统计字段（可以多个）</param>
    /// <param name="sumFields">合并计算的字段</param>
    public static void RenderGroupbyToExcel(string strFileName, DataTable table, string titleName, int groupFieldNo, int[] sumFields,int sumFieldNo)
    {
        using (MemoryStream ms = RenderGroupbySumToExcel(table, titleName,  groupFieldNo, sumFields, sumFieldNo))
        {
            SaveToFile(ms,  strFileName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="table">数据表</param>
    /// <param name="titleName">标题名称</param>
    /// <param name="groupFieldNo">分组的字段</param>
    /// <param name="sumFields">要统计字段（可以多个）</param>
    /// <param name="sumFields">合并计算的字段</param>
    /// <returns>文件流</returns>
    


    ///
    public static MemoryStream RenderGroupbySumToExcel( DataTable table, string titleName, int groupFieldNo, int[] sumFields,int sumFieldNo)
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            //生成Excel 表格标题名称首行
            CreateExcelTitle(table, titleName, workbook, sheet);

            int rowIndex = 1;//设定初始行

            //生成Excel明细数据函数
            #region 写入主体数据
            //列头标题行样式
            ICellStyle headStyle = workbook.CreateCellStyle();
            StyleHeadRow(workbook, headStyle);
            //Excel明细行样试
            ICellStyle infoStyle = workbook.CreateCellStyle();
            StyleInfoRow(workbook, infoStyle);

            // 列头标题行.
            IRow headerRow = sheet.CreateRow(rowIndex);
            foreach (DataColumn column in table.Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
                headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                if (column.Ordinal == 2)
                {
                    sheet.SetColumnWidth(column.Ordinal, 40 * 256);
                }
                else
                {
                    sheet.SetColumnWidth(column.Ordinal, 15 * 256);
                }
                
            }
            ////增加回款合计列
            //headerRow.CreateCell(table.Columns.Count).SetCellValue("回款合计");//If Caption not set, returns the ColumnName value
            //headerRow.GetCell(table.Columns.Count).CellStyle = headStyle;
            //sheet.SetColumnWidth(table.Columns.Count, 20 * 256);

            // handling value.
            rowIndex++;
            decimal daySum = 0;
            string dt="";
            int lastRows = 2;
            foreach (DataRow row in table.Rows)
            {                
                if (dt == "")
                {
                    dt = row[groupFieldNo].ToString();
                }
                
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in table.Columns)
                {
                    //是 int decimal 两种类型的数据
                    if (daySum.GetType() == row[column].GetType() || lastRows.GetType() == row[column].GetType())
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble( row[column].ToString()));
                    }
                    else
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    dataRow.GetCell(column.Ordinal).CellStyle = infoStyle;
                }
                //按日汇总行
                if (dt != row[groupFieldNo].ToString() && rowIndex > 2)
                {
                    //汇总合单元格并设定值
                    SetSumCellVelue(table, sheet, rowIndex, infoStyle, daySum, lastRows,sumFieldNo);

                    dt = row[groupFieldNo].ToString();
                    lastRows = rowIndex;
                    daySum = 0;
                }
                //计算汇总的金额
                foreach (int d in sumFields)
                {
                    daySum = daySum + Convert.ToDecimal(row[d].ToString());
                }
                rowIndex++;
            }

            //最后一行汇总
            if (dt != "" && rowIndex > 2)
            {
                //汇总合单元格并设定值
                SetSumCellVelue(table, sheet, rowIndex, infoStyle, daySum, lastRows,sumFieldNo);
            }

            //AutoSizeColumns(sheet);            
            //sheet.PrintSetup.Scale = 100;
            //sheet.PrintSetup.PaperSize = 9;//A4纸张打印 
            #endregion
            //设定页边距
            SetExcelPrintPageAndMargin(sheet);

            AutoSizeColumns(sheet);
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

        }
        return ms;
    }
    //汇总合单元格并设定值
    private static void SetSumCellVelue(DataTable table, ISheet sheet, int rowIndex, ICellStyle infoStyle, decimal daySum, int lastRows,int sumFieldNo)
    {
        IRow sumRow = sheet.GetRow(lastRows);
        sumRow.CreateCell(sumFieldNo).SetCellValue(Convert.ToDouble(daySum));
        sumRow.GetCell(sumFieldNo).CellStyle = infoStyle;
        SetCellRangeAddress(sheet, lastRows, rowIndex - 1, sumFieldNo, sumFieldNo);//合并单元格
    }

    //生成Excel 表格标题名称首行
    private static void CreateExcelTitle(DataTable table, string titleName, IWorkbook workbook, ISheet sheet, string strCountTime = "")
    {
        ICellStyle titleStyle = workbook.CreateCellStyle();
        StyleTitleRow(workbook, titleStyle);

        //表格的主标题名称行
        IRow titleRow = sheet.CreateRow(0);
        titleRow.HeightInPoints = 35;//行高
        titleRow.CreateCell(0).SetCellValue(titleName);
        titleRow.GetCell(0).CellStyle = titleStyle;

        //SetCellRangeAddress(sheet, 0, 0, 0, table.Columns.Count - 1);//合并单元格
        SetCellRangeAddress(sheet, 0, 0, 0, table.Columns.Count );//合并单元格,加一列序号


        if (strCountTime != "")
        {
            // 显示统计日期行.
            IRow headerCountTimeRow = sheet.CreateRow(1);
            headerCountTimeRow.CreateCell(0).SetCellValue(strCountTime);
            SetCellRangeAddress(sheet, 1, 1, 0, table.Columns.Count);//合并单元格,加一列序号
        }
    }

    //生成Excel表主体数据行
    private static void CreateExcelDetail(DataTable table,  IWorkbook workbook, ISheet sheet, int rowIndex, string strCountTime = "")
    {

        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);
        //Excel明细行样试
        ICellStyle infoStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoStyle);
        ICellStyle dateStyle = workbook.CreateCellStyle();
        StyleDateRow(workbook, dateStyle);
        ICellStyle decimalStyle = workbook.CreateCellStyle();

        StyleDecimalRow(workbook, decimalStyle);
        // 列头标题行.
        IRow headerRow = sheet.CreateRow(rowIndex);

        headerRow.CreateCell(0).SetCellValue("序号");//If Caption not set, returns the ColumnName value
        headerRow.GetCell(0).CellStyle = headStyle;
        sheet.SetColumnWidth(0, 4 * 256);


        foreach (DataColumn column in table.Columns)
        {
            
            headerRow.CreateCell(column.Ordinal + 1).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
            headerRow.GetCell(column.Ordinal + 1).CellStyle = headStyle;
            if (table.Rows[0][column].ToString().Length > 10)
            {
                sheet.SetColumnWidth(column.Ordinal + 1, 2*11 * 256);
            }
            else if (column.DataType.Equals(typeof(decimal)))
            {
                sheet.SetColumnWidth(column.Ordinal + 1, 9 * 256);
            }
            else
            {
                sheet.SetColumnWidth(column.Ordinal + 1, 11 * 256);
            }
            
        }

        // handling value.
        rowIndex++;
        foreach (DataRow row in table.Rows)
        {
            IRow dataRow = sheet.CreateRow(rowIndex);

            if (strCountTime != "")
            {
                dataRow.CreateCell(0).SetCellValue(rowIndex - 2);
            }
            else
            {
                dataRow.CreateCell(0).SetCellValue(rowIndex - 1);
            }
            dataRow.GetCell(0).CellStyle = infoStyle;           
            
            
            foreach (DataColumn column in table.Columns)
            {
                //StyleInfoRow(workbook, infoStyle);
                NPOI.HSSF.UserModel.HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                infoStyle.DataFormat = format.GetFormat("");
                //dataRow.CreateCell(column.Ordinal + 1).SetCellValue(row[column].ToString());
                Type dataType = column.DataType;
                if (dataType.Name == "DateTime")
                {
                    if (row[column].ToString() != "")
                    {
                        dataRow.CreateCell(column.Ordinal + 1).SetCellValue(Convert.ToDateTime(row[column].ToString()));
                    }
                    else
                    {
                        dataRow.CreateCell(column.Ordinal + 1).SetCellValue("");
                    }
                    dataRow.GetCell(column.Ordinal + 1).CellStyle = dateStyle;
                }
                else if (dataType.Name == "DateTime" || dataType.Name == "Double")
                {
                    if (row[column].ToString() != "")
                    {
                        dataRow.CreateCell(column.Ordinal + 1).SetCellValue(Convert.ToDouble(row[column].ToString()));
                    }
                    else
                    {
                        dataRow.CreateCell(column.Ordinal + 1).SetCellValue("");
                    }
                    dataRow.GetCell(column.Ordinal + 1).CellStyle = decimalStyle;
                    
                }
                else
                {
                    if (dataType.Name == "String")
                    {
                        dataRow.CreateCell(column.Ordinal + 1).SetCellValue(row[column].ToString());
                    }
                    else if (dataType.Name == "Int32")
                    {
                        if (row[column].ToString() != "")
                        {
                            dataRow.CreateCell(column.Ordinal + 1).SetCellValue(Convert.ToDouble(row[column].ToString()));
                        }
                        else
                        {
                            dataRow.CreateCell(column.Ordinal + 1).SetCellValue("");
                        }
                    }                    
                    else
                    {
                        //其他数据类型的判断.......
                        dataRow.CreateCell(column.Ordinal + 1).SetCellValue(row[column].ToString());
                    }
                    dataRow.GetCell(column.Ordinal + 1).CellStyle = infoStyle;
                }
            }
            rowIndex++;
        }

        //设定页眉页脚
        SetExcelPrintHeaderAndFooter(sheet);

        AutoSizeColumns(sheet);
        //设定Excel的边距和纸张类型
        SetExcelPrintPageAndMargin(sheet);
    }

    private static void CreateExcelDetailNotId(DataTable table, IWorkbook workbook, ISheet sheet, int rowIndex, string strCountTime = "")
    {

        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);
        //Excel明细行样试
        ICellStyle infoStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoStyle);

        // 列头标题行.
        IRow headerRow = sheet.CreateRow(rowIndex);

        //headerRow.CreateCell(0).SetCellValue("序号");//If Caption not set, returns the ColumnName value
        //headerRow.GetCell(0).CellStyle = headStyle;
        //sheet.SetColumnWidth(0, 4 * 256);


        foreach (DataColumn column in table.Columns)
        {
            headerRow.CreateCell(column.Ordinal ).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
            headerRow.GetCell(column.Ordinal ).CellStyle = headStyle;
            if (table.Rows[0][column].ToString().Length > 10)
            {
                sheet.SetColumnWidth(column.Ordinal , 2 * 11 * 256);
            }
            else if (column.DataType.Equals(typeof(decimal)))
            {
                sheet.SetColumnWidth(column.Ordinal , 9 * 256);
            }
            else
            {
                sheet.SetColumnWidth(column.Ordinal , 11 * 256);
            }

        }

        // handling value.
        rowIndex++;
        foreach (DataRow row in table.Rows)
        {
            IRow dataRow = sheet.CreateRow(rowIndex);

            if (strCountTime != "")
            {
                dataRow.CreateCell(0).SetCellValue(rowIndex - 2);
            }
            else
            {
                dataRow.CreateCell(0).SetCellValue(rowIndex - 1);
            }
            dataRow.GetCell(0).CellStyle = infoStyle;




            foreach (DataColumn column in table.Columns)
            {
                Type dataType = column.DataType;
                if (dataType.Name == "String")
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                else if (dataType.Name == "Int32")
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToInt32(row[column].ToString()));
                }
                else if (dataType.Name == "decimal")
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column].ToString()));
                }
                else if (dataType.Name == "Double")
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column].ToString()));
                }
                else if (dataType.Name == "DateTime")
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDateTime(row[column].ToString()));
                }
                else
                {
                    //其他数据类型的判断.......
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                
                dataRow.GetCell(column.Ordinal ).CellStyle = infoStyle;
            }
            rowIndex++;
        }

        //设定页眉页脚
        SetExcelPrintHeaderAndFooter(sheet);

        AutoSizeColumns(sheet);
        //设定Excel的边距和纸张类型
        SetExcelPrintPageAndMargin(sheet);
    }

    //（两栏模式）生成Excel表主体数据行
    private static void CreateExcelTitle_TowCol(DataTable table, string titleName, IWorkbook workbook, ISheet sheet, string strCountTime = "")
    {
        ICellStyle titleStyle = workbook.CreateCellStyle();
        StyleTitleRow(workbook, titleStyle);

        //表格的主标题名称行
        IRow titleRow = sheet.CreateRow(0);
        titleRow.HeightInPoints = 35;//行高
        titleRow.CreateCell(0).SetCellValue(titleName);
        titleRow.GetCell(0).CellStyle = titleStyle;

        SetCellRangeAddress(sheet, 0, 0, 0, 2*table.Columns.Count - 1+1);//合并单元格

        if (strCountTime != "")
        {
            // 显示统计日期行.
            IRow headerCountTimeRow = sheet.CreateRow(1);
            headerCountTimeRow.CreateCell(0).SetCellValue(strCountTime);
            SetCellRangeAddress(sheet, 1, 1, 0, table.Columns.Count);//合并单元格,加一列序号
        }
    }

    //（两栏模式）生成Excel表主体数据行
    private static void CreateExcelDetail_TwoCol(DataTable table, IWorkbook workbook, ISheet sheet, int rowIndex)
    {
        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);
        //Excel明细行样试
        ICellStyle infoStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoStyle);

        // 列头标题行.
        IRow headerRow = sheet.CreateRow(rowIndex);
        foreach (DataColumn column in table.Columns)
        {
            headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

            sheet.SetColumnWidth(column.Ordinal, 11 * 256);
        }
        //间隔列
        headerRow.CreateCell( table.Columns.Count).SetCellValue(" ");//If Caption not set, returns the ColumnName value
        //headerRow.GetCell(table.Columns.Count).CellStyle = headStyle;

        sheet.SetColumnWidth(  table.Columns.Count, 3 * 256);
        
        //第二栏
        foreach (DataColumn column in table.Columns)
        {
            headerRow.CreateCell(column.Ordinal+1 + table.Columns.Count).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
            headerRow.GetCell(column.Ordinal +1+ table.Columns.Count).CellStyle = headStyle;

            sheet.SetColumnWidth(column.Ordinal + 1 + table.Columns.Count, 11 * 256);
        }

        // handling value.
        rowIndex++;
        //foreach (DataRow row in table.Rows)
        //{
            
        //}
        DataRow row;
        for (int k = 0; k < table.Rows.Count;k++ )
        {
            IRow dataRow = sheet.CreateRow(rowIndex);
            row = table.Rows[k];
            foreach (DataColumn column in table.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                dataRow.GetCell(column.Ordinal).CellStyle = infoStyle;
            }
            //rowIndex++;
            k++;
            if (k < table.Rows.Count)
            {                
                row = table.Rows[k];
                //第二栏
                foreach (DataColumn column in table.Columns)
                {
                    dataRow.CreateCell(column.Ordinal + 1 + table.Columns.Count).SetCellValue(row[column].ToString());
                    dataRow.GetCell(column.Ordinal + 1 + table.Columns.Count).CellStyle = infoStyle;
                }
            }
            
            rowIndex++;
        }

        //页面设置
        //exce.ActiveWindow.DisplayGridlines=false;//不显示网格线
        //sheets.DisplayAutomaticPageBreaks=true;//显示分页线
        //sheets.PageSetup.CenterFooter = "第 &P 页，共 &N 页";
        //sheets.PageSetup.TopMargin=exce.InchesToPoints(0.590551181102362);//上1.5
        //sheets.PageSetup.BottomMargin=exce.InchesToPoints(0.590551181102362);//下1.5
        //sheets.PageSetup.LeftMargin=exce.InchesToPoints(0.78740157480315);//左边距2
        //sheets.PageSetup.RightMargin=exce.InchesToPoints(0.393700787401575);//右边距1
        //sheets.PageSetup.HeaderMargin=exce.InchesToPoints(0.393700787401575);//页眉1
        //sheets.PageSetup.FooterMargin=exce.InchesToPoints(0.393700787401575);//页脚1
        //sheets.PageSetup.CenterHorizontally=true;//水平居中
        //sheets.PageSetup.PrintTitleRows = "$1:$3";//顶端标题行
        //sheets.PageSetup.PaperSize=Excel.XlPaperSize.xlPaperA3;//.xlPaperB4;//纸张大小
        //sheets.PageSetup.Orientation=Excel.XlPageOrientation.xlLandscape;//纸张方向.横向

        //设定页眉页脚
        SetExcelPrintHeaderAndFooter(sheet);
        
        //设定Excel的边距和纸张类型
        SetExcelPrintPageAndMargin(sheet);

        if (table.Columns.Count>4)
        {
            sheet.PrintSetup.Scale = 81;
        }
    }

    //设定页眉页脚
    private static void SetExcelPrintHeaderAndFooter(ISheet sheet)
    {
        sheet.Header.Right = "打印日期: &D ";
        sheet.Footer.Left = "制表:______________";
        sheet.Footer.Center = "第 &P 页，共 &N 页";
        sheet.Footer.Right = "审核:______________";
    }
    //设定Excel的边距和纸张类型
    private static void SetExcelPrintPageAndMargin(ISheet sheet)
    {
        sheet.SetMargin(MarginType.LeftMargin, (double)0.5);
        sheet.SetMargin(MarginType.RightMargin, (double)0.5);
        sheet.SetMargin(MarginType.BottomMargin, (double)0.6);
        sheet.SetMargin(MarginType.TopMargin, (double)0.5);

        sheet.SetMargin(MarginType.HeaderMargin, (double)0.3);
        sheet.SetMargin(MarginType.FooterMargin, (double)0.3);
        sheet.FitToPage = false;

        //自动列宽
        //AutoSizeColumns(sheet);
        sheet.PrintSetup.Scale = 100;
        sheet.PrintSetup.PaperSize = 9;//A4纸张打印
    }

    //按DataTable指定列进行汇总合计，并增加汇总行
    /// <summary>
    /// 
    /// </summary>
    /// <param name="table"></param>
    /// <param name="workbook"></param>
    /// <param name="sumFieldNo"></param>
    /// <param name="sheet"></param>
    /// <param name="rowIndex"></param>
    /// <param name="mul_col">多栏2栏</param>
    /// <returns></returns>
    private static int CreateExcelFieldSum(DataTable table, IWorkbook workbook, int sumFieldNo, ISheet sheet, int rowIndex, bool mul_lan = false)
    {
        rowIndex++;
        decimal sumMoney = 0;
        if (mul_lan == true)
        {
            DataRow row;
            for (int k=0;k<table.Rows.Count; k++ )
            {
                row =table.Rows[k];
                sumMoney = sumMoney + Convert.ToDecimal(row[sumFieldNo].ToString());
                k++;
                if (k < table.Rows.Count)
                {
                    row = table.Rows[k];
                    sumMoney = sumMoney + Convert.ToDecimal(row[sumFieldNo].ToString());
                }
                rowIndex++;
            }
        }
        else
        {
            foreach (DataRow row in table.Rows)
            {
                sumMoney = sumMoney + Convert.ToDecimal(row[sumFieldNo].ToString());
                rowIndex++;
            }
        }
        

        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);

        IRow footerRow = sheet.CreateRow(rowIndex);

        int seqnoCol = 1;

        footerRow.CreateCell(0).SetCellValue("");
        footerRow.GetCell(0).CellStyle = headStyle;
        if(mul_lan==true)
        {
            seqnoCol = 0;
        }
        for (int i = 0; i < table.Columns.Count; i++)
        {
            if (sumFieldNo == i)
            {
                footerRow.CreateCell(i + seqnoCol).SetCellValue(sumMoney.ToString());
            }
            else if (sumFieldNo - 1 == i)
            {
                footerRow.CreateCell(i + seqnoCol).SetCellValue("合计:");
            }
            else
            {
                footerRow.CreateCell(i + seqnoCol).SetCellValue("");
            }
            footerRow.GetCell(i + seqnoCol).CellStyle = headStyle;
        }
        return rowIndex;
    }



    private static void InsertGroupRow(DataTable table, int sumFieldNo, int groupField, ISheet sheet, ref int rowIndex, ref decimal groupMoney, ref string groupName, string newGroupName)
    {

        IRow groupRow = sheet.CreateRow(rowIndex);
        for (int i = 0; i < table.Columns.Count; i++)
        {
            if (sumFieldNo == i)
            {
                groupRow.CreateCell(i+1).SetCellValue(groupMoney.ToString());
            }
            else if (sumFieldNo - 1 == i)
            {
                groupRow.CreateCell(i+1).SetCellValue("小计:");
            }
            else
            {
                groupRow.CreateCell(i+1).SetCellValue("");
            }
        }
        groupName = newGroupName;
        groupMoney = 0;
        sheet.SetRowBreak(rowIndex);//设置分页符
        rowIndex++;

    }



    /// <summary>
    /// DataReader转换成Excel文档流，并保存到文件
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="fileName">保存的路径</param>
    public static void RenderToExcel(IDataReader reader, string fileName)
    {
        using (MemoryStream ms = RenderToExcel(reader))
        {
            SaveToFile(ms, fileName);
        }
    }

    /// <summary>
    /// DataReader转换成Excel文档流，并输出到客户端
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="context">HTTP上下文</param>
    /// <param name="fileName">输出的文件名</param>
    public static void RenderToExcel(IDataReader reader, HttpContext context, string fileName)
    {
        using (MemoryStream ms = RenderToExcel(reader))
        {
            RenderToBrowser(ms, context, fileName);
        }
    }


    /// <summary>
    /// DataTable转换成Excel文档流，并保存到文件
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fileName">保存的路径</param>
    public static void RenderToExcel_NoSeqno(DataTable table, string fileName)
    {
        using (MemoryStream ms = RenderToExcel_NoSeqno(table))
        {
            SaveToFile(ms, fileName);
        }
    }

    /// <summary>
    /// DataTable转换成Excel文档流
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MemoryStream RenderToExcel_NoSeqno(DataTable table)
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            int rowIndex = 0;//设定初始行
            //生成Excel明细数据函数
            CreateExcelDetail_NoSeqno(table, workbook, sheet, rowIndex);

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
        }
        return ms;
    }

    //生成Excel表主体数据行
    private static void CreateExcelDetail_NoSeqno(DataTable table, IWorkbook workbook, ISheet sheet, int rowIndex)
    {

        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);
        //Excel明细行样试
        ICellStyle infoStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoStyle);

        // 列头标题行.
        IRow headerRow = sheet.CreateRow(rowIndex);


        foreach (DataColumn column in table.Columns)
        {
            headerRow.CreateCell(column.Ordinal ).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

            sheet.SetColumnWidth(column.Ordinal , 11 * 256);
        }

        // handling value.
        rowIndex++;
        foreach (DataRow row in table.Rows)
        {
            IRow dataRow = sheet.CreateRow(rowIndex);


            foreach (DataColumn column in table.Columns)
            {
                dataRow.CreateCell(column.Ordinal ).SetCellValue(row[column].ToString());
                dataRow.GetCell(column.Ordinal ).CellStyle = infoStyle;
            }
            rowIndex++;
        }

        //设定页眉页脚
        SetExcelPrintHeaderAndFooter(sheet);

        AutoSizeColumns(sheet);
        //设定Excel的边距和纸张类型
        SetExcelPrintPageAndMargin(sheet);
    }


    /// <summary>
    /// DataTable转换成Excel文档流，并保存到文件
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fileName">保存的路径</param>
    public static void RenderToExcel(DataTable table, string fileName, bool mul_lan = false)
    {
        using (MemoryStream ms = RenderToExcel(table, mul_lan))
        {
            SaveToFile(ms, fileName);
        }
    }

    /// <summary>
    /// DataTable转换成Excel文档流
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MemoryStream RenderToExcel(DataTable table,  bool mul_lan = false, string strCountTime = "")
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            int rowIndex = 0;//设定初始行
            //生成Excel明细数据函数
            CreateExcelDetail(table,  workbook, sheet, rowIndex, strCountTime);

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
        }
        return ms;
    }

    

    
    public static void RenderToExcel(DataTable table, string titleName, string fileName, bool mul_lan = false)
    {
        using (MemoryStream ms = RenderSumToExcel(table, titleName, mul_lan))
        {
            SaveToFile(ms, fileName);
        }
    }
    //消费统计清单的Excel导出
    public static void RenderToExcel(DataTable table, string titleName, string fileName, xfSummaryInfo xfsuminfo,DateTime startTime,DateTime endTime)
    {
        using (MemoryStream ms = RenderSumToExcel(table, titleName, xfsuminfo,startTime,endTime))
        {
            SaveToFile(ms, fileName);
        }
    }
    //消费统计清单的Excel导出
    public static MemoryStream RenderSumToExcel(DataTable table, string titleName, xfSummaryInfo xfsuminfo,DateTime startTime,DateTime endTime)
    {
        MemoryStream ms = new MemoryStream();

        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            //bool mul_lan = false;//多栏导出标志

            int rowIndex = 1;//设定初始行
            //生成Excel 表格标题名称首行


            CreateExcelTitle(table, titleName, workbook, sheet, "");
            //生成Excel明细数据函数
            CreateExcelDetail(table, workbook, sheet, rowIndex, "");




            //列头标题行样式
            ICellStyle headStyle = workbook.CreateCellStyle();
            StyleHeadRow(workbook, headStyle);
            //Excel明细行样试
            ICellStyle infoStyle = workbook.CreateCellStyle();
            StyleInfoRow(workbook, infoStyle);

            

            // handling value.
            rowIndex=table.Rows.Count+2;

            //xfsuminfo;
            IRow sumRow = sheet.CreateRow(rowIndex);
            sumRow.CreateCell(0).SetCellValue("合计");
            sumRow.GetCell(0).CellStyle = headStyle;
            sumRow.CreateCell(1).SetCellValue(string.Format("银行卡号:{0}", xfsuminfo.BankAccCode));
            sumRow.GetCell(1).CellStyle = infoStyle;            
            sumRow.CreateCell(2).SetCellValue("");
            sumRow.GetCell(2).CellStyle = infoStyle;
            SetCellRangeAddress(sheet, rowIndex, rowIndex, 1,2);//合并单元格,加一列序号            
            sumRow.CreateCell(3).SetCellValue(string.Format("所在队别:{0}",xfsuminfo.FAreaName));
            sumRow.GetCell(3).CellStyle = infoStyle;
            sumRow.CreateCell(4).SetCellValue(string.Format("统计区间:{0} 至 {1}",startTime.ToShortDateString(),endTime.ToShortDateString()) );
            sumRow.GetCell(4).CellStyle = infoStyle;
            sumRow.CreateCell(5).SetCellValue("");
            sumRow.GetCell(5).CellStyle = infoStyle;
            SetCellRangeAddress(sheet, rowIndex, rowIndex, 4, 5);//合并单元格,加一列序号


            rowIndex++;
            sumRow = sheet.CreateRow(rowIndex);
            sumRow.CreateCell(0).SetCellValue("1、");
            sumRow.GetCell(0).CellStyle = headStyle;
            sumRow.CreateCell(1).SetCellValue(string.Format("累计总消费:{0}(刚需消费{1})",xfsuminfo.Leijizongxf,xfsuminfo.gxFood));
            sumRow.GetCell(1).CellStyle = infoStyle;
            sumRow.CreateCell(2).SetCellValue("");
            sumRow.GetCell(2).CellStyle = infoStyle;
            SetCellRangeAddress(sheet, rowIndex, rowIndex, 1, 2);//合并单元格,加一列序号
            sumRow.CreateCell(3).SetCellValue(string.Format("统计月数:{0}", xfsuminfo.Tongjiyueshu));
            sumRow.GetCell(3).CellStyle = infoStyle;
            sumRow.CreateCell(4).SetCellValue(string.Format("购物月均消费:{0}", xfsuminfo.Yuejunxf));
            sumRow.GetCell(4).CellStyle = infoStyle;
            sumRow.CreateCell(5).SetCellValue("");
            sumRow.GetCell(5).CellStyle = infoStyle;
            SetCellRangeAddress(sheet, rowIndex, rowIndex, 4,5);//合并单元格,加一列序号

            rowIndex++;
            sumRow = sheet.CreateRow(rowIndex);
            sumRow.CreateCell(0).SetCellValue("");
            sumRow.GetCell(0).CellStyle = headStyle;
            sumRow.CreateCell(1).SetCellValue("其中");
            sumRow.GetCell(1).CellStyle = infoStyle;
            sumRow.CreateCell(2).SetCellValue(string.Format("衣服被褥:{0}", xfsuminfo.Yufubeizi));
            sumRow.GetCell(2).CellStyle = infoStyle;
            sumRow.CreateCell(3).SetCellValue(string.Format("购物消费:{0}", xfsuminfo.Chaoshigouwu));
            sumRow.GetCell(3).CellStyle = infoStyle;
            sumRow.CreateCell(4).SetCellValue(string.Format("其他扣款"));
            sumRow.GetCell(4).CellStyle = infoStyle;
            sumRow.CreateCell(5).SetCellValue(string.Format("{0}", xfsuminfo.Yaopinqita));
            sumRow.GetCell(5).CellStyle = infoStyle;
            //SetCellRangeAddress(sheet, rowIndex, rowIndex, 3, 4);//合并单元格,加一列序号

            rowIndex++;
            sumRow = sheet.CreateRow(rowIndex);
            sumRow.CreateCell(0).SetCellValue("");
            sumRow.GetCell(0).CellStyle = headStyle;
            sumRow.CreateCell(1).SetCellValue("");
            sumRow.GetCell(1).CellStyle = infoStyle;
            sumRow.CreateCell(2).SetCellValue(string.Format("营养餐:{0}", xfsuminfo.Xiaochao));
            sumRow.GetCell(2).CellStyle = infoStyle;
            sumRow.CreateCell(3).SetCellValue(string.Format("书刊报纸:{0}", xfsuminfo.Shukanbaozi));
            sumRow.GetCell(3).CellStyle = infoStyle;
            sumRow.CreateCell(4).SetCellValue(string.Format("交罚金(总共)"));
            sumRow.GetCell(4).CellStyle = infoStyle;
            sumRow.CreateCell(5).SetCellValue(string.Format("{0}", xfsuminfo.Jiaofajin));
            sumRow.GetCell(5).CellStyle = infoStyle;
            SetCellRangeAddress(sheet, rowIndex-2, rowIndex, 0, 0);//合并单元格,加一列序号
            SetCellRangeAddress(sheet, rowIndex - 1, rowIndex, 1, 1);//合并单元格,加一列序号

            rowIndex++;
            sumRow = sheet.CreateRow(rowIndex);
            sumRow.CreateCell(0).SetCellValue("2、");
            sumRow.GetCell(0).CellStyle = headStyle;
            sumRow.CreateCell(1).SetCellValue("总收入");
            sumRow.GetCell(1).CellStyle = headStyle;
            sumRow.CreateCell(2).SetCellValue(string.Format("{0}", xfsuminfo.Zongshouru));
            sumRow.GetCell(2).CellStyle = infoStyle;
            sumRow.CreateCell(3).SetCellValue(string.Format("其中汇款:{0}", xfsuminfo.Huikuanshouru));
            sumRow.GetCell(3).CellStyle = infoStyle;
            sumRow.CreateCell(4).SetCellValue(string.Format("劳动报酬:{0}",xfsuminfo.Laodongbaochou));
            sumRow.GetCell(4).CellStyle = infoStyle;
            sumRow.CreateCell(5).SetCellValue(string.Format("零用金:{0}", xfsuminfo.Ningyongjin));
            sumRow.GetCell(5).CellStyle = infoStyle;
            //SetCellRangeAddress(sheet, rowIndex - 1, rowIndex, 0, 0);//合并单元格,加一列序号

            rowIndex++;
            sumRow = sheet.CreateRow(rowIndex);
            sumRow.CreateCell(0).SetCellValue("3、");
            sumRow.GetCell(0).CellStyle = headStyle;
            sumRow.CreateCell(1).SetCellValue("账户余额");
            sumRow.GetCell(1).CellStyle = headStyle;
            sumRow.CreateCell(2).SetCellValue(string.Format("{0}(其中劳动报酬不可用金额{1})", xfsuminfo.Zhanghuzongyue,xfsuminfo.BuKeYongMoney));
            sumRow.GetCell(2).CellStyle = infoStyle;
            sumRow.CreateCell(3).SetCellValue("");
            sumRow.GetCell(3).CellStyle = infoStyle;
            sumRow.CreateCell(4).SetCellValue("");
            sumRow.GetCell(4).CellStyle = infoStyle;
            sumRow.CreateCell(5).SetCellValue("");
            sumRow.GetCell(5).CellStyle = infoStyle;
            SetCellRangeAddress(sheet, rowIndex , rowIndex, 2, 5);//合并单元格,加一列序号

            rowIndex++;


            sheet.SetColumnWidth(0, 6 * 256);

            foreach (DataColumn column in table.Columns)
            {
                sheet.SetColumnWidth(column.Ordinal + 1, 16 * 256);
            }
            sheet.SetColumnWidth(3, 20 * 256);

            //设定页眉页脚
            //SetExcelPrintHeaderAndFooter(sheet);

            sheet.Header.Right = "打印日期: &D ";
            sheet.Footer.Left = "制表:______________";
            sheet.Footer.Center = "第 &P 页，共 &N 页";
            sheet.Footer.Right = "";

            //AutoSizeColumns(sheet);
            //设定Excel的边距和纸张类型
            SetExcelPrintPageAndMargin(sheet);





            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

        }
        return ms;
    }


    /// <summary>
    /// 带统计时间的格式输出Excel
    /// </summary>
    /// <param name="table"></param>
    /// <param name="titleName"></param>
    /// <param name="sumFieldNo"></param>
    /// <param name="fileName"></param>
    /// <param name="mul_lan"></param>
    /// <param name="strCountTime"></param>
    public static void RenderToExcel(DataTable table, string titleName, int sumFieldNo, string fileName, bool mul_lan = false, string strCountTime="")
    {
        using (MemoryStream ms = RenderSumToExcel(table, titleName, sumFieldNo, mul_lan,strCountTime))
        {
            SaveToFile(ms, fileName);
        }
    }

    /// <summary>
    /// Excel导出，支持标题、未尾汇总、按指列分组汇总
    /// </summary>
    /// <param name="table">数据表DataTable</param>
    /// <param name="titleName">表头标题</param>
    /// <param name="sumFieldNo">汇总字段</param>
    /// <param name="fileName">保存的文件名</param>
    /// <param name="groupField">分组汇总的字段</param>
    public static void RenderToExcel(DataTable table, string titleName, int sumFieldNo, string fileName, int groupField, bool mul_lan = false)
    {
        using (MemoryStream ms = RenderSumToExcel(table, titleName, sumFieldNo, groupField, mul_lan))
        {
            SaveToFile(ms, fileName);
        }
    }

    /// <summary>
    /// DataTable转换成Excel文档流，并输出到客户端
    /// </summary>
    /// <param name="table"></param>
    /// <param name="response"></param>
    /// <param name="fileName">输出的文件名</param>
    public static void RenderToExcel(DataTable table, HttpContext context, string fileName)
    {
        using (MemoryStream ms = RenderToExcel(table))
        {
            RenderToBrowser(ms, context, fileName);
        }
    }

    /// <summary>
    /// Excel文档流是否有数据
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <returns></returns>
    public static bool HasData(Stream excelFileStream)
    {
        return HasData(excelFileStream, 0);
    }

    /// <summary>
    /// Excel文档流是否有数据
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="sheetIndex">表索引号，如第一个表为0</param>
    /// <returns></returns>
    public static bool HasData(Stream excelFileStream, int sheetIndex)
    {
        using (excelFileStream)
        {
            IWorkbook workbook = new HSSFWorkbook(excelFileStream);
            if (workbook.NumberOfSheets > 0)
            {
                if (sheetIndex < workbook.NumberOfSheets)
                {
                    ISheet sheet = workbook.GetSheetAt(sheetIndex);
                    return sheet.PhysicalNumberOfRows > 0;
                }
            }

        }
        return false;
    }

    /// <summary>
    /// Excel文档流转换成DataTable
    /// 第一行必须为标题行
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="sheetName">表名称</param>
    /// <returns></returns>
    public static DataTable RenderFromExcel(Stream excelFileStream, string sheetName)
    {
        return RenderFromExcel(excelFileStream, sheetName, 0);
    }

    /// <summary>
    /// Excel文档流转换成DataTable
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="sheetName">表名称</param>
    /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
    /// <returns></returns>
    public static DataTable RenderFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex)
    {
        DataTable table = null;

        using (excelFileStream)
        {
            IWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheet(sheetName);
            table = RenderFromExcel(sheet, headerRowIndex);

        }
        return table;
    }

    /// <summary>
    /// Excel文档流转换成DataTable
    /// 默认转换Excel的第一个表
    /// 第一行必须为标题行
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <returns></returns>
    public static DataTable RenderFromExcel(Stream excelFileStream)
    {
        return RenderFromExcel(excelFileStream, 0, 0);
    }

    /// <summary>
    /// Excel文档流转换成DataTable
    /// 第一行必须为标题行
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="sheetIndex">表索引号，如第一个表为0</param>
    /// <returns></returns>
    public static DataTable RenderFromExcel(Stream excelFileStream, int sheetIndex)
    {
        return RenderFromExcel(excelFileStream, sheetIndex, 0);
    }

    /// <summary>
    /// Excel文档流转换成DataTable
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="sheetIndex">表索引号，如第一个表为0</param>
    /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
    /// <returns></returns>
    public static DataTable RenderFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex)
    {
        DataTable table = null;

        using (excelFileStream)
        {
            IWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            table = RenderFromExcel(sheet, headerRowIndex);
        }
        return table;
    }

    /// <summary>
    /// Excel表格转换成DataTable
    /// </summary>
    /// <param name="sheet">表格</param>
    /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
    /// <returns></returns>
    private static DataTable RenderFromExcel(ISheet sheet, int headerRowIndex)
    {
        DataTable table = new DataTable();

        IRow headerRow = sheet.GetRow(headerRowIndex);
        int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
        int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

        //handling header.
        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            table.Columns.Add(column);
        }

        for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
        {
            IRow row = sheet.GetRow(i);
            DataRow dataRow = table.NewRow();

            if (row != null)
            {
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = GetCellValue(row.GetCell(j));
                }
            }

            table.Rows.Add(dataRow);
        }

        return table;
    }

    /// <summary>
    /// Excel文档导入到数据库
    /// 默认取Excel的第一个表
    /// 第一行必须为标题行
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="insertSql">插入语句</param>
    /// <param name="dbAction">更新到数据库的方法</param>
    /// <returns></returns>
    public static int RenderToDb(Stream excelFileStream, string insertSql, DBAction dbAction)
    {
        return RenderToDb(excelFileStream, insertSql, dbAction, 0, 0);
    }

    public delegate int DBAction(string sql, params IDataParameter[] parameters);

    /// <summary>
    /// Excel文档导入到数据库
    /// </summary>
    /// <param name="excelFileStream">Excel文档流</param>
    /// <param name="insertSql">插入语句</param>
    /// <param name="dbAction">更新到数据库的方法</param>
    /// <param name="sheetIndex">表索引号，如第一个表为0</param>
    /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
    /// <returns></returns>
    public static int RenderToDb(Stream excelFileStream, string insertSql, DBAction dbAction, int sheetIndex, int headerRowIndex)
    {
        int rowAffected = 0;
        using (excelFileStream)
        {
            IWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            StringBuilder builder = new StringBuilder();

            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    builder.Append(insertSql);
                    builder.Append(" values (");
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        builder.AppendFormat("'{0}',", GetCellValue(row.GetCell(j)).Replace("'", "''"));
                    }
                    builder.Length = builder.Length - 1;
                    builder.Append(");");
                }

                if ((i % 50 == 0 || i == rowCount) && builder.Length > 0)
                {
                    //每50条记录一次批量插入到数据库
                    rowAffected += dbAction(builder.ToString());
                    builder.Length = 0;
                }
            }

        }
        return rowAffected;
    }

    /// <summary>
    /// 合并单元格
    /// </summary>
    /// <param name="sheet">要合并单元格所在的sheet</param>
    /// <param name="rowstart">开始行的索引</param>
    /// <param name="rowend">结束行的索引</param>
    /// <param name="colstart">开始列的索引</param>
    /// <param name="colend">结束列的索引</param>
    public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
    {
        CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
        sheet.AddMergedRegion(cellRangeAddress);
    }

    #region 银行开卡清单Excel

    public static void RenderToExcelByBankList(DataTable table, string strFileName, string titleName, string unitName,int[,] cols)
    {
        MemoryStream ms = new MemoryStream();
        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            int rowIndex = 0;//设定初始行

            //创建标题行

            CreateExcelBankListTitle(table, titleName, workbook, sheet, unitName, "");

            //创建明细
            //生成Excel明细数据函数
            rowIndex = 2;
            CreateExcelBankDetail(table, workbook, sheet, rowIndex,cols, "");

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
        }

        //保存文件
        //return ms;
        SaveToFile(ms, strFileName);

    }

    //生成Excel 表格标题名称首行
    private static void CreateExcelBankListTitle(DataTable table, string titleName, IWorkbook workbook, ISheet sheet, string unitName, string strCountTime = "")
    {
        ICellStyle titleStyle = workbook.CreateCellStyle();
        StyleTitleRow(workbook, titleStyle);

        //表格的主标题名称行
        IRow titleRow = sheet.CreateRow(0);
        titleRow.HeightInPoints = 35;//行高
        titleRow.CreateCell(0).SetCellValue(titleName);
        titleRow.GetCell(0).CellStyle = titleStyle;

        //SetCellRangeAddress(sheet, 0, 0, 0, table.Columns.Count - 1);//合并单元格
        SetCellRangeAddress(sheet, 0, 0, 0, table.Columns.Count);//合并单元格,加一列序号


        ICellStyle detailStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, detailStyle);

        //表格的主标题名称行
        IRow unitNameRow = sheet.CreateRow(1);
        unitNameRow.HeightInPoints = 20;//行高
        unitNameRow.CreateCell(0).SetCellValue(string.Format("申请单位名称:--------{0}----------", unitName));
        unitNameRow.GetCell(0).CellStyle = detailStyle;
        for (int k = 1; k <= 5; k++)
        {
            unitNameRow.CreateCell(k).SetCellValue("");
            unitNameRow.GetCell(k).CellStyle = detailStyle;
        }
        SetCellRangeAddress(sheet, 1, 1, 0, 5);//合并单元格,加一列序号
        unitNameRow.CreateCell(6).SetCellValue(string.Format("申请日期: {0}", DateTime.Today.ToString("yyyy-MM-dd")));
        unitNameRow.GetCell(6).CellStyle = detailStyle;

    }

    //生成Excel表主体数据行
    private static void CreateExcelBankDetail(DataTable table, IWorkbook workbook, ISheet sheet, int rowIndex,int[,] cols, string strCountTime = "")
    {

        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);
        //Excel明细行样试
        ICellStyle infoStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoStyle);

        ICellStyle infoLeftStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoLeftStyle, 0);

        ICellStyle infoRightStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoRightStyle, 2);

        // 列头标题行.
        IRow headerRow = sheet.CreateRow(rowIndex);

        headerRow.CreateCell(0).SetCellValue("序号");//If Caption not set, returns the ColumnName value
        headerRow.GetCell(0).CellStyle = headStyle;
        sheet.SetColumnWidth(0, 6 * 256);

        foreach (DataColumn column in table.Columns)
        {
            headerRow.CreateCell(column.Ordinal + 1).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
            headerRow.GetCell(column.Ordinal + 1).CellStyle = headStyle;

            sheet.SetColumnWidth(column.Ordinal + 1, cols[column.Ordinal, 0] * 256);
        }

        // handling value.
        rowIndex++;
        int i = 1;
        foreach (DataRow row in table.Rows)
        {
            IRow dataRow = sheet.CreateRow(rowIndex);
            if (strCountTime != "")
            {
                dataRow.CreateCell(0).SetCellValue(i);
            }
            else
            {
                dataRow.CreateCell(0).SetCellValue(i);
            }
            int cc = table.Columns.Count-1;
            
            int textLen = 0;
            string ss=row[cc].ToString();
            textLen = GetStringLengthByEngMode(textLen, ss);
            int rowHi = Convert.ToInt16(Math.Ceiling(textLen *0.95/ cols[cc, 0]));//行高
            //dataRow.HeightInPoints = 18* Convert.ToInt16(Math.Ceiling(row[cc].ToString().Length * 1.7/cols[cc,0]));//行高
            if (rowHi > 1)
            {
                dataRow.HeightInPoints = 15 * rowHi;
            }
            dataRow.GetCell(0).CellStyle = infoStyle;
            foreach (DataColumn column in table.Columns)
            {
                dataRow.CreateCell(column.Ordinal + 1).SetCellValue(row[column].ToString());
                
                switch (cols[column.Ordinal, 1])
                {
                    case 0: {
                        dataRow.GetCell(column.Ordinal + 1).CellStyle = infoLeftStyle;
                    } break;
                    case 2:
                        {
                            dataRow.GetCell(column.Ordinal + 1).CellStyle = infoRightStyle;
                        } break;
                    default:
                        dataRow.GetCell(column.Ordinal + 1).CellStyle = infoStyle;
                        break;
                }

                
            }
            rowIndex++;
            i++;
        }

        //设定页眉页脚
        //SetExcelPrintHeaderAndFooter(sheet);

        AutoSizeColumns(sheet);
        //设定Excel的边距和纸张类型
        SetExcelPrintPageAndMargin(sheet);
    }

    private static int GetStringLengthByEngMode(int textLen, string ss)
    {
        for (int j = 0; j < ss.Length; j++)
        {
            if (ss.ToCharArray()[j] > 255)
            {
                textLen += 2;
            }
            else
            {
                textLen++;
            }
        }
        return textLen;
    }
    
    #endregion

    #region 银行税收证明清单Excel

    public static void RenderToExcelByShuishouList(DataTable table, string strFileName, string titleName, string unitName,int[,] cols)
    {
        MemoryStream ms = new MemoryStream();
        using (table)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            int rowIndex = 0;//设定初始行

            //创建标题行

            CreateExcelShuishouTitle(table, titleName, workbook, sheet, unitName, "");

            //创建明细
            //生成Excel明细数据函数
            rowIndex = 6;
            CreateExcelBankDetail(table, workbook, sheet, rowIndex,cols, "");


            //人员名单
            rowIndex = table.Rows.Count + 7;
            //表尾部
            rowIndex = CreateExcelShuishouFooter(sheet, rowIndex);

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
        }

        //保存文件
        //return ms;
        SaveToFile(ms, strFileName);

    }

    private static int CreateExcelShuishouFooter(ISheet sheet, int rowIndex)
    {
        IRow footerOneRow = sheet.CreateRow(rowIndex);
        footerOneRow.HeightInPoints = 30;//行高

        footerOneRow.CreateCell(0).SetCellValue("单位签章:");
        footerOneRow.CreateCell(3).SetCellValue("银行经办签章:");

        rowIndex++;
        IRow footerTowRow = sheet.CreateRow(rowIndex);
        footerTowRow.HeightInPoints = 30;//行高

        footerTowRow.CreateCell(0).SetCellValue("日期:");
        footerTowRow.CreateCell(3).SetCellValue("银行复核签章:");
        return rowIndex;
    }

    //生成Excel 表格标题名称首行
    private static void CreateExcelShuishouTitle(DataTable table, string titleName, IWorkbook workbook, ISheet sheet, string unitName, string strCountTime = "")
    {
        ICellStyle titleStyle = workbook.CreateCellStyle();
        StyleTitleRow(workbook, titleStyle);

        //列头标题行样式
        ICellStyle headStyle = workbook.CreateCellStyle();
        StyleHeadRow(workbook, headStyle);

        //Excel明细行样试
        ICellStyle infoStyle = workbook.CreateCellStyle();
        StyleInfoRow(workbook, infoStyle);

        //表格的主标题名称行
        IRow titleRow = sheet.CreateRow(0);        
        titleRow.CreateCell(0).SetCellValue(titleName+"(烛光卡专用)");
        titleRow.GetCell(0).CellStyle = titleStyle;
        titleRow.HeightInPoints = 35;//行高
        //SetCellRangeAddress(sheet, 0, 0, 0, table.Columns.Count - 1);//合并单元格
        SetCellRangeAddress(sheet, 0, 0, 0, table.Columns.Count);//合并单元格,加一列序号


        

        //表格的主标题名称行
        IRow minitleRow = sheet.CreateRow(1);
        minitleRow.HeightInPoints = 20;//行高
        for (int k = 0; k <= table.Columns.Count; k++)
        {
            minitleRow.CreateCell(k).SetCellValue("");
            minitleRow.GetCell(k).CellStyle = headStyle;
        }
        minitleRow.GetCell(0).SetCellValue(titleName);
        SetCellRangeAddress(sheet, 1, 1, 0, table.Columns.Count);//合并单元格,加一列序号


        //单位名称行
        IRow unitNameRow = sheet.CreateRow(2);
        unitNameRow.HeightInPoints = 20;//行高
        unitNameRow.CreateCell(0).SetCellValue("单位名称:");
        unitNameRow.GetCell(0).CellStyle = headStyle;
        
        for (int k = 1; k <= table.Columns.Count; k++)
        {
            unitNameRow.CreateCell(k).SetCellValue("");
            unitNameRow.GetCell(k).CellStyle = headStyle;
        }
        unitNameRow.GetCell(1).SetCellValue(unitName);
        SetCellRangeAddress(sheet, 2, 2, 1, table.Columns.Count);//合并单元格,加一列序号

        //单位声明行
        IRow unitStatementRow = sheet.CreateRow(3);
        sheet.CreateRow(3).Height = 100*10;//行高
        for (int k = 0; k <= table.Columns.Count; k++)
        {
            unitStatementRow.CreateCell(k).SetCellValue("");
            unitStatementRow.GetCell(k).CellStyle = infoStyle;
        }
        //unitStatementRow.HeightInPoints = 37;//行高
        unitStatementRow.GetCell(0).SetCellValue("单位声明内容:下表中所有人员均在我单位的服刑人员，均为中国税收居民，服刑期间在银行开户所使用的身份证件号码均为服刑的档案编号，服刑期满后，该身份证件号码失效，对应的银行账户关闭");
        
        SetCellRangeAddress(sheet, 3, 3, 0, table.Columns.Count);//合并单元格,加一列序号
        infoStyle.WrapText = true;
        unitStatementRow.GetCell(0).CellStyle = infoStyle;

        //声明人数
        IRow userCountRow = sheet.CreateRow(4);
        unitStatementRow.HeightInPoints = 20;//行高

        for (int k = 0; k <= table.Columns.Count; k++)
        {
            userCountRow.CreateCell(k).SetCellValue("");
            userCountRow.GetCell(k).CellStyle = headStyle;
        }
        userCountRow.GetCell(0).SetCellValue("声明人数");
        userCountRow.GetCell(1).SetCellValue(table.Rows.Count.ToString());
        userCountRow.GetCell(2).SetCellValue("声明人情况");
        userCountRow.GetCell(3).SetCellValue("服刑人员");
        //SetCellRangeAddress(sheet, 1, 1, 0, table.Columns.Count);//合并单元格,加一列序号

        //人员名单
        IRow userListRow = sheet.CreateRow(5);
        userListRow.HeightInPoints = 20;//行高

        for (int k = 0; k <= table.Columns.Count; k++)
        {
            userListRow.CreateCell(k).SetCellValue("");
            userListRow.GetCell(k).CellStyle = headStyle;
        }
        userListRow.GetCell(0).SetCellValue("人员名单");
        SetCellRangeAddress(sheet, 5, 5, 0, table.Columns.Count);//合并单元格,加一列序号

    }

    //生成Excel表主体数据行

    #endregion


}


public class ExcelColumnsWides{
    public int ColumnWide { get; set; }
    public int CellStyle { get; set; }
}