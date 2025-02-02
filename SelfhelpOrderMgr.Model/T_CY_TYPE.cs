﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_CY_TYPE
    public class T_CY_TYPE
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
        /// FDesc
        /// </summary>		
        private string _fdesc;
        public string FDesc
        {
            get { return _fdesc; }
            set { _fdesc = value; }
        }
        /// <summary>
        /// famtmonth
        /// </summary>		
        private decimal _famtmonth;
        public decimal famtmonth
        {
            get { return _famtmonth; }
            set { _famtmonth = value; }
        }
        /// <summary>
        /// FamtLimit
        /// </summary>		
        private decimal _famtlimit;
        public decimal FamtLimit
        {
            get { return _famtlimit; }
            set { _famtlimit = value; }
        }
        /// <summary>
        /// fcamtlimit
        /// </summary>		
        private decimal _fcamtlimit;
        public decimal fcamtlimit
        {
            get { return _fcamtlimit; }
            set { _fcamtlimit = value; }
        }
        /// <summary>
        /// flag
        /// </summary>		
        private int _flag;
        public int flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        /// <summary>
        /// FLimittype
        /// </summary>		
        private int _flimittype;
        public int FLimittype
        {
            get { return _flimittype; }
            set { _flimittype = value; }
        }
        /// <summary>
        /// pct
        /// </summary>		
        private decimal _pct;
        public decimal pct
        {
            get { return _pct; }
            set { _pct = value; }
        }
        /// <summary>
        /// FdayLimitflag
        /// </summary>		
        private int _fdaylimitflag;
        public int FdayLimitflag
        {
            get { return _fdaylimitflag; }
            set { _fdaylimitflag = value; }
        }
        /// <summary>
        /// FdaylimitAmt
        /// </summary>		
        private decimal _fdaylimitamt;
        public decimal FdaylimitAmt
        {
            get { return _fdaylimitamt; }
            set { _fdaylimitamt = value; }
        }
        /// <summary>
        /// FBamtMonth
        /// </summary>		
        private decimal _fbamtmonth;
        public decimal FBamtMonth
        {
            get { return _fbamtmonth; }
            set { _fbamtmonth = value; }
        }
        /// <summary>
        /// FbamtmonthFlag
        /// </summary>		
        private int _fbamtmonthflag;
        public int FbamtmonthFlag
        {
            get { return _fbamtmonthflag; }
            set { _fbamtmonthflag = value; }
        }
        /// <summary>
        /// FAamtmonthflag
        /// </summary>		
        private int _faamtmonthflag;
        public int FAamtmonthflag
        {
            get { return _faamtmonthflag; }
            set { _faamtmonthflag = value; }
        }
        /// <summary>
        /// bpct
        /// </summary>		
        private decimal _bpct;
        public decimal bpct
        {
            get { return _bpct; }
            set { _bpct = value; }
        }
        /// <summary>
        /// Fbonusflag
        /// </summary>		
        private int _fbonusflag;
        public int Fbonusflag
        {
            get { return _fbonusflag; }
            set { _fbonusflag = value; }
        }
        /// <summary>
        /// cpct
        /// </summary>		
        private decimal _cpct;
        public decimal cpct
        {
            get { return _cpct; }
            set { _cpct = value; }
        }
        /// <summary>
        /// ftotamtmonthflag
        /// </summary>		
        private int _ftotamtmonthflag;
        public int ftotamtmonthflag
        {
            get { return _ftotamtmonthflag; }
            set { _ftotamtmonthflag = value; }
        }
        /// <summary>
        /// ftotamtmonth
        /// </summary>		
        private decimal _ftotamtmonth;
        public decimal ftotamtmonth
        {
            get { return _ftotamtmonth; }
            set { _ftotamtmonth = value; }
        }
        /// <summary>
        /// totpct
        /// </summary>		
        private decimal _totpct;
        public decimal totpct
        {
            get { return _totpct; }
            set { _totpct = value; }
        }
        /// <summary>
        /// FPower
        /// </summary>		
        private string _fpower;
        public string FPower
        {
            get { return _fpower; }
            set { _fpower = value; }
        }
        /// <summary>
        /// FDinnerAFlag
        /// </summary>		
        private int _fdinneraflag;
        public int FDinnerAFlag
        {
            get { return _fdinneraflag; }
            set { _fdinneraflag = value; }
        }
        /// <summary>
        /// FDinnerBFlag
        /// </summary>		
        private int _fdinnerbflag;
        public int FDinnerBFlag
        {
            get { return _fdinnerbflag; }
            set { _fdinnerbflag = value; }
        }
        /// <summary>
        /// payaccount
        /// </summary>		
        private int _payaccount;
        public int payaccount
        {
            get { return _payaccount; }
            set { _payaccount = value; }
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
        /// <summary>
        /// FTZSP_Zero_Flag
        /// </summary>		
        private int _ftzsp_zero_flag;
        public int FTZSP_Zero_Flag
        {
            get { return _ftzsp_zero_flag; }
            set { _ftzsp_zero_flag = value; }
        }
        /// <summary>
        /// JaRi_Cy_Money
        /// </summary>		
        private decimal _jari_cy_money;
        public decimal JaRi_Cy_Money
        {
            get { return _jari_cy_money; }
            set { _jari_cy_money = value; }
        }

        public decimal JaRi_Cy_FTZSP_Money { get; set; }//节日非劳食品可用金额
        public decimal FTZSP_Zero_MaxMoney { get; set; }//归零后，最高可用食品的金额
    }
}

