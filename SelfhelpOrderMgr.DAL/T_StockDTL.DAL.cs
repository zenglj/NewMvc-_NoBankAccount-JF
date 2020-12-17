using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    //T_StockDTL
    public partial class T_StockDTLDAL
    {

        public bool Exists(int SeqId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_StockDTL");
            strSql.Append(" where ");
            strSql.Append(" SeqId = @SeqId  ");
            SqlParameter[] parameters = {
					new SqlParameter("@SeqId", SqlDbType.Int,4)
			};
            parameters[0].Value = SeqId;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_StockDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_StockDTL(");
            strSql.Append("Remark,WareHouseCode,StockId,GCode,GTXM,GCount,GDJ,Flag,StockFlag,InOutFlag");
            strSql.Append(") values (");
            strSql.Append("@Remark,@WareHouseCode,@StockId,@GCode,@GTXM,@GCount,@GDJ,@Flag,@StockFlag,@InOutFlag");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WareHouseCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StockId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GCount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@StockFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Remark;
            parameters[1].Value = model.WareHouseCode;
            parameters[2].Value = model.StockId;
            parameters[3].Value = model.GCode;
            parameters[4].Value = model.GTXM;
            parameters[5].Value = model.GCount;
            parameters[6].Value = model.GDJ;
            parameters[7].Value = model.Flag;
            parameters[8].Value = model.StockFlag;
            parameters[9].Value = model.InOutFlag;

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
        public bool Update(SelfhelpOrderMgr.Model.T_StockDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_StockDTL set ");

            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" WareHouseCode = @WareHouseCode , ");
            strSql.Append(" StockId = @StockId , ");
            strSql.Append(" GCode = @GCode , ");
            strSql.Append(" GTXM = @GTXM , ");
            strSql.Append(" GCount = @GCount , ");
            strSql.Append(" GDJ = @GDJ , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" StockFlag = @StockFlag , ");
            strSql.Append(" InOutFlag = @InOutFlag  ");
            strSql.Append(" where SeqId=@SeqId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@SeqId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WareHouseCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StockId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GCount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@StockFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.SeqId;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.WareHouseCode;
            parameters[3].Value = model.StockId;
            parameters[4].Value = model.GCode;
            parameters[5].Value = model.GTXM;
            parameters[6].Value = model.GCount;
            parameters[7].Value = model.GDJ;
            parameters[8].Value = model.Flag;
            parameters[9].Value = model.StockFlag;
            parameters[10].Value = model.InOutFlag;
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
        public bool Delete(int SeqId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_StockDTL ");
            strSql.Append(" where SeqId=@SeqId");
            SqlParameter[] parameters = {
					new SqlParameter("@SeqId", SqlDbType.Int,4)
			};
            parameters[0].Value = SeqId;


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
        public bool DeleteList(string SeqIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_StockDTL ");
            strSql.Append(" where ID in (" + SeqIdlist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_StockDTL GetModel(int SeqId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SeqId, Remark, WareHouseCode, StockId, GCode, GTXM, GCount, GDJ, Flag, StockFlag, InOutFlag  ");
            strSql.Append("  from T_StockDTL ");
            strSql.Append(" where SeqId=@SeqId");
            SqlParameter[] parameters = {
					new SqlParameter("@SeqId", SqlDbType.Int,4)
			};
            parameters[0].Value = SeqId;


            SelfhelpOrderMgr.Model.T_StockDTL model = new SelfhelpOrderMgr.Model.T_StockDTL();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SeqId"].ToString() != "")
                {
                    model.SeqId = int.Parse(ds.Tables[0].Rows[0]["SeqId"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.WareHouseCode = ds.Tables[0].Rows[0]["WareHouseCode"].ToString();
                model.StockId = ds.Tables[0].Rows[0]["StockId"].ToString();
                model.GCode = ds.Tables[0].Rows[0]["GCode"].ToString();
                model.GTXM = ds.Tables[0].Rows[0]["GTXM"].ToString();
                if (ds.Tables[0].Rows[0]["GCount"].ToString() != "")
                {
                    model.GCount = decimal.Parse(ds.Tables[0].Rows[0]["GCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GDJ"].ToString() != "")
                {
                    model.GDJ = decimal.Parse(ds.Tables[0].Rows[0]["GDJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StockFlag"].ToString() != "")
                {
                    model.StockFlag = int.Parse(ds.Tables[0].Rows[0]["StockFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InOutFlag"].ToString() != "")
                {
                    model.InOutFlag = int.Parse(ds.Tables[0].Rows[0]["InOutFlag"].ToString());
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
            strSql.Append(" FROM T_StockDTL ");
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
            strSql.Append(" FROM T_StockDTL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

