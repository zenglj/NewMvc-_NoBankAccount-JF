using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_bankinfo
		public class t_bankinfo
	{
   		     
      	/// <summary>
		/// AccCode
        /// </summary>		
		private string _acccode;
        public string AccCode
        {
            get{ return _acccode; }
            set{ _acccode = value; }
        }        
		/// <summary>
		/// BankName
        /// </summary>		
		private string _bankname;
        public string BankName
        {
            get{ return _bankname; }
            set{ _bankname = value; }
        }        
		/// <summary>
		/// TradeCode
        /// </summary>		
		private string _tradecode;
        public string TradeCode
        {
            get{ return _tradecode; }
            set{ _tradecode = value; }
        }        
		/// <summary>
		/// TradeBankNo
        /// </summary>		
		private string _tradebankno;
        public string TradeBankNo
        {
            get{ return _tradebankno; }
            set{ _tradebankno = value; }
        }        
		/// <summary>
		/// inoutflag
        /// </summary>		
		private int _inoutflag;
        public int inoutflag
        {
            get{ return _inoutflag; }
            set{ _inoutflag = value; }
        }        
		/// <summary>
		/// TradeTerminal
        /// </summary>		
		private string _tradeterminal;
        public string TradeTerminal
        {
            get{ return _tradeterminal; }
            set{ _tradeterminal = value; }
        }        
		/// <summary>
		/// TradeBy
        /// </summary>		
		private string _tradeby;
        public string TradeBy
        {
            get{ return _tradeby; }
            set{ _tradeby = value; }
        }        
		/// <summary>
		/// compcode
        /// </summary>		
		private string _compcode;
        public string compcode
        {
            get{ return _compcode; }
            set{ _compcode = value; }
        }        
		/// <summary>
		/// portcode
        /// </summary>		
		private int _portcode;
        public int portcode
        {
            get{ return _portcode; }
            set{ _portcode = value; }
        }        
		/// <summary>
		/// AgentPort
        /// </summary>		
		private int _agentport;
        public int AgentPort
        {
            get{ return _agentport; }
            set{ _agentport = value; }
        }        
		/// <summary>
		/// Custid_type
        /// </summary>		
		private string _custid_type;
        public string Custid_type
        {
            get{ return _custid_type; }
            set{ _custid_type = value; }
        }        
		/// <summary>
		/// cust_id
        /// </summary>		
		private string _cust_id;
        public string cust_id
        {
            get{ return _cust_id; }
            set{ _cust_id = value; }
        }        
		/// <summary>
		/// FeeCode
        /// </summary>		
		private string _feecode;
        public string FeeCode
        {
            get{ return _feecode; }
            set{ _feecode = value; }
        }        
		/// <summary>
		/// MainFeeCode
        /// </summary>		
		private string _mainfeecode;
        public string MainFeeCode
        {
            get{ return _mainfeecode; }
            set{ _mainfeecode = value; }
        }        
		   
	}
}

