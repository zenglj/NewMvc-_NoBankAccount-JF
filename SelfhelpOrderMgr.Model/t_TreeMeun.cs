﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//t_TreeMeun
		public partial class t_TreeMeun
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
		/// fcode
        /// </summary>		
		private string _fcode;
        public string fcode
        {
            get{ return _fcode; }
            set{ _fcode = value; }
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
		/// <summary>
		/// Text
        /// </summary>		
		private string _text;
        public string Text
        {
            get{ return _text; }
            set{ _text = value; }
        }        
		/// <summary>
		/// FId
        /// </summary>		
		private int _fid;
        public int FId
        {
            get{ return _fid; }
            set{ _fid = value; }
        }        
		/// <summary>
		/// URL
        /// </summary>		
		private string _url;
        public string URL
        {
            get{ return _url; }
            set{ _url = value; }
        }        
		   
	}
}

