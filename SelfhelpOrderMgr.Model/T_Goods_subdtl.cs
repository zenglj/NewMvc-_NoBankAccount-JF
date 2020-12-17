using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Goods_subdtl
		public class T_Goods_subdtl
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
		/// GCode
        /// </summary>		
		private string _gcode;
        public string GCode
        {
            get{ return _gcode; }
            set{ _gcode = value; }
        }        
		/// <summary>
		/// SubGcode
        /// </summary>		
		private string _subgcode;
        public string SubGcode
        {
            get{ return _subgcode; }
            set{ _subgcode = value; }
        }        
		/// <summary>
		/// GNum
        /// </summary>		
		private decimal _gnum;
        public decimal GNum
        {
            get{ return _gnum; }
            set{ _gnum = value; }
        }        
		   
	}
}

