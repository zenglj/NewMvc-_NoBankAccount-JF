using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//Tlog
		public class Tlog
	{
   		     
      	/// <summary>
		/// Fncode
        /// </summary>		
		private int _fncode;
        public int Fncode
        {
            get{ return _fncode; }
            set{ _fncode = value; }
        }        
		/// <summary>
		/// Fname
        /// </summary>		
		private string _fname;
        public string Fname
        {
            get{ return _fname; }
            set{ _fname = value; }
        }        
		/// <summary>
		/// Floginuser
        /// </summary>		
		private string _floginuser;
        public string Floginuser
        {
            get{ return _floginuser; }
            set{ _floginuser = value; }
        }        
		/// <summary>
		/// Fsdate
        /// </summary>		
		private DateTime _fsdate;
        public DateTime Fsdate
        {
            get{ return _fsdate; }
            set{ _fsdate = value; }
        }        
		/// <summary>
		/// Fedate
        /// </summary>		
		private DateTime _fedate;
        public DateTime Fedate
        {
            get{ return _fedate; }
            set{ _fedate = value; }
        }        
		/// <summary>
		/// Fip
        /// </summary>		
		private string _fip;
        public string Fip
        {
            get{ return _fip; }
            set{ _fip = value; }
        }        
		   
	}
}

