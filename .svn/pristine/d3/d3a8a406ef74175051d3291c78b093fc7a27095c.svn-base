using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Czy_priv
		public partial class T_Czy_privDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Czy_priv model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Czy_priv(");			
            strSql.Append("seqno,fcode,item01,item02,flag,itemname");
			strSql.Append(") values (");
            strSql.Append("@seqno,@fcode,@item01,@item02,@flag,@itemname");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@item01", SqlDbType.Int,4) ,            
                        new SqlParameter("@item02", SqlDbType.Int,4) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@itemname", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.item01;                        
            parameters[3].Value = model.item02;                        
            parameters[4].Value = model.flag;                        
            parameters[5].Value = model.itemname;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Czy_priv model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Czy_priv set ");
			                        
            strSql.Append(" seqno = @seqno , ");                                    
            strSql.Append(" fcode = @fcode , ");                                    
            strSql.Append(" item01 = @item01 , ");                                    
            strSql.Append(" item02 = @item02 , ");                                    
            strSql.Append(" flag = @flag , ");                                    
            strSql.Append(" itemname = @itemname  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@item01", SqlDbType.Int,4) ,            
                        new SqlParameter("@item02", SqlDbType.Int,4) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@itemname", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.item01;                        
            parameters[3].Value = model.item02;                        
            parameters[4].Value = model.flag;                        
            parameters[5].Value = model.itemname;                        
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
			strSql.Append("delete from T_Czy_priv ");
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
		public SelfhelpOrderMgr.Model.T_Czy_priv GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, fcode, item01, item02, flag, itemname  ");			
			strSql.Append("  from T_Czy_priv ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_Czy_priv model=new SelfhelpOrderMgr.Model.T_Czy_priv();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.fcode= ds.Tables[0].Rows[0]["fcode"].ToString();
																												if(ds.Tables[0].Rows[0]["item01"].ToString()!="")
				{
					model.item01=int.Parse(ds.Tables[0].Rows[0]["item01"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["item02"].ToString()!="")
				{
					model.item02=int.Parse(ds.Tables[0].Rows[0]["item02"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["flag"].ToString()!="")
				{
					model.flag=int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
				}
																																				model.itemname= ds.Tables[0].Rows[0]["itemname"].ToString();
																										
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
			strSql.Append("select seqno, fcode, item01, item02, flag, itemname  ");
			strSql.Append(" FROM T_Czy_priv ");
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
			strSql.Append(" seqno, fcode, item01, item02, flag, itemname  ");
			strSql.Append(" FROM T_Czy_priv ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

