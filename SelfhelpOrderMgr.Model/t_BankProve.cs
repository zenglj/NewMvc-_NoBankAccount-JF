﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_BankProve
		public partial class t_BankProve
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
		/// foudate
        /// </summary>		
		private DateTime _foudate;
        public DateTime foudate
        {
            get{ return _foudate; }
            set{ _foudate = value; }
        }        
		/// <summary>
		/// FIdenNo
        /// </summary>		
		private string _fidenno;
        public string FIdenNo
        {
            get{ return _fidenno; }
            set{ _fidenno = value; }
        }        
		/// <summary>
		/// fareaName
        /// </summary>		
		private string _fareaname;
        public string fareaName
        {
            get{ return _fareaname; }
            set{ _fareaname = value; }
        }        
		/// <summary>
		/// BankCode
        /// </summary>		
		private string _bankcode;
        public string BankCode
        {
            get{ return _bankcode; }
            set{ _bankcode = value; }
        }        
		/// <summary>
		/// CardMoney
        /// </summary>		
		private decimal _cardmoney;
        public decimal CardMoney
        {
            get{ return _cardmoney; }
            set{ _cardmoney = value; }
        }        
		/// <summary>
		/// crtDate
        /// </summary>		
		private DateTime _crtdate;
        public DateTime crtDate
        {
            get{ return _crtdate; }
            set{ _crtdate = value; }
        }        
		   
	}
}

