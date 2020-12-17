using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_ye
		public class t_ye
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
		/// accno
        /// </summary>		
		private string _accno;
        public string accno
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
		/// amount
        /// </summary>		
		private decimal _amount;
        public decimal amount
        {
            get{ return _amount; }
            set{ _amount = value; }
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

