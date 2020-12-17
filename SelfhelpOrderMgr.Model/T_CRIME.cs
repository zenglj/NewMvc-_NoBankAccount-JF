using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_CRIME
		public class T_CRIME
	{
   		     
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
		/// FName
        /// </summary>		
		private string _fname;
        public string FName
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// FDesc
        /// </summary>		
		private string _fdesc;
        public string FDesc
        {
            get{ return _fdesc; }
            set{ _fdesc = value; }
        }        
		   
	}
}

