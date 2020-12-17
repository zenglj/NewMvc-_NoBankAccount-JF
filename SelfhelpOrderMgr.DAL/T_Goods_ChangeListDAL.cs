using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Goods_ChangeList
    public partial class T_Goods_ChangeListDAL
    {

        public bool Exists(int Seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Goods_ChangeList");
            strSql.Append(" where ");
            strSql.Append(" Seqno = @Seqno  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = Seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_Goods_ChangeList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_ChangeList(");
            strSql.Append("CrtBy,CrtDate,AuditBy,AuditArea,AuditDate,AuditInfo,AuditFlag,Remark,GCode,GName,GTXM,ChangeType,ChangeTypeName,OldPrice,NewPrice,ChangeInfo");
            strSql.Append(") values (");
            strSql.Append("@CrtBy,@CrtDate,@AuditBy,@AuditArea,@AuditDate,@AuditInfo,@AuditFlag,@Remark,@GCode,@GName,@GTXM,@ChangeType,@ChangeTypeName,@OldPrice,@NewPrice,@ChangeInfo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditArea", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChangeType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChangeTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@NewPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChangeInfo", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.CrtBy;
            parameters[1].Value = model.CrtDate;
            parameters[2].Value = model.AuditBy;
            parameters[3].Value = model.AuditArea;
            parameters[4].Value = model.AuditDate;
            parameters[5].Value = model.AuditInfo;
            parameters[6].Value = model.AuditFlag;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.GCode;
            parameters[9].Value = model.GName;
            parameters[10].Value = model.GTXM;
            parameters[11].Value = model.ChangeType;
            parameters[12].Value = model.ChangeTypeName;
            parameters[13].Value = model.OldPrice;
            parameters[14].Value = model.NewPrice;
            parameters[15].Value = model.ChangeInfo;

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
        public bool Update(SelfhelpOrderMgr.Model.T_Goods_ChangeList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods_ChangeList set ");

            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDate = @CrtDate , ");
            strSql.Append(" AuditBy = @AuditBy , ");
            strSql.Append(" AuditArea = @AuditArea , ");
            strSql.Append(" AuditDate = @AuditDate , ");
            strSql.Append(" AuditInfo = @AuditInfo , ");
            strSql.Append(" AuditFlag = @AuditFlag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" GCode = @GCode , ");
            strSql.Append(" GName = @GName , ");
            strSql.Append(" GTXM = @GTXM , ");
            strSql.Append(" ChangeType = @ChangeType , ");
            strSql.Append(" ChangeTypeName = @ChangeTypeName , ");
            strSql.Append(" OldPrice = @OldPrice , ");
            strSql.Append(" NewPrice = @NewPrice , ");
            strSql.Append(" ChangeInfo = @ChangeInfo  ");
            strSql.Append(" where Seqno=@Seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditArea", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@GCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChangeType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChangeTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@NewPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChangeInfo", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.Seqno;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDate;
            parameters[3].Value = model.AuditBy;
            parameters[4].Value = model.AuditArea;
            parameters[5].Value = model.AuditDate;
            parameters[6].Value = model.AuditInfo;
            parameters[7].Value = model.AuditFlag;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.GCode;
            parameters[10].Value = model.GName;
            parameters[11].Value = model.GTXM;
            parameters[12].Value = model.ChangeType;
            parameters[13].Value = model.ChangeTypeName;
            parameters[14].Value = model.OldPrice;
            parameters[15].Value = model.NewPrice;
            parameters[16].Value = model.ChangeInfo;
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
            strSql.Append("delete from T_Goods_ChangeList ");
            strSql.Append(" where Seqno=@Seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)
			};
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Seqnolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_ChangeList ");
            strSql.Append(" where ID in (" + Seqnolist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_Goods_ChangeList GetModel(int Seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Seqno, CrtBy, CrtDate, AuditBy, AuditArea, AuditDate, AuditInfo, AuditFlag, Remark, GCode, GName, GTXM, ChangeType, ChangeTypeName, OldPrice, NewPrice, ChangeInfo  ");
            strSql.Append("  from T_Goods_ChangeList ");
            strSql.Append(" where Seqno=@Seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = Seqno;


            SelfhelpOrderMgr.Model.T_Goods_ChangeList model = new SelfhelpOrderMgr.Model.T_Goods_ChangeList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Seqno"].ToString() != "")
                {
                    model.Seqno = int.Parse(ds.Tables[0].Rows[0]["Seqno"].ToString());
                }
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDate"].ToString() != "")
                {
                    model.CrtDate = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDate"].ToString());
                }
                model.AuditBy = ds.Tables[0].Rows[0]["AuditBy"].ToString();
                model.AuditArea = ds.Tables[0].Rows[0]["AuditArea"].ToString();
                if (ds.Tables[0].Rows[0]["AuditDate"].ToString() != "")
                {
                    model.AuditDate = DateTime.Parse(ds.Tables[0].Rows[0]["AuditDate"].ToString());
                }
                model.AuditInfo = ds.Tables[0].Rows[0]["AuditInfo"].ToString();
                if (ds.Tables[0].Rows[0]["AuditFlag"].ToString() != "")
                {
                    model.AuditFlag = int.Parse(ds.Tables[0].Rows[0]["AuditFlag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.GCode = ds.Tables[0].Rows[0]["GCode"].ToString();
                model.GName = ds.Tables[0].Rows[0]["GName"].ToString();
                model.GTXM = ds.Tables[0].Rows[0]["GTXM"].ToString();
                model.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
                model.ChangeTypeName = ds.Tables[0].Rows[0]["ChangeTypeName"].ToString();
                if (ds.Tables[0].Rows[0]["OldPrice"].ToString() != "")
                {
                    model.OldPrice = decimal.Parse(ds.Tables[0].Rows[0]["OldPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewPrice"].ToString() != "")
                {
                    model.NewPrice = decimal.Parse(ds.Tables[0].Rows[0]["NewPrice"].ToString());
                }
                model.ChangeInfo = ds.Tables[0].Rows[0]["ChangeInfo"].ToString();

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
            strSql.Append(" FROM T_Goods_ChangeList ");
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
            strSql.Append(" FROM T_Goods_ChangeList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

