using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_GOODSSTOCKMAIN
		public partial class T_GOODSSTOCKMAINDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_GOODSSTOCKMAIN(");			
            strSql.Append("GCODE,BALANCE,TMPBALANCE,GCurMonthNum,GCurMonthPrice,GLastAvgNum,GLastAvgPrice,LastDate,GJieShuanNumber");
			strSql.Append(") values (");
            strSql.Append("@GCODE,@BALANCE,@TMPBALANCE,@GCurMonthNum,@GCurMonthPrice,@GLastAvgNum,@GLastAvgPrice,@LastDate,@GJieShuanNumber");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BALANCE", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@TMPBALANCE", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GCurMonthNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GCurMonthPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GLastAvgNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GLastAvgPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@LastDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@GJieShuanNumber", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.GCODE;                        
            parameters[1].Value = model.BALANCE;                        
            parameters[2].Value = model.TMPBALANCE;                        
            parameters[3].Value = model.GCurMonthNum;                        
            parameters[4].Value = model.GCurMonthPrice;                        
            parameters[5].Value = model.GLastAvgNum;                        
            parameters[6].Value = model.GLastAvgPrice;                        
            parameters[7].Value = model.LastDate;                        
            parameters[8].Value = model.GJieShuanNumber;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_GOODSSTOCKMAIN set ");
			                                                
            strSql.Append(" GCODE = @GCODE , ");                                    
            strSql.Append(" BALANCE = @BALANCE , ");                                    
            strSql.Append(" TMPBALANCE = @TMPBALANCE , ");                                    
            strSql.Append(" GCurMonthNum = @GCurMonthNum , ");                                    
            strSql.Append(" GCurMonthPrice = @GCurMonthPrice , ");                                    
            strSql.Append(" GLastAvgNum = @GLastAvgNum , ");                                    
            strSql.Append(" GLastAvgPrice = @GLastAvgPrice , ");                                    
            strSql.Append(" LastDate = @LastDate , ");                                    
            strSql.Append(" GJieShuanNumber = @GJieShuanNumber  ");            			
			strSql.Append(" where SEQNO=@SEQNO ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@SEQNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BALANCE", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@TMPBALANCE", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GCurMonthNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GCurMonthPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GLastAvgNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GLastAvgPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@LastDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@GJieShuanNumber", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.SEQNO;                        
            parameters[1].Value = model.GCODE;                        
            parameters[2].Value = model.BALANCE;                        
            parameters[3].Value = model.TMPBALANCE;                        
            parameters[4].Value = model.GCurMonthNum;                        
            parameters[5].Value = model.GCurMonthPrice;                        
            parameters[6].Value = model.GLastAvgNum;                        
            parameters[7].Value = model.GLastAvgPrice;                        
            parameters[8].Value = model.LastDate;                        
            parameters[9].Value = model.GJieShuanNumber;                        
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
		public bool Delete(int SEQNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_GOODSSTOCKMAIN ");
			strSql.Append(" where SEQNO=@SEQNO");
						SqlParameter[] parameters = {
					new SqlParameter("@SEQNO", SqlDbType.Int,4)
			};
			parameters[0].Value = SEQNO;


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
		public bool DeleteList(string SEQNOlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_GOODSSTOCKMAIN ");
			strSql.Append(" where ID in ("+SEQNOlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN GetModel(int SEQNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SEQNO, GCODE, BALANCE, TMPBALANCE, GCurMonthNum, GCurMonthPrice, GLastAvgNum, GLastAvgPrice, LastDate, GJieShuanNumber  ");			
			strSql.Append("  from T_GOODSSTOCKMAIN ");
			strSql.Append(" where SEQNO=@SEQNO");
						SqlParameter[] parameters = {
					new SqlParameter("@SEQNO", SqlDbType.Int,4)
			};
			parameters[0].Value = SEQNO;

			
			SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN model=new SelfhelpOrderMgr.Model.T_GOODSSTOCKMAIN();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["SEQNO"].ToString()!="")
				{
					model.SEQNO=int.Parse(ds.Tables[0].Rows[0]["SEQNO"].ToString());
				}
																																				model.GCODE= ds.Tables[0].Rows[0]["GCODE"].ToString();
																												if(ds.Tables[0].Rows[0]["BALANCE"].ToString()!="")
				{
					model.BALANCE=decimal.Parse(ds.Tables[0].Rows[0]["BALANCE"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["TMPBALANCE"].ToString()!="")
				{
					model.TMPBALANCE=decimal.Parse(ds.Tables[0].Rows[0]["TMPBALANCE"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["GCurMonthNum"].ToString()!="")
				{
					model.GCurMonthNum=decimal.Parse(ds.Tables[0].Rows[0]["GCurMonthNum"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["GCurMonthPrice"].ToString()!="")
				{
					model.GCurMonthPrice=decimal.Parse(ds.Tables[0].Rows[0]["GCurMonthPrice"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["GLastAvgNum"].ToString()!="")
				{
					model.GLastAvgNum=decimal.Parse(ds.Tables[0].Rows[0]["GLastAvgNum"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["GLastAvgPrice"].ToString()!="")
				{
					model.GLastAvgPrice=decimal.Parse(ds.Tables[0].Rows[0]["GLastAvgPrice"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["LastDate"].ToString()!="")
				{
					model.LastDate=DateTime.Parse(ds.Tables[0].Rows[0]["LastDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["GJieShuanNumber"].ToString()!="")
				{
					model.GJieShuanNumber=decimal.Parse(ds.Tables[0].Rows[0]["GJieShuanNumber"].ToString());
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
			strSql.Append("select SEQNO, GCODE, BALANCE, TMPBALANCE, GCurMonthNum, GCurMonthPrice, GLastAvgNum, GLastAvgPrice, LastDate, GJieShuanNumber  ");
			strSql.Append(" FROM T_GOODSSTOCKMAIN ");
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
			strSql.Append(" SEQNO, GCODE, BALANCE, TMPBALANCE, GCurMonthNum, GCurMonthPrice, GLastAvgNum, GLastAvgPrice, LastDate, GJieShuanNumber  ");
			strSql.Append(" FROM T_GOODSSTOCKMAIN ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

