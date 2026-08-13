using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Web.Models
{
    public class ReportSearchReqDto: PageReqDto
    {
        public string FCode { get; set; }
        public string FName { get; set; }
        public string cyName { get; set; }

        // 日期时间类型，使用可空类型防止前端传空字符串时反序列化报错
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string areaName { get; set; }

        // 数组类型字段，使用 List<string> 接收
        public List<string> CashTypes { get; set; }
        public List<string> PayTypes { get; set; }

        public string CrtBy { get; set; }
        public string CriminalFlag { get; set; }

        public DateTime? SendDate_Start { get; set; }
        public DateTime? SendDate_End { get; set; }

        public List<string> AccTypes { get; set; }
        public List<string> BankFlags { get; set; }

        public string CheckFlag { get; set; }
        public List<string> FFlags { get; set; }

        public string CardTypeFlag { get; set; }
        public string PayMode { get; set; }
        public string FRemark { get; set; }



    }

    /// <summary>
    /// 分页请求dto
    /// </summary>
    public class  PageReqDto
    {
        // 分页参数
        public int page { get; set; } = 1; // 默认值 1
        public int rows { get; set; } = 10; // 默认值 10
        public string sort { get; set; }
        public string order { get; set; }

    }
}


