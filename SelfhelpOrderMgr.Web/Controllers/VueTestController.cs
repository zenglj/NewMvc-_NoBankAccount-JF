using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SelfhelpOrderMgr.Web.CommonHeler;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class VueTestController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string userLoginName = "";
        BaseDapperBLL _bll = new BaseDapperBLL();
        //
        // GET: /VueTest/
        public ActionResult Index()
        {
            return View();
        }
        public ResultInfo GetDateBalance()
        {
            ResultInfo rs = new ResultInfo();



            //获取银企直连配置参数
            var setting = new ConfigHelper().GetYinQiSetting();
            string strCommand = "b2e0012";
            object _obj = new
            {
                unitCode = "FJJXYY08",
                fCode = "3512027398",
                remark = ""
            };
            string _res = HttpHelper.HttpPostByJson("http://39.98.197.192:8090/api/admin/Auth/GetQueryCriminalBankInfo", jss.Serialize(_obj),"");

            _res = _res.Replace("utf-8", "UTF-8");
            //替换增加一个<root></root>根目录
        



            rs.ReMsg = "成功";
            rs.Flag = true;
            return rs;
        }
    }
}