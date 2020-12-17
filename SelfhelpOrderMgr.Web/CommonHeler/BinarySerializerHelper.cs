using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class BinarySerializerHelper
    {
        /// <summary>
        /// 对象转成二进制流 Object To Bin
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool BinarySerializer(object obj, string path)
        {
            FileStream Stream = new FileStream(path, FileMode.OpenOrCreate);
            //创建序列化对象 
            BinaryFormatter bin = new BinaryFormatter();
            try
            {    //序列化对象
                bin.Serialize(Stream, obj);
                Stream.Close();
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// 二进制流转成对象 Bin To Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T BinaryDeserializer<T>(string path)
        {
            try
            {
                FileStream binfile = new FileStream(path, FileMode.Open);

                BinaryFormatter bin = new BinaryFormatter();
                //序列化对象
                //xmlfile.Close();
                T t = (T)bin.Deserialize(binfile);
                binfile.Close();
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