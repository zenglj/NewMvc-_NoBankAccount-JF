using Newtonsoft.Json;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Transactions;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    //[MyLogActionFilterAttribute]
    public class BankATMController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        BankAtmService bll = new BankAtmService();
        ResultInfo rs = new ResultInfo() {
            Flag = false,
            ReMsg = "未处理",
            DataInfo = null
        };

        public ActionResult GetToken(string ipAddr, string pwd)
        {
            if (string.IsNullOrWhiteSpace(ipAddr))
            {
                return Content("Error|请输入IP地址");
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                return Content("Error|请输入传输密码");
            }

            string strJsonWhere = jss.Serialize(new { IPAddr = ipAddr, Pwd = pwd });
            var m = new BankAtmService().GetModelFirst<T_Bank_AtmMachine, T_Bank_AtmMachine>(strJsonWhere);
            if (m == null)
            {
                return Content("Error|访问IP地址未授权.");
            }
            var ss = MD5ProcessHelper.GetMD5Token(m.Pwd, ipAddr);
            return Content("OK|token:" + ss);
        }

        [MyLogActionFilterAttribute]
        public ActionResult AtmCall(ReqDataAtm reqJson)
        {
            if (string.IsNullOrWhiteSpace(reqJson.Action) == false)
            {
                try
                {
                    string strF_AMOUNT = (Convert.ToDecimal(reqJson.Data["F_AMOUNT"]) / 100).ToString();
                    switch (reqJson.Action)
                    {
                        case "ClearMachine"://清机
                            {
                                var _data = new reqAtmActionInfo()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"]+reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"]
                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.ClearMachine(_data);
                            }
                            break;
                        case "GetCardInfo": //读卡
                            {
                                var _data = new ReqCardCode()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],
                                    CardCode = reqJson.Data["CardCode"],
                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.GetCardInfo(_data);
                            }
                            break;
                        case "AtmWithdrawMoney"://取款
                            {
                                var _data = new ReqWithDrawal()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    F_AMOUNT = strF_AMOUNT,
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],
                                    CardCode = reqJson.Data["CardCode"],
                                    WithdrawalPassword = reqJson.Data["WithdrawalPassword"],
                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.AtmWithdrawMoney(_data);
                            }
                            break;
                        case "UpdateCashOutStatus"://更新出钞状态
                            {
                                var _data = new ReqCashOut()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],
                                    CardCode = reqJson.Data["CardCode"],
                                    WithdrawalPassword = reqJson.Data["WithdrawalPassword"],
                                    PayId = Convert.ToInt32(reqJson.Data["PayId"]),
                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.UpdateCashOutStatus(_data);
                            }
                            break;
                        case "CashOutRectification"://出钞冲正
                            {
                                var _data = new ReqCashOut()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],
                                    CardCode = reqJson.Data["CardCode"],
                                    WithdrawalPassword = reqJson.Data["WithdrawalPassword"],
                                    PayId = Convert.ToInt32(reqJson.Data["PayId"]),
                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.CashOutRectification(_data);
                            } break;
                        case "AddNotes"://加钞
                            {
                                var _data = new reqAtmActionInfo()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],

                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.AddNotes(_data);
                            }
                            break;
                        case "CashOver"://长款
                            {
                                var _data = new reqAtmActionInfo()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],

                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.CashOver(_data);
                            }
                            break;
                        case "Reconciliation"://对账
                            {
                                var _data = new reqAtmActionInfo()
                                {
                                    AtmSerialNo = reqJson.Data["TranDate"] + reqJson.Data["AtmSerialNo"],
                                    //F_AMOUNT = strF_AMOUNT,
                                    F_AMOUNT = reqJson.Data["F_AMOUNT"],
                                    MacCode = reqJson.Data["MacCode"],
                                    TranDate = reqJson.Data["TranDate"],
                                    TranTime = reqJson.Data["TranTime"],
                                };
                                //var _data=JsonConvert.DeserializeObject<reqAtmActionInfo>( ss);
                                rs = this.Reconciliation(_data);
                            }
                            break;
                        default:
                            {
                                rs.Flag = false;
                                rs.ReMsg = "未定义的动作参数，请检查ActionType的参数是否正确？";
                                rs.DataInfo = null;
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    rs.Flag = false;
                    rs.ReMsg = e.Message.ToString();
                }

            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询结算账户信息
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <returns></returns>
        private ResultInfo GetCardInfo(ReqCardCode reqJson)
        {
            string cardCode = reqJson.CardCode;
            string fcrimecode = "";
            //判断账户是否为空或者是有效
            fcrimecode = CheckParamInfo(cardCode, fcrimecode);
            if (rs.ReMsg != "未处理")
            {
                return (rs);
            }

            PageResult<ViewPaymentRecordExtend> rec;
            ViewBankUserInfo model;
            bll.GetPaymentRecordInfo(fcrimecode, 0, out rec, out model);

            if (rec.rows.Count == 1)
            {
                if (rec.rows[0].AuditFlag == 1)
                {
                    rs.Flag = true;
                    rs.ReMsg = "成功";
                    rs.DataInfo = model;
                }
                else
                {
                    rs.Flag = false;
                    rs.ReMsg = "结算余额未审核，不能支取，请与监狱管理民警联系!";
                    rs.DataInfo = null;
                }
            }
            else if (rec.rows.Count > 1)
            {
                rs.Flag = false;
                rs.ReMsg = "出现重复的记录，不能支取，请与监狱管理民警联系!";
                rs.DataInfo = null;
            }
            else
            {
                rs.Flag = false;
                rs.ReMsg = "没有找到相应的记录";
                rs.DataInfo = null;
            }



            //====================================================
            //3Des 加密方式======================================
            string ss = jss.Serialize(rs.DataInfo);
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            //key为abcdefghijklmnopqrstuvwx的Base64编码
            byte[] key = Convert.FromBase64String("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4");
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };      //当模式为ECB时，IV无用
            //byte[] data = utf8.GetBytes("中国ABCabc123");
            byte[] data = utf8.GetBytes(ss);
            System.Console.WriteLine("ECB模式:");
            byte[] str1 = Des3PasswordHelper.Des3EncodeECB(key, iv, data);
            byte[] str2 = Des3PasswordHelper.Des3DecodeECB(key, iv, str1);
            //string encstr = System.Text.Encoding.UTF8.GetString(str1);
            string encstr = Convert.ToBase64String(str1);
            string decstr = System.Text.Encoding.UTF8.GetString(str2);
            string decstrEnd = System.Text.Encoding.UTF8.GetString(str2).Replace("\0", "");

            byte[] str3 = Convert.FromBase64String(encstr); //密文转数组
            byte[] strEeee = Des3PasswordHelper.Des3DecodeECB(key, iv, str3);//解密得到明文数组
            string decstrEEE = System.Text.Encoding.UTF8.GetString(strEeee);//数组转字符串
            System.Console.WriteLine(Convert.ToBase64String(str1));
            System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(str2));
            //rs.DataInfo = encstr;
            //=================end====================================
            //====================================================================

            return (rs);
        }



        private string CheckParamInfo(string cardCode, string fcrimecode)
        {

            if (string.IsNullOrWhiteSpace(cardCode))
            {
                rs.ReMsg = "用户编号不能为空";
            }
            if (cardCode.Length < 10 && cardCode.Length > 15)
            {
                rs.ReMsg = "IC卡号不正确";

            }
            var cards = new T_Criminal_cardBLL().GetModelList("CardCodeA='" + cardCode + "'");
            if (cards.Count != 1)
            {
                rs.ReMsg = "请求的IC卡号不正确";

            }
            else
            {
                if (cards[0].cardflaga != 4)
                {
                    rs.ReMsg = "您未办理离监结算,无法取账户余款";
                }
                fcrimecode = cards[0].fcrimecode;
            }
            return fcrimecode;
        }

        /// <summary>
        /// ATM账户取款
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo AtmWithdrawMoney(ReqWithDrawal reqJson)
        {
            string cardCode = reqJson.CardCode;
            decimal money = Convert.ToDecimal(reqJson.F_AMOUNT);
            string fcrimecode = "";
            fcrimecode = this.CheckParamInfo(cardCode, fcrimecode);
            if (this.rs.ReMsg != "未处理")
            {
                return this.rs;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.WithdrawalOperation(money, reqJson.WithdrawalPassword, fcrimecode, 0, 1, "出账成功，待ATM机出钞", "出账");
                if (this.rs.Flag)
                {
                    string title = "取款";
                    string strIp = IpAddressHelper.GetHostAddress();
                    reqAtmActionInfo expr_8E = new reqAtmActionInfo();
                    expr_8E.AtmSerialNo = reqJson.AtmSerialNo.ToString();
                    expr_8E.F_AMOUNT = Convert.ToString(decimal.Zero - money);
                    DateTime now = DateTime.Now;
                    expr_8E.TranDate = now.ToString("yyyyMMdd");
                    now = DateTime.Now;
                    expr_8E.TranTime = now.ToString("hhmmss");
                    expr_8E.MacCode = reqJson.MacCode;
                    reqAtmActionInfo oInsertJson = expr_8E;
                    this.bll.InsertATMOperationREC(oInsertJson, strIp, title);
                }
                ts.Complete();
            }
            return this.rs;
        }

        /// <summary>
        /// 出钞冲正
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo CashOutRectification(ReqCashOut reqJson)
        {
            int arg_06_0 = reqJson.PayId;
            string cardCode = reqJson.CardCode;
            decimal money = Convert.ToDecimal(reqJson.F_AMOUNT);
            string fcrimecode = "";
            fcrimecode = this.CheckParamInfo(cardCode, fcrimecode);
            if (this.rs.ReMsg != "未处理")
            {
                return this.rs;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.WithdrawalOperation(money, reqJson.WithdrawalPassword, fcrimecode, 1, 0, "冲正成功，ATM出钞失败", "冲正");
                if (this.rs.Flag)
                {
                    string title = "冲正";
                    string strIp = IpAddressHelper.GetHostAddress();
                    reqAtmActionInfo expr_95 = new reqAtmActionInfo();
                    expr_95.AtmSerialNo = reqJson.AtmSerialNo.ToString();
                    expr_95.F_AMOUNT = Convert.ToString(money);
                    DateTime now = DateTime.Now;
                    expr_95.TranDate = now.ToString("yyyyMMdd");
                    now = DateTime.Now;
                    expr_95.TranTime = now.ToString("hhmmss");
                    expr_95.MacCode = reqJson.MacCode;
                    reqAtmActionInfo oInsertJson = expr_95;
                    this.bll.InsertATMOperationREC(oInsertJson, strIp, title);
                }
                ts.Complete();
            }
            return this.rs;
        }

        /// <summary>
        /// 清机
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo ClearMachine(reqAtmActionInfo reqJson)
        {
            string title = "清机";
            string strIp = IpAddressHelper.GetHostAddress();
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.InsertATMOperationREC(reqJson, strIp, title);
                ts.Complete();
            }
            return this.rs;
        }

        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo Reconciliation(reqAtmActionInfo reqJson)
        {
            string strIp = IpAddressHelper.GetHostAddress();
            string title = "对账";
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.InsertATMOperationREC(reqJson, strIp, title);
                ts.Complete();
            }
            return this.rs;
        }

        /// <summary>
        /// 更新出钞状态
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo UpdateCashOutStatus(ReqCashOut reqJson)
        {
            int arg_06_0 = reqJson.PayId;
            string cardCode = reqJson.CardCode;
            decimal money = Convert.ToDecimal(reqJson.F_AMOUNT);
            string fcrimecode = "";
            fcrimecode = this.CheckParamInfo(cardCode, fcrimecode);
            if (this.rs.ReMsg != "未处理")
            {
                return this.rs;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.WithdrawalOperation(money, reqJson.WithdrawalPassword, fcrimecode, 1, 2, "支付成功，ATM已经出钞", "出钞");
                if (this.rs.Flag)
                {
                    string title = "出钞";
                    string strIp = IpAddressHelper.GetHostAddress();
                    reqAtmActionInfo expr_95 = new reqAtmActionInfo();
                    expr_95.AtmSerialNo = reqJson.AtmSerialNo.ToString();
                    expr_95.F_AMOUNT = Convert.ToString(decimal.Zero - money);
                    DateTime now = DateTime.Now;
                    expr_95.TranDate = now.ToString("yyyyMMdd");
                    now = DateTime.Now;
                    expr_95.TranTime = now.ToString("hhmmss");
                    expr_95.MacCode = reqJson.MacCode;
                    reqAtmActionInfo oInsertJson = expr_95;
                    this.bll.InsertATMOperationREC(oInsertJson, strIp, title);
                }
                ts.Complete();
            }
            return this.rs;
        }

        
        /// <summary>
        /// 加钞
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo AddNotes(reqAtmActionInfo reqJson)
        {
            string title = "加钞";
            string strIp = IpAddressHelper.GetHostAddress();
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.InsertATMOperationREC(reqJson, strIp, title);
                ts.Complete();
            }
            return this.rs;
        }

        /// <summary>
        /// 长款
        /// </summary>
        /// <param name="reqJson"></param>
        /// <returns></returns>
        private ResultInfo CashOver(reqAtmActionInfo reqJson)
        {
            string title = "长款";
            string strIp = IpAddressHelper.GetHostAddress();
            using (TransactionScope ts = new TransactionScope())
            {
                this.rs = this.bll.InsertATMOperationREC(reqJson, strIp, title);
                ts.Complete();
            }
            return this.rs;
        }



    }
}