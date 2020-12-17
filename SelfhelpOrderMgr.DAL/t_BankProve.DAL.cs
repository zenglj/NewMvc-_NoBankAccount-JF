using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_BankProve
		public partial class t_BankProveDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.t_BankProve model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_BankProve(");			
            strSql.Append("fcode,fname,foudate,FIdenNo,fareaName,BankCode,CardMoney,crtDate");
			strSql.Append(") values (");
            strSql.Append("@fcode,@fname,@foudate,@FIdenNo,@fareaName,@BankCode,@CardMoney,@crtDate");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@foudate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@BankCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CardMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@crtDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.fcode;                        
            parameters[1].Value = model.fname;                        
            parameters[2].Value = model.foudate;                        
            parameters[3].Value = model.FIdenNo;                        
            parameters[4].Value = model.fareaName;                        
            parameters[5].Value = model.BankCode;                        
            parameters[6].Value = model.CardMoney;                        
            parameters[7].Value = model.crtDate;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_BankProve model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_BankProve set ");
			                        
            strSql.Append(" fcode = @fcode , ");                                    
            strSql.Append(" fname = @fname , ");                                    
            strSql.Append(" foudate = @foudate , ");                                    
            strSql.Append(" FIdenNo = @FIdenNo , ");                                    
            strSql.Append(" fareaName = @fareaName , ");                                    
            strSql.Append(" BankCode = @BankCode , ");                                    
            strSql.Append(" CardMoney = @CardMoney , ");                                    
            strSql.Append(" crtDate = @crtDate  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@foudate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@BankCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CardMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@crtDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.fcode;                        
            parameters[1].Value = model.fname;                        
            parameters[2].Value = model.foudate;                        
            parameters[3].Value = model.FIdenNo;                        
            parameters[4].Value = model.fareaName;                        
            parameters[5].Value = model.BankCode;                        
            parameters[6].Value = model.CardMoney;                        
            parameters[7].Value = model.crtDate;                        
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
			strSql.Append("delete from t_BankProve ");
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
		public SelfhelpOrderMgr.Model.t_BankProve GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select fcode, fname, foudate, FIdenNo, fareaName, BankCode, CardMoney, crtDate  ");			
			strSql.Append("  from t_BankProve ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.t_BankProve model=new SelfhelpOrderMgr.Model.t_BankProve();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.fcode= ds.Tables[0].Rows[0]["fcode"].ToString();
																																model.fname= ds.Tables[0].Rows[0]["fname"].ToString();
																												if(ds.Tables[0].Rows[0]["foudate"].ToString()!="")
				{
					model.foudate=DateTime.Parse(ds.Tables[0].Rows[0]["foudate"].ToString());
				}
																																				model.FIdenNo= ds.Tables[0].Rows[0]["FIdenNo"].ToString();
																																model.fareaName= ds.Tables[0].Rows[0]["fareaName"].ToString();
																																model.BankCode= ds.Tables[0].Rows[0]["BankCode"].ToString();
																												if(ds.Tables[0].Rows[0]["CardMoney"].ToString()!="")
				{
					model.CardMoney=decimal.Parse(ds.Tables[0].Rows[0]["CardMoney"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["crtDate"].ToString()!="")
				{
					model.crtDate=DateTime.Parse(ds.Tables[0].Rows[0]["crtDate"].ToString());
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
			strSql.Append("select fcode, fname, foudate, FIdenNo, fareaName, BankCode, CardMoney, crtDate  ");
			strSql.Append(" FROM t_BankProve ");
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
			strSql.Append(" fcode, fname, foudate, FIdenNo, fareaName, BankCode, CardMoney, crtDate  ");
			strSql.Append(" FROM t_BankProve ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

