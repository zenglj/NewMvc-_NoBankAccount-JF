using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_BONUS
    public partial class T_BONUSDAL
    {

        public bool Exists(string BID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_BONUS");
            strSql.Append(" where ");
            strSql.Append(" BID = @BID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20)			};
            parameters[0].Value = BID;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_BONUS model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_BONUS(");
            strSql.Append("BID,CheckDate,FLAG,fareaName,Frealareacode,FrealAreaName,udate,ptype,cnt,auditby,auditflag,FAREACODE,auditdate,Fdbcheckflag,Fdbcheckdate,FPostBy,FPostDate,FPostFlag,FDbCheckBY,FCheckFlag,fAMOUNT,Remark,ApplyBy,Applydt,Crtby,crtdt,CHECKBY");
            strSql.Append(") values (");
            strSql.Append("@BID,@CheckDate,@FLAG,@fareaName,@Frealareacode,@FrealAreaName,@udate,@ptype,@cnt,@auditby,@auditflag,@FAREACODE,@auditdate,@Fdbcheckflag,@Fdbcheckdate,@FPostBy,@FPostDate,@FPostFlag,@FDbCheckBY,@FCheckFlag,@fAMOUNT,@Remark,@ApplyBy,@Applydt,@Crtby,@crtdt,@CHECKBY");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BID", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@udate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ptype", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@cnt", SqlDbType.Int,4) ,            
                        new SqlParameter("@auditby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@auditflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAREACODE", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@auditdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Fdbcheckflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Fdbcheckdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPostBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPostDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPostFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FDbCheckBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@fAMOUNT", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Applydt", SqlDbType.DateTime) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CHECKBY", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.BID;
            parameters[1].Value = model.CheckDate;
            parameters[2].Value = model.FLAG;
            parameters[3].Value = model.fareaName;
            parameters[4].Value = model.Frealareacode;
            parameters[5].Value = model.FrealAreaName;
            parameters[6].Value = model.udate;
            parameters[7].Value = model.ptype;
            parameters[8].Value = model.cnt;
            parameters[9].Value = model.auditby;
            parameters[10].Value = model.auditflag;
            parameters[11].Value = model.FAREACODE;
            parameters[12].Value = model.auditdate;
            parameters[13].Value = model.Fdbcheckflag;
            parameters[14].Value = model.Fdbcheckdate;
            parameters[15].Value = model.FPostBy;
            parameters[16].Value = model.FPostDate;
            parameters[17].Value = model.FPostFlag;
            parameters[18].Value = model.FDbCheckBY;
            parameters[19].Value = model.FCheckFlag;
            parameters[20].Value = model.fAMOUNT;
            parameters[21].Value = model.Remark;
            parameters[22].Value = model.ApplyBy;
            parameters[23].Value = model.Applydt;
            parameters[24].Value = model.Crtby;
            parameters[25].Value = model.crtdt;
            parameters[26].Value = model.CHECKBY;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_BONUS model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_BONUS set ");

            strSql.Append(" BID = @BID , ");
            strSql.Append(" CheckDate = @CheckDate , ");
            strSql.Append(" FLAG = @FLAG , ");
            strSql.Append(" fareaName = @fareaName , ");
            strSql.Append(" Frealareacode = @Frealareacode , ");
            strSql.Append(" FrealAreaName = @FrealAreaName , ");
            strSql.Append(" udate = @udate , ");
            strSql.Append(" ptype = @ptype , ");
            strSql.Append(" cnt = @cnt , ");
            strSql.Append(" auditby = @auditby , ");
            strSql.Append(" auditflag = @auditflag , ");
            strSql.Append(" FAREACODE = @FAREACODE , ");
            strSql.Append(" auditdate = @auditdate , ");
            strSql.Append(" Fdbcheckflag = @Fdbcheckflag , ");
            strSql.Append(" Fdbcheckdate = @Fdbcheckdate , ");
            strSql.Append(" FPostBy = @FPostBy , ");
            strSql.Append(" FPostDate = @FPostDate , ");
            strSql.Append(" FPostFlag = @FPostFlag , ");
            strSql.Append(" FDbCheckBY = @FDbCheckBY , ");
            strSql.Append(" FCheckFlag = @FCheckFlag , ");
            strSql.Append(" fAMOUNT = @fAMOUNT , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" ApplyBy = @ApplyBy , ");
            strSql.Append(" Applydt = @Applydt , ");
            strSql.Append(" Crtby = @Crtby , ");
            strSql.Append(" crtdt = @crtdt , ");
            strSql.Append(" CHECKBY = @CHECKBY  ");
            strSql.Append(" where BID=@BID  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BID", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@udate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ptype", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@cnt", SqlDbType.Int,4) ,            
                        new SqlParameter("@auditby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@auditflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAREACODE", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@auditdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Fdbcheckflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Fdbcheckdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPostBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPostDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FPostFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FDbCheckBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@fAMOUNT", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Applydt", SqlDbType.DateTime) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CHECKBY", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.BID;
            parameters[1].Value = model.CheckDate;
            parameters[2].Value = model.FLAG;
            parameters[3].Value = model.fareaName;
            parameters[4].Value = model.Frealareacode;
            parameters[5].Value = model.FrealAreaName;
            parameters[6].Value = model.udate;
            parameters[7].Value = model.ptype;
            parameters[8].Value = model.cnt;
            parameters[9].Value = model.auditby;
            parameters[10].Value = model.auditflag;
            parameters[11].Value = model.FAREACODE;
            parameters[12].Value = model.auditdate;
            parameters[13].Value = model.Fdbcheckflag;
            parameters[14].Value = model.Fdbcheckdate;
            parameters[15].Value = model.FPostBy;
            parameters[16].Value = model.FPostDate;
            parameters[17].Value = model.FPostFlag;
            parameters[18].Value = model.FDbCheckBY;
            parameters[19].Value = model.FCheckFlag;
            parameters[20].Value = model.fAMOUNT;
            parameters[21].Value = model.Remark;
            parameters[22].Value = model.ApplyBy;
            parameters[23].Value = model.Applydt;
            parameters[24].Value = model.Crtby;
            parameters[25].Value = model.crtdt;
            parameters[26].Value = model.CHECKBY;
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
        public bool Delete(string BID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_BONUS ");
            strSql.Append(" where BID=@BID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20)			};
            parameters[0].Value = BID;


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
        public SelfhelpOrderMgr.Model.T_BONUS GetModel(string BID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BID, CheckDate, FLAG, fareaName, Frealareacode, FrealAreaName, udate, ptype, cnt, auditby, auditflag, FAREACODE, auditdate, Fdbcheckflag, Fdbcheckdate, FPostBy, FPostDate, FPostFlag, FDbCheckBY, FCheckFlag, fAMOUNT, Remark, ApplyBy, Applydt, Crtby, crtdt, CHECKBY  ");
            strSql.Append("  from T_BONUS ");
            strSql.Append(" where BID=@BID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20)			};
            parameters[0].Value = BID;


            SelfhelpOrderMgr.Model.T_BONUS model = new SelfhelpOrderMgr.Model.T_BONUS();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.BID = ds.Tables[0].Rows[0]["BID"].ToString();
                if (ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    model.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FLAG"].ToString() != "")
                {
                    model.FLAG = int.Parse(ds.Tables[0].Rows[0]["FLAG"].ToString());
                }
                model.fareaName = ds.Tables[0].Rows[0]["fareaName"].ToString();
                model.Frealareacode = ds.Tables[0].Rows[0]["Frealareacode"].ToString();
                model.FrealAreaName = ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
                if (ds.Tables[0].Rows[0]["udate"].ToString() != "")
                {
                    model.udate = DateTime.Parse(ds.Tables[0].Rows[0]["udate"].ToString());
                }
                model.ptype = ds.Tables[0].Rows[0]["ptype"].ToString();
                if (ds.Tables[0].Rows[0]["cnt"].ToString() != "")
                {
                    model.cnt = int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString());
                }
                model.auditby = ds.Tables[0].Rows[0]["auditby"].ToString();
                if (ds.Tables[0].Rows[0]["auditflag"].ToString() != "")
                {
                    model.auditflag = int.Parse(ds.Tables[0].Rows[0]["auditflag"].ToString());
                }
                model.FAREACODE = ds.Tables[0].Rows[0]["FAREACODE"].ToString();
                if (ds.Tables[0].Rows[0]["auditdate"].ToString() != "")
                {
                    model.auditdate = DateTime.Parse(ds.Tables[0].Rows[0]["auditdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fdbcheckflag"].ToString() != "")
                {
                    model.Fdbcheckflag = int.Parse(ds.Tables[0].Rows[0]["Fdbcheckflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fdbcheckdate"].ToString() != "")
                {
                    model.Fdbcheckdate = DateTime.Parse(ds.Tables[0].Rows[0]["Fdbcheckdate"].ToString());
                }
                model.FPostBy = ds.Tables[0].Rows[0]["FPostBy"].ToString();
                if (ds.Tables[0].Rows[0]["FPostDate"].ToString() != "")
                {
                    model.FPostDate = DateTime.Parse(ds.Tables[0].Rows[0]["FPostDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FPostFlag"].ToString() != "")
                {
                    model.FPostFlag = int.Parse(ds.Tables[0].Rows[0]["FPostFlag"].ToString());
                }
                model.FDbCheckBY = ds.Tables[0].Rows[0]["FDbCheckBY"].ToString();
                if (ds.Tables[0].Rows[0]["FCheckFlag"].ToString() != "")
                {
                    model.FCheckFlag = int.Parse(ds.Tables[0].Rows[0]["FCheckFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fAMOUNT"].ToString() != "")
                {
                    model.fAMOUNT = decimal.Parse(ds.Tables[0].Rows[0]["fAMOUNT"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.ApplyBy = ds.Tables[0].Rows[0]["ApplyBy"].ToString();
                if (ds.Tables[0].Rows[0]["Applydt"].ToString() != "")
                {
                    model.Applydt = DateTime.Parse(ds.Tables[0].Rows[0]["Applydt"].ToString());
                }
                model.Crtby = ds.Tables[0].Rows[0]["Crtby"].ToString();
                if (ds.Tables[0].Rows[0]["crtdt"].ToString() != "")
                {
                    model.crtdt = DateTime.Parse(ds.Tables[0].Rows[0]["crtdt"].ToString());
                }
                model.CHECKBY = ds.Tables[0].Rows[0]["CHECKBY"].ToString();

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
            strSql.Append(" FROM T_BONUS ");
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
            strSql.Append(" FROM T_BONUS ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

