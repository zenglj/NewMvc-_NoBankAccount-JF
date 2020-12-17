using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Supplyer
		public partial class T_SupplyerDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Supplyer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Supplyer(");			
            strSql.Append("scode,sName,sAddress,sTel,sFax,sAtten,sAccountNo,sBank");
			strSql.Append(") values (");
            strSql.Append("@scode,@sName,@sAddress,@sTel,@sFax,@sAtten,@sAccountNo,@sBank");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@scode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sName", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@sAddress", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@sTel", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@sFax", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@sAtten", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sAccountNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sBank", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.scode;                        
            parameters[1].Value = model.sName;                        
            parameters[2].Value = model.sAddress;                        
            parameters[3].Value = model.sTel;                        
            parameters[4].Value = model.sFax;                        
            parameters[5].Value = model.sAtten;                        
            parameters[6].Value = model.sAccountNo;                        
            parameters[7].Value = model.sBank;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Supplyer model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Supplyer set ");
			                        
            strSql.Append(" sName = @sName , ");                                    
            strSql.Append(" sAddress = @sAddress , ");                                    
            strSql.Append(" sTel = @sTel , ");                                    
            strSql.Append(" sFax = @sFax , ");                                    
            strSql.Append(" sAtten = @sAtten , ");                                    
            strSql.Append(" sAccountNo = @sAccountNo , ");                                    
            strSql.Append(" sBank = @sBank  ");
            strSql.Append(" where scode = @scode  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@scode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sName", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@sAddress", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@sTel", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@sFax", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@sAtten", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sAccountNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sBank", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.scode;                        
            parameters[1].Value = model.sName;                        
            parameters[2].Value = model.sAddress;                        
            parameters[3].Value = model.sTel;                        
            parameters[4].Value = model.sFax;                        
            parameters[5].Value = model.sAtten;                        
            parameters[6].Value = model.sAccountNo;                        
            parameters[7].Value = model.sBank;                        
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
        public bool Delete(string scode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Supplyer ");
			strSql.Append(" where ");
            SqlParameter[] parameters = {new SqlParameter("@scode", SqlDbType.VarChar,20)
			};
            parameters[0].Value = scode;

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
		public SelfhelpOrderMgr.Model.T_Supplyer GetModel(string scode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select scode, sName, sAddress, sTel, sFax, sAtten, sAccountNo, sBank  ");			
			strSql.Append("  from T_Supplyer ");
            strSql.Append(" where scode=@scode");
            SqlParameter[] parameters = {new SqlParameter("@scode", SqlDbType.VarChar,20)
			};
            parameters[0].Value = scode;
			
			SelfhelpOrderMgr.Model.T_Supplyer model=new SelfhelpOrderMgr.Model.T_Supplyer();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.scode= ds.Tables[0].Rows[0]["scode"].ToString();
																																model.sName= ds.Tables[0].Rows[0]["sName"].ToString();
																																model.sAddress= ds.Tables[0].Rows[0]["sAddress"].ToString();
																																model.sTel= ds.Tables[0].Rows[0]["sTel"].ToString();
																																model.sFax= ds.Tables[0].Rows[0]["sFax"].ToString();
																																model.sAtten= ds.Tables[0].Rows[0]["sAtten"].ToString();
																																model.sAccountNo= ds.Tables[0].Rows[0]["sAccountNo"].ToString();
																																model.sBank= ds.Tables[0].Rows[0]["sBank"].ToString();
																										
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
			strSql.Append("select scode, sName, sAddress, sTel, sFax, sAtten, sAccountNo, sBank  ");
			strSql.Append(" FROM T_Supplyer ");
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
			strSql.Append(" scode, sName, sAddress, sTel, sFax, sAtten, sAccountNo, sBank  ");
			strSql.Append(" FROM T_Supplyer ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

