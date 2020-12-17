using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfhelpOrderMgr.Model
{
    public class InvoiceEntity
    {
        private string _InvoiceNo;

        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        private string _CardCode;

        public string CardCode
        {
            get { return _CardCode; }
            set { _CardCode = value; }
        }
        private string _FCrimeCode;

        public string FCrimeCode
        {
            get { return _FCrimeCode; }
            set { _FCrimeCode = value; }
        }
        private decimal _Amount;

        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private DateTime _OrderDate;

        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        private DateTime _PayDate;

        public DateTime PayDate
        {
            get { return _PayDate; }
            set { _PayDate = value; }
        }
        private string _PType;

        public string PType
        {
            get { return _PType; }
            set { _PType = value; }
        }
        private int _Flag;

        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private decimal _ServAmount;

        public decimal ServAmount
        {
            get { return _ServAmount; }
            set { _ServAmount = value; }
        }
        private string _Crtby;

        public string Crtby
        {
            get { return _Crtby; }
            set { _Crtby = value; }
        }
        private DateTime _Crtdate;

        public DateTime Crtdate
        {
            get { return _Crtdate; }
            set { _Crtdate = value; }
        }
        private string _fsn;

        public string Fsn
        {
            get { return _fsn; }
            set { _fsn = value; }
        }
        private string _FAreaCode;

        public string FAreaCode
        {
            get { return _FAreaCode; }
            set { _FAreaCode = value; }
        }
        private string _FAreaName;

        public string FAreaName
        {
            get { return _FAreaName; }
            set { _FAreaName = value; }
        }
        private string _FCriminal;

        public string FCriminal
        {
            get { return _FCriminal; }
            set { _FCriminal = value; }
        }
        private string _Frealareacode;

        public string Frealareacode
        {
            get { return _Frealareacode; }
            set { _Frealareacode = value; }
        }
        private string _FrealAreaName;

        public string FrealAreaName
        {
            get { return _FrealAreaName; }
            set { _FrealAreaName = value; }
        }
        private int _TypeFlag;

        public int TypeFlag
        {
            get { return _TypeFlag; }
            set { _TypeFlag = value; }
        }
        private int _CardType;

        public int CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        private decimal _AmountA;

        public decimal AmountA
        {
            get { return _AmountA; }
            set { _AmountA = value; }
        }
        private decimal _AmountB;

        public decimal AmountB
        {
            get { return _AmountB; }
            set { _AmountB = value; }
        }
        private int _Fifoflag;

        public int Fifoflag
        {
            get { return _Fifoflag; }
            set { _Fifoflag = value; }
        }
        private decimal _FreeAmountA;

        public decimal FreeAmountA
        {
            get { return _FreeAmountA; }
            set { _FreeAmountA = value; }
        }
        private decimal _FreeAmountB;

        public decimal FreeAmountB
        {
            get { return _FreeAmountB; }
            set { _FreeAmountB = value; }
        }
        private int _Checkflag;

        public int Checkflag
        {
            get { return _Checkflag; }
            set { _Checkflag = value; }
        }
        private string _RoomNo;

        public string RoomNo
        {
            get { return _RoomNo; }
            set { _RoomNo = value; }
        }
        private int _OrderId;

        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        private int _OrderStatus;
        public int OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }
    }
}
