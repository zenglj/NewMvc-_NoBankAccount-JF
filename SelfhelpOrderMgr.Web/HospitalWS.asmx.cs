using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;
using System.Collections;
using System.Xml;

namespace SelfhelpOrderMgr.Web
{
    /// <summary>
    /// HospitalWS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://PrisonWS.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class HospitalWS : System.Web.Services.WebService
    {
        /// <summary>
        /// 犯人入院
        /// </summary>
        /// <param name="FCode"></param>
        /// <returns></returns>
        /// [WebMethod(Description = "<H3>获取犯人基本信息</H3>输入参数:FCode, 返回结果：T_Criminal 类实体")]
        [WebMethod(Description = "<H3>获取犯人基本信息,并为犯人办理入院（修改犯人在监狱卡状态）</H3>输入参数:FCode, 返回结果：CriminalIn 类实体")]
        //public CriminalIn CriminalInHospital(string FCode)
        //{
        //    CriminalIn reModel = JXHospitalBLL.CriminalIn(FCode);
        //    return reModel;
        //}

        public XmlDocument CriminalInHospital(string unitCode, string action, object data)
        {
            System.Xml.XmlNode[] nodes = (System.Xml.XmlNode[])data;
            string Action = action;
            string FCode = nodes[0].InnerText;            
            CriminalIn model = JXHospitalBLL.CriminalIn(FCode);

            //ReqDataEntity reModel = new ReqDataEntity();
            //reModel.doResult = "0000";
            //reModel.data = model;

            //返回XmlDocument 数据文件
            string xml = WebServiceCaller.ObjectToXML(model);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml("<xml><doResult>0000</doResult><data>" + xml + "</data></xml>");
            
            return xdoc;

        }
        

        /// <summary>
        /// 犯人本月在狱中消费信息
        /// </summary>
        /// <param name="FCode"></param>
        /// <returns></returns>
        [WebMethod(Description = "<H3>获取犯人本月在狱中消费信息,并为犯人办理入院</H3>输入参数:FCode, 返回结果：InvoiceEntity 类实体")]
        public InvoiceEntity CriminalConsumed(string FCode)
        {
            InvoiceEntity reModel = JXHospitalBLL.CriminalConsumedQuota(FCode);
            return reModel;
        }

        /// <summary>
        /// 犯人出院
        /// </summary>
        /// <param name="FCode"></param>
        /// <returns></returns>
        [WebMethod(Description = "<H3>犯人出院（修改犯人在监狱的卡状态）</H3>输入参数:FCode, 返回结果：ResultEntity 类实体")]
        public ResultEntity CriminalOutHospital(string FCode)
        {
            ResultEntity reModel = JXHospitalBLL.CriminalOut(FCode);
            return reModel;
        }

        /// <summary>
        /// 犯人在建新消费
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        //[WebMethod(Description = "<H3>回写犯人在建新医院消费订单</H3>输入参数:InvoiceEntity实体, 返回结果：ResultEntity 类实体")]
        //public ResultEntity CriminalConsume(InvoiceEntity reqModel)
        //{
        //    ResultEntity reModel = JXHospitalBLL.CriminalConsume(reqModel);
        //    return reModel;
        //}

        [WebMethod(Description = "<H3>中行WebService转发服务</H3>输入参数: unitCode:用户单位号，action:执行的方法名,data:传输数据实体, 返回结果：object Xml对象实体")]
        public object TranPrisonInfoForBank(string unitCode, string action, object data)
        {

            string Action = action;
            //调用示例：
            Hashtable ht = new Hashtable();  //Hashtable 为webservice所需要的参数集

            //ht.Add("unitCode", "33332222");
            //ht.Add("doResult", "0000");
            ht.Add("data", data);
            XmlDocument xx = WebServiceCaller.QuerySoapWebService("http://127.0.0.1:8080/HospitalWS.asmx", Action, ht,"");
            //XmlDocument xx = WebServiceCaller.QuerySoapWebService("http://localhost:7149/HospitalWS.asmx", Action, ht, "");

            XmlNode rootNode = xx.SelectSingleNode("xml");
            XmlNodeList subNodeList = rootNode.ChildNodes;
            subNodeList[0].InnerText = "9999";//修改节点内容
            //foreach (XmlNode subNode in subNodeList)
            //{
            //    XmlElement xmlElement = (XmlElement)subNode;
            //    string dd = xmlElement.InnerText;
            //}
            

            return  xx.InnerXml;


        }


        [WebMethod(Description = "<H3>回写犯人在建新医院消费订单</H3>输入参数:InvoiceEntity实体, 返回结果：ResultEntity 类实体")]
        public XmlDocument CriminalConsume(InvoiceEntity data)
        {
            //System.Xml.XmlNode[] nodes = (System.Xml.XmlNode[])data;
            //System.Xml.XmlText xmltxt = (System.Xml.XmlText)data;
            //string xmltxt = WebServiceCaller.ObjectToXML(data);
            //InvoiceEntity reqModel = WebServiceCaller.XMLToObject<InvoiceEntity>(xmltxt);
            InvoiceEntity reqModel = (InvoiceEntity)data;

            InvoiceEntity model = reqModel;
            //ReqDataEntity reModel = new ReqDataEntity();
            //ResultEntity model = JXHospitalBLL.CriminalConsume(reqModel);
            //reModel.doResult = "0000";
            //reModel.data = reqModel;
            //string xml = WebServiceCaller.ObjectToXML(reModel);
            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml("<xml>" + xml + "</xml>");

            string xml = WebServiceCaller.ObjectToXML(model);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml("<xml><doResult>0000</doResult><data>" + xml + "</data></xml>");

            return xdoc;

            
        }

        /// <summary>
        /// 获取犯人实时余额
        /// </summary>
        /// <param name="FCode"></param>
        /// <returns></returns>
        [WebMethod(Description = "<H3>同步犯人在监狱的余额信息</H3>输入参数:FCode, 返回结果：CriminalIn 类实体")]
        public CriminalIn GetRealTimeBalance(string FCode)
        {
            CriminalIn rspModel = JXHospitalBLL.RealTimeBalance(FCode);
            return rspModel;
        }

        /// <summary>
        /// 同步犯人未完成订单信息
        /// </summary>
        /// <param name="criminalCodeList"></param>
        /// <returns></returns>
        [WebMethod(Description = "<H3>同步犯人在建新未完成订单信息</H3>输入参数:List<string> 类型 InvoiceNos订单表编号, 返回结果：CriminalIn 类实体")]
        public CriminalIn GetInvoiceSynList(List<string> InvoiceNos)
        {
            //List<string> strInvoiceNo = InvoiceNos.ToList();
            CriminalIn rspModel = JXHospitalBLL.SynInvoiceResult(InvoiceNos);
            return rspModel;
        }

        /// <summary>
        /// 每日同步犯人余额
        /// </summary>
        /// <param name="FCodes"></param>
        /// <returns></returns>
        [WebMethod(Description = "<H3>同步犯人在建新未完成订单信息</H3>输入参数:List<string> 类型 FCodes犯人编号, 返回结果：CriminalIn 类实体")]
        public CriminalIn SynBalance(List<string> FCodes)
        {
            CriminalIn rspModel = JXHospitalBLL.SynInvoiceResult(FCodes);
            return rspModel;
        }
    }
}
