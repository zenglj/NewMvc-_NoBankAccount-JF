using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_TreeRole
		public class t_TreeRole
	{
   		     
      	/// <summary>
		/// RoleID
        /// </summary>		
		private int _roleid;
        public int RoleID
        {
            get{ return _roleid; }
            set{ _roleid = value; }
        }        
		/// <summary>
		/// RoleName
        /// </summary>		
		private string _rolename;
        public string RoleName
        {
            get{ return _rolename; }
            set{ _rolename = value; }
        }        
		   
	}
}

