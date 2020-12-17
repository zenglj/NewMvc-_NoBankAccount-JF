using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_TotalMeet
		public partial class T_TotalMeetDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_TotalMeet model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_TotalMeet(");			
            strSql.Append("Area,mon1,mon2,mon3,mon4,mon5,mon6,mon7,mon8,mon9,mon10,mon11,mon12");
			strSql.Append(") values (");
            strSql.Append("@Area,@mon1,@mon2,@mon3,@mon4,@mon5,@mon6,@mon7,@mon8,@mon9,@mon10,@mon11,@mon12");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@Area", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@mon1", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon2", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon3", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon4", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon5", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon6", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon7", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon8", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon9", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon10", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon11", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon12", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.Area;                        
            parameters[1].Value = model.mon1;                        
            parameters[2].Value = model.mon2;                        
            parameters[3].Value = model.mon3;                        
            parameters[4].Value = model.mon4;                        
            parameters[5].Value = model.mon5;                        
            parameters[6].Value = model.mon6;                        
            parameters[7].Value = model.mon7;                        
            parameters[8].Value = model.mon8;                        
            parameters[9].Value = model.mon9;                        
            parameters[10].Value = model.mon10;                        
            parameters[11].Value = model.mon11;                        
            parameters[12].Value = model.mon12;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TotalMeet model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_TotalMeet set ");
			                        
            strSql.Append(" Area = @Area , ");                                    
            strSql.Append(" mon1 = @mon1 , ");                                    
            strSql.Append(" mon2 = @mon2 , ");                                    
            strSql.Append(" mon3 = @mon3 , ");                                    
            strSql.Append(" mon4 = @mon4 , ");                                    
            strSql.Append(" mon5 = @mon5 , ");                                    
            strSql.Append(" mon6 = @mon6 , ");                                    
            strSql.Append(" mon7 = @mon7 , ");                                    
            strSql.Append(" mon8 = @mon8 , ");                                    
            strSql.Append(" mon9 = @mon9 , ");                                    
            strSql.Append(" mon10 = @mon10 , ");                                    
            strSql.Append(" mon11 = @mon11 , ");                                    
            strSql.Append(" mon12 = @mon12  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@Area", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@mon1", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon2", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon3", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon4", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon5", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon6", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon7", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon8", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon9", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon10", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon11", SqlDbType.Int,4) ,            
                        new SqlParameter("@mon12", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.Area;                        
            parameters[1].Value = model.mon1;                        
            parameters[2].Value = model.mon2;                        
            parameters[3].Value = model.mon3;                        
            parameters[4].Value = model.mon4;                        
            parameters[5].Value = model.mon5;                        
            parameters[6].Value = model.mon6;                        
            parameters[7].Value = model.mon7;                        
            parameters[8].Value = model.mon8;                        
            parameters[9].Value = model.mon9;                        
            parameters[10].Value = model.mon10;                        
            parameters[11].Value = model.mon11;                        
            parameters[12].Value = model.mon12;                        
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
			strSql.Append("delete from T_TotalMeet ");
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
		public SelfhelpOrderMgr.Model.T_TotalMeet GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Area, mon1, mon2, mon3, mon4, mon5, mon6, mon7, mon8, mon9, mon10, mon11, mon12  ");			
			strSql.Append("  from T_TotalMeet ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_TotalMeet model=new SelfhelpOrderMgr.Model.T_TotalMeet();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.Area= ds.Tables[0].Rows[0]["Area"].ToString();
																												if(ds.Tables[0].Rows[0]["mon1"].ToString()!="")
				{
					model.mon1=int.Parse(ds.Tables[0].Rows[0]["mon1"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon2"].ToString()!="")
				{
					model.mon2=int.Parse(ds.Tables[0].Rows[0]["mon2"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon3"].ToString()!="")
				{
					model.mon3=int.Parse(ds.Tables[0].Rows[0]["mon3"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon4"].ToString()!="")
				{
					model.mon4=int.Parse(ds.Tables[0].Rows[0]["mon4"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon5"].ToString()!="")
				{
					model.mon5=int.Parse(ds.Tables[0].Rows[0]["mon5"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon6"].ToString()!="")
				{
					model.mon6=int.Parse(ds.Tables[0].Rows[0]["mon6"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon7"].ToString()!="")
				{
					model.mon7=int.Parse(ds.Tables[0].Rows[0]["mon7"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon8"].ToString()!="")
				{
					model.mon8=int.Parse(ds.Tables[0].Rows[0]["mon8"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon9"].ToString()!="")
				{
					model.mon9=int.Parse(ds.Tables[0].Rows[0]["mon9"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon10"].ToString()!="")
				{
					model.mon10=int.Parse(ds.Tables[0].Rows[0]["mon10"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon11"].ToString()!="")
				{
					model.mon11=int.Parse(ds.Tables[0].Rows[0]["mon11"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["mon12"].ToString()!="")
				{
					model.mon12=int.Parse(ds.Tables[0].Rows[0]["mon12"].ToString());
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
			strSql.Append("select Area, mon1, mon2, mon3, mon4, mon5, mon6, mon7, mon8, mon9, mon10, mon11, mon12  ");
			strSql.Append(" FROM T_TotalMeet ");
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
			strSql.Append(" Area, mon1, mon2, mon3, mon4, mon5, mon6, mon7, mon8, mon9, mon10, mon11, mon12  ");
			strSql.Append(" FROM T_TotalMeet ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

