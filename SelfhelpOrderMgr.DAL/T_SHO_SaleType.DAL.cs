using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    //T_SHO_SaleType
    public partial class T_SHO_SaleTypeDAL
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SHO_SaleType");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)            };
            parameters[0].Value = Id;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_SHO_SaleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SHO_SaleType(");
            strSql.Append("Id,PType,TypeFlagId,CanconsumeAccount,FirstPaymentAccount,ShoppingFlag,Remark,Fifoflag,UseType,ControlName");
            strSql.Append(") values (");
            strSql.Append("@Id,@PType,@TypeFlagId,@CanconsumeAccount,@FirstPaymentAccount,@ShoppingFlag,@Remark,@Fifoflag,@UseType,@ControlName");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@PType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TypeFlagId", SqlDbType.Int,4) ,
                        new SqlParameter("@CanconsumeAccount", SqlDbType.Int,4) ,
                        new SqlParameter("@FirstPaymentAccount", SqlDbType.Int,4) ,
                        new SqlParameter("@ShoppingFlag", SqlDbType.Int,4) ,
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,
                        new SqlParameter("@Fifoflag", SqlDbType.Int,4),
                        new SqlParameter("@UseType", SqlDbType.Int,4),
                        new SqlParameter("@ControlName", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.PType;
            parameters[2].Value = model.TypeFlagId;
            parameters[3].Value = model.CanconsumeAccount;
            parameters[4].Value = model.FirstPaymentAccount;
            parameters[5].Value = model.ShoppingFlag;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Fifoflag;
            parameters[8].Value = model.UseType;
            parameters[9].Value = model.ControlName;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_SaleType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SHO_SaleType set ");

            strSql.Append(" Id = @Id , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" TypeFlagId = @TypeFlagId , ");
            strSql.Append(" CanconsumeAccount = @CanconsumeAccount , ");
            strSql.Append(" FirstPaymentAccount = @FirstPaymentAccount , ");
            strSql.Append(" ShoppingFlag = @ShoppingFlag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" Fifoflag = @Fifoflag,  ");
            strSql.Append(" UseType = @UseType,  ");
            strSql.Append(" ControlName = @ControlName  ");
            strSql.Append(" where Id=@Id  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@PType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TypeFlagId", SqlDbType.Int,4) ,
                        new SqlParameter("@CanconsumeAccount", SqlDbType.Int,4) ,
                        new SqlParameter("@FirstPaymentAccount", SqlDbType.Int,4) ,
                        new SqlParameter("@ShoppingFlag", SqlDbType.Int,4) ,
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,
                        new SqlParameter("@Fifoflag", SqlDbType.Int,4),
                        new SqlParameter("@UseType", SqlDbType.Int,4),
                        new SqlParameter("@ControlName", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.PType;
            parameters[2].Value = model.TypeFlagId;
            parameters[3].Value = model.CanconsumeAccount;
            parameters[4].Value = model.FirstPaymentAccount;
            parameters[5].Value = model.ShoppingFlag;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Fifoflag;
            parameters[8].Value = model.UseType;
            parameters[9].Value = model.ControlName;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_SaleType ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)            };
            parameters[0].Value = Id;


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
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_SaleType GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PType, TypeFlagId, CanconsumeAccount, FirstPaymentAccount, ShoppingFlag, Remark, Fifoflag ,UseType,ControlName");
            strSql.Append("  from T_SHO_SaleType ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)            };
            parameters[0].Value = Id;


            SelfhelpOrderMgr.Model.T_SHO_SaleType model = new SelfhelpOrderMgr.Model.T_SHO_SaleType();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                if (ds.Tables[0].Rows[0]["TypeFlagId"].ToString() != "")
                {
                    model.TypeFlagId = int.Parse(ds.Tables[0].Rows[0]["TypeFlagId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CanconsumeAccount"].ToString() != "")
                {
                    model.CanconsumeAccount = int.Parse(ds.Tables[0].Rows[0]["CanconsumeAccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FirstPaymentAccount"].ToString() != "")
                {
                    model.FirstPaymentAccount = int.Parse(ds.Tables[0].Rows[0]["FirstPaymentAccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShoppingFlag"].ToString() != "")
                {
                    model.ShoppingFlag = int.Parse(ds.Tables[0].Rows[0]["ShoppingFlag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["Fifoflag"].ToString() != "")
                {
                    model.Fifoflag = int.Parse(ds.Tables[0].Rows[0]["Fifoflag"].ToString());
                }

                if (ds.Tables[0].Rows[0]["UseType"].ToString() != "")
                {
                    model.UseType = int.Parse(ds.Tables[0].Rows[0]["UseType"].ToString());
                }

                model.ControlName = ds.Tables[0].Rows[0]["ControlName"].ToString();


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
            strSql.Append(" FROM T_SHO_SaleType ");
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
            strSql.Append(" FROM T_SHO_SaleType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

