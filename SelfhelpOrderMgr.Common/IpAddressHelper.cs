using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace SelfhelpOrderMgr.Common
{
    public class IpAddressHelper
    {
        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 返回MAC地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string getHostMac(string ip)
        {
            byte[] bs = new byte[50] { 0x0, 0x00, 0x0, 0x10, 0x0, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x43, 0x4b, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x0, 0x0, 0x21, 0x0, 0x1 };
            byte[] Buf = new byte[500];
            byte[,] recv = new byte[18, 28];
            string str = "", strHost = "", Group = "", User = "", strMac = "";
            int receive, macline = 0, usernum = 0;
            string[] domainuser = new string[2];
            domainuser[0] = "";
            domainuser[1] = "";

            try
            {
                IPEndPoint senderTest = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)senderTest;

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), 137);

                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
                server.SendTo(bs, bs.Length, SocketFlags.None, ipep);
                receive = server.ReceiveFrom(Buf, ref Remote);
                server.Close();

                if (receive > 0)
                {
                    recv = new byte[18, (receive - 56) % 18];

                    for (int k = 0; k < (receive - 56) % 18; k++)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            recv[j, k] = Buf[57 + 18 * k + j];
                        }
                    }

                    for (int k = 0; k < (receive - 56) % 18; k++)
                    {
                        str = "";
                        if (System.Convert.ToString(recv[15, k], 16) == "0" && (System.Convert.ToString(recv[16, k], 16) == "4" || System.Convert.ToString(recv[16, k], 16) == "44"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            strHost = str.Trim();
                        }

                        if (System.Convert.ToString(recv[15, k], 16) == "0" && (System.Convert.ToString(recv[16, k], 16) == "84" || System.Convert.ToString(recv[16, k], 16).ToUpper() == "C4"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            Group = str.Trim();
                        }

                        if (System.Convert.ToString(recv[15, k], 16) == "3" && (System.Convert.ToString(recv[16, k], 16) == "4" || System.Convert.ToString(recv[16, k], 16) == "44"))
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str += System.Convert.ToChar(recv[j, k]).ToString();
                            }
                            domainuser[usernum] = str.Trim();
                            usernum++;
                        }

                        if (System.Convert.ToString(recv[15, k], 16) == "0" && System.Convert.ToString(recv[16, k], 16) == "0" && System.Convert.ToString(recv[17, k], 16) == "0")
                        {
                            macline = k;

                            for (int i = 0; i < 6; i++)
                            {
                                if (i < 5)
                                {
                                    strMac += System.Convert.ToString(recv[i, macline], 16).PadLeft(2, '0').ToUpper() + "-";
                                }
                                if (i == 5)
                                {
                                    strMac += System.Convert.ToString(recv[i, macline], 16).PadLeft(2, '0').ToUpper();
                                }
                            }
                            k = (receive - 56) % 18;
                        }
                    }
                    User = domainuser[1];
                    if (string.IsNullOrEmpty(domainuser[1])) { User = domainuser[0]; }
                    return strMac;

                }
            }
            catch (SocketException ex)
            {
                //Console.WriteLine(ip);
                throw ( new Exception( "无法获取MAC:" + ex.Message));
            }
            return "";
        }
    }
}