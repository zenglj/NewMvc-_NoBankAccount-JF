﻿using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using Dapper;
using SelfhelpOrderMgr.Model; 
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SHO_ManagerSet
		public partial class T_SHO_ManagerSetDAL
	{
   		     
		public bool Exists(string KeyName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SHO_ManagerSet");
			strSql.Append(" where ");
			                                       strSql.Append(" KeyName = @KeyName  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.VarChar,50)			};
			parameters[0].Value = KeyName;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_SHO_ManagerSet model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SHO_ManagerSet(");			
            strSql.Append("KeyName,KeyMode,MgrName,MgrValue,StartTime,Remark");
			strSql.Append(") values (");
            strSql.Append("@KeyName,@KeyMode,@MgrName,@MgrValue,@StartTime,@Remark");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@KeyName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@KeyMode", SqlDbType.Int,4) ,            
                        new SqlParameter("@MgrName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MgrValue", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StartTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100)             
              
            };
			            
            parameters[0].Value = model.KeyName;                        
            parameters[1].Value = model.KeyMode;                        
            parameters[2].Value = model.MgrName;                        
            parameters[3].Value = model.MgrValue;
            parameters[4].Value = model.StartTime;                        
            parameters[5].Value = model.Remark;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_ManagerSet model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SHO_ManagerSet set ");
			                        
            strSql.Append(" KeyName = @KeyName , ");                                    
            strSql.Append(" KeyMode = @KeyMode , ");                                    
            strSql.Append(" MgrName = @MgrName , ");                                    
            strSql.Append(" MgrValue = @MgrValue , ");                                    
            strSql.Append(" StartTime = @StartTime , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where KeyName=@KeyName  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@KeyName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@KeyMode", SqlDbType.Int,4) ,            
                        new SqlParameter("@MgrName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MgrValue", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StartTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100)             
              
            };
						            
            parameters[0].Value = model.KeyName;                        
            parameters[1].Value = model.KeyMode;                        
            parameters[2].Value = model.MgrName;                        
            parameters[3].Value = model.MgrValue;                        
            parameters[4].Value = model.StartTime;                        
            parameters[5].Value = model.Remark;                        
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
		public bool Delete(string KeyName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SHO_ManagerSet ");
			strSql.Append(" where KeyName=@KeyName ");
						SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.VarChar,50)			};
			parameters[0].Value = KeyName;


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
		public SelfhelpOrderMgr.Model.T_SHO_ManagerSet GetModel(string KeyName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select KeyName, KeyMode, MgrName, MgrValue, StartTime, Remark  ");			
			strSql.Append("  from T_SHO_ManagerSet ");
			strSql.Append(" where KeyName=@KeyName ");
						SqlParameter[] parameters = {
					new SqlParameter("@KeyName", SqlDbType.VarChar,50)			};
			parameters[0].Value = KeyName;

			
			SelfhelpOrderMgr.Model.T_SHO_ManagerSet model=new SelfhelpOrderMgr.Model.T_SHO_ManagerSet();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.KeyName= ds.Tables[0].Rows[0]["KeyName"].ToString();
																												if(ds.Tables[0].Rows[0]["KeyMode"].ToString()!="")
				{
					model.KeyMode=int.Parse(ds.Tables[0].Rows[0]["KeyMode"].ToString());
				}
																																				model.MgrName= ds.Tables[0].Rows[0]["MgrName"].ToString();
																																model.MgrValue= ds.Tables[0].Rows[0]["MgrValue"].ToString();
																												if(ds.Tables[0].Rows[0]["StartTime"].ToString()!="")
				{
					model.StartTime=DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
																																				model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																										
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
			strSql.Append("select * ");
			strSql.Append(" FROM T_SHO_ManagerSet ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM T_SHO_ManagerSet ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

