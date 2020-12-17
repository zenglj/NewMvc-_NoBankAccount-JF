using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_MONEY
		public class T_MONEY
	{
   		     
      	/// <summary>
		/// FSeq
        /// </summary>		
		private int _fseq;
        public int FSeq
        {
            get{ return _fseq; }
            set{ _fseq = value; }
        }        
		/// <summary>
		/// FIdenNo
        /// </summary>		
		private string _fidenno;
        public string FIdenNo
        {
            get{ return _fidenno; }
            set{ _fidenno = value; }
        }        
		/// <summary>
		/// FMoney
        /// </summary>		
		private decimal _fmoney;
        public decimal FMoney
        {
            get{ return _fmoney; }
            set{ _fmoney = value; }
        }        
		/// <summary>
		/// FDate
        /// </summary>		
		private DateTime _fdate;
        public DateTime FDate
        {
            get{ return _fdate; }
            set{ _fdate = value; }
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
		   
	}
}

