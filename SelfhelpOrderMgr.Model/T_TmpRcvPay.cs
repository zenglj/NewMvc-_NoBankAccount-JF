using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_TmpRcvPay
		public class T_TmpRcvPay
	{
   		     
      	/// <summary>
		/// BankName
        /// </summary>		
		private string _bankname;
        public string BankName
        {
            get{ return _bankname; }
            set{ _bankname = value; }
        }        
		/// <summary>
		/// AccNo
        /// </summary>		
		private string _accno;
        public string AccNo
        {
            get{ return _accno; }
            set{ _accno = value; }
        }        
		/// <summary>
		/// Dtype
        /// </summary>		
		private string _dtype;
        public string Dtype
        {
            get{ return _dtype; }
            set{ _dtype = value; }
        }        
		/// <summary>
		/// paydate
        /// </summary>		
		private DateTime _paydate;
        public DateTime paydate
        {
            get{ return _paydate; }
            set{ _paydate = value; }
        }        
		/// <summary>
		/// Fmoney
        /// </summary>		
		private decimal _fmoney;
        public decimal Fmoney
        {
            get{ return _fmoney; }
            set{ _fmoney = value; }
        }        
		/// <summary>
		/// SucMoney
        /// </summary>		
		private decimal _sucmoney;
        public decimal SucMoney
        {
            get{ return _sucmoney; }
            set{ _sucmoney = value; }
        }        
		/// <summary>
		/// ErrMoney
        /// </summary>		
		private decimal _errmoney;
        public decimal ErrMoney
        {
            get{ return _errmoney; }
            set{ _errmoney = value; }
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

