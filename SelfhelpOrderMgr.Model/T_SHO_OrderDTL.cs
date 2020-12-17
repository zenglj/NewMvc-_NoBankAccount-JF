using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_SHO_OrderDTL
    public partial class T_SHO_OrderDTL
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
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
        /// GCode
        /// </summary>		
        private string _gcode;
        public string GCode
        {
            get { return _gcode; }
            set { _gcode = value; }
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
        /// GName
        /// </summary>		
        private string _gname;
        public string GName
        {
            get { return _gname; }
            set { _gname = value; }
        }
        /// <summary>
        /// GCount
        /// </summary>		
        private decimal _gcount;
        public decimal GCount
        {
            get { return _gcount; }
            set { _gcount = value; }
        }
        /// <summary>
        /// GPrice
        /// </summary>		
        private decimal _gprice;
        public decimal GPrice
        {
            get { return _gprice; }
            set { _gprice = value; }
        }
        /// <summary>
        /// GAmount
        /// </summary>		
        private decimal _gamount;
        public decimal GAmount
        {
            get { return _gamount; }
            set { _gamount = value; }
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
        /// FreeFlag
        /// </summary>		
        private int _freeflag;
        public int FreeFlag
        {
            get { return _freeflag; }
            set { _freeflag = value; }
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
        /// SPShortCode
        /// </summary>		
        private string _spshortcode;
        public string SPShortCode
        {
            get { return _spshortcode; }
            set { _spshortcode = value; }
        }
        /// <summary>
        /// FTZSP_TypeFlag
        /// </summary>		
        private int _ftzsp_typeflag;
        public int FTZSP_TypeFlag
        {
            get { return _ftzsp_typeflag; }
            set { _ftzsp_typeflag = value; }
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

