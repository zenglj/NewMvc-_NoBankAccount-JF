using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_GoodsStock
		public class T_GoodsStock
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
		/// gcode
        /// </summary>		
		private string _gcode;
        public string gcode
        {
            get{ return _gcode; }
            set{ _gcode = value; }
        }        
		/// <summary>
		/// gtxm
        /// </summary>		
		private string _gtxm;
        public string gtxm
        {
            get{ return _gtxm; }
            set{ _gtxm = value; }
        }        
		/// <summary>
		/// InDate
        /// </summary>		
		private DateTime _indate;
        public DateTime InDate
        {
            get{ return _indate; }
            set{ _indate = value; }
        }        
		/// <summary>
		/// Gdj
        /// </summary>		
		private decimal _gdj;
        public decimal Gdj
        {
            get{ return _gdj; }
            set{ _gdj = value; }
        }        
		/// <summary>
		/// Balance
        /// </summary>		
		private decimal _balance;
        public decimal Balance
        {
            get{ return _balance; }
            set{ _balance = value; }
        }        
		/// <summary>
		/// TmpBalance
        /// </summary>		
		private decimal _tmpbalance;
        public decimal TmpBalance
        {
            get{ return _tmpbalance; }
            set{ _tmpbalance = value; }
        }        
		   
	}
}

