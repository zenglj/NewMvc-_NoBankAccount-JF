using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_CRIMINAL_LIST
		public partial class T_CRIMINAL_LISTDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_CRIMINAL_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_CRIMINAL_LIST(");			
            strSql.Append("FCode,FName,FIdenNo,FAge,FSex,FAddr,FCrimeCode,FCYCode,FTerm,FInDate,FOuDate,FAreaCode,FSubArea,FDesc,FStatus,FStatus2,FAddr_tmp,fczy,FModDate");
			strSql.Append(") values (");
            strSql.Append("@FCode,@FName,@FIdenNo,@FAge,@FSex,@FAddr,@FCrimeCode,@FCYCode,@FTerm,@FInDate,@FOuDate,@FAreaCode,@FSubArea,@FDesc,@FStatus,@FStatus2,@FAddr_tmp,@fczy,@FModDate");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAge", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FCYCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FTerm", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FInDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FOuDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FSubArea", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@FStatus2", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@FAddr_tmp", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fczy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FModDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FIdenNo;                        
            parameters[3].Value = model.FAge;                        
            parameters[4].Value = model.FSex;                        
            parameters[5].Value = model.FAddr;                        
            parameters[6].Value = model.FCrimeCode;                        
            parameters[7].Value = model.FCYCode;                        
            parameters[8].Value = model.FTerm;                        
            parameters[9].Value = model.FInDate;                        
            parameters[10].Value = model.FOuDate;                        
            parameters[11].Value = model.FAreaCode;                        
            parameters[12].Value = model.FSubArea;                        
            parameters[13].Value = model.FDesc;                        
            parameters[14].Value = model.FStatus;                        
            parameters[15].Value = model.FStatus2;                        
            parameters[16].Value = model.FAddr_tmp;                        
            parameters[17].Value = model.fczy;                        
            parameters[18].Value = model.FModDate;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_CRIMINAL_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_CRIMINAL_LIST set ");
			                        
            strSql.Append(" FCode = @FCode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" FIdenNo = @FIdenNo , ");                                    
            strSql.Append(" FAge = @FAge , ");                                    
            strSql.Append(" FSex = @FSex , ");                                    
            strSql.Append(" FAddr = @FAddr , ");                                    
            strSql.Append(" FCrimeCode = @FCrimeCode , ");                                    
            strSql.Append(" FCYCode = @FCYCode , ");                                    
            strSql.Append(" FTerm = @FTerm , ");                                    
            strSql.Append(" FInDate = @FInDate , ");                                    
            strSql.Append(" FOuDate = @FOuDate , ");                                    
            strSql.Append(" FAreaCode = @FAreaCode , ");                                    
            strSql.Append(" FSubArea = @FSubArea , ");                                    
            strSql.Append(" FDesc = @FDesc , ");                                    
            strSql.Append(" FStatus = @FStatus , ");                                    
            strSql.Append(" FStatus2 = @FStatus2 , ");                                    
            strSql.Append(" FAddr_tmp = @FAddr_tmp , ");                                    
            strSql.Append(" fczy = @fczy , ");                                    
            strSql.Append(" FModDate = @FModDate  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAge", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FCYCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FTerm", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FInDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FOuDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FSubArea", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@FStatus2", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@FAddr_tmp", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fczy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FModDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FIdenNo;                        
            parameters[3].Value = model.FAge;                        
            parameters[4].Value = model.FSex;                        
            parameters[5].Value = model.FAddr;                        
            parameters[6].Value = model.FCrimeCode;                        
            parameters[7].Value = model.FCYCode;                        
            parameters[8].Value = model.FTerm;                        
            parameters[9].Value = model.FInDate;                        
            parameters[10].Value = model.FOuDate;                        
            parameters[11].Value = model.FAreaCode;                        
            parameters[12].Value = model.FSubArea;                        
            parameters[13].Value = model.FDesc;                        
            parameters[14].Value = model.FStatus;                        
            parameters[15].Value = model.FStatus2;                        
            parameters[16].Value = model.FAddr_tmp;                        
            parameters[17].Value = model.fczy;                        
            parameters[18].Value = model.FModDate;                        
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
			strSql.Append("delete from T_CRIMINAL_LIST ");
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
		public SelfhelpOrderMgr.Model.T_CRIMINAL_LIST GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FCode, FName, FIdenNo, FAge, FSex, FAddr, FCrimeCode, FCYCode, FTerm, FInDate, FOuDate, FAreaCode, FSubArea, FDesc, FStatus, FStatus2, FAddr_tmp, fczy, FModDate  ");			
			strSql.Append("  from T_CRIMINAL_LIST ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_CRIMINAL_LIST model=new SelfhelpOrderMgr.Model.T_CRIMINAL_LIST();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.FCode= ds.Tables[0].Rows[0]["FCode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																																model.FIdenNo= ds.Tables[0].Rows[0]["FIdenNo"].ToString();
																												if(ds.Tables[0].Rows[0]["FAge"].ToString()!="")
				{
					model.FAge=int.Parse(ds.Tables[0].Rows[0]["FAge"].ToString());
				}
																																				model.FSex= ds.Tables[0].Rows[0]["FSex"].ToString();
																																model.FAddr= ds.Tables[0].Rows[0]["FAddr"].ToString();
																																model.FCrimeCode= ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
																																model.FCYCode= ds.Tables[0].Rows[0]["FCYCode"].ToString();
																																model.FTerm= ds.Tables[0].Rows[0]["FTerm"].ToString();
																												if(ds.Tables[0].Rows[0]["FInDate"].ToString()!="")
				{
					model.FInDate=DateTime.Parse(ds.Tables[0].Rows[0]["FInDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FOuDate"].ToString()!="")
				{
					model.FOuDate=DateTime.Parse(ds.Tables[0].Rows[0]["FOuDate"].ToString());
				}
																																				model.FAreaCode= ds.Tables[0].Rows[0]["FAreaCode"].ToString();
																																model.FSubArea= ds.Tables[0].Rows[0]["FSubArea"].ToString();
																																model.FDesc= ds.Tables[0].Rows[0]["FDesc"].ToString();
																												if(ds.Tables[0].Rows[0]["FStatus"].ToString()!="")
				{
					model.FStatus=int.Parse(ds.Tables[0].Rows[0]["FStatus"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FStatus2"].ToString()!="")
				{
					model.FStatus2=int.Parse(ds.Tables[0].Rows[0]["FStatus2"].ToString());
				}
																																				model.FAddr_tmp= ds.Tables[0].Rows[0]["FAddr_tmp"].ToString();
																																model.fczy= ds.Tables[0].Rows[0]["fczy"].ToString();
																												if(ds.Tables[0].Rows[0]["FModDate"].ToString()!="")
				{
					model.FModDate=DateTime.Parse(ds.Tables[0].Rows[0]["FModDate"].ToString());
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
			strSql.Append("select FCode, FName, FIdenNo, FAge, FSex, FAddr, FCrimeCode, FCYCode, FTerm, FInDate, FOuDate, FAreaCode, FSubArea, FDesc, FStatus, FStatus2, FAddr_tmp, fczy, FModDate  ");
			strSql.Append(" FROM T_CRIMINAL_LIST ");
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
			strSql.Append(" FCode, FName, FIdenNo, FAge, FSex, FAddr, FCrimeCode, FCYCode, FTerm, FInDate, FOuDate, FAreaCode, FSubArea, FDesc, FStatus, FStatus2, FAddr_tmp, fczy, FModDate  ");
			strSql.Append(" FROM T_CRIMINAL_LIST ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

