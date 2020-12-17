using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SHO_ManagerSet
		public partial  class T_SHO_ManagerSet
	{
   		     
      	/// <summary>
		/// KeyName
        /// </summary>		
		private string _keyname;
        public string KeyName
        {
            get{ return _keyname; }
            set{ _keyname = value; }
        }        
		/// <summary>
		/// KeyMode
        /// </summary>		
		private int _keymode;
        public int KeyMode
        {
            get{ return _keymode; }
            set{ _keymode = value; }
        }        
		/// <summary>
		/// MgrName
        /// </summary>		
		private string _mgrname;
        public string MgrName
        {
            get{ return _mgrname; }
            set{ _mgrname = value; }
        }        
		/// <summary>
		/// MgrValue
        /// </summary>		
		private string _mgrvalue;
        public string MgrValue
        {
            get{ return _mgrvalue; }
            set{ _mgrvalue = value; }
        }        
		/// <summary>
		/// StartTime
        /// </summary>		
		private DateTime _starttime;
        public DateTime StartTime
        {
            get{ return _starttime; }
            set{ _starttime = value; }
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

