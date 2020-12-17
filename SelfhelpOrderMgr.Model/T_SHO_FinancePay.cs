using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_SHO_FinancePay
    public class T_SHO_FinancePay
    {

        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// FType
        /// </summary>		
        private string _ftype;
        public string FType
        {
            get { return _ftype; }
            set { _ftype = value; }
        }
        /// <summary>
        /// FTitle
        /// </summary>		
        private string _ftitle;
        public string FTitle
        {
            get { return _ftitle; }
            set { _ftitle = value; }
        }
        /// <summary>
        /// FCount
        /// </summary>		
        private int _fcount;
        public int FCount
        {
            get { return _fcount; }
            set { _fcount = value; }
        }
        /// <summary>
        /// FMoney
        /// </summary>		
        private decimal _fmoney;
        public decimal FMoney
        {
            get { return _fmoney; }
            set { _fmoney = value; }
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
        /// CrtDt
        /// </summary>		
        private DateTime _crtdt;
        public DateTime CrtDt
        {
            get { return _crtdt; }
            set { _crtdt = value; }
        }
        /// <summary>
        /// PosName
        /// </summary>		
        private string _posname;
        public string PosName
        {
            get { return _posname; }
            set { _posname = value; }
        }
        /// <summary>
        /// BankCard
        /// </summary>		
        private string _bankcard;
        public string BankCard
        {
            get { return _bankcard; }
            set { _bankcard = value; }
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
        /// PayBy
        /// </summary>		
        private string _payby;
        public string PayBy
        {
            get { return _payby; }
            set { _payby = value; }
        }
        /// <summary>
        /// PayDate
        /// </summary>		
        private DateTime _paydate;
        public DateTime PayDate
        {
            get { return _paydate; }
            set { _paydate = value; }
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

