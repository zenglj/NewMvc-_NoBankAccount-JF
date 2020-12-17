using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
	 	//T_SHO_BankReturnList
		public class T_SHO_BankReturnList
	{
   		     
      	/// <summary>
		/// BID
        /// </summary>		
		private string _bid;
        public string BID
        {
            get{ return _bid; }
            set{ _bid = value; }
        }        
		/// <summary>
		/// BankCard
        /// </summary>		
		private string _bankcard;
        public string BankCard
        {
            get{ return _bankcard; }
            set{ _bankcard = value; }
        }        
		/// <summary>
		/// UserName
        /// </summary>		
		private string _username;
        public string UserName
        {
            get{ return _username; }
            set{ _username = value; }
        }        
		/// <summary>
		/// UserMoney
        /// </summary>		
		private decimal _usermoney;
        public decimal UserMoney
        {
            get{ return _usermoney; }
            set{ _usermoney = value; }
        }        
		/// <summary>
		/// ResultInfo
        /// </summary>		
		private string _resultinfo;
        public string ResultInfo
        {
            get{ return _resultinfo; }
            set{ _resultinfo = value; }
        }        
		/// <summary>
		/// Flag
        /// </summary>		
		private int _flag;
        public int Flag
        {
            get{ return _flag; }
            set{ _flag = value; }
        }        
		   
	}
}

