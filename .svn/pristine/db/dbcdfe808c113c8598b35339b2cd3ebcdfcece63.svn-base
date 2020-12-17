using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_EdiDetail
		public partial class T_EdiDetailDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(SelfhelpOrderMgr.Model.T_EdiDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_EdiDetail(");			
            strSql.Append("seqno,Mainseqno,vouno,DAMOUNT,CAMOUNT,FCRIMECODE,TYPEFLAG,SubTypeflag,AccCode,origid,SuccFlag,vcrdseqno,remark,fareacode");
			strSql.Append(") values (");
            strSql.Append("@seqno,@Mainseqno,@vouno,@DAMOUNT,@CAMOUNT,@FCRIMECODE,@TYPEFLAG,@SubTypeflag,@AccCode,@origid,@SuccFlag,@vcrdseqno,@remark,@fareacode");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@Mainseqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@vouno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DAMOUNT", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CAMOUNT", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FCRIMECODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TYPEFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@SubTypeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@origid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SuccFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@vcrdseqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,120) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.Mainseqno;                        
            parameters[2].Value = model.vouno;                        
            parameters[3].Value = model.DAMOUNT;                        
            parameters[4].Value = model.CAMOUNT;                        
            parameters[5].Value = model.FCRIMECODE;                        
            parameters[6].Value = model.TYPEFLAG;                        
            parameters[7].Value = model.SubTypeflag;                        
            parameters[8].Value = model.AccCode;                        
            parameters[9].Value = model.origid;                        
            parameters[10].Value = model.SuccFlag;                        
            parameters[11].Value = model.vcrdseqno;                        
            parameters[12].Value = model.remark;                        
            parameters[13].Value = model.fareacode;                        
			   
			object obj = SqlHelper.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                                    
            	return Convert.ToInt64(obj);
                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_EdiDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_EdiDetail set ");
			                                                
            strSql.Append(" seqno = @seqno , ");                                    
            strSql.Append(" Mainseqno = @Mainseqno , ");                                    
            strSql.Append(" vouno = @vouno , ");                                    
            strSql.Append(" DAMOUNT = @DAMOUNT , ");                                    
            strSql.Append(" CAMOUNT = @CAMOUNT , ");                                    
            strSql.Append(" FCRIMECODE = @FCRIMECODE , ");                                    
            strSql.Append(" TYPEFLAG = @TYPEFLAG , ");                                    
            strSql.Append(" SubTypeflag = @SubTypeflag , ");                                    
            strSql.Append(" AccCode = @AccCode , ");                                    
            strSql.Append(" origid = @origid , ");                                    
            strSql.Append(" SuccFlag = @SuccFlag , ");                                    
            strSql.Append(" vcrdseqno = @vcrdseqno , ");                                    
            strSql.Append(" remark = @remark , ");                                    
            strSql.Append(" fareacode = @fareacode  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@Mainseqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@vouno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DAMOUNT", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CAMOUNT", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FCRIMECODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@TYPEFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@SubTypeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@origid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SuccFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@vcrdseqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,120) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,20)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.seqno;                        
            parameters[2].Value = model.Mainseqno;                        
            parameters[3].Value = model.vouno;                        
            parameters[4].Value = model.DAMOUNT;                        
            parameters[5].Value = model.CAMOUNT;                        
            parameters[6].Value = model.FCRIMECODE;                        
            parameters[7].Value = model.TYPEFLAG;                        
            parameters[8].Value = model.SubTypeflag;                        
            parameters[9].Value = model.AccCode;                        
            parameters[10].Value = model.origid;                        
            parameters[11].Value = model.SuccFlag;                        
            parameters[12].Value = model.vcrdseqno;                        
            parameters[13].Value = model.remark;                        
            parameters[14].Value = model.fareacode;                        
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
		public bool Delete(long ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_EdiDetail ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
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
			strSql.Append("delete from T_EdiDetail ");
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
		public SelfhelpOrderMgr.Model.T_EdiDetail GetModel(long ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, seqno, Mainseqno, vouno, DAMOUNT, CAMOUNT, FCRIMECODE, TYPEFLAG, SubTypeflag, AccCode, origid, SuccFlag, vcrdseqno, remark, fareacode  ");			
			strSql.Append("  from T_EdiDetail ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
			parameters[0].Value = ID;

			
			SelfhelpOrderMgr.Model.T_EdiDetail model=new SelfhelpOrderMgr.Model.T_EdiDetail();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=long.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Mainseqno"].ToString()!="")
				{
					model.Mainseqno=int.Parse(ds.Tables[0].Rows[0]["Mainseqno"].ToString());
				}
																																				model.vouno= ds.Tables[0].Rows[0]["vouno"].ToString();
																												if(ds.Tables[0].Rows[0]["DAMOUNT"].ToString()!="")
				{
					model.DAMOUNT=decimal.Parse(ds.Tables[0].Rows[0]["DAMOUNT"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CAMOUNT"].ToString()!="")
				{
					model.CAMOUNT=decimal.Parse(ds.Tables[0].Rows[0]["CAMOUNT"].ToString());
				}
																																				model.FCRIMECODE= ds.Tables[0].Rows[0]["FCRIMECODE"].ToString();
																												if(ds.Tables[0].Rows[0]["TYPEFLAG"].ToString()!="")
				{
					model.TYPEFLAG=int.Parse(ds.Tables[0].Rows[0]["TYPEFLAG"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["SubTypeflag"].ToString()!="")
				{
					model.SubTypeflag=int.Parse(ds.Tables[0].Rows[0]["SubTypeflag"].ToString());
				}
																																				model.AccCode= ds.Tables[0].Rows[0]["AccCode"].ToString();
																																model.origid= ds.Tables[0].Rows[0]["origid"].ToString();
																												if(ds.Tables[0].Rows[0]["SuccFlag"].ToString()!="")
				{
					model.SuccFlag=int.Parse(ds.Tables[0].Rows[0]["SuccFlag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["vcrdseqno"].ToString()!="")
				{
					model.vcrdseqno=int.Parse(ds.Tables[0].Rows[0]["vcrdseqno"].ToString());
				}
																																				model.remark= ds.Tables[0].Rows[0]["remark"].ToString();
																																model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
																										
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
			strSql.Append("select ID, seqno, Mainseqno, vouno, DAMOUNT, CAMOUNT, FCRIMECODE, TYPEFLAG, SubTypeflag, AccCode, origid, SuccFlag, vcrdseqno, remark, fareacode  ");
			strSql.Append(" FROM T_EdiDetail ");
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
			strSql.Append(" ID, seqno, Mainseqno, vouno, DAMOUNT, CAMOUNT, FCRIMECODE, TYPEFLAG, SubTypeflag, AccCode, origid, SuccFlag, vcrdseqno, remark, fareacode  ");
			strSql.Append(" FROM T_EdiDetail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

