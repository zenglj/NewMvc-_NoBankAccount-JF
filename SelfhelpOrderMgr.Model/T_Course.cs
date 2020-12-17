using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Course
		public class T_Course
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
		/// FName
        /// </summary>		
		private string _fname;
        public string FName
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// FAccess
        /// </summary>		
		private int _faccess;
        public int FAccess
        {
            get{ return _faccess; }
            set{ _faccess = value; }
        }        
		/// <summary>
		/// FInterfaceMethods
        /// </summary>		
		private string _finterfacemethods;
        public string FInterfaceMethods
        {
            get{ return _finterfacemethods; }
            set{ _finterfacemethods = value; }
        }        
		/// <summary>
		/// BankID
        /// </summary>		
		private int _bankid;
        public int BankID
        {
            get{ return _bankid; }
            set{ _bankid = value; }
        }        
		/// <summary>
		/// BankAccNo
        /// </summary>		
		private string _bankaccno;
        public string BankAccNo
        {
            get{ return _bankaccno; }
            set{ _bankaccno = value; }
        }        
		/// <summary>
		/// Remark
        /// </summary>		
		private string _remark;
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		   
	}
}

