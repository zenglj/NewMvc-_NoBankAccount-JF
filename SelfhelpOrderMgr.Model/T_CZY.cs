using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_CZY
    public partial class T_CZY:BaseModel
    {

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
        /// FPwd
        /// </summary>		
        private string _fpwd;
        public string FPwd
        {
            get { return _fpwd; }
            set { _fpwd = value; }
        }
        /// <summary>
        /// FFlag
        /// </summary>		
        private int _fflag;
        public int FFlag
        {
            get { return _fflag; }
            set { _fflag = value; }
        }
        /// <summary>
        /// FPRIVATE
        /// </summary>		
        private int _fprivate;
        public int FPRIVATE
        {
            get { return _fprivate; }
            set { _fprivate = value; }
        }
        /// <summary>
        /// FSTOCKCHK
        /// </summary>		
        private int _fstockchk;
        public int FSTOCKCHK
        {
            get { return _fstockchk; }
            set { _fstockchk = value; }
        }
        /// <summary>
        /// FINVCHK
        /// </summary>		
        private int _finvchk;
        public int FINVCHK
        {
            get { return _finvchk; }
            set { _finvchk = value; }
        }
        /// <summary>
        /// rolecode
        /// </summary>		
        private string _rolecode;
        public string rolecode
        {
            get { return _rolecode; }
            set { _rolecode = value; }
        }
        /// <summary>
        /// FUserArea
        /// </summary>		
        private string _fuserarea;
        public string FUserArea
        {
            get { return _fuserarea; }
            set { _fuserarea = value; }
        }
        /// <summary>
        /// FRole
        /// </summary>		
        private string _frole;
        public string FRole
        {
            get { return _frole; }
            set { _frole = value; }
        }
        /// <summary>
        /// fauditflag
        /// </summary>		
        private int _fauditflag;
        public int fauditflag
        {
            get { return _fauditflag; }
            set { _fauditflag = value; }
        }
        /// <summary>
        /// fBonusPost
        /// </summary>		
        private int _fbonuspost;
        public int fBonusPost
        {
            get { return _fbonuspost; }
            set { _fbonuspost = value; }
        }
        /// <summary>
        /// ver
        /// </summary>		
        private string _ver;
        public string ver
        {
            get { return _ver; }
            set { _ver = value; }
        }
        /// <summary>
        /// FUserChinaName
        /// </summary>		
        private string _fuserchinaname;
        public string FUserChinaName
        {
            get { return _fuserchinaname; }
            set { _fuserchinaname = value; }
        }
        /// <summary>
        /// FManagerCard
        /// </summary>		
        private string _fmanagercard;
        public string FManagerCard
        {
            get { return _fmanagercard; }
            set { _fmanagercard = value; }
        }
        //相片
        public string Photo { get; set; }
    }
}