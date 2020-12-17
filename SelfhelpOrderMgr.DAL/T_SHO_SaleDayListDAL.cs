using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_SHO_SaleDayList
    public partial class T_SHO_SaleDayListDAL
    {

        public bool Exists(int Seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SHO_SaleDayList");
            strSql.Append(" where ");
            strSql.Append(" Seqno = @Seqno  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)			};
            parameters[0].Value = Seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_SHO_SaleDayList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SHO_SaleDayList(");
            strSql.Append("Seqno,SaleTypeId,PType,StartDay,EndDay,Flag,Remark,FAreaCode,LevelId");
            strSql.Append(") values (");
            strSql.Append("@Seqno,@SaleTypeId,@PType,@StartDay,@EndDay,@Flag,@Remark,@FAreaCode,@LevelId");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@SaleTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@PType", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@StartDay", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@EndDay", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LevelId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Seqno;
            parameters[1].Value = model.SaleTypeId;
            parameters[2].Value = model.PType;
            parameters[3].Value = model.StartDay;
            parameters[4].Value = model.EndDay;
            parameters[5].Value = model.Flag;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.FAreaCode;
            parameters[8].Value = model.LevelId;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_SaleDayList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SHO_SaleDayList set ");

            strSql.Append(" Seqno = @Seqno , ");
            strSql.Append(" SaleTypeId = @SaleTypeId , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" StartDay = @StartDay , ");
            strSql.Append(" EndDay = @EndDay , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" LevelId = @LevelId  ");
            strSql.Append(" where Seqno=@Seqno  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@SaleTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@PType", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@StartDay", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@EndDay", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LevelId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Seqno;
            parameters[1].Value = model.SaleTypeId;
            parameters[2].Value = model.PType;
            parameters[3].Value = model.StartDay;
            parameters[4].Value = model.EndDay;
            parameters[5].Value = model.Flag;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.FAreaCode;
            parameters[8].Value = model.LevelId;
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
        public bool Delete(int Seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_SaleDayList ");
            strSql.Append(" where Seqno=@Seqno ");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)			};
            parameters[0].Value = Seqno;


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
        public SelfhelpOrderMgr.Model.T_SHO_SaleDayList GetModel(int Seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Seqno, SaleTypeId, PType, StartDay, EndDay, Flag, Remark, FAreaCode, LevelId  ");
            strSql.Append("  from T_SHO_SaleDayList ");
            strSql.Append(" where Seqno=@Seqno ");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)			};
            parameters[0].Value = Seqno;


            SelfhelpOrderMgr.Model.T_SHO_SaleDayList model = new SelfhelpOrderMgr.Model.T_SHO_SaleDayList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Seqno"].ToString() != "")
                {
                    model.Seqno = int.Parse(ds.Tables[0].Rows[0]["Seqno"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SaleTypeId"].ToString() != "")
                {
                    model.SaleTypeId = int.Parse(ds.Tables[0].Rows[0]["SaleTypeId"].ToString());
                }
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                model.StartDay = ds.Tables[0].Rows[0]["StartDay"].ToString();
                model.EndDay = ds.Tables[0].Rows[0]["EndDay"].ToString();
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                if (ds.Tables[0].Rows[0]["LevelId"].ToString() != "")
                {
                    model.LevelId = int.Parse(ds.Tables[0].Rows[0]["LevelId"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_SHO_SaleDayList ");
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
            strSql.Append(" FROM T_SHO_SaleDayList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

