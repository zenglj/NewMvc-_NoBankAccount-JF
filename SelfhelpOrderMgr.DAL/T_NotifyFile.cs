using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_NotifyFile
		public partial class T_NotifyFileDAL
	{
   		     
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_NotifyFile");
			strSql.Append(" where ");
			                                       strSql.Append(" ID = @ID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_NotifyFile model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_NotifyFile(");			
            strSql.Append("FTitle,FAbstract,FAuthor,FDate,LinkWebFile,Remark");
			strSql.Append(") values (");
            strSql.Append("@FTitle,@FAbstract,@FAuthor,@FDate,@LinkWebFile,@Remark");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FTitle", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@FAbstract", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@FAuthor", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@LinkWebFile", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,300)             
              
            };
			            
            parameters[0].Value = model.FTitle;                        
            parameters[1].Value = model.FAbstract;                        
            parameters[2].Value = model.FAuthor;                        
            parameters[3].Value = model.FDate;                        
            parameters[4].Value = model.LinkWebFile;                        
            parameters[5].Value = model.Remark;                        
			   
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
        public bool Update(SelfhelpOrderMgr.Model.T_NotifyFile model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_NotifyFile set ");
			                                                
            strSql.Append(" FTitle = @FTitle , ");                                    
            strSql.Append(" FAbstract = @FAbstract , ");                                    
            strSql.Append(" FAuthor = @FAuthor , ");                                    
            strSql.Append(" FDate = @FDate , ");                                    
            strSql.Append(" LinkWebFile = @LinkWebFile , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTitle", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@FAbstract", SqlDbType.NVarChar,200) ,            
                        new SqlParameter("@FAuthor", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@LinkWebFile", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,300)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.FTitle;                        
            parameters[2].Value = model.FAbstract;                        
            parameters[3].Value = model.FAuthor;                        
            parameters[4].Value = model.FDate;                        
            parameters[5].Value = model.LinkWebFile;                        
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
			strSql.Append("delete from T_NotifyFile ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_NotifyFile ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_NotifyFile GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, FTitle, FAbstract, FAuthor, FDate, LinkWebFile, Remark  ");			
			strSql.Append("  from T_NotifyFile ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


            SelfhelpOrderMgr.Model.T_NotifyFile model = new SelfhelpOrderMgr.Model.T_NotifyFile();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.FTitle= ds.Tables[0].Rows[0]["FTitle"].ToString();
																																model.FAbstract= ds.Tables[0].Rows[0]["FAbstract"].ToString();
																																model.FAuthor= ds.Tables[0].Rows[0]["FAuthor"].ToString();
																												if(ds.Tables[0].Rows[0]["FDate"].ToString()!="")
				{
					model.FDate=DateTime.Parse(ds.Tables[0].Rows[0]["FDate"].ToString());
				}
																																				model.LinkWebFile= ds.Tables[0].Rows[0]["LinkWebFile"].ToString();
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
			strSql.Append(" FROM T_NotifyFile ");
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
			strSql.Append(" FROM T_NotifyFile ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

