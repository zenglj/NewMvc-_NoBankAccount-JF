using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_FeeList
		public class t_FeeList
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
		/// code
        /// </summary>		
		private int _code;
        public int code
        {
            get{ return _code; }
            set{ _code = value; }
        }        
		/// <summary>
		/// subcode
        /// </summary>		
		private int _subcode;
        public int subcode
        {
            get{ return _subcode; }
            set{ _subcode = value; }
        }        
		/// <summary>
		/// fname
        /// </summary>		
		private string _fname;
        public string fname
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// DCFLAG
        /// </summary>		
		private int _dcflag;
        public int DCFLAG
        {
            get{ return _dcflag; }
            set{ _dcflag = value; }
        }        
		/// <summary>
		/// Subflag
        /// </summary>		
		private string _subflag;
        public string Subflag
        {
            get{ return _subflag; }
            set{ _subflag = value; }
        }        
		/// <summary>
		/// levelid
        /// </summary>		
		private int _levelid;
        public int levelid
        {
            get{ return _levelid; }
            set{ _levelid = value; }
        }        
		   
	}
}

