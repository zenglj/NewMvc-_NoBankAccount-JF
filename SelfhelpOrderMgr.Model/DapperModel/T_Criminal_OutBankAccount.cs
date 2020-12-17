using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    [Table("T_Criminal_OutBankAccount")]
    public class T_Criminal_OutBankAccount : BaseModel
    {
        [Key]
        public string FCrimecode { get; set; }

        public string OutBankCard { get; set; }

        public string BankUserName { get; set; }

        public string BankOrgName { get; set; }

        public string OpeningBank { get; set; }

        /// <summary>
        /// 开户行号
        /// </summary>
        public string BankCNAPS { get; set; }

        public string CrtBy { get; set; }

        public DateTime CrtDate { get; set; }

        public int Flag { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyTime { get; set; }

        public string OutBankRemark { get; set; }

    }
}