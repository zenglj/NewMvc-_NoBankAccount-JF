using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_ICCARD_LISTBLL
    {
        public bool UpdateCardStatus(int fflag, string cardNo, string fcode)
        {
            return new T_ICCARD_LISTDAL().UpdateCardStatus(fflag, cardNo, fcode);
        }
    }
}