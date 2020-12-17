using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_GoodsStock
		public partial class T_GoodsStockDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_GoodsStock model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_GoodsStock(");			
            strSql.Append("gcode,gtxm,InDate,Gdj,Balance,TmpBalance");
			strSql.Append(") values (");
            strSql.Append("@gcode,@gtxm,@InDate,@Gdj,@Balance,@TmpBalance");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@gcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@gtxm", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Gdj", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Balance", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@TmpBalance", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.gcode;                        
            parameters[1].Value = model.gtxm;                        
            parameters[2].Value = model.InDate;                        
            parameters[3].Value = model.Gdj;                        
            parameters[4].Value = model.Balance;                        
            parameters[5].Value = model.TmpBalance;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_GoodsStock model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_GoodsStock set ");
			                                                
            strSql.Append(" gcode = @gcode , ");                                    
            strSql.Append(" gtxm = @gtxm , ");                                    
            strSql.Append(" InDate = @InDate , ");                                    
            strSql.Append(" Gdj = @Gdj , ");                                    
            strSql.Append(" Balance = @Balance , ");                                    
            strSql.Append(" TmpBalance = @TmpBalance  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@gcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@gtxm", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Gdj", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Balance", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@TmpBalance", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.gcode;                        
            parameters[2].Value = model.gtxm;                        
            parameters[3].Value = model.InDate;                        
            parameters[4].Value = model.Gdj;                        
            parameters[5].Value = model.Balance;                        
            parameters[6].Value = model.TmpBalance;                        
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
			strSql.Append("delete from T_GoodsStock ");
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
			strSql.Append("delete from T_GoodsStock ");
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
		public SelfhelpOrderMgr.Model.T_GoodsStock GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, gcode, gtxm, InDate, Gdj, Balance, TmpBalance  ");			
			strSql.Append("  from T_GoodsStock ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_GoodsStock model=new SelfhelpOrderMgr.Model.T_GoodsStock();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.gcode= ds.Tables[0].Rows[0]["gcode"].ToString();
																																model.gtxm= ds.Tables[0].Rows[0]["gtxm"].ToString();
																												if(ds.Tables[0].Rows[0]["InDate"].ToString()!="")
				{
					model.InDate=DateTime.Parse(ds.Tables[0].Rows[0]["InDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Gdj"].ToString()!="")
				{
					model.Gdj=decimal.Parse(ds.Tables[0].Rows[0]["Gdj"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Balance"].ToString()!="")
				{
					model.Balance=decimal.Parse(ds.Tables[0].Rows[0]["Balance"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["TmpBalance"].ToString()!="")
				{
					model.TmpBalance=decimal.Parse(ds.Tables[0].Rows[0]["TmpBalance"].ToString());
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
			strSql.Append("select seqno, gcode, gtxm, InDate, Gdj, Balance, TmpBalance  ");
			strSql.Append(" FROM T_GoodsStock ");
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
			strSql.Append(" seqno, gcode, gtxm, InDate, Gdj, Balance, TmpBalance  ");
			strSql.Append(" FROM T_GoodsStock ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

