using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class BedInfomation : BaseModel
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 级别Id
        /// </summary>
        public int LevelId { get; set; }
        /// <summary>
        /// 队别编号
        /// </summary>
        public string FAreaCode { get; set; }
        /// <summary>
        /// 队别名称
        /// </summary>
        public string FAreaName { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int FId { get; set; }
        /// <summary>
        /// 罪犯编号
        /// </summary>
        public string FCrimeCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string FCrimeName { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public int FFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FRemark { get; set; }


    }
}