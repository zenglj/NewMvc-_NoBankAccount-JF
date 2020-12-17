using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Czy_area
		public partial class T_Czy_areaDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Czy_area model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Czy_area(");			
            strSql.Append("seqno,fcode,fareacode,fflag");
			strSql.Append(") values (");
            strSql.Append("@seqno,@fcode,@fareacode,@fflag");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fflag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.fareacode;                        
            parameters[3].Value = model.fflag;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Czy_area model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Czy_area set ");
			                        
            strSql.Append(" seqno = @seqno , ");                                    
            strSql.Append(" fcode = @fcode , ");                                    
            strSql.Append(" fareacode = @fareacode , ");                                    
            strSql.Append(" fflag = @fflag  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fflag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.fareacode;                        
            parameters[3].Value = model.fflag;                        
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
			strSql.Append("delete from T_Czy_area ");
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
		public SelfhelpOrderMgr.Model.T_Czy_area GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, fcode, fareacode, fflag  ");			
			strSql.Append("  from T_Czy_area ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_Czy_area model=new SelfhelpOrderMgr.Model.T_Czy_area();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.fcode= ds.Tables[0].Rows[0]["fcode"].ToString();
																																model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
																												if(ds.Tables[0].Rows[0]["fflag"].ToString()!="")
				{
					model.fflag=int.Parse(ds.Tables[0].Rows[0]["fflag"].ToString());
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
			strSql.Append("select seqno, fcode, fareacode, fflag  ");
			strSql.Append(" FROM T_Czy_area ");
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
			strSql.Append(" seqno, fcode, fareacode, fflag  ");
			strSql.Append(" FROM T_Czy_area ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

