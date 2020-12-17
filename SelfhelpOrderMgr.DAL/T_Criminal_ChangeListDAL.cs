using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Criminal_ChangeList
    public partial class T_Criminal_ChangeListDAL
    {

        public bool Exists(int Seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Criminal_ChangeList");
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
        public int Add(SelfhelpOrderMgr.Model.T_Criminal_ChangeList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Criminal_ChangeList(");
            strSql.Append("ChangeInfo,CrtBy,CrtDate,AuditBy,AuditArea,AuditDate,AuditInfo,AuditFlag,Remark,FCode,FName,ChangeType,ChangeTypeName,OldCode,OldName,NewCode,NewName");
            strSql.Append(") values (");
            strSql.Append("@ChangeInfo,@CrtBy,@CrtDate,@AuditBy,@AuditArea,@AuditDate,@AuditInfo,@AuditFlag,@Remark,@FCode,@FName,@ChangeType,@ChangeTypeName,@OldCode,@OldName,@NewCode,@NewName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ChangeInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditArea", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChangeType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChangeTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NewCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NewName", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.ChangeInfo;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDate;
            parameters[3].Value = model.AuditBy;
            parameters[4].Value = model.AuditArea;
            parameters[5].Value = model.AuditDate;
            parameters[6].Value = model.AuditInfo;
            parameters[7].Value = model.AuditFlag;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.FCode;
            parameters[10].Value = model.FName;
            parameters[11].Value = model.ChangeType;
            parameters[12].Value = model.ChangeTypeName;
            parameters[13].Value = model.OldCode;
            parameters[14].Value = model.OldName;
            parameters[15].Value = model.NewCode;
            parameters[16].Value = model.NewName;

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
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal_ChangeList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Criminal_ChangeList set ");

            strSql.Append(" ChangeInfo = @ChangeInfo , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDate = @CrtDate , ");
            strSql.Append(" AuditBy = @AuditBy , ");
            strSql.Append(" AuditArea = @AuditArea , ");
            strSql.Append(" AuditDate = @AuditDate , ");
            strSql.Append(" AuditInfo = @AuditInfo , ");
            strSql.Append(" AuditFlag = @AuditFlag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" ChangeType = @ChangeType , ");
            strSql.Append(" ChangeTypeName = @ChangeTypeName , ");
            strSql.Append(" OldCode = @OldCode , ");
            strSql.Append(" OldName = @OldName , ");
            strSql.Append(" NewCode = @NewCode , ");
            strSql.Append(" NewName = @NewName  ");
            strSql.Append(" where Seqno=@Seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChangeInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditArea", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuditDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AuditInfo", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChangeType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChangeTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OldName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NewCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NewName", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.Seqno;
            parameters[1].Value = model.ChangeInfo;
            parameters[2].Value = model.CrtBy;
            parameters[3].Value = model.CrtDate;
            parameters[4].Value = model.AuditBy;
            parameters[5].Value = model.AuditArea;
            parameters[6].Value = model.AuditDate;
            parameters[7].Value = model.AuditInfo;
            parameters[8].Value = model.AuditFlag;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.FCode;
            parameters[11].Value = model.FName;
            parameters[12].Value = model.ChangeType;
            parameters[13].Value = model.ChangeTypeName;
            parameters[14].Value = model.OldCode;
            parameters[15].Value = model.OldName;
            parameters[16].Value = model.NewCode;
            parameters[17].Value = model.NewName;
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
            strSql.Append("delete from T_Criminal_ChangeList ");
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
            strSql.Append("delete from T_Criminal_ChangeList ");
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
        public SelfhelpOrderMgr.Model.T_Criminal_ChangeList GetModel(int Seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Seqno, ChangeInfo, CrtBy, CrtDate, AuditBy, AuditArea, AuditDate, AuditInfo, AuditFlag, Remark, FCode, FName, ChangeType, ChangeTypeName, OldCode, OldName, NewCode, NewName  ");
            strSql.Append("  from T_Criminal_ChangeList ");
            strSql.Append(" where Seqno=@Seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@Seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = Seqno;


            SelfhelpOrderMgr.Model.T_Criminal_ChangeList model = new SelfhelpOrderMgr.Model.T_Criminal_ChangeList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Seqno"].ToString() != "")
                {
                    model.Seqno = int.Parse(ds.Tables[0].Rows[0]["Seqno"].ToString());
                }
                model.ChangeInfo = ds.Tables[0].Rows[0]["ChangeInfo"].ToString();
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
                model.FCode = ds.Tables[0].Rows[0]["FCode"].ToString();
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                model.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
                model.ChangeTypeName = ds.Tables[0].Rows[0]["ChangeTypeName"].ToString();
                model.OldCode = ds.Tables[0].Rows[0]["OldCode"].ToString();
                model.OldName = ds.Tables[0].Rows[0]["OldName"].ToString();
                model.NewCode = ds.Tables[0].Rows[0]["NewCode"].ToString();
                model.NewName = ds.Tables[0].Rows[0]["NewName"].ToString();

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
            strSql.Append(" FROM T_Criminal_ChangeList ");
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
            strSql.Append(" FROM T_Criminal_ChangeList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

