using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_TmpRcvPay
		public partial class T_TmpRcvPayDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_TmpRcvPay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_TmpRcvPay(");			
            strSql.Append("BankName,AccNo,Dtype,paydate,Fmoney,SucMoney,ErrMoney,Remark");
			strSql.Append(") values (");
            strSql.Append("@BankName,@AccNo,@Dtype,@paydate,@Fmoney,@SucMoney,@ErrMoney,@Remark");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@BankName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Dtype", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@paydate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Fmoney", SqlDbType.Decimal,17) ,            
                        new SqlParameter("@SucMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ErrMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.BankName;                        
            parameters[1].Value = model.AccNo;                        
            parameters[2].Value = model.Dtype;                        
            parameters[3].Value = model.paydate;                        
            parameters[4].Value = model.Fmoney;                        
            parameters[5].Value = model.SucMoney;                        
            parameters[6].Value = model.ErrMoney;                        
            parameters[7].Value = model.Remark;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TmpRcvPay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_TmpRcvPay set ");
			                        
            strSql.Append(" BankName = @BankName , ");                                    
            strSql.Append(" AccNo = @AccNo , ");                                    
            strSql.Append(" Dtype = @Dtype , ");                                    
            strSql.Append(" paydate = @paydate , ");                                    
            strSql.Append(" Fmoney = @Fmoney , ");                                    
            strSql.Append(" SucMoney = @SucMoney , ");                                    
            strSql.Append(" ErrMoney = @ErrMoney , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@BankName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Dtype", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@paydate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Fmoney", SqlDbType.Decimal,17) ,            
                        new SqlParameter("@SucMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ErrMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.BankName;                        
            parameters[1].Value = model.AccNo;                        
            parameters[2].Value = model.Dtype;                        
            parameters[3].Value = model.paydate;                        
            parameters[4].Value = model.Fmoney;                        
            parameters[5].Value = model.SucMoney;                        
            parameters[6].Value = model.ErrMoney;                        
            parameters[7].Value = model.Remark;                        
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
			strSql.Append("delete from T_TmpRcvPay ");
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
		public SelfhelpOrderMgr.Model.T_TmpRcvPay GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BankName, AccNo, Dtype, paydate, Fmoney, SucMoney, ErrMoney, Remark  ");			
			strSql.Append("  from T_TmpRcvPay ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_TmpRcvPay model=new SelfhelpOrderMgr.Model.T_TmpRcvPay();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.BankName= ds.Tables[0].Rows[0]["BankName"].ToString();
																																model.AccNo= ds.Tables[0].Rows[0]["AccNo"].ToString();
																																model.Dtype= ds.Tables[0].Rows[0]["Dtype"].ToString();
																												if(ds.Tables[0].Rows[0]["paydate"].ToString()!="")
				{
					model.paydate=DateTime.Parse(ds.Tables[0].Rows[0]["paydate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Fmoney"].ToString()!="")
				{
					model.Fmoney=decimal.Parse(ds.Tables[0].Rows[0]["Fmoney"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["SucMoney"].ToString()!="")
				{
					model.SucMoney=decimal.Parse(ds.Tables[0].Rows[0]["SucMoney"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ErrMoney"].ToString()!="")
				{
					model.ErrMoney=decimal.Parse(ds.Tables[0].Rows[0]["ErrMoney"].ToString());
				}
																																				model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																										
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
			strSql.Append("select BankName, AccNo, Dtype, paydate, Fmoney, SucMoney, ErrMoney, Remark  ");
			strSql.Append(" FROM T_TmpRcvPay ");
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
			strSql.Append(" BankName, AccNo, Dtype, paydate, Fmoney, SucMoney, ErrMoney, Remark  ");
			strSql.Append(" FROM T_TmpRcvPay ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

