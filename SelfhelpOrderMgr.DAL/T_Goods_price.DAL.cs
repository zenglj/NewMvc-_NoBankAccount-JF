using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Goods_price
		public partial class T_Goods_priceDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Goods_price model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Goods_price(");			
            strSql.Append("gcode,gname,Gdj,BegDate,CRTBY,CRTDT,MODBY,MODDT,gorigdj,PlanPrice,ChkBy,ChkDate,ChkFlag,Remark,ChkInfo");
			strSql.Append(") values (");
            strSql.Append("@gcode,@gname,@Gdj,@BegDate,@CRTBY,@CRTDT,@MODBY,@MODDT,@gorigdj,@PlanPrice,@ChkBy,@ChkDate,@ChkFlag,@Remark,@ChkInfo");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@gcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@gname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Gdj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BegDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CRTBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CRTDT", SqlDbType.DateTime) ,            
                        new SqlParameter("@MODBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@MODDT", SqlDbType.DateTime) ,            
                        new SqlParameter("@gorigdj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PlanPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChkBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChkDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ChkFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChkInfo", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.gcode;                        
            parameters[1].Value = model.gname;                        
            parameters[2].Value = model.Gdj;                        
            parameters[3].Value = model.BegDate;                        
            parameters[4].Value = model.CRTBY;                        
            parameters[5].Value = model.CRTDT;                        
            parameters[6].Value = model.MODBY;                        
            parameters[7].Value = model.MODDT;                        
            parameters[8].Value = model.gorigdj;                        
            parameters[9].Value = model.PlanPrice;                        
            parameters[10].Value = model.ChkBy;                        
            parameters[11].Value = model.ChkDate;                        
            parameters[12].Value = model.ChkFlag;                        
            parameters[13].Value = model.Remark;                        
            parameters[14].Value = model.ChkInfo;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_Goods_price model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Goods_price set ");
			                                                
            strSql.Append(" gcode = @gcode , ");                                    
            strSql.Append(" gname = @gname , ");                                    
            strSql.Append(" Gdj = @Gdj , ");                                    
            strSql.Append(" BegDate = @BegDate , ");                                    
            strSql.Append(" CRTBY = @CRTBY , ");                                    
            strSql.Append(" CRTDT = @CRTDT , ");                                    
            strSql.Append(" MODBY = @MODBY , ");                                    
            strSql.Append(" MODDT = @MODDT , ");                                    
            strSql.Append(" gorigdj = @gorigdj , ");                                    
            strSql.Append(" PlanPrice = @PlanPrice , ");                                    
            strSql.Append(" ChkBy = @ChkBy , ");                                    
            strSql.Append(" ChkDate = @ChkDate , ");                                    
            strSql.Append(" ChkFlag = @ChkFlag , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" ChkInfo = @ChkInfo  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@gcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@gname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Gdj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BegDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CRTBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CRTDT", SqlDbType.DateTime) ,            
                        new SqlParameter("@MODBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@MODDT", SqlDbType.DateTime) ,            
                        new SqlParameter("@gorigdj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PlanPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChkBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChkDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ChkFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChkInfo", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.gcode;                        
            parameters[2].Value = model.gname;                        
            parameters[3].Value = model.Gdj;                        
            parameters[4].Value = model.BegDate;                        
            parameters[5].Value = model.CRTBY;                        
            parameters[6].Value = model.CRTDT;                        
            parameters[7].Value = model.MODBY;                        
            parameters[8].Value = model.MODDT;                        
            parameters[9].Value = model.gorigdj;                        
            parameters[10].Value = model.PlanPrice;                        
            parameters[11].Value = model.ChkBy;                        
            parameters[12].Value = model.ChkDate;                        
            parameters[13].Value = model.ChkFlag;                        
            parameters[14].Value = model.Remark;                        
            parameters[15].Value = model.ChkInfo;                        
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
			strSql.Append("delete from T_Goods_price ");
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
			strSql.Append("delete from T_Goods_price ");
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
		public SelfhelpOrderMgr.Model.T_Goods_price GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, gcode, gname, Gdj, BegDate, CRTBY, CRTDT, MODBY, MODDT, gorigdj, PlanPrice, ChkBy, ChkDate, ChkFlag, Remark, ChkInfo  ");			
			strSql.Append("  from T_Goods_price ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Goods_price model=new SelfhelpOrderMgr.Model.T_Goods_price();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.gcode= ds.Tables[0].Rows[0]["gcode"].ToString();
																																model.gname= ds.Tables[0].Rows[0]["gname"].ToString();
																												if(ds.Tables[0].Rows[0]["Gdj"].ToString()!="")
				{
					model.Gdj=decimal.Parse(ds.Tables[0].Rows[0]["Gdj"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["BegDate"].ToString()!="")
				{
					model.BegDate=DateTime.Parse(ds.Tables[0].Rows[0]["BegDate"].ToString());
				}
																																				model.CRTBY= ds.Tables[0].Rows[0]["CRTBY"].ToString();
																												if(ds.Tables[0].Rows[0]["CRTDT"].ToString()!="")
				{
					model.CRTDT=DateTime.Parse(ds.Tables[0].Rows[0]["CRTDT"].ToString());
				}
																																				model.MODBY= ds.Tables[0].Rows[0]["MODBY"].ToString();
																												if(ds.Tables[0].Rows[0]["MODDT"].ToString()!="")
				{
					model.MODDT=DateTime.Parse(ds.Tables[0].Rows[0]["MODDT"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["gorigdj"].ToString()!="")
				{
					model.gorigdj=decimal.Parse(ds.Tables[0].Rows[0]["gorigdj"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["PlanPrice"].ToString()!="")
				{
					model.PlanPrice=decimal.Parse(ds.Tables[0].Rows[0]["PlanPrice"].ToString());
				}
																																				model.ChkBy= ds.Tables[0].Rows[0]["ChkBy"].ToString();
																												if(ds.Tables[0].Rows[0]["ChkDate"].ToString()!="")
				{
					model.ChkDate=DateTime.Parse(ds.Tables[0].Rows[0]["ChkDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ChkFlag"].ToString()!="")
				{
					model.ChkFlag=int.Parse(ds.Tables[0].Rows[0]["ChkFlag"].ToString());
				}
																																				model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																																model.ChkInfo= ds.Tables[0].Rows[0]["ChkInfo"].ToString();
																										
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
			strSql.Append("select seqno, gcode, gname, Gdj, BegDate, CRTBY, CRTDT, MODBY, MODDT, gorigdj, PlanPrice, ChkBy, ChkDate, ChkFlag, Remark, ChkInfo  ");
			strSql.Append(" FROM T_Goods_price ");
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
			strSql.Append(" seqno, gcode, gname, Gdj, BegDate, CRTBY, CRTDT, MODBY, MODDT, gorigdj, PlanPrice, ChkBy, ChkDate, ChkFlag, Remark, ChkInfo  ");
			strSql.Append(" FROM T_Goods_price ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

