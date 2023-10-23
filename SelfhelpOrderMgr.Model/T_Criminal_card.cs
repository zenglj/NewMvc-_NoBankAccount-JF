using System;
namespace SelfhelpOrderMgr.Model
{
	public class T_Criminal_card :BaseModel
	{
		private int _seqno;
		private string _fcrimecode;
		private string _cardcodea;
		private decimal _amounta;
		private decimal _unpaidamta;
		private int _cardflaga;
		private string _cardcodeb;
		private decimal _amountb;
		private int _cardflagb;
		private decimal _unpaidamtb;
		private int _flimitflag;
		private decimal _flimitamt;
		private string _bankaccno;
		private int _regflag;
		private int _useflag;
		private decimal _bankamount;
		private decimal _amountc;
		private DateTime _bankdate;
		private int _bankregflag;
		private decimal _tmpbankamount;
		private decimal _curbankamount;
		private int _unregflag;
		private string _secondarybankcard;
		private int _secondarycardflag;
		private decimal _accpoints;
		public int seqno
		{
			get
			{
				return this._seqno;
			}
			set
			{
				this._seqno = value;
			}
		}
		public string fcrimecode
		{
			get
			{
				return this._fcrimecode;
			}
			set
			{
				this._fcrimecode = value;
			}
		}
		public string cardcodea
		{
			get
			{
				return this._cardcodea;
			}
			set
			{
				this._cardcodea = value;
			}
		}
		public decimal AmountA
		{
			get
			{
				return this._amounta;
			}
			set
			{
				this._amounta = value;
			}
		}
		public decimal UnPaidAmtA
		{
			get
			{
				return this._unpaidamta;
			}
			set
			{
				this._unpaidamta = value;
			}
		}
		public int cardflaga
		{
			get
			{
				return this._cardflaga;
			}
			set
			{
				this._cardflaga = value;
			}
		}
		public string cardcodeb
		{
			get
			{
				return this._cardcodeb;
			}
			set
			{
				this._cardcodeb = value;
			}
		}
		public decimal AmountB
		{
			get
			{
				return this._amountb;
			}
			set
			{
				this._amountb = value;
			}
		}
		public int cardflagb
		{
			get
			{
				return this._cardflagb;
			}
			set
			{
				this._cardflagb = value;
			}
		}
		public decimal UnPaidAmtB
		{
			get
			{
				return this._unpaidamtb;
			}
			set
			{
				this._unpaidamtb = value;
			}
		}
		public int flimitflag
		{
			get
			{
				return this._flimitflag;
			}
			set
			{
				this._flimitflag = value;
			}
		}
		public decimal flimitamt
		{
			get
			{
				return this._flimitamt;
			}
			set
			{
				this._flimitamt = value;
			}
		}
		public string BankAccNo
		{
			get
			{
				return this._bankaccno;
			}
			set
			{
				this._bankaccno = value;
			}
		}
		public int RegFlag
		{
			get
			{
				return this._regflag;
			}
			set
			{
				this._regflag = value;
			}
		}
		public int UseFlag
		{
			get
			{
				return this._useflag;
			}
			set
			{
				this._useflag = value;
			}
		}
		public decimal BankAmount
		{
			get
			{
				return this._bankamount;
			}
			set
			{
				this._bankamount = value;
			}
		}
		public decimal AmountC
		{
			get
			{
				return this._amountc;
			}
			set
			{
				this._amountc = value;
			}
		}
		public DateTime bankdate
		{
			get
			{
				return this._bankdate;
			}
			set
			{
				this._bankdate = value;
			}
		}
		public int BankRegFlag
		{
			get
			{
				return this._bankregflag;
			}
			set
			{
				this._bankregflag = value;
			}
		}
		public decimal tmpbankAmount
		{
			get
			{
				return this._tmpbankamount;
			}
			set
			{
				this._tmpbankamount = value;
			}
		}
		public decimal curbankamount
		{
			get
			{
				return this._curbankamount;
			}
			set
			{
				this._curbankamount = value;
			}
		}
		public int unregflag
		{
			get
			{
				return this._unregflag;
			}
			set
			{
				this._unregflag = value;
			}
		}
		public string SecondaryBankCard
		{
			get
			{
				return this._secondarybankcard;
			}
			set
			{
				this._secondarybankcard = value;
			}
		}
		public int SecondaryCardFlag
		{
			get
			{
				return this._secondarycardflag;
			}
			set
			{
				this._secondarycardflag = value;
			}
		}

		public decimal AccPoints
		{
			get
			{
				return this._accpoints;
			}
			set
			{
				this._accpoints = value;
			}
		}
	}
}