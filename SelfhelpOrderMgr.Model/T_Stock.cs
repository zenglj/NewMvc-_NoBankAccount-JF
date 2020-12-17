using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Stock
    public class T_Stock
    {

        /// <summary>
        /// StockId
        /// </summary>		
        private string _stockid;
        public string StockId
        {
            get { return _stockid; }
            set { _stockid = value; }
        }
        /// <summary>
        /// InOutDate
        /// </summary>		
        private DateTime _inoutdate;
        public DateTime InOutDate
        {
            get { return _inoutdate; }
            set { _inoutdate = value; }
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
        /// StockType
        /// </summary>		
        private string _stocktype;
        public string StockType
        {
            get { return _stocktype; }
            set { _stocktype = value; }
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
        /// CheckFlag
        /// </summary>		
        private int _checkflag;
        public int CheckFlag
        {
            get { return _checkflag; }
            set { _checkflag = value; }
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
        /// CheckDt
        /// </summary>		
        private DateTime _checkdt;
        public DateTime CheckDt
        {
            get { return _checkdt; }
            set { _checkdt = value; }
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
        /// InvoiceNo
        /// </summary>		
        private string _invoiceno;
        public string InvoiceNo
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        /// <summary>
        /// Stockflag
        /// </summary>		
        private int _stockflag;
        public int Stockflag
        {
            get { return _stockflag; }
            set { _stockflag = value; }
        }
        /// <summary>
        /// InOutFlag
        /// </summary>		
        private int _inoutflag;
        public int InOutFlag
        {
            get { return _inoutflag; }
            set { _inoutflag = value; }
        }
        /// <summary>
        /// WareHouseCode
        /// </summary>		
        private string _warehousecode;
        public string WareHouseCode
        {
            get { return _warehousecode; }
            set { _warehousecode = value; }
        }

    }
}

