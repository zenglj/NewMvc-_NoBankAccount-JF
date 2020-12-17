using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Vcrd_error
		public partial class T_Vcrd_errorDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Vcrd_error model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Vcrd_error(");			
            strSql.Append("fcrimecode,fcriminal,famount,famounta,famountb,Remark,Crtby,crtdate,pc,typeflag,acctype,notes");
			strSql.Append(") values (");
            strSql.Append("@fcrimecode,@fcriminal,@famount,@famounta,@famountb,@Remark,@Crtby,@crtdate,@pc,@typeflag,@acctype,@notes");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@famount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@famounta", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@famountb", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,125) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@acctype", SqlDbType.Int,4) ,            
                        new SqlParameter("@notes", SqlDbType.VarChar,125)             
              
            };
			            
            parameters[0].Value = model.fcrimecode;                        
            parameters[1].Value = model.fcriminal;                        
            parameters[2].Value = model.famount;                        
            parameters[3].Value = model.famounta;                        
            parameters[4].Value = model.famountb;                        
            parameters[5].Value = model.Remark;                        
            parameters[6].Value = model.Crtby;                        
            parameters[7].Value = model.crtdate;                        
            parameters[8].Value = model.pc;                        
            parameters[9].Value = model.typeflag;                        
            parameters[10].Value = model.acctype;                        
            parameters[11].Value = model.notes;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_Vcrd_error model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Vcrd_error set ");
			                                                
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" fcriminal = @fcriminal , ");                                    
            strSql.Append(" famount = @famount , ");                                    
            strSql.Append(" famounta = @famounta , ");                                    
            strSql.Append(" famountb = @famountb , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" Crtby = @Crtby , ");                                    
            strSql.Append(" crtdate = @crtdate , ");                                    
            strSql.Append(" pc = @pc , ");                                    
            strSql.Append(" typeflag = @typeflag , ");                                    
            strSql.Append(" acctype = @acctype , ");                                    
            strSql.Append(" notes = @notes  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@famount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@famounta", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@famountb", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,125) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@acctype", SqlDbType.Int,4) ,            
                        new SqlParameter("@notes", SqlDbType.VarChar,125)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fcrimecode;                        
            parameters[2].Value = model.fcriminal;                        
            parameters[3].Value = model.famount;                        
            parameters[4].Value = model.famounta;                        
            parameters[5].Value = model.famountb;                        
            parameters[6].Value = model.Remark;                        
            parameters[7].Value = model.Crtby;                        
            parameters[8].Value = model.crtdate;                        
            parameters[9].Value = model.pc;                        
            parameters[10].Value = model.typeflag;                        
            parameters[11].Value = model.acctype;                        
            parameters[12].Value = model.notes;                        
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
		public bool Delete(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Vcrd_error ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;


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
		public bool DeleteList(string seqnolist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Vcrd_error ");
			strSql.Append(" where ID in ("+seqnolist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_Vcrd_error GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, fcrimecode, fcriminal, famount, famounta, famountb, Remark, Crtby, crtdate, pc, typeflag, acctype, notes  ");			
			strSql.Append("  from T_Vcrd_error ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Vcrd_error model=new SelfhelpOrderMgr.Model.T_Vcrd_error();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																																model.fcriminal= ds.Tables[0].Rows[0]["fcriminal"].ToString();
																												if(ds.Tables[0].Rows[0]["famount"].ToString()!="")
				{
					model.famount=decimal.Parse(ds.Tables[0].Rows[0]["famount"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["famounta"].ToString()!="")
				{
					model.famounta=decimal.Parse(ds.Tables[0].Rows[0]["famounta"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["famountb"].ToString()!="")
				{
					model.famountb=decimal.Parse(ds.Tables[0].Rows[0]["famountb"].ToString());
				}
																																				model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																																model.Crtby= ds.Tables[0].Rows[0]["Crtby"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(ds.Tables[0].Rows[0]["crtdate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["pc"].ToString()!="")
				{
					model.pc=int.Parse(ds.Tables[0].Rows[0]["pc"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["typeflag"].ToString()!="")
				{
					model.typeflag=int.Parse(ds.Tables[0].Rows[0]["typeflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["acctype"].ToString()!="")
				{
					model.acctype=int.Parse(ds.Tables[0].Rows[0]["acctype"].ToString());
				}
																																				model.notes= ds.Tables[0].Rows[0]["notes"].ToString();
																										
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
			strSql.Append("select seqno, fcrimecode, fcriminal, famount, famounta, famountb, Remark, Crtby, crtdate, pc, typeflag, acctype, notes  ");
			strSql.Append(" FROM T_Vcrd_error ");
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
			strSql.Append(" seqno, fcrimecode, fcriminal, famount, famounta, famountb, Remark, Crtby, crtdate, pc, typeflag, acctype, notes  ");
			strSql.Append(" FROM T_Vcrd_error ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

