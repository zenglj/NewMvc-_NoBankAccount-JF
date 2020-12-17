using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_XFQueryTmp
		public partial class t_XFQueryTmpDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.t_XFQueryTmp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_XFQueryTmp(");			
            strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,delby,deldate,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,senddate,Bankflag,checkflag,checkdate,checkby,pc,seqno,subtypeflag,rcvdate");
			strSql.Append(") values (");
            strSql.Append("@VOUNO,@cardcode,@fcrimecode,@DAMOUNT,@CAMOUNT,@crtBy,@CRTDATE,@DTYPE,@DEPOSITER,@REMARK,@flag,@delby,@deldate,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@ptype,@udate,@origid,@cardtype,@TYPEFLAG,@acctype,@senddate,@Bankflag,@checkflag,@checkdate,@checkby,@pc,@seqno,@subtypeflag,@rcvdate");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@VOUNO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@cardcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DAMOUNT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@CAMOUNT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@crtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CRTDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@DTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DEPOSITER", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@REMARK", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@delby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@deldate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@ptype", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@udate", SqlDbType.DateTime) ,            
                        new SqlParameter("@origid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@cardtype", SqlDbType.Int,4) ,            
                        new SqlParameter("@TYPEFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@acctype", SqlDbType.Int,4) ,            
                        new SqlParameter("@senddate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Bankflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@checkflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@checkdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@checkby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@subtypeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@rcvdate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.VOUNO;                        
            parameters[1].Value = model.cardcode;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.DAMOUNT;                        
            parameters[4].Value = model.CAMOUNT;                        
            parameters[5].Value = model.crtBy;                        
            parameters[6].Value = model.CRTDATE;                        
            parameters[7].Value = model.DTYPE;                        
            parameters[8].Value = model.DEPOSITER;                        
            parameters[9].Value = model.REMARK;                        
            parameters[10].Value = model.flag;                        
            parameters[11].Value = model.delby;                        
            parameters[12].Value = model.deldate;                        
            parameters[13].Value = model.fareacode;                        
            parameters[14].Value = model.fareaName;                        
            parameters[15].Value = model.fcriminal;                        
            parameters[16].Value = model.Frealareacode;                        
            parameters[17].Value = model.FrealAreaName;                        
            parameters[18].Value = model.ptype;                        
            parameters[19].Value = model.udate;                        
            parameters[20].Value = model.origid;                        
            parameters[21].Value = model.cardtype;                        
            parameters[22].Value = model.TYPEFLAG;                        
            parameters[23].Value = model.acctype;                        
            parameters[24].Value = model.senddate;                        
            parameters[25].Value = model.Bankflag;                        
            parameters[26].Value = model.checkflag;                        
            parameters[27].Value = model.checkdate;                        
            parameters[28].Value = model.checkby;                        
            parameters[29].Value = model.pc;                        
            parameters[30].Value = model.seqno;                        
            parameters[31].Value = model.subtypeflag;                        
            parameters[32].Value = model.rcvdate;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_XFQueryTmp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_XFQueryTmp set ");
			                        
            strSql.Append(" VOUNO = @VOUNO , ");                                    
            strSql.Append(" cardcode = @cardcode , ");                                    
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" DAMOUNT = @DAMOUNT , ");                                    
            strSql.Append(" CAMOUNT = @CAMOUNT , ");                                    
            strSql.Append(" crtBy = @crtBy , ");                                    
            strSql.Append(" CRTDATE = @CRTDATE , ");                                    
            strSql.Append(" DTYPE = @DTYPE , ");                                    
            strSql.Append(" DEPOSITER = @DEPOSITER , ");                                    
            strSql.Append(" REMARK = @REMARK , ");                                    
            strSql.Append(" flag = @flag , ");                                    
            strSql.Append(" delby = @delby , ");                                    
            strSql.Append(" deldate = @deldate , ");                                    
            strSql.Append(" fareacode = @fareacode , ");                                    
            strSql.Append(" fareaName = @fareaName , ");                                    
            strSql.Append(" fcriminal = @fcriminal , ");                                    
            strSql.Append(" Frealareacode = @Frealareacode , ");                                    
            strSql.Append(" FrealAreaName = @FrealAreaName , ");                                    
            strSql.Append(" ptype = @ptype , ");                                    
            strSql.Append(" udate = @udate , ");                                    
            strSql.Append(" origid = @origid , ");                                    
            strSql.Append(" cardtype = @cardtype , ");                                    
            strSql.Append(" TYPEFLAG = @TYPEFLAG , ");                                    
            strSql.Append(" acctype = @acctype , ");                                    
            strSql.Append(" senddate = @senddate , ");                                    
            strSql.Append(" Bankflag = @Bankflag , ");                                    
            strSql.Append(" checkflag = @checkflag , ");                                    
            strSql.Append(" checkdate = @checkdate , ");                                    
            strSql.Append(" checkby = @checkby , ");                                    
            strSql.Append(" pc = @pc , ");                                    
            strSql.Append(" seqno = @seqno , ");                                    
            strSql.Append(" subtypeflag = @subtypeflag , ");                                    
            strSql.Append(" rcvdate = @rcvdate  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@VOUNO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@cardcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DAMOUNT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@CAMOUNT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@crtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CRTDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@DTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DEPOSITER", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@REMARK", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@delby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@deldate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@ptype", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@udate", SqlDbType.DateTime) ,            
                        new SqlParameter("@origid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@cardtype", SqlDbType.Int,4) ,            
                        new SqlParameter("@TYPEFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@acctype", SqlDbType.Int,4) ,            
                        new SqlParameter("@senddate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Bankflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@checkflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@checkdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@checkby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@subtypeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@rcvdate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.VOUNO;                        
            parameters[1].Value = model.cardcode;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.DAMOUNT;                        
            parameters[4].Value = model.CAMOUNT;                        
            parameters[5].Value = model.crtBy;                        
            parameters[6].Value = model.CRTDATE;                        
            parameters[7].Value = model.DTYPE;                        
            parameters[8].Value = model.DEPOSITER;                        
            parameters[9].Value = model.REMARK;                        
            parameters[10].Value = model.flag;                        
            parameters[11].Value = model.delby;                        
            parameters[12].Value = model.deldate;                        
            parameters[13].Value = model.fareacode;                        
            parameters[14].Value = model.fareaName;                        
            parameters[15].Value = model.fcriminal;                        
            parameters[16].Value = model.Frealareacode;                        
            parameters[17].Value = model.FrealAreaName;                        
            parameters[18].Value = model.ptype;                        
            parameters[19].Value = model.udate;                        
            parameters[20].Value = model.origid;                        
            parameters[21].Value = model.cardtype;                        
            parameters[22].Value = model.TYPEFLAG;                        
            parameters[23].Value = model.acctype;                        
            parameters[24].Value = model.senddate;                        
            parameters[25].Value = model.Bankflag;                        
            parameters[26].Value = model.checkflag;                        
            parameters[27].Value = model.checkdate;                        
            parameters[28].Value = model.checkby;                        
            parameters[29].Value = model.pc;                        
            parameters[30].Value = model.seqno;                        
            parameters[31].Value = model.subtypeflag;                        
            parameters[32].Value = model.rcvdate;                        
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
			strSql.Append("delete from t_XFQueryTmp ");
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
		public SelfhelpOrderMgr.Model.t_XFQueryTmp GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select VOUNO, cardcode, fcrimecode, DAMOUNT, CAMOUNT, crtBy, CRTDATE, DTYPE, DEPOSITER, REMARK, flag, delby, deldate, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, ptype, udate, origid, cardtype, TYPEFLAG, acctype, senddate, Bankflag, checkflag, checkdate, checkby, pc, seqno, subtypeflag, rcvdate  ");			
			strSql.Append("  from t_XFQueryTmp ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.t_XFQueryTmp model=new SelfhelpOrderMgr.Model.t_XFQueryTmp();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.VOUNO= ds.Tables[0].Rows[0]["VOUNO"].ToString();
																																model.cardcode= ds.Tables[0].Rows[0]["cardcode"].ToString();
																																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																												if(ds.Tables[0].Rows[0]["DAMOUNT"].ToString()!="")
				{
					model.DAMOUNT=decimal.Parse(ds.Tables[0].Rows[0]["DAMOUNT"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CAMOUNT"].ToString()!="")
				{
					model.CAMOUNT=decimal.Parse(ds.Tables[0].Rows[0]["CAMOUNT"].ToString());
				}
																																				model.crtBy= ds.Tables[0].Rows[0]["crtBy"].ToString();
																												if(ds.Tables[0].Rows[0]["CRTDATE"].ToString()!="")
				{
					model.CRTDATE=DateTime.Parse(ds.Tables[0].Rows[0]["CRTDATE"].ToString());
				}
																																				model.DTYPE= ds.Tables[0].Rows[0]["DTYPE"].ToString();
																																model.DEPOSITER= ds.Tables[0].Rows[0]["DEPOSITER"].ToString();
																																model.REMARK= ds.Tables[0].Rows[0]["REMARK"].ToString();
																												if(ds.Tables[0].Rows[0]["flag"].ToString()!="")
				{
					model.flag=int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
				}
																																				model.delby= ds.Tables[0].Rows[0]["delby"].ToString();
																												if(ds.Tables[0].Rows[0]["deldate"].ToString()!="")
				{
					model.deldate=DateTime.Parse(ds.Tables[0].Rows[0]["deldate"].ToString());
				}
																																				model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
																																model.fareaName= ds.Tables[0].Rows[0]["fareaName"].ToString();
																																model.fcriminal= ds.Tables[0].Rows[0]["fcriminal"].ToString();
																																model.Frealareacode= ds.Tables[0].Rows[0]["Frealareacode"].ToString();
																																model.FrealAreaName= ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
																																model.ptype= ds.Tables[0].Rows[0]["ptype"].ToString();
																												if(ds.Tables[0].Rows[0]["udate"].ToString()!="")
				{
					model.udate=DateTime.Parse(ds.Tables[0].Rows[0]["udate"].ToString());
				}
																																				model.origid= ds.Tables[0].Rows[0]["origid"].ToString();
																												if(ds.Tables[0].Rows[0]["cardtype"].ToString()!="")
				{
					model.cardtype=int.Parse(ds.Tables[0].Rows[0]["cardtype"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["TYPEFLAG"].ToString()!="")
				{
					model.TYPEFLAG=int.Parse(ds.Tables[0].Rows[0]["TYPEFLAG"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["acctype"].ToString()!="")
				{
					model.acctype=int.Parse(ds.Tables[0].Rows[0]["acctype"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["senddate"].ToString()!="")
				{
					model.senddate=DateTime.Parse(ds.Tables[0].Rows[0]["senddate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Bankflag"].ToString()!="")
				{
					model.Bankflag=int.Parse(ds.Tables[0].Rows[0]["Bankflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["checkflag"].ToString()!="")
				{
					model.checkflag=int.Parse(ds.Tables[0].Rows[0]["checkflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["checkdate"].ToString()!="")
				{
					model.checkdate=DateTime.Parse(ds.Tables[0].Rows[0]["checkdate"].ToString());
				}
																																				model.checkby= ds.Tables[0].Rows[0]["checkby"].ToString();
																												if(ds.Tables[0].Rows[0]["pc"].ToString()!="")
				{
					model.pc=int.Parse(ds.Tables[0].Rows[0]["pc"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["subtypeflag"].ToString()!="")
				{
					model.subtypeflag=int.Parse(ds.Tables[0].Rows[0]["subtypeflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["rcvdate"].ToString()!="")
				{
					model.rcvdate=DateTime.Parse(ds.Tables[0].Rows[0]["rcvdate"].ToString());
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
			strSql.Append("select VOUNO, cardcode, fcrimecode, DAMOUNT, CAMOUNT, crtBy, CRTDATE, DTYPE, DEPOSITER, REMARK, flag, delby, deldate, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, ptype, udate, origid, cardtype, TYPEFLAG, acctype, senddate, Bankflag, checkflag, checkdate, checkby, pc, seqno, subtypeflag, rcvdate  ");
			strSql.Append(" FROM t_XFQueryTmp ");
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
			strSql.Append(" VOUNO, cardcode, fcrimecode, DAMOUNT, CAMOUNT, crtBy, CRTDATE, DTYPE, DEPOSITER, REMARK, flag, delby, deldate, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, ptype, udate, origid, cardtype, TYPEFLAG, acctype, senddate, Bankflag, checkflag, checkdate, checkby, pc, seqno, subtypeflag, rcvdate  ");
			strSql.Append(" FROM t_XFQueryTmp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

