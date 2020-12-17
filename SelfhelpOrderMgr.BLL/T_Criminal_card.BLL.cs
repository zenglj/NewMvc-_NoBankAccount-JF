using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.BLL
{
	public partial class T_Criminal_cardBLL
	{
		private readonly T_Criminal_cardDAL dal = new T_Criminal_cardDAL();

		public bool Exists(string fcrimecode)
		{
			return this.dal.Exists(fcrimecode);
		}
		public void Add(T_Criminal_card model)
		{
			this.dal.Add(model);
		}
		public bool Update(T_Criminal_card model)
		{
			return this.dal.Update(model);
		}
		public bool Delete(string fcrimecode)
		{
			return this.dal.Delete(fcrimecode);
		}
		public T_Criminal_card GetModel(string fcrimecode)
		{
			return this.dal.GetModel(fcrimecode);
		}
		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}
		public List<T_Criminal_card> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}
		public List<T_Criminal_card> DataTableToList(DataTable dt)
		{
			List<T_Criminal_card> modelList = new List<T_Criminal_card>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					T_Criminal_card model = new T_Criminal_card();
					if (dt.Rows[i]["seqno"].ToString() != "")
					{
						model.seqno = int.Parse(dt.Rows[i]["seqno"].ToString());
					}
					if (dt.Rows[i]["UnPaidAmtB"].ToString() != "")
					{
						model.UnPaidAmtB = decimal.Parse(dt.Rows[i]["UnPaidAmtB"].ToString());
					}
					if (dt.Rows[i]["flimitflag"].ToString() != "")
					{
						model.flimitflag = int.Parse(dt.Rows[i]["flimitflag"].ToString());
					}
					if (dt.Rows[i]["flimitamt"].ToString() != "")
					{
						model.flimitamt = decimal.Parse(dt.Rows[i]["flimitamt"].ToString());
					}
					model.BankAccNo = dt.Rows[i]["BankAccNo"].ToString();
					if (dt.Rows[i]["RegFlag"].ToString() != "")
					{
						model.RegFlag = int.Parse(dt.Rows[i]["RegFlag"].ToString());
					}
					if (dt.Rows[i]["UseFlag"].ToString() != "")
					{
						model.UseFlag = int.Parse(dt.Rows[i]["UseFlag"].ToString());
					}
					if (dt.Rows[i]["BankAmount"].ToString() != "")
					{
						model.BankAmount = decimal.Parse(dt.Rows[i]["BankAmount"].ToString());
					}
					if (dt.Rows[i]["AmountC"].ToString() != "")
					{
						model.AmountC = decimal.Parse(dt.Rows[i]["AmountC"].ToString());
					}
					if (dt.Rows[i]["bankdate"].ToString() != "")
					{
						model.bankdate = DateTime.Parse(dt.Rows[i]["bankdate"].ToString());
					}
					if (dt.Rows[i]["BankRegFlag"].ToString() != "")
					{
						model.BankRegFlag = int.Parse(dt.Rows[i]["BankRegFlag"].ToString());
					}
					model.fcrimecode = dt.Rows[i]["fcrimecode"].ToString();
					if (dt.Rows[i]["tmpbankAmount"].ToString() != "")
					{
						model.tmpbankAmount = decimal.Parse(dt.Rows[i]["tmpbankAmount"].ToString());
					}
					if (dt.Rows[i]["curbankamount"].ToString() != "")
					{
						model.curbankamount = decimal.Parse(dt.Rows[i]["curbankamount"].ToString());
					}
					if (dt.Rows[i]["unregflag"].ToString() != "")
					{
						model.unregflag = int.Parse(dt.Rows[i]["unregflag"].ToString());
					}
					model.SecondaryBankCard = dt.Rows[i]["SecondaryBankCard"].ToString();
					if (dt.Rows[i]["SecondaryCardFlag"].ToString() != "")
					{
						model.SecondaryCardFlag = int.Parse(dt.Rows[i]["SecondaryCardFlag"].ToString());
					}
					model.cardcodea = dt.Rows[i]["cardcodea"].ToString();
					if (dt.Rows[i]["AmountA"].ToString() != "")
					{
						model.AmountA = decimal.Parse(dt.Rows[i]["AmountA"].ToString());
					}
					if (dt.Rows[i]["UnPaidAmtA"].ToString() != "")
					{
						model.UnPaidAmtA = decimal.Parse(dt.Rows[i]["UnPaidAmtA"].ToString());
					}
					if (dt.Rows[i]["cardflaga"].ToString() != "")
					{
						model.cardflaga = int.Parse(dt.Rows[i]["cardflaga"].ToString());
					}
					model.cardcodeb = dt.Rows[i]["cardcodeb"].ToString();
					if (dt.Rows[i]["AmountB"].ToString() != "")
					{
						model.AmountB = decimal.Parse(dt.Rows[i]["AmountB"].ToString());
					}
					if (dt.Rows[i]["cardflagb"].ToString() != "")
					{
						model.cardflagb = int.Parse(dt.Rows[i]["cardflagb"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
		public DataSet GetAllList()
		{
			return this.GetList("");
		}
	}
}
