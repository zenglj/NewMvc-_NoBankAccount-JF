using SelfhelpOrderMgr.Common.CustomExtend;
using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace SelfhelpOrderMgr.BLL
{
    public class T_Bank_PaymentRecordBLL:BaseDapperBLL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        private T_Bank_PaymentRecordDAL _dal = new T_Bank_PaymentRecordDAL();

        public ResultInfo VcrdPayListCreate(string crtby, T_Bank_PayRecBase tranParam, string strJsonWhere, string selectMulIds)
        {
            string otherStrWhere = " CAmount>0 and Flag=0 and isnull(PayAuditFlag,0)=0 and isnull(BankFlag,0)<1 ";
            var rs = new ResultInfo() { 
                ReMsg="未创建",
                Flag=false,
                DataInfo=null
            };
            otherStrWhere += "  and seqno in(" + selectMulIds + ")";

            var list = this.GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", strJsonWhere, 1, 999999, otherStrWhere);
            if(list.rows.Count<=0)
            {
                rs.ReMsg = "查询到Vcrd的行数未0，不能创建";
                return rs;
            }
            var accCode = this.GetModelFirst<T_Trans_FeeList, T_Trans_FeeList>(jss.Serialize(new { TypeName=list.rows[0].DType})).AccCode;
            if(string.IsNullOrWhiteSpace(accCode)){
                rs.ReMsg = "银行传输配置表里没有【" + list.rows[0].DType + "】,请与系统管理员联系";
                return rs;
            }
            var accId = this.GetModelFirst<T_Trans_BankAccount, T_Trans_BankAccount>(jss.Serialize(new { AccCode = accCode })).Id;
            decimal sumMoney = list.rows.Sum(p => p.CAmount);

            T_Bank_PaymentRecord model = new T_Bank_PaymentRecord() { 
                Id=0,
                FCrimeCode = InterFaceTranType.BtoB.GetRemark(),
                TranType = tranParam.TranType,
                PayMode=tranParam.PayMode,
                Amount = sumMoney,
                ToBankId = accId,
                AuditFlag=0,
                AuditBy="",
                AuditDate=null,
                TranMoney = sumMoney,
                PurposeInfo=tranParam.PurposeInfo,
                TranDate=null,
                TranStatus=0,
                Crtdate=DateTime.Now,
                ReturnTime=null,
                BankObssid=null,
                BankResultInfo=""            
            };


            var t=base.Insert<T_Bank_PaymentRecord>(model);

            //更新Vcrd记录的传输状态
            var dtls = new List<T_Bank_PaymentDetail>();
            foreach (var item in list.rows)
            {
                //更新Vcrd记录的传输状态
                item.BankFlag = 1;
                item.BankInterfaceFlag = 1;
                item.PayAuditFlag = 1;
                item.SendDate = DateTime.Now;
                item.CheckFlag = 0;
                item.CheckBy = crtby;
                item.CheckDate=DateTime.Now;
                //增加PaymentDetail记录的传输记录
                var dtl = new T_Bank_PaymentDetail() {
                    MainId=t.Id,
                    Vcrdseqno=item.seqno,
                    SuccFlag=0
                };
                dtls.Add(dtl);
            }
            
            var ss=new{BankFlag=1,BankInterfaceFlag=1,PayAuditFlag=1,SendDate=DateTime.Now,CheckFlag=1,CheckBy=crtby,CheckDate=DateTime.Now};
            //执行Vcrd记录的更新
            this.Update<T_Vcrd>(list.rows, jss.Serialize(ss), otherStrWhere,  false);
            //增加传输记录到PaymentDetail表中
            this.Insert<T_Bank_PaymentDetail>(dtls);

            rs.ReMsg = "提交完成";
            rs.Flag = true;
            rs.DataInfo = t;
            return rs;

        }

        /// <summary>
        /// 直接失败的记录复位重发
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public string ResetCheckSend(int id,string remark)
        {
            return _dal.ResetCheckSend(id,remark);
        }

        /// <summary>
        /// 转账成功后退款的复位重发
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public string ResetRefund(int id, string remark)
        {
            
            return _dal.ResetRefund(id, remark);
        }

    }
}