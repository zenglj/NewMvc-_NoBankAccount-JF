using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using Dapper;
using SelfhelpOrderMgr.Model; 
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SHO_GoodsForAttr
		public partial class T_SHO_GoodsForAttrDAL
	{
   		     
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SHO_GoodsForAttr");
			strSql.Append(" where ");
			                                       strSql.Append(" ID = @ID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SHO_GoodsForAttr(");			
            strSql.Append("GCode,GoodsAttrId,AttrInfo");
			strSql.Append(") values (");
            strSql.Append("@GCode,@GoodsAttrId,@AttrInfo");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@GCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GoodsAttrId", SqlDbType.Int,4) ,            
                        new SqlParameter("@AttrInfo", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.GCode;                        
            parameters[1].Value = model.GoodsAttrId;                        
            parameters[2].Value = model.AttrInfo;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SHO_GoodsForAttr set ");
			                                                
            strSql.Append(" GCode = @GCode , ");                                    
            strSql.Append(" GoodsAttrId = @GoodsAttrId , ");                                    
            strSql.Append(" AttrInfo = @AttrInfo  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GoodsAttrId", SqlDbType.Int,4) ,            
                        new SqlParameter("@AttrInfo", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.GCode;                        
            parameters[2].Value = model.GoodsAttrId;                        
            parameters[3].Value = model.AttrInfo;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SHO_GoodsForAttr ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SHO_GoodsForAttr ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, GCode, GoodsAttrId, AttrInfo  ");			
			strSql.Append("  from T_SHO_GoodsForAttr ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr model=new SelfhelpOrderMgr.Model.T_SHO_GoodsForAttr();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.GCode= ds.Tables[0].Rows[0]["GCode"].ToString();
																												if(ds.Tables[0].Rows[0]["GoodsAttrId"].ToString()!="")
				{
					model.GoodsAttrId=int.Parse(ds.Tables[0].Rows[0]["GoodsAttrId"].ToString());
				}
																																				model.AttrInfo= ds.Tables[0].Rows[0]["AttrInfo"].ToString();
																										
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
			strSql.Append("select * ");
			strSql.Append(" FROM T_SHO_GoodsForAttr ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM T_SHO_GoodsForAttr ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

