using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Dto
{
    public class StockDetailAddDto
    {

        /// <summary>
        /// SeqId
        /// </summary>		
        public int Id { get; set; }

        ///// <summary>
        ///// GCode
        ///// </summary>		
        //public string GCode { get; set; }
        /// <summary>
        /// GTXM
        /// </summary>		
        public string GTXM { get; set; }

        public string GCode { get; set; }

        public string GName { get; set; }

        /// <summary>
        /// GCount
        /// </summary>		
        public decimal GCount { get; set; }

        /// <summary>
        /// GDJ
        /// </summary>		
        public decimal GDJ { get; set; }

        /// <summary>
        /// Remark
        /// </summary>		

        public string Remark { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? ProductDate { get; set; }
    }
}
