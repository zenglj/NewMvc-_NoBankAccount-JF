﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_StockDTL
    public class T_StockDTL
    {

        /// <summary>
        /// SeqId
        /// </summary>		
        private int _seqid;
        public int SeqId
        {
            get { return _seqid; }
            set { _seqid = value; }
        }
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
        /// GCount
        /// </summary>		
        private decimal _gcount;
        public decimal GCount
        {
            get { return _gcount; }
            set { _gcount = value; }
        }
        /// <summary>
        /// GDJ
        /// </summary>		
        private decimal _gdj;
        public decimal GDJ
        {
            get { return _gdj; }
            set { _gdj = value; }
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
        /// StockFlag
        /// </summary>		
        private int _stockflag;
        public int StockFlag
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
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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

