using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_bank_dealList
		public class t_bank_dealList
	{
   		     
      	/// <summary>
		/// seqno
        /// </summary>		
		private int _seqno;
        public int seqno
        {
            get{ return _seqno; }
            set{ _seqno = value; }
        }        
		/// <summary>
		/// AccNo
        /// </summary>		
		private string _accno;
        public string AccNo
        {
            get{ return _accno; }
            set{ _accno = value; }
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
		/// fName
        /// </summary>		
		private string _fname;
        public string fName
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// dcflag
        /// </summary>		
		private int _dcflag;
        public int dcflag
        {
            get{ return _dcflag; }
            set{ _dcflag = value; }
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
		/// BalAmount
        /// </summary>		
		private decimal _balamount;
        public decimal BalAmount
        {
            get{ return _balamount; }
            set{ _balamount = value; }
        }        
		/// <summary>
		/// Remark
        /// </summary>		
		private string _remark;
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		/// <summary>
		/// BankSeqno
        /// </summary>		
		private string _bankseqno;
        public string BankSeqno
        {
            get{ return _bankseqno; }
            set{ _bankseqno = value; }
        }        
		/// <summary>
		/// BankDealCode
        /// </summary>		
		private string _bankdealcode;
        public string BankDealCode
        {
            get{ return _bankdealcode; }
            set{ _bankdealcode = value; }
        }        
		/// <summary>
		/// DealDate
        /// </summary>		
		private DateTime _dealdate;
        public DateTime DealDate
        {
            get{ return _dealdate; }
            set{ _dealdate = value; }
        }        
		/// <summary>
		/// LoadDate
        /// </summary>		
		private DateTime _loaddate;
        public DateTime LoadDate
        {
            get{ return _loaddate; }
            set{ _loaddate = value; }
        }        
		/// <summary>
		/// flag
        /// </summary>		
		private int _flag;
        public int flag
        {
            get{ return _flag; }
            set{ _flag = value; }
        }        
		   
	}
}

