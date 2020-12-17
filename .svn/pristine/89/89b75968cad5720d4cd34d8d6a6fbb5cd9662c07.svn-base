using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SearchBankAccNo
		public partial class T_SearchBankAccNoDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(SelfhelpOrderMgr.Model.T_SearchBankAccNo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SearchBankAccNo(");			
            strSql.Append("fcrimecode,FName,BankAccNo");
			strSql.Append(") values (");
            strSql.Append("@fcrimecode,@FName,@BankAccNo");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.fcrimecode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.BankAccNo;                        
			   
			object obj = SqlHelper.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                                    
            	return Convert.ToInt64(obj);
                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SearchBankAccNo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SearchBankAccNo set ");
			                                                
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" BankAccNo = @BankAccNo  ");            			
			strSql.Append(" where keyId=@keyId ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@keyId", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,20)             
              
            };
						            
            parameters[0].Value = model.keyId;                        
            parameters[1].Value = model.fcrimecode;                        
            parameters[2].Value = model.FName;                        
            parameters[3].Value = model.BankAccNo;                        
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
		public bool Delete(long keyId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SearchBankAccNo ");
			strSql.Append(" where keyId=@keyId");
						SqlParameter[] parameters = {
					new SqlParameter("@keyId", SqlDbType.BigInt)
			};
			parameters[0].Value = keyId;


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
		public bool DeleteList(string keyIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SearchBankAccNo ");
			strSql.Append(" where ID in ("+keyIdlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_SearchBankAccNo GetModel(long keyId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select keyId, fcrimecode, FName, BankAccNo  ");			
			strSql.Append("  from T_SearchBankAccNo ");
			strSql.Append(" where keyId=@keyId");
						SqlParameter[] parameters = {
					new SqlParameter("@keyId", SqlDbType.BigInt)
			};
			parameters[0].Value = keyId;

			
			SelfhelpOrderMgr.Model.T_SearchBankAccNo model=new SelfhelpOrderMgr.Model.T_SearchBankAccNo();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["keyId"].ToString()!="")
				{
					model.keyId=long.Parse(ds.Tables[0].Rows[0]["keyId"].ToString());
				}
																																				model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																																model.BankAccNo= ds.Tables[0].Rows[0]["BankAccNo"].ToString();
																										
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
			strSql.Append("select keyId, fcrimecode, FName, BankAccNo  ");
			strSql.Append(" FROM T_SearchBankAccNo ");
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
			strSql.Append(" keyId, fcrimecode, FName, BankAccNo  ");
			strSql.Append(" FROM T_SearchBankAccNo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

