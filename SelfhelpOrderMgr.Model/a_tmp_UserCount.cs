using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//a_tmp_UserCount
		public class a_tmp_UserCount
	{
   		     
      	/// <summary>
		/// 证件号码
        /// </summary>		
		private string _证件号码;
        public string 证件号码
        {
            get{ return _证件号码; }
            set{ _证件号码 = value; }
        }        
		/// <summary>
		/// 中文名称
        /// </summary>		
		private string _中文名称;
        public string 中文名称
        {
            get{ return _中文名称; }
            set{ _中文名称 = value; }
        }        
		/// <summary>
		/// 卡号
        /// </summary>		
		private string _卡号;
        public string 卡号
        {
            get{ return _卡号; }
            set{ _卡号 = value; }
        }        
		   
	}
}

