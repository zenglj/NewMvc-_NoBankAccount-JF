using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web
{

    public class bonusInfo
    {
        public string strDType { get; set; }//类型
        public int iTypeFlag { get; set; }//类型编码
        public int? iSubTypeFlag { get; set; }//字类型编码

        public string strApplyBy { get; set; }//申请人
        public string strFAreaName { get; set; }//队别名称
        public string strFAreaCode { get; set; }//队别编号
        public string strFYear { get; set; }//年
        public string strFMonth { get; set; }//月 
        public string strRemark { get; set; }//备注
        public string strCrtDate { get; set; }
    }

    /// <summary>
    /// 报酬Excel
    /// </summary>
    /// <typeparam name="T">报酬主单,如：T_BONUS</typeparam>
    /// <typeparam name="DTL">报酬明细,如：T_BONUSDTL</typeparam>
    public class rtnLaobaoExcel<T,DTL>
    {
        public T bonus { get; set; }
        public List<DTL> dtls { get; set; }
    }

    public class ReqAddMainOrderInfo
    {
        public string strFAreaName { get; set; }
        public string strFYear { get; set; }//年
        public string strFMonth { get; set; }//月 
    }
}