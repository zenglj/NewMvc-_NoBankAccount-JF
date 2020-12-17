using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Criminal
    public partial class T_CriminalDAL
    {

        public bool Exists(string FCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Criminal");
            strSql.Append(" where ");
            strSql.Append(" FCode = @FCode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,20)			};
            parameters[0].Value = FCode;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Criminal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Criminal(");
            strSql.Append("FCode,FInDate,FOuDate,FAreaCode,FSubArea,FDesc,FStatus,FStatus2,FAddr_tmp,FCZY,fflag,FName,flimitflag,flimitamt,Frealareacode,amount,TP_YingYangCan_Money,RSB_Flag,FIdenNo,FAge,FSex,FAddr,FCrimeCode,FCYCode,FTerm");
            strSql.Append(") values (");
            strSql.Append("@FCode,@FInDate,@FOuDate,@FAreaCode,@FSubArea,@FDesc,@FStatus,@FStatus2,@FAddr_tmp,@FCZY,@fflag,@FName,@flimitflag,@flimitamt,@Frealareacode,@amount,@TP_YingYangCan_Money,@RSB_Flag,@FIdenNo,@FAge,@FSex,@FAddr,@FCrimeCode,@FCYCode,@FTerm");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FInDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FOuDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@FSubArea", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@FStatus2", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@FAddr_tmp", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FCZY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@flimitflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@flimitamt", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@TP_YingYangCan_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RSB_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAge", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FCYCode", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@FTerm", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FInDate;
            parameters[2].Value = model.FOuDate;
            parameters[3].Value = model.FAreaCode;
            parameters[4].Value = model.FSubArea;
            parameters[5].Value = model.FDesc;
            parameters[6].Value = model.FStatus;
            parameters[7].Value = model.FStatus2;
            parameters[8].Value = model.FAddr_tmp;
            parameters[9].Value = model.FCZY;
            parameters[10].Value = model.fflag;
            parameters[11].Value = model.FName;
            parameters[12].Value = model.flimitflag;
            parameters[13].Value = model.flimitamt;
            parameters[14].Value = model.Frealareacode;
            parameters[15].Value = model.amount;
            parameters[16].Value = model.TP_YingYangCan_Money;
            parameters[17].Value = model.RSB_Flag;
            parameters[18].Value = model.FIdenNo;
            parameters[19].Value = model.FAge;
            parameters[20].Value = model.FSex;
            parameters[21].Value = model.FAddr;
            parameters[22].Value = model.FCrimeCode;
            parameters[23].Value = model.FCYCode;
            parameters[24].Value = model.FTerm;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Criminal set ");

            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FInDate = @FInDate , ");
            strSql.Append(" FOuDate = @FOuDate , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FSubArea = @FSubArea , ");
            strSql.Append(" FDesc = @FDesc , ");
            strSql.Append(" FStatus = @FStatus , ");
            strSql.Append(" FStatus2 = @FStatus2 , ");
            strSql.Append(" FAddr_tmp = @FAddr_tmp , ");
            strSql.Append(" FCZY = @FCZY , ");
            strSql.Append(" fflag = @fflag , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" flimitflag = @flimitflag , ");
            strSql.Append(" flimitamt = @flimitamt , ");
            strSql.Append(" Frealareacode = @Frealareacode , ");
            strSql.Append(" amount = @amount , ");
            strSql.Append(" TP_YingYangCan_Money = @TP_YingYangCan_Money , ");
            strSql.Append(" RSB_Flag = @RSB_Flag , ");
            strSql.Append(" FIdenNo = @FIdenNo , ");
            strSql.Append(" FAge = @FAge , ");
            strSql.Append(" FSex = @FSex , ");
            strSql.Append(" FAddr = @FAddr , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" FCYCode = @FCYCode , ");
            strSql.Append(" FTerm = @FTerm  ");
            strSql.Append(" where FCode=@FCode  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FInDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FOuDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@FSubArea", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@FStatus2", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@FAddr_tmp", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FCZY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@flimitflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@flimitamt", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@TP_YingYangCan_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RSB_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAge", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FCYCode", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@FTerm", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FInDate;
            parameters[2].Value = model.FOuDate;
            parameters[3].Value = model.FAreaCode;
            parameters[4].Value = model.FSubArea;
            parameters[5].Value = model.FDesc;
            parameters[6].Value = model.FStatus;
            parameters[7].Value = model.FStatus2;
            parameters[8].Value = model.FAddr_tmp;
            parameters[9].Value = model.FCZY;
            parameters[10].Value = model.fflag;
            parameters[11].Value = model.FName;
            parameters[12].Value = model.flimitflag;
            parameters[13].Value = model.flimitamt;
            parameters[14].Value = model.Frealareacode;
            parameters[15].Value = model.amount;
            parameters[16].Value = model.TP_YingYangCan_Money;
            parameters[17].Value = model.RSB_Flag;
            parameters[18].Value = model.FIdenNo;
            parameters[19].Value = model.FAge;
            parameters[20].Value = model.FSex;
            parameters[21].Value = model.FAddr;
            parameters[22].Value = model.FCrimeCode;
            parameters[23].Value = model.FCYCode;
            parameters[24].Value = model.FTerm;
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
            strSql.Append("delete from T_Criminal ");
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
        public SelfhelpOrderMgr.Model.T_Criminal GetModel(string FCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FCode, FInDate, FOuDate, FAreaCode, FSubArea, FDesc, FStatus, FStatus2, FAddr_tmp, FCZY, fflag, FName, flimitflag, flimitamt, Frealareacode, amount, TP_YingYangCan_Money, RSB_Flag, FIdenNo, FAge, FSex, FAddr, FCrimeCode, FCYCode, FTerm  ");
            strSql.Append("  from T_Criminal ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,20)			};
            parameters[0].Value = FCode;


            SelfhelpOrderMgr.Model.T_Criminal model = new SelfhelpOrderMgr.Model.T_Criminal();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.FCode = ds.Tables[0].Rows[0]["FCode"].ToString();
                if (ds.Tables[0].Rows[0]["FInDate"].ToString() != "")
                {
                    model.FInDate = DateTime.Parse(ds.Tables[0].Rows[0]["FInDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FOuDate"].ToString() != "")
                {
                    model.FOuDate = DateTime.Parse(ds.Tables[0].Rows[0]["FOuDate"].ToString());
                }
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                model.FSubArea = ds.Tables[0].Rows[0]["FSubArea"].ToString();
                model.FDesc = ds.Tables[0].Rows[0]["FDesc"].ToString();
                if (ds.Tables[0].Rows[0]["FStatus"].ToString() != "")
                {
                    model.FStatus = int.Parse(ds.Tables[0].Rows[0]["FStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FStatus2"].ToString() != "")
                {
                    model.FStatus2 = int.Parse(ds.Tables[0].Rows[0]["FStatus2"].ToString());
                }
                model.FAddr_tmp = ds.Tables[0].Rows[0]["FAddr_tmp"].ToString();
                model.FCZY = ds.Tables[0].Rows[0]["FCZY"].ToString();
                if (ds.Tables[0].Rows[0]["fflag"].ToString() != "")
                {
                    model.fflag = int.Parse(ds.Tables[0].Rows[0]["fflag"].ToString());
                }
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                if (ds.Tables[0].Rows[0]["flimitflag"].ToString() != "")
                {
                    model.flimitflag = int.Parse(ds.Tables[0].Rows[0]["flimitflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["flimitamt"].ToString() != "")
                {
                    model.flimitamt = decimal.Parse(ds.Tables[0].Rows[0]["flimitamt"].ToString());
                }
                model.Frealareacode = ds.Tables[0].Rows[0]["Frealareacode"].ToString();
                if (ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TP_YingYangCan_Money"].ToString() != "")
                {
                    model.TP_YingYangCan_Money = decimal.Parse(ds.Tables[0].Rows[0]["TP_YingYangCan_Money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RSB_Flag"].ToString() != "")
                {
                    model.RSB_Flag = int.Parse(ds.Tables[0].Rows[0]["RSB_Flag"].ToString());
                }
                model.FIdenNo = ds.Tables[0].Rows[0]["FIdenNo"].ToString();
                if (ds.Tables[0].Rows[0]["FAge"].ToString() != "")
                {
                    model.FAge = int.Parse(ds.Tables[0].Rows[0]["FAge"].ToString());
                }
                model.FSex = ds.Tables[0].Rows[0]["FSex"].ToString();
                model.FAddr = ds.Tables[0].Rows[0]["FAddr"].ToString();
                model.FCrimeCode = ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
                model.FCYCode = ds.Tables[0].Rows[0]["FCYCode"].ToString();
                model.FTerm = ds.Tables[0].Rows[0]["FTerm"].ToString();

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
            strSql.Append(" FROM T_Criminal ");
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
            strSql.Append(" FROM T_Criminal ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

