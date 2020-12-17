using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Vcrd
    public partial class T_Vcrd:BaseModel
    {

        /// <summary>
        /// Vouno
        /// </summary>		
        private string _vouno;
        public string Vouno
        {
            get { return _vouno; }
            set { _vouno = value; }
        }
        /// <summary>
        /// CardCode
        /// </summary>		
        private string _cardcode;
        public string CardCode
        {
            get { return _cardcode; }
            set { _cardcode = value; }
        }
        /// <summary>
        /// FCrimeCode
        /// </summary>		
        private string _fcrimecode;
        public string FCrimeCode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
        }
        /// <summary>
        /// DAmount
        /// </summary>		
        private decimal _damount;
        public decimal DAmount
        {
            get { return _damount; }
            set { _damount = value; }
        }
        /// <summary>
        /// CAmount
        /// </summary>		
        private decimal _camount;
        public decimal CAmount
        {
            get { return _camount; }
            set { _camount = value; }
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
        /// DType
        /// </summary>		
        private string _dtype;
        public string DType
        {
            get { return _dtype; }
            set { _dtype = value; }
        }
        /// <summary>
        /// Depositer
        /// </summary>		
        private string _depositer;
        public string Depositer
        {
            get { return _depositer; }
            set { _depositer = value; }
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
        /// Flag
        /// </summary>		
        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        /// <summary>
        /// DelBy
        /// </summary>		
        private string _delby;
        public string DelBy
        {
            get { return _delby; }
            set { _delby = value; }
        }
        /// <summary>
        /// DelDate
        /// </summary>		
        private DateTime _deldate;
        public DateTime DelDate
        {
            get { return _deldate; }
            set { _deldate = value; }
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
        /// FAreaName
        /// </summary>		
        private string _fareaname;
        public string FAreaName
        {
            get { return _fareaname; }
            set { _fareaname = value; }
        }
        /// <summary>
        /// FCriminal
        /// </summary>		
        private string _fcriminal;
        public string FCriminal
        {
            get { return _fcriminal; }
            set { _fcriminal = value; }
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
        /// PType
        /// </summary>		
        private string _ptype;
        public string PType
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        /// <summary>
        /// UDate
        /// </summary>		
        private DateTime _udate;
        public DateTime UDate
        {
            get { return _udate; }
            set { _udate = value; }
        }
        /// <summary>
        /// OrigId
        /// </summary>		
        private string _origid;
        public string OrigId
        {
            get { return _origid; }
            set { _origid = value; }
        }
        /// <summary>
        /// CardType
        /// </summary>		
        private int _cardtype;
        public int CardType
        {
            get { return _cardtype; }
            set { _cardtype = value; }
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
        /// AccType
        /// </summary>		
        private int _acctype;
        public int AccType
        {
            get { return _acctype; }
            set { _acctype = value; }
        }
        /// <summary>
        /// SendDate
        /// </summary>		
        private DateTime _senddate;
        public DateTime SendDate
        {
            get { return _senddate; }
            set { _senddate = value; }
        }
        /// <summary>
        /// BankFlag
        /// </summary>		
        private int _bankflag;
        public int BankFlag
        {
            get { return _bankflag; }
            set { _bankflag = value; }
        }
        /// <summary>
        /// CheckFlag
        /// </summary>		
        private int _checkflag;
        public int CheckFlag
        {
            get { return _checkflag; }
            set { _checkflag = value; }
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
        /// CheckBy
        /// </summary>		
        private string _checkby;
        public string CheckBy
        {
            get { return _checkby; }
            set { _checkby = value; }
        }
        /// <summary>
        /// pc
        /// </summary>		
        private int _pc;
        public int pc
        {
            get { return _pc; }
            set { _pc = value; }
        }
        /// <summary>
        /// seqno
        /// </summary>		
        private int _seqno;
        public int seqno
        {
            get { return _seqno; }
            set { _seqno = value; }
        }
        /// <summary>
        /// SubTypeFlag
        /// </summary>		
        private int _subtypeflag;
        public int SubTypeFlag
        {
            get { return _subtypeflag; }
            set { _subtypeflag = value; }
        }
        /// <summary>
        /// RcvDate
        /// </summary>		
        private DateTime _rcvdate;
        public DateTime RcvDate
        {
            get { return _rcvdate; }
            set { _rcvdate = value; }
        }
        /// <summary>
        /// CurUserAmount
        /// </summary>		
        private decimal _curuseramount;
        public decimal CurUserAmount
        {
            get { return _curuseramount; }
            set { _curuseramount = value; }
        }
        /// <summary>
        /// CurAllAmount
        /// </summary>		
        private decimal _curallamount;
        public decimal CurAllAmount
        {
            get { return _curallamount; }
            set { _curallamount = value; }
        }
        /// <summary>
        /// bankRcvFlag
        /// </summary>		
        private int _bankrcvflag;
        public int bankRcvFlag
        {
            get { return _bankrcvflag; }
            set { _bankrcvflag = value; }
        }
        /// <summary>
        /// FinancePayId
        /// </summary>		
        private int _financepayid;
        public int FinancePayId
        {
            get { return _financepayid; }
            set { _financepayid = value; }
        }
        /// <summary>
        /// FinancePayFlag
        /// </summary>		
        private int _financepayflag;
        public int FinancePayFlag
        {
            get { return _financepayflag; }
            set { _financepayflag = value; }
        }

        /// <summary>
        /// BankInterfaceFlag
        /// </summary>		
        private int _bankinterfaceflag;
        public int BankInterfaceFlag
        {
            get { return _bankinterfaceflag; }
            set { _bankinterfaceflag = value; }
        }

        /// <summary>
        /// BankInterfaceFlag
        /// </summary>		
        private int _payauditflag;
        public int PayAuditFlag
        {
            get { return _payauditflag; }
            set { _payauditflag = value; }
        }

        
    }
}

