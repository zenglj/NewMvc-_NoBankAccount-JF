using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Criminal_TPList
    public partial class T_Criminal_TPListDAL
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Criminal_TPList");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_Criminal_TPList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Criminal_TPList(");
            strSql.Append("SrcFileName,DelBy,DelDate,MoneyUseFlag,FCode,FName,TPMoney,CrtBy,CrtDate,EffectiveDate,FifoFlag,Remark");
            strSql.Append(") values (");
            strSql.Append("@SrcFileName,@DelBy,@DelDate,@MoneyUseFlag,@FCode,@FName,@TPMoney,@CrtBy,@CrtDate,@EffectiveDate,@FifoFlag,@Remark");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@SrcFileName", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@DelBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@DelDate", SqlDbType.NVarChar,30) ,            
                        new SqlParameter("@MoneyUseFlag", SqlDbType.Money,8) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TPMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EffectiveDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FifoFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,100)             
              
            };

            parameters[0].Value = model.SrcFileName;
            parameters[1].Value = model.DelBy;
            parameters[2].Value = model.DelDate;
            parameters[3].Value = model.MoneyUseFlag;
            parameters[4].Value = model.FCode;
            parameters[5].Value = model.FName;
            parameters[6].Value = model.TPMoney;
            parameters[7].Value = model.CrtBy;
            parameters[8].Value = model.CrtDate;
            parameters[9].Value = model.EffectiveDate;
            parameters[10].Value = model.FifoFlag;
            parameters[11].Value = model.Remark;

            object obj = SqlHelper.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal_TPList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Criminal_TPList set ");

            strSql.Append(" SrcFileName = @SrcFileName , ");
            strSql.Append(" DelBy = @DelBy , ");
            strSql.Append(" DelDate = @DelDate , ");
            strSql.Append(" MoneyUseFlag = @MoneyUseFlag , ");
            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" TPMoney = @TPMoney , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDate = @CrtDate , ");
            strSql.Append(" EffectiveDate = @EffectiveDate , ");
            strSql.Append(" FifoFlag = @FifoFlag , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@SrcFileName", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@DelBy", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@DelDate", SqlDbType.NVarChar,30) ,            
                        new SqlParameter("@MoneyUseFlag", SqlDbType.Money,8) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TPMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EffectiveDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FifoFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar,100)             
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.SrcFileName;
            parameters[2].Value = model.DelBy;
            parameters[3].Value = model.DelDate;
            parameters[4].Value = model.MoneyUseFlag;
            parameters[5].Value = model.FCode;
            parameters[6].Value = model.FName;
            parameters[7].Value = model.TPMoney;
            parameters[8].Value = model.CrtBy;
            parameters[9].Value = model.CrtDate;
            parameters[10].Value = model.EffectiveDate;
            parameters[11].Value = model.FifoFlag;
            parameters[12].Value = model.Remark;
            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Criminal_TPList ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Criminal_TPList ");
            strSql.Append(" where ID in (" + idlist + ")  ");
            int rows = SqlHelper.ExecuteSql(strSql.ToString());
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
        public SelfhelpOrderMgr.Model.T_Criminal_TPList GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, SrcFileName, DelBy, DelDate, MoneyUseFlag, FCode, FName, TPMoney, CrtBy, CrtDate, EffectiveDate, FifoFlag, Remark  ");
            strSql.Append("  from T_Criminal_TPList ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            SelfhelpOrderMgr.Model.T_Criminal_TPList model = new SelfhelpOrderMgr.Model.T_Criminal_TPList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.SrcFileName = ds.Tables[0].Rows[0]["SrcFileName"].ToString();
                model.DelBy = ds.Tables[0].Rows[0]["DelBy"].ToString();
                model.DelDate = ds.Tables[0].Rows[0]["DelDate"].ToString();
                if (ds.Tables[0].Rows[0]["MoneyUseFlag"].ToString() != "")
                {
                    model.MoneyUseFlag = decimal.Parse(ds.Tables[0].Rows[0]["MoneyUseFlag"].ToString());
                }
                model.FCode = ds.Tables[0].Rows[0]["FCode"].ToString();
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                if (ds.Tables[0].Rows[0]["TPMoney"].ToString() != "")
                {
                    model.TPMoney = decimal.Parse(ds.Tables[0].Rows[0]["TPMoney"].ToString());
                }
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDate"].ToString() != "")
                {
                    model.CrtDate = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EffectiveDate"].ToString() != "")
                {
                    model.EffectiveDate = DateTime.Parse(ds.Tables[0].Rows[0]["EffectiveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FifoFlag"].ToString() != "")
                {
                    model.FifoFlag = int.Parse(ds.Tables[0].Rows[0]["FifoFlag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Criminal_TPList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM T_Criminal_TPList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

