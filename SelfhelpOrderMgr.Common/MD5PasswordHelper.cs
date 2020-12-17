using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.Common
{
    public class MD5ProcessHelper
    {
        /// <summary>
        /// 输入pwd和IpAddr，获取MD5KeyWord
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="ipAddr"></param>
        /// <returns></returns>
        public static string GetMD5Token(string pwd, string ipAddr)
        {
            string strText = pwd+ "|"+ "zenglj83754355" + "|"+ipAddr;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(strText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            string strMD5Token = byte2String;
            return strMD5Token;
        }
        /// <summary>
        /// 验证key和密码是否有效
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pwd"></param>
        /// <param name="ipAddr"></param>
        /// <returns></returns>
        public static bool CheckMd5Token(string token, string pwd, string ipAddr)
        {
            string strText = pwd + "|" + "zenglj83754355" + "|" + ipAddr;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(strText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            string strMD5Keyword = byte2String;
            return strMD5Keyword == token;
        }

        public static string LicenseMD5(string strText)
        {
            DateTime dt = DateTime.Now.AddDays(30);
            strText = strText + "zenglj83754355" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(strText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            string LicenseMd5Code = byte2String;
            return LicenseMd5Code + "|" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
        }
        public static string LicenseMD5_Date(string strText, DateTime licenseDate)
        {
            strText = strText + "zenglj83754355" + licenseDate.Year.ToString() + "-" + licenseDate.Month.ToString() + "-" + licenseDate.Day.ToString();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(strText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            string LicenseMd5Code = byte2String;
            return LicenseMd5Code + "|" + licenseDate.Year.ToString() + "-" + licenseDate.Month.ToString() + "-" + licenseDate.Day.ToString();
        }

        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }
        /// <summary>
        /// 计算文件的MD5校验
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

    }
}

