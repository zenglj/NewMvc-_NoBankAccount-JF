using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SGOODS
		public partial class T_SGOODSDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_SGOODS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SGOODS(");			
            strSql.Append("GCODE,GNAME,GTYPE,GUnit,GDJ,ACTIVE,Ffreeflag");
			strSql.Append(") values (");
            strSql.Append("@GCODE,@GNAME,@GTYPE,@GUnit,@GDJ,@ACTIVE,@Ffreeflag");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GNAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@GTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GUnit", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@ACTIVE", SqlDbType.Char,1) ,            
                        new SqlParameter("@Ffreeflag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.GCODE;                        
            parameters[1].Value = model.GNAME;                        
            parameters[2].Value = model.GTYPE;                        
            parameters[3].Value = model.GUnit;                        
            parameters[4].Value = model.GDJ;                        
            parameters[5].Value = model.ACTIVE;                        
            parameters[6].Value = model.Ffreeflag;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SGOODS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SGOODS set ");
			                        
            strSql.Append(" GCODE = @GCODE , ");                                    
            strSql.Append(" GNAME = @GNAME , ");                                    
            strSql.Append(" GTYPE = @GTYPE , ");                                    
            strSql.Append(" GUnit = @GUnit , ");                                    
            strSql.Append(" GDJ = @GDJ , ");                                    
            strSql.Append(" ACTIVE = @ACTIVE , ");                                    
            strSql.Append(" Ffreeflag = @Ffreeflag  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GNAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@GTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GUnit", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@ACTIVE", SqlDbType.Char,1) ,            
                        new SqlParameter("@Ffreeflag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.GCODE;                        
            parameters[1].Value = model.GNAME;                        
            parameters[2].Value = model.GTYPE;                        
            parameters[3].Value = model.GUnit;                        
            parameters[4].Value = model.GDJ;                        
            parameters[5].Value = model.ACTIVE;                        
            parameters[6].Value = model.Ffreeflag;                        
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
			strSql.Append("delete from T_SGOODS ");
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
		public SelfhelpOrderMgr.Model.T_SGOODS GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select GCODE, GNAME, GTYPE, GUnit, GDJ, ACTIVE, Ffreeflag  ");			
			strSql.Append("  from T_SGOODS ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_SGOODS model=new SelfhelpOrderMgr.Model.T_SGOODS();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.GCODE= ds.Tables[0].Rows[0]["GCODE"].ToString();
																																model.GNAME= ds.Tables[0].Rows[0]["GNAME"].ToString();
																																model.GTYPE= ds.Tables[0].Rows[0]["GTYPE"].ToString();
																																model.GUnit= ds.Tables[0].Rows[0]["GUnit"].ToString();
																												if(ds.Tables[0].Rows[0]["GDJ"].ToString()!="")
				{
					model.GDJ=decimal.Parse(ds.Tables[0].Rows[0]["GDJ"].ToString());
				}
																																				model.ACTIVE= ds.Tables[0].Rows[0]["ACTIVE"].ToString();
																												if(ds.Tables[0].Rows[0]["Ffreeflag"].ToString()!="")
				{
					model.Ffreeflag=int.Parse(ds.Tables[0].Rows[0]["Ffreeflag"].ToString());
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
			strSql.Append("select GCODE, GNAME, GTYPE, GUnit, GDJ, ACTIVE, Ffreeflag  ");
			strSql.Append(" FROM T_SGOODS ");
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
			strSql.Append(" GCODE, GNAME, GTYPE, GUnit, GDJ, ACTIVE, Ffreeflag  ");
			strSql.Append(" FROM T_SGOODS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

