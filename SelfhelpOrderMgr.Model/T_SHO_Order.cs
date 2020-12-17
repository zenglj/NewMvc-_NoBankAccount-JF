using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_SHO_Order
    public class T_SHO_Order
    {

        /// <summary>
        /// OrderID
        /// </summary>		
        private int _orderid;
        public int OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
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
        /// FCrimecode
        /// </summary>		
        private string _fcrimecode;
        public string FCrimecode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
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
        /// CrtDate
        /// </summary>		
        private DateTime _crtdate;
        public DateTime CrtDate
        {
            get { return _crtdate; }
            set { _crtdate = value; }
        }
        /// <summary>
        /// FAmount
        /// </summary>		
        private decimal _famount;
        public decimal FAmount
        {
            get { return _famount; }
            set { _famount = value; }
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
        /// InvoiceNo
        /// </summary>		
        private string _invoiceno;
        public string InvoiceNo
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        /// <summary>
        /// IPAddr
        /// </summary>		
        private string _ipaddr;
        public string IPAddr
        {
            get { return _ipaddr; }
            set { _ipaddr = value; }
        }
        /// <summary>
        /// FreeAmount
        /// </summary>		
        private decimal _freeamount;
        public decimal FreeAmount
        {
            get { return _freeamount; }
            set { _freeamount = value; }
        }
        /// <summary>
        /// RoomNO
        /// </summary>		
        private string _roomno;
        public string RoomNO
        {
            get { return _roomno; }
            set { _roomno = value; }
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
        /// FTZSP_Money
        /// </summary>		
        private decimal _ftzsp_money;
        public decimal FTZSP_Money
        {
            get { return _ftzsp_money; }
            set { _ftzsp_money = value; }
        }

    }
}

