using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_BONUS
    public class T_JF_BONUS:BaseModel
    {

        /// <summary>
        /// BID
        /// </summary>		
        private string _bid;
        public string BID
        {
            get { return _bid; }
            set { _bid = value; }
        }
        /// <summary>
        /// FAREACODE
        /// </summary>		
        private string _fareacode;
        public string FAREACODE
        {
            get { return _fareacode; }
            set { _fareacode = value; }
        }
        /// <summary>
        /// fAMOUNT
        /// </summary>		
        private decimal _famount;
        public decimal fAMOUNT
        {
            get { return _famount; }
            set { _famount = value; }
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
        /// ApplyBy
        /// </summary>		
        private string _applyby;
        public string ApplyBy
        {
            get { return _applyby; }
            set { _applyby = value; }
        }
        /// <summary>
        /// Applydt
        /// </summary>		
        private DateTime _applydt;
        public DateTime Applydt
        {
            get { return _applydt; }
            set { _applydt = value; }
        }
        /// <summary>
        /// Crtby
        /// </summary>		
        private string _crtby;
        public string Crtby
        {
            get { return _crtby; }
            set { _crtby = value; }
        }
        /// <summary>
        /// crtdt
        /// </summary>		
        private DateTime _crtdt;
        public DateTime crtdt
        {
            get { return _crtdt; }
            set { _crtdt = value; }
        }
        /// <summary>
        /// CHECKBY
        /// </summary>		
        private string _checkby;
        public string CHECKBY
        {
            get { return _checkby; }
            set { _checkby = value; }
        }
        /// <summary>
        /// CheckDate
        /// </summary>		
        private DateTime _checkdate;
        public DateTime CheckDate
        {
            get { return _checkdate; }
            set { _checkdate = value; }
        }
        /// <summary>
        /// FLAG
        /// </summary>		
        private int _flag;
        public int FLAG
        {
            get { return _flag; }
            set { _flag = value; }
        }
        /// <summary>
        /// fareaName
        /// </summary>		
        private string _fareaname;
        public string fareaName
        {
            get { return _fareaname; }
            set { _fareaname = value; }
        }
        /// <summary>
        /// Frealareacode
        /// </summary>		
        private string _frealareacode;
        public string Frealareacode
        {
            get { return _frealareacode; }
            set { _frealareacode = value; }
        }
        /// <summary>
        /// FrealAreaName
        /// </summary>		
        private string _frealareaname;
        public string FrealAreaName
        {
            get { return _frealareaname; }
            set { _frealareaname = value; }
        }
        /// <summary>
        /// udate
        /// </summary>		
        private DateTime _udate;
        public DateTime udate
        {
            get { return _udate; }
            set { _udate = value; }
        }
        /// <summary>
        /// ptype
        /// </summary>		
        private string _ptype;
        public string ptype
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        /// <summary>
        /// cnt
        /// </summary>		
        private int _cnt;
        public int cnt
        {
            get { return _cnt; }
            set { _cnt = value; }
        }
        /// <summary>
        /// auditby
        /// </summary>		
        private string _auditby;
        public string auditby
        {
            get { return _auditby; }
            set { _auditby = value; }
        }
        /// <summary>
        /// auditflag
        /// </summary>		
        private int _auditflag;
        public int auditflag
        {
            get { return _auditflag; }
            set { _auditflag = value; }
        }
        /// <summary>
        /// auditdate
        /// </summary>		
        private DateTime _auditdate;
        public DateTime auditdate
        {
            get { return _auditdate; }
            set { _auditdate = value; }
        }
        /// <summary>
        /// Fdbcheckflag
        /// </summary>		
        private int _fdbcheckflag;
        public int Fdbcheckflag
        {
            get { return _fdbcheckflag; }
            set { _fdbcheckflag = value; }
        }
        /// <summary>
        /// Fdbcheckdate
        /// </summary>		
        private DateTime _fdbcheckdate;
        public DateTime Fdbcheckdate
        {
            get { return _fdbcheckdate; }
            set { _fdbcheckdate = value; }
        }
        /// <summary>
        /// FPostBy
        /// </summary>		
        private string _fpostby;
        public string FPostBy
        {
            get { return _fpostby; }
            set { _fpostby = value; }
        }
        /// <summary>
        /// FPostDate
        /// </summary>		
        private DateTime _fpostdate;
        public DateTime FPostDate
        {
            get { return _fpostdate; }
            set { _fpostdate = value; }
        }
        /// <summary>
        /// FPostFlag
        /// </summary>		
        private int _fpostflag;
        public int FPostFlag
        {
            get { return _fpostflag; }
            set { _fpostflag = value; }
        }
        /// <summary>
        /// FDbCheckBY
        /// </summary>		
        private string _fdbcheckby;
        public string FDbCheckBY
        {
            get { return _fdbcheckby; }
            set { _fdbcheckby = value; }
        }
        /// <summary>
        /// FCheckFlag
        /// </summary>		
        private int _fcheckflag;
        public int FCheckFlag
        {
            get { return _fcheckflag; }
            set { _fcheckflag = value; }
        }
        /// <summary>
        /// TypeFlag
        /// </summary>		
        private int _typeflag;
        public int TypeFlag
        {
            get { return _typeflag; }
            set { _typeflag = value; }
        }
        /// <summary>
        /// SubTypeFlag
        /// </summary>		
        private int? _subtypeflag;
        public int? SubTypeFlag
        {
            get { return _subtypeflag; }
            set { _subtypeflag = value; }
        }
        /// <summary>
        /// DType
        /// </summary>		
        private string _dtype;
        public string DType
        {
            get { return _dtype; }
            set { _dtype = value; }
        }
        /// <summary>
        /// TargetExaminerBy
        /// </summary>		
        private string _targetexaminerby;
        public string TargetExaminerBy
        {
            get { return _targetexaminerby; }
            set { _targetexaminerby = value; }
        }
        /// <summary>
        /// MainStatus
        /// </summary>		
        private int _mainstatus;
        public int MainStatus
        {
            get { return _mainstatus; }
            set { _mainstatus = value; }
        }
        

    }
}

