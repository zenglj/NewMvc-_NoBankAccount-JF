using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class SocketHelper
    {
        public static ResultInfo SendInfo(string sendData, string ipAddress, int portNum)
        {
            ResultInfo rs = new ResultInfo();
            #region Socket发送验证图片信息

            #region 初始化Socket连接
            string stringdata = "";
            //实例化socket
            Socket sendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
            IPEndPoint ipendpiont = new IPEndPoint(IPAddress.Parse(ipAddress), portNum);
            sendsocket.Connect(ipendpiont);
            //MessageBox.Show("服务器IP:" + sendsocket.RemoteEndPoint);
            #endregion

            #region 接收连接请求返回
            byte[] msgByte = new byte[1024 * 1024 * 2];
            int length = 0;

            length = sendsocket.Receive(msgByte, msgByte.Length, SocketFlags.None);
            string strConnectRcv = ("连接时请求返回：" + Encoding.UTF8.GetString(msgByte, 0, length));
            Log4NetHelper.logger.Info(strConnectRcv);//写入连接记录

            #endregion


            #region 接收报文请求返回
            byte[] orgByte = Encoding.Default.GetBytes(sendData);

            sendsocket.Send(orgByte);

            //==================================================
            //接收数据

            msgByte = new byte[1024 * 1024 * 2];
            length = 0;
            stringdata = "";
            try
            {
                length = sendsocket.Receive(msgByte, msgByte.Length, SocketFlags.None);
                if (length > 0)
                {
                    stringdata = ("人脸比对结果：" + Encoding.UTF8.GetString(msgByte, 0, length));
                    Log4NetHelper.logger.Info(strConnectRcv);

                    rs.Flag = true;
                    rs.ReMsg = "OK|发送成功";
                    rs.DataInfo = stringdata;
                }
            }
            catch
            {
                stringdata = "recvice data fail";
                rs.ReMsg = "Err|recvice data fail";
                rs.DataInfo = stringdata;
            }


            
            #endregion

            //关闭Socket连接
            sendsocket.Shutdown(System.Net.Sockets.SocketShutdown.Send);
            sendsocket.Close();
            sendsocket.Dispose();

            #endregion

            return rs;

        }
    }
}