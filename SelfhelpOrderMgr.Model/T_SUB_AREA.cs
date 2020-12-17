using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SUB_AREA
		public class T_SUB_AREA
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
		/// FSubCode
        /// </summary>		
		private string _fsubcode;
        public string FSubCode
        {
            get{ return _fsubcode; }
            set{ _fsubcode = value; }
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
		   
	}
}

