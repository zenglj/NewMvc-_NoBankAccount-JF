using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfhelpOrderMgr.Model
{
    public class CriminalCardEntity
    {
        private int _seqno;

        public int Seqno
        {
            get { return _seqno; }
            set { _seqno = value; }
        }
        private string _fcrimecode;

        public string Fcrimecode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
        }
        private string _cardcodea;

        public string Cardcodea
        {
            get { return _cardcodea; }
            set { _cardcodea = value; }
        }
        private decimal _AmountA;

        public decimal AmountA
        {
            get { return _AmountA; }
            set { _AmountA = value; }
        }
        private decimal _UnPaidAmtA;

        public decimal UnPaidAmtA
        {
            get { return _UnPaidAmtA; }
            set { _UnPaidAmtA = value; }
        }
        private int _cardflaga;

        public int Cardflaga
        {
            get { return _cardflaga; }
            set { _cardflaga = value; }
        }
        private string _cardcodeb;

        public string Cardcodeb
        {
            get { return _cardcodeb; }
            set { _cardcodeb = value; }
        }
        private decimal _AmountB;

        public decimal AmountB
        {
            get { return _AmountB; }
            set { _AmountB = value; }
        }
        private int _cardflagb;

        public int Cardflagb
        {
            get { return _cardflagb; }
            set { _cardflagb = value; }
        }
        private decimal _UnPaidAmtB;

        public decimal UnPaidAmtB
        {
            get { return _UnPaidAmtB; }
            set { _UnPaidAmtB = value; }
        }
        private int _flimitflag;

        public int Flimitflag
        {
            get { return _flimitflag; }
            set { _flimitflag = value; }
        }
        private decimal _flimitamt;

        public decimal Flimitamt
        {
            get { return _flimitamt; }
            set { _flimitamt = value; }
        }
        private string _BankAccNo;

        public string BankAccNo
        {
            get { return _BankAccNo; }
            set { _BankAccNo = value; }
        }
        private int _RegFlag;

        public int RegFlag
        {
            get { return _RegFlag; }
            set { _RegFlag = value; }
        }
        private int _UseFlag;

        public int UseFlag
        {
            get { return _UseFlag; }
            set { _UseFlag = value; }
        }
        private decimal _BankAmount;

        public decimal BankAmount
        {
            get { return _BankAmount; }
            set { _BankAmount = value; }
        }
        private decimal _AmountC;

        public decimal AmountC
        {
            get { return _AmountC; }
            set { _AmountC = value; }
        }
        private DateTime _bankdate;

        public DateTime Bankdate
        {
            get { return _bankdate; }
            set { _bankdate = value; }
        }
        private int _BankRegFlag;

        public int BankRegFlag
        {
            get { return _BankRegFlag; }
            set { _BankRegFlag = value; }
        }
        private decimal _tmpbankAmount;

        public decimal TmpbankAmount
        {
            get { return _tmpbankAmount; }
            set { _tmpbankAmount = value; }
        }
        private decimal _curbankamount;

        public decimal Curbankamount
        {
            get { return _curbankamount; }
            set { _curbankamount = value; }
        }
        private int _unregflag;

        public int Unregflag
        {
            get { return _unregflag; }
            set { _unregflag = value; }
        }
    }
}
