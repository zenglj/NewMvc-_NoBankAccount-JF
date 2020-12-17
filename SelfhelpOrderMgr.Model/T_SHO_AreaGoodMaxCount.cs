using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SHO_AreaGoodMaxCount
		public class T_SHO_AreaGoodMaxCount
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
		/// FAreaCode
        /// </summary>		
		private string _fareacode;
        public string FAreaCode
        {
            get{ return _fareacode; }
            set{ _fareacode = value; }
        }        
		/// <summary>
		/// FAreaName
        /// </summary>		
		private string _fareaname;
        public string FAreaName
        {
            get{ return _fareaname; }
            set{ _fareaname = value; }
        }        
		/// <summary>
		/// FGtxm
        /// </summary>		
		private string _fgtxm;
        public string FGtxm
        {
            get{ return _fgtxm; }
            set{ _fgtxm = value; }
        }        
		/// <summary>
		/// FGoodName
        /// </summary>		
		private string _fgoodname;
        public string FGoodName
        {
            get{ return _fgoodname; }
            set{ _fgoodname = value; }
        }        
		/// <summary>
		/// FGoodType
        /// </summary>		
		private string _fgoodtype;
        public string FGoodType
        {
            get{ return _fgoodtype; }
            set{ _fgoodtype = value; }
        }        
		/// <summary>
		/// FGoodMaxCount
        /// </summary>		
		private int _fgoodmaxcount;
        public int FGoodMaxCount
        {
            get{ return _fgoodmaxcount; }
            set{ _fgoodmaxcount = value; }
        }        
		   
	}
}

