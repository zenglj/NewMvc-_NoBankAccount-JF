using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace SelfhelpOrderMgr.DAL
{
	public partial class T_Criminal_cardDAL
	{

		public bool Exists(string fcrimecode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_Criminal_card");
			stringBuilder.Append(" where ");
			stringBuilder.Append(" fcrimecode = @fcrimecode  ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@fcrimecode", SqlDbType.VarChar, 20)
			};
			array[0].Value = fcrimecode;
			return SqlHelper.Exists(stringBuilder.ToString(), array);
		}
		public void Add(T_Criminal_card model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into T_Criminal_card(");
			stringBuilder.Append("UnPaidAmtB,flimitflag,flimitamt,BankAccNo,RegFlag,UseFlag,BankAmount,AmountC,bankdate,BankRegFlag,fcrimecode,tmpbankAmount,curbankamount,unregflag,SecondaryBankCard,SecondaryCardFlag,cardcodea,AmountA,UnPaidAmtA,cardflaga,cardcodeb,AmountB,cardflagb,AccPoints");
			stringBuilder.Append(") values (");
			stringBuilder.Append("@UnPaidAmtB,@flimitflag,@flimitamt,@BankAccNo,@RegFlag,@UseFlag,@BankAmount,@AmountC,@bankdate,@BankRegFlag,@fcrimecode,@tmpbankAmount,@curbankamount,@unregflag,@SecondaryBankCard,@SecondaryCardFlag,@cardcodea,@AmountA,@UnPaidAmtA,@cardflaga,@cardcodeb,@AmountB,@cardflagb,@AccPoints");
			stringBuilder.Append(") ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@UnPaidAmtB", SqlDbType.Decimal, 9),
				new SqlParameter("@flimitflag", SqlDbType.Int, 4),
				new SqlParameter("@flimitamt", SqlDbType.Decimal, 9),
				new SqlParameter("@BankAccNo", SqlDbType.VarChar, 20),
				new SqlParameter("@RegFlag", SqlDbType.Int, 4),
				new SqlParameter("@UseFlag", SqlDbType.Int, 4),
				new SqlParameter("@BankAmount", SqlDbType.Money, 8),
				new SqlParameter("@AmountC", SqlDbType.Decimal, 9),
				new SqlParameter("@bankdate", SqlDbType.DateTime),
				new SqlParameter("@BankRegFlag", SqlDbType.Int, 4),
				new SqlParameter("@fcrimecode", SqlDbType.VarChar, 20),
				new SqlParameter("@tmpbankAmount", SqlDbType.Decimal, 9),
				new SqlParameter("@curbankamount", SqlDbType.Decimal, 9),
				new SqlParameter("@unregflag", SqlDbType.Int, 4),
				new SqlParameter("@SecondaryBankCard", SqlDbType.NVarChar, 20),
				new SqlParameter("@SecondaryCardFlag", SqlDbType.Int, 4),
				new SqlParameter("@cardcodea", SqlDbType.VarChar, 20),
				new SqlParameter("@AmountA", SqlDbType.Decimal, 9),
				new SqlParameter("@UnPaidAmtA", SqlDbType.Decimal, 9),
				new SqlParameter("@cardflaga", SqlDbType.Int, 4),
				new SqlParameter("@cardcodeb", SqlDbType.VarChar, 20),
				new SqlParameter("@AmountB", SqlDbType.Decimal, 9),
				new SqlParameter("@cardflagb", SqlDbType.Int, 4),
				new SqlParameter("@AccPoints", SqlDbType.Decimal, 9)
			};
			array[0].Value = model.UnPaidAmtB;
			array[1].Value = model.flimitflag;
			array[2].Value = model.flimitamt;
			array[3].Value = model.BankAccNo;
			array[4].Value = model.RegFlag;
			array[5].Value = model.UseFlag;
			array[6].Value = model.BankAmount;
			array[7].Value = model.AmountC;
			array[8].Value = model.bankdate;
			array[9].Value = model.BankRegFlag;
			array[10].Value = model.fcrimecode;
			array[11].Value = model.tmpbankAmount;
			array[12].Value = model.curbankamount;
			array[13].Value = model.unregflag;
			array[14].Value = model.SecondaryBankCard;
			array[15].Value = model.SecondaryCardFlag;
			array[16].Value = model.cardcodea;
			array[17].Value = model.AmountA;
			array[18].Value = model.UnPaidAmtA;
			array[19].Value = model.cardflaga;
			array[20].Value = model.cardcodeb;
			array[21].Value = model.AmountB;
			array[22].Value = model.cardflagb;
			array[23].Value = model.AccPoints;
			SqlHelper.ExecuteSql(stringBuilder.ToString(), array);
		}
		public bool Update(T_Criminal_card model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_Criminal_card set ");
			stringBuilder.Append(" UnPaidAmtB = @UnPaidAmtB , ");
			stringBuilder.Append(" flimitflag = @flimitflag , ");
			stringBuilder.Append(" flimitamt = @flimitamt , ");
			stringBuilder.Append(" BankAccNo = @BankAccNo , ");
			stringBuilder.Append(" RegFlag = @RegFlag , ");
			stringBuilder.Append(" UseFlag = @UseFlag , ");
			stringBuilder.Append(" BankAmount = @BankAmount , ");
			stringBuilder.Append(" AmountC = @AmountC , ");
			stringBuilder.Append(" bankdate = @bankdate , ");
			stringBuilder.Append(" BankRegFlag = @BankRegFlag , ");
			stringBuilder.Append(" fcrimecode = @fcrimecode , ");
			stringBuilder.Append(" tmpbankAmount = @tmpbankAmount , ");
			stringBuilder.Append(" curbankamount = @curbankamount , ");
			stringBuilder.Append(" unregflag = @unregflag , ");
			stringBuilder.Append(" SecondaryBankCard = @SecondaryBankCard , ");
			stringBuilder.Append(" SecondaryCardFlag = @SecondaryCardFlag , ");
			stringBuilder.Append(" cardcodea = @cardcodea , ");
			stringBuilder.Append(" AmountA = @AmountA , ");
			stringBuilder.Append(" UnPaidAmtA = @UnPaidAmtA , ");
			stringBuilder.Append(" cardflaga = @cardflaga , ");
			stringBuilder.Append(" cardcodeb = @cardcodeb , ");
			stringBuilder.Append(" AmountB = @AmountB , ");
			stringBuilder.Append(" cardflagb = @cardflagb , ");
			stringBuilder.Append(" AccPoints = @AccPoints  ");
			stringBuilder.Append(" where fcrimecode=@fcrimecode  ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@Id", SqlDbType.Int, 4),
				new SqlParameter("@UnPaidAmtB", SqlDbType.Decimal, 9),
				new SqlParameter("@flimitflag", SqlDbType.Int, 4),
				new SqlParameter("@flimitamt", SqlDbType.Decimal, 9),
				new SqlParameter("@BankAccNo", SqlDbType.VarChar, 20),
				new SqlParameter("@RegFlag", SqlDbType.Int, 4),
				new SqlParameter("@UseFlag", SqlDbType.Int, 4),
				new SqlParameter("@BankAmount", SqlDbType.Money, 8),
				new SqlParameter("@AmountC", SqlDbType.Decimal, 9),
				new SqlParameter("@bankdate", SqlDbType.DateTime),
				new SqlParameter("@BankRegFlag", SqlDbType.Int, 4),
				new SqlParameter("@fcrimecode", SqlDbType.VarChar, 20),
				new SqlParameter("@tmpbankAmount", SqlDbType.Decimal, 9),
				new SqlParameter("@curbankamount", SqlDbType.Decimal, 9),
				new SqlParameter("@unregflag", SqlDbType.Int, 4),
				new SqlParameter("@SecondaryBankCard", SqlDbType.NVarChar, 20),
				new SqlParameter("@SecondaryCardFlag", SqlDbType.Int, 4),
				new SqlParameter("@cardcodea", SqlDbType.VarChar, 20),
				new SqlParameter("@AmountA", SqlDbType.Decimal, 9),
				new SqlParameter("@UnPaidAmtA", SqlDbType.Decimal, 9),
				new SqlParameter("@cardflaga", SqlDbType.Int, 4),
				new SqlParameter("@cardcodeb", SqlDbType.VarChar, 20),
				new SqlParameter("@AmountB", SqlDbType.Decimal, 9),
				new SqlParameter("@AccPoints", SqlDbType.Decimal, 9)
			};
			array[0].Value = model.Id;
			array[1].Value = model.UnPaidAmtB;
			array[2].Value = model.flimitflag;
			array[3].Value = model.flimitamt;
			array[4].Value = model.BankAccNo;
			array[5].Value = model.RegFlag;
			array[6].Value = model.UseFlag;
			array[7].Value = model.BankAmount;
			array[8].Value = model.AmountC;
			array[9].Value = model.bankdate;
			array[10].Value = model.BankRegFlag;
			array[11].Value = model.fcrimecode;
			array[12].Value = model.tmpbankAmount;
			array[13].Value = model.curbankamount;
			array[14].Value = model.unregflag;
			array[15].Value = model.SecondaryBankCard;
			array[16].Value = model.SecondaryCardFlag;
			array[17].Value = model.cardcodea;
			array[18].Value = model.AmountA;
			array[19].Value = model.UnPaidAmtA;
			array[20].Value = model.cardflaga;
			array[21].Value = model.cardcodeb;
			array[22].Value = model.AmountB;
			array[23].Value = model.cardflagb;
			array[24].Value = model.AccPoints;
			return SqlHelper.ExecuteSql(stringBuilder.ToString(), array) > 0;
		}
		public bool Delete(string fcrimecode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_Criminal_card ");
			stringBuilder.Append(" where fcrimecode=@fcrimecode ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@fcrimecode", SqlDbType.VarChar, 20)
			};
			array[0].Value = fcrimecode;
			return SqlHelper.ExecuteSql(stringBuilder.ToString(), array) > 0;
		}
		public T_Criminal_card GetModel(string fcrimecode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select Id, UnPaidAmtB, flimitflag, flimitamt, BankAccNo, RegFlag, UseFlag, BankAmount, AmountC, bankdate, BankRegFlag, fcrimecode, tmpbankAmount, curbankamount, unregflag, SecondaryBankCard, SecondaryCardFlag, cardcodea, AmountA, UnPaidAmtA, cardflaga, cardcodeb, AmountB, cardflagb,AccPoints  ");
			stringBuilder.Append("  from T_Criminal_card ");
			stringBuilder.Append(" where fcrimecode=@fcrimecode ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@fcrimecode", SqlDbType.VarChar, 20)
			};
			array[0].Value = fcrimecode;
			T_Criminal_card t_Criminal_card;
			DataSet dataSet = SqlHelper.Query(stringBuilder.ToString(), array);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				return t_Criminal_card = SetDataRowToEntity(dataSet.Tables[0].Rows[0]);
			}
			return null;
		}

		private T_Criminal_card SetDataRowToEntity(DataRow row)
		{
			T_Criminal_card t_Criminal_card = new T_Criminal_card();
			if (row["Id"].ToString() != "")
			{
				t_Criminal_card.Id = int.Parse(row["Id"].ToString());
			}
			if (row["UnPaidAmtB"].ToString() != "")
			{
				t_Criminal_card.UnPaidAmtB = decimal.Parse(row["UnPaidAmtB"].ToString());
			}
			if (row["flimitflag"].ToString() != "")
			{
				t_Criminal_card.flimitflag = int.Parse(row["flimitflag"].ToString());
			}
			if (row["flimitamt"].ToString() != "")
			{
				t_Criminal_card.flimitamt = decimal.Parse(row["flimitamt"].ToString());
			}
			t_Criminal_card.BankAccNo = row["BankAccNo"].ToString();
			if (row["RegFlag"].ToString() != "")
			{
				t_Criminal_card.RegFlag = int.Parse(row["RegFlag"].ToString());
			}
			if (row["UseFlag"].ToString() != "")
			{
				t_Criminal_card.UseFlag = int.Parse(row["UseFlag"].ToString());
			}
			if (row["BankAmount"].ToString() != "")
			{
				t_Criminal_card.BankAmount = decimal.Parse(row["BankAmount"].ToString());
			}
			if (row["AmountC"].ToString() != "")
			{
				t_Criminal_card.AmountC = decimal.Parse(row["AmountC"].ToString());
			}
			if (row["bankdate"].ToString() != "")
			{
				t_Criminal_card.bankdate = DateTime.Parse(row["bankdate"].ToString());
			}
			if (row["BankRegFlag"].ToString() != "")
			{
				t_Criminal_card.BankRegFlag = int.Parse(row["BankRegFlag"].ToString());
			}
			t_Criminal_card.fcrimecode = row["fcrimecode"].ToString();
			if (row["tmpbankAmount"].ToString() != "")
			{
				t_Criminal_card.tmpbankAmount = decimal.Parse(row["tmpbankAmount"].ToString());
			}
			if (row["curbankamount"].ToString() != "")
			{
				t_Criminal_card.curbankamount = decimal.Parse(row["curbankamount"].ToString());
			}
			if (row["unregflag"].ToString() != "")
			{
				t_Criminal_card.unregflag = int.Parse(row["unregflag"].ToString());
			}
			t_Criminal_card.SecondaryBankCard = row["SecondaryBankCard"].ToString();
			if (row["SecondaryCardFlag"].ToString() != "")
			{
				t_Criminal_card.SecondaryCardFlag = int.Parse(row["SecondaryCardFlag"].ToString());
			}
			t_Criminal_card.cardcodea = row["cardcodea"].ToString();
			if (row["AmountA"].ToString() != "")
			{
				t_Criminal_card.AmountA = decimal.Parse(row["AmountA"].ToString());
			}
			if (row["UnPaidAmtA"].ToString() != "")
			{
				t_Criminal_card.UnPaidAmtA = decimal.Parse(row["UnPaidAmtA"].ToString());
			}
			if (row["cardflaga"].ToString() != "")
			{
				t_Criminal_card.cardflaga = int.Parse(row["cardflaga"].ToString());
			}
			t_Criminal_card.cardcodeb = row["cardcodeb"].ToString();
			if (row["AmountB"].ToString() != "")
			{
				t_Criminal_card.AmountB = decimal.Parse(row["AmountB"].ToString());
			}
			if (row["cardflagb"].ToString() != "")
			{
				t_Criminal_card.cardflagb = int.Parse(row["cardflagb"].ToString());
			}
			if (row["AccPoints"].ToString() != "")
			{
				t_Criminal_card.AccPoints = decimal.Parse(row["AccPoints"].ToString());
			}
			return t_Criminal_card;
		}

		public List<T_Criminal_card> GetModelList(string strWhere)
		{
			DataSet ds = this.GetList(strWhere);
			List<T_Criminal_card> list = new List<T_Criminal_card>();
			foreach (DataRow row in ds.Tables[0].Rows)
			{
				list.Add(this.SetDataRowToEntity(row));
			}
			return list;
		}
		public DataSet GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * ");
			stringBuilder.Append(" FROM T_Criminal_card ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			return SqlHelper.Query(stringBuilder.ToString(), new SqlParameter[0]);
		}
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ");
			if (Top > 0)
			{
				stringBuilder.Append(" top " + Top.ToString());
			}
			stringBuilder.Append(" * ");
			stringBuilder.Append(" FROM T_Criminal_card ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return SqlHelper.Query(stringBuilder.ToString(), new SqlParameter[0]);
		}
	}
}
