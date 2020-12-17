using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Czy_priv
		public class T_Czy_priv
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
		/// fcode
        /// </summary>		
		private string _fcode;
        public string fcode
        {
            get{ return _fcode; }
            set{ _fcode = value; }
        }        
		/// <summary>
		/// item01
        /// </summary>		
		private int _item01;
        public int item01
        {
            get{ return _item01; }
            set{ _item01 = value; }
        }        
		/// <summary>
		/// item02
        /// </summary>		
		private int _item02;
        public int item02
        {
            get{ return _item02; }
            set{ _item02 = value; }
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
		/// itemname
        /// </summary>		
		private string _itemname;
        public string itemname
        {
            get{ return _itemname; }
            set{ _itemname = value; }
        }        
		   
	}
}

