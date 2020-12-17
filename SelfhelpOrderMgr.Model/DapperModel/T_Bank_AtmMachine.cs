using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_AtmMachine:BaseModel
    {
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 机器余额
        /// </summary>
        public decimal MachineBalance { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddr { get; set; }
        /// <summary>
        /// ATM机流水号
        /// </summary>
        public string AtmSerialNo { get; set; }

        /// <summary>
        /// 对账日期
        /// </summary>
        public DateTime? ReconciliationDate { get; set; }

        /// <summary>
        /// 机器的状态：-1异常
        /// </summary>     
        public int StatusFlag { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Key的密码
        /// </summary>
        public string Pwd { get; set; }
    }
}