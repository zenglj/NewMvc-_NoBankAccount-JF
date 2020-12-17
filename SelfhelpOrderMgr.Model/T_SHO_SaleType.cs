using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_SHO_SaleType
    public class T_SHO_SaleType
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
        /// PType
        /// </summary>		
        private string _ptype;
        public string PType
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        /// <summary>
        /// TypeFlagId
        /// </summary>		
        private int _typeflagid;
        public int TypeFlagId
        {
            get { return _typeflagid; }
            set { _typeflagid = value; }
        }
        /// <summary>
        /// 可消费账户,0表示全部，1表示A账户，2表示B账户
        /// </summary>		
        private int _canconsumeaccount;
        public int CanconsumeAccount
        {
            get { return _canconsumeaccount; }
            set { _canconsumeaccount = value; }
        }
        /// <summary>
        /// 优先付款账户,0表示A账户，1表示B账户
        /// </summary>		
        private int _firstpaymentaccount;
        public int FirstPaymentAccount
        {
            get { return _firstpaymentaccount; }
            set { _firstpaymentaccount = value; }
        }
        /// <summary>
        /// ShoppingFlag
        /// </summary>		
        private int _shoppingflag;
        public int ShoppingFlag
        {
            get { return _shoppingflag; }
            set { _shoppingflag = value; }
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
        /// Fifoflag
        /// </summary>		
        private int _fifoflag;
        public int Fifoflag
        {
            get { return _fifoflag; }
            set { _fifoflag = value; }
        }

    }
}

