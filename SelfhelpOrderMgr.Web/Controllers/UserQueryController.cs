using SelfhelpOrderMgr.BLL;
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
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class UserQueryController : BaseMenuController
    {
        private readonly static string bankIp = ConfigurationManager.ConnectionStrings["bankIp"].ConnectionString;
        private readonly static string bankPort = ConfigurationManager.ConnectionStrings["bankPort"].ConnectionString;
        private readonly static string bankUserCode = ConfigurationManager.ConnectionStrings["bankUserCode"].ConnectionString;
        private readonly static string id_Type = ConfigurationManager.ConnectionStrings["id_Type"].ConnectionString;

        JifenMgrService _jifenMgrService = new JifenMgrService();
        //
        // GET: /UserQuery/
        public ActionResult Index()
        {
            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");

            var fcodeInfo = Request["fcrimecode"];
            string fcode = "";

            if (fcodeInfo != null && fcodeInfo.ToString().Contains("|"))
            {
                string[] words = fcodeInfo.ToString().Split(new char[] { '|' }); // 使用逗号和分号作为分隔符
                
                if (MD5ProcessHelper.GetMD5(words[0]) == words[1])
                {
                    fcode = words[0];
                }
            }
            ViewData["fcrimecode"] = fcode;

            ViewData["loginMode"] = loginMode.MgrValue;
            //ViewData["fcrimecode"] = Request["fcrimecode"];
            


            T_SHO_ManagerSet softNumerKeyBoard = new T_SHO_ManagerSetBLL().GetModel("SoftNumerKeyBoard");
            if (softNumerKeyBoard == null)
            {
                ViewData["softNumerKeyBoard"] = "0";
            }
            else
            {
                ViewData["softNumerKeyBoard"] = softNumerKeyBoard.MgrValue;
            }
            return View();

        }

        //查询用户的出监账户的信息
        public ActionResult QueryUserInfo()
        {
            string fcardCode = Request["FCardCode"];

            string startDate = Request["startDate"];
            string endDate = Request["endDate"];

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string status = "Error|查询失败";
            if (fcardCode.Length != 10)
            {
                return Content(status);
            }
            List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode.ToString() + "'");

            if (cards.Count == 0)
            {
                status = "Error|该卡找不对应人员信息";
                return Content(status);
            }
            string fcrimeCode = cards[0].fcrimecode;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, 1);
            //获取查询到的近三个月的存款记录

            T_SHO_ManagerSet msetHide = new T_SHO_ManagerSetBLL().GetModel("HideBankCardFlag");
            if (msetHide != null && msetHide.MgrValue == "1")
            {
                cards[0].BankAccNo = "*******************";
                cards[0].SecondaryBankCard = "*******************";
                criminal.BankCardNo = "******************";
            }


            string vcrdWhere = "flag=0 and fcrimecode='" + criminal.FCode + "' and crtdate>='" + DateTime.Now.AddMonths(-3).ToShortDateString() + "' and crtdate<'" + DateTime.Now.AddDays(1).ToShortDateString() + "'";
            if (!(string.IsNullOrWhiteSpace(startDate) && string.IsNullOrWhiteSpace(endDate)))
            {
                vcrdWhere = "flag=0 and fcrimecode='" + criminal.FCode + "' and crtdate>='" + startDate + "'and crtdate<'" + Convert.ToDateTime(endDate).AddDays(1).ToShortDateString() + "'";
            }

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList(100, vcrdWhere, " CrtDate desc");
            for (int i = 0; i < vcrds.Count; i++)
            {

                if (vcrds[i].TypeFlag == 51)
                {
                    if (!string.IsNullOrWhiteSpace(vcrds[i].Remark))
                    {
                        int ileft = vcrds[i].Remark.IndexOf("(");
                        int iright = vcrds[i].Remark.IndexOf(")");
                        string userName = vcrds[i].Remark.Substring(0, ileft);
                        string phoneNum = vcrds[i].Remark.Substring(iright + 1, vcrds[i].Remark.Length - iright - 1);

                        vcrds[i].Remark = userName.Substring(0, 1) + "*" + userName.Substring(2)
                            + "(关系:**)" + phoneNum.Substring(0, 3) + "****" + phoneNum.Substring(7);
                    }
                }
            }

            List<T_JF_Vcrd> jfvcrds = _jifenMgrService.QueryList<T_JF_Vcrd>(vcrdWhere);
            rtnQueryUserInfo userinfo = new rtnQueryUserInfo();
            userinfo.UserInfo = criminal;
            userinfo.UserCard = cards[0];
            userinfo.vcrds = vcrds;
            userinfo.jfvcrds = jfvcrds.OrderByDescending(o => o.CrtDate).ToList();
            userinfo.bankTimeMoney = "";

            //判断是否要执行银行实时余额查询
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("openBankTimeSearchFlag");
            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    if (cards.Count > 0)
                    {
                        if (cards[0].BankAccNo != "")
                        {
                            userinfo.bankTimeMoney = SearchBankAmount(criminal.FCode);
                        }
                    }
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();

            status = jss.Serialize(userinfo);
            status = "There|" + status;
            return Content(status);
        }

        //获取用户IC卡信息
        public ActionResult GetCardInfo()
        {
            string userCode = Request["userCode"];
            string userPwd = Request["userPwd"];
            if (string.IsNullOrEmpty(userCode))
            {
                return Content("Err|用户名或是密码有错");
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                userPwd = "";
            }
            List<T_ICCARD_LIST> cardInfos = new T_ICCARD_LISTBLL().GetModelList("FCrimeCode='" + userCode + "' and isnull(FPWD,'')='" + userPwd + "' and FFlag=1");
            if (cardInfos.Count == 1)
            {
                return Content("OK|" + cardInfos[0].CardCode.ToString());
            }
            return Content("Err|用户名或是密码有错");
        }

        #region 中行模式余额查询

        /// <summary>
        /// 查询银行余额
        /// </summary>
        /// <returns></returns>
        public string SearchBankAmount(string fcode)
        {
            Socket clientSocket;
            //string fcode = Request["fcode"];
            //string action = Request["action"];
            //string reqInfo = Request["reqInfo"];



            string stringdata;
            string msgSend;
            msgSend = SetBankRequestInfo(fcode, out stringdata);

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
                return ("无法连接银行服务器");
            }

            if (msgSend == "")
            {
                return ("无此账户");
            }
            //加长度4位
            byte[] orgTmpByte = Encoding.UTF8.GetBytes(msgSend);
            byte[] orgByte = Encoding.Default.GetBytes(msgSend);
            clientSocket.Send(orgByte);
            Log4NetHelper.logger.Info("发送银行实时余额查询：" + msgSend);
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

            clientSocket.Close();
            //clientThread.Abort();
            Log4NetHelper.logger.Info("接收银行返回余额结果：" + stringdata);
            string returnInfo = "";
            string retrunCode = stringdata.Substring(8, 4);
            switch (retrunCode)
            {
                case "0000":
                    {
                        //returnInfo = "成功";
                        returnInfo = (Convert.ToDecimal(stringdata.Substring(stringdata.Length - 13, 13)) / 100).ToString();
                    }
                    break;
                case "0001":
                    {
                        returnInfo = "卡已挂失";
                    }
                    break;
                case "0002":
                    {
                        returnInfo = "卡已销户";
                    }
                    break;
                case "0003":
                    {
                        returnInfo = "密码不符";
                    }
                    break;
                case "0004":
                    {
                        returnInfo = "余额不足";
                    }
                    break;
                case "0005":
                    {
                        returnInfo = "无此帐号";
                    }
                    break;
                case "0006":
                    {
                        returnInfo = "不允许转帐";
                    }
                    break;
                case "0007":
                    {
                        returnInfo = "帐户状态异常";
                    }
                    break;
                case "0008":
                    {
                        returnInfo = "密钥不同步";
                    }
                    break;
                case "0009":
                    {
                        returnInfo = "银行网络故障";
                    }
                    break;
                case "0010":
                    {
                        returnInfo = "超时";
                    }
                    break;
                case "0011":
                    {
                        returnInfo = "无此签约信息";
                    }
                    break;
                case "0012":
                    {
                        returnInfo = "服务暂停";
                    }
                    break;
                case "0013":
                    {
                        returnInfo = "不存在该笔流水";
                    }
                    break;
                case "0014":
                    {
                        returnInfo = "正在对帐，无法交易";
                    }
                    break;
            }



            //return Content(stringdata);

            return (returnInfo);


        }

        private static string SetBankRequestInfo(string fcode, out string stringdata)
        {
            //发送请求数据并接收
            stringdata = "";
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
            MERCH_no = bankUserCode + MERCH_no;
            MERCH_no = MERCH_no.Substring(0, 10);

            //用户编号
            Merch_cus_id = criminal.FCode + Merch_cus_id;
            Merch_cus_id = Merch_cus_id.Substring(0, 25);
            //银行卡号
            Bank_card_no = card.BankAccNo + Bank_card_no;
            Bank_card_no = Bank_card_no.Substring(0, 20);
            //用户姓名
            Cus_Name = criminal.FName + Cus_Name.Substring(0, 30 - criminal.FName.Length * 2);
            //Cus_Name = criminal.FName+Cus_Name.Substring(0, 30);
            //证件类型
            Cus_id_type = id_Type + Cus_id_type;
            Cus_id_type = Cus_id_type.Substring(0, 2);
            //证件号码
            if ("01" == Cus_id_type)//身份证号
            {
                Cus_id_no = criminal.FIdenNo + Cus_id_no;
            }
            else//狱政号
            {
                Cus_id_no = criminal.FCode + Cus_id_no;
            }
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




        #region 账户查询模块

        public ActionResult QueryAccount()
        {
            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["loginMode"] = loginMode.MgrValue;

            T_SHO_ManagerSet softNumerKeyBoard = new T_SHO_ManagerSetBLL().GetModel("SoftNumerKeyBoard");
            if (softNumerKeyBoard == null)
            {
                ViewData["softNumerKeyBoard"] = "0";
            }
            else
            {
                ViewData["softNumerKeyBoard"] = softNumerKeyBoard.MgrValue;
            }
            return View();

        }

        BaseDapperBLL _baseBLL = new BaseDapperBLL();

        //查询用户的汇款人账户的信息
        public ActionResult SearchAccountInfo()
        {
            ResultInfo rs = new ResultInfo();

            string fcardCode = Request["FCardCode"];

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string status = "Error|查询失败";
            if (fcardCode.Length != 10)
            {
                return Content(status);
            }
            List<T_Criminal_card> cards = (List<T_Criminal_card>)new T_Criminal_cardBLL().GetModelList("CardCodeA='" + fcardCode.ToString() + "'");

            if (cards.Count == 0)
            {
                rs.ReMsg = "Error|该卡找不对应人员信息";
                return Json(rs);
            }
            string fcrimeCode = cards[0].fcrimecode;
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimeCode, 1);


            List<T_Bank_Rcv> rcvs = _baseBLL.QueryList<T_Bank_Rcv>("select distinct fractname,fractnactacn,fractnibknum,fractnibkname from t_bank_rcv where fcrimecode=@fcrimecode", new { fcrimecode = fcrimeCode });

            var scans = _baseBLL.QueryList<T_Bank_Rcv>("select distinct isnull(Familyname+'('+ relation +')','') fractname,PhoneNum fractnactacn,a.Source fractnibknum, a.Source as fractnibkname from T_Bank_Recharge a,t_bank_posdetail b where a.fcode=@fcrimecode and a.payls=b.orderNo", new { fcrimecode = fcrimeCode });

            foreach (var item in scans)
            {
                rcvs.Add(item);
            }

            rtnQueryAccountInfo userinfo = new rtnQueryAccountInfo();
            userinfo.UserInfo = criminal;
            userinfo.rcvs = rcvs;

            JavaScriptSerializer jss = new JavaScriptSerializer();

            rs.Flag = true;
            rs.ReMsg = "OK|获取成功";
            rs.DataInfo = userinfo;

            return Json(rs);
        }

        #endregion


    }

    //执行结果的返回状态
    public class rtnQueryUserInfo
    {
        public T_Criminal UserInfo { get; set; }//犯人信息信息
        public T_Criminal_card UserCard { get; set; }//IC卡信息
        public List<T_Vcrd> vcrds { get; set; }//存款记录
        public List<T_JF_Vcrd> jfvcrds { get; set; }//存款记录
        public string bankTimeMoney { get; set; }//银行实时余额

    }


    //执行结果的返回状态
    public class rtnQueryAccountInfo
    {
        public T_Criminal UserInfo { get; set; }//犯人信息信息
        public List<T_Bank_Rcv> rcvs { get; set; }//汇款人记录

    }
}