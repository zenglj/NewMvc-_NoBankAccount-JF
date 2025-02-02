﻿using SelfhelpOrderMgr.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_GoodsDAL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IEnumerable<T_Goods> GetListOfIEnumerable(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.*,isnull(b.balance,0) Balance from t_goods a left outer join T_GOODSSTOCKMAIN b
                            on a.gcode=b.gcode
                            ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by SPShortCode ");
            using(SqlConnection conn=new SqlConnection(SqlHelper.getConnstr())){
                return conn.Query<T_Goods>(strSql.ToString());
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public IEnumerable<T_Goods> GetListOfIEnumerable(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(@" a.*,isnull(b.balance,0) Balance from t_goods a left outer join T_GOODSSTOCKMAIN b
                            on a.gcode=b.gcode");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                return conn.Query<T_Goods>(strSql.ToString());
            }
        }

        public IEnumerable<T_Goods> GetPageListOfIEnumerable(int page, int pageRow, string strWhere)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select [GCODE] ,[GNAME] ,[GTYPE],[GUnit],[GStandard]
                      ,[GDJ],[GSupplyer],[GTXM],[SPShortCode],[CrtBy],[Crtdt],[ModBy]
                      ,[Moddt],[GBalance],[ACTIVE],[COMBFLAG],[gindj],[subflag],[madein]
                      ,[Ffreeflag],[balflag],[Serviceflag],[gjm],[src],[data],[Xgsl],[XgMode]");
                strSql.Append(" from (");
                strSql.Append( "select ROW_NUMBER() OVER (ORDER BY gcode) AS RowNumber,* from T_Goods");
                if(strWhere!="")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                return conn.Query<T_Goods>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateByShortCode(SelfhelpOrderMgr.Model.T_Goods model)
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
            strSql.Append(" GTYPE = @GTYPE , ");
            strSql.Append(" GUnit = @GUnit , ");
            strSql.Append(" GStandard = @GStandard , ");
            strSql.Append(" GDJ = @GDJ , ");
            strSql.Append(" GSupplyer = @GSupplyer , ");
            strSql.Append(" GTXM = @GTXM , ");
            strSql.Append(" XgMode = @XgMode  ");
            strSql.Append(" where SPShortCode = @SPShortCode  ");

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
                        new SqlParameter("@gjm", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@src", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@data", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Xgsl", SqlDbType.Int,4) ,            
                        new SqlParameter("@GTYPE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GUnit", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GStandard", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GSupplyer", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,50) ,  
                        new SqlParameter("@XgMode", SqlDbType.Int,4) ,
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
            parameters[19].Value = model.GTYPE;
            parameters[20].Value = model.GUnit;
            parameters[21].Value = model.GStandard;
            parameters[22].Value = model.GDJ;
            parameters[23].Value = model.GSupplyer;
            parameters[24].Value = model.GTXM;
            parameters[25].Value = model.XgMode;
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

    }
}