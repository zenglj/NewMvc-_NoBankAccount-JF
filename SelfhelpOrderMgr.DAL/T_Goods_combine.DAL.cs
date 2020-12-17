using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Goods_combine
		public partial class T_Goods_combineDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Goods_combine model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Goods_combine(");			
            strSql.Append("ParGcode,ParGTXM,SubGcode,SubGTXM,Qty");
			strSql.Append(") values (");
            strSql.Append("@ParGcode,@ParGTXM,@SubGcode,@SubGTXM,@Qty");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@ParGcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ParGTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SubGcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SubGTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Qty", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.ParGcode;                        
            parameters[1].Value = model.ParGTXM;                        
            parameters[2].Value = model.SubGcode;                        
            parameters[3].Value = model.SubGTXM;                        
            parameters[4].Value = model.Qty;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_Goods_combine model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Goods_combine set ");
			                                                
            strSql.Append(" ParGcode = @ParGcode , ");                                    
            strSql.Append(" ParGTXM = @ParGTXM , ");                                    
            strSql.Append(" SubGcode = @SubGcode , ");                                    
            strSql.Append(" SubGTXM = @SubGTXM , ");                                    
            strSql.Append(" Qty = @Qty  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParGcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ParGTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SubGcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SubGTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Qty", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.ParGcode;                        
            parameters[2].Value = model.ParGTXM;                        
            parameters[3].Value = model.SubGcode;                        
            parameters[4].Value = model.SubGTXM;                        
            parameters[5].Value = model.Qty;                        
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
			strSql.Append("delete from T_Goods_combine ");
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
			strSql.Append("delete from T_Goods_combine ");
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
		public SelfhelpOrderMgr.Model.T_Goods_combine GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, ParGcode, ParGTXM, SubGcode, SubGTXM, Qty  ");			
			strSql.Append("  from T_Goods_combine ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Goods_combine model=new SelfhelpOrderMgr.Model.T_Goods_combine();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.ParGcode= ds.Tables[0].Rows[0]["ParGcode"].ToString();
																																model.ParGTXM= ds.Tables[0].Rows[0]["ParGTXM"].ToString();
																																model.SubGcode= ds.Tables[0].Rows[0]["SubGcode"].ToString();
																																model.SubGTXM= ds.Tables[0].Rows[0]["SubGTXM"].ToString();
																												if(ds.Tables[0].Rows[0]["Qty"].ToString()!="")
				{
					model.Qty=decimal.Parse(ds.Tables[0].Rows[0]["Qty"].ToString());
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
			strSql.Append("select seqno, ParGcode, ParGTXM, SubGcode, SubGTXM, Qty  ");
			strSql.Append(" FROM T_Goods_combine ");
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
			strSql.Append(" seqno, ParGcode, ParGTXM, SubGcode, SubGTXM, Qty  ");
			strSql.Append(" FROM T_Goods_combine ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

