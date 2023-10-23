using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_AREA
    public class T_AREA
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
        /// ID
        /// </summary>		
        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// FID
        /// </summary>		
        private string _fid;
        public string FID
        {
            get { return _fid; }
            set { _fid = value; }
        }
        /// <summary>
        /// URL
        /// </summary>		
        private string _url;
        public string URL
        {
            get { return _url; }
            set { _url = value; }
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
        /// SaleCloseFlag
        /// </summary>		
        private int _salecloseflag;
        public int SaleCloseFlag
        {
            get { return _salecloseflag; }
            set { _salecloseflag = value; }
        }


        /// <summary>
        /// JiFenCloseFlag
        /// </summary>		
        private int _jifencloseflag;
        public int JiFenCloseFlag
        {
            get { return _jifencloseflag; }
            set { _jifencloseflag = value; }
        }
    }
}

