using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL.ExtBLL
{
    public class T_EdiBankSumListBLL
    {
        public IEnumerable<T_EdiBankSumList> GetModelListByPage(int page, int pageRow, DateTime startDate, DateTime endDate, string Code, string Remark,string succflag)
        {
            return new T_EdiBankSumListDAL().GetModelListByPage(page, pageRow, startDate, endDate, Code, Remark, succflag);
        }
    
    }
}