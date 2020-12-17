using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    public class DapperHelperBLL<T> where T:class
    {
        //这个地方必须用静态的方法
        public static List<T> GetAll(string sql, object paramter)
        {
            return DapperHelper<T>.Query(sql, paramter);
        }


    }
}