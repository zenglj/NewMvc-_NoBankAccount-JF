using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_AtmDealList:BaseModel
    {
        /// <summary>
        /// ATM机Id号
        /// </summary>
        [System.ComponentModel.Description("ATM机Id号")]
        public int MacId { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        [System.ComponentModel.Description("流水号")]
        public string AtmSerialNo {get;set;}
        /// <summary>
        /// 动作类型
        /// </summary>
        [System.ComponentModel.Description("动作类型")]
        public string ActionType {get;set;}
        /// <summary>
        /// 变动金额
        /// </summary>
        [System.ComponentModel.Description("变动金额")]
        public decimal ChangeAmount {get;set;}
        /// <summary>
        /// 机器余额
        /// </summary>
        [System.ComponentModel.Description("机器余额")]
        public decimal MachineBalance {get;set;}

        /// <summary>
        /// 传送日期
        /// </summary>
        [System.ComponentModel.Description("传送日期")]
        public string TranDate { get; set; }
        /// <summary>
        /// 传送时间
        /// </summary>
        [System.ComponentModel.Description("传送时间")]
        public string TranTime { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [System.ComponentModel.Description("创建日期")]
        public DateTime CrtDate {get;set;}
        /// <summary>
        /// 对账状态（-1对账失败，0默认，1对账成功）
        /// </summary>
        [System.ComponentModel.Description("对账状态")]
        public int StatusFlag {get;set;}
        /// <summary>
        /// 备注信息
        /// </summary>
        [System.ComponentModel.Description("备注")]
        public string Remark {get;set;}
    }
}