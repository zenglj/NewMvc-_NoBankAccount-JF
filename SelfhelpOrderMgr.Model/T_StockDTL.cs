using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_StockDTL
    public class T_StockDTL:BaseModel
    {


        /// <summary>
        /// StockId
        /// </summary>		
        public string StockId { get; set; }

        /// <summary>
        /// GCode
        /// </summary>		
        public string GCode { get; set; }

        public string GName { get; set; }

        /// <summary>
        /// GTXM
        /// </summary>		
        public string GTXM { get; set; }

        /// <summary>
        /// GCount
        /// </summary>		
        public decimal GCount { get; set; }

        /// <summary>
        /// GDJ
        /// </summary>		
        public decimal GDJ { get; set; }

        /// <summary>
        /// Flag
        /// </summary>		
        public int Flag { get; set; }

        /// <summary>
        /// StockFlag
        /// </summary>		
        public int StockFlag { get; set; }
        /// <summary>
        /// InOutFlag
        /// </summary>		
        public int InOutFlag { get; set; }
        /// <summary>
        /// Remark
        /// </summary>		

        public string Remark { get; set; }

        /// <summary>
        /// WareHouseCode
        /// </summary>		
        public string WareHouseCode { get; set; }

        /// <summary>
        /// ProductDate生成日期
        /// </summary>
        public DateTime? ProductDate { get; set; }
    }
}

