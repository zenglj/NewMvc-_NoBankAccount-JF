using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Goods_combine
		public class T_Goods_combine
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
		/// ParGcode
        /// </summary>		
		private string _pargcode;
        public string ParGcode
        {
            get{ return _pargcode; }
            set{ _pargcode = value; }
        }        
		/// <summary>
		/// ParGTXM
        /// </summary>		
		private string _pargtxm;
        public string ParGTXM
        {
            get{ return _pargtxm; }
            set{ _pargtxm = value; }
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
		/// SubGTXM
        /// </summary>		
		private string _subgtxm;
        public string SubGTXM
        {
            get{ return _subgtxm; }
            set{ _subgtxm = value; }
        }        
		/// <summary>
		/// Qty
        /// </summary>		
		private decimal _qty;
        public decimal Qty
        {
            get{ return _qty; }
            set{ _qty = value; }
        }        
		   
	}
}

