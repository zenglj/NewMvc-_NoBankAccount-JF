using SelfhelpOrderMgr.Common.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class MyTestController : Controller
    {
        // GET: MyTest
        public ActionResult Index()
        {
            string sss = RsaEncrypt.Encrypt("123abc中文", "30819f300d06092a864886f70d010101050003818d0030818902818100e2723c7c47e70c28ad0dfc58f49e462e8008f985c6e00aeb9c736ba35bb05fc1f2e3d28c7104cd75fea9429aa8f8ace76ab9abd96a66e28576cf6eac167c62f50f992db1b54c729747f3bcd5edea3e67d649d24efe67b1731a4f0e860a0a11387b4bd3ff74c95d51809d7bc96340dda5a50ca24df47256ff20c1a35cbb2f61290203010001");
            return Content(sss);
            //return View();
        }
    }
}