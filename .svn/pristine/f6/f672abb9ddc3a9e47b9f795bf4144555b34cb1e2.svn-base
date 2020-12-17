using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_bankinfo
		public partial class t_bankinfoDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.t_bankinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_bankinfo(");			
            strSql.Append("AccCode,BankName,TradeCode,TradeBankNo,inoutflag,TradeTerminal,TradeBy,compcode,portcode,AgentPort,Custid_type,cust_id,FeeCode,MainFeeCode");
			strSql.Append(") values (");
            strSql.Append("@AccCode,@BankName,@TradeCode,@TradeBankNo,@inoutflag,@TradeTerminal,@TradeBy,@compcode,@portcode,@AgentPort,@Custid_type,@cust_id,@FeeCode,@MainFeeCode");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankName", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@TradeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TradeBankNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@inoutflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@TradeTerminal", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TradeBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@compcode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@portcode", SqlDbType.Int,4) ,            
                        new SqlParameter("@AgentPort", SqlDbType.Int,4) ,            
                        new SqlParameter("@Custid_type", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@cust_id", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FeeCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@MainFeeCode", SqlDbType.VarChar,10)             
              
            };
			            
            parameters[0].Value = model.AccCode;                        
            parameters[1].Value = model.BankName;                        
            parameters[2].Value = model.TradeCode;                        
            parameters[3].Value = model.TradeBankNo;                        
            parameters[4].Value = model.inoutflag;                        
            parameters[5].Value = model.TradeTerminal;                        
            parameters[6].Value = model.TradeBy;                        
            parameters[7].Value = model.compcode;                        
            parameters[8].Value = model.portcode;                        
            parameters[9].Value = model.AgentPort;                        
            parameters[10].Value = model.Custid_type;                        
            parameters[11].Value = model.cust_id;                        
            parameters[12].Value = model.FeeCode;                        
            parameters[13].Value = model.MainFeeCode;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bankinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_bankinfo set ");
			                        
            strSql.Append(" AccCode = @AccCode , ");                                    
            strSql.Append(" BankName = @BankName , ");                                    
            strSql.Append(" TradeCode = @TradeCode , ");                                    
            strSql.Append(" TradeBankNo = @TradeBankNo , ");                                    
            strSql.Append(" inoutflag = @inoutflag , ");                                    
            strSql.Append(" TradeTerminal = @TradeTerminal , ");                                    
            strSql.Append(" TradeBy = @TradeBy , ");                                    
            strSql.Append(" compcode = @compcode , ");                                    
            strSql.Append(" portcode = @portcode , ");                                    
            strSql.Append(" AgentPort = @AgentPort , ");                                    
            strSql.Append(" Custid_type = @Custid_type , ");                                    
            strSql.Append(" cust_id = @cust_id , ");                                    
            strSql.Append(" FeeCode = @FeeCode , ");                                    
            strSql.Append(" MainFeeCode = @MainFeeCode  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankName", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@TradeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TradeBankNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@inoutflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@TradeTerminal", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TradeBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@compcode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@portcode", SqlDbType.Int,4) ,            
                        new SqlParameter("@AgentPort", SqlDbType.Int,4) ,            
                        new SqlParameter("@Custid_type", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@cust_id", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FeeCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@MainFeeCode", SqlDbType.VarChar,10)             
              
            };
						            
            parameters[0].Value = model.AccCode;                        
            parameters[1].Value = model.BankName;                        
            parameters[2].Value = model.TradeCode;                        
            parameters[3].Value = model.TradeBankNo;                        
            parameters[4].Value = model.inoutflag;                        
            parameters[5].Value = model.TradeTerminal;                        
            parameters[6].Value = model.TradeBy;                        
            parameters[7].Value = model.compcode;                        
            parameters[8].Value = model.portcode;                        
            parameters[9].Value = model.AgentPort;                        
            parameters[10].Value = model.Custid_type;                        
            parameters[11].Value = model.cust_id;                        
            parameters[12].Value = model.FeeCode;                        
            parameters[13].Value = model.MainFeeCode;                        
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
			strSql.Append("delete from t_bankinfo ");
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
		public SelfhelpOrderMgr.Model.t_bankinfo GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AccCode, BankName, TradeCode, TradeBankNo, inoutflag, TradeTerminal, TradeBy, compcode, portcode, AgentPort, Custid_type, cust_id, FeeCode, MainFeeCode  ");			
			strSql.Append("  from t_bankinfo ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.t_bankinfo model=new SelfhelpOrderMgr.Model.t_bankinfo();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.AccCode= ds.Tables[0].Rows[0]["AccCode"].ToString();
																																model.BankName= ds.Tables[0].Rows[0]["BankName"].ToString();
																																model.TradeCode= ds.Tables[0].Rows[0]["TradeCode"].ToString();
																																model.TradeBankNo= ds.Tables[0].Rows[0]["TradeBankNo"].ToString();
																												if(ds.Tables[0].Rows[0]["inoutflag"].ToString()!="")
				{
					model.inoutflag=int.Parse(ds.Tables[0].Rows[0]["inoutflag"].ToString());
				}
																																				model.TradeTerminal= ds.Tables[0].Rows[0]["TradeTerminal"].ToString();
																																model.TradeBy= ds.Tables[0].Rows[0]["TradeBy"].ToString();
																																model.compcode= ds.Tables[0].Rows[0]["compcode"].ToString();
																												if(ds.Tables[0].Rows[0]["portcode"].ToString()!="")
				{
					model.portcode=int.Parse(ds.Tables[0].Rows[0]["portcode"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["AgentPort"].ToString()!="")
				{
					model.AgentPort=int.Parse(ds.Tables[0].Rows[0]["AgentPort"].ToString());
				}
																																				model.Custid_type= ds.Tables[0].Rows[0]["Custid_type"].ToString();
																																model.cust_id= ds.Tables[0].Rows[0]["cust_id"].ToString();
																																model.FeeCode= ds.Tables[0].Rows[0]["FeeCode"].ToString();
																																model.MainFeeCode= ds.Tables[0].Rows[0]["MainFeeCode"].ToString();
																										
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
			strSql.Append("select AccCode, BankName, TradeCode, TradeBankNo, inoutflag, TradeTerminal, TradeBy, compcode, portcode, AgentPort, Custid_type, cust_id, FeeCode, MainFeeCode  ");
			strSql.Append(" FROM t_bankinfo ");
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
			strSql.Append(" AccCode, BankName, TradeCode, TradeBankNo, inoutflag, TradeTerminal, TradeBy, compcode, portcode, AgentPort, Custid_type, cust_id, FeeCode, MainFeeCode  ");
			strSql.Append(" FROM t_bankinfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

