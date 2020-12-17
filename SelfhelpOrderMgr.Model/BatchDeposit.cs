using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//BatchDeposit
		public class BatchDeposit
	{
   		     
      	/// <summary>
		/// BID
        /// </summary>		
		private string _bid;
        public string BID
        {
            get{ return _bid; }
            set{ _bid = value; }
        }        
		/// <summary>
		/// FAREACODE
        /// </summary>		
		private string _fareacode;
        public string FAREACODE
        {
            get{ return _fareacode; }
            set{ _fareacode = value; }
        }        
		/// <summary>
		/// fAMOUNT
        /// </summary>		
		private decimal _famount;
        public decimal fAMOUNT
        {
            get{ return _famount; }
            set{ _famount = value; }
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
		/// ApplyBy
        /// </summary>		
		private string _applyby;
        public string ApplyBy
        {
            get{ return _applyby; }
            set{ _applyby = value; }
        }        
		/// <summary>
		/// Applydt
        /// </summary>		
		private DateTime _applydt;
        public DateTime Applydt
        {
            get{ return _applydt; }
            set{ _applydt = value; }
        }        
		/// <summary>
		/// Crtby
        /// </summary>		
		private string _crtby;
        public string Crtby
        {
            get{ return _crtby; }
            set{ _crtby = value; }
        }        
		/// <summary>
		/// crtdt
        /// </summary>		
		private DateTime _crtdt;
        public DateTime crtdt
        {
            get{ return _crtdt; }
            set{ _crtdt = value; }
        }        
		/// <summary>
		/// CHECKBY
        /// </summary>		
		private string _checkby;
        public string CHECKBY
        {
            get{ return _checkby; }
            set{ _checkby = value; }
        }        
		/// <summary>
		/// CheckDt
        /// </summary>		
		private DateTime _checkdt;
        public DateTime CheckDt
        {
            get{ return _checkdt; }
            set{ _checkdt = value; }
        }        
		/// <summary>
		/// FLAG
        /// </summary>		
		private int _flag;
        public int FLAG
        {
            get{ return _flag; }
            set{ _flag = value; }
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
		/// udate
        /// </summary>		
		private DateTime _udate;
        public DateTime udate
        {
            get{ return _udate; }
            set{ _udate = value; }
        }        
		/// <summary>
		/// ptype
        /// </summary>		
		private string _ptype;
        public string ptype
        {
            get{ return _ptype; }
            set{ _ptype = value; }
        }        
		/// <summary>
		/// cnt
        /// </summary>		
		private int _cnt;
        public int cnt
        {
            get{ return _cnt; }
            set{ _cnt = value; }
        }        
		   
	}
}

