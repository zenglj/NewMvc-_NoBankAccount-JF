using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Drawing;

/// <summary>
///CommonClass 的摘要说明
/// </summary>
public class CommonClass
{
	public CommonClass()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
    }
    public static void ReportToExcel(System.Web.UI.Control ctl, string fileName)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page page = new Page();
        HtmlForm form = new HtmlForm();
        ctl.Page.EnableViewState = false;
        page.EnableEventValidation = false;
        page.DesignerInitialize();
        page.Controls.Add(form);
        form.Controls.Add(ctl);
        page.RenderControl(htw);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;

        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF7;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Charset = "gb2312";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", Encoding.UTF8).ToString());

        HttpContext.Current.Response.Write(sb.ToString());
        HttpContext.Current.Response.End();

    }
    private static void RecursiveClear(Control control)
    {

        for (int i = control.Controls.Count - 1; i >= 0; i--)
        {
            RecursiveClear(control.Controls[i]);
        }

        if (control is Repeater)
        {
            control.Parent.Controls.Remove(control);//5~1-a-s-p-x
        }
        else if (control is LinkButton)
        {
            LiteralControl literal = new LiteralControl();
            control.Parent.Controls.Add(literal);
            literal.Text = ((LinkButton)control).Text;
            control.Parent.Controls.Remove(control);
        }
        else if (control is Button)
        {
            control.Parent.Controls.Remove(control);
        }

        else if (control is System.Web.UI.WebControls.Image)
        {
            if (((System.Web.UI.WebControls.Image)control).Visible)
            {
                control.Parent.Controls.Add(new LiteralControl("<span style='font-size:8px;'>o</span>"));
            }
            control.Parent.Controls.Remove(control);
        }
        else if (control is ListControl)
        {
            LiteralControl literal = new LiteralControl();
            control.Parent.Controls.Add(literal);
            try
            {
                literal.Text = ((ListControl)control).SelectedItem.Text;
            }
            catch
            {
            }
            control.Parent.Controls.Remove(control);

        }

        return;
    }
    protected static void ClearChildControls(Control dg1)
    {
        if (dg1 is GridView)
        {
            GridView dg = (GridView)dg1;
            for (int i = dg.Columns.Count - 1; i >= 0; i--)
            {
                if (dg.Columns[i].GetType().Name == "ButtonColumn"
                    || dg.Columns[i].GetType().Name == "CheckBoxField"
                    || dg.Columns[i].GetType().Name == "CommandField")
                {
                    dg.Columns[i].Visible = false;
                }
            }
            RecursiveClear(dg1);
        }
        else
        {
            RecursiveClear(dg1);
        }
    }

    public string UTF8ToGB2312(string str)
    {
        try
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding gb2312 = Encoding.GetEncoding("gb2312");//Encoding.Default ,936
            byte[] temp = utf8.GetBytes(str);
            byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
            string result = gb2312.GetString(temp1);
            return result;
        }
        catch
        {
            return null;
        }
    }

    public string GB2312ToUTF8(string str)
    {
        try
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8"); ;
            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            byte[] unicodeBytes = gb2312.GetBytes(str);
            byte[] asciiBytes = Encoding.Convert(gb2312, utf8, unicodeBytes);
            char[] asciiChars = new char[utf8.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            utf8.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string result = new string(asciiChars);
            return result;
        }
        catch
        {
            return "";
        }
    }

    //获取IP地址的最后一组地址
    public static string GetIpLastCode(string ip)
    {
        string cc = ".";
        string[] ips = ip.Split(cc.ToCharArray());
        //string ipLastCode = string.Format("000", ips[3]);
        string ipLastCode = "001";
        if (ips.Length > 3)
        {
            ipLastCode = "000" + ips[3];
            ipLastCode = ipLastCode.Substring(ipLastCode.Length - 3);
        }
        return ipLastCode;
    }
}
