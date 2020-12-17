using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_TempLilun
		public partial class T_TempLilunDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_TempLilun model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_TempLilun(");			
            strSql.Append("gcode,gname,price,gnum,gCurMonthPrice,lilun");
			strSql.Append(") values (");
            strSql.Append("@gcode,@gname,@price,@gnum,@gCurMonthPrice,@lilun");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@gcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@gname", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@gnum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@gCurMonthPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@lilun", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.gcode;                        
            parameters[1].Value = model.gname;                        
            parameters[2].Value = model.price;                        
            parameters[3].Value = model.gnum;                        
            parameters[4].Value = model.gCurMonthPrice;                        
            parameters[5].Value = model.lilun;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TempLilun model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_TempLilun set ");
			                        
            strSql.Append(" gcode = @gcode , ");                                    
            strSql.Append(" gname = @gname , ");                                    
            strSql.Append(" price = @price , ");                                    
            strSql.Append(" gnum = @gnum , ");                                    
            strSql.Append(" gCurMonthPrice = @gCurMonthPrice , ");                                    
            strSql.Append(" lilun = @lilun  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@gcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@gname", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@gnum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@gCurMonthPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@lilun", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.gcode;                        
            parameters[1].Value = model.gname;                        
            parameters[2].Value = model.price;                        
            parameters[3].Value = model.gnum;                        
            parameters[4].Value = model.gCurMonthPrice;                        
            parameters[5].Value = model.lilun;                        
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
			strSql.Append("delete from T_TempLilun ");
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
		public SelfhelpOrderMgr.Model.T_TempLilun GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select gcode, gname, price, gnum, gCurMonthPrice, lilun  ");			
			strSql.Append("  from T_TempLilun ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_TempLilun model=new SelfhelpOrderMgr.Model.T_TempLilun();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.gcode= ds.Tables[0].Rows[0]["gcode"].ToString();
																																model.gname= ds.Tables[0].Rows[0]["gname"].ToString();
																												if(ds.Tables[0].Rows[0]["price"].ToString()!="")
				{
					model.price=decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["gnum"].ToString()!="")
				{
					model.gnum=decimal.Parse(ds.Tables[0].Rows[0]["gnum"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["gCurMonthPrice"].ToString()!="")
				{
					model.gCurMonthPrice=decimal.Parse(ds.Tables[0].Rows[0]["gCurMonthPrice"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["lilun"].ToString()!="")
				{
					model.lilun=decimal.Parse(ds.Tables[0].Rows[0]["lilun"].ToString());
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
			strSql.Append("select gcode, gname, price, gnum, gCurMonthPrice, lilun  ");
			strSql.Append(" FROM T_TempLilun ");
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
			strSql.Append(" gcode, gname, price, gnum, gCurMonthPrice, lilun  ");
			strSql.Append(" FROM T_TempLilun ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

