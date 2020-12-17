using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_EdiDetail
		public partial class T_EdiDetail
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private long _id;
        public long ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
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
		/// Mainseqno
        /// </summary>		
		private int _mainseqno;
        public int Mainseqno
        {
            get{ return _mainseqno; }
            set{ _mainseqno = value; }
        }        
		/// <summary>
		/// vouno
        /// </summary>		
		private string _vouno;
        public string vouno
        {
            get{ return _vouno; }
            set{ _vouno = value; }
        }        
		/// <summary>
		/// DAMOUNT
        /// </summary>		
		private decimal _damount;
        public decimal DAMOUNT
        {
            get{ return _damount; }
            set{ _damount = value; }
        }        
		/// <summary>
		/// CAMOUNT
        /// </summary>		
		private decimal _camount;
        public decimal CAMOUNT
        {
            get{ return _camount; }
            set{ _camount = value; }
        }        
		/// <summary>
		/// FCRIMECODE
        /// </summary>		
		private string _fcrimecode;
        public string FCRIMECODE
        {
            get{ return _fcrimecode; }
            set{ _fcrimecode = value; }
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
		/// SubTypeflag
        /// </summary>		
		private int _subtypeflag;
        public int SubTypeflag
        {
            get{ return _subtypeflag; }
            set{ _subtypeflag = value; }
        }        
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
		/// origid
        /// </summary>		
		private string _origid;
        public string origid
        {
            get{ return _origid; }
            set{ _origid = value; }
        }        
		/// <summary>
		/// SuccFlag
        /// </summary>		
		private int _succflag;
        public int SuccFlag
        {
            get{ return _succflag; }
            set{ _succflag = value; }
        }        
		/// <summary>
		/// vcrdseqno
        /// </summary>		
		private int _vcrdseqno;
        public int vcrdseqno
        {
            get{ return _vcrdseqno; }
            set{ _vcrdseqno = value; }
        }        
		/// <summary>
		/// remark
        /// </summary>		
		private string _remark;
        public string remark
        {
            get{ return _remark; }
            set{ _remark = value; }
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
		   
	}
}

