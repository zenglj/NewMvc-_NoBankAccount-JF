using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 积分的消费等级的标准
    /// </summary>
    public class T_JF_DengjiType : BaseModel
    {
		/// <summary>
		/// 工种编号
		/// </summary>
		public int TypeFlag { get; set; }
		/// <summary>
		/// 工种名称
		/// </summary>
		public string TypeName { get; set; }
		/// <summary>
		/// 完成率
		/// </summary>
		public decimal CompletionRate { get; set; }
		/// <summary>
		/// 评议等级
		/// </summary>
		public string WorkResult { get; set; }
		/// <summary>
		/// 对应商品等级
		/// </summary>
		public string LevelName { get; set; }

		/// <summary>
		/// 积分使用金额
		/// </summary>
		public decimal JfUseMaxPoints { get; set; }
		/// <summary>
		/// 间隔月数
		/// </summary>
		public string MonthCount { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		/// 使用类型 0是超市购物 1是积分兑换
		/// </summary>
		public int UseType { get; set; }
		public string CrtBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string ModBy { get; set; }
		public DateTime? ModifyDate { get; set; }
		public bool isDelete { get; set; }
	}
}