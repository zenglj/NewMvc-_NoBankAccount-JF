using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Goods_ChangeList
    public class T_Goods_ChangeList
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
        /// GCode
        /// </summary>		
        private string _gcode;
        public string GCode
        {
            get { return _gcode; }
            set { _gcode = value; }
        }
        /// <summary>
        /// GName
        /// </summary>		
        private string _gname;
        public string GName
        {
            get { return _gname; }
            set { _gname = value; }
        }
        /// <summary>
        /// GTXM
        /// </summary>		
        private string _gtxm;
        public string GTXM
        {
            get { return _gtxm; }
            set { _gtxm = value; }
        }
        /// <summary>
        /// ChangeType
        /// </summary>		
        private string _changetype;
        public string ChangeType
        {
            get { return _changetype; }
            set { _changetype = value; }
        }
        /// <summary>
        /// ChangeTypeName
        /// </summary>		
        private string _changetypename;
        public string ChangeTypeName
        {
            get { return _changetypename; }
            set { _changetypename = value; }
        }
        /// <summary>
        /// OldPrice
        /// </summary>		
        private decimal _oldprice;
        public decimal OldPrice
        {
            get { return _oldprice; }
            set { _oldprice = value; }
        }
        /// <summary>
        /// NewPrice
        /// </summary>		
        private decimal _newprice;
        public decimal NewPrice
        {
            get { return _newprice; }
            set { _newprice = value; }
        }
        /// <summary>
        /// ChangeInfo
        /// </summary>		
        private string _changeinfo;
        public string ChangeInfo
        {
            get { return _changeinfo; }
            set { _changeinfo = value; }
        }
        /// <summary>
        /// CrtBy
        /// </summary>		
        private string _crtby;
        public string CrtBy
        {
            get { return _crtby; }
            set { _crtby = value; }
        }
        /// <summary>
        /// CrtDate
        /// </summary>		
        private DateTime _crtdate;
        public DateTime CrtDate
        {
            get { return _crtdate; }
            set { _crtdate = value; }
        }
        /// <summary>
        /// AuditBy
        /// </summary>		
        private string _auditby;
        public string AuditBy
        {
            get { return _auditby; }
            set { _auditby = value; }
        }
        /// <summary>
        /// AuditArea
        /// </summary>		
        private string _auditarea;
        public string AuditArea
        {
            get { return _auditarea; }
            set { _auditarea = value; }
        }
        /// <summary>
        /// AuditDate
        /// </summary>		
        private DateTime _auditdate;
        public DateTime AuditDate
        {
            get { return _auditdate; }
            set { _auditdate = value; }
        }
        /// <summary>
        /// AuditInfo
        /// </summary>		
        private string _auditinfo;
        public string AuditInfo
        {
            get { return _auditinfo; }
            set { _auditinfo = value; }
        }
        /// <summary>
        /// AuditFlag
        /// </summary>		
        private int _auditflag;
        public int AuditFlag
        {
            get { return _auditflag; }
            set { _auditflag = value; }
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

