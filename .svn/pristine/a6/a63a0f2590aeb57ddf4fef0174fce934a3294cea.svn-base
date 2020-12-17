using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Course
		public partial class T_CourseDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Course model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Course(");			
            strSql.Append("ID,FName,FAccess,FInterfaceMethods,BankID,BankAccNo,Remark");
			strSql.Append(") values (");
            strSql.Append("@ID,@FName,@FAccess,@FInterfaceMethods,@BankID,@BankAccNo,@Remark");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FAccess", SqlDbType.Int,4) ,            
                        new SqlParameter("@FInterfaceMethods", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BankID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FAccess;                        
            parameters[3].Value = model.FInterfaceMethods;                        
            parameters[4].Value = model.BankID;                        
            parameters[5].Value = model.BankAccNo;                        
            parameters[6].Value = model.Remark;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Course model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Course set ");
			                        
            strSql.Append(" ID = @ID , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" FAccess = @FAccess , ");                                    
            strSql.Append(" FInterfaceMethods = @FInterfaceMethods , ");                                    
            strSql.Append(" BankID = @BankID , ");                                    
            strSql.Append(" BankAccNo = @BankAccNo , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where ID=@ID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FAccess", SqlDbType.Int,4) ,            
                        new SqlParameter("@FInterfaceMethods", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BankID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FAccess;                        
            parameters[3].Value = model.FInterfaceMethods;                        
            parameters[4].Value = model.BankID;                        
            parameters[5].Value = model.BankAccNo;                        
            parameters[6].Value = model.Remark;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Course ");
			strSql.Append(" where ID=@ID ");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = ID;


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
		public SelfhelpOrderMgr.Model.T_Course GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, FName, FAccess, FInterfaceMethods, BankID, BankAccNo, Remark  ");			
			strSql.Append("  from T_Course ");
			strSql.Append(" where ID=@ID ");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = ID;

			
			SelfhelpOrderMgr.Model.T_Course model=new SelfhelpOrderMgr.Model.T_Course();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																												if(ds.Tables[0].Rows[0]["FAccess"].ToString()!="")
				{
					model.FAccess=int.Parse(ds.Tables[0].Rows[0]["FAccess"].ToString());
				}
																																				model.FInterfaceMethods= ds.Tables[0].Rows[0]["FInterfaceMethods"].ToString();
																												if(ds.Tables[0].Rows[0]["BankID"].ToString()!="")
				{
					model.BankID=int.Parse(ds.Tables[0].Rows[0]["BankID"].ToString());
				}
																																				model.BankAccNo= ds.Tables[0].Rows[0]["BankAccNo"].ToString();
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
			strSql.Append("select ID, FName, FAccess, FInterfaceMethods, BankID, BankAccNo, Remark  ");
			strSql.Append(" FROM T_Course ");
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
			strSql.Append(" ID, FName, FAccess, FInterfaceMethods, BankID, BankAccNo, Remark  ");
			strSql.Append(" FROM T_Course ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

