using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_EdiList
		public class T_EdiList
	{
   		     
      	/// <summary>
		/// seqno
        /// </summary>		
		private int _seqno;
        public int seqno
        {
            get{ return _seqno; }
            set{ _seqno = value; }
        }        
		/// <summary>
		/// Code
        /// </summary>		
		private string _code;
        public string Code
        {
            get{ return _code; }
            set{ _code = value; }
        }        
		/// <summary>
		/// sfile
        /// </summary>		
		private string _sfile;
        public string sfile
        {
            get{ return _sfile; }
            set{ _sfile = value; }
        }        
		/// <summary>
		/// UpLoadDate
        /// </summary>		
		private DateTime _uploaddate;
        public DateTime UpLoadDate
        {
            get{ return _uploaddate; }
            set{ _uploaddate = value; }
        }        
		/// <summary>
		/// DownLoadDate
        /// </summary>		
		private DateTime _downloaddate;
        public DateTime DownLoadDate
        {
            get{ return _downloaddate; }
            set{ _downloaddate = value; }
        }        
		/// <summary>
		/// UploadFlag
        /// </summary>		
		private int _uploadflag;
        public int UploadFlag
        {
            get{ return _uploadflag; }
            set{ _uploadflag = value; }
        }        
		/// <summary>
		/// DownLoadFlag
        /// </summary>		
		private int _downloadflag;
        public int DownLoadFlag
        {
            get{ return _downloadflag; }
            set{ _downloadflag = value; }
        }        
		/// <summary>
		/// InOutFlag
        /// </summary>		
		private int _inoutflag;
        public int InOutFlag
        {
            get{ return _inoutflag; }
            set{ _inoutflag = value; }
        }        
		/// <summary>
		/// MainFlag
        /// </summary>		
		private int _mainflag;
        public int MainFlag
        {
            get{ return _mainflag; }
            set{ _mainflag = value; }
        }        
		/// <summary>
		/// DetailFile
        /// </summary>		
		private string _detailfile;
        public string DetailFile
        {
            get{ return _detailfile; }
            set{ _detailfile = value; }
        }        
		/// <summary>
		/// Mainseqno
        /// </summary>		
		private int _mainseqno;
        public int Mainseqno
        {
            get{ return _mainseqno; }
            set{ _mainseqno = value; }
        }        
		/// <summary>
		/// DetailUploadflag
        /// </summary>		
		private int _detailuploadflag;
        public int DetailUploadflag
        {
            get{ return _detailuploadflag; }
            set{ _detailuploadflag = value; }
        }        
		/// <summary>
		/// DetailDownloadFlag
        /// </summary>		
		private int _detaildownloadflag;
        public int DetailDownloadFlag
        {
            get{ return _detaildownloadflag; }
            set{ _detaildownloadflag = value; }
        }        
		/// <summary>
		/// DCFLAG
        /// </summary>		
		private string _dcflag;
        public string DCFLAG
        {
            get{ return _dcflag; }
            set{ _dcflag = value; }
        }        
		/// <summary>
		/// AccCode
        /// </summary>		
		private string _acccode;
        public string AccCode
        {
            get{ return _acccode; }
            set{ _acccode = value; }
        }        
		/// <summary>
		/// Succflag
        /// </summary>		
		private int _succflag;
        public int Succflag
        {
            get{ return _succflag; }
            set{ _succflag = value; }
        }        
		/// <summary>
		/// DetailUploadDate
        /// </summary>		
		private DateTime _detailuploaddate;
        public DateTime DetailUploadDate
        {
            get{ return _detailuploaddate; }
            set{ _detailuploaddate = value; }
        }        
		/// <summary>
		/// DetailDownLoadDate
        /// </summary>		
		private DateTime _detaildownloaddate;
        public DateTime DetailDownLoadDate
        {
            get{ return _detaildownloaddate; }
            set{ _detaildownloaddate = value; }
        }        
		/// <summary>
		/// modecode
        /// </summary>		
		private string _modecode;
        public string modecode
        {
            get{ return _modecode; }
            set{ _modecode = value; }
        }        
		/// <summary>
		/// datadate
        /// </summary>		
		private DateTime _datadate;
        public DateTime datadate
        {
            get{ return _datadate; }
            set{ _datadate = value; }
        }        
		/// <summary>
		/// resetflag
        /// </summary>		
		private int _resetflag;
        public int resetflag
        {
            get{ return _resetflag; }
            set{ _resetflag = value; }
        }        
		/// <summary>
		/// feecode
        /// </summary>		
		private string _feecode;
        public string feecode
        {
            get{ return _feecode; }
            set{ _feecode = value; }
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
		/// <summary>
		/// crtdate
        /// </summary>		
		private DateTime _crtdate;
        public DateTime crtdate
        {
            get{ return _crtdate; }
            set{ _crtdate = value; }
        }        
		/// <summary>
		/// typeflag
        /// </summary>		
		private int _typeflag;
        public int typeflag
        {
            get{ return _typeflag; }
            set{ _typeflag = value; }
        }        
		/// <summary>
		/// subTypeFlag
        /// </summary>		
		private int _subtypeflag;
        public int subTypeFlag
        {
            get{ return _subtypeflag; }
            set{ _subtypeflag = value; }
        }        
		/// <summary>
		/// balflag
        /// </summary>		
		private int _balflag;
        public int balflag
        {
            get{ return _balflag; }
            set{ _balflag = value; }
        }        
		/// <summary>
		/// rseqno
        /// </summary>		
		private int _rseqno;
        public int rseqno
        {
            get{ return _rseqno; }
            set{ _rseqno = value; }
        }        
		   
	}
}

