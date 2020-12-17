using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_area_change
		public partial class t_area_changeDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.t_area_change model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_area_change(");			
            strSql.Append("fcrimecode,fcriminal,fareacode,fareaname,fNewAreacode,fNewAreaName,crtby,crtdate,amount,DVOUNO,CVOUNO");
			strSql.Append(") values (");
            strSql.Append("@fcrimecode,@fcriminal,@fareacode,@fareaname,@fNewAreacode,@fNewAreaName,@crtby,@crtdate,@amount,@DVOUNO,@CVOUNO");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaname", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fNewAreacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fNewAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DVOUNO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CVOUNO", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.fcrimecode;                        
            parameters[1].Value = model.fcriminal;                        
            parameters[2].Value = model.fareacode;                        
            parameters[3].Value = model.fareaname;                        
            parameters[4].Value = model.fNewAreacode;                        
            parameters[5].Value = model.fNewAreaName;                        
            parameters[6].Value = model.crtby;                        
            parameters[7].Value = model.crtdate;                        
            parameters[8].Value = model.amount;                        
            parameters[9].Value = model.DVOUNO;                        
            parameters[10].Value = model.CVOUNO;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.t_area_change model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_area_change set ");
			                                                
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" fcriminal = @fcriminal , ");                                    
            strSql.Append(" fareacode = @fareacode , ");                                    
            strSql.Append(" fareaname = @fareaname , ");                                    
            strSql.Append(" fNewAreacode = @fNewAreacode , ");                                    
            strSql.Append(" fNewAreaName = @fNewAreaName , ");                                    
            strSql.Append(" crtby = @crtby , ");                                    
            strSql.Append(" crtdate = @crtdate , ");                                    
            strSql.Append(" amount = @amount , ");                                    
            strSql.Append(" DVOUNO = @DVOUNO , ");                                    
            strSql.Append(" CVOUNO = @CVOUNO  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaname", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fNewAreacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fNewAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DVOUNO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CVOUNO", SqlDbType.VarChar,20)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fcrimecode;                        
            parameters[2].Value = model.fcriminal;                        
            parameters[3].Value = model.fareacode;                        
            parameters[4].Value = model.fareaname;                        
            parameters[5].Value = model.fNewAreacode;                        
            parameters[6].Value = model.fNewAreaName;                        
            parameters[7].Value = model.crtby;                        
            parameters[8].Value = model.crtdate;                        
            parameters[9].Value = model.amount;                        
            parameters[10].Value = model.DVOUNO;                        
            parameters[11].Value = model.CVOUNO;                        
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
			strSql.Append("delete from t_area_change ");
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
			strSql.Append("delete from t_area_change ");
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
		public SelfhelpOrderMgr.Model.t_area_change GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, fcrimecode, fcriminal, fareacode, fareaname, fNewAreacode, fNewAreaName, crtby, crtdate, amount, DVOUNO, CVOUNO  ");			
			strSql.Append("  from t_area_change ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.t_area_change model=new SelfhelpOrderMgr.Model.t_area_change();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																																model.fcriminal= ds.Tables[0].Rows[0]["fcriminal"].ToString();
																																model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
																																model.fareaname= ds.Tables[0].Rows[0]["fareaname"].ToString();
																																model.fNewAreacode= ds.Tables[0].Rows[0]["fNewAreacode"].ToString();
																																model.fNewAreaName= ds.Tables[0].Rows[0]["fNewAreaName"].ToString();
																																model.crtby= ds.Tables[0].Rows[0]["crtby"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(ds.Tables[0].Rows[0]["crtdate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
				}
																																				model.DVOUNO= ds.Tables[0].Rows[0]["DVOUNO"].ToString();
																																model.CVOUNO= ds.Tables[0].Rows[0]["CVOUNO"].ToString();
																										
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
			strSql.Append("select seqno, fcrimecode, fcriminal, fareacode, fareaname, fNewAreacode, fNewAreaName, crtby, crtdate, amount, DVOUNO, CVOUNO  ");
			strSql.Append(" FROM t_area_change ");
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
			strSql.Append(" seqno, fcrimecode, fcriminal, fareacode, fareaname, fNewAreacode, fNewAreaName, crtby, crtdate, amount, DVOUNO, CVOUNO  ");
			strSql.Append(" FROM t_area_change ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

