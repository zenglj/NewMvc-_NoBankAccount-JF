using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class T_Criminal
    {
        public decimal AmountAmoney { get; set; }//存款账户上金额
        public decimal AmountBmoney { get; set; }//劳酬账户上金额
        public decimal AmountCmoney { get; set; }//留存账户上金额
        public decimal Xiaofeimoney { get; set; }//本月已经消费金额
        public decimal NoXiaofeimoney { get; set; }//当前可消费金额
        public decimal CanUseMoneyA { get; set; }//A账户能用金额
        public decimal CanUseMoneyB { get; set; }//B账户能用金额            
        public decimal MonthStandard { get; set; }//月消费金额标准
        public decimal dongjieMoney { get; set; }//账户冻结金额

        public decimal AmountA { get; set; }//A账户原金额
        public decimal AmountB { get; set; }//B账户原金额
        public decimal AmountC { get; set; }//C账户原金额


        //限额方法：
        //1是存款账户限额;
        //2是报酬账户限额;
        //3是总额限额;
        //4是存款和报酬两个分别限;
        public int XianEMethod { get; set; }
        public string CyName { get; set; }//犯人的处遇中方名称

        public string CardCode { get; set; }//IC卡号
        public decimal OkUseAllMoney { get; set; }//账户可用总金额
        public string FAreaName { get; set; }
        public string BankCardNo { get; set; }//银行卡号
        public int SaleCloseFlag { get; set; }//所在队别的消费开关状态，0是开，1是关

        public string FCardStatus { get; set; }//IC卡状态

        public string CrimeName { get; set; }//犯罪名称（罪名）

        public decimal FTZSP_CanUseMoney { get; set; }//特种商品可用金额
        public decimal FTZSP_AreaXFMoney { get; set; }//监区特种商品已经使用的金额
        public decimal TZSP_AreaMoney { get; set; }//特种商品犯人所在监区对应的可用金额
        public decimal TZSP_cyMoney { get; set; }//特种商品犯人对应处遇的可用金额

        public string UnitID { get; set; }//送犯单时ID
        public string OldFCode { get; set; }//老编号
        public string IdentityCard { get; set; }//关键卡号
        public int CriminalType { get; set; }//犯人的类型，联网，本地，非联网，公安等

        public string UserCyDesc { get; set; }//用户消费时的处遇描述（处遇+节日+领导特批）
        public string ErrInfo { get; set; }//错误信息
        public decimal JaRi_Cy_Money { get; set; }//节假日补助金额
    }
}