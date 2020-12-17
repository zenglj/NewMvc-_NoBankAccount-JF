using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

    /// <summary>
    ///  利用WebRequest/WebResponse进行WebService调用的类
    /// </summary>
    public class WebServiceCaller
    {
        #region Tip:使用说明
        //webServices 应该支持Get和Post调用，在web.config应该增加以下代码
        //<webServices>
        //  <protocols>
        //    <add name="HttpGet"/>
        //    <add name="HttpPost"/>
        //  </protocols>
        //</webServices>

        //调用示例：
        //Hashtable ht = new Hashtable();  //Hashtable 为webservice所需要的参数集
        //ht.Add("str", "test");
        //ht.Add("b", "true");
        //XmlDocument xx = WebSvcCaller.QuerySoapWebService("http://localhost:81/service.asmx", "HelloWorld", ht);
        //MessageBox.Show(xx.OuterXml);
        #endregion

        /// <summary>
        /// 需要WebService支持Post调用
        /// </summary>
        public static XmlDocument QueryPostWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadXmlResponse(request.GetResponse());
        }

        /// <summary>
        /// 需要WebService支持Get调用
        /// </summary>
        public static XmlDocument QueryGetWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName + "?" + ParsToString(Pars));
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            return ReadXmlResponse(request.GetResponse());
        }

        /// <summary>
        /// 通用WebService调用(Soap),参数Pars为String类型的参数名、参数值
        /// </summary>
        public static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars,string RootName="root")
        {
            if (_xmlNamespaces.ContainsKey(URL))
            {
                return QuerySoapWebService(URL, MethodName, Pars, _xmlNamespaces[URL].ToString(),RootName);
            }
            else
            {
                return QuerySoapWebService(URL, MethodName, Pars, GetNamespace(URL),RootName);
            }
        }

        

        private static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars, string XmlNs,string RootName="root")
        {
            _xmlNamespaces[URL] = XmlNs;//加入缓存，提高效率
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
            SetWebRequest(request);
            byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();
            doc = ReadXmlResponse(request.GetResponse());

            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
            if(RootName=="")
            {
                RetXml=RetXml.Replace("&gt;", ">");
                RetXml=RetXml.Replace("&lt;", "<");
                doc2.LoadXml(RetXml);
                return doc2;
            }
            else
            {
                RetXml = RetXml.Replace("&gt;", ">");
                RetXml = RetXml.Replace("&lt;", "<");
                
                doc2.LoadXml("<" + RootName + ">" + RetXml + "</" + RootName + ">");
            }            
            //AddDelaration(doc2);
            return doc2;
        }
        private static string GetNamespace(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
            SetWebRequest(request);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            return doc.SelectSingleNode("//@targetNamespace").Value;
        }

        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
            AddDelaration(doc);
            //XmlElement soapBody = doc.createElement_x_x("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            //XmlElement soapMethod = doc.createElement_x_x(MethodName);
            XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            foreach (string k in Pars.Keys)
            {
                //XmlElement soapPar = doc.createElement_x_x(k);
                XmlElement soapPar = doc.CreateElement(k);
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
                soapMethod.AppendChild(soapPar);
            }
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }

        /// <summary>
        /// 对象对转序列化XML格式字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }


        /// <summary>
        /// 反系列化xml
        /// </summary>
        /// <param name="type">可系列化的对象类型</param>
        /// <param name="xmlNode">xml中对象对应的节点</param>
        /// <returns></returns>
        public static object XmlDeserialize(Type type, XmlNode xmlNode)
        {
            XmlElement xe = (XmlElement)xmlNode;
            object parentObj = CreateExample(type);
            foreach (var parentItem in parentObj.GetType().GetProperties())
            {
                var parentSetobj = parentObj.GetType().GetProperty(parentItem.Name);
                XmlElement xmlElement = (XmlElement)xe.SelectSingleNode(parentItem.Name);
                Type parentType = parentItem.PropertyType;
                bool isClass = IsClass(parentType);
                if (isClass)
                {
                    object subObj = CreateExample(parentType);
                    string subName = subObj.GetType().Name;
                    XmlNode subNode = xe.SelectSingleNode(subName);
                    foreach (var subItem in subObj.GetType().GetProperties())
                    {
                        XmlElement subxl = (XmlElement)subNode;
                        var subSetobj = subObj.GetType().GetProperty(subItem.Name);
                        Type proType = subItem.PropertyType;
                        if (IsClass(proType))
                        {
                            subObj = XmlDeserialize(subObj.GetType(), subNode);
                            continue;
                        }
                        object value = Convert.ChangeType(subxl.SelectSingleNode(subItem.Name).InnerText, proType);
                        subSetobj.SetValue(subObj, value, null);
                    }
                    parentSetobj.SetValue(parentObj, subObj, null);
                    continue;
                }
                object parentValue = Convert.ChangeType(xmlElement.InnerText, parentType);
                parentSetobj.SetValue(parentObj, parentValue, null);
            }
            return parentObj;
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object CreateExample(Type type)
        {
            object obj = Activator.CreateInstance(type);
            return obj;
        }

        /// <summary>
        /// 根据TypeCode枚举判读是否为引用对象
        /// </summary>
        /// <param name="type">判断类型</param>
        /// <returns></returns>
        public static bool IsClass(Type type)
        {
            string[] typeCodeNameArray = Enum.GetNames(typeof(TypeCode));
            foreach (string item in typeCodeNameArray)
            {
                if (type.Name == item)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// XML序列化对象
        /// </summary>
        /// <param name="emp">object that would be converted into xml</param>
        /// <returns></returns>
        public static string ObjectToXML(Object Instance)
        {
            MemoryStream stream = null;
            TextWriter writer = null;
            string ObjectXml = string.Empty;
            try
            {
                stream = new MemoryStream(); // read xml in memory
                writer = new StreamWriter(stream, Encoding.UTF8);

                // get serialise object
                Type t = Instance.GetType();
                XmlSerializer serializer = new XmlSerializer(t);

                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);

                serializer.Serialize(writer, Instance, xsn); // read object
                int count = (int)stream.Length; // saves object in memory stream
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array
                stream.Read(arr, 0, count);
                //UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string
                UTF8Encoding utf = new UTF8Encoding();
                ObjectXml = utf.GetString(arr).Trim();
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            finally
            {
                if (stream != null && stream.Length > 0)
                {
                    stream.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }

            return FormatXml(ObjectXml);
        }

        /// <summary>
        /// XML序列化对象
        /// </summary>
        /// <param name="Instance">实例</param>
        /// <param name="encoding">编码</param>
        /// <param name="ClearHeader">是去除声明头</param>
        /// <returns></returns>
        public static string ObjectToXML(Object Instance, Encoding encoding, bool ClearHeader)
        {
            MemoryStream stream = null;
            TextWriter writer = null;
            string ObjectXml = string.Empty;
            try
            {
                stream = new MemoryStream(); // read xml in memory
                writer = new StreamWriter(stream, encoding);

                // get serialise object
                Type t = Instance.GetType();
                XmlSerializer serializer = new XmlSerializer(t);

                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);

                serializer.Serialize(writer, Instance, xsn); // read object
                int count = (int)stream.Length; // saves object in memory stream
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array
                stream.Read(arr, 0, count);

                ObjectXml = encoding.GetString(arr).Trim();
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            finally
            {
                if (stream != null && stream.Length > 0)
                {
                    stream.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }

            if (ClearHeader)
                return FormatXml(ObjectXml);
            else
                return ObjectXml;
        }

        /// <summary>
        /// XML序列化对象
        /// </summary>
        /// <param name="Instance">实例</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ObjectToXML(Object Instance, Encoding encoding)
        {
            return ObjectToXML(Instance, encoding, true);
        }

        /// <summary>
        /// 格式化XML
        /// </summary>
        /// <param name="Xml"></param>
        /// <returns></returns>
        public static string FormatXml(string Xml)
        {
            if (string.IsNullOrEmpty(Xml))
            {
                return "";
            }

            string startXml = "<?";
            string endXml = "?>";
            int startPos = Xml.IndexOf(startXml);
            int endPos = Xml.IndexOf(endXml);
            if (!(startPos == -1 || endPos == -1))
            {
                return Xml.Remove(startPos, endPos - startPos + endXml.Length);
            }
            else
            {
                return Xml;
            }
        }

        /// <summary>
        /// 反序列化XML字符串为对象
        /// </summary>
        /// <param name="xml">xml data of employee</param>
        /// <returns></returns>
        public static T XMLToObject<T>(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            T o = default(T);
            try
            {
                // serialise to object
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                stream = new StringReader(xml); // read xml data
                reader = new XmlTextReader(stream);  // create reader

                //XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                //xsn.Add("xmlns", "http://tempuri.org/XMLSchema.xsd");
                // covert reader to object
                o = (T)(serializer.Deserialize(reader));
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return o;
        }

        /// <summary>
        /// 设置凭证与超时时间
        /// </summary>
        /// <param name="request"></param>
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }

        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }

        private static String ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }

        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc;
        }

        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }

        private static Hashtable _xmlNamespaces = new Hashtable();//缓存xmlNamespace，避免重复调用GetNamespace
    }
