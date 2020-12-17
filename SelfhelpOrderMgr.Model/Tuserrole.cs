using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//Tuserrole
		public class Tuserrole
	{
   		     
      	/// <summary>
		/// Fncode
        /// </summary>		
		private string _fncode;
        public string Fncode
        {
            get{ return _fncode; }
            set{ _fncode = value; }
        }        
		/// <summary>
		/// Frole
        /// </summary>		
		private string _frole;
        public string Frole
        {
            get{ return _frole; }
            set{ _frole = value; }
        }        
		/// <summary>
		/// Fnpriv
        /// </summary>		
		private int _fnpriv;
        public int Fnpriv
        {
            get{ return _fnpriv; }
            set{ _fnpriv = value; }
        }        
		   
	}
}

