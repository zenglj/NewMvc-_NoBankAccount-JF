using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Criminal_ChangeList
    public class T_Criminal_ChangeList
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
        /// FCode
        /// </summary>		
        private string _fcode;
        public string FCode
        {
            get { return _fcode; }
            set { _fcode = value; }
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
        /// OldCode
        /// </summary>		
        private string _oldcode;
        public string OldCode
        {
            get { return _oldcode; }
            set { _oldcode = value; }
        }
        /// <summary>
        /// OldName
        /// </summary>		
        private string _oldname;
        public string OldName
        {
            get { return _oldname; }
            set { _oldname = value; }
        }
        /// <summary>
        /// NewCode
        /// </summary>		
        private string _newcode;
        public string NewCode
        {
            get { return _newcode; }
            set { _newcode = value; }
        }
        /// <summary>
        /// NewName
        /// </summary>		
        private string _newname;
        public string NewName
        {
            get { return _newname; }
            set { _newname = value; }
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

