using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace SelfhelpOrderMgr.BLL
{
    public class BankEnterpriseSerice
    {
        BaseDapperBLL _bll = new BaseDapperBLL();

        public ResultInfo GetBankCardDateBalance(DateTime startDate, DateTime endDate)
        {
            ResultInfo rs = new ResultInfo();
            //获取银企直连配置参数
            var setting = new ConfigHelper().GetYinQiSetting();
            string strCommand = "b2e0012";
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                    + "<bocb2e version = \"120\" locale = \"zh_CN\">"
         + "<head>"
             + "<termid>" + setting.termid + "</termid>"
             + "<trnid>" + setting.trnid + "</trnid>"
             + "<custid>" + setting.custid + "</custid>"
             + "<cusopr>" + setting.cusopr + "</cusopr>"
             + "<trncod>" + strCommand + "</trncod>"
             + "<token>" + setting.token + "</token>"
         + "</head>"
         + "<trans>"
             + "<trn-b2e0012-rq>"
                 + "<b2e0012-rq>"
                     + "<account>"
                         + "<ibknum>" + setting.ibknum + "</ibknum>"
                         + "<actacn>" + setting.actacn + "</actacn>"
                     + "</account>"
                     + "<datescope>"
                         + "<from>" + startDate.ToString("yyyyMMdd") + "</from>"
                         + "<to>" + endDate.ToString("yyyyMMdd") + "</to>"
                     + "</datescope>"
                 + "</b2e0012-rq>"
             + "</trn-b2e0012-rq>"
         + "</trans>"
     + "</bocb2e>";
            string _res = HttpHelper.HttpPostStr(setting.postServerUrl, xml);

            _res = _res.Replace("utf-8", "UTF-8");
            //替换增加一个<root></root>根目录
            string subxml = _res.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            string _tempXml = $"<?xml version=\"1.0\" encoding=\"UTF-8\" ?><root>{subxml}</root>";

            //读取Xml文件转成Xml对象
            StringReader reader = new StringReader(_tempXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            //验证是否请求失败
            var errxmls = xmlDoc.SelectSingleNode("//trn-b2eerror-rs");
            if (errxmls != null)
            {
                rs.ReMsg = errxmls.SelectSingleNode("status").SelectSingleNode("rspmsg").InnerText;
                //return Content( errxmls.SelectSingleNode("status").SelectSingleNode("rspmsg").InnerText);
                return rs;
            }
            //获取相应的【b2e0012-rs】结点数据
            XmlNodeList xmls = xmlDoc.SelectNodes("//b2e0012-rs");

            List<T_Bank_DateBalance> bals = new List<T_Bank_DateBalance>();
            foreach (XmlNode w in xmls)
            {
                T_Bank_DateBalance bal = new T_Bank_DateBalance();
                bal.rspcod = w.SelectSingleNode("status").SelectSingleNode("rspcod").InnerText;
                bal.rspmsg = w.SelectSingleNode("status").SelectSingleNode("rspmsg").InnerText;
                bal.ibknum = w.SelectSingleNode("account").SelectSingleNode("ibknum").InnerText;
                bal.actacn = w.SelectSingleNode("account").SelectSingleNode("actacn").InnerText;
                bal.curcde = w.SelectSingleNode("account").SelectSingleNode("curcde").InnerText;
                bal.bokbal = Convert.ToDecimal(w.SelectSingleNode("balance").SelectSingleNode("bokbal").InnerText);
                bal.avabal = Convert.ToDecimal(w.SelectSingleNode("balance").SelectSingleNode("avabal").InnerText);
                bal.baldat = Convert.ToDateTime(w.SelectSingleNode("baldat").InnerText.Substring(0, 4) + "-" + w.SelectSingleNode("baldat").InnerText.Substring(4, 2) + "-" + w.SelectSingleNode("baldat").InnerText.Substring(6, 2));
                bals.Add(bal);
            }

            foreach (var bal in bals)
            {
                var b = _bll.GetModelFirst<T_Bank_DateBalance, T_Bank_DateBalance>(Newtonsoft.Json.JsonConvert.SerializeObject(new { actacn = bal.actacn, baldat = bal.baldat }));
                if (b == null)
                {
                    var _datebal = _bll.Insert<T_Bank_DateBalance>(bal);
                }
            }

            rs.ReMsg = "成功";
            rs.Flag = true;
            return rs;
        }

    }
}