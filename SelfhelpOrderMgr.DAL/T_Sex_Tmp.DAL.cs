﻿using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Sex_Tmp
		public partial class T_Sex_TmpDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Sex_Tmp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Sex_Tmp(");			
            strSql.Append("Fcode,FName");
			strSql.Append(") values (");
            strSql.Append("@Fcode,@FName");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@Fcode", SqlDbType.VarChar,5) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,10)             
              
            };
			            
            parameters[0].Value = model.Fcode;                        
            parameters[1].Value = model.FName;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Sex_Tmp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Sex_Tmp set ");
			                        
            strSql.Append(" Fcode = @Fcode , ");                                    
            strSql.Append(" FName = @FName  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@Fcode", SqlDbType.VarChar,5) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,10)             
              
            };
						            
            parameters[0].Value = model.Fcode;                        
            parameters[1].Value = model.FName;                        
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
			strSql.Append("delete from T_Sex_Tmp ");
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
		public SelfhelpOrderMgr.Model.T_Sex_Tmp GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Fcode, FName  ");			
			strSql.Append("  from T_Sex_Tmp ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_Sex_Tmp model=new SelfhelpOrderMgr.Model.T_Sex_Tmp();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.Fcode= ds.Tables[0].Rows[0]["Fcode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																										
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
			strSql.Append("select Fcode, FName  ");
			strSql.Append(" FROM T_Sex_Tmp ");
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
			strSql.Append(" Fcode, FName  ");
			strSql.Append(" FROM T_Sex_Tmp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

