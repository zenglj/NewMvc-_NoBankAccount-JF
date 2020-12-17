using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_MONEY
		public partial class T_MONEYDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_MONEY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_MONEY(");			
            strSql.Append("FIdenNo,FMoney,FDate,FFlag");
			strSql.Append(") values (");
            strSql.Append("@FIdenNo,@FMoney,@FDate,@FFlag");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FMoney", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.FIdenNo;                        
            parameters[1].Value = model.FMoney;                        
            parameters[2].Value = model.FDate;                        
            parameters[3].Value = model.FFlag;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_MONEY model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_MONEY set ");
			                                                
            strSql.Append(" FIdenNo = @FIdenNo , ");                                    
            strSql.Append(" FMoney = @FMoney , ");                                    
            strSql.Append(" FDate = @FDate , ");                                    
            strSql.Append(" FFlag = @FFlag  ");            			
			strSql.Append(" where FSeq=@FSeq ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FSeq", SqlDbType.Int,4) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FMoney", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.FSeq;                        
            parameters[1].Value = model.FIdenNo;                        
            parameters[2].Value = model.FMoney;                        
            parameters[3].Value = model.FDate;                        
            parameters[4].Value = model.FFlag;                        
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
		public bool Delete(int FSeq)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_MONEY ");
			strSql.Append(" where FSeq=@FSeq");
						SqlParameter[] parameters = {
					new SqlParameter("@FSeq", SqlDbType.Int,4)
			};
			parameters[0].Value = FSeq;


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
		public bool DeleteList(string FSeqlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_MONEY ");
			strSql.Append(" where ID in ("+FSeqlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_MONEY GetModel(int FSeq)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FSeq, FIdenNo, FMoney, FDate, FFlag  ");			
			strSql.Append("  from T_MONEY ");
			strSql.Append(" where FSeq=@FSeq");
						SqlParameter[] parameters = {
					new SqlParameter("@FSeq", SqlDbType.Int,4)
			};
			parameters[0].Value = FSeq;

			
			SelfhelpOrderMgr.Model.T_MONEY model=new SelfhelpOrderMgr.Model.T_MONEY();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["FSeq"].ToString()!="")
				{
					model.FSeq=int.Parse(ds.Tables[0].Rows[0]["FSeq"].ToString());
				}
																																				model.FIdenNo= ds.Tables[0].Rows[0]["FIdenNo"].ToString();
																												if(ds.Tables[0].Rows[0]["FMoney"].ToString()!="")
				{
					model.FMoney=decimal.Parse(ds.Tables[0].Rows[0]["FMoney"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FDate"].ToString()!="")
				{
					model.FDate=DateTime.Parse(ds.Tables[0].Rows[0]["FDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FFlag"].ToString()!="")
				{
					model.FFlag=int.Parse(ds.Tables[0].Rows[0]["FFlag"].ToString());
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
			strSql.Append("select FSeq, FIdenNo, FMoney, FDate, FFlag  ");
			strSql.Append(" FROM T_MONEY ");
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
			strSql.Append(" FSeq, FIdenNo, FMoney, FDate, FFlag  ");
			strSql.Append(" FROM T_MONEY ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

