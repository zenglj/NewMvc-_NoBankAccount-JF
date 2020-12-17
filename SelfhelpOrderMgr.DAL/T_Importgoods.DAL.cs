using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Importgoods
		public partial class T_ImportgoodsDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Importgoods model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Importgoods(");			
            strSql.Append("pc,gtypename,gname,gtxm,Remark,crtdate");
			strSql.Append(") values (");
            strSql.Append("@pc,@gtypename,@gname,@gtxm,@Remark,@crtdate");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@gtypename", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@gname", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@gtxm", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.pc;                        
            parameters[1].Value = model.gtypename;                        
            parameters[2].Value = model.gname;                        
            parameters[3].Value = model.gtxm;                        
            parameters[4].Value = model.Remark;                        
            parameters[5].Value = model.crtdate;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_Importgoods model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Importgoods set ");
			                                                
            strSql.Append(" pc = @pc , ");                                    
            strSql.Append(" gtypename = @gtypename , ");                                    
            strSql.Append(" gname = @gname , ");                                    
            strSql.Append(" gtxm = @gtxm , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" crtdate = @crtdate  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@gtypename", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@gname", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@gtxm", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.pc;                        
            parameters[2].Value = model.gtypename;                        
            parameters[3].Value = model.gname;                        
            parameters[4].Value = model.gtxm;                        
            parameters[5].Value = model.Remark;                        
            parameters[6].Value = model.crtdate;                        
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
			strSql.Append("delete from T_Importgoods ");
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
			strSql.Append("delete from T_Importgoods ");
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
		public SelfhelpOrderMgr.Model.T_Importgoods GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, pc, gtypename, gname, gtxm, Remark, crtdate  ");			
			strSql.Append("  from T_Importgoods ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Importgoods model=new SelfhelpOrderMgr.Model.T_Importgoods();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["pc"].ToString()!="")
				{
					model.pc=int.Parse(ds.Tables[0].Rows[0]["pc"].ToString());
				}
																																				model.gtypename= ds.Tables[0].Rows[0]["gtypename"].ToString();
																																model.gname= ds.Tables[0].Rows[0]["gname"].ToString();
																																model.gtxm= ds.Tables[0].Rows[0]["gtxm"].ToString();
																																model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(ds.Tables[0].Rows[0]["crtdate"].ToString());
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
			strSql.Append("select seqno, pc, gtypename, gname, gtxm, Remark, crtdate  ");
			strSql.Append(" FROM T_Importgoods ");
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
			strSql.Append(" seqno, pc, gtypename, gname, gtxm, Remark, crtdate  ");
			strSql.Append(" FROM T_Importgoods ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

