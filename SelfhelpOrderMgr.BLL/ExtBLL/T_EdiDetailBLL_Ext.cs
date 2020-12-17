using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_EdiDetailBLL
    {
        public IEnumerable<T_EdiDetail> GetListByMainseqno(int mainseqno)
        {
            return new T_EdiDetailDAL().GetListByMainseqno(mainseqno);
        }
    }
}