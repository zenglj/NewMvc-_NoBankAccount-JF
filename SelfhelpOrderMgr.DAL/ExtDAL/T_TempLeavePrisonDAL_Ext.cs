using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_TempLeavePrisonDAL
    {
        public IEnumerable<T_TempLeavePrison> GetLeavePrison(string czyCode, string startDate, string endDate, string FAreaCode, string fcode, string fname)
        { 
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select top 200 c.*,isnull((d.AmountA+d.AmountB+d.AmountC),0) JSMoney,e.OutBankCard,e.BankUserName,e.OpeningBank ,d.PayMode,d.CollectMoneyFlag,d.seqno from (
select a.fcode,a.fname,a.foudate,isnull(b.fname,'') fareaName,a.foudate strOutDate
                    ,case isnull(c.CardFlaga,0) when 4 then '已结算' when 2 then '已挂失' when 3 then '已作废' else '未结' end as FStatus,isnull(c.BankAccNo,'') BankCardNo
                    ,c.AmountA,c.AmountB,c.AmountC
                    from t_criminal a left outer join t_area b on a.fareacode=b.fcode left join t_criminal_card c 
                    on  a.Fcode=c.fcrimecode where 1=1 ");
                if(string.IsNullOrEmpty(startDate)==false && string.IsNullOrEmpty(endDate)==false)
                {
                    strSql.Append(" and foudate>=@StartDate and foudate<@EndDate ");
                }
                    
//                strSql.Append(@" and fareacode in (
//	                select fareaCode from t_czy_area where fflag=2 and fcode =@czyCode) ");
                if (string.IsNullOrEmpty(FAreaCode)==false)
                {
                    strSql.Append(" and a.FAreaCode=@FAreaCode ");
                }
                if(string.IsNullOrEmpty(fcode)==false)
                {
                    strSql.Append(" and a.FCode=@FCode ");
                }
                if (string.IsNullOrEmpty(fname) == false)
                {
                    strSql.Append(" and a.fname like @FName ");
                }
                strSql.Append(") c");
                strSql.Append(" left outer join t_balanceList d on c.fcode=d.fcrimecode");
                strSql.Append(" left outer join T_Criminal_OutBankAccount e on c.fcode=e.fcrimecode");


                strSql.Append(" order by c.fareaName,c.foudate;");
                
                conn.Open();
                return conn.Query<T_TempLeavePrison>(strSql.ToString(), new { FCode = fcode, FName = "%"+fname+"%", FAreaCode=FAreaCode,StartDate=startDate,EndDate=endDate, czyCode = czyCode });
            }
        }

        public string ExcuteStoredProcedure( string fcrimecode, string crtby)
        {
            string strReturnResult = "";//返回结果
            string connstr = SqlHelper.getConnstr();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("P_OutPrisonJieSuan", conn))
                {
                    //cmd = new SqlCommand("P_OutPrisonJieSuan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FCrimeCode", fcrimecode);　　//给输入参数赋值
                    cmd.Parameters.AddWithValue("@CrtBy", crtby);　　//给输入参数赋值
                    SqlParameter parOutput = cmd.Parameters.Add("@ResultFlag", SqlDbType.Int);　　//定义输出参数
                    parOutput.Direction = ParameterDirection.Output;　　//参数类型为Output
                    SqlParameter parReturn = new SqlParameter("@return", SqlDbType.Int);
                    parReturn.Direction = ParameterDirection.ReturnValue; 　　//参数类型为ReturnValue                    cmd.Parameters.Add(parReturn);

                    cmd.ExecuteNonQuery();
                    //int RtnResult = Convert.ToInt32(parReturn.Value.ToString());
                    int rtnFlag = Convert.ToInt32(parOutput.Value.ToString());
                    if (rtnFlag >= 5)
                    {
                        strReturnResult = "OK|Success";
                    }
                    else
                    {
                        switch (rtnFlag)
                        {
                            case 0:
                                {
                                    strReturnResult = "Error|0未执行";
                                } break;
                            case 1:
                                {
                                    strReturnResult = "Error|1只生代付";
                                } break;
                            case 2:
                                {
                                    strReturnResult = "Error|2只停用IC卡";
                                } break;
                            case 3:
                                {
                                    strReturnResult = "Error|3已生Vcrd记录";
                                } break;
                            case 4:
                                {
                                    strReturnResult = "Error|4未清空余额";
                                } break;
                            default:
                                break;
                        }
                    }
                    //MessageBox.Show(parOutput.Value.ToString());   //显示输出参数的值
                    //MessageBox.Show(parReturn.Value.ToString());　　//显示返回值
                }
            }
            return strReturnResult;
        }


        public string ExcuteStoredProc_NoBankCard(string fcrimecode, string crtby, int payMode)
        {
            string strReturnResult = "";//返回结果
            string connstr = SqlHelper.getConnstr();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("P_OutPrisonJieSuan_TranAccount", conn))
                {
                    //cmd = new SqlCommand("P_OutPrisonJieSuan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FCrimeCode", fcrimecode);　　//给输入参数赋值
                    cmd.Parameters.AddWithValue("@CrtBy", crtby);　　//给输入参数赋值
                    cmd.Parameters.AddWithValue("@payMode", payMode);　　//给输入参数赋值
                    SqlParameter parOutput = cmd.Parameters.Add("@ResultFlag", SqlDbType.Int);　　//定义输出参数
                    parOutput.Direction = ParameterDirection.Output;　　//参数类型为Output
                    SqlParameter parReturn = new SqlParameter("@return", SqlDbType.Int);
                    parReturn.Direction = ParameterDirection.ReturnValue; 　　//参数类型为ReturnValue                    cmd.Parameters.Add(parReturn);

                    cmd.ExecuteNonQuery();
                    //int RtnResult = Convert.ToInt32(parReturn.Value.ToString());
                    int rtnFlag = Convert.ToInt32(parOutput.Value.ToString());
                    if (rtnFlag >= 5)
                    {
                        strReturnResult = "OK|Success";
                    }
                    else
                    {
                        switch (rtnFlag)
                        {
                            case -2:
                                {
                                    strReturnResult = "Error|0 结算参数不正确";
                                } break;
                            case -1:
                                {
                                    strReturnResult = "Error|-1 没有正确的结算银行卡";
                                } break;
                            case 0:
                                {
                                    strReturnResult = "Error|0未执行";
                                } break;
                            case 1:
                                {
                                    strReturnResult = "Error|1只生代付";
                                } break;
                            case 2:
                                {
                                    strReturnResult = "Error|2只停用IC卡";
                                } break;
                            case 3:
                                {
                                    strReturnResult = "Error|3已生Vcrd记录";
                                } break;
                            case 4:
                                {
                                    strReturnResult = "Error|4未清空余额";
                                } break;
                            case 5:
                                {
                                    strReturnResult = "Error|6结算模式不存在";
                                } break;
                            default:
                                break;
                        }
                    }
                    //MessageBox.Show(parOutput.Value.ToString());   //显示输出参数的值
                    //MessageBox.Show(parReturn.Value.ToString());　　//显示返回值
                }
            }
            return strReturnResult;
        }

        public string InsertBankProve(string fcode,int payMode)
        {
            try
            {
                string strSql = @"delete from t_BankProve";
                SqlHelper.ExecuteSql( strSql);

                strSql = @"insert into t_BankProve (fcode,fname,foudate,FIdenNo,fareaname,BankCode,CardMoney,CrtDate)
                            select a.fcode,a.fname,a.foudate,a.FIdenNo,b.fname fareaName,c.bankaccno BankCode,d.CardMoney,d.CrtDate 
                            from t_criminal a,t_area b,t_criminal_card c";
                if (payMode != 3)
                {
                    strSql += @" ,(select top 1 fcrimecode,(AmountA+AmountB+AmountC) as CardMoney,CrtDate from t_balanceList
                            where fcrimecode=@fcode
                             order by seqno desc) d ";
                }
                else
                {
                    strSql += @" ,(select top 1 fcrimecode,(AmountA+AmountB+AmountC) as CardMoney,getdate() as CrtDate from T_Criminal_Card
                            where fcrimecode=@fcode) d ";
                }

                strSql += @" where a.fareacode=b.fcode and a.fcode=c.fcrimecode and a.fcode=d.fcrimecode 
                             and a.fcode=@fcode";

                if (payMode != 3)
                {
                    strSql += @" and isnull(FFlag,0)=1 ";
                }
//                strSql = @"insert into t_BankProve (fcode,fname,foudate,FIdenNo,fareaname,BankCode,CardMoney,CrtDate)
//                            select a.fcode,a.fname,a.foudate,a.FIdenNo,b.fname fareaName,c.bankaccno BankCode,d.CardMoney,d.CrtDate 
//                            from t_criminal a,t_area b,t_criminal_card c ,
//                            (select top 1 fcrimecode,(AmountA+AmountB+AmountC) as CardMoney,CrtDate from t_balanceList
//                            where fcrimecode=@fcode
//                             order by seqno desc) d
//                            where a.fareacode=b.fcode and a.fcode=c.fcrimecode and a.fcode=d.fcrimecode 
//                            and isnull(FFlag,0)=1 and a.fcode=@fcode";

                SqlHelper.ExecuteSql( strSql,
                    new SqlParameter("fcode", fcode));

                return "OK|InsertOK";
            }
            catch
            {
                return "Err|InsertErr";
            }
        }

        //挂失并插入结算记录
        public string SetLossAndInsertBankProve(string fcode)
        {
            try
            {
                string strSql = @"delete from t_BankProve";
                SqlHelper.ExecuteSql(strSql);


                //                strSql = @"insert into t_BankProve (fcode,fname,foudate,FIdenNo,fareaname,BankCode) select a.fcode,a.fname,a.foudate,a.FIdenNo,b.fname fareaName,c.bankaccno BankCode from t_criminal a,t_area b,t_criminal_card c 
                //                            where a.fareacode=b.fcode and a.fcode=c.fcrimecode and isnull(FFlag,0)=0 and a.fcode=@fcode";
                strSql = @"update t_criminal_card set CardFlaga=2 where fcrimecode=@fcode;";
                strSql =strSql+ @" update t_IcCard_list set FFlag=2 where fcrimecode=@fcode and FFlag=1;";
                strSql = strSql + @"insert into t_BankProve (fcode,fname,foudate,FIdenNo,fareaname,BankCode,CardMoney,CrtDate)
                            select a.fcode,a.fname,a.foudate,a.FIdenNo,b.fname fareaName,c.bankaccno BankCode,d.CardMoney,d.CrtDate 
                            from t_criminal a,t_area b,t_criminal_card c ,
                            (select top 1 fcrimecode,(AmountA+AmountB+AmountC) as CardMoney,getDate() CrtDate from t_criminal_card
                            where fcrimecode=@fcode
                             order by seqno desc) d
                            where a.fareacode=b.fcode and a.fcode=c.fcrimecode and a.fcode=d.fcrimecode 
                            and a.fcode=@fcode;";

                SqlHelper.ExecuteSql(strSql,
                    new SqlParameter("fcode", fcode));



                return "OK|InsertOK";
            }
            catch
            {
                return "Err|InsertErr";
            }
        }
    }
}