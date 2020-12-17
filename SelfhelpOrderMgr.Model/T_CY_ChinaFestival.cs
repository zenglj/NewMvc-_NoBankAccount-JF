using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_CY_ChinaFestival
    public partial class T_CY_ChinaFestival
    {

        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// FName
        /// </summary>		
        private string _fname;
        public string FName
        {
            get { return _fname; }
            set { _fname = value; }
        }
        /// <summary>
        /// FDate
        /// </summary>		
        private DateTime _fdate;
        public DateTime FDate
        {
            get { return _fdate; }
            set { _fdate = value; }
        }
        /// <summary>
        /// Festival_Name
        /// </summary>		
        private string _festival_name;
        public string Festival_Name
        {
            get { return _festival_name; }
            set { _festival_name = value; }
        }
        /// <summary>
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

    }
}

