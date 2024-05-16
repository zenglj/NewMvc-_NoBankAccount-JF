using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.BLL
{
    public class BankAtmService : BaseDapperBLL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ResultInfo rs = new ResultInfo()
        {
            Flag = false,
            ReMsg = "未处理",
            DataInfo = null
        };


        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="reqJson"></param>
        /// <param name="strIp"></param>
        /// <returns></returns>
        public ResultInfo AtmReconciliation(reqAtmReconciliation reqJson, string strIp)
        {
            string jsonWhere = this.jss.Serialize(new
            {
                IPAddr = strIp
            });
            int iStatusFlag = 1;
            string strRemark = "对账相符";
            decimal _balance = Convert.ToDecimal(reqJson.F_AMOUNT);
            T_Bank_AtmMachine i = base.GetModelFirst<T_Bank_AtmMachine, T_Bank_AtmMachine>(jsonWhere);
            if (i.MachineBalance != _balance)
            {
                iStatusFlag = -1;
                strRemark = string.Format("系统的余额是{0},对账单的余额是{1},两者金额不符", i.MachineBalance, _balance);
            }
            i.MachineBalance = _balance;
            i.ReconciliationDate = DateTime.Now;
            i.StatusFlag = iStatusFlag;
            i.Remark = strRemark;
            base.Update<T_Bank_AtmMachine>(i);
            T_Bank_AtmDealList deal = new T_Bank_AtmDealList
            {
                Id = 0,
                MacId = i.Id,
                AtmSerialNo = reqJson.AtmSerialNo,
                ActionType = "对账",
                MachineBalance = _balance,
                ChangeAmount = decimal.Zero,
                CrtDate = DateTime.Now,
                StatusFlag = iStatusFlag,
                Remark = strRemark
            };
            base.Insert<T_Bank_AtmDealList>(deal);
            this.rs.Flag = (iStatusFlag == 1);
            this.rs.ReMsg = strRemark;
            return this.rs;
        }


        /// <summary>
        /// 获取结算付款记录
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="tranStatus"></param>
        /// <param name="rec"></param>
        /// <param name="model"></param>
        public void GetPaymentRecordInfo(string fcrimecode, int tranStatus, out PageResult<ViewPaymentRecordExtend> rec, out ViewBankUserInfo model)
        {
            //var _s = new { FCrimeCode = fcrimecode, tranStatus = tranStatus };
            //rec = this.GetPageList<ViewPaymentRecordExtend, ViewPaymentRecordExtend>("Id",jss.Serialize(_s));
            //ViewPaymentRecordExtend row = rec.rows[0];
            //model = new ViewBankUserInfo { 
            //     Id=row.Id,
            //     FCrimeCode=row.FCrimeCode,
            //     FCrimeName=row.FCrimeName,
            //     TranMoney=row.TranMoney,
            //     TranStatus=row.TranStatus
            //};

            var where = new
            {
                FCrimeCode = fcrimecode,
                TranType = 0,
                PayMode = 1,
                TranStatus = tranStatus
            };
            rec = base.GetPageList<ViewPaymentRecordExtend, ViewPaymentRecordExtend>("Id", this.jss.Serialize(where), 1, 1, "");
            if (rec.rows.Count >= 1)
            {
                ViewPaymentRecordExtend row = rec.rows[0];
                //增加传输IC卡号
                T_Criminal_card _card = base.QueryModel<T_Criminal_card>("fcrimecode", row.FCrimeCode);
                model = new ViewBankUserInfo
                {
                    Id = row.Id,
                    FCrimeCode = row.FCrimeCode,
                    FCrimeName = row.FCrimeName,
                    CardCode = _card.cardcodea,
                    TranMoney = row.TranMoney,
                    TranStatus = row.TranStatus,
                    PurposeInfo = row.PurposeInfo
                };
            }
            else
            {

                model = new ViewBankUserInfo();
            }
            

        }

        

        public ResultInfo InsertATMOperationREC(reqAtmActionInfo reqJson, string strIp, string title)
        {
            string jsonWhere = this.jss.Serialize(new
            {
                IPAddr = strIp
            });
            T_Bank_AtmMachine atm = base.GetModelFirst<T_Bank_AtmMachine, T_Bank_AtmMachine>(jsonWhere);
            if (atm == null)
            {
                this.rs.ReMsg = "ATM的IP地址授权";
                return this.rs;
            }
            decimal changeMoney = 0m;
            string remark = "";
            int istatusFlag = 0;
            if (title == "取款" || title == "冲正" || title == "长款" || title == "加钞")
            {
                changeMoney = Convert.ToDecimal(reqJson.F_AMOUNT);
            }
            else
            {
                //if (title == "清机" || title == "对账")
                if ( title == "对账")
                    {
                    changeMoney = 0m;
                    if (atm.MachineBalance == Convert.ToDecimal(reqJson.F_AMOUNT))
                    {
                        istatusFlag = 1;
                        remark = title + "-成功|机器与记账都是:" + reqJson.F_AMOUNT+"。钞箱:"+reqJson.CashBoxInfo;
                    }
                    else
                    {
                        istatusFlag = -1;
                        remark = string.Format("{0}-失败|机器金额为{1},记账为:{2},差额为{3}" + "。钞箱:" + reqJson.CashBoxInfo, new object[]
                        {
                    title,
                    reqJson.F_AMOUNT,
                    atm.MachineBalance,
                    atm.MachineBalance - Convert.ToDecimal(reqJson.F_AMOUNT)
                        });
                    }

                }
                else if (title == "清机")
                    {
                        changeMoney = 0m;
                        if (atm.MachineBalance == Convert.ToDecimal(reqJson.F_AMOUNT))
                        {
                            istatusFlag = 1;
                            remark = title + "-成功|机器与记账都是:" + reqJson.F_AMOUNT;
                        }
                        else
                        {
                            istatusFlag = -1;
                            remark = string.Format("{0}-失败|机器金额为{1},记账为:{2},差额为{3}", new object[]
                            {
                    title,
                    reqJson.F_AMOUNT,
                    atm.MachineBalance,
                    atm.MachineBalance - Convert.ToDecimal(reqJson.F_AMOUNT)
                            });
                        }

                    }
                else
                {
                    changeMoney = 0m;
                    remark = title + ":" + reqJson.F_AMOUNT;
                }
            }
            T_Bank_AtmDealList i = new T_Bank_AtmDealList
            {
                Id = 0,
                MacId = atm.Id,
                AtmSerialNo = reqJson.AtmSerialNo,
                ActionType = title,
                ChangeAmount = changeMoney,
                MachineBalance = atm.MachineBalance + changeMoney,
                TranDate = reqJson.TranDate,
                TranTime = reqJson.TranTime,
                CrtDate = DateTime.Now,
                Remark = remark,
                StatusFlag = istatusFlag
            };
            T_Bank_AtmDealList model = base.Insert<T_Bank_AtmDealList>(i);
            if (model != null)
            {
                if ( title == "对账")
                {
                    if (!string.IsNullOrWhiteSpace(reqJson.F_AMOUNT) && reqJson.F_AMOUNT != "0")
                    {
                        atm.MachineBalance = Convert.ToDecimal(reqJson.F_AMOUNT);
                        base.Update<T_Bank_AtmMachine>(atm);
                    }
                }
                else if (title == "清机")
                {
                    if (!string.IsNullOrWhiteSpace(reqJson.F_AMOUNT) && reqJson.F_AMOUNT != "0")
                    {
                        atm.MachineBalance =0;
                        base.Update<T_Bank_AtmMachine>(atm);
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(reqJson.F_AMOUNT) && reqJson.F_AMOUNT != "0")
                    {
                        atm.MachineBalance += changeMoney;
                        base.Update<T_Bank_AtmMachine>(atm);
                    }
                }
                this.rs.Flag = true;
                this.rs.ReMsg = title + "成功";
                this.rs.DataInfo = model;
            }
            else
            {
                this.rs.Flag = false;
                this.rs.ReMsg = title + "失败";
            }
            return this.rs;
        }


        /// <summary>
        /// 支取操作(取款、更新出钞、冲正出钞)
        /// </summary>
        /// <param name="money"></param>
        /// <param name="withdrawalPassword"></param>
        /// <param name="fcrimecode"></param>
        /// <param name="tranStatus"></param>
        /// <param name="updateStatus"></param>
        /// <param name="bankResultInfo"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public ResultInfo WithdrawalOperation(decimal money, string withdrawalPassword, string fcrimecode, int tranStatus, int updateStatus, string bankResultInfo, string actionName)
        {
            PageResult<ViewPaymentRecordExtend> rec;
            ViewBankUserInfo model;
            this.GetPaymentRecordInfo(fcrimecode, tranStatus, out rec, out model);
            if (rec.rows.Count > 0)
            {
                if (rec.rows[0].PwdErrCount > 6)
                {
                    this.rs.ReMsg = "密码输入错误超过限制次数，请与管理员联系";
                    return rs;
                }
                if (rec.rows[0].AuditFlag == 0)
                {
                    this.rs.ReMsg = "结算余额未审核，请与监狱管理民警联系";
                }
                else
                {
                    if (withdrawalPassword != rec.rows[0].WithdrawalPassword)
                    {
                        this.rs.ReMsg = "取款密码不对";
                        T_Bank_PaymentRecord tempRec = this.GetModel<T_Bank_PaymentRecord>(rec.rows[0].Id);
                        tempRec.PwdErrCount = tempRec.PwdErrCount + 1;
                        this.Update<T_Bank_PaymentRecord>(tempRec);
                    }
                    else
                    {
                        if (money != model.TranMoney)
                        {
                            this.rs.Flag = false;
                            this.rs.ReMsg = "支取的金额与系统不一致";
                        }
                        else
                        {
                            if (tranStatus != model.TranStatus)
                            {
                                this.rs.Flag = false;
                                this.rs.ReMsg = "支付状态不对，不能" + actionName;
                            }
                            else
                            {
                                T_Bank_PaymentRecord _record = base.GetModel<T_Bank_PaymentRecord>(model.Id);
                                _record.TranStatus = updateStatus;
                                _record.BankResultInfo = bankResultInfo;
                                _record.TranDate = new DateTime?(DateTime.Now);
                                if (base.Update<T_Bank_PaymentRecord>(_record))
                                {
                                    this.rs.Flag = true;
                                    this.rs.ReMsg = actionName + "成功";
                                    model.TranStatus = updateStatus;
                                    model.TranMoney *= 100m;
                                    this.rs.DataInfo = model;
                                }
                                else
                                {
                                    this.rs.Flag = false;
                                    this.rs.ReMsg = actionName + "失败";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.rs.Flag = false;
                this.rs.ReMsg = "没有找到相应的记录";
            }
            return this.rs;
        }

    }

    /// <summary>
    /// 取款
    /// </summary>
    public class ReqWithDrawal: reqAtmActionInfo
    {
        //public string AtmSerialNo { get; set; }
        //public string F_AMOUNT { get; set; }
        //public string MacCode { get; set; }
        //public string TranDate { get; set; }
        //public string TranTime { get; set; }
        public string CardCode { get; set; }
        public string WithdrawalPassword { get; set; }
    }

    /// <summary>
    /// 出钞
    /// </summary>
    public class ReqCashOut: reqAtmActionInfo
    {
        //public string AtmSerialNo { get; set; }
        //public string F_AMOUNT { get; set; }
        //public string MacCode { get; set; }
        //public string TranDate { get; set; }
        //public string TranTime { get; set; }
        public string CardCode { get; set; }
        public string WithdrawalPassword { get; set; }
        public int PayId { get; set; }

    }

    public class reqAtmActionInfo
    {
        public string AtmSerialNo { get; set; }
        public string F_AMOUNT { get; set; }
        public string MacCode { get; set; }
        public string TranDate { get; set; }
        public string TranTime { get; set; }
        public string CashBoxInfo { get; set; }//钞箱信息

    }

    public class reqAtmFaceInfo: reqAtmActionInfo
    {
        public string FaceData { get; set; }

    }

    public class reqAtmCashBoxInfo : reqAtmActionInfo
    {
        public string CashBoxMoney { get; set; }
        public string CashBoxMoneyNotReturn { get; set; }
        public string CashBoxReturnMoney { get; set; }
        public List<T_Bank_ATMCashBoxDetail> BoxDetial { get; set; } 
    }



    /// <summary>
    /// 对账信息
    /// </summary>
    public class reqAtmReconciliation : reqAtmActionInfo
    {

    }

    /// <summary>
    /// 请求卡号
    /// </summary>
    public class ReqCardCode: reqAtmActionInfo
    {
        public string CardCode { get; set; }

    }

    /// <summary>
    /// ATM请求数据
    /// </summary>
    public class ReqDataAtm
    {
        public string token { get; set; }
        public string Action { get; set; }
        public Dictionary<string,string> Data { get; set; }

    }


}