using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class XMLSerilazerHelper
    {
        /// <summary>
        /// Object对象转成XML文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Serializer<T>(object obj, string path)
        {
            FileStream xmlfile = new FileStream(path, FileMode.OpenOrCreate);

            //创建序列化对象 
            XmlSerializer xml = new XmlSerializer(typeof(T));
            try
            {    //序列化对象
                xml.Serialize(xmlfile, obj);
                xmlfile.Close();
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return true;

        }

        /// <summary>
        /// XML文件转成Object对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Deserializer<T>(string path)
        {
            try
            {
                FileStream xmlfile = new FileStream(path, FileMode.Open);

                XmlSerializer xml = new XmlSerializer(typeof(T));
                //序列化对象
                //xmlfile.Close();
                T t = (T)xml.Deserialize(xmlfile);
                xmlfile.Close();
                return t;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (FileNotFoundException)
            { throw; }
            finally
            {

            }
        }
    }
}