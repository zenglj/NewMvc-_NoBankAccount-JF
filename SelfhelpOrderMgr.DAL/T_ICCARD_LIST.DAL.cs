using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_ICCARD_LIST
		public partial class T_ICCARD_LISTDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_ICCARD_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_ICCARD_LIST(");			
            strSql.Append("SEQNO,CardCode,FPWD,FCrimeCode,FRCZY,FRDate,FDELCZY,FDelDate,Amount,FFlag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,cardtype");
			strSql.Append(") values (");
            strSql.Append("@SEQNO,@CardCode,@FPWD,@FCrimeCode,@FRCZY,@FRDate,@FDELCZY,@FDelDate,@Amount,@FFlag,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@cardtype");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@SEQNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPWD", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRCZY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FDELCZY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FDelDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@cardtype", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.SEQNO;                        
            parameters[1].Value = model.CardCode;                        
            parameters[2].Value = model.FPWD;                        
            parameters[3].Value = model.FCrimeCode;                        
            parameters[4].Value = model.FRCZY;                        
            parameters[5].Value = model.FRDate;                        
            parameters[6].Value = model.FDELCZY;                        
            parameters[7].Value = model.FDelDate;                        
            parameters[8].Value = model.Amount;                        
            parameters[9].Value = model.FFlag;                        
            parameters[10].Value = model.fareacode;                        
            parameters[11].Value = model.fareaName;                        
            parameters[12].Value = model.fcriminal;                        
            parameters[13].Value = model.Frealareacode;                        
            parameters[14].Value = model.FrealAreaName;                        
            parameters[15].Value = model.cardtype;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_ICCARD_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_ICCARD_LIST set ");
			                        
            strSql.Append(" SEQNO = @SEQNO , ");                                    
            strSql.Append(" CardCode = @CardCode , ");                                    
            strSql.Append(" FPWD = @FPWD , ");                                    
            strSql.Append(" FCrimeCode = @FCrimeCode , ");                                    
            strSql.Append(" FRCZY = @FRCZY , ");                                    
            strSql.Append(" FRDate = @FRDate , ");                                    
            strSql.Append(" FDELCZY = @FDELCZY , ");                                    
            strSql.Append(" FDelDate = @FDelDate , ");                                    
            strSql.Append(" Amount = @Amount , ");                                    
            strSql.Append(" FFlag = @FFlag , ");                                    
            strSql.Append(" fareacode = @fareacode , ");                                    
            strSql.Append(" fareaName = @fareaName , ");                                    
            strSql.Append(" fcriminal = @fcriminal , ");                                    
            strSql.Append(" Frealareacode = @Frealareacode , ");                                    
            strSql.Append(" FrealAreaName = @FrealAreaName , ");                                    
            strSql.Append(" cardtype = @cardtype  ");
            strSql.Append(" where CardCode=@CardCode ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@SEQNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPWD", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRCZY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FDELCZY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FDelDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@cardtype", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.SEQNO;                        
            parameters[1].Value = model.CardCode;                        
            parameters[2].Value = model.FPWD;                        
            parameters[3].Value = model.FCrimeCode;                        
            parameters[4].Value = model.FRCZY;                        
            parameters[5].Value = model.FRDate;                        
            parameters[6].Value = model.FDELCZY;                        
            parameters[7].Value = model.FDelDate;                        
            parameters[8].Value = model.Amount;                        
            parameters[9].Value = model.FFlag;                        
            parameters[10].Value = model.fareacode;                        
            parameters[11].Value = model.fareaName;                        
            parameters[12].Value = model.fcriminal;                        
            parameters[13].Value = model.Frealareacode;                        
            parameters[14].Value = model.FrealAreaName;                        
            parameters[15].Value = model.cardtype;                        
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
			strSql.Append("delete from T_ICCARD_LIST ");
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
		public SelfhelpOrderMgr.Model.T_ICCARD_LIST GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SEQNO, CardCode, FPWD, FCrimeCode, FRCZY, FRDate, FDELCZY, FDelDate, Amount, FFlag, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, cardtype  ");			
			strSql.Append("  from T_ICCARD_LIST ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_ICCARD_LIST model=new SelfhelpOrderMgr.Model.T_ICCARD_LIST();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SEQNO"].ToString()!="")
				{
					model.SEQNO=int.Parse(ds.Tables[0].Rows[0]["SEQNO"].ToString());
				}
				model.CardCode= ds.Tables[0].Rows[0]["CardCode"].ToString();
				model.FPWD= ds.Tables[0].Rows[0]["FPWD"].ToString();
				model.FCrimeCode= ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
				model.FRCZY= ds.Tables[0].Rows[0]["FRCZY"].ToString();
				if(ds.Tables[0].Rows[0]["FRDate"].ToString()!="")
				{
					model.FRDate=DateTime.Parse(ds.Tables[0].Rows[0]["FRDate"].ToString());
				}
                else
                {
                    model.FRDate = DateTime.Parse("1900-01-01");
                }
				model.FDELCZY= ds.Tables[0].Rows[0]["FDELCZY"].ToString();
				if(ds.Tables[0].Rows[0]["FDelDate"].ToString()!="")
				{
					model.FDelDate=DateTime.Parse(ds.Tables[0].Rows[0]["FDelDate"].ToString());
				}
                else
                {
                    model.FDelDate = DateTime.Parse("1900-01-01");
                }
				if(ds.Tables[0].Rows[0]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FFlag"].ToString()!="")
				{
					model.FFlag=int.Parse(ds.Tables[0].Rows[0]["FFlag"].ToString());
				}
				model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
				model.fareaName= ds.Tables[0].Rows[0]["fareaName"].ToString();
				model.fcriminal= ds.Tables[0].Rows[0]["fcriminal"].ToString();
				model.Frealareacode= ds.Tables[0].Rows[0]["Frealareacode"].ToString();
				model.FrealAreaName= ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
				if(ds.Tables[0].Rows[0]["cardtype"].ToString()!="")
				{
					model.cardtype=int.Parse(ds.Tables[0].Rows[0]["cardtype"].ToString());
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
			strSql.Append("select SEQNO, CardCode, FPWD, FCrimeCode, FRCZY, FRDate, FDELCZY, FDelDate, Amount, FFlag, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, cardtype  ");
			strSql.Append(" FROM T_ICCARD_LIST ");
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
			strSql.Append(" SEQNO, CardCode, FPWD, FCrimeCode, FRCZY, FRDate, FDELCZY, FDelDate, Amount, FFlag, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, cardtype  ");
			strSql.Append(" FROM T_ICCARD_LIST ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

