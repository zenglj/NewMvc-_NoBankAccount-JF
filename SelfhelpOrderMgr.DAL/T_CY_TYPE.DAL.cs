using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_CY_TYPE
    public partial class T_CY_TYPEDAL
    {

        public bool Exists(string FCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_CY_TYPE");
            strSql.Append(" where ");
            strSql.Append(" FCode = @FCode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,3)			};
            parameters[0].Value = FCode;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_CY_TYPE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_CY_TYPE(");
            strSql.Append("FCode,FdayLimitflag,FdaylimitAmt,FBamtMonth,FbamtmonthFlag,FAamtmonthflag,bpct,Fbonusflag,cpct,ftotamtmonthflag,ftotamtmonth,FName,totpct,FPower,FDinnerAFlag,FDinnerBFlag,payaccount,FTZSP_Money,FTZSP_Zero_Flag,JaRi_Cy_Money,FDesc,famtmonth,FamtLimit,fcamtlimit,flag,FLimittype,pct,FTZSP_Zero_MaxMoney,JaRi_Cy_FTZSP_Money");
            strSql.Append(") values (");
            strSql.Append("@FCode,@FdayLimitflag,@FdaylimitAmt,@FBamtMonth,@FbamtmonthFlag,@FAamtmonthflag,@bpct,@Fbonusflag,@cpct,@ftotamtmonthflag,@ftotamtmonth,@FName,@totpct,@FPower,@FDinnerAFlag,@FDinnerBFlag,@payaccount,@FTZSP_Money,@FTZSP_Zero_Flag,@JaRi_Cy_Money,@FDesc,@famtmonth,@FamtLimit,@fcamtlimit,@flag,@FLimittype,@pct,@FTZSP_Zero_MaxMoney,@JaRi_Cy_FTZSP_Money");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FdayLimitflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FdaylimitAmt", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FBamtMonth", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FbamtmonthFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAamtmonthflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@bpct", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Fbonusflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@cpct", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ftotamtmonthflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ftotamtmonth", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@totpct", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FPower", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FDinnerAFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FDinnerBFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@payaccount", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FTZSP_Zero_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@JaRi_Cy_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@famtmonth", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FamtLimit", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@fcamtlimit", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FLimittype", SqlDbType.Int,4) ,            
                        new SqlParameter("@pct", SqlDbType.Decimal,9),          
                        new SqlParameter("@FTZSP_Zero_MaxMoney", SqlDbType.Decimal,9),
                        new SqlParameter("@JaRi_Cy_FTZSP_Money", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FdayLimitflag;
            parameters[2].Value = model.FdaylimitAmt;
            parameters[3].Value = model.FBamtMonth;
            parameters[4].Value = model.FbamtmonthFlag;
            parameters[5].Value = model.FAamtmonthflag;
            parameters[6].Value = model.bpct;
            parameters[7].Value = model.Fbonusflag;
            parameters[8].Value = model.cpct;
            parameters[9].Value = model.ftotamtmonthflag;
            parameters[10].Value = model.ftotamtmonth;
            parameters[11].Value = model.FName;
            parameters[12].Value = model.totpct;
            parameters[13].Value = model.FPower;
            parameters[14].Value = model.FDinnerAFlag;
            parameters[15].Value = model.FDinnerBFlag;
            parameters[16].Value = model.payaccount;
            parameters[17].Value = model.FTZSP_Money;
            parameters[18].Value = model.FTZSP_Zero_Flag;
            parameters[19].Value = model.JaRi_Cy_Money;
            parameters[20].Value = model.FDesc;
            parameters[21].Value = model.famtmonth;
            parameters[22].Value = model.FamtLimit;
            parameters[23].Value = model.fcamtlimit;
            parameters[24].Value = model.flag;
            parameters[25].Value = model.FLimittype;
            parameters[26].Value = model.pct; 
            parameters[27].Value = model.FTZSP_Zero_MaxMoney;
            parameters[28].Value = model.JaRi_Cy_FTZSP_Money;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_CY_TYPE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CY_TYPE set ");

            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FdayLimitflag = @FdayLimitflag , ");
            strSql.Append(" FdaylimitAmt = @FdaylimitAmt , ");
            strSql.Append(" FBamtMonth = @FBamtMonth , ");
            strSql.Append(" FbamtmonthFlag = @FbamtmonthFlag , ");
            strSql.Append(" FAamtmonthflag = @FAamtmonthflag , ");
            strSql.Append(" bpct = @bpct , ");
            strSql.Append(" Fbonusflag = @Fbonusflag , ");
            strSql.Append(" cpct = @cpct , ");
            strSql.Append(" ftotamtmonthflag = @ftotamtmonthflag , ");
            strSql.Append(" ftotamtmonth = @ftotamtmonth , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" totpct = @totpct , ");
            strSql.Append(" FPower = @FPower , ");
            strSql.Append(" FDinnerAFlag = @FDinnerAFlag , ");
            strSql.Append(" FDinnerBFlag = @FDinnerBFlag , ");
            strSql.Append(" payaccount = @payaccount , ");
            strSql.Append(" FTZSP_Money = @FTZSP_Money , ");
            strSql.Append(" FTZSP_Zero_Flag = @FTZSP_Zero_Flag , ");
            strSql.Append(" JaRi_Cy_Money = @JaRi_Cy_Money , ");
            strSql.Append(" FDesc = @FDesc , ");
            strSql.Append(" famtmonth = @famtmonth , ");
            strSql.Append(" FamtLimit = @FamtLimit , ");
            strSql.Append(" fcamtlimit = @fcamtlimit , ");
            strSql.Append(" flag = @flag , ");
            strSql.Append(" FLimittype = @FLimittype , ");
            strSql.Append(" pct = @pct , ");
            strSql.Append(" FTZSP_Zero_MaxMoney = @FTZSP_Zero_MaxMoney , ");
            strSql.Append(" JaRi_Cy_FTZSP_Money = @JaRi_Cy_FTZSP_Money ");
            
            strSql.Append(" where FCode=@FCode  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FdayLimitflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FdaylimitAmt", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FBamtMonth", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FbamtmonthFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAamtmonthflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@bpct", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Fbonusflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@cpct", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ftotamtmonthflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ftotamtmonth", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@totpct", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FPower", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FDinnerAFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FDinnerBFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@payaccount", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FTZSP_Zero_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@JaRi_Cy_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@famtmonth", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FamtLimit", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@fcamtlimit", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FLimittype", SqlDbType.Int,4) ,            
                        new SqlParameter("@pct", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FTZSP_Zero_MaxMoney", SqlDbType.Decimal,9),
                        new SqlParameter("@JaRi_Cy_FTZSP_Money", SqlDbType.Decimal,9)
                        

            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FdayLimitflag;
            parameters[2].Value = model.FdaylimitAmt;
            parameters[3].Value = model.FBamtMonth;
            parameters[4].Value = model.FbamtmonthFlag;
            parameters[5].Value = model.FAamtmonthflag;
            parameters[6].Value = model.bpct;
            parameters[7].Value = model.Fbonusflag;
            parameters[8].Value = model.cpct;
            parameters[9].Value = model.ftotamtmonthflag;
            parameters[10].Value = model.ftotamtmonth;
            parameters[11].Value = model.FName;
            parameters[12].Value = model.totpct;
            parameters[13].Value = model.FPower;
            parameters[14].Value = model.FDinnerAFlag;
            parameters[15].Value = model.FDinnerBFlag;
            parameters[16].Value = model.payaccount;
            parameters[17].Value = model.FTZSP_Money;
            parameters[18].Value = model.FTZSP_Zero_Flag;
            parameters[19].Value = model.JaRi_Cy_Money;
            parameters[20].Value = model.FDesc;
            parameters[21].Value = model.famtmonth;
            parameters[22].Value = model.FamtLimit;
            parameters[23].Value = model.fcamtlimit;
            parameters[24].Value = model.flag;
            parameters[25].Value = model.FLimittype;
            parameters[26].Value = model.pct; 
            parameters[27].Value = model.FTZSP_Zero_MaxMoney;
            parameters[28].Value = model.JaRi_Cy_FTZSP_Money;
            
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
            strSql.Append("delete from T_CY_TYPE ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,3)			};
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
        public SelfhelpOrderMgr.Model.T_CY_TYPE GetModel(string FCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FCode, FdayLimitflag, FdaylimitAmt, FBamtMonth, FbamtmonthFlag, FAamtmonthflag, bpct, Fbonusflag, cpct, ftotamtmonthflag, ftotamtmonth, FName, totpct, FPower, FDinnerAFlag, FDinnerBFlag, payaccount, FTZSP_Money, FTZSP_Zero_Flag, JaRi_Cy_Money, FDesc, famtmonth, FamtLimit, fcamtlimit, flag, FLimittype, pct,FTZSP_Zero_MaxMoney ,JaRi_Cy_FTZSP_Money ");
            strSql.Append("  from T_CY_TYPE ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,3)			};
            parameters[0].Value = FCode;


            SelfhelpOrderMgr.Model.T_CY_TYPE model = new SelfhelpOrderMgr.Model.T_CY_TYPE();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.FCode = ds.Tables[0].Rows[0]["FCode"].ToString();
                if (ds.Tables[0].Rows[0]["FdayLimitflag"].ToString() != "")
                {
                    model.FdayLimitflag = int.Parse(ds.Tables[0].Rows[0]["FdayLimitflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FdaylimitAmt"].ToString() != "")
                {
                    model.FdaylimitAmt = decimal.Parse(ds.Tables[0].Rows[0]["FdaylimitAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FBamtMonth"].ToString() != "")
                {
                    model.FBamtMonth = decimal.Parse(ds.Tables[0].Rows[0]["FBamtMonth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FbamtmonthFlag"].ToString() != "")
                {
                    model.FbamtmonthFlag = int.Parse(ds.Tables[0].Rows[0]["FbamtmonthFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FAamtmonthflag"].ToString() != "")
                {
                    model.FAamtmonthflag = int.Parse(ds.Tables[0].Rows[0]["FAamtmonthflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bpct"].ToString() != "")
                {
                    model.bpct = decimal.Parse(ds.Tables[0].Rows[0]["bpct"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fbonusflag"].ToString() != "")
                {
                    model.Fbonusflag = int.Parse(ds.Tables[0].Rows[0]["Fbonusflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cpct"].ToString() != "")
                {
                    model.cpct = decimal.Parse(ds.Tables[0].Rows[0]["cpct"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ftotamtmonthflag"].ToString() != "")
                {
                    model.ftotamtmonthflag = int.Parse(ds.Tables[0].Rows[0]["ftotamtmonthflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ftotamtmonth"].ToString() != "")
                {
                    model.ftotamtmonth = decimal.Parse(ds.Tables[0].Rows[0]["ftotamtmonth"].ToString());
                }
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                if (ds.Tables[0].Rows[0]["totpct"].ToString() != "")
                {
                    model.totpct = decimal.Parse(ds.Tables[0].Rows[0]["totpct"].ToString());
                }
                model.FPower = ds.Tables[0].Rows[0]["FPower"].ToString();
                if (ds.Tables[0].Rows[0]["FDinnerAFlag"].ToString() != "")
                {
                    model.FDinnerAFlag = int.Parse(ds.Tables[0].Rows[0]["FDinnerAFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FDinnerBFlag"].ToString() != "")
                {
                    model.FDinnerBFlag = int.Parse(ds.Tables[0].Rows[0]["FDinnerBFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payaccount"].ToString() != "")
                {
                    model.payaccount = int.Parse(ds.Tables[0].Rows[0]["payaccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FTZSP_Money"].ToString() != "")
                {
                    model.FTZSP_Money = decimal.Parse(ds.Tables[0].Rows[0]["FTZSP_Money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FTZSP_Zero_Flag"].ToString() != "")
                {
                    model.FTZSP_Zero_Flag = int.Parse(ds.Tables[0].Rows[0]["FTZSP_Zero_Flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JaRi_Cy_Money"].ToString() != "")
                {
                    model.JaRi_Cy_Money = decimal.Parse(ds.Tables[0].Rows[0]["JaRi_Cy_Money"].ToString());
                }
                model.FDesc = ds.Tables[0].Rows[0]["FDesc"].ToString();
                if (ds.Tables[0].Rows[0]["famtmonth"].ToString() != "")
                {
                    model.famtmonth = decimal.Parse(ds.Tables[0].Rows[0]["famtmonth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FamtLimit"].ToString() != "")
                {
                    model.FamtLimit = decimal.Parse(ds.Tables[0].Rows[0]["FamtLimit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fcamtlimit"].ToString() != "")
                {
                    model.fcamtlimit = decimal.Parse(ds.Tables[0].Rows[0]["fcamtlimit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["flag"].ToString() != "")
                {
                    model.flag = int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FLimittype"].ToString() != "")
                {
                    model.FLimittype = int.Parse(ds.Tables[0].Rows[0]["FLimittype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pct"].ToString() != "")
                {
                    model.pct = decimal.Parse(ds.Tables[0].Rows[0]["pct"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FTZSP_Zero_MaxMoney"].ToString() != "")
                {
                    model.FTZSP_Zero_MaxMoney = decimal.Parse(ds.Tables[0].Rows[0]["FTZSP_Zero_MaxMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JaRi_Cy_FTZSP_Money"].ToString() != "")
                {
                    model.JaRi_Cy_FTZSP_Money = decimal.Parse(ds.Tables[0].Rows[0]["JaRi_Cy_FTZSP_Money"].ToString());
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
            strSql.Append(" FROM T_CY_TYPE ");
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
            strSql.Append(" FROM T_CY_TYPE ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

