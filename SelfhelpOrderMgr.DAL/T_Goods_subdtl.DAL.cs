using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Goods_subdtl
		public partial class T_Goods_subdtlDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Goods_subdtl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Goods_subdtl(");			
            strSql.Append("GCode,SubGcode,GNum");
			strSql.Append(") values (");
            strSql.Append("@GCode,@SubGcode,@GNum");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SubGcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GNum", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.GCode;                        
            parameters[1].Value = model.SubGcode;                        
            parameters[2].Value = model.GNum;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_Goods_subdtl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Goods_subdtl set ");
			                                                
            strSql.Append(" GCode = @GCode , ");                                    
            strSql.Append(" SubGcode = @SubGcode , ");                                    
            strSql.Append(" GNum = @GNum  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SubGcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GNum", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.GCode;                        
            parameters[2].Value = model.SubGcode;                        
            parameters[3].Value = model.GNum;                        
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
			strSql.Append("delete from T_Goods_subdtl ");
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
			strSql.Append("delete from T_Goods_subdtl ");
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
		public SelfhelpOrderMgr.Model.T_Goods_subdtl GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, GCode, SubGcode, GNum  ");			
			strSql.Append("  from T_Goods_subdtl ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Goods_subdtl model=new SelfhelpOrderMgr.Model.T_Goods_subdtl();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.GCode= ds.Tables[0].Rows[0]["GCode"].ToString();
																																model.SubGcode= ds.Tables[0].Rows[0]["SubGcode"].ToString();
																												if(ds.Tables[0].Rows[0]["GNum"].ToString()!="")
				{
					model.GNum=decimal.Parse(ds.Tables[0].Rows[0]["GNum"].ToString());
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
			strSql.Append("select seqno, GCode, SubGcode, GNum  ");
			strSql.Append(" FROM T_Goods_subdtl ");
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
			strSql.Append(" seqno, GCode, SubGcode, GNum  ");
			strSql.Append(" FROM T_Goods_subdtl ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

