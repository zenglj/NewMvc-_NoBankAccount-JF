using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_FAMILY_LIST
		public partial class T_FAMILY_LISTDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_FAMILY_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_FAMILY_LIST(");			
            strSql.Append("FCrimeCode,FName,FIdenNo,FSex,FAddr,FRelation,FDesc,FStatus,FAddr_tmp,PICDATA,fczy,FModDate");
			strSql.Append(") values (");
            strSql.Append("@FCrimeCode,@FName,@FIdenNo,@FSex,@FAddr,@FRelation,@FDesc,@FStatus,@FAddr_tmp,@PICDATA,@fczy,@FModDate");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@FRelation", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,64) ,            
                        new SqlParameter("@FStatus", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@FAddr_tmp", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PICDATA", SqlDbType.Image) ,            
                        new SqlParameter("@fczy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FModDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.FCrimeCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FIdenNo;                        
            parameters[3].Value = model.FSex;                        
            parameters[4].Value = model.FAddr;                        
            parameters[5].Value = model.FRelation;                        
            parameters[6].Value = model.FDesc;                        
            parameters[7].Value = model.FStatus;                        
            parameters[8].Value = model.FAddr_tmp;                        
            parameters[9].Value = model.PICDATA;                        
            parameters[10].Value = model.fczy;                        
            parameters[11].Value = model.FModDate;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_FAMILY_LIST model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_FAMILY_LIST set ");
			                                                
            strSql.Append(" FCrimeCode = @FCrimeCode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" FIdenNo = @FIdenNo , ");                                    
            strSql.Append(" FSex = @FSex , ");                                    
            strSql.Append(" FAddr = @FAddr , ");                                    
            strSql.Append(" FRelation = @FRelation , ");                                    
            strSql.Append(" FDesc = @FDesc , ");                                    
            strSql.Append(" FStatus = @FStatus , ");                                    
            strSql.Append(" FAddr_tmp = @FAddr_tmp , ");                                    
            strSql.Append(" PICDATA = @PICDATA , ");                                    
            strSql.Append(" fczy = @fczy , ");                                    
            strSql.Append(" FModDate = @FModDate  ");            			
			strSql.Append(" where FCode=@FCode ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@FRelation", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,64) ,            
                        new SqlParameter("@FStatus", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@FAddr_tmp", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PICDATA", SqlDbType.Image) ,            
                        new SqlParameter("@fczy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FModDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FCrimeCode;                        
            parameters[2].Value = model.FName;                        
            parameters[3].Value = model.FIdenNo;                        
            parameters[4].Value = model.FSex;                        
            parameters[5].Value = model.FAddr;                        
            parameters[6].Value = model.FRelation;                        
            parameters[7].Value = model.FDesc;                        
            parameters[8].Value = model.FStatus;                        
            parameters[9].Value = model.FAddr_tmp;                        
            parameters[10].Value = model.PICDATA;                        
            parameters[11].Value = model.fczy;                        
            parameters[12].Value = model.FModDate;                        
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
		public bool Delete(int FCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_FAMILY_LIST ");
			strSql.Append(" where FCode=@FCode");
						SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.Int,4)
			};
			parameters[0].Value = FCode;


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
		public bool DeleteList(string FCodelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_FAMILY_LIST ");
			strSql.Append(" where ID in ("+FCodelist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_FAMILY_LIST GetModel(int FCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FCode, FCrimeCode, FName, FIdenNo, FSex, FAddr, FRelation, FDesc, FStatus, FAddr_tmp, PICDATA, fczy, FModDate  ");			
			strSql.Append("  from T_FAMILY_LIST ");
			strSql.Append(" where FCode=@FCode");
						SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.Int,4)
			};
			parameters[0].Value = FCode;

			
			SelfhelpOrderMgr.Model.T_FAMILY_LIST model=new SelfhelpOrderMgr.Model.T_FAMILY_LIST();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["FCode"].ToString()!="")
				{
					model.FCode=int.Parse(ds.Tables[0].Rows[0]["FCode"].ToString());
				}
																																				model.FCrimeCode= ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																																model.FIdenNo= ds.Tables[0].Rows[0]["FIdenNo"].ToString();
																																model.FSex= ds.Tables[0].Rows[0]["FSex"].ToString();
																																model.FAddr= ds.Tables[0].Rows[0]["FAddr"].ToString();
																																model.FRelation= ds.Tables[0].Rows[0]["FRelation"].ToString();
																																model.FDesc= ds.Tables[0].Rows[0]["FDesc"].ToString();
																												if(ds.Tables[0].Rows[0]["FStatus"].ToString()!="")
				{
					model.FStatus=int.Parse(ds.Tables[0].Rows[0]["FStatus"].ToString());
				}
																																				model.FAddr_tmp= ds.Tables[0].Rows[0]["FAddr_tmp"].ToString();
																																				if(ds.Tables[0].Rows[0]["PICDATA"].ToString()!="")
				{
					model.PICDATA= (byte[])ds.Tables[0].Rows[0]["PICDATA"];
				}
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
			strSql.Append("select FCode, FCrimeCode, FName, FIdenNo, FSex, FAddr, FRelation, FDesc, FStatus, FAddr_tmp, PICDATA, fczy, FModDate  ");
			strSql.Append(" FROM T_FAMILY_LIST ");
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
			strSql.Append(" FCode, FCrimeCode, FName, FIdenNo, FSex, FAddr, FRelation, FDesc, FStatus, FAddr_tmp, PICDATA, fczy, FModDate  ");
			strSql.Append(" FROM T_FAMILY_LIST ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

