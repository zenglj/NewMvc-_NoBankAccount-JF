using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfhelpOrderMgr.Model
{
    public class CriminalIn
    {
        private bool _result;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }
        private string _resultMsg;

        public string ResultMsg
        {
            get { return _resultMsg; }
            set { _resultMsg = value; }
        }
        private CriminalEntity _CriminalModel;

        public CriminalEntity CriminalModel
        {
            get { return _CriminalModel; }
            set { _CriminalModel = value; }
        }

        private CriminalCardEntity _CriminalCardModel;

        public CriminalCardEntity CriminalCardModel
        {
            get { return _CriminalCardModel; }
            set { _CriminalCardModel = value; }
        }

        private List<CriminalCardEntity> _CriminalCardList;

        public List<CriminalCardEntity> CriminalCardList
        {
            get { return _CriminalCardList; }
            set { _CriminalCardList = value; }
        }

        private List<VcrdEntity> _VcrdListModel;

        public List<VcrdEntity> VcrdListModel
        {
            get { return _VcrdListModel; }
            set { _VcrdListModel = value; }
        }

       
    }

    public class CriminalEntity
    {
        private string _FCode;

        public string FCode
        {
            get { return _FCode; }
            set { _FCode = value; }
        }
        private string _FName;

        public string FName
        {
            get { return _FName; }
            set { _FName = value; }
        }
        private string _FIdenNo;

        public string FIdenNo
        {
            get { return _FIdenNo; }
            set { _FIdenNo = value; }
        }
        private int _FAge;

        public int FAge
        {
            get { return _FAge; }
            set { _FAge = value; }
        }
        private string _FSex;

        public string FSex
        {
            get { return _FSex; }
            set { _FSex = value; }
        }
        private string _FAddr;

        public string FAddr
        {
            get { return _FAddr; }
            set { _FAddr = value; }
        }
        private string _FCrimeCode;

        public string FCrimeCode
        {
            get { return _FCrimeCode; }
            set { _FCrimeCode = value; }
        }
        private string _FCYCode;

        public string FCYCode
        {
            get { return _FCYCode; }
            set { _FCYCode = value; }
        }
        private string _FTerm;

        public string FTerm
        {
            get { return _FTerm; }
            set { _FTerm = value; }
        }
        private DateTime _FInDate;

        public DateTime FInDate
        {
            get { return _FInDate; }
            set { _FInDate = value; }
        }
        private DateTime _FOuDate;

        public DateTime FOuDate
        {
            get { return _FOuDate; }
            set { _FOuDate = value; }
        }
        private string _FAreaCode;

        public string FAreaCode
        {
            get { return _FAreaCode; }
            set { _FAreaCode = value; }
        }
        private string _FSubArea;

        public string FSubArea
        {
            get { return _FSubArea; }
            set { _FSubArea = value; }
        }
        private string _FDesc;

        public string FDesc
        {
            get { return _FDesc; }
            set { _FDesc = value; }
        }
        private int _FStatus;

        public int FStatus
        {
            get { return _FStatus; }
            set { _FStatus = value; }
        }
        private string _FAddr_tmp;

        public string FAddr_tmp
        {
            get { return _FAddr_tmp; }
            set { _FAddr_tmp = value; }
        }
        private string _FCZY;

        public string FCZY
        {
            get { return _FCZY; }
            set { _FCZY = value; }
        }
        private int _fflag;

        public int Fflag
        {
            get { return _fflag; }
            set { _fflag = value; }
        }
        private int _Cardflag;

        public int Cardflag
        {
            get { return _Cardflag; }
            set { _Cardflag = value; }
        }

        private string _Frealareacode;

        public string Frealareacode
        {
            get { return _Frealareacode; }
            set { _Frealareacode = value; }
        }
        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

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

    //提交给银行的响应数据
    public class ReqDataEntity
    {
        //银行处理的结果，成功返回“9999”
        public string doResult { get; set; }
        //系统返回的结果
        public object data { get; set; }

    }
}
