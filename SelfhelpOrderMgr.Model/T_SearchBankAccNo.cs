using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SearchBankAccNo
		public class T_SearchBankAccNo
	{
   		     
      	/// <summary>
		/// keyId
        /// </summary>		
		private long _keyid;
        public long keyId
        {
            get{ return _keyid; }
            set{ _keyid = value; }
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
		/// FName
        /// </summary>		
		private string _fname;
        public string FName
        {
            get{ return _fname; }
            set{ _fname = value; }
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
		   
	}
}

