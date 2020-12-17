using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_BatchMoneyTrade
    public partial class T_BatchMoneyTradeDAL
    {

        public bool Exists(string Bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_BatchMoneyTrade");
            strSql.Append(" where ");
            strSql.Append(" Bid = @Bid  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Bid", SqlDbType.VarChar,20)			};
            parameters[0].Value = Bid;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_BatchMoneyTrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_BatchMoneyTrade(");
            strSql.Append("Bid,Crtby,crtdt,CheckBy,CheckDate,Flag,FAreaName,FrealAreaCode,FrealAreaName,UDate,PType,FCourseType,cnt,AuditBy,AuditFlag,AuditDate,FdbCheckFlag,FdbCheckDate,FPostBy,FPostDate,FPoestFlag,FDbCheckBY,FCourseCode,FCheckFlag,FMoneyInOutFlag,FAreaCode,FAmount,Remark,ApplyBy,Applydt");
            strSql.Append(") values (");
            strSql.Append("@Bid,@Crtby,@crtdt,@CheckBy,@CheckDate,@Flag,@FAreaName,@FrealAreaCode,@FrealAreaName,@UDate,@PType,@FCourseType,@cnt,@AuditBy,@AuditFlag,@AuditDate,@FdbCheckFlag,@FdbCheckDate,@FPostBy,@FPostDate,@FPoestFlag,@FDbCheckBY,@FCourseCode,@FCheckFlag,@FMoneyInOutFlag,@FAreaCode,@FAmount,@Remark,@ApplyBy,@Applydt");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Bid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CheckBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FrealAreaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCourseType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@cnt", SqlDbType.Int,4) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FdbCheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FdbCheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPostBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPostDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPoestFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FDbCheckBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCourseCode", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FMoneyInOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Applydt", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.Bid;
            parameters[1].Value = model.Crtby;
            parameters[2].Value = model.crtdt;
            parameters[3].Value = model.CheckBy;
            parameters[4].Value = model.CheckDate;
            parameters[5].Value = model.Flag;
            parameters[6].Value = model.FAreaName;
            parameters[7].Value = model.FrealAreaCode;
            parameters[8].Value = model.FrealAreaName;
            parameters[9].Value = model.UDate;
            parameters[10].Value = model.PType;
            parameters[11].Value = model.FCourseType;
            parameters[12].Value = model.cnt;
            parameters[13].Value = model.AuditBy;
            parameters[14].Value = model.AuditFlag;
            parameters[15].Value = model.AuditDate;
            parameters[16].Value = model.FdbCheckFlag;
            parameters[17].Value = model.FdbCheckDate;
            parameters[18].Value = model.FPostBy;
            parameters[19].Value = model.FPostDate;
            parameters[20].Value = model.FPoestFlag;
            parameters[21].Value = model.FDbCheckBY;
            parameters[22].Value = model.FCourseCode;
            parameters[23].Value = model.FCheckFlag;
            parameters[24].Value = model.FMoneyInOutFlag;
            parameters[25].Value = model.FAreaCode;
            parameters[26].Value = model.FAmount;
            parameters[27].Value = model.Remark;
            parameters[28].Value = model.ApplyBy;
            parameters[29].Value = model.Applydt;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_BatchMoneyTrade model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_BatchMoneyTrade set ");

            strSql.Append(" Bid = @Bid , ");
            strSql.Append(" Crtby = @Crtby , ");
            strSql.Append(" crtdt = @crtdt , ");
            strSql.Append(" CheckBy = @CheckBy , ");
            strSql.Append(" CheckDate = @CheckDate , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" FAreaName = @FAreaName , ");
            strSql.Append(" FrealAreaCode = @FrealAreaCode , ");
            strSql.Append(" FrealAreaName = @FrealAreaName , ");
            strSql.Append(" UDate = @UDate , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" FCourseType = @FCourseType , ");
            strSql.Append(" cnt = @cnt , ");
            strSql.Append(" AuditBy = @AuditBy , ");
            strSql.Append(" AuditFlag = @AuditFlag , ");
            strSql.Append(" AuditDate = @AuditDate , ");
            strSql.Append(" FdbCheckFlag = @FdbCheckFlag , ");
            strSql.Append(" FdbCheckDate = @FdbCheckDate , ");
            strSql.Append(" FPostBy = @FPostBy , ");
            strSql.Append(" FPostDate = @FPostDate , ");
            strSql.Append(" FPoestFlag = @FPoestFlag , ");
            strSql.Append(" FDbCheckBY = @FDbCheckBY , ");
            strSql.Append(" FCourseCode = @FCourseCode , ");
            strSql.Append(" FCheckFlag = @FCheckFlag , ");
            strSql.Append(" FMoneyInOutFlag = @FMoneyInOutFlag , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FAmount = @FAmount , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" ApplyBy = @ApplyBy , ");
            strSql.Append(" Applydt = @Applydt  ");
            strSql.Append(" where Bid=@Bid  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Bid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CheckBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FrealAreaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCourseType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@cnt", SqlDbType.Int,4) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FdbCheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FdbCheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPostBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPostDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPoestFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FDbCheckBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCourseCode", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FMoneyInOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Applydt", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.Bid;
            parameters[1].Value = model.Crtby;
            parameters[2].Value = model.crtdt;
            parameters[3].Value = model.CheckBy;
            parameters[4].Value = model.CheckDate;
            parameters[5].Value = model.Flag;
            parameters[6].Value = model.FAreaName;
            parameters[7].Value = model.FrealAreaCode;
            parameters[8].Value = model.FrealAreaName;
            parameters[9].Value = model.UDate;
            parameters[10].Value = model.PType;
            parameters[11].Value = model.FCourseType;
            parameters[12].Value = model.cnt;
            parameters[13].Value = model.AuditBy;
            parameters[14].Value = model.AuditFlag;
            parameters[15].Value = model.AuditDate;
            parameters[16].Value = model.FdbCheckFlag;
            parameters[17].Value = model.FdbCheckDate;
            parameters[18].Value = model.FPostBy;
            parameters[19].Value = model.FPostDate;
            parameters[20].Value = model.FPoestFlag;
            parameters[21].Value = model.FDbCheckBY;
            parameters[22].Value = model.FCourseCode;
            parameters[23].Value = model.FCheckFlag;
            parameters[24].Value = model.FMoneyInOutFlag;
            parameters[25].Value = model.FAreaCode;
            parameters[26].Value = model.FAmount;
            parameters[27].Value = model.Remark;
            parameters[28].Value = model.ApplyBy;
            parameters[29].Value = model.Applydt;
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
        public bool Delete(string Bid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_BatchMoneyTrade ");
            strSql.Append(" where Bid=@Bid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Bid", SqlDbType.VarChar,20)			};
            parameters[0].Value = Bid;


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
        public SelfhelpOrderMgr.Model.T_BatchMoneyTrade GetModel(string Bid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Bid, Crtby, crtdt, CheckBy, CheckDate, Flag, FAreaName, FrealAreaCode, FrealAreaName, UDate, PType, FCourseType, cnt, AuditBy, AuditFlag, AuditDate, FdbCheckFlag, FdbCheckDate, FPostBy, FPostDate, FPoestFlag, FDbCheckBY, FCourseCode, FCheckFlag, FMoneyInOutFlag, FAreaCode, FAmount, Remark, ApplyBy, Applydt  ");
            strSql.Append("  from T_BatchMoneyTrade ");
            strSql.Append(" where Bid=@Bid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Bid", SqlDbType.VarChar,20)			};
            parameters[0].Value = Bid;


            SelfhelpOrderMgr.Model.T_BatchMoneyTrade model = new SelfhelpOrderMgr.Model.T_BatchMoneyTrade();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Bid = ds.Tables[0].Rows[0]["Bid"].ToString();
                model.Crtby = ds.Tables[0].Rows[0]["Crtby"].ToString();
                if (ds.Tables[0].Rows[0]["crtdt"].ToString() != "")
                {
                    model.crtdt = DateTime.Parse(ds.Tables[0].Rows[0]["crtdt"].ToString());
                }
                model.CheckBy = ds.Tables[0].Rows[0]["CheckBy"].ToString();
                if (ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    model.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.FAreaName = ds.Tables[0].Rows[0]["FAreaName"].ToString();
                model.FrealAreaCode = ds.Tables[0].Rows[0]["FrealAreaCode"].ToString();
                model.FrealAreaName = ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
                if (ds.Tables[0].Rows[0]["UDate"].ToString() != "")
                {
                    model.UDate = DateTime.Parse(ds.Tables[0].Rows[0]["UDate"].ToString());
                }
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                model.FCourseType = ds.Tables[0].Rows[0]["FCourseType"].ToString();
                if (ds.Tables[0].Rows[0]["cnt"].ToString() != "")
                {
                    model.cnt = int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString());
                }
                model.AuditBy = ds.Tables[0].Rows[0]["AuditBy"].ToString();
                if (ds.Tables[0].Rows[0]["AuditFlag"].ToString() != "")
                {
                    model.AuditFlag = int.Parse(ds.Tables[0].Rows[0]["AuditFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuditDate"].ToString() != "")
                {
                    model.AuditDate = DateTime.Parse(ds.Tables[0].Rows[0]["AuditDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FdbCheckFlag"].ToString() != "")
                {
                    model.FdbCheckFlag = int.Parse(ds.Tables[0].Rows[0]["FdbCheckFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FdbCheckDate"].ToString() != "")
                {
                    model.FdbCheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["FdbCheckDate"].ToString());
                }
                model.FPostBy = ds.Tables[0].Rows[0]["FPostBy"].ToString();
                if (ds.Tables[0].Rows[0]["FPostDate"].ToString() != "")
                {
                    model.FPostDate = DateTime.Parse(ds.Tables[0].Rows[0]["FPostDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FPoestFlag"].ToString() != "")
                {
                    model.FPoestFlag = int.Parse(ds.Tables[0].Rows[0]["FPoestFlag"].ToString());
                }
                model.FDbCheckBY = ds.Tables[0].Rows[0]["FDbCheckBY"].ToString();
                if (ds.Tables[0].Rows[0]["FCourseCode"].ToString() != "")
                {
                    model.FCourseCode = int.Parse(ds.Tables[0].Rows[0]["FCourseCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FCheckFlag"].ToString() != "")
                {
                    model.FCheckFlag = int.Parse(ds.Tables[0].Rows[0]["FCheckFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMoneyInOutFlag"].ToString() != "")
                {
                    model.FMoneyInOutFlag = int.Parse(ds.Tables[0].Rows[0]["FMoneyInOutFlag"].ToString());
                }
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                if (ds.Tables[0].Rows[0]["FAmount"].ToString() != "")
                {
                    model.FAmount = decimal.Parse(ds.Tables[0].Rows[0]["FAmount"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.ApplyBy = ds.Tables[0].Rows[0]["ApplyBy"].ToString();
                if (ds.Tables[0].Rows[0]["Applydt"].ToString() != "")
                {
                    model.Applydt = DateTime.Parse(ds.Tables[0].Rows[0]["Applydt"].ToString());
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
            strSql.Append(" FROM T_BatchMoneyTrade ");
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
            strSql.Append(" FROM T_BatchMoneyTrade ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

