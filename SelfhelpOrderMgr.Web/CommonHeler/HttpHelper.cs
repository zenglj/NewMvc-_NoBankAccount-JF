using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public static class HttpHelper
    {
        public static string HttpPostStr(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";

            //request.ContentType = "text/xml;charset=UTF-8";
            request.ContentType = "application/xml;charset=UTF-8";

            byte[] postData = Encoding.Default.GetBytes(postDataStr);
            request.ContentLength = postData.Length;
            Stream reqStream = request.GetRequestStreamAsync().Result;

            reqStream.Write(postData, 0, postData.Length);

            reqStream.Dispose();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            string cs = response.CharacterSet.ToLower();

            //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string retString = reader.ReadToEnd();
            //retString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><bocb2e version=\"120\" locale=\"zh_CN\"><head><termid>E100002001206</termid><trnid>20200710140433001</trnid><custid>302063705</custid><cusopr>334187829</cusopr><trncod>b2e0012</trncod><token>D963661F92F95CD3D572AE5B1D5F306F</token></head><trans><trn-b2e0012-rs><status><rspcod>B001</rspcod><rspmsg>ok</rspmsg></status><b2e0012-rs><status><rspcod>B001</rspcod><rspmsg>ok</rspmsg></status><account><ibknum>45481</ibknum><actacn>411776557063</actacn><curcde>CNY</curcde></account><balance><bokbal>13619380.50</bokbal><avabal>13489960.75</avabal></balance><baldat>20220609</baldat></b2e0012-rs></trn-b2e0012-rs></trans></bocb2e>";
            //retString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><bocb2e xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><head><termid>E100002001206</termid><trnid>20200710140433001</trnid><custid>302063705</custid><cusopr>334187829</cusopr><trncod>bocb2e</trncod><token>D963661F92F95CD3D572AE5B1D5F306F</token></head><trans><trn-b2e0012-rs><status><rspcod>B001</rspcod><rspmsg>ok</rspmsg></status><b2e0012-rs><status><rspcod>B001</rspcod><rspmsg>ok</rspmsg></status><account><ibknum>45481</ibknum><actacn>411776557063</actacn><curcde>CNY</curcde></account><balance><bokbal>13639290.58</bokbal><avabal>13663311.49</avabal></balance><baldat>20220601</baldat></b2e0012-rs><b2e0012-rs><status><rspcod>B001</rspcod><rspmsg>ok</rspmsg></status><account><ibknum>45481</ibknum><actacn>411776557063</actacn><curcde>CNY</curcde></account><balance><bokbal>13663311.49</bokbal><avabal>13533799.99</avabal></balance><baldat>20220602</baldat></b2e0012-rs></trn-b2e0012-rs></trans></bocb2e>";
            return retString;
        }

    }
}