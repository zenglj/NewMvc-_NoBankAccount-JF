using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 赔偿金的标准
    /// </summary>
    public class T_PCJ_BaseType:BaseModel
    {
		[Key]
		public string TypeName { get; set; }
		/// <summary>
		/// 最大使用金额标准
		/// </summary>
		public decimal MaxUseMoney { get; set; }
		/// <summary>
		/// 赔偿金留存的比例
		/// </summary>
		public int RetentionRate { get; set; }
		public string Remark { get; set; }
		public string CrtBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string ModBy { get; set; }
		public DateTime? ModifyDate { get; set; }
		public bool isDelete { get; set; }
	}
}