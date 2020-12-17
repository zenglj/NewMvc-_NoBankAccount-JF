using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_CZY
    public partial class T_CZYDAL
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_CZY model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_CZY(");
            strSql.Append("FCode,FRole,fauditflag,fBonusPost,ver,FUserChinaName,FManagerCard,FName,FPwd,FFlag,FPRIVATE,FSTOCKCHK,FINVCHK,rolecode,FUserArea");
            strSql.Append(") values (");
            strSql.Append("@FCode,@FRole,@fauditflag,@fBonusPost,@ver,@FUserChinaName,@FManagerCard,@FName,@FPwd,@FFlag,@FPRIVATE,@FSTOCKCHK,@FINVCHK,@rolecode,@FUserArea");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FRole", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fauditflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@fBonusPost", SqlDbType.Int,4) ,            
                        new SqlParameter("@ver", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FUserChinaName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FManagerCard", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPwd", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FPRIVATE", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSTOCKCHK", SqlDbType.Int,4) ,            
                        new SqlParameter("@FINVCHK", SqlDbType.Int,4) ,            
                        new SqlParameter("@rolecode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FUserArea", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FRole;
            parameters[2].Value = model.fauditflag;
            parameters[3].Value = model.fBonusPost;
            parameters[4].Value = model.ver;
            parameters[5].Value = model.FUserChinaName;
            parameters[6].Value = model.FManagerCard;
            parameters[7].Value = model.FName;
            parameters[8].Value = model.FPwd;
            parameters[9].Value = model.FFlag;
            parameters[10].Value = model.FPRIVATE;
            parameters[11].Value = model.FSTOCKCHK;
            parameters[12].Value = model.FINVCHK;
            parameters[13].Value = model.rolecode;
            parameters[14].Value = model.FUserArea;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_CZY model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CZY set ");

            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FRole = @FRole , ");
            strSql.Append(" fauditflag = @fauditflag , ");
            strSql.Append(" fBonusPost = @fBonusPost , ");
            strSql.Append(" ver = @ver , ");
            strSql.Append(" FUserChinaName = @FUserChinaName , ");
            strSql.Append(" FManagerCard = @FManagerCard , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" FPwd = @FPwd , ");
            strSql.Append(" FFlag = @FFlag , ");
            strSql.Append(" FPRIVATE = @FPRIVATE , ");
            strSql.Append(" FSTOCKCHK = @FSTOCKCHK , ");
            strSql.Append(" FINVCHK = @FINVCHK , ");
            strSql.Append(" rolecode = @rolecode , ");
            strSql.Append(" FUserArea = @FUserArea  ");
            strSql.Append(" where FCode=@FCode  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FRole", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fauditflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@fBonusPost", SqlDbType.Int,4) ,            
                        new SqlParameter("@ver", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FUserChinaName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FManagerCard", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPwd", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FPRIVATE", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSTOCKCHK", SqlDbType.Int,4) ,            
                        new SqlParameter("@FINVCHK", SqlDbType.Int,4) ,            
                        new SqlParameter("@rolecode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FUserArea", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FRole;
            parameters[2].Value = model.fauditflag;
            parameters[3].Value = model.fBonusPost;
            parameters[4].Value = model.ver;
            parameters[5].Value = model.FUserChinaName;
            parameters[6].Value = model.FManagerCard;
            parameters[7].Value = model.FName;
            parameters[8].Value = model.FPwd;
            parameters[9].Value = model.FFlag;
            parameters[10].Value = model.FPRIVATE;
            parameters[11].Value = model.FSTOCKCHK;
            parameters[12].Value = model.FINVCHK;
            parameters[13].Value = model.rolecode;
            parameters[14].Value = model.FUserArea;
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
        public bool Delete(string FCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_CZY ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,20)			};
            parameters[0].Value = FCode;


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
        public SelfhelpOrderMgr.Model.T_CZY GetModel(string FCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FCode, FRole, fauditflag, fBonusPost, ver, FUserChinaName, FManagerCard, FName, FPwd, FFlag, FPRIVATE, FSTOCKCHK, FINVCHK, rolecode, FUserArea  ");
            strSql.Append("  from T_CZY ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,20)			};
            parameters[0].Value = FCode;


            SelfhelpOrderMgr.Model.T_CZY model = new SelfhelpOrderMgr.Model.T_CZY();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.FCode = ds.Tables[0].Rows[0]["FCode"].ToString();
                model.FRole = ds.Tables[0].Rows[0]["FRole"].ToString();
                if (ds.Tables[0].Rows[0]["fauditflag"].ToString() != "")
                {
                    model.fauditflag = int.Parse(ds.Tables[0].Rows[0]["fauditflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fBonusPost"].ToString() != "")
                {
                    model.fBonusPost = int.Parse(ds.Tables[0].Rows[0]["fBonusPost"].ToString());
                }
                model.ver = ds.Tables[0].Rows[0]["ver"].ToString();
                model.FUserChinaName = ds.Tables[0].Rows[0]["FUserChinaName"].ToString();
                model.FManagerCard = ds.Tables[0].Rows[0]["FManagerCard"].ToString();
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                model.FPwd = ds.Tables[0].Rows[0]["FPwd"].ToString();
                if (ds.Tables[0].Rows[0]["FFlag"].ToString() != "")
                {
                    model.FFlag = int.Parse(ds.Tables[0].Rows[0]["FFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FPRIVATE"].ToString() != "")
                {
                    model.FPRIVATE = int.Parse(ds.Tables[0].Rows[0]["FPRIVATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FSTOCKCHK"].ToString() != "")
                {
                    model.FSTOCKCHK = int.Parse(ds.Tables[0].Rows[0]["FSTOCKCHK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FINVCHK"].ToString() != "")
                {
                    model.FINVCHK = int.Parse(ds.Tables[0].Rows[0]["FINVCHK"].ToString());
                }
                model.rolecode = ds.Tables[0].Rows[0]["rolecode"].ToString();
                model.FUserArea = ds.Tables[0].Rows[0]["FUserArea"].ToString();

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
            strSql.Append("select FCode, FRole, fauditflag, fBonusPost, ver, FUserChinaName, FManagerCard, FName, FPwd, FFlag, FPRIVATE, FSTOCKCHK, FINVCHK, rolecode, FUserArea  ");
            strSql.Append(" FROM T_CZY ");
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
            strSql.Append(" FCode, FRole, fauditflag, fBonusPost, ver, FUserChinaName, FManagerCard, FName, FPwd, FFlag, FPRIVATE, FSTOCKCHK, FINVCHK, rolecode, FUserArea  ");
            strSql.Append(" FROM T_CZY ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

