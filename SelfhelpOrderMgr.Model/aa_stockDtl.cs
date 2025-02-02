﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.Model{
	 	//aa_stockDtl
		public class aa_stockDtl
	{
   		     
      	/// <summary>
		/// seqid
        /// </summary>		
		private int _seqid;
        public int seqid
        {
            get{ return _seqid; }
            set{ _seqid = value; }
        }        
		/// <summary>
		/// StockId
        /// </summary>		
		private string _stockid;
        public string StockId
        {
            get{ return _stockid; }
            set{ _stockid = value; }
        }        
		/// <summary>
		/// GCODE
        /// </summary>		
		private string _gcode;
        public string GCODE
        {
            get{ return _gcode; }
            set{ _gcode = value; }
        }        
		/// <summary>
		/// GTXM
        /// </summary>		
		private string _gtxm;
        public string GTXM
        {
            get{ return _gtxm; }
            set{ _gtxm = value; }
        }        
		/// <summary>
		/// GCOUNT
        /// </summary>		
		private decimal _gcount;
        public decimal GCOUNT
        {
            get{ return _gcount; }
            set{ _gcount = value; }
        }        
		/// <summary>
		/// GDJ
        /// </summary>		
		private decimal _gdj;
        public decimal GDJ
        {
            get{ return _gdj; }
            set{ _gdj = value; }
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
		/// stockflag
        /// </summary>		
		private int _stockflag;
        public int stockflag
        {
            get{ return _stockflag; }
            set{ _stockflag = value; }
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
		   
	}
}

