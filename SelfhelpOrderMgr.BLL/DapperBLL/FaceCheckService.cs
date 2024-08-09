using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public static class FaceCheckService
    {
        private readonly static string faceServerIP = ConfigurationManager.ConnectionStrings["faceServerIP"].ConnectionString;
        private readonly static string facePort = ConfigurationManager.ConnectionStrings["facePort"].ConnectionString;
        //private readonly static string faceServerIP = "127.0.0.1";
        //private readonly static string facePort = "7788";
        public static  ResultInfo SendAndCheckFace(string fcrimecode, string imageSrc, string faceMode,  T_Criminal _criminal,int typeFlag=0,string fareaCode=null)
        {
            ResultInfo rs = new ResultInfo();
            #region Socket发送验证图片信息


            using (Socket sendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) )
            {
                //实例化socket
                //Socket sendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
                IPEndPoint ipendpiont = new IPEndPoint(IPAddress.Parse(faceServerIP), Convert.ToInt32(facePort));
                //IPEndPoint ipendpiont = new IPEndPoint(IPAddress.Parse("192.168.2.97"), 7788);
                sendsocket.Connect(ipendpiont);
                //MessageBox.Show("服务器IP:" + sendsocket.RemoteEndPoint);

                byte[] msgByte = new byte[1024 * 1024 * 8];
                int length = 0;

                PhotoEntity photo = new PhotoEntity()
                {
                    fcrimeCode = fcrimecode,
                    photoBase64Data = imageSrc,
                    TypeFlag = typeFlag,
                    FAreaCode = fareaCode
                };

                if (_criminal != null)
                {
                    photo = new PhotoEntity()
                    {
                        fcrimeCode = fcrimecode,
                        photoName = fcrimecode + ".png",
                        photoBase64Data = imageSrc,
                        TypeFlag = typeFlag,
                        FAreaCode = fareaCode

                    };
                }


                length = sendsocket.Receive(msgByte, msgByte.Length, SocketFlags.None);
                string strConnectRcv = ("人脸比对连接请求返回：" + Encoding.UTF8.GetString(msgByte, 0, length));
                if (strConnectRcv.Contains("连接服务端成功"))
                {
                    //Log4NetHelper.logger.Info(strConnectRcv);//写入连接记录
                    //imageSrc = "0001" + imageSrc;//增加报文头
                    //imageSrc = "0001" + Newtonsoft.Json.JsonConvert.SerializeObject(photo);//增加报文头
                    //faceMode 人脸模式:0001是验证,0002是注册
                    imageSrc = faceMode + Newtonsoft.Json.JsonConvert.SerializeObject(photo);//增加报文头
                                                                                             //Log4NetHelper.logger.Info("发送人脸的报文,系统：" + imageSrc );

                    byte[] orgByte = Encoding.Default.GetBytes(imageSrc);

                    sendsocket.Send(orgByte);

                    //==================================================
                    //接收数据


                    msgByte = new byte[1024 * 1024 * 8];
                    length = 0;
                    string stringdata = "";
                    try
                    {
                        length = sendsocket.Receive(msgByte, msgByte.Length, SocketFlags.None);
                        if (length > 0)
                        {
                            string strRecv = Encoding.UTF8.GetString(msgByte, 0, length);
                            rs = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultInfo>(strRecv);
                            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<FaceCheckResult>(rs.DataInfo.ToString());
                            model.UserCode = $"{model.UserCode}|{MD5ProcessHelper.GetMD5(model.UserCode)}";
                            rs.DataInfo = model;

                            //stringdata = ("人脸比对结果：" + Encoding.UTF8.GetString(msgByte, 0, length));

                            //Log4NetHelper.logger.Info(strConnectRcv);//写入连接记录

                        }
                    }
                    catch
                    {
                        //stringdata = "recvice data fail";
                        stringdata = "接收失败，请重试";
                        rs.ReMsg = stringdata;
                    }
                }
                else
                {
                    rs.ReMsg = "连接人脸验证服务识别，请重试";
                }
                

            }





            //sendsocket.Shutdown(System.Net.Sockets.SocketShutdown.Send);
            //sendsocket.Close();
            //sendsocket.Dispose();



            //return Content("OK|"+ stringdata);
            //Log4NetHelper.logger.Info("接收处理人脸结果的报文,系统：" + Newtonsoft.Json.JsonConvert.SerializeObject(rs));

            return (rs);

            #endregion
        }
    }
}