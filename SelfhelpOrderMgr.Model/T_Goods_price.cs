using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Goods_price
		public class T_Goods_price
	{
   		     
      	/// <summary>
		/// seqno
        /// </summary>		
		private int _seqno;
        public int seqno
        {
            get{ return _seqno; }
            set{ _seqno = value; }
        }        
		/// <summary>
		/// gcode
        /// </summary>		
		private string _gcode;
        public string gcode
        {
            get{ return _gcode; }
            set{ _gcode = value; }
        }        
		/// <summary>
		/// gname
        /// </summary>		
		private string _gname;
        public string gname
        {
            get{ return _gname; }
            set{ _gname = value; }
        }        
		/// <summary>
		/// Gdj
        /// </summary>		
		private decimal _gdj;
        public decimal Gdj
        {
            get{ return _gdj; }
            set{ _gdj = value; }
        }        
		/// <summary>
		/// BegDate
        /// </summary>		
		private DateTime _begdate;
        public DateTime BegDate
        {
            get{ return _begdate; }
            set{ _begdate = value; }
        }        
		/// <summary>
		/// CRTBY
        /// </summary>		
		private string _crtby;
        public string CRTBY
        {
            get{ return _crtby; }
            set{ _crtby = value; }
        }        
		/// <summary>
		/// CRTDT
        /// </summary>		
		private DateTime _crtdt;
        public DateTime CRTDT
        {
            get{ return _crtdt; }
            set{ _crtdt = value; }
        }        
		/// <summary>
		/// MODBY
        /// </summary>		
		private string _modby;
        public string MODBY
        {
            get{ return _modby; }
            set{ _modby = value; }
        }        
		/// <summary>
		/// MODDT
        /// </summary>		
		private DateTime _moddt;
        public DateTime MODDT
        {
            get{ return _moddt; }
            set{ _moddt = value; }
        }        
		/// <summary>
		/// gorigdj
        /// </summary>		
		private decimal _gorigdj;
        public decimal gorigdj
        {
            get{ return _gorigdj; }
            set{ _gorigdj = value; }
        }        
		/// <summary>
		/// PlanPrice
        /// </summary>		
		private decimal _planprice;
        public decimal PlanPrice
        {
            get{ return _planprice; }
            set{ _planprice = value; }
        }        
		/// <summary>
		/// ChkBy
        /// </summary>		
		private string _chkby;
        public string ChkBy
        {
            get{ return _chkby; }
            set{ _chkby = value; }
        }        
		/// <summary>
		/// ChkDate
        /// </summary>		
		private DateTime _chkdate;
        public DateTime ChkDate
        {
            get{ return _chkdate; }
            set{ _chkdate = value; }
        }        
		/// <summary>
		/// ChkFlag
        /// </summary>		
		private int _chkflag;
        public int ChkFlag
        {
            get{ return _chkflag; }
            set{ _chkflag = value; }
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
		/// <summary>
		/// ChkInfo
        /// </summary>		
		private string _chkinfo;
        public string ChkInfo
        {
            get{ return _chkinfo; }
            set{ _chkinfo = value; }
        }        
		   
	}
}

