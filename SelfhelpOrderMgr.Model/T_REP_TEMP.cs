using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_REP_TEMP
		public class T_REP_TEMP
	{
   		     
      	/// <summary>
		/// FCCode
        /// </summary>		
		private string _fccode;
        public string FCCode
        {
            get{ return _fccode; }
            set{ _fccode = value; }
        }        
		/// <summary>
		/// FCount
        /// </summary>		
		private int _fcount;
        public int FCount
        {
            get{ return _fcount; }
            set{ _fcount = value; }
        }        
		   
	}
}

