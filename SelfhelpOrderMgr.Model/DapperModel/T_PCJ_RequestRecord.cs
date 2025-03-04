using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_PCJ_RequestRecord : BaseModel
    {
		/// <summary>
		/// FCrimeCode
		/// </summary>
		[System.ComponentModel.Description("狱政编号")]
		public string FCrimeCode { get; set; }
		/// <summary>
		/// FCrimeName
		/// </summary>
		[System.ComponentModel.Description("姓名")]
		public string FCrimeName { get; set; }
		/// <summary>
		/// FAreaCode
		/// </summary>
		[System.ComponentModel.Description("队别编号")]
		public string FAreaCode { get; set; }

		[System.ComponentModel.Description("队别名称")]
		public string FAreaName { get; set; }
		/// <summary>
		/// 申请赔偿金标准的类型ID
		/// </summary>
		[System.ComponentModel.Description("标准编号")]
		public int TypeId { get; set; }
		/// <summary>
		/// 赔偿金标准的名称
		/// </summary>
		[System.ComponentModel.Description("申请标准")]
		public string TypeName { get; set; }
		/// <summary>
		/// 申请的时长，通常是表示几个月
		/// </summary>
		[System.ComponentModel.Description("申请时长")]
		public int UseMonths { get; set; }
		/// <summary>
		/// 截止日期
		/// </summary>
		[System.ComponentModel.Description("截止日期")]
		public DateTime EndTime { get; set; }
		/// <summary>
		/// 审核状态
		/// </summary>
		[System.ComponentModel.Description("审核状态")]
		public int Flag { get; set; }

		/// <summary>
		/// 审核摘要
		/// </summary>
		[System.ComponentModel.Description("审核摘要")]
		public string AuditText { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[System.ComponentModel.Description("备注")]
		public string Remark { get; set; }
		/// <summary>
		/// 是否删除
		/// </summary>
		[System.ComponentModel.Description("是否删除")]
		public bool IsDelete { get; set; }

		[System.ComponentModel.Description("创建人")]
		public string CrtBy { get; set; }

		[System.ComponentModel.Description("创建时间")]
		public DateTime CreateDate { get; set; }

		[System.ComponentModel.Description("修改人")]
		public string ModBy { get; set; }

		[System.ComponentModel.Description("最后修改时间")]
		public DateTime? ModifyDate { get; set; }


    }
}