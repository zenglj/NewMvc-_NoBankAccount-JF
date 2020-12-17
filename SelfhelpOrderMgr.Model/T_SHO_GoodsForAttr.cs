using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_SHO_GoodsForAttr
		public partial  class T_SHO_GoodsForAttr
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
		/// GCode
        /// </summary>		
		private string _gcode;
        public string GCode
        {
            get{ return _gcode; }
            set{ _gcode = value; }
        }        
		/// <summary>
		/// GoodsAttrId
        /// </summary>		
		private int _goodsattrid;
        public int GoodsAttrId
        {
            get{ return _goodsattrid; }
            set{ _goodsattrid = value; }
        }        
		/// <summary>
		/// AttrInfo
        /// </summary>		
		private string _attrinfo;
        public string AttrInfo
        {
            get{ return _attrinfo; }
            set{ _attrinfo = value; }
        }        
		   
	}
}

