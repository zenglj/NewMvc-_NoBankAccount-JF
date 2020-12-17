using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //t_XFQueryList
    public partial class t_XFQueryList
    {

        /// <summary>
        /// fcrimecode
        /// </summary>		
        private string _fcrimecode;
        public string fcrimecode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
        }
        /// <summary>
        /// fname
        /// </summary>		
        private string _fname;
        public string fname
        {
            get { return _fname; }
            set { _fname = value; }
        }
        /// <summary>
        /// CDate
        /// </summary>		
        private DateTime _cdate;
        public DateTime CDate
        {
            get { return _cdate; }
            set { _cdate = value; }
        }
        /// <summary>
        /// Cmoney
        /// </summary>		
        private decimal _cmoney;
        public decimal Cmoney
        {
            get { return _cmoney; }
            set { _cmoney = value; }
        }
        /// <summary>
        /// Dtype
        /// </summary>		
        private string _dtype;
        public string Dtype
        {
            get { return _dtype; }
            set { _dtype = value; }
        }

    }
}

