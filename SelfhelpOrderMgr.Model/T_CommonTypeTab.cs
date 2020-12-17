using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_CommonTypeTab
		public class T_CommonTypeTab
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// FType
        /// </summary>		
		private string _ftype;
        public string FType
        {
            get{ return _ftype; }
            set{ _ftype = value; }
        }        
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
		/// FRemark
        /// </summary>		
		private string _fremark;
        public string FRemark
        {
            get{ return _fremark; }
            set{ _fremark = value; }
        }        
		   
	}
}

