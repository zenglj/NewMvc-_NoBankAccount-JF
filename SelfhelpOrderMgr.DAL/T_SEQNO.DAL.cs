using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SEQNO
		public partial class T_SEQNODAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_SEQNO model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SEQNO(");			
            strSql.Append("SEQTYPE,SEQNO");
			strSql.Append(") values (");
            strSql.Append("@SEQTYPE,@SEQNO");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@SEQTYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SEQNO", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.SEQTYPE;                        
            parameters[1].Value = model.SEQNO;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SEQNO model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SEQNO set ");
			                        
            strSql.Append(" SEQNO = @SEQNO  ");
            strSql.Append(" where  SEQTYPE = @SEQTYPE");
						
SqlParameter[] parameters = {
			            new SqlParameter("@SEQTYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SEQNO", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.SEQTYPE;                        
            parameters[1].Value = model.SEQNO;                        
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
			strSql.Append("delete from T_SEQNO ");
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
		public SelfhelpOrderMgr.Model.T_SEQNO GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SEQTYPE, SEQNO  ");			
			strSql.Append("  from T_SEQNO ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_SEQNO model=new SelfhelpOrderMgr.Model.T_SEQNO();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.SEQTYPE= ds.Tables[0].Rows[0]["SEQTYPE"].ToString();
																												if(ds.Tables[0].Rows[0]["SEQNO"].ToString()!="")
				{
					model.SEQNO=int.Parse(ds.Tables[0].Rows[0]["SEQNO"].ToString());
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
			strSql.Append("select SEQTYPE, SEQNO  ");
			strSql.Append(" FROM T_SEQNO ");
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
			strSql.Append(" SEQTYPE, SEQNO  ");
			strSql.Append(" FROM T_SEQNO ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

