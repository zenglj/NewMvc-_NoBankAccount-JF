using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Common
{
    public class SqlEscapeHelper
    {

        public static string SqlEscape(string input)
        {
            if (string.IsNullOrWhiteSpace(input)==true)
                return "";

            return input
                .Replace(";", "\\;") // 转义单引号
                .Replace("'", "''") // 转义单引号
                .Replace("\\", "\\\\") // 转义反斜杠
                .Replace("[", "\\[") // 转义方括号
                .Replace("]", "\\]"); // 转义方括号
        }
    }
}