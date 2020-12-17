using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_ICCARD_LIST
		public class T_ICCARD_LIST
	{
   		     
      	/// <summary>
		/// SEQNO
        /// </summary>		
		private int _seqno;
        public int SEQNO
        {
            get{ return _seqno; }
            set{ _seqno = value; }
        }        
		/// <summary>
		/// CardCode
        /// </summary>		
		private string _cardcode;
        public string CardCode
        {
            get{ return _cardcode; }
            set{ _cardcode = value; }
        }        
		/// <summary>
		/// FPWD
        /// </summary>		
		private string _fpwd;
        public string FPWD
        {
            get{ return _fpwd; }
            set{ _fpwd = value; }
        }        
		/// <summary>
		/// FCrimeCode
        /// </summary>		
		private string _fcrimecode;
        public string FCrimeCode
        {
            get{ return _fcrimecode; }
            set{ _fcrimecode = value; }
        }        
		/// <summary>
		/// FRCZY
        /// </summary>		
		private string _frczy;
        public string FRCZY
        {
            get{ return _frczy; }
            set{ _frczy = value; }
        }        
		/// <summary>
		/// FRDate
        /// </summary>		
		private DateTime _frdate;
        public DateTime FRDate
        {
            get{ return _frdate; }
            set{ _frdate = value; }
        }        
		/// <summary>
		/// FDELCZY
        /// </summary>		
		private string _fdelczy;
        public string FDELCZY
        {
            get{ return _fdelczy; }
            set{ _fdelczy = value; }
        }        
		/// <summary>
		/// FDelDate
        /// </summary>		
		private DateTime _fdeldate;
        public DateTime FDelDate
        {
            get{ return _fdeldate; }
            set{ _fdeldate = value; }
        }        
		/// <summary>
		/// Amount
        /// </summary>		
		private decimal _amount;
        public decimal Amount
        {
            get{ return _amount; }
            set{ _amount = value; }
        }        
		/// <summary>
		/// FFlag
        /// </summary>		
		private int _fflag;
        public int FFlag
        {
            get{ return _fflag; }
            set{ _fflag = value; }
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
		/// cardtype
        /// </summary>		
		private int _cardtype;
        public int cardtype
        {
            get{ return _cardtype; }
            set{ _cardtype = value; }
        }        
		   
	}
}

