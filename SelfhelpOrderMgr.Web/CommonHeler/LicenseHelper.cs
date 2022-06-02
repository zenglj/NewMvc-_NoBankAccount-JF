using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class LicenseHelper
    {
        static string strSalt = "zenglj83754355";
        static string strPwd = "zenlj4355";
        public static string LicenseMD5(string strText)
        {
            DateTime dt = DateTime.Now.AddDays(30);
            strText = strText + strSalt + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
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
        public static string LicenseMD5_Date(string strText, DateTime licenseDate,string getCodePwd)
        {
            if (getCodePwd == strPwd)
            {
                strText = strText + strSalt + licenseDate.Year.ToString() + "-" + licenseDate.Month.ToString() + "-" + licenseDate.Day.ToString();
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
            else
            {
                return "Err|验证执行码错误";
            }
            
        }

        /// <summary>
        /// 获取注册码信息
        /// </summary>
        /// <returns></returns>
        public static string GetRegInfo()
        {
            try
            {
                string strFileName =Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Upload/RegInfo.rar");
                HardwareInfo hwi = new HardwareInfo();
                string hardId = hwi.GetHardDiskID();
                if (hardId == "")
                {
                    hardId = strSalt;
                }
                var s = System.Text.Encoding.UTF8.GetBytes(hardId);
                string _b64 = Convert.ToBase64String(s);
                //return _b64;

                StreamWriter sw = new StreamWriter(strFileName,false);
                sw.Write(_b64);
                sw.Close();
                return "OK|RegInfo.rar";

            }
            catch
            {
                return ("Err|获取注册信息失败");
            }
        }

        //验证授权码
        public static string CheckLicenseCode()
        {
            HardwareInfo hwi = new HardwareInfo();
            string hardId = hwi.GetHardDiskID();
            //HardwareInfo.GetHardDiskID
            if (hardId == "")
            {
                hardId = strSalt;
            }

            var s = System.Text.Encoding.UTF8.GetBytes(hardId);
            string _b64 = Convert.ToBase64String(s);
            //文件方式注册
            string see;
            try
            {
                string strFileUpload = $"{System.AppDomain.CurrentDomain.BaseDirectory}Upload";
                //("~/Upload");
                //创建月份目录
                if (!Directory.Exists(strFileUpload))
                {
                    DirectoryInfo d = Directory.CreateDirectory(strFileUpload);
                }
                //string strFileName = Server.MapPath("C:/Windows/System32/JvdLicenseCode.rar");
                //string strFileName = Server.MapPath("~/JvdLicenseCode.rar");
                string strFileName = $"{System.AppDomain.CurrentDomain.BaseDirectory}JvdLicenseCode.rar";

                StreamReader sr = new StreamReader(strFileName);
                see = sr.ReadToEnd();
                sr.Close();
            }

            catch
            {
                return ("Error|系统未注册，不能使用");
            }

            if (see == null)
            {
                return ("Error|系统未注册，不能使用");
            }
            char[] aa = "|".ToCharArray();
            string[] regInfos = see.Split(aa);
            DateTime licenseDate = Convert.ToDateTime(regInfos[1]);
            if (licenseDate < DateTime.Today)
            {
                return ("Error|注册码已过期");
            }
            string licenseCode = MD5Process.LicenseMD5_Date(_b64, licenseDate);
            if (licenseCode != see)
            {
                return ("Error|注册码无效");
            }
            return ("OK|注册验证通过");
        }

        public static string GetLicense (string CheckCode, string RegCode,int RegDays)
        {
            if (string.IsNullOrEmpty(CheckCode))
            {
                return ("OK&执行验证码不能为空");
            }
            if (strPwd != CheckCode)
            {
                return ("Err&执行验证码不正确");
            }
            if (string.IsNullOrEmpty(RegCode))
            {
                return ("OK&用户的注册码不能为空");
            }
            if (RegDays<1)
            {
                return ("OK&授权许可日期不能为空");
            }

            try
            {
                //int days = Convert.ToInt32(RegDays);
                DateTime dt = DateTime.Today.AddDays(RegDays);
                string rcode = MD5Process.LicenseMD5_Date(RegCode, dt);

                string strFileName = $"{ System.AppDomain.CurrentDomain.BaseDirectory}Upload/JvdLicenseCode.rar";
                StreamWriter sw = new StreamWriter(strFileName);
                sw.Write(rcode);
                sw.Close();

                return ("OK&" + rcode + "&JvdLicenseCode.rar");
            }
            catch
            {
                return ("Err&授权许可日期格式不正确");
            }
        }

    }
}