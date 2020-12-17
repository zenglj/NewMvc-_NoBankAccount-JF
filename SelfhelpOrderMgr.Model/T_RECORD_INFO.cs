using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_RECORD_INFO
		public class T_RECORD_INFO
	{
   		     
      	/// <summary>
		/// SEQ
        /// </summary>		
		private int _seq;
        public int SEQ
        {
            get{ return _seq; }
            set{ _seq = value; }
        }        
		/// <summary>
		/// FCode
        /// </summary>		
		private string _fcode;
        public string FCode
        {
            get{ return _fcode; }
            set{ _fcode = value; }
        }        
		/// <summary>
		/// BILLNO
        /// </summary>		
		private string _billno;
        public string BILLNO
        {
            get{ return _billno; }
            set{ _billno = value; }
        }        
		/// <summary>
		/// FILEPATH
        /// </summary>		
		private string _filepath;
        public string FILEPATH
        {
            get{ return _filepath; }
            set{ _filepath = value; }
        }        
		/// <summary>
		/// PNO
        /// </summary>		
		private string _pno;
        public string PNO
        {
            get{ return _pno; }
            set{ _pno = value; }
        }        
		/// <summary>
		/// WNO
        /// </summary>		
		private int _wno;
        public int WNO
        {
            get{ return _wno; }
            set{ _wno = value; }
        }        
		/// <summary>
		/// TYPE
        /// </summary>		
		private int _type;
        public int TYPE
        {
            get{ return _type; }
            set{ _type = value; }
        }        
		/// <summary>
		/// DUAL
        /// </summary>		
		private int _dual;
        public int DUAL
        {
            get{ return _dual; }
            set{ _dual = value; }
        }        
		/// <summary>
		/// STARTTIME
        /// </summary>		
		private DateTime _starttime;
        public DateTime STARTTIME
        {
            get{ return _starttime; }
            set{ _starttime = value; }
        }        
		/// <summary>
		/// STARTDATE
        /// </summary>		
		private DateTime _startdate;
        public DateTime STARTDATE
        {
            get{ return _startdate; }
            set{ _startdate = value; }
        }        
		   
	}
}

