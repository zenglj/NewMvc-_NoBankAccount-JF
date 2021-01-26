using SelfhelpOrderMgr.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //自定义异常处理
            filters.Add(new CustomExceptionFilterAttribute());
        }
    }
}
