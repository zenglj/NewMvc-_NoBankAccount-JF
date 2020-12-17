using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Invoice_old
		public class T_Invoice_old
	{
   		     
      	/// <summary>
		/// INVOICENO
        /// </summary>		
		private string _invoiceno;
        public string INVOICENO
        {
            get{ return _invoiceno; }
            set{ _invoiceno = value; }
        }        
		/// <summary>
		/// cardcode
        /// </summary>		
		private string _cardcode;
        public string cardcode
        {
            get{ return _cardcode; }
            set{ _cardcode = value; }
        }        
		/// <summary>
		/// fcrimecode
        /// </summary>		
		private string _fcrimecode;
        public string fcrimecode
        {
            get{ return _fcrimecode; }
            set{ _fcrimecode = value; }
        }        
		/// <summary>
		/// amount
        /// </summary>		
		private decimal _amount;
        public decimal amount
        {
            get{ return _amount; }
            set{ _amount = value; }
        }        
		/// <summary>
		/// OrderDate
        /// </summary>		
		private DateTime _orderdate;
        public DateTime OrderDate
        {
            get{ return _orderdate; }
            set{ _orderdate = value; }
        }        
		/// <summary>
		/// PayDATE
        /// </summary>		
		private DateTime _paydate;
        public DateTime PayDATE
        {
            get{ return _paydate; }
            set{ _paydate = value; }
        }        
		/// <summary>
		/// PTYPE
        /// </summary>		
		private string _ptype;
        public string PTYPE
        {
            get{ return _ptype; }
            set{ _ptype = value; }
        }        
		/// <summary>
		/// Flag
        /// </summary>		
		private int _flag;
        public int Flag
        {
            get{ return _flag; }
            set{ _flag = value; }
        }        
		/// <summary>
		/// REMARK
        /// </summary>		
		private string _remark;
        public string REMARK
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		/// <summary>
		/// servamount
        /// </summary>		
		private decimal _servamount;
        public decimal servamount
        {
            get{ return _servamount; }
            set{ _servamount = value; }
        }        
		/// <summary>
		/// crtby
        /// </summary>		
		private string _crtby;
        public string crtby
        {
            get{ return _crtby; }
            set{ _crtby = value; }
        }        
		/// <summary>
		/// crtdate
        /// </summary>		
		private DateTime _crtdate;
        public DateTime crtdate
        {
            get{ return _crtdate; }
            set{ _crtdate = value; }
        }        
		/// <summary>
		/// fsn
        /// </summary>		
		private string _fsn;
        public string fsn
        {
            get{ return _fsn; }
            set{ _fsn = value; }
        }        
		/// <summary>
		/// fareacode
        /// </summary>		
		private string _fareacode;
        public string fareacode
        {
            get{ return _fareacode; }
            set{ _fareacode = value; }
        }        
		/// <summary>
		/// fareaName
        /// </summary>		
		private string _fareaname;
        public string fareaName
        {
            get{ return _fareaname; }
            set{ _fareaname = value; }
        }        
		/// <summary>
		/// fcriminal
        /// </summary>		
		private string _fcriminal;
        public string fcriminal
        {
            get{ return _fcriminal; }
            set{ _fcriminal = value; }
        }        
		/// <summary>
		/// Frealareacode
        /// </summary>		
		private string _frealareacode;
        public string Frealareacode
        {
            get{ return _frealareacode; }
            set{ _frealareacode = value; }
        }        
		/// <summary>
		/// FrealAreaName
        /// </summary>		
		private string _frealareaname;
        public string FrealAreaName
        {
            get{ return _frealareaname; }
            set{ _frealareaname = value; }
        }        
		/// <summary>
		/// TYPEFLAG
        /// </summary>		
		private int _typeflag;
        public int TYPEFLAG
        {
            get{ return _typeflag; }
            set{ _typeflag = value; }
        }        
		/// <summary>
		/// CardType
        /// </summary>		
		private int _cardtype;
        public int CardType
        {
            get{ return _cardtype; }
            set{ _cardtype = value; }
        }        
		/// <summary>
		/// AmountA
        /// </summary>		
		private decimal _amounta;
        public decimal AmountA
        {
            get{ return _amounta; }
            set{ _amounta = value; }
        }        
		/// <summary>
		/// AmountB
        /// </summary>		
		private decimal _amountb;
        public decimal AmountB
        {
            get{ return _amountb; }
            set{ _amountb = value; }
        }        
		/// <summary>
		/// fifoflag
        /// </summary>		
		private int _fifoflag;
        public int fifoflag
        {
            get{ return _fifoflag; }
            set{ _fifoflag = value; }
        }        
		/// <summary>
		/// FreeAmountA
        /// </summary>		
		private decimal _freeamounta;
        public decimal FreeAmountA
        {
            get{ return _freeamounta; }
            set{ _freeamounta = value; }
        }        
		/// <summary>
		/// FreeAmountB
        /// </summary>		
		private decimal _freeamountb;
        public decimal FreeAmountB
        {
            get{ return _freeamountb; }
            set{ _freeamountb = value; }
        }        
		/// <summary>
		/// checkflag
        /// </summary>		
		private int _checkflag;
        public int checkflag
        {
            get{ return _checkflag; }
            set{ _checkflag = value; }
        }        
		   
	}
}

