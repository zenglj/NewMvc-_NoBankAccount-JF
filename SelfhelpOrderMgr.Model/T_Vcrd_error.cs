using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Vcrd_error
		public class T_Vcrd_error
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
		/// fcrimecode
        /// </summary>		
		private string _fcrimecode;
        public string fcrimecode
        {
            get{ return _fcrimecode; }
            set{ _fcrimecode = value; }
        }        
		/// <summary>
		/// fcriminal
        /// </summary>		
		private string _fcriminal;
        public string fcriminal
        {
            get{ return _fcriminal; }
            set{ _fcriminal = value; }
        }        
		/// <summary>
		/// famount
        /// </summary>		
		private decimal _famount;
        public decimal famount
        {
            get{ return _famount; }
            set{ _famount = value; }
        }        
		/// <summary>
		/// famounta
        /// </summary>		
		private decimal _famounta;
        public decimal famounta
        {
            get{ return _famounta; }
            set{ _famounta = value; }
        }        
		/// <summary>
		/// famountb
        /// </summary>		
		private decimal _famountb;
        public decimal famountb
        {
            get{ return _famountb; }
            set{ _famountb = value; }
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
		/// Crtby
        /// </summary>		
		private string _crtby;
        public string Crtby
        {
            get{ return _crtby; }
            set{ _crtby = value; }
        }        
		/// <summary>
		/// crtdate
        /// </summary>		
		private DateTime _crtdate;
        public DateTime crtdate
        {
            get{ return _crtdate; }
            set{ _crtdate = value; }
        }        
		/// <summary>
		/// pc
        /// </summary>		
		private int _pc;
        public int pc
        {
            get{ return _pc; }
            set{ _pc = value; }
        }        
		/// <summary>
		/// typeflag
        /// </summary>		
		private int _typeflag;
        public int typeflag
        {
            get{ return _typeflag; }
            set{ _typeflag = value; }
        }        
		/// <summary>
		/// acctype
        /// </summary>		
		private int _acctype;
        public int acctype
        {
            get{ return _acctype; }
            set{ _acctype = value; }
        }        
		/// <summary>
		/// notes
        /// </summary>		
		private string _notes;
        public string notes
        {
            get{ return _notes; }
            set{ _notes = value; }
        }        
		   
	}
}

