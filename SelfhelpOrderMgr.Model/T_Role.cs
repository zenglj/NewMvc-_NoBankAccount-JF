using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Role
		public class T_Role
	{
   		     
      	/// <summary>
		/// fcode
        /// </summary>		
		private string _fcode;
        public string fcode
        {
            get{ return _fcode; }
            set{ _fcode = value; }
        }        
		/// <summary>
		/// fname
        /// </summary>		
		private string _fname;
        public string fname
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// fflag
        /// </summary>		
		private int _fflag;
        public int fflag
        {
            get{ return _fflag; }
            set{ _fflag = value; }
        }        
		/// <summary>
		/// fprivate
        /// </summary>		
		private int _fprivate;
        public int fprivate
        {
            get{ return _fprivate; }
            set{ _fprivate = value; }
        }        
		   
	}
}

