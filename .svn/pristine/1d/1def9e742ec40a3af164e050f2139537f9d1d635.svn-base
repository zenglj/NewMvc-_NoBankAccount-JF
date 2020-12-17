using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_Provide
    public partial class T_ProvideDAL
    {

        public bool Exists(string PId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Provide");
            strSql.Append(" where ");
            strSql.Append(" PId = @PId  ");
            SqlParameter[] parameters = {
					new SqlParameter("@PId", SqlDbType.VarChar,20)			};
            parameters[0].Value = PId;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Provide model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Provide(");
            strSql.Append("PId,CrtBy,CrtDt,FrealareaCode,Flag,CheckBy,CheckDt,PType,PFlag,ApplyBy,ApplyDt,FAreaCode,FRealareaName,FAreaName,PTag,PTagName,FManNb,FWomNb,FManAmount,FWomAmount,PDate,FAmount,Remark");
            strSql.Append(") values (");
            strSql.Append("@PId,@CrtBy,@CrtDt,@FrealareaCode,@Flag,@CheckBy,@CheckDt,@PType,@PFlag,@ApplyBy,@ApplyDt,@FAreaCode,@FRealareaName,@FAreaName,@PTag,@PTagName,@FManNb,@FWomNb,@FManAmount,@FWomAmount,@PDate,@FAmount,@Remark");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@FrealareaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CheckBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ApplyDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FRealareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PTag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PTagName", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@FManNb", SqlDbType.Int,4) ,            
                        new SqlParameter("@FWomNb", SqlDbType.Int,4) ,            
                        new SqlParameter("@FManAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FWomAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@PDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,250)             
              
            };

            parameters[0].Value = model.PId;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDt;
            parameters[3].Value = model.FrealareaCode;
            parameters[4].Value = model.Flag;
            parameters[5].Value = model.CheckBy;
            parameters[6].Value = model.CheckDt;
            parameters[7].Value = model.PType;
            parameters[8].Value = model.PFlag;
            parameters[9].Value = model.ApplyBy;
            parameters[10].Value = model.ApplyDt;
            parameters[11].Value = model.FAreaCode;
            parameters[12].Value = model.FRealareaName;
            parameters[13].Value = model.FAreaName;
            parameters[14].Value = model.PTag;
            parameters[15].Value = model.PTagName;
            parameters[16].Value = model.FManNb;
            parameters[17].Value = model.FWomNb;
            parameters[18].Value = model.FManAmount;
            parameters[19].Value = model.FWomAmount;
            parameters[20].Value = model.PDate;
            parameters[21].Value = model.FAmount;
            parameters[22].Value = model.Remark;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Provide model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Provide set ");

            strSql.Append(" PId = @PId , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDt = @CrtDt , ");
            strSql.Append(" FrealareaCode = @FrealareaCode , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" CheckBy = @CheckBy , ");
            strSql.Append(" CheckDt = @CheckDt , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" PFlag = @PFlag , ");
            strSql.Append(" ApplyBy = @ApplyBy , ");
            strSql.Append(" ApplyDt = @ApplyDt , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FRealareaName = @FRealareaName , ");
            strSql.Append(" FAreaName = @FAreaName , ");
            strSql.Append(" PTag = @PTag , ");
            strSql.Append(" PTagName = @PTagName , ");
            strSql.Append(" FManNb = @FManNb , ");
            strSql.Append(" FWomNb = @FWomNb , ");
            strSql.Append(" FManAmount = @FManAmount , ");
            strSql.Append(" FWomAmount = @FWomAmount , ");
            strSql.Append(" PDate = @PDate , ");
            strSql.Append(" FAmount = @FAmount , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where PId=@PId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@FrealareaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CheckBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ApplyDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FRealareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PTag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PTagName", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@FManNb", SqlDbType.Int,4) ,            
                        new SqlParameter("@FWomNb", SqlDbType.Int,4) ,            
                        new SqlParameter("@FManAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FWomAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@PDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,250)             
              
            };

            parameters[0].Value = model.PId;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDt;
            parameters[3].Value = model.FrealareaCode;
            parameters[4].Value = model.Flag;
            parameters[5].Value = model.CheckBy;
            parameters[6].Value = model.CheckDt;
            parameters[7].Value = model.PType;
            parameters[8].Value = model.PFlag;
            parameters[9].Value = model.ApplyBy;
            parameters[10].Value = model.ApplyDt;
            parameters[11].Value = model.FAreaCode;
            parameters[12].Value = model.FRealareaName;
            parameters[13].Value = model.FAreaName;
            parameters[14].Value = model.PTag;
            parameters[15].Value = model.PTagName;
            parameters[16].Value = model.FManNb;
            parameters[17].Value = model.FWomNb;
            parameters[18].Value = model.FManAmount;
            parameters[19].Value = model.FWomAmount;
            parameters[20].Value = model.PDate;
            parameters[21].Value = model.FAmount;
            parameters[22].Value = model.Remark;
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
        public bool Delete(string PId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Provide ");
            strSql.Append(" where PId=@PId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PId", SqlDbType.VarChar,20)			};
            parameters[0].Value = PId;


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
        public SelfhelpOrderMgr.Model.T_Provide GetModel(string PId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PId, CrtBy, CrtDt, FrealareaCode, Flag, CheckBy, CheckDt, PType, PFlag, ApplyBy, ApplyDt, FAreaCode, FRealareaName, FAreaName, PTag, PTagName, FManNb, FWomNb, FManAmount, FWomAmount, PDate, FAmount, Remark  ");
            strSql.Append("  from T_Provide ");
            strSql.Append(" where PId=@PId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PId", SqlDbType.VarChar,20)			};
            parameters[0].Value = PId;


            SelfhelpOrderMgr.Model.T_Provide model = new SelfhelpOrderMgr.Model.T_Provide();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PId = ds.Tables[0].Rows[0]["PId"].ToString();
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDt"].ToString() != "")
                {
                    model.CrtDt = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDt"].ToString());
                }
                model.FrealareaCode = ds.Tables[0].Rows[0]["FrealareaCode"].ToString();
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.CheckBy = ds.Tables[0].Rows[0]["CheckBy"].ToString();
                if (ds.Tables[0].Rows[0]["CheckDt"].ToString() != "")
                {
                    model.CheckDt = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDt"].ToString());
                }
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                if (ds.Tables[0].Rows[0]["PFlag"].ToString() != "")
                {
                    model.PFlag = int.Parse(ds.Tables[0].Rows[0]["PFlag"].ToString());
                }
                model.ApplyBy = ds.Tables[0].Rows[0]["ApplyBy"].ToString();
                if (ds.Tables[0].Rows[0]["ApplyDt"].ToString() != "")
                {
                    model.ApplyDt = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyDt"].ToString());
                }
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                model.FRealareaName = ds.Tables[0].Rows[0]["FRealareaName"].ToString();
                model.FAreaName = ds.Tables[0].Rows[0]["FAreaName"].ToString();
                if (ds.Tables[0].Rows[0]["PTag"].ToString() != "")
                {
                    model.PTag = int.Parse(ds.Tables[0].Rows[0]["PTag"].ToString());
                }
                model.PTagName = ds.Tables[0].Rows[0]["PTagName"].ToString();
                if (ds.Tables[0].Rows[0]["FManNb"].ToString() != "")
                {
                    model.FManNb = int.Parse(ds.Tables[0].Rows[0]["FManNb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FWomNb"].ToString() != "")
                {
                    model.FWomNb = int.Parse(ds.Tables[0].Rows[0]["FWomNb"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FManAmount"].ToString() != "")
                {
                    model.FManAmount = decimal.Parse(ds.Tables[0].Rows[0]["FManAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FWomAmount"].ToString() != "")
                {
                    model.FWomAmount = decimal.Parse(ds.Tables[0].Rows[0]["FWomAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PDate"].ToString() != "")
                {
                    model.PDate = DateTime.Parse(ds.Tables[0].Rows[0]["PDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FAmount"].ToString() != "")
                {
                    model.FAmount = decimal.Parse(ds.Tables[0].Rows[0]["FAmount"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

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
            strSql.Append(" FROM T_Provide ");
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
            strSql.Append(" FROM T_Provide ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

