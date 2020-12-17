using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
	//t_balanceList
	public partial class t_balanceListDAL
	{

		public bool Exists(int seqno)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from t_balanceList");
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
		public int Add(SelfhelpOrderMgr.Model.t_balanceList model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into t_balanceList(");
			strSql.Append("AmountB,crtdate,crtby,remark,baltype,DEPOSITER,AmountC,bankamount,PayMode,fcrimecode,vounoa,cardcodea,typeflagA,AmountA,vounob,cardcodeB,typeflagB");
			strSql.Append(") values (");
			strSql.Append("@AmountB,@crtdate,@crtby,@remark,@baltype,@DEPOSITER,@AmountC,@bankamount,@PayMode,@fcrimecode,@vounoa,@cardcodea,@typeflagA,@AmountA,@vounob,@cardcodeB,@typeflagB");
			strSql.Append(") ");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
						new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,
						new SqlParameter("@crtdate", SqlDbType.DateTime) ,
						new SqlParameter("@crtby", SqlDbType.VarChar,20) ,
						new SqlParameter("@remark", SqlDbType.VarChar,256) ,
						new SqlParameter("@baltype", SqlDbType.Int,4) ,
						new SqlParameter("@DEPOSITER", SqlDbType.VarChar,30) ,
						new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,
						new SqlParameter("@bankamount", SqlDbType.Decimal,9) ,
						new SqlParameter("@PayMode", SqlDbType.Int,4) ,
						new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,
						new SqlParameter("@vounoa", SqlDbType.VarChar,20) ,
						new SqlParameter("@cardcodea", SqlDbType.VarChar,20) ,
						new SqlParameter("@typeflagA", SqlDbType.Int,4) ,
						new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,
						new SqlParameter("@vounob", SqlDbType.VarChar,20) ,
						new SqlParameter("@cardcodeB", SqlDbType.VarChar,20) ,
						new SqlParameter("@typeflagB", SqlDbType.Int,4)

			};

			parameters[0].Value = model.AmountB;
			parameters[1].Value = model.crtdate;
			parameters[2].Value = model.crtby;
			parameters[3].Value = model.remark;
			parameters[4].Value = model.baltype;
			parameters[5].Value = model.DEPOSITER;
			parameters[6].Value = model.AmountC;
			parameters[7].Value = model.bankamount;
			parameters[8].Value = model.PayMode;
			parameters[9].Value = model.fcrimecode;
			parameters[10].Value = model.vounoa;
			parameters[11].Value = model.cardcodea;
			parameters[12].Value = model.typeflagA;
			parameters[13].Value = model.AmountA;
			parameters[14].Value = model.vounob;
			parameters[15].Value = model.cardcodeB;
			parameters[16].Value = model.typeflagB;

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
		public bool Update(SelfhelpOrderMgr.Model.t_balanceList model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update t_balanceList set ");

			strSql.Append(" AmountB = @AmountB , ");
			strSql.Append(" crtdate = @crtdate , ");
			strSql.Append(" crtby = @crtby , ");
			strSql.Append(" remark = @remark , ");
			strSql.Append(" baltype = @baltype , ");
			strSql.Append(" DEPOSITER = @DEPOSITER , ");
			strSql.Append(" AmountC = @AmountC , ");
			strSql.Append(" bankamount = @bankamount , ");
			strSql.Append(" PayMode = @PayMode , ");
			strSql.Append(" fcrimecode = @fcrimecode , ");
			strSql.Append(" vounoa = @vounoa , ");
			strSql.Append(" cardcodea = @cardcodea , ");
			strSql.Append(" typeflagA = @typeflagA , ");
			strSql.Append(" AmountA = @AmountA , ");
			strSql.Append(" vounob = @vounob , ");
			strSql.Append(" cardcodeB = @cardcodeB , ");
			strSql.Append(" typeflagB = @typeflagB  ");
			strSql.Append(" where seqno=@seqno ");

			SqlParameter[] parameters = {
						new SqlParameter("@seqno", SqlDbType.Int,4) ,
						new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,
						new SqlParameter("@crtdate", SqlDbType.DateTime) ,
						new SqlParameter("@crtby", SqlDbType.VarChar,20) ,
						new SqlParameter("@remark", SqlDbType.VarChar,256) ,
						new SqlParameter("@baltype", SqlDbType.Int,4) ,
						new SqlParameter("@DEPOSITER", SqlDbType.VarChar,30) ,
						new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,
						new SqlParameter("@bankamount", SqlDbType.Decimal,9) ,
						new SqlParameter("@PayMode", SqlDbType.Int,4) ,
						new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,
						new SqlParameter("@vounoa", SqlDbType.VarChar,20) ,
						new SqlParameter("@cardcodea", SqlDbType.VarChar,20) ,
						new SqlParameter("@typeflagA", SqlDbType.Int,4) ,
						new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,
						new SqlParameter("@vounob", SqlDbType.VarChar,20) ,
						new SqlParameter("@cardcodeB", SqlDbType.VarChar,20) ,
						new SqlParameter("@typeflagB", SqlDbType.Int,4)

			};

			parameters[0].Value = model.seqno;
			parameters[1].Value = model.AmountB;
			parameters[2].Value = model.crtdate;
			parameters[3].Value = model.crtby;
			parameters[4].Value = model.remark;
			parameters[5].Value = model.baltype;
			parameters[6].Value = model.DEPOSITER;
			parameters[7].Value = model.AmountC;
			parameters[8].Value = model.bankamount;
			parameters[9].Value = model.PayMode;
			parameters[10].Value = model.fcrimecode;
			parameters[11].Value = model.vounoa;
			parameters[12].Value = model.cardcodea;
			parameters[13].Value = model.typeflagA;
			parameters[14].Value = model.AmountA;
			parameters[15].Value = model.vounob;
			parameters[16].Value = model.cardcodeB;
			parameters[17].Value = model.typeflagB;
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
			strSql.Append("delete from t_balanceList ");
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
			strSql.Append("delete from t_balanceList ");
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
		public SelfhelpOrderMgr.Model.t_balanceList GetModel(int seqno)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select seqno, AmountB, crtdate, crtby, remark, baltype, DEPOSITER, AmountC, bankamount, PayMode, fcrimecode, vounoa, cardcodea, typeflagA, AmountA, vounob, cardcodeB, typeflagB  ");
			strSql.Append("  from t_balanceList ");
			strSql.Append(" where seqno=@seqno");
			SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;


			SelfhelpOrderMgr.Model.t_balanceList model = new SelfhelpOrderMgr.Model.t_balanceList();
			DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
				{
					model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AmountB"].ToString() != "")
				{
					model.AmountB = decimal.Parse(ds.Tables[0].Rows[0]["AmountB"].ToString());
				}
				if (ds.Tables[0].Rows[0]["crtdate"].ToString() != "")
				{
					model.crtdate = DateTime.Parse(ds.Tables[0].Rows[0]["crtdate"].ToString());
				}
				model.crtby = ds.Tables[0].Rows[0]["crtby"].ToString();
				model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
				if (ds.Tables[0].Rows[0]["baltype"].ToString() != "")
				{
					model.baltype = int.Parse(ds.Tables[0].Rows[0]["baltype"].ToString());
				}
				model.DEPOSITER = ds.Tables[0].Rows[0]["DEPOSITER"].ToString();
				if (ds.Tables[0].Rows[0]["AmountC"].ToString() != "")
				{
					model.AmountC = decimal.Parse(ds.Tables[0].Rows[0]["AmountC"].ToString());
				}
				if (ds.Tables[0].Rows[0]["bankamount"].ToString() != "")
				{
					model.bankamount = decimal.Parse(ds.Tables[0].Rows[0]["bankamount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PayMode"].ToString() != "")
				{
					model.PayMode = int.Parse(ds.Tables[0].Rows[0]["PayMode"].ToString());
				}
				model.fcrimecode = ds.Tables[0].Rows[0]["fcrimecode"].ToString();
				model.vounoa = ds.Tables[0].Rows[0]["vounoa"].ToString();
				model.cardcodea = ds.Tables[0].Rows[0]["cardcodea"].ToString();
				if (ds.Tables[0].Rows[0]["typeflagA"].ToString() != "")
				{
					model.typeflagA = int.Parse(ds.Tables[0].Rows[0]["typeflagA"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AmountA"].ToString() != "")
				{
					model.AmountA = decimal.Parse(ds.Tables[0].Rows[0]["AmountA"].ToString());
				}
				model.vounob = ds.Tables[0].Rows[0]["vounob"].ToString();
				model.cardcodeB = ds.Tables[0].Rows[0]["cardcodeB"].ToString();
				if (ds.Tables[0].Rows[0]["typeflagB"].ToString() != "")
				{
					model.typeflagB = int.Parse(ds.Tables[0].Rows[0]["typeflagB"].ToString());
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
			strSql.Append(" FROM t_balanceList ");
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
			strSql.Append(" FROM t_balanceList ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}


	}
}

