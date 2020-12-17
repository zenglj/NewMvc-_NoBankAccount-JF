using SelfhelpOrderMgr.Model;
using System;
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
			stringBuilder.Append("UnPaidAmtB,flimitflag,flimitamt,BankAccNo,RegFlag,UseFlag,BankAmount,AmountC,bankdate,BankRegFlag,fcrimecode,tmpbankAmount,curbankamount,unregflag,SecondaryBankCard,SecondaryCardFlag,cardcodea,AmountA,UnPaidAmtA,cardflaga,cardcodeb,AmountB,cardflagb");
			stringBuilder.Append(") values (");
			stringBuilder.Append("@UnPaidAmtB,@flimitflag,@flimitamt,@BankAccNo,@RegFlag,@UseFlag,@BankAmount,@AmountC,@bankdate,@BankRegFlag,@fcrimecode,@tmpbankAmount,@curbankamount,@unregflag,@SecondaryBankCard,@SecondaryCardFlag,@cardcodea,@AmountA,@UnPaidAmtA,@cardflaga,@cardcodeb,@AmountB,@cardflagb");
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
				new SqlParameter("@cardflagb", SqlDbType.Int, 4)
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
			stringBuilder.Append(" cardflagb = @cardflagb  ");
			stringBuilder.Append(" where fcrimecode=@fcrimecode  ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@seqno", SqlDbType.Int, 4),
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
				new SqlParameter("@cardflagb", SqlDbType.Int, 4)
			};
			array[0].Value = model.seqno;
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
			stringBuilder.Append("select seqno, UnPaidAmtB, flimitflag, flimitamt, BankAccNo, RegFlag, UseFlag, BankAmount, AmountC, bankdate, BankRegFlag, fcrimecode, tmpbankAmount, curbankamount, unregflag, SecondaryBankCard, SecondaryCardFlag, cardcodea, AmountA, UnPaidAmtA, cardflaga, cardcodeb, AmountB, cardflagb  ");
			stringBuilder.Append("  from T_Criminal_card ");
			stringBuilder.Append(" where fcrimecode=@fcrimecode ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@fcrimecode", SqlDbType.VarChar, 20)
			};
			array[0].Value = fcrimecode;
			T_Criminal_card t_Criminal_card = new T_Criminal_card();
			DataSet dataSet = SqlHelper.Query(stringBuilder.ToString(), array);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["seqno"].ToString() != "")
				{
					t_Criminal_card.seqno = int.Parse(dataSet.Tables[0].Rows[0]["seqno"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["UnPaidAmtB"].ToString() != "")
				{
					t_Criminal_card.UnPaidAmtB = decimal.Parse(dataSet.Tables[0].Rows[0]["UnPaidAmtB"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["flimitflag"].ToString() != "")
				{
					t_Criminal_card.flimitflag = int.Parse(dataSet.Tables[0].Rows[0]["flimitflag"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["flimitamt"].ToString() != "")
				{
					t_Criminal_card.flimitamt = decimal.Parse(dataSet.Tables[0].Rows[0]["flimitamt"].ToString());
				}
				t_Criminal_card.BankAccNo = dataSet.Tables[0].Rows[0]["BankAccNo"].ToString();
				if (dataSet.Tables[0].Rows[0]["RegFlag"].ToString() != "")
				{
					t_Criminal_card.RegFlag = int.Parse(dataSet.Tables[0].Rows[0]["RegFlag"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["UseFlag"].ToString() != "")
				{
					t_Criminal_card.UseFlag = int.Parse(dataSet.Tables[0].Rows[0]["UseFlag"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["BankAmount"].ToString() != "")
				{
					t_Criminal_card.BankAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["BankAmount"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["AmountC"].ToString() != "")
				{
					t_Criminal_card.AmountC = decimal.Parse(dataSet.Tables[0].Rows[0]["AmountC"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["bankdate"].ToString() != "")
				{
					t_Criminal_card.bankdate = DateTime.Parse(dataSet.Tables[0].Rows[0]["bankdate"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["BankRegFlag"].ToString() != "")
				{
					t_Criminal_card.BankRegFlag = int.Parse(dataSet.Tables[0].Rows[0]["BankRegFlag"].ToString());
				}
				t_Criminal_card.fcrimecode = dataSet.Tables[0].Rows[0]["fcrimecode"].ToString();
				if (dataSet.Tables[0].Rows[0]["tmpbankAmount"].ToString() != "")
				{
					t_Criminal_card.tmpbankAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["tmpbankAmount"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["curbankamount"].ToString() != "")
				{
					t_Criminal_card.curbankamount = decimal.Parse(dataSet.Tables[0].Rows[0]["curbankamount"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["unregflag"].ToString() != "")
				{
					t_Criminal_card.unregflag = int.Parse(dataSet.Tables[0].Rows[0]["unregflag"].ToString());
				}
				t_Criminal_card.SecondaryBankCard = dataSet.Tables[0].Rows[0]["SecondaryBankCard"].ToString();
				if (dataSet.Tables[0].Rows[0]["SecondaryCardFlag"].ToString() != "")
				{
					t_Criminal_card.SecondaryCardFlag = int.Parse(dataSet.Tables[0].Rows[0]["SecondaryCardFlag"].ToString());
				}
				t_Criminal_card.cardcodea = dataSet.Tables[0].Rows[0]["cardcodea"].ToString();
				if (dataSet.Tables[0].Rows[0]["AmountA"].ToString() != "")
				{
					t_Criminal_card.AmountA = decimal.Parse(dataSet.Tables[0].Rows[0]["AmountA"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["UnPaidAmtA"].ToString() != "")
				{
					t_Criminal_card.UnPaidAmtA = decimal.Parse(dataSet.Tables[0].Rows[0]["UnPaidAmtA"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["cardflaga"].ToString() != "")
				{
					t_Criminal_card.cardflaga = int.Parse(dataSet.Tables[0].Rows[0]["cardflaga"].ToString());
				}
				t_Criminal_card.cardcodeb = dataSet.Tables[0].Rows[0]["cardcodeb"].ToString();
				if (dataSet.Tables[0].Rows[0]["AmountB"].ToString() != "")
				{
					t_Criminal_card.AmountB = decimal.Parse(dataSet.Tables[0].Rows[0]["AmountB"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["cardflagb"].ToString() != "")
				{
					t_Criminal_card.cardflagb = int.Parse(dataSet.Tables[0].Rows[0]["cardflagb"].ToString());
				}
				return t_Criminal_card;
			}
			return null;
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
