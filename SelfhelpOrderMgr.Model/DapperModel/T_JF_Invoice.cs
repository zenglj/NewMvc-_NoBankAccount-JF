﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Invoice
    public partial class T_JF_Invoice:BaseModel
    {

        /// <summary>
        /// InvoiceNo
        /// </summary>		
        private string _invoiceno;
        public string InvoiceNo
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        /// <summary>
        /// CardCode
        /// </summary>		
        private string _cardcode;
        public string CardCode
        {
            get { return _cardcode; }
            set { _cardcode = value; }
        }
        /// <summary>
        /// FCrimeCode
        /// </summary>		
        private string _fcrimecode;
        public string FCrimeCode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
        }
        /// <summary>
        /// Amount
        /// </summary>		
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// OrderDate
        /// </summary>		
        private DateTime _orderdate;
        public DateTime OrderDate
        {
            get { return _orderdate; }
            set { _orderdate = value; }
        }
        /// <summary>
        /// PayDate
        /// </summary>		
        private DateTime _paydate;
        public DateTime PayDate
        {
            get { return _paydate; }
            set { _paydate = value; }
        }
        /// <summary>
        /// PType
        /// </summary>		
        private string _ptype;
        public string PType
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        /// <summary>
        /// Flag
        /// </summary>		
        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        /// <summary>
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// ServAmount
        /// </summary>		
        private decimal _servamount;
        public decimal ServAmount
        {
            get { return _servamount; }
            set { _servamount = value; }
        }
        /// <summary>
        /// Crtby
        /// </summary>		
        private string _crtby;
        public string Crtby
        {
            get { return _crtby; }
            set { _crtby = value; }
        }
        /// <summary>
        /// Crtdate
        /// </summary>		
        private DateTime _crtdate;
        public DateTime Crtdate
        {
            get { return _crtdate; }
            set { _crtdate = value; }
        }
        /// <summary>
        /// fsn
        /// </summary>		
        private string _fsn;
        public string fsn
        {
            get { return _fsn; }
            set { _fsn = value; }
        }
        /// <summary>
        /// FAreaCode
        /// </summary>		
        private string _fareacode;
        public string FAreaCode
        {
            get { return _fareacode; }
            set { _fareacode = value; }
        }
        /// <summary>
        /// FAreaName
        /// </summary>		
        private string _fareaname;
        public string FAreaName
        {
            get { return _fareaname; }
            set { _fareaname = value; }
        }
        /// <summary>
        /// FCriminal
        /// </summary>		
        private string _fcriminal;
        public string FCriminal
        {
            get { return _fcriminal; }
            set { _fcriminal = value; }
        }
        /// <summary>
        /// Frealareacode
        /// </summary>		
        private string _frealareacode;
        public string Frealareacode
        {
            get { return _frealareacode; }
            set { _frealareacode = value; }
        }
        /// <summary>
        /// FrealAreaName
        /// </summary>		
        private string _frealareaname;
        public string FrealAreaName
        {
            get { return _frealareaname; }
            set { _frealareaname = value; }
        }
        /// <summary>
        /// TypeFlag
        /// </summary>		
        private int _typeflag;
        public int TypeFlag
        {
            get { return _typeflag; }
            set { _typeflag = value; }
        }
        /// <summary>
        /// CardType
        /// </summary>		
        private int _cardtype;
        public int CardType
        {
            get { return _cardtype; }
            set { _cardtype = value; }
        }
        /// <summary>
        /// AmountA
        /// </summary>		
        private decimal _amounta;
        public decimal AmountA
        {
            get { return _amounta; }
            set { _amounta = value; }
        }
        /// <summary>
        /// AmountB
        /// </summary>		
        private decimal _amountb;
        public decimal AmountB
        {
            get { return _amountb; }
            set { _amountb = value; }
        }
        /// <summary>
        /// Fifoflag
        /// </summary>		
        private int _fifoflag;
        public int Fifoflag
        {
            get { return _fifoflag; }
            set { _fifoflag = value; }
        }
        /// <summary>
        /// FreeAmountA
        /// </summary>		
        private decimal _freeamounta;
        public decimal FreeAmountA
        {
            get { return _freeamounta; }
            set { _freeamounta = value; }
        }
        /// <summary>
        /// FreeAmountB
        /// </summary>		
        private decimal _freeamountb;
        public decimal FreeAmountB
        {
            get { return _freeamountb; }
            set { _freeamountb = value; }
        }
        /// <summary>
        /// Checkflag
        /// </summary>		
        private int _checkflag;
        public int Checkflag
        {
            get { return _checkflag; }
            set { _checkflag = value; }
        }
        /// <summary>
        /// RoomNo
        /// </summary>		
        private string _roomno;
        public string RoomNo
        {
            get { return _roomno; }
            set { _roomno = value; }
        }
        /// <summary>
        /// OrderId
        /// </summary>		
        private int _orderid;
        public int OrderId
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
        /// <summary>
        /// FTZSP_Money
        /// </summary>		
        private decimal _ftzsp_money;
        public decimal FTZSP_Money
        {
            get { return _ftzsp_money; }
            set { _ftzsp_money = value; }
        }
        /// <summary>
        /// printCount
        /// </summary>		
        private int _printcount;
        public int printCount
        {
            get { return _printcount; }
            set { _printcount = value; }
        }
        /// <summary>
        /// OrderStatus
        /// </summary>		
        private int _orderstatus;
        public int OrderStatus
        {
            get { return _orderstatus; }
            set { _orderstatus = value; }
        }
        /// <summary>
        /// UserCyDesc
        /// </summary>		
        private string _usercydesc;
        public string UserCyDesc
        {
            get { return _usercydesc; }
            set { _usercydesc = value; }
        }

    }
}

