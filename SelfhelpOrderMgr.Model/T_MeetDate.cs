using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_MeetDate
		public class T_MeetDate
	{
   		     
      	/// <summary>
		/// Fcode
        /// </summary>		
		private string _fcode;
        public string Fcode
        {
            get{ return _fcode; }
            set{ _fcode = value; }
        }        
		/// <summary>
		/// FName
        /// </summary>		
		private string _fname;
        public string FName
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// Fdesc
        /// </summary>		
		private string _fdesc;
        public string Fdesc
        {
            get{ return _fdesc; }
            set{ _fdesc = value; }
        }        
		   
	}
}

