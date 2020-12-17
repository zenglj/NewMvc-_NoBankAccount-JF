using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_BALANCE
		public partial class T_BALANCEDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_BALANCE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_BALANCE(");			
            strSql.Append("seqNo,cardcode,fcrimecode,amount,CRTDATE,BTYPE,REMARK");
			strSql.Append(") values (");
            strSql.Append("@seqNo,@cardcode,@fcrimecode,@amount,@CRTDATE,@BTYPE,@REMARK");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@seqNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@cardcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@CRTDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@BTYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@REMARK", SqlDbType.VarChar,512)             
              
            };
			            
            parameters[0].Value = model.seqNo;                        
            parameters[1].Value = model.cardcode;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.amount;                        
            parameters[4].Value = model.CRTDATE;                        
            parameters[5].Value = model.BTYPE;                        
            parameters[6].Value = model.REMARK;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_BALANCE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_BALANCE set ");
			                        
            strSql.Append(" seqNo = @seqNo , ");                                    
            strSql.Append(" cardcode = @cardcode , ");                                    
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" amount = @amount , ");                                    
            strSql.Append(" CRTDATE = @CRTDATE , ");                                    
            strSql.Append(" BTYPE = @BTYPE , ");                                    
            strSql.Append(" REMARK = @REMARK  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@cardcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@CRTDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@BTYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@REMARK", SqlDbType.VarChar,512)             
              
            };
						            
            parameters[0].Value = model.seqNo;                        
            parameters[1].Value = model.cardcode;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.amount;                        
            parameters[4].Value = model.CRTDATE;                        
            parameters[5].Value = model.BTYPE;                        
            parameters[6].Value = model.REMARK;                        
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
			strSql.Append("delete from T_BALANCE ");
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
		public SelfhelpOrderMgr.Model.T_BALANCE GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqNo, cardcode, fcrimecode, amount, CRTDATE, BTYPE, REMARK  ");			
			strSql.Append("  from T_BALANCE ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_BALANCE model=new SelfhelpOrderMgr.Model.T_BALANCE();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqNo"].ToString()!="")
				{
					model.seqNo=int.Parse(ds.Tables[0].Rows[0]["seqNo"].ToString());
				}
																																				model.cardcode= ds.Tables[0].Rows[0]["cardcode"].ToString();
																																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																												if(ds.Tables[0].Rows[0]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CRTDATE"].ToString()!="")
				{
					model.CRTDATE=DateTime.Parse(ds.Tables[0].Rows[0]["CRTDATE"].ToString());
				}
																																				model.BTYPE= ds.Tables[0].Rows[0]["BTYPE"].ToString();
																																model.REMARK= ds.Tables[0].Rows[0]["REMARK"].ToString();
																										
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
			strSql.Append("select seqNo, cardcode, fcrimecode, amount, CRTDATE, BTYPE, REMARK  ");
			strSql.Append(" FROM T_BALANCE ");
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
			strSql.Append(" seqNo, cardcode, fcrimecode, amount, CRTDATE, BTYPE, REMARK  ");
			strSql.Append(" FROM T_BALANCE ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

