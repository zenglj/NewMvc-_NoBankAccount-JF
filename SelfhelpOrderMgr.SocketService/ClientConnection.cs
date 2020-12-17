using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;


using System.Data;
using System.Linq;
using System.Web;
using SelfhelpOrderMgr.Common;


namespace SelfhelpOrderMgr.SocketService
{
    /// <summary>
    /// 和客户端连接的通道类(内含Socket)
    /// </summary>
    class ClientConnection
    {
        Thread threadClient = null;
        Socket socket = null;
        FrmMain frmMain = null;
        bool doesClose = false;
        private readonly static string sendActionFlag = AppLinkHelper.getSendActionFlag();
        //数据包部的长度，默认是32 位
        private readonly static int bagHeadLength = AppLinkHelper.getBagHeadLength();
        private int bagAllLength=4;
        public ClientConnection(FrmMain frmMain,Socket socket)
        {
            this.frmMain = frmMain;
            this.socket = socket;
            threadClient = new Thread(WatchMsg);
            threadClient.IsBackground = true;
            threadClient.Start();
        }

        #region 监听客户端消息 - WatchMsg()

        private void WatchMsg()
        {
            while (!doesClose)
            {
                try
                {
                    byte[] byteMsgRec = new byte[1024 * 1024 * 4];
                    int length = socket.Receive(byteMsgRec, byteMsgRec.Length, SocketFlags.None);
                    if (length > 0)
                    {
                        //string strMsgRec = Encoding.UTF8.GetString(byteMsgRec, 1, length - 1);
                        string strMsgRec = Encoding.UTF8.GetString(byteMsgRec, 0, length );

                        //============连库并处理或是转换成JSon=========================
                        T_Criminal criminal=null;
                        string msgData ;
                        //去除包长度，默认是4位
                        string strBagLen = strMsgRec.Substring(0, bagAllLength);
                        strMsgRec = strMsgRec.Substring(4);

                        string action = "0007";//默认动作是判断是否在线
                        switch(sendActionFlag)
                        {
                            case "0"://银行是原包转发的，所以需要去掉前面32位
                                {
                                    action = strMsgRec.Substring(0, 4);
                                    msgData = strMsgRec.Substring(bagHeadLength);
                                }break;
                            case "1"://银行是只转发Data部分
                                {
                                    msgData = strMsgRec.Substring(bagHeadLength);
                                }break;
                            case "2"://q银行是转发“交易码”+Data部份
                                {
                                    action = strMsgRec.Substring(0, 4);
                                    msgData = strMsgRec.Substring(4);
                                }break;
                            default://默认是原包转发的，所以需要去掉前面32位
                                {
                                    action = strMsgRec.Substring(0, 4);
                                    msgData = strMsgRec.Substring(bagHeadLength);
                                }break;
                        }
                        string rtnText = @"{""ResultCode"":""0000"",""ResultData"":""该方法未定义,系统建设中...""}";
                        switch(action)
                        {
                            case "0001":
                                {
                                    UserEntity ss = (UserEntity)new JsonHelper().ToEntity<UserEntity>(msgData);
                                    criminal = new T_CriminalBLL().GetModel(ss.FCode);
                                    if (criminal == null)
                                    {
                                        rtnText = @"{""ResultCode"":""0000"",""ResultData"":""No Find Info""}";
                                    }
                                    else
                                    {
                                        T_Criminal_card card = new T_Criminal_cardBLL().GetModel(ss.FCode);
                                        if(card==null)
                                        {
                                            card = new T_Criminal_card();
                                        }
                                        CriminalEntity userInfo = new CriminalEntity() { 
                                            FName=criminal.FName,
                                            FCode=criminal.FCode,
                                            FFlag=criminal.fflag,
                                            BankAccNo = card.BankAccNo,
                                            AmountA=card.AmountA,
                                            AmountB = card.AmountB,
                                            AmountC = card.AmountC,
                                            BankAmount = card.BankAmount
                                        };

                                        ResultEntity rData = new ResultEntity() { 
                                            ResultCode="1111",
                                            ResultData = userInfo
                                        };
                                        rtnText = new JsonHelper().ToJson(rData);
                                    }
                                }break;
                            case "0002":
                                {
                                    rtnText = strMsgRec.Substring(35);
                                } break;
                            case "0003":
                                {
                                    rtnText = strMsgRec.Substring(35);
                                } break;
                            case "0004":
                                {
                                    rtnText = strMsgRec.Substring(35);
                                } break;
                            case "0005":
                                {
                                    rtnText = strMsgRec.Substring(35);
                                } break;
                            case "0006":
                                {
                                     rtnText = strMsgRec.Substring(35);
                                } break;
                            case "0007":
                                {
                                    rtnText = strMsgRec.Substring(35);
                                } break;
                            default:
                                {
                                    rtnText = @"{""ResultCode"":""0000"",""ResultData"":""该方法未定义,系统建设中...""}";
                                }break;
                        }
                        
                        

                        //=====================================
                        //加上前位4位，数据包的长度.
                        byte[] orgTmpByte = Encoding.UTF8.GetBytes(rtnText);
                        rtnText = orgTmpByte.Length.ToString("0000") + rtnText;
                        byte[] orgByte = Encoding.UTF8.GetBytes(rtnText);
                        //byte[] orgByte = Encoding.UTF8.GetBytes("接收_" + strMsgRec +",返回是:" + rtnText);
                        //byte[] finalByte = new byte[orgByte.Length + 1];
                        //finalByte[0] = 0;//传输类型标志：文字消息
                        //Buffer.BlockCopy(orgByte,0, finalByte, 1,orgByte.Length);
                        //socket.Send(finalByte);
                        socket.Send(orgByte);
                                               

                        ShowMsg(socket.RemoteEndPoint.ToString() + "收到是：" + strMsgRec);
                        Close();//关闭客户的连接
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (socket != null)
                            ShowErr("客户端" + socket.RemoteEndPoint.ToString() + "断开连接:", ex);
                        frmMain.RemoveListItem(socket.RemoteEndPoint.ToString());
                    }
                    catch
                    { }
                    break;
                }
            }
        } 
        #endregion

        #region 发送消息
        public void SendMsg(string msg)
        {
            try
            {
                byte[] msgSendByte = Encoding.UTF8.GetBytes(msg);
                byte[] finalByte = new byte[msgSendByte.Length + 1];
                finalByte[0] = 0;
                Buffer.BlockCopy(msgSendByte, 0, finalByte, 1, msgSendByte.Length);
                socket.Send(finalByte);
            }
            catch (Exception ex)
            {
                ShowErr("SendMsg(string msg)", ex);
            }
        } 
        #endregion

        #region 发送抖动
        public void SendShake()
        {
            try
            {
                byte[] finalByte = new byte[1];
                finalByte[0] = 2;
                socket.Send(finalByte);
            }
            catch (Exception ex)
            {
                ShowErr("SendShake()", ex);
            }
        }
        #endregion

        #region 发送文件
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public void SendFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                byte[] byteFile = new byte[1024 * 1024 * 5];
                int length = fs.Read(byteFile, 0, byteFile.Length);
                if (length > 0)
                {
                    byte[] byteFinalFile = new byte[length + 1];
                    byteFinalFile[0] = 1;
                    Buffer.BlockCopy(byteFile, 0, byteFinalFile, 1, length);
                    socket.Send(byteFinalFile);
                }
            }
            catch (Exception ex)
            {
                ShowErr("SendFile(string fileName)", ex);
            }
            finally
            {
                fs.Close();
            }
        } 
        #endregion

        #region 关闭与客户端连接 - Close()
        public void Close()
        {
            doesClose = true;
            threadClient.Abort();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        } 
        #endregion

        #region 在窗体面板上显示消息
        private void ShowErr(string msg, Exception ex)
        {
            frmMain.ShowErr(msg, ex);
        }

        private void ShowMsg(string msg)
        {
            frmMain.ShowMsg(msg);
        } 
        #endregion
    }
}
