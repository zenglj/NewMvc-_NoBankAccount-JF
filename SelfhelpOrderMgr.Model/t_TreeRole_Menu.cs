﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_TreeRole_Menu
		public partial class t_TreeRole_Menu
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
		private int _id;
        public int id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
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
		/// TreeId
        /// </summary>		
		private int _treeid;
        public int TreeId
        {
            get{ return _treeid; }
            set{ _treeid = value; }
        }        
		/// <summary>
		/// flag
        /// </summary>		
		private int _flag;
        public int flag
        {
            get{ return _flag; }
            set{ _flag = value; }
        }

        public int FId { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }
	}
}

