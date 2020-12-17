using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_TmpRcvPayDAL
    {
        /// <summary>
        /// 查询到将要离监用户的名单
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<T_TmpRcvPay> GetTmpRcvPay( DateTime startDate, DateTime endDate, string rcvpay)
        {

            string strSql = @"exec tmprcvPay '" + Convert.ToString(startDate) + "','" + Convert.ToString(endDate) + "','" + rcvpay + "'";
            SqlHelper.ExecuteSql( strSql);

            strSql = @"select * from t_tmpRcvpay order by paydate,accno,dtype";
            DataSet ds = SqlHelper.Query( strSql);
            DataTable dt = ds.Tables[0];

            List<T_TmpRcvPay> list = new List<T_TmpRcvPay>();
            foreach (DataRow row in dt.Rows)
            {
                T_TmpRcvPay op = SetLeavePrison(row);//设定用户记录的值
                list.Add(op);
            }
            return list;
        }



        /// <summary>
        /// 根据数据行,设定离监用户记录的值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static T_TmpRcvPay SetLeavePrison(DataRow row)
        {
            T_TmpRcvPay op = new T_TmpRcvPay();
            op.BankName = (string)row["BankName"];
            op.AccNo = (string)row["AccNo"];
            op.Dtype = (string)row["Dtype"];
            op.paydate = Convert.ToDateTime((row["paydate"]).ToString());
            op.Fmoney = (decimal)SqlHelper.FromDbNull(row["Fmoney"]);
            op.SucMoney = (decimal)SqlHelper.FromDbNull(row["SucMoney"]);
            op.ErrMoney = (decimal)SqlHelper.FromDbNull(row["ErrMoney"]);
            op.Remark = (string)row["Remark"];


            return op;
        }

    }
}