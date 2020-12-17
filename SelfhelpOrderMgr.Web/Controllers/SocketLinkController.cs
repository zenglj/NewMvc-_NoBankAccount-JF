using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class SocketLinkController : Controller
    {


        private readonly static string bankIp = ConfigurationManager.ConnectionStrings["bankIp"].ConnectionString;
        private readonly static string bankPort = ConfigurationManager.ConnectionStrings["bankPort"].ConnectionString;
        private readonly static string bankUserCode = ConfigurationManager.ConnectionStrings["bankUserCode"].ConnectionString;
        private readonly static string id_Type = ConfigurationManager.ConnectionStrings["id_Type"].ConnectionString;
        
        //
        // GET: /SocketLink/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchUser()
        {
            Socket clientSocket;
            string fcode = Request["fcode"];
            string action = Request["action"];
            string reqInfo = Request["reqInfo"];

            //==============================================================
            //开启Socket边连接

             byte [] data = new   byte [ 1024 ];
             clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             string ipadd = "192.168.0.250";
            int port = 6000;
            IPEndPoint ie = new  IPEndPoint(IPAddress.Parse(ipadd),port); // 服务器的IP和端口 

             try 
            {
                 // 因为客户端只是用来向特定的服务器发送信息，所以不需要绑定本机的IP和端口。不需要监听。 
                clientSocket.Connect(ie);
            }
             catch 
            {
                return Content(" unable to connect to server ");
            }

             string stringdata = SendRecvSocketData(action,reqInfo, clientSocket);

             //clientSocket.Close();
             //clientThread.Abort();
            return Content(stringdata);


        }


        private string SendRecvSocketData(string action, string data, Socket clientSocket)
        {
            #region 建新测试成功的代码示例
            //2019-01-09注释的
            ////发送请求数据并接收
            //string stringdata = "";
            //string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //string head = action + "999905913501" + date + " ";
            //string msgSend = head + data;
            ////加长度4位
            //byte[] orgTmpByte = Encoding.UTF8.GetBytes(msgSend);
            //msgSend = orgTmpByte.Length.ToString("0000") + msgSend;
            //byte[] orgByte = Encoding.UTF8.GetBytes(msgSend);
            //clientSocket.Send(orgByte); 
            #endregion


            //发送请求数据并接收
            string stringdata = "";
            //string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //string head = action + "999905913501" + date + " ";
            //string msgSend = head + data;

            //报文头
            string Tran_code = "0005";	//交易代码	Char	4
            string Ret_code = "9999";	//返回码	Char	4
            string Tran_date = "";	//交易日期	Char	8
            string Tran_time = "";	//交易时间	Char	6
            string Act_date = "        ";	//帐务日期	Char	8
            string Bank_rrn = "            ";	//银行方流水号	Char	12
            string Merch_rrn = "000000000000";	//代理方流水号	Char	12
            string Txn_terminal = "001         ";	//交易终端	Char	12
            string Txn_teller = "001       ";	//交易柜员	Char	10
            string Txn_curr = "CNY ";//交易货币	Char	4
            string MERCH_no = "          ";	//代理单位编号	Char	10
            string Bank_no = "          ";	//银行编号	Char 	10
            string Ret_desc = "                                                  ";	//返回码描述	Char	50

            //银行余额请求报文主体
            string Merch_cus_id = "                         ";	//外单位客户号	Char	25
            string Bank_card_no = "                    ";	//银行卡号	Char	20
            string Cus_Name = "                              ";	//客户姓名	Char	30
            string Cus_id_type = "  ";	//证件类型	Char	2
            string Cus_id_no = "                    ";	//证件号	Char	20

            string pageLeng = "0247";

            //报文头
            string pageTop = Tran_code
                    + Ret_code
                    + Tran_date
                    + Tran_time
                    + Act_date
                    + Bank_rrn
                    + Merch_rrn
                    + Txn_terminal
                    + Txn_teller
                    + Txn_curr
                    + MERCH_no
                    + Bank_no
                    + Ret_desc;

            //余额查询报文主体
            string pageRequest = Merch_cus_id
                            + Bank_card_no
                            + Cus_Name
                            + Cus_id_type
                            + Cus_id_no;

            //string msgSend = "02470005999920190308142733                    000000000519001         001       CNY 00006485                                                              3518008929               6217856400018877549 杨群                          113518008929          ";

            string msgSend = pageLeng + pageTop + pageRequest;
            //加长度4位
            byte[] orgTmpByte = Encoding.UTF8.GetBytes(msgSend);
            //msgSend = orgTmpByte.Length.ToString("0000") + msgSend;
            //byte[] orgByte = Encoding.UTF8.GetBytes(msgSend);
            //msgSend = "0247" + msgSend;
            byte[] orgByte = Encoding.Default.GetBytes(msgSend);
            clientSocket.Send(orgByte);

            //==================================================
            //接收数据


            byte[] msgByte = new byte[1024 * 1024 * 2];
            int length = 0;
            try
            {
                length = clientSocket.Receive(msgByte, msgByte.Length, SocketFlags.None);
                if (length > 0)
                {
                    stringdata = ("对方说：" + Encoding.Default.GetString(msgByte, 0, length));
                    //if (msgByte[0] == 0)//接收文字
                    //{
                    //    stringdata = ("对方说：" + Encoding.UTF8.GetString(msgByte, 1, length - 1));
                    //}
                    //else
                    //{
                    //    stringdata = "unknow func";
                    //}
                }
            }
            catch
            {
                stringdata = "recvice data fail";
            }

            //断开连接

            return stringdata;
        }



        #region 中行模式余额查询

        /// <summary>
        /// 查询银行余额
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchBankAmount()
        {
            Socket clientSocket;
            string fcode = Request["fcode"];
            string action = Request["action"];
            string reqInfo = Request["reqInfo"];



            string stringdata;
            string msgSend;
            msgSend=SetBankRequestInfo(fcode, out stringdata);

            //==============================================================
            //开启Socket边连接

            byte[] data = new byte[1024];
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //string ipadd = "192.168.0.250";
            string ipadd = bankIp;
            int port = Convert.ToInt32(bankPort);
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), port); // 服务器的IP和端口 

            try
            {
                // 因为客户端只是用来向特定的服务器发送信息，所以不需要绑定本机的IP和端口。不需要监听。 
                clientSocket.Connect(ie);
            }
            catch
            {
                return Content(" unable to connect to server ");
            }

            if (msgSend == "")
            {
                return Content("无此账户");
            }
            //加长度4位
            byte[] orgTmpByte = Encoding.UTF8.GetBytes(msgSend);
            byte[] orgByte = Encoding.Default.GetBytes(msgSend);
            clientSocket.Send(orgByte);

            //==================================================
            //接收数据


            byte[] msgByte = new byte[1024 * 1024 * 2];
            int length = 0;
            try
            {
                length = clientSocket.Receive(msgByte, msgByte.Length, SocketFlags.None);
                if (length > 0)
                {
                    stringdata = (Encoding.Default.GetString(msgByte, 0, length));
                    //if (msgByte[0] == 0)//接收文字
                    //{
                    //    stringdata = ("对方说：" + Encoding.UTF8.GetString(msgByte, 1, length - 1));
                    //}
                    //else
                    //{
                    //    stringdata = "unknow func";
                    //}
                }
            }
            catch
            {
                stringdata = "recvice data fail";
            }

            //断开连接

            //return stringdata;

            //string stringdata = SendInfoToBankBySocket(fcode, clientSocket);

            //clientSocket.Close();
            //clientThread.Abort();
            
            string returnInfo="";
            string retrunCode=stringdata.Substring(8,4);
            switch(retrunCode){
                case "0000":  {
                    //returnInfo = "成功";
                    returnInfo="余额:"+(Convert.ToDecimal( stringdata.Substring(stringdata.Length - 13, 13))/100).ToString();
                }break;
                case "0001":  {
                    returnInfo="卡已挂失";
                }break;
                case "0002":{
                    returnInfo="卡已销户";
                }break;  
                case "0003":{
                    returnInfo="密码不符";
                }break;  
                case "0004": {
                    returnInfo="余额不足";
                }break; 
                case "0005":{
                    returnInfo="无此帐号";
                }break;  
                case "0006":{
                    returnInfo="不允许转帐";
                }break;  
                case "0007":{
                    returnInfo="帐户状态异常";
                }break;  
                case "0008":{
                    returnInfo="密钥不同步";
                }break;  
                case "0009":{
                    returnInfo="银行网络故障";
                }break;  
                case "0010":{
                    returnInfo="超时";
                }break;  
                case "0011":{
                    returnInfo="无此签约信息";
                }break;  
                case "0012":{
                    returnInfo="服务暂停";
                }break;  
                case "0013": {
                    returnInfo="不存在该笔流水";
                }break; 
                case "0014":{
                    returnInfo="正在对帐，无法交易";
                }break;   
            }

            

            //return Content(stringdata);

            return Content(returnInfo);


        }

        private static string SetBankRequestInfo(string fcode, out string stringdata)
        {
            //发送请求数据并接收
            stringdata = "";
            //string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //string head = action + "999905913501" + date + " ";
            //string msgSend = head + data;

            //报文头
            string Tran_code = "0005";	//交易代码	Char	4
            string Ret_code = "9999";	//返回码	Char	4
            string Tran_date = "        ";	//交易日期	Char	8
            string Tran_time = "      ";	//交易时间	Char	6
            string Act_date = "        ";	//帐务日期	Char	8
            string Bank_rrn = "            ";	//银行方流水号	Char	12
            string Merch_rrn = "000000000000";	//代理方流水号	Char	12
            string Txn_terminal = "001         ";	//交易终端	Char	12
            string Txn_teller = "001       ";	//交易柜员	Char	10
            string Txn_curr = "CNY ";//交易货币	Char	4
            string MERCH_no = "          ";	//代理单位编号	Char	10
            string Bank_no = "          ";	//银行编号	Char 	10
            string Ret_desc = "                                                  ";	//返回码描述	Char	50

            //银行余额请求报文主体
            string Merch_cus_id = "                         ";	//外单位客户号	Char	25
            string Bank_card_no = "                    ";	//银行卡号	Char	20
            string Cus_Name = "                              ";	//客户姓名	Char	30
            string Cus_id_type = "  ";	//证件类型	Char	2
            string Cus_id_no = "                    ";	//证件号	Char	20

            string pageLeng = "0247";


            T_Criminal criminal = new T_CriminalBLL().GetModel(fcode);
            if (criminal == null)
            {
                return "";
            }
            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(fcode);
            if (card == null)
            {
                return "";
            }

            Tran_date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
            Tran_time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");

            Merch_rrn = Merch_rrn + DateTime.Now.Date.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            Merch_rrn = Merch_rrn.Substring(Merch_rrn.Length - 12, 12);
            //代理单位编号
            MERCH_no = bankUserCode +MERCH_no;
            MERCH_no = MERCH_no.Substring(0,10);

            //用户编号
            Merch_cus_id = criminal.FCode + Merch_cus_id;
            Merch_cus_id = Merch_cus_id.Substring(0, 25);
            //银行卡号
            Bank_card_no = card.BankAccNo + Bank_card_no;
            Bank_card_no = Bank_card_no.Substring(0, 20);
            //用户姓名
            Cus_Name = criminal.FName + Cus_Name.Substring(0,30-criminal.FName.Length*2);
            //Cus_Name = criminal.FName+Cus_Name.Substring(0, 30);
            //证件类型
            Cus_id_type = id_Type + Cus_id_type;
            Cus_id_type = Cus_id_type.Substring(0, 2);
            //证件号码
            Cus_id_no = criminal.FCode + Cus_id_no;
            Cus_id_no = Cus_id_no.Substring(0, 20);

            //报文头
            string pageTop = Tran_code
                    + Ret_code
                    + Tran_date
                    + Tran_time
                    + Act_date
                    + Bank_rrn
                    + Merch_rrn
                    + Txn_terminal
                    + Txn_teller
                    + Txn_curr
                    + MERCH_no
                    + Bank_no
                    + Ret_desc;

            //余额查询报文主体
            string pageRequest = Merch_cus_id
                            + Bank_card_no
                            + Cus_Name
                            + Cus_id_type
                            + Cus_id_no;

            //string msgSend = "02470005999920190308142733                    000000000519001         001       CNY 00006485                                                              3518008929               6217856400018877549 杨群                          113518008929          ";

            string msgSend = pageLeng + pageTop + pageRequest;
            return msgSend;
        }


        #endregion



	}
}