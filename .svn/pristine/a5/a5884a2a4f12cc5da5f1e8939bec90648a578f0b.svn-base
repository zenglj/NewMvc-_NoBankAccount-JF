using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_RECORD_INFO
		public partial class T_RECORD_INFODAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_RECORD_INFO model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_RECORD_INFO(");			
            strSql.Append("FCode,BILLNO,FILEPATH,PNO,WNO,TYPE,DUAL,STARTTIME,STARTDATE");
			strSql.Append(") values (");
            strSql.Append("@FCode,@BILLNO,@FILEPATH,@PNO,@WNO,@TYPE,@DUAL,@STARTTIME,@STARTDATE");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BILLNO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FILEPATH", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@PNO", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@WNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@TYPE", SqlDbType.Int,4) ,            
                        new SqlParameter("@DUAL", SqlDbType.Int,4) ,            
                        new SqlParameter("@STARTTIME", SqlDbType.DateTime) ,            
                        new SqlParameter("@STARTDATE", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.BILLNO;                        
            parameters[2].Value = model.FILEPATH;                        
            parameters[3].Value = model.PNO;                        
            parameters[4].Value = model.WNO;                        
            parameters[5].Value = model.TYPE;                        
            parameters[6].Value = model.DUAL;                        
            parameters[7].Value = model.STARTTIME;                        
            parameters[8].Value = model.STARTDATE;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_RECORD_INFO model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_RECORD_INFO set ");
			                                                
            strSql.Append(" FCode = @FCode , ");                                    
            strSql.Append(" BILLNO = @BILLNO , ");                                    
            strSql.Append(" FILEPATH = @FILEPATH , ");                                    
            strSql.Append(" PNO = @PNO , ");                                    
            strSql.Append(" WNO = @WNO , ");                                    
            strSql.Append(" TYPE = @TYPE , ");                                    
            strSql.Append(" DUAL = @DUAL , ");                                    
            strSql.Append(" STARTTIME = @STARTTIME , ");                                    
            strSql.Append(" STARTDATE = @STARTDATE  ");            			
			strSql.Append(" where SEQ=@SEQ ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@SEQ", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BILLNO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FILEPATH", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@PNO", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@WNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@TYPE", SqlDbType.Int,4) ,            
                        new SqlParameter("@DUAL", SqlDbType.Int,4) ,            
                        new SqlParameter("@STARTTIME", SqlDbType.DateTime) ,            
                        new SqlParameter("@STARTDATE", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.SEQ;                        
            parameters[1].Value = model.FCode;                        
            parameters[2].Value = model.BILLNO;                        
            parameters[3].Value = model.FILEPATH;                        
            parameters[4].Value = model.PNO;                        
            parameters[5].Value = model.WNO;                        
            parameters[6].Value = model.TYPE;                        
            parameters[7].Value = model.DUAL;                        
            parameters[8].Value = model.STARTTIME;                        
            parameters[9].Value = model.STARTDATE;                        
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
		public bool Delete(int SEQ)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_RECORD_INFO ");
			strSql.Append(" where SEQ=@SEQ");
						SqlParameter[] parameters = {
					new SqlParameter("@SEQ", SqlDbType.Int,4)
			};
			parameters[0].Value = SEQ;


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
		public bool DeleteList(string SEQlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_RECORD_INFO ");
			strSql.Append(" where ID in ("+SEQlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_RECORD_INFO GetModel(int SEQ)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SEQ, FCode, BILLNO, FILEPATH, PNO, WNO, TYPE, DUAL, STARTTIME, STARTDATE  ");			
			strSql.Append("  from T_RECORD_INFO ");
			strSql.Append(" where SEQ=@SEQ");
						SqlParameter[] parameters = {
					new SqlParameter("@SEQ", SqlDbType.Int,4)
			};
			parameters[0].Value = SEQ;

			
			SelfhelpOrderMgr.Model.T_RECORD_INFO model=new SelfhelpOrderMgr.Model.T_RECORD_INFO();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["SEQ"].ToString()!="")
				{
					model.SEQ=int.Parse(ds.Tables[0].Rows[0]["SEQ"].ToString());
				}
																																				model.FCode= ds.Tables[0].Rows[0]["FCode"].ToString();
																																model.BILLNO= ds.Tables[0].Rows[0]["BILLNO"].ToString();
																																model.FILEPATH= ds.Tables[0].Rows[0]["FILEPATH"].ToString();
																																model.PNO= ds.Tables[0].Rows[0]["PNO"].ToString();
																												if(ds.Tables[0].Rows[0]["WNO"].ToString()!="")
				{
					model.WNO=int.Parse(ds.Tables[0].Rows[0]["WNO"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["TYPE"].ToString()!="")
				{
					model.TYPE=int.Parse(ds.Tables[0].Rows[0]["TYPE"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DUAL"].ToString()!="")
				{
					model.DUAL=int.Parse(ds.Tables[0].Rows[0]["DUAL"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["STARTTIME"].ToString()!="")
				{
					model.STARTTIME=DateTime.Parse(ds.Tables[0].Rows[0]["STARTTIME"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["STARTDATE"].ToString()!="")
				{
					model.STARTDATE=DateTime.Parse(ds.Tables[0].Rows[0]["STARTDATE"].ToString());
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
			strSql.Append("select SEQ, FCode, BILLNO, FILEPATH, PNO, WNO, TYPE, DUAL, STARTTIME, STARTDATE  ");
			strSql.Append(" FROM T_RECORD_INFO ");
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
			strSql.Append(" SEQ, FCode, BILLNO, FILEPATH, PNO, WNO, TYPE, DUAL, STARTTIME, STARTDATE  ");
			strSql.Append(" FROM T_RECORD_INFO ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

