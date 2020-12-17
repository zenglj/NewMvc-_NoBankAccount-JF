using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_MeetDate
		public partial class T_MeetDateDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_MeetDate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_MeetDate(");			
            strSql.Append("Fcode,FName,Fdesc");
			strSql.Append(") values (");
            strSql.Append("@Fcode,@FName,@Fdesc");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@Fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Fdesc", SqlDbType.VarChar,60)             
              
            };
			            
            parameters[0].Value = model.Fcode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.Fdesc;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MeetDate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_MeetDate set ");
			                        
            strSql.Append(" Fcode = @Fcode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" Fdesc = @Fdesc  ");            			
			strSql.Append(" where Fcode=@Fcode and FName=@FName  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@Fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Fdesc", SqlDbType.VarChar,60)             
              
            };
						            
            parameters[0].Value = model.Fcode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.Fdesc;                        
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
		public bool Delete(string Fcode,string FName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_MeetDate ");
			strSql.Append(" where Fcode=@Fcode and FName=@FName ");
						SqlParameter[] parameters = {
					new SqlParameter("@Fcode", SqlDbType.VarChar,3),
					new SqlParameter("@FName", SqlDbType.VarChar,20)			};
			parameters[0].Value = Fcode;
			parameters[1].Value = FName;


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
		public SelfhelpOrderMgr.Model.T_MeetDate GetModel(string Fcode,string FName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Fcode, FName, Fdesc  ");			
			strSql.Append("  from T_MeetDate ");
			strSql.Append(" where Fcode=@Fcode and FName=@FName ");
						SqlParameter[] parameters = {
					new SqlParameter("@Fcode", SqlDbType.VarChar,3),
					new SqlParameter("@FName", SqlDbType.VarChar,20)			};
			parameters[0].Value = Fcode;
			parameters[1].Value = FName;

			
			SelfhelpOrderMgr.Model.T_MeetDate model=new SelfhelpOrderMgr.Model.T_MeetDate();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.Fcode= ds.Tables[0].Rows[0]["Fcode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																																model.Fdesc= ds.Tables[0].Rows[0]["Fdesc"].ToString();
																										
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
			strSql.Append("select Fcode, FName, Fdesc  ");
			strSql.Append(" FROM T_MeetDate ");
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
			strSql.Append(" Fcode, FName, Fdesc  ");
			strSql.Append(" FROM T_MeetDate ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

