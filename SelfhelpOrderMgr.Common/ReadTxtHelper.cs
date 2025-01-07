using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Common
{
    public class ReadTxtHelper
    {
        public static string ReadTxtFile(string filePath)
        {
            //string filePath = "path_to_your_txt_file.txt";
            string fileContent = "";

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                fileContent = "";
                // 处理读取文件失败的异常情况
            }

            //Console.WriteLine(fileContent);
            return fileContent;
        }
    }
}