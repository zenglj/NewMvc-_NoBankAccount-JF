using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_Savetype
    public class T_Savetype
    {

        /// <summary>
        /// fcode
        /// </summary>		
        private int _fcode;
        public int fcode
        {
            get { return _fcode; }
            set { _fcode = value; }
        }
        /// <summary>
        /// fname
        /// </summary>		
        private string _fname;
        public string fname
        {
            get { return _fname; }
            set { _fname = value; }
        }
        /// <summary>
        /// typeflag
        /// </summary>		
        private int _typeflag;
        public int typeflag
        {
            get { return _typeflag; }
            set { _typeflag = value; }
        }
        /// <summary>
        /// PLXE_Flag
        /// </summary>		
        private int _plxe_flag;
        public int PLXE_Flag
        {
            get { return _plxe_flag; }
            set { _plxe_flag = value; }
        }
        /// <summary>
        /// ZZKK_Flag
        /// </summary>		
        private int _zzkk_flag;
        public int ZZKK_Flag
        {
            get { return _zzkk_flag; }
            set { _zzkk_flag = value; }
        }
        /// <summary>
        /// AccType
        /// </summary>		
        private int _acctype;
        public int AccType
        {
            get { return _acctype; }
            set { _acctype = value; }
        }
        /// <summary>
        /// FuShuFlag
        /// </summary>		
        private int _fushuflag;
        public int FuShuFlag
        {
            get { return _fushuflag; }
            set { _fushuflag = value; }
        }
        //使用类型：0是普通 还是 1是积分消费
        public int UseType { get; set; }
    }
}

