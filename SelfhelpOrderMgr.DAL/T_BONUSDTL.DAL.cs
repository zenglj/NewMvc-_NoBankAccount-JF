﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
	//T_BONUSDTL
	public partial class T_BONUSDTLDAL
	{

		public bool Exists(int seqno)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from T_BONUSDTL");
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
		public int Add(SelfhelpOrderMgr.Model.T_BONUSDTL model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into T_BONUSDTL(");
			strSql.Append("vouno,Frealareacode,FrealAreaName,remark,ptype,udate,crtby,crtdt,applyby,acctype,BID,cardtype,AmountC,cqbt,gwjt,ldjx,tbbz,grkj,AmountA,AmountB,FCRIMECODE,CARDCODE,FAMOUNT,FLAG,fareacode,fareaName,fcriminal");
			strSql.Append(") values (");
			strSql.Append("@vouno,@Frealareacode,@FrealAreaName,@remark,@ptype,@udate,@crtby,@crtdt,@applyby,@acctype,@BID,@cardtype,@AmountC,@cqbt,@gwjt,@ldjx,@tbbz,@grkj,@AmountA,@AmountB,@FCRIMECODE,@CARDCODE,@FAMOUNT,@FLAG,@fareacode,@fareaName,@fcriminal");
			strSql.Append(") ");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
						new SqlParameter("@vouno", SqlDbType.VarChar,20) ,
						new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,
						new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,
						new SqlParameter("@remark", SqlDbType.VarChar,512) ,
						new SqlParameter("@ptype", SqlDbType.VarChar,100) ,
						new SqlParameter("@udate", SqlDbType.DateTime) ,
						new SqlParameter("@crtby", SqlDbType.VarChar,20) ,
						new SqlParameter("@crtdt", SqlDbType.DateTime) ,
						new SqlParameter("@applyby", SqlDbType.VarChar,20) ,
						new SqlParameter("@acctype", SqlDbType.Int,4) ,
						new SqlParameter("@BID", SqlDbType.VarChar,20) ,
						new SqlParameter("@cardtype", SqlDbType.Int,4) ,
						new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,
						new SqlParameter("@cqbt", SqlDbType.Decimal,9) ,
						new SqlParameter("@gwjt", SqlDbType.Decimal,9) ,
						new SqlParameter("@ldjx", SqlDbType.Decimal,9) ,
						new SqlParameter("@tbbz", SqlDbType.Decimal,9) ,
						new SqlParameter("@grkj", SqlDbType.Decimal,9) ,
						new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,
						new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,
						new SqlParameter("@FCRIMECODE", SqlDbType.VarChar,20) ,
						new SqlParameter("@CARDCODE", SqlDbType.VarChar,20) ,
						new SqlParameter("@FAMOUNT", SqlDbType.Decimal,9) ,
						new SqlParameter("@FLAG", SqlDbType.Int,4) ,
						new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,
						new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,
						new SqlParameter("@fcriminal", SqlDbType.VarChar,50)

			};

			parameters[0].Value = model.vouno;
			parameters[1].Value = model.Frealareacode;
			parameters[2].Value = model.FrealAreaName;
			parameters[3].Value = model.remark;
			parameters[4].Value = model.ptype;
			parameters[5].Value = model.udate;
			parameters[6].Value = model.crtby;
			parameters[7].Value = model.crtdt;
			parameters[8].Value = model.applyby;
			parameters[9].Value = model.acctype;
			parameters[10].Value = model.BID;
			parameters[11].Value = model.cardtype;
			parameters[12].Value = model.AmountC;
			parameters[13].Value = model.cqbt;
			parameters[14].Value = model.gwjt;
			parameters[15].Value = model.ldjx;
			parameters[16].Value = model.tbbz;
			parameters[17].Value = model.grkj;
			parameters[18].Value = model.AmountA;
			parameters[19].Value = model.AmountB;
			parameters[20].Value = model.FCRIMECODE;
			parameters[21].Value = model.CARDCODE;
			parameters[22].Value = model.FAMOUNT;
			parameters[23].Value = model.FLAG;
			parameters[24].Value = model.fareacode;
			parameters[25].Value = model.fareaName;
			parameters[26].Value = model.fcriminal;

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
		public bool Update(SelfhelpOrderMgr.Model.T_BONUSDTL model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update T_BONUSDTL set ");

			strSql.Append(" vouno = @vouno , ");
			strSql.Append(" Frealareacode = @Frealareacode , ");
			strSql.Append(" FrealAreaName = @FrealAreaName , ");
			strSql.Append(" remark = @remark , ");
			strSql.Append(" ptype = @ptype , ");
			strSql.Append(" udate = @udate , ");
			strSql.Append(" crtby = @crtby , ");
			strSql.Append(" crtdt = @crtdt , ");
			strSql.Append(" applyby = @applyby , ");
			strSql.Append(" acctype = @acctype , ");
			strSql.Append(" BID = @BID , ");
			strSql.Append(" cardtype = @cardtype , ");
			strSql.Append(" AmountC = @AmountC , ");
			strSql.Append(" cqbt = @cqbt , ");
			strSql.Append(" gwjt = @gwjt , ");
			strSql.Append(" ldjx = @ldjx , ");
			strSql.Append(" tbbz = @tbbz , ");
			strSql.Append(" grkj = @grkj , ");
			strSql.Append(" AmountA = @AmountA , ");
			strSql.Append(" AmountB = @AmountB , ");
			strSql.Append(" FCRIMECODE = @FCRIMECODE , ");
			strSql.Append(" CARDCODE = @CARDCODE , ");
			strSql.Append(" FAMOUNT = @FAMOUNT , ");
			strSql.Append(" FLAG = @FLAG , ");
			strSql.Append(" fareacode = @fareacode , ");
			strSql.Append(" fareaName = @fareaName , ");
			strSql.Append(" fcriminal = @fcriminal  ");
			strSql.Append(" where seqno=@seqno ");

			SqlParameter[] parameters = {
						new SqlParameter("@seqno", SqlDbType.Int,4) ,
						new SqlParameter("@vouno", SqlDbType.VarChar,20) ,
						new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,
						new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,
						new SqlParameter("@remark", SqlDbType.VarChar,512) ,
						new SqlParameter("@ptype", SqlDbType.VarChar,100) ,
						new SqlParameter("@udate", SqlDbType.DateTime) ,
						new SqlParameter("@crtby", SqlDbType.VarChar,20) ,
						new SqlParameter("@crtdt", SqlDbType.DateTime) ,
						new SqlParameter("@applyby", SqlDbType.VarChar,20) ,
						new SqlParameter("@acctype", SqlDbType.Int,4) ,
						new SqlParameter("@BID", SqlDbType.VarChar,20) ,
						new SqlParameter("@cardtype", SqlDbType.Int,4) ,
						new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,
						new SqlParameter("@cqbt", SqlDbType.Decimal,9) ,
						new SqlParameter("@gwjt", SqlDbType.Decimal,9) ,
						new SqlParameter("@ldjx", SqlDbType.Decimal,9) ,
						new SqlParameter("@tbbz", SqlDbType.Decimal,9) ,
						new SqlParameter("@grkj", SqlDbType.Decimal,9) ,
						new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,
						new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,
						new SqlParameter("@FCRIMECODE", SqlDbType.VarChar,20) ,
						new SqlParameter("@CARDCODE", SqlDbType.VarChar,20) ,
						new SqlParameter("@FAMOUNT", SqlDbType.Decimal,9) ,
						new SqlParameter("@FLAG", SqlDbType.Int,4) ,
						new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,
						new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,
						new SqlParameter("@fcriminal", SqlDbType.VarChar,50)

			};

			parameters[0].Value = model.seqno;
			parameters[1].Value = model.vouno;
			parameters[2].Value = model.Frealareacode;
			parameters[3].Value = model.FrealAreaName;
			parameters[4].Value = model.remark;
			parameters[5].Value = model.ptype;
			parameters[6].Value = model.udate;
			parameters[7].Value = model.crtby;
			parameters[8].Value = model.crtdt;
			parameters[9].Value = model.applyby;
			parameters[10].Value = model.acctype;
			parameters[11].Value = model.BID;
			parameters[12].Value = model.cardtype;
			parameters[13].Value = model.AmountC;
			parameters[14].Value = model.cqbt;
			parameters[15].Value = model.gwjt;
			parameters[16].Value = model.ldjx;
			parameters[17].Value = model.tbbz;
			parameters[18].Value = model.grkj;
			parameters[19].Value = model.AmountA;
			parameters[20].Value = model.AmountB;
			parameters[21].Value = model.FCRIMECODE;
			parameters[22].Value = model.CARDCODE;
			parameters[23].Value = model.FAMOUNT;
			parameters[24].Value = model.FLAG;
			parameters[25].Value = model.fareacode;
			parameters[26].Value = model.fareaName;
			parameters[27].Value = model.fcriminal;
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
			strSql.Append("delete from T_BONUSDTL ");
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
			strSql.Append("delete from T_BONUSDTL ");
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
		public SelfhelpOrderMgr.Model.T_BONUSDTL GetModel(int seqno)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select seqno, vouno, Frealareacode, FrealAreaName, remark, ptype, udate, crtby, crtdt, applyby, acctype, BID, cardtype, AmountC, cqbt, gwjt, ldjx, tbbz, grkj, AmountA, AmountB, FCRIMECODE, CARDCODE, FAMOUNT, FLAG, fareacode, fareaName, fcriminal  ");
			strSql.Append("  from T_BONUSDTL ");
			strSql.Append(" where seqno=@seqno");
			SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;


			SelfhelpOrderMgr.Model.T_BONUSDTL model = new SelfhelpOrderMgr.Model.T_BONUSDTL();
			DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
				{
					model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
				model.vouno = ds.Tables[0].Rows[0]["vouno"].ToString();
				model.Frealareacode = ds.Tables[0].Rows[0]["Frealareacode"].ToString();
				model.FrealAreaName = ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
				model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
				model.ptype = ds.Tables[0].Rows[0]["ptype"].ToString();
				if (ds.Tables[0].Rows[0]["udate"].ToString() != "")
				{
					model.udate = DateTime.Parse(ds.Tables[0].Rows[0]["udate"].ToString());
				}
				model.crtby = ds.Tables[0].Rows[0]["crtby"].ToString();
				if (ds.Tables[0].Rows[0]["crtdt"].ToString() != "")
				{
					model.crtdt = DateTime.Parse(ds.Tables[0].Rows[0]["crtdt"].ToString());
				}
				model.applyby = ds.Tables[0].Rows[0]["applyby"].ToString();
				if (ds.Tables[0].Rows[0]["acctype"].ToString() != "")
				{
					model.acctype = int.Parse(ds.Tables[0].Rows[0]["acctype"].ToString());
				}
				model.BID = ds.Tables[0].Rows[0]["BID"].ToString();
				if (ds.Tables[0].Rows[0]["cardtype"].ToString() != "")
				{
					model.cardtype = int.Parse(ds.Tables[0].Rows[0]["cardtype"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AmountC"].ToString() != "")
				{
					model.AmountC = decimal.Parse(ds.Tables[0].Rows[0]["AmountC"].ToString());
				}
				if (ds.Tables[0].Rows[0]["cqbt"].ToString() != "")
				{
					model.cqbt = decimal.Parse(ds.Tables[0].Rows[0]["cqbt"].ToString());
				}
				if (ds.Tables[0].Rows[0]["gwjt"].ToString() != "")
				{
					model.gwjt = decimal.Parse(ds.Tables[0].Rows[0]["gwjt"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ldjx"].ToString() != "")
				{
					model.ldjx = decimal.Parse(ds.Tables[0].Rows[0]["ldjx"].ToString());
				}
				if (ds.Tables[0].Rows[0]["tbbz"].ToString() != "")
				{
					model.tbbz = decimal.Parse(ds.Tables[0].Rows[0]["tbbz"].ToString());
				}
				if (ds.Tables[0].Rows[0]["grkj"].ToString() != "")
				{
					model.grkj = decimal.Parse(ds.Tables[0].Rows[0]["grkj"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AmountA"].ToString() != "")
				{
					model.AmountA = decimal.Parse(ds.Tables[0].Rows[0]["AmountA"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AmountB"].ToString() != "")
				{
					model.AmountB = decimal.Parse(ds.Tables[0].Rows[0]["AmountB"].ToString());
				}
				model.FCRIMECODE = ds.Tables[0].Rows[0]["FCRIMECODE"].ToString();
				model.CARDCODE = ds.Tables[0].Rows[0]["CARDCODE"].ToString();
				if (ds.Tables[0].Rows[0]["FAMOUNT"].ToString() != "")
				{
					model.FAMOUNT = decimal.Parse(ds.Tables[0].Rows[0]["FAMOUNT"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FLAG"].ToString() != "")
				{
					model.FLAG = int.Parse(ds.Tables[0].Rows[0]["FLAG"].ToString());
				}
				model.fareacode = ds.Tables[0].Rows[0]["fareacode"].ToString();
				model.fareaName = ds.Tables[0].Rows[0]["fareaName"].ToString();
				model.fcriminal = ds.Tables[0].Rows[0]["fcriminal"].ToString();

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
			strSql.Append(" FROM T_BONUSDTL ");
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
			strSql.Append(" FROM T_BONUSDTL ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}


	}
}

