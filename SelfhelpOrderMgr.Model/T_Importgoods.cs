using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Importgoods
		public class T_Importgoods
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
		/// pc
        /// </summary>		
		private int _pc;
        public int pc
        {
            get{ return _pc; }
            set{ _pc = value; }
        }        
		/// <summary>
		/// gtypename
        /// </summary>		
		private string _gtypename;
        public string gtypename
        {
            get{ return _gtypename; }
            set{ _gtypename = value; }
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
		/// gtxm
        /// </summary>		
		private string _gtxm;
        public string gtxm
        {
            get{ return _gtxm; }
            set{ _gtxm = value; }
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
		/// crtdate
        /// </summary>		
		private DateTime _crtdate;
        public DateTime crtdate
        {
            get{ return _crtdate; }
            set{ _crtdate = value; }
        }        
		   
	}
}

