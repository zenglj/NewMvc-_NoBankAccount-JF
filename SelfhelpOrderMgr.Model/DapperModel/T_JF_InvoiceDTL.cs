using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_InvoiceDTL
    public class T_JF_InvoiceDTL:BaseModel
    {


        /// <summary>
        /// INVOICENO
        /// </summary>		
        private string _invoiceno;
        public string INVOICENO
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        /// <summary>
        /// GCODE
        /// </summary>		
        private string _gcode;
        public string GCODE
        {
            get { return _gcode; }
            set { _gcode = value; }
        }
        /// <summary>
        /// GNAME
        /// </summary>		
        private string _gname;
        public string GNAME
        {
            get { return _gname; }
            set { _gname = value; }
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
        /// PayDATE
        /// </summary>		
        private DateTime _paydate;
        public DateTime PayDATE
        {
            get { return _paydate; }
            set { _paydate = value; }
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
        /// GDJ
        /// </summary>		
        private decimal _gdj;
        public decimal GDJ
        {
            get { return _gdj; }
            set { _gdj = value; }
        }
        /// <summary>
        /// QTY
        /// </summary>		
        private decimal _qty;
        public decimal QTY
        {
            get { return _qty; }
            set { _qty = value; }
        }
        /// <summary>
        /// AMOUNT
        /// </summary>		
        private decimal _amount;
        public decimal AMOUNT
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// Servamount
        /// </summary>		
        private decimal _servamount;
        public decimal Servamount
        {
            get { return _servamount; }
            set { _servamount = value; }
        }
        /// <summary>
        /// GTXM
        /// </summary>		
        private string _gtxm;
        public string GTXM
        {
            get { return _gtxm; }
            set { _gtxm = value; }
        }
        /// <summary>
        /// FCrimecode
        /// </summary>		
        private string _fcrimecode;
        public string FCrimecode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
        }
        /// <summary>
        /// GORGDJ
        /// </summary>		
        private decimal _gorgdj;
        public decimal GORGDJ
        {
            get { return _gorgdj; }
            set { _gorgdj = value; }
        }
        /// <summary>
        /// GORGAMT
        /// </summary>		
        private decimal _gorgamt;
        public decimal GORGAMT
        {
            get { return _gorgamt; }
            set { _gorgamt = value; }
        }
        /// <summary>
        /// StockSeqno
        /// </summary>		
        private int _stockseqno;
        public int StockSeqno
        {
            get { return _stockseqno; }
            set { _stockseqno = value; }
        }
        /// <summary>
        /// Typeflag
        /// </summary>		
        private int _typeflag;
        public int Typeflag
        {
            get { return _typeflag; }
            set { _typeflag = value; }
        }
        /// <summary>
        /// Cardtype
        /// </summary>		
        private int _cardtype;
        public int Cardtype
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
        /// Backqty
        /// </summary>		
        private decimal _backqty;
        public decimal Backqty
        {
            get { return _backqty; }
            set { _backqty = value; }
        }
        /// <summary>
        /// FreeFlag
        /// </summary>		
        private int _freeflag;
        public int FreeFlag
        {
            get { return _freeflag; }
            set { _freeflag = value; }
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
        /// SPShortCode
        /// </summary>		
        private string _spshortcode;
        public string SPShortCode
        {
            get { return _spshortcode; }
            set { _spshortcode = value; }
        }
        /// <summary>
        /// FTZSP_TypeFlag
        /// </summary>		
        private int _ftzsp_typeflag;
        public int FTZSP_TypeFlag
        {
            get { return _ftzsp_typeflag; }
            set { _ftzsp_typeflag = value; }
        }

    }
}

