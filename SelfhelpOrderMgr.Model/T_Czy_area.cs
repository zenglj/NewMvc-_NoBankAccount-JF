using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//T_Czy_area
		public class T_Czy_area
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
		/// fareacode
        /// </summary>		
		private string _fareacode;
        public string fareacode
        {
            get{ return _fareacode; }
            set{ _fareacode = value; }
        }        
		/// <summary>
		/// fflag
        /// </summary>		
		private int _fflag;
        public int fflag
        {
            get{ return _fflag; }
            set{ _fflag = value; }
        }

        /// <summary>
        /// fflag
        /// </summary>		
        private T_AREA _tarea;
        public T_AREA TArea
        {
            get { return _tarea; }
            set { _tarea = value; }
        }        
		   
	}
}

