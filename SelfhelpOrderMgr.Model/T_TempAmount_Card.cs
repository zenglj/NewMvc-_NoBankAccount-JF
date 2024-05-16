using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_TempAmount_Card
		public class T_TempAmount_Card
	{
   		     
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
		/// fname
        /// </summary>		
		private string _fname;
        public string fname
        {
            get{ return _fname; }
            set{ _fname = value; }
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
		/// BankAccNo
        /// </summary>		
		private string _bankaccno;
        public string BankAccNo
        {
            get{ return _bankaccno; }
            set{ _bankaccno = value; }
        }        
		/// <summary>
		/// amounta
        /// </summary>		
		private decimal _amounta;
        public decimal amounta
        {
            get{ return _amounta; }
            set{ _amounta = value; }
        }        
		/// <summary>
		/// amountb
        /// </summary>		
		private decimal _amountb;
        public decimal amountb
        {
            get{ return _amountb; }
            set{ _amountb = value; }
        }        
		/// <summary>
		/// amountc
        /// </summary>		
		private decimal _amountc;
        public decimal amountc
        {
            get{ return _amountc; }
            set{ _amountc = value; }
        }        
		/// <summary>
		/// fmoney
        /// </summary>		
		private decimal _fmoney;
        public decimal fmoney
        {
            get{ return _fmoney; }
            set{ _fmoney = value; }
        }

        public int cardflaga { get; set; }//IC的状态

        public decimal AccPoints { get; set; }
	}
}

