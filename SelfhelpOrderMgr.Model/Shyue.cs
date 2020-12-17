using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//Shyue
		public class Shyue
	{
   		     
      	/// <summary>
		/// 卡号
        /// </summary>		
		private string _卡号;
        public string 卡号
        {
            get{ return _卡号; }
            set{ _卡号 = value; }
        }        
		/// <summary>
		/// 户名
        /// </summary>		
		private string _户名;
        public string 户名
        {
            get{ return _户名; }
            set{ _户名 = value; }
        }        
		/// <summary>
		/// 余额
        /// </summary>		
		private decimal _余额;
        public decimal 余额
        {
            get{ return _余额; }
            set{ _余额 = value; }
        }        
		   
	}
}

