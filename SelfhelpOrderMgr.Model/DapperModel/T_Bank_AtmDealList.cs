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
        public int MacId { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string AtmSerialNo {get;set;}
        /// <summary>
        /// 动作类型
        /// </summary>
        public string ActionType {get;set;}
        /// <summary>
        /// 变动金额
        /// </summary>
        public decimal ChangeAmount {get;set;}
        /// <summary>
        /// 机器余额
        /// </summary>
        public decimal MachineBalance {get;set;}

        /// <summary>
        /// 传送日期
        /// </summary>
        public string TranDate { get; set; }
        /// <summary>
        /// 传送时间
        /// </summary>
        public string TranTime { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CrtDate {get;set;}
        /// <summary>
        /// 对账状态（-1对账失败，0默认，1对账成功）
        /// </summary>
        public int StatusFlag {get;set;}
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark {get;set;}
    }
}