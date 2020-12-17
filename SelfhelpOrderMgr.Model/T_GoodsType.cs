using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_GoodsType
    public class T_GoodsType
    {

        /// <summary>
        /// Fcode
        /// </summary>		
        private string _fcode;
        public string Fcode
        {
            get { return _fcode; }
            set { _fcode = value; }
        }
        /// <summary>
        /// Fname
        /// </summary>		
        private string _fname;
        public string Fname
        {
            get { return _fname; }
            set { _fname = value; }
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
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// SaleTypeId
        /// </summary>		
        private int _saletypeid;
        public int SaleTypeId
        {
            get { return _saletypeid; }
            set { _saletypeid = value; }
        }
        /// <summary>
        /// LevelNo
        /// </summary>		
        private int _levelno;
        public int LevelNo
        {
            get { return _levelno; }
            set { _levelno = value; }
        }
        /// <summary>
        /// FTypeCode
        /// </summary>		
        private string _ftypecode;
        public string FTypeCode
        {
            get { return _ftypecode; }
            set { _ftypecode = value; }
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
        /// MaxBuyCount
        /// </summary>		
        private int _maxbuycount;
        public int MaxBuyCount
        {
            get { return _maxbuycount; }
            set { _maxbuycount = value; }
        }
        /// <summary>
        /// CtrlMode
        /// </summary>		
        private int _ctrlmode;
        public int CtrlMode
        {
            get { return _ctrlmode; }
            set { _ctrlmode = value; }
        }

    }
}

