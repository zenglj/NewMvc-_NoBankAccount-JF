﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Goods
    public partial class T_GoodsDAL
    {

        public bool Exists(string GTXM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Goods");
            strSql.Append(" where ");
            strSql.Append(" GTXM = @GTXM  ");
            SqlParameter[] parameters = {
					new SqlParameter("@GTXM", SqlDbType.VarChar,50)			};
            parameters[0].Value = GTXM;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SelfhelpOrderMgr.Model.T_Goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods(");
            strSql.Append("GCODE,CrtBy,Crtdt,ModBy,Moddt,GBalance,ACTIVE,COMBFLAG,gindj,subflag,madein,GNAME,Ffreeflag,balflag,Serviceflag,gjm,src,data,Xgsl,XgMode,GTYPE,GUnit,GStandard,GDJ,GSupplyer,GTXM,SPShortCode");
            strSql.Append(") values (");
            strSql.Append("@GCODE,@CrtBy,@Crtdt,@ModBy,@Moddt,@GBalance,@ACTIVE,@COMBFLAG,@gindj,@subflag,@madein,@GNAME,@Ffreeflag,@balflag,@Serviceflag,@gjm,@src,@data,@Xgsl,@XgMode,@GTYPE,@GUnit,@GStandard,@GDJ,@GSupplyer,@GTXM,@SPShortCode");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModBy", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Moddt", SqlDbType.DateTime) ,            
                        new SqlParameter("@GBalance", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@ACTIVE", SqlDbType.Char,1) ,            
                        new SqlParameter("@COMBFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@gindj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@subflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@madein", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@GNAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Ffreeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@balflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Serviceflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@gjm", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@src", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@data", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Xgsl", SqlDbType.Int,4) ,            
                        new SqlParameter("@XgMode", SqlDbType.Int,4) ,            
                        new SqlParameter("@GTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GUnit", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GStandard", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GSupplyer", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SPShortCode", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.GCODE;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.Crtdt;
            parameters[3].Value = model.ModBy;
            parameters[4].Value = model.Moddt;
            parameters[5].Value = model.GBalance;
            parameters[6].Value = model.ACTIVE;
            parameters[7].Value = model.COMBFLAG;
            parameters[8].Value = model.gindj;
            parameters[9].Value = model.subflag;
            parameters[10].Value = model.madein;
            parameters[11].Value = model.GNAME;
            parameters[12].Value = model.Ffreeflag;
            parameters[13].Value = model.balflag;
            parameters[14].Value = model.Serviceflag;
            parameters[15].Value = model.gjm;
            parameters[16].Value = model.src;
            parameters[17].Value = model.data;
            parameters[18].Value = model.Xgsl;
            parameters[19].Value = model.XgMode;
            parameters[20].Value = model.GTYPE;
            parameters[21].Value = model.GUnit;
            parameters[22].Value = model.GStandard;
            parameters[23].Value = model.GDJ;
            parameters[24].Value = model.GSupplyer;
            parameters[25].Value = model.GTXM;
            parameters[26].Value = model.SPShortCode;
            int i=SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods set ");

            strSql.Append(" GCODE = @GCODE , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" Crtdt = @Crtdt , ");
            strSql.Append(" ModBy = @ModBy , ");
            strSql.Append(" Moddt = @Moddt , ");
            strSql.Append(" GBalance = @GBalance , ");
            strSql.Append(" ACTIVE = @ACTIVE , ");
            strSql.Append(" COMBFLAG = @COMBFLAG , ");
            strSql.Append(" gindj = @gindj , ");
            strSql.Append(" subflag = @subflag , ");
            strSql.Append(" madein = @madein , ");
            strSql.Append(" GNAME = @GNAME , ");
            strSql.Append(" Ffreeflag = @Ffreeflag , ");
            strSql.Append(" balflag = @balflag , ");
            strSql.Append(" Serviceflag = @Serviceflag , ");
            strSql.Append(" gjm = @gjm , ");
            strSql.Append(" src = @src , ");
            strSql.Append(" data = @data , ");
            strSql.Append(" Xgsl = @Xgsl , ");
            strSql.Append(" XgMode = @XgMode , ");
            strSql.Append(" GTYPE = @GTYPE , ");
            strSql.Append(" GUnit = @GUnit , ");
            strSql.Append(" GStandard = @GStandard , ");
            strSql.Append(" GDJ = @GDJ , ");
            strSql.Append(" GSupplyer = @GSupplyer , ");
            strSql.Append(" GTXM = @GTXM , ");
            strSql.Append(" SPShortCode = @SPShortCode  ");
            strSql.Append(" where GTXM=@GTXM  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModBy", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Moddt", SqlDbType.DateTime) ,            
                        new SqlParameter("@GBalance", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@ACTIVE", SqlDbType.Char,1) ,            
                        new SqlParameter("@COMBFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@gindj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@subflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@madein", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@GNAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Ffreeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@balflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Serviceflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@gjm", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@src", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@data", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Xgsl", SqlDbType.Int,4) ,            
                        new SqlParameter("@XgMode", SqlDbType.Int,4) ,            
                        new SqlParameter("@GTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GUnit", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GStandard", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GSupplyer", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SPShortCode", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.GCODE;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.Crtdt;
            parameters[3].Value = model.ModBy;
            parameters[4].Value = model.Moddt;
            parameters[5].Value = model.GBalance;
            parameters[6].Value = model.ACTIVE;
            parameters[7].Value = model.COMBFLAG;
            parameters[8].Value = model.gindj;
            parameters[9].Value = model.subflag;
            parameters[10].Value = model.madein;
            parameters[11].Value = model.GNAME;
            parameters[12].Value = model.Ffreeflag;
            parameters[13].Value = model.balflag;
            parameters[14].Value = model.Serviceflag;
            parameters[15].Value = model.gjm;
            parameters[16].Value = model.src;
            parameters[17].Value = model.data;
            parameters[18].Value = model.Xgsl;
            parameters[19].Value = model.XgMode;
            parameters[20].Value = model.GTYPE;
            parameters[21].Value = model.GUnit;
            parameters[22].Value = model.GStandard;
            parameters[23].Value = model.GDJ;
            parameters[24].Value = model.GSupplyer;
            parameters[25].Value = model.GTXM;
            parameters[26].Value = model.SPShortCode;
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
        public bool Delete(string GTXM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods ");
            strSql.Append(" where GTXM=@GTXM ");
            SqlParameter[] parameters = {
					new SqlParameter("@GTXM", SqlDbType.VarChar,50)			};
            parameters[0].Value = GTXM;


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
        public SelfhelpOrderMgr.Model.T_Goods GetModel(string GTXM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GCODE, CrtBy, Crtdt, ModBy, Moddt, GBalance, ACTIVE, COMBFLAG, gindj, subflag, madein, GNAME, Ffreeflag, balflag, Serviceflag, gjm, src, data, Xgsl, XgMode, GTYPE, GUnit, GStandard, GDJ, GSupplyer, GTXM, SPShortCode  ");
            strSql.Append("  from T_Goods ");
            strSql.Append(" where GTXM=@GTXM ");
            SqlParameter[] parameters = {
					new SqlParameter("@GTXM", SqlDbType.VarChar,50)			};
            parameters[0].Value = GTXM;


            SelfhelpOrderMgr.Model.T_Goods model = new SelfhelpOrderMgr.Model.T_Goods();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.GCODE = ds.Tables[0].Rows[0]["GCODE"].ToString();
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["Crtdt"].ToString() != "")
                {
                    model.Crtdt = ds.Tables[0].Rows[0]["Crtdt"].ToString();
                }
                model.ModBy = ds.Tables[0].Rows[0]["ModBy"].ToString();
                if (ds.Tables[0].Rows[0]["Moddt"].ToString() != "")
                {
                    model.Moddt = ds.Tables[0].Rows[0]["Moddt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GBalance"].ToString() != "")
                {
                    model.GBalance = decimal.Parse(ds.Tables[0].Rows[0]["GBalance"].ToString());
                }
                model.ACTIVE = ds.Tables[0].Rows[0]["ACTIVE"].ToString();
                if (ds.Tables[0].Rows[0]["COMBFLAG"].ToString() != "")
                {
                    model.COMBFLAG = int.Parse(ds.Tables[0].Rows[0]["COMBFLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gindj"].ToString() != "")
                {
                    model.gindj = decimal.Parse(ds.Tables[0].Rows[0]["gindj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["subflag"].ToString() != "")
                {
                    model.subflag = int.Parse(ds.Tables[0].Rows[0]["subflag"].ToString());
                }
                model.madein = ds.Tables[0].Rows[0]["madein"].ToString();
                model.GNAME = ds.Tables[0].Rows[0]["GNAME"].ToString();
                if (ds.Tables[0].Rows[0]["Ffreeflag"].ToString() != "")
                {
                    model.Ffreeflag = int.Parse(ds.Tables[0].Rows[0]["Ffreeflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["balflag"].ToString() != "")
                {
                    model.balflag = int.Parse(ds.Tables[0].Rows[0]["balflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Serviceflag"].ToString() != "")
                {
                    model.Serviceflag = int.Parse(ds.Tables[0].Rows[0]["Serviceflag"].ToString());
                }
                model.gjm = ds.Tables[0].Rows[0]["gjm"].ToString();
                model.src = ds.Tables[0].Rows[0]["src"].ToString();
                model.data = ds.Tables[0].Rows[0]["data"].ToString();
                if (ds.Tables[0].Rows[0]["Xgsl"].ToString() != "")
                {
                    model.Xgsl = int.Parse(ds.Tables[0].Rows[0]["Xgsl"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XgMode"].ToString() != "")
                {
                    model.XgMode = int.Parse(ds.Tables[0].Rows[0]["XgMode"].ToString());
                }
                model.GTYPE = ds.Tables[0].Rows[0]["GTYPE"].ToString();
                model.GUnit = ds.Tables[0].Rows[0]["GUnit"].ToString();
                model.GStandard = ds.Tables[0].Rows[0]["GStandard"].ToString();
                if (ds.Tables[0].Rows[0]["GDJ"].ToString() != "")
                {
                    model.GDJ = decimal.Parse(ds.Tables[0].Rows[0]["GDJ"].ToString());
                }
                model.GSupplyer = ds.Tables[0].Rows[0]["GSupplyer"].ToString();
                model.GTXM = ds.Tables[0].Rows[0]["GTXM"].ToString();
                model.SPShortCode = ds.Tables[0].Rows[0]["SPShortCode"].ToString();

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
            strSql.Append(" FROM T_Goods ");
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
            strSql.Append(" FROM T_Goods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

