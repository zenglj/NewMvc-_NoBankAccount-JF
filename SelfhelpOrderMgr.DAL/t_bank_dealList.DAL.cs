using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_bank_dealList
		public partial class t_bank_dealListDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.t_bank_dealList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_bank_dealList(");			
            strSql.Append("AccNo,fcrimecode,fName,dcflag,amount,BalAmount,Remark,BankSeqno,BankDealCode,DealDate,LoadDate,flag");
			strSql.Append(") values (");
            strSql.Append("@AccNo,@fcrimecode,@fName,@dcflag,@amount,@BalAmount,@Remark,@BankSeqno,@BankDealCode,@DealDate,@LoadDate,@flag");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@dcflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BalAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@BankSeqno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankDealCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DealDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@LoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.AccNo;                        
            parameters[1].Value = model.fcrimecode;                        
            parameters[2].Value = model.fName;                        
            parameters[3].Value = model.dcflag;                        
            parameters[4].Value = model.amount;                        
            parameters[5].Value = model.BalAmount;                        
            parameters[6].Value = model.Remark;                        
            parameters[7].Value = model.BankSeqno;                        
            parameters[8].Value = model.BankDealCode;                        
            parameters[9].Value = model.DealDate;                        
            parameters[10].Value = model.LoadDate;                        
            parameters[11].Value = model.flag;                        
			   
			object obj = SqlHelper.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bank_dealList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_bank_dealList set ");
			                                                
            strSql.Append(" AccNo = @AccNo , ");                                    
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" fName = @fName , ");                                    
            strSql.Append(" dcflag = @dcflag , ");                                    
            strSql.Append(" amount = @amount , ");                                    
            strSql.Append(" BalAmount = @BalAmount , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" BankSeqno = @BankSeqno , ");                                    
            strSql.Append(" BankDealCode = @BankDealCode , ");                                    
            strSql.Append(" DealDate = @DealDate , ");                                    
            strSql.Append(" LoadDate = @LoadDate , ");                                    
            strSql.Append(" flag = @flag  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@dcflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BalAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@BankSeqno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankDealCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DealDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@LoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.AccNo;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.fName;                        
            parameters[4].Value = model.dcflag;                        
            parameters[5].Value = model.amount;                        
            parameters[6].Value = model.BalAmount;                        
            parameters[7].Value = model.Remark;                        
            parameters[8].Value = model.BankSeqno;                        
            parameters[9].Value = model.BankDealCode;                        
            parameters[10].Value = model.DealDate;                        
            parameters[11].Value = model.LoadDate;                        
            parameters[12].Value = model.flag;                        
            int rows=SqlHelper.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_bank_dealList ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;


			int rows=SqlHelper.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string seqnolist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_bank_dealList ");
			strSql.Append(" where ID in ("+seqnolist + ")  ");
			int rows=SqlHelper.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.t_bank_dealList GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, AccNo, fcrimecode, fName, dcflag, amount, BalAmount, Remark, BankSeqno, BankDealCode, DealDate, LoadDate, flag  ");			
			strSql.Append("  from t_bank_dealList ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.t_bank_dealList model=new SelfhelpOrderMgr.Model.t_bank_dealList();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.AccNo= ds.Tables[0].Rows[0]["AccNo"].ToString();
																																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																																model.fName= ds.Tables[0].Rows[0]["fName"].ToString();
																												if(ds.Tables[0].Rows[0]["dcflag"].ToString()!="")
				{
					model.dcflag=int.Parse(ds.Tables[0].Rows[0]["dcflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["BalAmount"].ToString()!="")
				{
					model.BalAmount=decimal.Parse(ds.Tables[0].Rows[0]["BalAmount"].ToString());
				}
																																				model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																																model.BankSeqno= ds.Tables[0].Rows[0]["BankSeqno"].ToString();
																																model.BankDealCode= ds.Tables[0].Rows[0]["BankDealCode"].ToString();
																												if(ds.Tables[0].Rows[0]["DealDate"].ToString()!="")
				{
					model.DealDate=DateTime.Parse(ds.Tables[0].Rows[0]["DealDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["LoadDate"].ToString()!="")
				{
					model.LoadDate=DateTime.Parse(ds.Tables[0].Rows[0]["LoadDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["flag"].ToString()!="")
				{
					model.flag=int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
				}
																														
				return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, AccNo, fcrimecode, fName, dcflag, amount, BalAmount, Remark, BankSeqno, BankDealCode, DealDate, LoadDate, flag  ");
			strSql.Append(" FROM t_bank_dealList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return SqlHelper.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" seqno, AccNo, fcrimecode, fName, dcflag, amount, BalAmount, Remark, BankSeqno, BankDealCode, DealDate, LoadDate, flag  ");
			strSql.Append(" FROM t_bank_dealList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

