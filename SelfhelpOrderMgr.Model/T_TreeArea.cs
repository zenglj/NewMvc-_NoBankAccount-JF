using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_TreeArea
		public class T_TreeArea
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private string _id;
        public string ID
        {
            get{ return _id; }
            set{ _id = value; }
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
		/// FID
        /// </summary>		
		private string _fid;
        public string FID
        {
            get{ return _fid; }
            set{ _fid = value; }
        }        
		/// <summary>
		/// fmeetdate
        /// </summary>		
		private string _fmeetdate;
        public string fmeetdate
        {
            get{ return _fmeetdate; }
            set{ _fmeetdate = value; }
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

