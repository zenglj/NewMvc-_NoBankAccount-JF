using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model
{
	 	//T_NotifyFile
		public class T_NotifyFile
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// FTitle
        /// </summary>		
		private string _ftitle;
        public string FTitle
        {
            get{ return _ftitle; }
            set{ _ftitle = value; }
        }        
		/// <summary>
		/// FAbstract
        /// </summary>		
		private string _fabstract;
        public string FAbstract
        {
            get{ return _fabstract; }
            set{ _fabstract = value; }
        }        
		/// <summary>
		/// FAuthor
        /// </summary>		
		private string _fauthor;
        public string FAuthor
        {
            get{ return _fauthor; }
            set{ _fauthor = value; }
        }        
		/// <summary>
		/// FDate
        /// </summary>		
		private DateTime _fdate;
        public DateTime FDate
        {
            get{ return _fdate; }
            set{ _fdate = value; }
        }        
		/// <summary>
		/// LinkWebFile
        /// </summary>		
		private string _linkwebfile;
        public string LinkWebFile
        {
            get{ return _linkwebfile; }
            set{ _linkwebfile = value; }
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

