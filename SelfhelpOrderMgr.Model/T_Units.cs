using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Units
    public class T_Units
    {

        /// <summary>
        /// UnitID
        /// </summary>		
        private string _unitid;
        public string UnitID
        {
            get { return _unitid; }
            set { _unitid = value; }
        }
        /// <summary>
        /// UnitName
        /// </summary>		
        private string _unitname;
        public string UnitName
        {
            get { return _unitname; }
            set { _unitname = value; }
        }
        /// <summary>
        /// UnitIp
        /// </summary>		
        private string _unitip;
        public string UnitIp
        {
            get { return _unitip; }
            set { _unitip = value; }
        }
        /// <summary>
        /// UnitService
        /// </summary>		
        private string _unitservice;
        public string UnitService
        {
            get { return _unitservice; }
            set { _unitservice = value; }
        }
        /// <summary>
        /// UnitPort
        /// </summary>		
        private string _unitport;
        public string UnitPort
        {
            get { return _unitport; }
            set { _unitport = value; }
        }
        /// <summary>
        /// IsSigned
        /// </summary>		
        private int _issigned;
        public int IsSigned
        {
            get { return _issigned; }
            set { _issigned = value; }
        }
        /// <summary>
        /// Status
        /// </summary>		
        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// tab
        /// </summary>		
        private DateTime _tab;
        public DateTime tab
        {
            get { return _tab; }
            set { _tab = value; }
        }
        /// <summary>
        /// timeflag
        /// </summary>		
        private int _timeflag;
        public int timeflag
        {
            get { return _timeflag; }
            set { _timeflag = value; }
        }

    }
}

