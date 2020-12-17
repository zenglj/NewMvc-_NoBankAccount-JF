using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_FeeList
		public partial class t_FeeListDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.t_FeeList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_FeeList(");			
            strSql.Append("seqno,code,subcode,fname,DCFLAG,Subflag,levelid");
			strSql.Append(") values (");
            strSql.Append("@seqno,@code,@subcode,@fname,@DCFLAG,@Subflag,@levelid");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@code", SqlDbType.Int,4) ,            
                        new SqlParameter("@subcode", SqlDbType.Int,4) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DCFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@Subflag", SqlDbType.Char,1) ,            
                        new SqlParameter("@levelid", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.code;                        
            parameters[2].Value = model.subcode;                        
            parameters[3].Value = model.fname;                        
            parameters[4].Value = model.DCFLAG;                        
            parameters[5].Value = model.Subflag;                        
            parameters[6].Value = model.levelid;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_FeeList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_FeeList set ");
			                        
            strSql.Append(" seqno = @seqno , ");                                    
            strSql.Append(" code = @code , ");                                    
            strSql.Append(" subcode = @subcode , ");                                    
            strSql.Append(" fname = @fname , ");                                    
            strSql.Append(" DCFLAG = @DCFLAG , ");                                    
            strSql.Append(" Subflag = @Subflag , ");                                    
            strSql.Append(" levelid = @levelid  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@code", SqlDbType.Int,4) ,            
                        new SqlParameter("@subcode", SqlDbType.Int,4) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DCFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@Subflag", SqlDbType.Char,1) ,            
                        new SqlParameter("@levelid", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.code;                        
            parameters[2].Value = model.subcode;                        
            parameters[3].Value = model.fname;                        
            parameters[4].Value = model.DCFLAG;                        
            parameters[5].Value = model.Subflag;                        
            parameters[6].Value = model.levelid;                        
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
			strSql.Append("delete from t_FeeList ");
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
		public SelfhelpOrderMgr.Model.t_FeeList GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, code, subcode, fname, DCFLAG, Subflag, levelid  ");			
			strSql.Append("  from t_FeeList ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.t_FeeList model=new SelfhelpOrderMgr.Model.t_FeeList();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["code"].ToString()!="")
				{
					model.code=int.Parse(ds.Tables[0].Rows[0]["code"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["subcode"].ToString()!="")
				{
					model.subcode=int.Parse(ds.Tables[0].Rows[0]["subcode"].ToString());
				}
																																				model.fname= ds.Tables[0].Rows[0]["fname"].ToString();
																												if(ds.Tables[0].Rows[0]["DCFLAG"].ToString()!="")
				{
					model.DCFLAG=int.Parse(ds.Tables[0].Rows[0]["DCFLAG"].ToString());
				}
																																				model.Subflag= ds.Tables[0].Rows[0]["Subflag"].ToString();
																												if(ds.Tables[0].Rows[0]["levelid"].ToString()!="")
				{
					model.levelid=int.Parse(ds.Tables[0].Rows[0]["levelid"].ToString());
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
			strSql.Append("select seqno, code, subcode, fname, DCFLAG, Subflag, levelid  ");
			strSql.Append(" FROM t_FeeList ");
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
			strSql.Append(" seqno, code, subcode, fname, DCFLAG, Subflag, levelid  ");
			strSql.Append(" FROM t_FeeList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

