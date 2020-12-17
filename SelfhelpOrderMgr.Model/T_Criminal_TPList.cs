using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Criminal_TPList
    public class T_Criminal_TPList
    {

        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
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
        /// TPMoney
        /// </summary>		
        private decimal _tpmoney;
        public decimal TPMoney
        {
            get { return _tpmoney; }
            set { _tpmoney = value; }
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
        /// EffectiveDate
        /// </summary>		
        private DateTime _effectivedate;
        public DateTime EffectiveDate
        {
            get { return _effectivedate; }
            set { _effectivedate = value; }
        }
        /// <summary>
        /// FifoFlag
        /// </summary>		
        private int _fifoflag;
        public int FifoFlag
        {
            get { return _fifoflag; }
            set { _fifoflag = value; }
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
        /// SrcFileName
        /// </summary>		
        private string _srcfilename;
        public string SrcFileName
        {
            get { return _srcfilename; }
            set { _srcfilename = value; }
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
        private string _deldate;
        public string DelDate
        {
            get { return _deldate; }
            set { _deldate = value; }
        }
        /// <summary>
        /// MoneyUseFlag
        /// </summary>		
        private decimal _moneyuseflag;
        public decimal MoneyUseFlag
        {
            get { return _moneyuseflag; }
            set { _moneyuseflag = value; }
        }

    }
}

