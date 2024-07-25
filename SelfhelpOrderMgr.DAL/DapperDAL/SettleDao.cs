using Dapper;
using SelfhelpOrderMgr.Common.CustomExtend;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.DAL
{
    public class SettleDao:BaseDapperDAL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        private ResultInfo rs = new ResultInfo() { 
            Flag=false,
            ReMsg="未处理",
            DataInfo=null
        };

        public ResultInfo RestoryInPrisonBase(string fcrimecode,MoneyPayMode payMode)
        {//恢复在押
            switch (payMode)
            {
                case MoneyPayMode.Outlets:
                    { 
                        rs= RestoryInPrisonByOutlets(fcrimecode);
                    }break;
                case MoneyPayMode.Cash:
                    {
                        rs=RestoryInPrisonByCash(fcrimecode);
                    }break;
                case MoneyPayMode.TranAccount:
                    {
                        rs = RestoryInPrisonByTran(fcrimecode);
                    }break;
            }
            return rs;
        }

        /// <summary>
        /// 恢复在押，网点结算模式的恢复
        /// </summary>
        /// <param name="fcrimecode">编号</param>
        /// <returns></returns>
        private ResultInfo RestoryInPrisonByOutlets(string fcrimecode)
        {
            string msg = "用户余款已经转账，不能恢复";
            //删除 T_Bank_PaymentRecord 和 T_Bank_PaymentDetail的记录
            var oWhere = new { FCrimeCode = fcrimecode };
            var strJsonWhere = jss.Serialize(oWhere);
            T_Bank_PaymentRecord rec=new T_Bank_PaymentRecord();
            List<T_Bank_PaymentRecord> recs = this.GetModelList<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(strJsonWhere,"Id",10);
            T_SHO_ManagerSet mySet = new T_SHO_ManagerSetDAL().GetModel("JieSuanLuFeiQuXian");
            if(mySet!=null && mySet.MgrValue == "1")
            {
                if (recs.Count == 2)
                {
                    rec = recs[0];
                    if (rec.TranStatus > 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = msg;
                        return rs;
                    }
                }
                else if (recs.Count > 2)
                {
                    rs.Flag = false;
                    rs.ReMsg = $"错误，存在{recs.Count}条的离监付款记录，无法恢复，请以管理员联系";
                    return rs;
                }
                if (recs.Count == 1)
                {
                    rec = recs[0];
                    if (rec.TranStatus > 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = msg;
                        return rs;
                    }
                }
                else
                {

                }
            }
            else
            {
                if (recs.Count == 1)
                {
                    rec = recs[0];
                    if (rec.TranStatus > 0)
                    {
                        rs.Flag = false;
                        rs.ReMsg = msg;
                        return rs;
                    }
                }
                else if (recs.Count > 1)
                {
                    rs.Flag = false;
                    rs.ReMsg = $"错误，存在{recs.Count}条的离监付款记录，无法恢复，请以管理员联系";
                    return rs;
                }
                else
                {

                }
            }
            
            

            //恢复在押
            StringBuilder strSql = new StringBuilder();

            strSql.Append("delete from  T_Bank_PaymentRecord where Id=@Id and FCrimeCode=@fcrimecode;");
            strSql.Append("delete from  T_Bank_PaymentDetail where MainId=@MainId;");
            
            //获取基本的SQL恢复脚本
            GetBaseRestorySQL(strSql);

            dynamic paramInfo = new { fcrimecode = fcrimecode, Id = rec.Id, MainId = rec.Id };
            return ExecRestorySQL(paramInfo, strSql);
        }

        /// <summary>
        /// 恢复在押，现金结算模式的恢复
        /// </summary>
        /// <param name="fcrimecode">编号</param>
        /// <returns></returns>
        private ResultInfo RestoryInPrisonByCash(string fcrimecode)
        {//恢复在押

            string msg = "用户余款已经在ATM机，不能恢复";
            return RestoryInPrisonByNoCard(fcrimecode, msg);
        }

        /// <summary>
        /// 恢复在押，转账支付模式的恢复
        /// </summary>
        /// <param name="fcrimecode">编号</param>
        /// <returns></returns>
        private ResultInfo RestoryInPrisonByTran(string fcrimecode)
        {//恢复在押

            string msg = "用户余款已经转账，不能恢复";
            return RestoryInPrisonByNoCard(fcrimecode, msg);
        }

        /// <summary>
        /// 中银结算卡方式---恢复在押方法
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private ResultInfo RestoryInPrisonByNoCard(string fcrimecode, string msg)
        {
            //删除 T_Bank_PaymentRecord 和 T_Bank_PaymentDetail的记录
            var oWhere = new { FCrimeCode = fcrimecode };
            var strJsonWhere = jss.Serialize(oWhere);
            T_Bank_PaymentRecord rec = this.GetModelFirst<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(strJsonWhere);
            if (rec == null)
            {
                rec = new T_Bank_PaymentRecord();
            }
            else
            {
                if (rec.TranStatus > 0)
                {
                    rs.Flag = false;
                    rs.ReMsg = msg;
                    return rs;
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from  T_Bank_PaymentRecord where Id=@Id and FCrimeCode=@fcrimecode;");
            strSql.Append("delete from  T_Bank_PaymentDetail where MainId=@MainId;");
            GetBaseRestorySQL(strSql);
            
            dynamic paramInfo = new { fcrimecode = fcrimecode, Id = rec.Id, MainId = rec.Id };
            return ExecRestorySQL(paramInfo, strSql);
        }

        /// <summary>
        /// 获取恢复在押基本SQL脚本
        /// </summary>
        /// <param name="strSql"></param>
        private static void GetBaseRestorySQL(StringBuilder strSql)
        {
            strSql.Append("update t_criminal set FFlag=0 where fcode =@fcrimecode;");
            strSql.Append("update T_ICCARD_LIST set FFlag=1 where fcrimecode =@fcrimecode and FFlag=4;");
            strSql.Append("delete T_Vcrd where fcrimecode =@fcrimecode and TypeFlag=52 and seqno in (select a.seqno from t_Vcrd a, t_balanceList b where a.fcrimecode=b.fcrimecode and b.fcrimecode=@fcrimecode and a.DAMOUNT=b.AccPoints and CONVERT(varchar(10), a.CRTDATE,120)=CONVERT(varchar(10), b.CRTDATE,120)); ");
            strSql.Append("delete T_JF_Vcrd where fcrimecode =@fcrimecode and TypeFlag=5 and Id=(select a.Id from T_JF_Vcrd a, t_balanceList b where a.fcrimecode=b.fcrimecode and b.fcrimecode=@fcrimecode and a.CAMOUNT=b.AccPoints and CONVERT(varchar(10), a.CRTDATE,120)=CONVERT(varchar(10), b.CRTDATE,120)); ");
            //strSql.Append("delete t_balanceList where fcrimecode =@fcrimecode;");
            strSql.Append("delete t_Vcrd where fcrimecode =@fcrimecode and typeFlag in(5,6) and Convert(Varchar(10),CrtDate,120)=(select top 1  Convert(varchar(10),CrtDate,120) t_balanceList where fcrimecode =@fcrimecode Order by SeqNo Desc) ;");
            //strSql.Append(@"update t_Criminal_Card set cardflaga=1,amounta=b.a,amountb=b.b,amountc=b.c,AccPoints=b.d from t_Criminal_Card a,(
            //        select fcrimecode,sum(case when acctype=0 then damount-camount else 0 end) A
            //        ,sum(case when acctype=1 then damount-camount else 0 end) B
            //        ,sum(case when acctype=2 then damount-camount else 0 end) C
            //        ,sum(case when acctype=3 then damount-camount else 0 end) D
            //         from t_vcrd where flag=0 and fcrimecode=@fcrimecode group by fcrimecode) b where a.fcrimecode=b.fcrimecode;");

            //删除ATM记录
            strSql.Append(@"delete from T_Bank_PaymentDetail where MainId in(
                    select Id from T_Bank_PaymentRecord a,(
                    select top 1 fcrimecode,(AmountA+AmountB+AmountC) as TranAmount,AtmLuFeiAmount as AtmAmount,crtdate from  t_balanceList where fcrimecode =@fcrimecode Order by SeqNo Desc
                    ) b
                    where a.FCrimeCode=b.fcrimecode and a.Amount=b.TranAmount and CONVERT(varchar(10), a.Crtdate,120)=CONVERT(varchar(10), b.crtdate,120)
                    and a.AuditFlag=0 and isnull(a.TranStatus,0)<=0
                    );");
            strSql.Append(@"delete from T_Bank_PaymentRecord where FCrimeCode=@fcrimecode and Id in(
                select Id from T_Bank_PaymentRecord a,(
                select top 1 fcrimecode,(AmountA+AmountB+AmountC) as TranAmount,AtmLuFeiAmount as AtmAmount,crtdate from  t_balanceList where fcrimecode =@fcrimecode Order by SeqNo Desc
                ) b
                where a.FCrimeCode=b.fcrimecode and a.Amount=b.TranAmount and CONVERT(varchar(10), a.Crtdate,120)=CONVERT(varchar(10), b.crtdate,120)
                and a.AuditFlag=0 and isnull(a.TranStatus,0)<=0
                );");
            //删除转账记录
            strSql.Append(@"delete from T_Bank_PaymentDetail where MainId in(
                select Id from T_Bank_PaymentRecord a,(
                select top 1 fcrimecode,(AmountA+AmountB+AmountC) as TranAmount,AtmLuFeiAmount as AtmAmount,crtdate from  t_balanceList where fcrimecode =@fcrimecode Order by SeqNo Desc
                ) b
                where a.FCrimeCode=b.fcrimecode and a.Amount=b.TranAmount and CONVERT(varchar(10), a.Crtdate,120)=CONVERT(varchar(10), b.crtdate,120)
                and a.PayMode=2 
                and a.AuditFlag=0 and isnull(a.TranStatus,0)<=0
                );");
            strSql.Append(@"delete from T_Bank_PaymentRecord where FCrimeCode=@fcrimecode and Id in(
                select Id from T_Bank_PaymentRecord a,(
                select top 1 fcrimecode,(AmountA+AmountB+AmountC) as TranAmount,AtmLuFeiAmount as AtmAmount,crtdate from  t_balanceList where fcrimecode =@fcrimecode Order by SeqNo Desc
                ) b
                where a.FCrimeCode=b.fcrimecode and a.Amount=b.AtmAmount and CONVERT(varchar(10), a.Crtdate,120)=CONVERT(varchar(10), b.crtdate,120)
                and a.PayMode=1 
                and a.AuditFlag=0 and isnull(a.TranStatus,0)<=0
                );");
            strSql.Append(@"update t_Criminal_Card set cardflaga=1,amounta=b.a,amountb=b.b,amountc=b.c,AccPoints=b.d from t_Criminal_Card a,(
                    select top 1 fcrimecode,amounta as A,amountb as B,amountc+isnull(AtmLuFeiAmount,0) as C ,AccPoints as D from t_balanceList where fcrimecode=@fcrimecode order by Seqno desc) b where a.fcrimecode=b.fcrimecode;");
            strSql.Append("delete t_balanceList where fcrimecode =@fcrimecode and Seqno=(select top 1 Seqno t_balanceList where fcrimecode =@fcrimecode Order by SeqNo Desc);");
        }

        /// <summary>
        /// 执行恢复在押SQL的处理
        /// </summary>
        /// <param name="paramInfo">动态参数信息</param>
        /// <param name="strSql">StringBuilder类型的SQL脚本</param>
        /// <returns></returns>
        private ResultInfo ExecRestorySQL(dynamic paramInfo, StringBuilder strSql)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //param 直接传实体进去就可以
                int _sid = SqlMapper.Execute(conn, strSql.ToString(), paramInfo);//得出自增长的Id

                if (_sid > 0)
                {
                    rs.Flag = true;
                    rs.ReMsg = "OK|恭喜您，恢复成功";
                }
                else
                {
                    rs.ReMsg = "Err|恢复失败";
                }
                return rs;
            }
        }

        

    }
}