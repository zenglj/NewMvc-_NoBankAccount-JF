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
            string strJson = jss.Serialize( new { BankOpenName = str });

            PageResult<T_Bank_CNAPS> pagelist= this.dapperDal.GetPageList<T_Bank_CNAPS, T_Bank_CNAPS>("Id", strJson,1,10);
            if (pagelist.rows.Count<=0)
            {
                foreach (var item in queryArr)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        strJson = jss.Serialize(new { CNAPS = item });
                        break;
                    }
                }
                pagelist = this.dapperDal.GetPageList<T_Bank_CNAPS, T_Bank_CNAPS>("Id", strJson, 1, 10);
            }
            return pagelist;
        }
    }
}