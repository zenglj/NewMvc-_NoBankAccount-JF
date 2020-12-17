using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //t_bank_RcvList
    public class t_bank_RcvList
    {

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
        /// AccNo
        /// </summary>		
        private string _accno;
        public string AccNo
        {
            get { return _accno; }
            set { _accno = value; }
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
        /// FName
        /// </summary>		
        private string _fname;
        public string FName
        {
            get { return _fname; }
            set { _fname = value; }
        }
        /// <summary>
        /// Amount
        /// </summary>		
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// SuccFlag
        /// </summary>		
        private int _succflag;
        public int SuccFlag
        {
            get { return _succflag; }
            set { _succflag = value; }
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
        /// LoadDate
        /// </summary>		
        private DateTime _loaddate;
        public DateTime LoadDate
        {
            get { return _loaddate; }
            set { _loaddate = value; }
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
        /// BankAccNo
        /// </summary>		
        private string _bankaccno;
        public string BankAccNo
        {
            get { return _bankaccno; }
            set { _bankaccno = value; }
        }
        /// <summary>
        /// BankRcvDate
        /// </summary>		
        private DateTime _bankrcvdate;
        public DateTime BankRcvDate
        {
            get { return _bankrcvdate; }
            set { _bankrcvdate = value; }
        }

    }
}

