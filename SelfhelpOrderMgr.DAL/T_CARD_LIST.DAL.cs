using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_CARD_LIST
		public partial class T_CARD_LISTDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_CARD_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_CARD_LIST(");			
            strSql.Append("FSN,FCardCode,FCrimeCode,FRDate,FFlag,FCzy");
			strSql.Append(") values (");
            strSql.Append("@FSN,@FCardCode,@FCrimeCode,@FRDate,@FFlag,@FCzy");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FSN", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCzy", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.FSN;                        
            parameters[1].Value = model.FCardCode;                        
            parameters[2].Value = model.FCrimeCode;                        
            parameters[3].Value = model.FRDate;                        
            parameters[4].Value = model.FFlag;                        
            parameters[5].Value = model.FCzy;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_CARD_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_CARD_LIST set ");
			                        
            strSql.Append(" FSN = @FSN , ");                                    
            strSql.Append(" FCardCode = @FCardCode , ");                                    
            strSql.Append(" FCrimeCode = @FCrimeCode , ");                                    
            strSql.Append(" FRDate = @FRDate , ");                                    
            strSql.Append(" FFlag = @FFlag , ");                                    
            strSql.Append(" FCzy = @FCzy  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FSN", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCzy", SqlDbType.VarChar,20)             
              
            };
						            
            parameters[0].Value = model.FSN;                        
            parameters[1].Value = model.FCardCode;                        
            parameters[2].Value = model.FCrimeCode;                        
            parameters[3].Value = model.FRDate;                        
            parameters[4].Value = model.FFlag;                        
            parameters[5].Value = model.FCzy;                        
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
			strSql.Append("delete from T_CARD_LIST ");
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
		public SelfhelpOrderMgr.Model.T_CARD_LIST GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FSN, FCardCode, FCrimeCode, FRDate, FFlag, FCzy  ");			
			strSql.Append("  from T_CARD_LIST ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_CARD_LIST model=new SelfhelpOrderMgr.Model.T_CARD_LIST();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["FSN"].ToString()!="")
				{
					model.FSN=int.Parse(ds.Tables[0].Rows[0]["FSN"].ToString());
				}
																																				model.FCardCode= ds.Tables[0].Rows[0]["FCardCode"].ToString();
																																model.FCrimeCode= ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
																												if(ds.Tables[0].Rows[0]["FRDate"].ToString()!="")
				{
					model.FRDate=DateTime.Parse(ds.Tables[0].Rows[0]["FRDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FFlag"].ToString()!="")
				{
					model.FFlag=int.Parse(ds.Tables[0].Rows[0]["FFlag"].ToString());
				}
																																				model.FCzy= ds.Tables[0].Rows[0]["FCzy"].ToString();
																										
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
			strSql.Append("select FSN, FCardCode, FCrimeCode, FRDate, FFlag, FCzy  ");
			strSql.Append(" FROM T_CARD_LIST ");
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
			strSql.Append(" FSN, FCardCode, FCrimeCode, FRDate, FFlag, FCzy  ");
			strSql.Append(" FROM T_CARD_LIST ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

