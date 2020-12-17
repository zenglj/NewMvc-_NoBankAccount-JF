using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_BankAccount
		public class T_BankAccount
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
		/// FBankAccNo
        /// </summary>		
		private string _fbankaccno;
        public string FBankAccNo
        {
            get{ return _fbankaccno; }
            set{ _fbankaccno = value; }
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

