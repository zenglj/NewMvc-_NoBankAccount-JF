using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //t_balanceList
    public class t_balanceList
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
        /// fcrimecode
        /// </summary>		
        private string _fcrimecode;
        public string fcrimecode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
        }
        /// <summary>
        /// vounoa
        /// </summary>		
        private string _vounoa;
        public string vounoa
        {
            get { return _vounoa; }
            set { _vounoa = value; }
        }
        /// <summary>
        /// cardcodea
        /// </summary>		
        private string _cardcodea;
        public string cardcodea
        {
            get { return _cardcodea; }
            set { _cardcodea = value; }
        }
        /// <summary>
        /// typeflagA
        /// </summary>		
        private int _typeflaga;
        public int typeflagA
        {
            get { return _typeflaga; }
            set { _typeflaga = value; }
        }
        /// <summary>
        /// AmountA
        /// </summary>		
        private decimal _amounta;
        public decimal AmountA
        {
            get { return _amounta; }
            set { _amounta = value; }
        }
        /// <summary>
        /// vounob
        /// </summary>		
        private string _vounob;
        public string vounob
        {
            get { return _vounob; }
            set { _vounob = value; }
        }
        /// <summary>
        /// cardcodeB
        /// </summary>		
        private string _cardcodeb;
        public string cardcodeB
        {
            get { return _cardcodeb; }
            set { _cardcodeb = value; }
        }
        /// <summary>
        /// typeflagB
        /// </summary>		
        private int _typeflagb;
        public int typeflagB
        {
            get { return _typeflagb; }
            set { _typeflagb = value; }
        }
        /// <summary>
        /// AmountB
        /// </summary>		
        private decimal _amountb;
        public decimal AmountB
        {
            get { return _amountb; }
            set { _amountb = value; }
        }
        /// <summary>
        /// crtdate
        /// </summary>		
        private DateTime _crtdate;
        public DateTime crtdate
        {
            get { return _crtdate; }
            set { _crtdate = value; }
        }
        /// <summary>
        /// crtby
        /// </summary>		
        private string _crtby;
        public string crtby
        {
            get { return _crtby; }
            set { _crtby = value; }
        }
        /// <summary>
        /// remark
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// baltype
        /// </summary>		
        private int _baltype;
        public int baltype
        {
            get { return _baltype; }
            set { _baltype = value; }
        }
        /// <summary>
        /// DEPOSITER
        /// </summary>		
        private string _depositer;
        public string DEPOSITER
        {
            get { return _depositer; }
            set { _depositer = value; }
        }
        /// <summary>
        /// AmountC
        /// </summary>		
        private decimal _amountc;
        public decimal AmountC
        {
            get { return _amountc; }
            set { _amountc = value; }
        }
        /// <summary>
        /// bankamount
        /// </summary>		
        private decimal _bankamount;
        public decimal bankamount
        {
            get { return _bankamount; }
            set { _bankamount = value; }
        }
        /// <summary>
        /// PayMode
        /// </summary>		
        private int _paymode;
        public int PayMode
        {
            get { return _paymode; }
            set { _paymode = value; }
        }

    }
}

