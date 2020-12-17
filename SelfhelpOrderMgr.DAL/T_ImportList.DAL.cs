using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_ImportList
    public partial class T_ImportListDAL
    {

        public bool Exists(int seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_ImportList");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_ImportList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ImportList(");
            strSql.Append("notes,AmountA,AmountB,AmountC,ImportType,fcrimecode,fname,Amount,Crtdt,CrtBy,Remark,pc");
            strSql.Append(") values (");
            strSql.Append("@notes,@AmountA,@AmountB,@AmountC,@ImportType,@fcrimecode,@fname,@Amount,@Crtdt,@CrtBy,@Remark,@pc");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@notes", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ImportType", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,256) ,            
                        new SqlParameter("@pc", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.notes;
            parameters[1].Value = model.AmountA;
            parameters[2].Value = model.AmountB;
            parameters[3].Value = model.AmountC;
            parameters[4].Value = model.ImportType;
            parameters[5].Value = model.fcrimecode;
            parameters[6].Value = model.fname;
            parameters[7].Value = model.Amount;
            parameters[8].Value = model.Crtdt;
            parameters[9].Value = model.CrtBy;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.pc;

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
        public bool Update(SelfhelpOrderMgr.Model.T_ImportList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ImportList set ");

            strSql.Append(" notes = @notes , ");
            strSql.Append(" AmountA = @AmountA , ");
            strSql.Append(" AmountB = @AmountB , ");
            strSql.Append(" AmountC = @AmountC , ");
            strSql.Append(" ImportType = @ImportType , ");
            strSql.Append(" fcrimecode = @fcrimecode , ");
            strSql.Append(" fname = @fname , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" Crtdt = @Crtdt , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" pc = @pc  ");
            strSql.Append(" where seqno=@seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@notes", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ImportType", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,256) ,            
                        new SqlParameter("@pc", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.seqno;
            parameters[1].Value = model.notes;
            parameters[2].Value = model.AmountA;
            parameters[3].Value = model.AmountB;
            parameters[4].Value = model.AmountC;
            parameters[5].Value = model.ImportType;
            parameters[6].Value = model.fcrimecode;
            parameters[7].Value = model.fname;
            parameters[8].Value = model.Amount;
            parameters[9].Value = model.Crtdt;
            parameters[10].Value = model.CrtBy;
            parameters[11].Value = model.Remark;
            parameters[12].Value = model.pc;
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
        public bool Delete(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ImportList ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


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
        public bool DeleteList(string seqnolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ImportList ");
            strSql.Append(" where ID in (" + seqnolist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_ImportList GetModel(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select seqno, notes, AmountA, AmountB, AmountC, ImportType, fcrimecode, fname, Amount, Crtdt, CrtBy, Remark, pc  ");
            strSql.Append("  from T_ImportList ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


            SelfhelpOrderMgr.Model.T_ImportList model = new SelfhelpOrderMgr.Model.T_ImportList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
                {
                    model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
                }
                model.notes = ds.Tables[0].Rows[0]["notes"].ToString();
                if (ds.Tables[0].Rows[0]["AmountA"].ToString() != "")
                {
                    model.AmountA = decimal.Parse(ds.Tables[0].Rows[0]["AmountA"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AmountB"].ToString() != "")
                {
                    model.AmountB = decimal.Parse(ds.Tables[0].Rows[0]["AmountB"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AmountC"].ToString() != "")
                {
                    model.AmountC = decimal.Parse(ds.Tables[0].Rows[0]["AmountC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ImportType"].ToString() != "")
                {
                    model.ImportType = int.Parse(ds.Tables[0].Rows[0]["ImportType"].ToString());
                }
                model.fcrimecode = ds.Tables[0].Rows[0]["fcrimecode"].ToString();
                model.fname = ds.Tables[0].Rows[0]["fname"].ToString();
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Crtdt"].ToString() != "")
                {
                    model.Crtdt = DateTime.Parse(ds.Tables[0].Rows[0]["Crtdt"].ToString());
                }
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.pc = ds.Tables[0].Rows[0]["pc"].ToString();

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
            strSql.Append(" FROM T_ImportList ");
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
            strSql.Append(" FROM T_ImportList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

