using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SHO_ClientMachine
		public partial  class T_SHO_ClientMachine
	{
   		     
      	/// <summary>
		/// Id
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// ClientName
        /// </summary>		
		private string _clientname;
        public string ClientName
        {
            get{ return _clientname; }
            set{ _clientname = value; }
        }        
		/// <summary>
		/// IpAddr
        /// </summary>		
		private string _ipaddr;
        public string IpAddr
        {
            get{ return _ipaddr; }
            set{ _ipaddr = value; }
        }        
		   
	}
}

