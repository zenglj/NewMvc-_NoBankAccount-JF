using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SETTINGS
		public class T_SETTINGS
	{
   		     
      	/// <summary>
		/// SEQ
        /// </summary>		
		private int _seq;
        public int SEQ
        {
            get{ return _seq; }
            set{ _seq = value; }
        }        
		/// <summary>
		/// NAME
        /// </summary>		
		private string _name;
        public string NAME
        {
            get{ return _name; }
            set{ _name = value; }
        }        
		/// <summary>
		/// VALUE
        /// </summary>		
		private string _value;
        public string VALUE
        {
            get{ return _value; }
            set{ _value = value; }
        }        
		/// <summary>
		/// TYPE
        /// </summary>		
		private int _type;
        public int TYPE
        {
            get{ return _type; }
            set{ _type = value; }
        }        
		/// <summary>
		/// remark
        /// </summary>		
		private string _remark;
        public string remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		   
	}
}

