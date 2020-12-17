using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_SHO_SaleDayList
    public class T_SHO_SaleDayList
    {

        /// <summary>
        /// Seqno
        /// </summary>		
        private int _seqno;
        public int Seqno
        {
            get { return _seqno; }
            set { _seqno = value; }
        }
        /// <summary>
        /// SaleTypeId
        /// </summary>		
        private int _saletypeid;
        public int SaleTypeId
        {
            get { return _saletypeid; }
            set { _saletypeid = value; }
        }
        /// <summary>
        /// PType
        /// </summary>		
        private string _ptype;
        public string PType
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        /// <summary>
        /// StartDay
        /// </summary>		
        private string _startday;
        public string StartDay
        {
            get { return _startday; }
            set { _startday = value; }
        }
        /// <summary>
        /// EndDay
        /// </summary>		
        private string _endday;
        public string EndDay
        {
            get { return _endday; }
            set { _endday = value; }
        }
        /// <summary>
        /// Flag
        /// </summary>		
        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
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
        /// <summary>
        /// FAreaCode
        /// </summary>		
        private string _fareacode;
        public string FAreaCode
        {
            get { return _fareacode; }
            set { _fareacode = value; }
        }
        /// <summary>
        /// LevelId
        /// </summary>		
        private int _levelid;
        public int LevelId
        {
            get { return _levelid; }
            set { _levelid = value; }
        }

    }
}

