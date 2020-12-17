using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_TempAmount_Card
		public partial class T_TempAmount_CardDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_TempAmount_Card model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_TempAmount_Card(");			
            strSql.Append("fcrimecode,fname,fareaName,BankAccNo,amounta,amountb,amountc,fmoney");
			strSql.Append(") values (");
            strSql.Append("@fcrimecode,@fname,@fareaName,@BankAccNo,@amounta,@amountb,@amountc,@fmoney");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amounta", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@amountb", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@amountc", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@fmoney", SqlDbType.Decimal,13)             
              
            };
			            
            parameters[0].Value = model.fcrimecode;                        
            parameters[1].Value = model.fname;                        
            parameters[2].Value = model.fareaName;                        
            parameters[3].Value = model.BankAccNo;                        
            parameters[4].Value = model.amounta;                        
            parameters[5].Value = model.amountb;                        
            parameters[6].Value = model.amountc;                        
            parameters[7].Value = model.fmoney;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TempAmount_Card model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_TempAmount_Card set ");
			                        
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" fname = @fname , ");                                    
            strSql.Append(" fareaName = @fareaName , ");                                    
            strSql.Append(" BankAccNo = @BankAccNo , ");                                    
            strSql.Append(" amounta = @amounta , ");                                    
            strSql.Append(" amountb = @amountb , ");                                    
            strSql.Append(" amountc = @amountc , ");                                    
            strSql.Append(" fmoney = @fmoney  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amounta", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@amountb", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@amountc", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@fmoney", SqlDbType.Decimal,13)             
              
            };
						            
            parameters[0].Value = model.fcrimecode;                        
            parameters[1].Value = model.fname;                        
            parameters[2].Value = model.fareaName;                        
            parameters[3].Value = model.BankAccNo;                        
            parameters[4].Value = model.amounta;                        
            parameters[5].Value = model.amountb;                        
            parameters[6].Value = model.amountc;                        
            parameters[7].Value = model.fmoney;                        
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
		public bool Delete()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_TempAmount_Card ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};


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
		/// 得到一个对象实体
		/// </summary>
		public SelfhelpOrderMgr.Model.T_TempAmount_Card GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select fcrimecode, fname, fareaName, BankAccNo, amounta, amountb, amountc, fmoney  ");			
			strSql.Append("  from T_TempAmount_Card ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_TempAmount_Card model=new SelfhelpOrderMgr.Model.T_TempAmount_Card();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																																model.fname= ds.Tables[0].Rows[0]["fname"].ToString();
																																model.fareaName= ds.Tables[0].Rows[0]["fareaName"].ToString();
																																model.BankAccNo= ds.Tables[0].Rows[0]["BankAccNo"].ToString();
																												if(ds.Tables[0].Rows[0]["amounta"].ToString()!="")
				{
					model.amounta=decimal.Parse(ds.Tables[0].Rows[0]["amounta"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["amountb"].ToString()!="")
				{
					model.amountb=decimal.Parse(ds.Tables[0].Rows[0]["amountb"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["amountc"].ToString()!="")
				{
					model.amountc=decimal.Parse(ds.Tables[0].Rows[0]["amountc"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["fmoney"].ToString()!="")
				{
					model.fmoney=decimal.Parse(ds.Tables[0].Rows[0]["fmoney"].ToString());
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
			strSql.Append("select fcrimecode, fname, fareaName, BankAccNo, amounta, amountb, amountc, fmoney  ");
			strSql.Append(" FROM T_TempAmount_Card ");
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
			strSql.Append(" fcrimecode, fname, fareaName, BankAccNo, amounta, amountb, amountc, fmoney  ");
			strSql.Append(" FROM T_TempAmount_Card ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

