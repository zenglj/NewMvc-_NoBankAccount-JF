using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_BatchMoneyTrade_ErrList
    public class T_BatchMoneyTrade_ErrList
    {

        /// <summary>
        /// seqno
        /// </summary>		
        private int _seqno;
        public int seqno
        {
            get { return _seqno; }
            set { _seqno = value; }
        }
        /// <summary>
        /// ImportType
        /// </summary>		
        private int _importtype;
        public int ImportType
        {
            get { return _importtype; }
            set { _importtype = value; }
        }
        /// <summary>
        /// fcrimecode
        /// </summary>		
        private string _fcrimecode;
        public string fcrimecode
        {
            get { return _fcrimecode; }
            set { _fcrimecode = value; }
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
        /// Amount
        /// </summary>		
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// Crtdt
        /// </summary>		
        private DateTime _crtdt;
        public DateTime Crtdt
        {
            get { return _crtdt; }
            set { _crtdt = value; }
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
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// pc
        /// </summary>		
        private string _pc;
        public string pc
        {
            get { return _pc; }
            set { _pc = value; }
        }
        /// <summary>
        /// notes
        /// </summary>		
        private string _notes;
        public string notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

    }
}

