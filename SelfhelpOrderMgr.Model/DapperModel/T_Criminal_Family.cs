
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Criminal_Family:BaseModel
    {
        [System.ComponentModel.Description("狱政编号")]
        public string FCrimeCode { get; set; }

        [System.ComponentModel.Description("身份证号")]
        public string FIdenNo { get; set; }

        [System.ComponentModel.Description("家属姓名")]
        public string FamilyName { get; set; }

        [System.ComponentModel.Description("性别")]
        public string FSex { get; set; }

        [System.ComponentModel.Description("关系")]
        public string Relation { get; set; }

        [System.ComponentModel.Description("授权码")]
        public string UserAuthCode { get; set; }

        [System.ComponentModel.Description("手机号")]
        public string PhoneNum { get; set; }

        [System.ComponentModel.Description("地址")]
        public string FAddress { get; set; }

        [System.ComponentModel.Description("银行卡号")]
        public string BankCard { get; set; }

        [System.ComponentModel.Description("罪名姓名")]
        public string OpeningBank { get; set; }

        [System.ComponentModel.Description("建档员")]
        public string CrtBy { get; set; }

        [System.ComponentModel.Description("建档时间")]
        public DateTime CrtDate { get; set; }
        [System.ComponentModel.Description("修改员")]
        public string ModBy { get; set; }

        [System.ComponentModel.Description("修改时间")]
        public DateTime? ModDate { get; set; }

        [System.ComponentModel.Description("备注")]
        public string Remark { get; set; }
    }
}