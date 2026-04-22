using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SelfhelpOrderMgr.Dto
{
    public class StockImportExcelDto
    {
        public string StockId { get; set; }
        public string StockType { get; set; }
        public string Remark { get; set; }
        public DateTime InOutDate { get; set; }


        // 对应 formData.append('ExcelFile', ...)
        // 参数名必须与前端 append 的 Key 完全一致
        //public HttpPostedFileBase excelFile { get; set; }
    }

}
