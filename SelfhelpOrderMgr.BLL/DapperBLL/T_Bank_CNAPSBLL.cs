using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.BLL
{
    public class T_Bank_CNAPSBLL:BaseDapperBLL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        /// 按开户行名称查询数据
        /// </summary>
        /// <param name="queryArr"></param>
        /// <returns></returns>
        public PageResult<T_Bank_CNAPS> GetPagelistByBankOpenName(string[] queryArr)
        {
            //string[] queryArr = queryArr.Split(" ");
            string str = "%" + string.Join("%",queryArr).ToString() + "%";
            var strJson = jss.Serialize( new { BankOpenName = str });

            return this.dapperDal.GetPageList<T_Bank_CNAPS, T_Bank_CNAPS>("Id", strJson,1,10);
        }
    }
}