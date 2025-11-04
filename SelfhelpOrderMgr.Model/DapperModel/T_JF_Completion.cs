using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 积分的完成率
    /// </summary>
    public class T_JF_Completion:BaseModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [System.ComponentModel.Description("狱政编号")]
        public string FCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [System.ComponentModel.Description("姓名")]
        public string FName { get; set; }
        /// <summary>
        /// 队别编号
        /// </summary>
        [System.ComponentModel.Description("队别编号")]
        public string FAreaCode { get; set; }
        /// <summary>
        /// 队别名称
        /// </summary>
        [System.ComponentModel.Description("队别名称")]
        public string FAreaName { get; set; }
        /// <summary>
        /// 工作类型编号
        /// </summary>
        [System.ComponentModel.Description("工种编号")]
        public string WorkType { get; set; }
        /// <summary>
        /// 工作类型名称
        /// </summary>
        [System.ComponentModel.Description("工种")]
        public string WorkTypeName { get; set; }
        /// <summary>
        /// 产值
        /// </summary>
        [System.ComponentModel.Description("产值")]
        public decimal OutputValue { get; set; }
        /// <summary>
        /// 完成率百分比
        /// </summary>
        [System.ComponentModel.Description("完成率")]
        public decimal CompletionRate { get; set; }
        /// <summary>
        /// 评议结果
        /// </summary>
        [System.ComponentModel.Description("评议结果")]
        public string WorkResult { get; set; }

        /// <summary>
        /// 商品等级
        /// </summary>
        [System.ComponentModel.Description("商品等级")]
        public string LevelName { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        [System.ComponentModel.Description("月份")]
        public string YearMonth { get; set; }
        /// <summary>
        /// 审核文本信息
        /// </summary>
        [System.ComponentModel.Description("审核信息")]
        public string AuditText { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [System.ComponentModel.Description("备注信息")]
        public string Remark { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        [System.ComponentModel.Description("创建人")]
        public string CrtBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [System.ComponentModel.Description("创建时间")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        [System.ComponentModel.Description("修改人")]
        public string ModBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [System.ComponentModel.Description("修改时间")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 批次编号
        /// </summary>        
        [System.ComponentModel.Description("批次编号")]
        public string PcNo { get; set; }
        /// <summary>
        /// 审核状态标识
        /// </summary>
        [System.ComponentModel.Description("审核状态")]
        public int Flag { get; set; }
        public bool IsDelete { get; set; }
    }
}