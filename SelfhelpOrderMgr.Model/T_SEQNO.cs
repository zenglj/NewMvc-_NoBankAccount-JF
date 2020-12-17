using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SEQNO
		public class T_SEQNO
	{
   		     
      	/// <summary>
		/// SEQTYPE
        /// </summary>		
		private string _seqtype;
        public string SEQTYPE
        {
            get{ return _seqtype; }
            set{ _seqtype = value; }
        }        
		/// <summary>
		/// SEQNO
        /// </summary>		
		private int _seqno;
        public int SEQNO
        {
            get{ return _seqno; }
            set{ _seqno = value; }
        }        
		   
	}
}

