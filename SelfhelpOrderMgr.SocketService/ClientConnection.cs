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
    /// �Ϳͻ������ӵ�ͨ����(�ں�Socket)
    /// </summary>
    class ClientConnection
    {
        Thread threadClient = null;
        Socket socket = null;
        FrmMain frmMain = null;
        bool doesClose = false;
        private readonly static string sendActionFlag = AppLinkHelper.getSendActionFlag();
        //���ݰ����ĳ��ȣ�Ĭ����32 λ
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

        #region �����ͻ�����Ϣ - WatchMsg()

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

                        //============���Ⲣ�������ת����JSon=========================
                        T_Criminal criminal=null;
                        string msgData ;
                        //ȥ�������ȣ�Ĭ����4λ
                        string strBagLen = strMsgRec.Substring(0, bagAllLength);
                        strMsgRec = strMsgRec.Substring(4);

                        string action = "0007";//Ĭ�϶������ж��Ƿ�����
                        switch(sendActionFlag)
                        {
                            case "0"://������ԭ��ת���ģ�������Ҫȥ��ǰ��32λ
                                {
                                    action = strMsgRec.Substring(0, 4);
                                    msgData = strMsgRec.Substring(bagHeadLength);
                                }break;
                            case "1"://������ֻת��Data����
                                {
                                    msgData = strMsgRec.Substring(bagHeadLength);
                                }break;
                            case "2"://q������ת���������롱+Data����
                                {
                                    action = strMsgRec.Substring(0, 4);
                                    msgData = strMsgRec.Substring(4);
                                }break;
                            default://Ĭ����ԭ��ת���ģ�������Ҫȥ��ǰ��32λ
                                {
                                    action = strMsgRec.Substring(0, 4);
                                    msgData = strMsgRec.Substring(bagHeadLength);
                                }break;
                        }
                        string rtnText = @"{""ResultCode"":""0000"",""ResultData"":""�÷���δ����,ϵͳ������...""}";
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
                                    rtnText = @"{""ResultCode"":""0000"",""ResultData"":""�÷���δ����,ϵͳ������...""}";
                                }break;
                        }
                        
                        

                        //=====================================
                        //����ǰλ4λ�����ݰ��ĳ���.
                        byte[] orgTmpByte = Encoding.UTF8.GetBytes(rtnText);
                        rtnText = orgTmpByte.Length.ToString("0000") + rtnText;
                        byte[] orgByte = Encoding.UTF8.GetBytes(rtnText);
                        //byte[] orgByte = Encoding.UTF8.GetBytes("����_" + strMsgRec +",������:" + rtnText);
                        //byte[] finalByte = new byte[orgByte.Length + 1];
                        //finalByte[0] = 0;//�������ͱ�־��������Ϣ
                        //Buffer.BlockCopy(orgByte,0, finalByte, 1,orgByte.Length);
                        //socket.Send(finalByte);
                        socket.Send(orgByte);
                                               

                        ShowMsg(socket.RemoteEndPoint.ToString() + "�յ��ǣ�" + strMsgRec);
                        Close();//�رտͻ�������
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (socket != null)
                            ShowErr("�ͻ���" + socket.RemoteEndPoint.ToString() + "�Ͽ�����:", ex);
                        frmMain.RemoveListItem(socket.RemoteEndPoint.ToString());
                    }
                    catch
                    { }
                    break;
                }
            }
        } 
        #endregion

        #region ������Ϣ
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

        #region ���Ͷ���
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

        #region �����ļ�
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="fileName">�ļ�·��</param>
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

        #region �ر���ͻ������� - Close()
        public void Close()
        {
            doesClose = true;
            threadClient.Abort();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        } 
        #endregion

        #region �ڴ����������ʾ��Ϣ
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
