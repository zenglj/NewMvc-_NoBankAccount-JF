﻿using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_EdiListDAL
    {
        /// <summary>
        /// 改变T_ediList总单的状态，如为成功，或是失败，一般是用于处理失败后的复位处理
        /// </summary>
        /// <param name="op"></param>
        public int UpdateByMainSeqno( int mainSeqno, int succFlag, int resetFlag)
        {
            try
            {
                //更新三个表t_edilist,t_ediDetail,t_vcrd，相关记录
                SqlHelper.ExecuteSql( @"UPDATE [t_EdiList]
                                           SET [Succflag] = @Succflag
                                              ,[resetflag] = @resetflag
                                         WHERE MainSeqno=@MainSeqno;
                                     update t_edidetail set SuccFlag=-2,Remark='手工复位'+Remark where mainseqno=@MainSeqno;
                                     update t_vcrd set bankflag=0 where bankflag=1
                                        and flag=0 and seqno in(
                                        select vcrdseqno from t_edidetail where mainseqno=@MainSeqno
                                        );",
                                                   new SqlParameter("Succflag", SqlHelper.ToDbNull(succFlag)),
                                                   new SqlParameter("resetflag", SqlHelper.ToDbNull(resetFlag)),
                                                   new SqlParameter("MainSeqno", SqlHelper.ToDbNull(mainSeqno))
                                                   );
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<T_EdiMainOrder> GetAll()
        {
            DataSet ds = SqlHelper.Query( @"update t_EdiList set succflag=1 where DetailDownloadflag=1 and mainFlag=0;
                    select isnull(MainSeqno,'') mainseqno,isnull(remark,'') remark,UploadDate,case DetailDownloadflag* succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end SuccFlag
                        ,case isnull(resetflag,0) when 1 then '已复位' else '' end resetflag
                         from t_EdiList
                        where code<>'yhyewj'
                        order by MainSeqno desc");
            DataTable dt = ds.Tables[0];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{ 

            //}
            List<T_EdiMainOrder> list = new List<T_EdiMainOrder>();
            foreach (DataRow row in dt.Rows)
            {
                T_EdiMainOrder op = SetAreaInfo(row);//设定用户记录的值
                list.Add(op);
            }
            return list;
        }

        public IEnumerable<T_EdiMainOrder> GetListByMainSeqno( int mainSeqno)
        {
            DataSet ds = SqlHelper.Query( @"select isnull(MainSeqno,'') mainseqno,isnull(remark,'') remark,UploadDate,case DetailDownloadflag* succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end SuccFlag
                        ,case isnull(resetflag,0) when 1 then '已复位' else '' end resetflag
                         from t_EdiList
                        where code<>'yhyewj' and MainSeqno=@MainSeqno
                        order by MainSeqno desc",
                        new SqlParameter("@MainSeqno", mainSeqno)
                );

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<T_EdiMainOrder> list = new List<T_EdiMainOrder>();
                foreach (DataRow row in dt.Rows)
                {
                    T_EdiMainOrder op = SetAreaInfo(row);//设定用户记录的值
                    list.Add(op);
                }
                return list;
            }
        }

        /// <summary>
        /// 按登录用户的Code查询
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public IEnumerable<T_EdiMainOrder> GetListByDate(DateTime startDate, DateTime endDate, string rcvpay,string succflag,string remark)
        {
            string strSql = @"update t_EdiList set succflag=1 where DetailDownloadflag=1 and mainFlag=0;
                    select isnull(MainSeqno,'') mainseqno,isnull(remark,'') remark,DataDate UploadDate,case DetailDownloadflag* succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end SuccFlag
                        ,case isnull(resetflag,0) when 1 then '已复位' else '' end resetflag
                         from t_EdiList
                        where code<>'yhyewj' and DataDate>=@startDate and DataDate<@endDate";
            if (rcvpay == "pay")
            {
                strSql = strSql + " and code='pldfwj'";
            }
            else if (rcvpay == "rcv")
            {
                strSql = strSql + " and code='pldswj'";
            }

            if (string.IsNullOrEmpty(succflag) == false)
            {
                strSql = strSql + " and succflag='" + succflag + "'";
            }

            if (string.IsNullOrEmpty(remark) == false)
            {
                strSql = strSql + " and remark like '%" + remark + "%'";
            }
            
            strSql=strSql+" order by MainSeqno desc";
            DataSet ds = SqlHelper.Query( strSql,
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
                );

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<T_EdiMainOrder> list = new List<T_EdiMainOrder>();
                foreach (DataRow row in dt.Rows)
                {
                    T_EdiMainOrder op = SetAreaInfo(row);//设定用户记录的值
                    list.Add(op);
                }
                return list;
            }
        }


        /// <summary>
        /// 根据数据行,设定用户T_CZY记录的值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static T_EdiMainOrder SetAreaInfo(DataRow row)
        {
            T_EdiMainOrder op = new T_EdiMainOrder();

            op.MainSeqno = (string)Convert.ToString(SqlHelper.FromDbNull(row["MainSeqno"]));
            op.Remark = (string)SqlHelper.FromDbNull(row["Remark"]);
            op.UploadDate = (string)Convert.ToString(SqlHelper.FromDbNull(row["UploadDate"]));
            op.SuccFlag = (string)Convert.ToString(SqlHelper.FromDbNull(row["SuccFlag"]));
            op.ResetFlag = (string)SqlHelper.FromDbNull(row["ResetFlag"]);

            return op;
        }
    }
}