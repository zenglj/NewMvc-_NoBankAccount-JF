using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_EdiList
		public partial class T_EdiListDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_EdiList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_EdiList(");			
            strSql.Append("Code,sfile,UpLoadDate,DownLoadDate,UploadFlag,DownLoadFlag,InOutFlag,MainFlag,DetailFile,Mainseqno,DetailUploadflag,DetailDownloadFlag,DCFLAG,AccCode,Succflag,DetailUploadDate,DetailDownLoadDate,modecode,datadate,resetflag,feecode,remark,crtdate,typeflag,subTypeFlag,balflag,rseqno");
			strSql.Append(") values (");
            strSql.Append("@Code,@sfile,@UpLoadDate,@DownLoadDate,@UploadFlag,@DownLoadFlag,@InOutFlag,@MainFlag,@DetailFile,@Mainseqno,@DetailUploadflag,@DetailDownloadFlag,@DCFLAG,@AccCode,@Succflag,@DetailUploadDate,@DetailDownLoadDate,@modecode,@datadate,@resetflag,@feecode,@remark,@crtdate,@typeflag,@subTypeFlag,@balflag,@rseqno");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Code", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@sfile", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UpLoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@DownLoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UploadFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DownLoadFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@MainFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailFile", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Mainseqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailUploadflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailDownloadFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DCFLAG", SqlDbType.VarChar,1) ,            
                        new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Succflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailUploadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@DetailDownLoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@modecode", SqlDbType.Char,1) ,            
                        new SqlParameter("@datadate", SqlDbType.DateTime) ,            
                        new SqlParameter("@resetflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@feecode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@subTypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@balflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@rseqno", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.Code;                        
            parameters[1].Value = model.sfile;                        
            parameters[2].Value = model.UpLoadDate;                        
            parameters[3].Value = model.DownLoadDate;                        
            parameters[4].Value = model.UploadFlag;                        
            parameters[5].Value = model.DownLoadFlag;                        
            parameters[6].Value = model.InOutFlag;                        
            parameters[7].Value = model.MainFlag;                        
            parameters[8].Value = model.DetailFile;                        
            parameters[9].Value = model.Mainseqno;                        
            parameters[10].Value = model.DetailUploadflag;                        
            parameters[11].Value = model.DetailDownloadFlag;                        
            parameters[12].Value = model.DCFLAG;                        
            parameters[13].Value = model.AccCode;                        
            parameters[14].Value = model.Succflag;                        
            parameters[15].Value = model.DetailUploadDate;                        
            parameters[16].Value = model.DetailDownLoadDate;                        
            parameters[17].Value = model.modecode;                        
            parameters[18].Value = model.datadate;                        
            parameters[19].Value = model.resetflag;                        
            parameters[20].Value = model.feecode;                        
            parameters[21].Value = model.remark;                        
            parameters[22].Value = model.crtdate;                        
            parameters[23].Value = model.typeflag;                        
            parameters[24].Value = model.subTypeFlag;                        
            parameters[25].Value = model.balflag;                        
            parameters[26].Value = model.rseqno;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_EdiList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_EdiList set ");
			                                                
            strSql.Append(" Code = @Code , ");                                    
            strSql.Append(" sfile = @sfile , ");                                    
            strSql.Append(" UpLoadDate = @UpLoadDate , ");                                    
            strSql.Append(" DownLoadDate = @DownLoadDate , ");                                    
            strSql.Append(" UploadFlag = @UploadFlag , ");                                    
            strSql.Append(" DownLoadFlag = @DownLoadFlag , ");                                    
            strSql.Append(" InOutFlag = @InOutFlag , ");                                    
            strSql.Append(" MainFlag = @MainFlag , ");                                    
            strSql.Append(" DetailFile = @DetailFile , ");                                    
            strSql.Append(" Mainseqno = @Mainseqno , ");                                    
            strSql.Append(" DetailUploadflag = @DetailUploadflag , ");                                    
            strSql.Append(" DetailDownloadFlag = @DetailDownloadFlag , ");                                    
            strSql.Append(" DCFLAG = @DCFLAG , ");                                    
            strSql.Append(" AccCode = @AccCode , ");                                    
            strSql.Append(" Succflag = @Succflag , ");                                    
            strSql.Append(" DetailUploadDate = @DetailUploadDate , ");                                    
            strSql.Append(" DetailDownLoadDate = @DetailDownLoadDate , ");                                    
            strSql.Append(" modecode = @modecode , ");                                    
            strSql.Append(" datadate = @datadate , ");                                    
            strSql.Append(" resetflag = @resetflag , ");                                    
            strSql.Append(" feecode = @feecode , ");                                    
            strSql.Append(" remark = @remark , ");                                    
            strSql.Append(" crtdate = @crtdate , ");                                    
            strSql.Append(" typeflag = @typeflag , ");                                    
            strSql.Append(" subTypeFlag = @subTypeFlag , ");                                    
            strSql.Append(" balflag = @balflag , ");                                    
            strSql.Append(" rseqno = @rseqno  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@Code", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@sfile", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UpLoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@DownLoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@UploadFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DownLoadFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@MainFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailFile", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Mainseqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailUploadflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailDownloadFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DCFLAG", SqlDbType.VarChar,1) ,            
                        new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Succflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DetailUploadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@DetailDownLoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@modecode", SqlDbType.Char,1) ,            
                        new SqlParameter("@datadate", SqlDbType.DateTime) ,            
                        new SqlParameter("@resetflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@feecode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@subTypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@balflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@rseqno", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.Code;                        
            parameters[2].Value = model.sfile;                        
            parameters[3].Value = model.UpLoadDate;                        
            parameters[4].Value = model.DownLoadDate;                        
            parameters[5].Value = model.UploadFlag;                        
            parameters[6].Value = model.DownLoadFlag;                        
            parameters[7].Value = model.InOutFlag;                        
            parameters[8].Value = model.MainFlag;                        
            parameters[9].Value = model.DetailFile;                        
            parameters[10].Value = model.Mainseqno;                        
            parameters[11].Value = model.DetailUploadflag;                        
            parameters[12].Value = model.DetailDownloadFlag;                        
            parameters[13].Value = model.DCFLAG;                        
            parameters[14].Value = model.AccCode;                        
            parameters[15].Value = model.Succflag;                        
            parameters[16].Value = model.DetailUploadDate;                        
            parameters[17].Value = model.DetailDownLoadDate;                        
            parameters[18].Value = model.modecode;                        
            parameters[19].Value = model.datadate;                        
            parameters[20].Value = model.resetflag;                        
            parameters[21].Value = model.feecode;                        
            parameters[22].Value = model.remark;                        
            parameters[23].Value = model.crtdate;                        
            parameters[24].Value = model.typeflag;                        
            parameters[25].Value = model.subTypeFlag;                        
            parameters[26].Value = model.balflag;                        
            parameters[27].Value = model.rseqno;                        
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
			strSql.Append("delete from T_EdiList ");
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
			strSql.Append("delete from T_EdiList ");
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
		public SelfhelpOrderMgr.Model.T_EdiList GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, Code, sfile, UpLoadDate, DownLoadDate, UploadFlag, DownLoadFlag, InOutFlag, MainFlag, DetailFile, Mainseqno, DetailUploadflag, DetailDownloadFlag, DCFLAG, AccCode, Succflag, DetailUploadDate, DetailDownLoadDate, modecode, datadate, resetflag, feecode, remark, crtdate, typeflag, subTypeFlag, balflag, rseqno  ");			
			strSql.Append("  from T_EdiList ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_EdiList model=new SelfhelpOrderMgr.Model.T_EdiList();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.Code= ds.Tables[0].Rows[0]["Code"].ToString();
																																model.sfile= ds.Tables[0].Rows[0]["sfile"].ToString();
																												if(ds.Tables[0].Rows[0]["UpLoadDate"].ToString()!="")
				{
					model.UpLoadDate=DateTime.Parse(ds.Tables[0].Rows[0]["UpLoadDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DownLoadDate"].ToString()!="")
				{
					model.DownLoadDate=DateTime.Parse(ds.Tables[0].Rows[0]["DownLoadDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["UploadFlag"].ToString()!="")
				{
					model.UploadFlag=int.Parse(ds.Tables[0].Rows[0]["UploadFlag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DownLoadFlag"].ToString()!="")
				{
					model.DownLoadFlag=int.Parse(ds.Tables[0].Rows[0]["DownLoadFlag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["InOutFlag"].ToString()!="")
				{
					model.InOutFlag=int.Parse(ds.Tables[0].Rows[0]["InOutFlag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["MainFlag"].ToString()!="")
				{
					model.MainFlag=int.Parse(ds.Tables[0].Rows[0]["MainFlag"].ToString());
				}
																																				model.DetailFile= ds.Tables[0].Rows[0]["DetailFile"].ToString();
																												if(ds.Tables[0].Rows[0]["Mainseqno"].ToString()!="")
				{
					model.Mainseqno=int.Parse(ds.Tables[0].Rows[0]["Mainseqno"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DetailUploadflag"].ToString()!="")
				{
					model.DetailUploadflag=int.Parse(ds.Tables[0].Rows[0]["DetailUploadflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DetailDownloadFlag"].ToString()!="")
				{
					model.DetailDownloadFlag=int.Parse(ds.Tables[0].Rows[0]["DetailDownloadFlag"].ToString());
				}
																																				model.DCFLAG= ds.Tables[0].Rows[0]["DCFLAG"].ToString();
																																model.AccCode= ds.Tables[0].Rows[0]["AccCode"].ToString();
																												if(ds.Tables[0].Rows[0]["Succflag"].ToString()!="")
				{
					model.Succflag=int.Parse(ds.Tables[0].Rows[0]["Succflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DetailUploadDate"].ToString()!="")
				{
					model.DetailUploadDate=DateTime.Parse(ds.Tables[0].Rows[0]["DetailUploadDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DetailDownLoadDate"].ToString()!="")
				{
					model.DetailDownLoadDate=DateTime.Parse(ds.Tables[0].Rows[0]["DetailDownLoadDate"].ToString());
				}
																																				model.modecode= ds.Tables[0].Rows[0]["modecode"].ToString();
																												if(ds.Tables[0].Rows[0]["datadate"].ToString()!="")
				{
					model.datadate=DateTime.Parse(ds.Tables[0].Rows[0]["datadate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["resetflag"].ToString()!="")
				{
					model.resetflag=int.Parse(ds.Tables[0].Rows[0]["resetflag"].ToString());
				}
																																				model.feecode= ds.Tables[0].Rows[0]["feecode"].ToString();
																																model.remark= ds.Tables[0].Rows[0]["remark"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(ds.Tables[0].Rows[0]["crtdate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["typeflag"].ToString()!="")
				{
					model.typeflag=int.Parse(ds.Tables[0].Rows[0]["typeflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["subTypeFlag"].ToString()!="")
				{
					model.subTypeFlag=int.Parse(ds.Tables[0].Rows[0]["subTypeFlag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["balflag"].ToString()!="")
				{
					model.balflag=int.Parse(ds.Tables[0].Rows[0]["balflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["rseqno"].ToString()!="")
				{
					model.rseqno=int.Parse(ds.Tables[0].Rows[0]["rseqno"].ToString());
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
			strSql.Append("select seqno, Code, sfile, UpLoadDate, DownLoadDate, UploadFlag, DownLoadFlag, InOutFlag, MainFlag, DetailFile, Mainseqno, DetailUploadflag, DetailDownloadFlag, DCFLAG, AccCode, Succflag, DetailUploadDate, DetailDownLoadDate, modecode, datadate, resetflag, feecode, remark, crtdate, typeflag, subTypeFlag, balflag, rseqno  ");
			strSql.Append(" FROM T_EdiList ");
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
			strSql.Append(" seqno, Code, sfile, UpLoadDate, DownLoadDate, UploadFlag, DownLoadFlag, InOutFlag, MainFlag, DetailFile, Mainseqno, DetailUploadflag, DetailDownloadFlag, DCFLAG, AccCode, Succflag, DetailUploadDate, DetailDownLoadDate, modecode, datadate, resetflag, feecode, remark, crtdate, typeflag, subTypeFlag, balflag, rseqno  ");
			strSql.Append(" FROM T_EdiList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

