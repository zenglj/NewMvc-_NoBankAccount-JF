using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Common
{
    public class FileNameHelper
    {
        public static string GetFileName(string aFile)
        {
            string aFirstName = aFile.Substring(aFile.LastIndexOf("\\") + 1, (aFile.LastIndexOf(".") - aFile.LastIndexOf("\\") - 1));  //文件名
            return aFirstName;
        }
        public static string GetFileExtName(string aFile)
        {
            string aLastName = aFile.Substring(aFile.LastIndexOf(".") + 1, (aFile.Length - aFile.LastIndexOf(".") - 1));   //扩展名
            return aLastName;
        }
    }
}