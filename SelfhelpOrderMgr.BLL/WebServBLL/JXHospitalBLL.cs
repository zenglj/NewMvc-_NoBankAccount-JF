using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class JXHospitalBLL
    {
        /// <summary>
        /// 获取犯人基本信息
        /// </summary>
        /// <param name="FCode">犯人编号</param>
        /// <returns></returns>
        public static CriminalEntity getCriminal(string FCode)
        {
            CriminalEntity reModel = new CriminalEntity();
            T_Criminal re = new T_CriminalDAL().GetModel(FCode);
                           
            reModel = new CriminalEntity
            {
                Amount = re.amount == null ? 0 : (int)re.amount,
                FAddr = re.FAddr,
                FAddr_tmp = re.FAddr_tmp,
                FAge = re.FAge == null ? 0 : (int)re.FAge,
                FAreaCode = re.FAreaCode,
                FCode = re.FCode,
                FCrimeCode = re.FCrimeCode,
                FCYCode = re.FCYCode,
                FCZY = re.FCZY,
                FDesc = re.FDesc,
                Fflag = re.fflag ==null?0:(int)re.fflag,
                FIdenNo = re.FIdenNo,
                FInDate = re.FInDate == null ? Convert.ToDateTime("1900-01-01") : (DateTime)re.FInDate,
                Flimitamt = re.flimitamt == null ? 0 : (int)re.flimitamt,
                Flimitflag = re.flimitflag == null ? 0 : (int)re.flimitflag,
                FName = re.FName,
                FOuDate = re.FOuDate == null ? Convert.ToDateTime("1900-01-01") : (DateTime)re.FOuDate,
                Frealareacode = re.Frealareacode,
                FSex = re.FSex,
                FStatus = re.FStatus == null ? 0 : (int)re.FStatus,
                FSubArea = re.FSubArea,
                FTerm = re.FTerm
            };
            return reModel;
        }

        /// <summary>
        /// 获取犯人卡信息
        /// </summary>
        /// <param name="FCode"></param>
        /// <returns></returns>
        public static CriminalCardEntity getCriminalCard(string FCode)
        {
            CriminalCardEntity reModel = new CriminalCardEntity();
            T_Criminal_card a = new T_Criminal_cardDAL().GetModel(FCode);

            reModel = new CriminalCardEntity
            {
                AmountA = a.AmountA,
                AmountB = a.AmountB,
                AmountC = a.AmountC,
                BankAccNo = a.BankAccNo,
                BankAmount = a.BankAmount,
                Bankdate = a.bankdate == null ? Convert.ToDateTime("1900-01-01") : a.bankdate,
                BankRegFlag = a.BankRegFlag==null?0:a.BankRegFlag,
                Cardcodea = a.cardcodea,
                Cardcodeb = a.cardcodeb,
                Cardflaga = a.cardflaga == null ? 0 : a.cardflaga,
                Cardflagb = a.cardflagb==null?0:a.cardflagb,
                Curbankamount = a.curbankamount==null?0:a.curbankamount,
                Fcrimecode = a.fcrimecode,
                Flimitamt = a.flimitamt==null?0:a.flimitamt,
                Flimitflag = a.flimitflag==null?0:a.flimitflag,
                RegFlag = a.RegFlag==null?0:a.RegFlag,
                Seqno = a.seqno,
                TmpbankAmount = a.tmpbankAmount==null?0:a.tmpbankAmount,
                UnPaidAmtA = a.UnPaidAmtA==null?0:a.UnPaidAmtA,
                UnPaidAmtB = a.UnPaidAmtB==null?0:a.UnPaidAmtB,
                Unregflag = a.unregflag==null?0:a.unregflag,
                UseFlag = a.UseFlag==null?0:a.UseFlag
            };
            
            return reModel;
        }

        /// <summary>
        /// 犯人入院
        /// </summary>
        /// <param name="FCode">犯人编号</param>
        /// <returns></returns>
        public static CriminalIn CriminalIn(string FCode)
        {
            CriminalIn re = new CriminalIn();
            CriminalEntity crimiEnt =getCriminal(FCode);
            T_Criminal_card crimiCardEnt= new T_Criminal_cardDAL().GetModel( FCode);
            string errorMsg = "";
            try
            {
                if (crimiEnt.FCode == null)
                    {
                        re.Result = false;
                        re.ResultMsg = "查无此人，请检查编号是否正确！";
                        re.CriminalModel = new CriminalEntity();
                    }
                    else
                    {
                        if (crimiEnt.Fflag == 1)
                        {
                            re.Result = false;
                            re.ResultMsg = "该犯人已离监！";
                            re.CriminalModel = new CriminalEntity();
                        }
                    }

                    //获取犯人在监信息
                    re.CriminalModel = new CriminalEntity
                    {
                        AmountB = crimiCardEnt.AmountB,
                        AmountC = crimiCardEnt.AmountC,
                        AmountA = crimiCardEnt.AmountA,
                        BankAccNo = crimiCardEnt.BankAccNo,
                        BankAmount = crimiCardEnt.BankAmount,
                        Cardflaga = crimiCardEnt.cardflaga,
                        Cardflagb = crimiCardEnt.cardflagb,
                        FAddr = crimiEnt.FAddr,
                        FAreaCode = crimiEnt.FAreaCode,
                        FCode = crimiEnt.FCode,
                        FCrimeCode = crimiEnt.Fcrimecode,
                        FCYCode = crimiEnt.FCYCode,
                        FCZY = crimiEnt.FCZY,
                        FDesc = crimiEnt.FDesc,
                        FIdenNo = crimiEnt.FIdenNo,
                        FName = crimiEnt.FName,
                        FSex = crimiEnt.FSex,
                        BankRegFlag = crimiCardEnt.BankRegFlag,
                        RegFlag = crimiCardEnt.RegFlag,
                        UseFlag = crimiCardEnt.UseFlag,
                        Fflag = crimiEnt.Fflag
                    };

                    //初始化返回信息的值
                   
                    
                    //挂失监狱端的犯人消费卡
                    //db.proc_CriminalInHospital(FCode, ref errorMsg, ref errorCode);

                    int cardFlag = 2;
                    bool Result = true;
                    SetUserICCardStatus(ref errorMsg, cardFlag, ref Result, crimiCardEnt);

                    re.Result = Result;
                    re.ResultMsg = errorMsg;                    
                    
            }
            catch (Exception ex)
            {
                re.Result = false;
                re.ResultMsg = ex.Message;
                re.CriminalModel = new CriminalEntity();
            }

            return re;
        }

        /// <summary>
        /// 获取犯人住院前当月已在监狱消费情况
        /// </summary>
        /// <param name="FCode"></param>
        /// <returns></returns>
        public static InvoiceEntity CriminalConsumedQuota(string FCode)
        {
            InvoiceEntity reModel = new InvoiceEntity();
            //DateTime FirstDate= Convert.ToDateTime("2016/12/1 0:00:00");

            DateTime FirstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //DateTime EndDate = Convert.ToDateTime("2016/12/31 11:59:59");
            DateTime EndDate = DateTime.Parse(FirstDate.AddMonths(1).ToShortDateString()).AddSeconds(-1);

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(FCode, 7);

            if (criminal != null)
            {            
                reModel = new InvoiceEntity
                {
                    Amount = criminal.Xiaofeimoney,
                    AmountA = criminal.Xiaofeimoney,
                    AmountB = 0,
                    FreeAmountA = 0,
                    FreeAmountB =0,
                    CardCode = "",
                    CardType = 0,
                    Checkflag = 1,
                    Crtby = "",
                    Crtdate = Convert.ToDateTime(DateTime.Now.ToLongDateString()),
                    FAreaCode = "",
                    FAreaName = "",
                    FCrimeCode = criminal.FCode,
                    FCriminal =criminal.FName,
                    Fifoflag = -1,
                    Flag = 1,
                    Frealareacode = "",
                    FrealAreaName = "",
                    Fsn = "",
                    InvoiceNo = "",
                    OrderDate = DateTime.Now,
                    PayDate = DateTime.Now,
                    PType = "监狱消费",
                    Remark = "入院前所在监狱当月已消费",
                    TypeFlag = 7
                };
            }
            return reModel;
        }

        /// <summary>
        /// 犯人出院
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public static ResultEntity CriminalOut(string FCode)
        {
            ResultEntity reModel = new ResultEntity();
            string errorMsg = "";
            //string errorCode = "";
            int cardFlag = 1;
            bool Result = true;
            T_Criminal_card crimiCardEnt = new T_Criminal_cardDAL().GetModel(FCode);

            SetUserICCardStatus(ref errorMsg, cardFlag, ref Result, crimiCardEnt);

            reModel.Result = Result;
            reModel.ReMsg = errorMsg;
            return reModel;
        }

        //设定的用户的IC状态
        private static void SetUserICCardStatus(ref string errorMsg, int cardFlag, ref bool Result, T_Criminal_card crimiCardEnt)
        {
            crimiCardEnt.cardflaga = cardFlag;
            if (!new T_Criminal_cardDAL().Update(crimiCardEnt))
            {
                Result = false;
                errorMsg = "更新余额表IC卡状态失败";
            }

            List<T_ICCARD_LIST> cards = new T_ICCARD_LISTBLL().GetModelList("CardCode='" + crimiCardEnt.cardcodea + "'");
            if (cards.Count <= 0)
            {
                Result = false;
                errorMsg = "该犯没有相应的IC卡号:" + crimiCardEnt.cardcodea;
            }
            cards[0].FFlag = cardFlag;

            if (!new T_ICCARD_LISTDAL().Update(cards[0]))
            {
                Result = false;
                errorMsg = "更新ICCard表IC卡状态失败";
            }
        }

        /// <summary>
        /// 犯人消费
        /// </summary>
        /// <returns></returns>
        public static ResultEntity CriminalConsume(InvoiceEntity reqModel)
        {
            ResultEntity reModel = new ResultEntity();

            reModel.Result = true;
            reModel.ReMsg = "";
            string msg = new JXHospitalDAL().WriteJXInvoice(reqModel);
            if (msg != "OK|结算成功。")
            {
                reModel.Result =false;
                reModel.ReMsg = msg;
            }
            

            return reModel;
        }

        /// <summary>
        /// 获取犯人实时余额信息
        /// </summary>
        /// <param name="FCode">犯人编号</param>
        /// <returns></returns>
        public static CriminalIn RealTimeBalance(string FCode)
        {
            CriminalIn rspModel = new CriminalIn();
            try
            {
                T_Criminal_card a = new T_Criminal_cardBLL().GetModel(FCode);

                if (a != null)
                {
                    rspModel.CriminalCardModel = new CriminalCardEntity
                    {
                        AmountA = a.AmountA==null ? 0:a.AmountA ,
                        AmountB = a.AmountB == null ?0: a.AmountB ,
                        AmountC = a.AmountC == null ? 0:a.AmountC ,
                        BankAccNo = a.BankAccNo,
                        BankAmount = a.BankAmount == null ? 0:a.BankAmount,
                        Bankdate = a.bankdate == null ? new DateTime():Convert.ToDateTime(a.bankdate)  ,
                        BankRegFlag = a.BankRegFlag == null ?0: a.BankRegFlag ,
                        Cardcodea = a.cardcodea,
                        Cardcodeb = a.cardcodeb,
                        Cardflaga = a.cardflaga == null ?0: a.cardflaga,
                        Cardflagb = a.cardflagb == null ?0: a.cardflagb ,
                        Curbankamount = a.curbankamount == null ?0: a.curbankamount,
                        Fcrimecode = a.fcrimecode,
                        Flimitamt = a.flimitamt == null ?0: a.flimitamt,
                        Flimitflag = a.flimitflag == null ?0: a.flimitflag ,
                        RegFlag = a.RegFlag == null?0: a.RegFlag,
                        Seqno = a.seqno,
                        TmpbankAmount = a.tmpbankAmount == null?0: a.tmpbankAmount ,
                        UnPaidAmtA = a.UnPaidAmtA == null?0: a.UnPaidAmtA,
                        UnPaidAmtB = a.UnPaidAmtB == null?0: a.UnPaidAmtB ,
                        Unregflag = a.unregflag == null?0: a.unregflag 
                    };
                }
                rspModel.Result = true;
                rspModel.ResultMsg = "成功";

                
            }
            catch (Exception ex)
            {
                rspModel.Result = false;
                rspModel.ResultMsg = ex.Message.ToString();
            }
            return rspModel;
        }

        /// <summary>
        /// 获取犯人在医院消费订单信息
        /// </summary>
        /// <param name="CriminalCodeList"></param>
        /// <returns></returns>
        public static CriminalIn SynInvoiceResult(List<string> InvoiceNoList)
        {
            CriminalIn rspModel = new CriminalIn();
            rspModel.VcrdListModel = new List<VcrdEntity>();
            try
            {
                string invs="";
                foreach( string inv in InvoiceNoList)
                {
                    if(inv=="")
                    {
                        invs="'"+ inv +"'";
                    }
                    else
                    {
                        invs=invs+",'"+ inv +"'";
                    }
                    
                }
                List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("Origid in (invs)");
                var reList = (from a in vcrds
                                group a by new { a.CardCode, a.FCrimeCode, a.BankFlag, a.OrigId } into b
                                select new
                                {
                                    cardcode = b.Key.CardCode,
                                    fcrimecode = b.Key.FCrimeCode,
                                    CAMOUNT = b.Sum(c => c.CAmount),
                                    Bankflag = b.Key.BankFlag,
                                    origid = b.Key.OrigId
                                }).ToList();

                if (reList.Count > 0)
                {
                    foreach (var a in reList)
                    {
                        rspModel.VcrdListModel.Add(new VcrdEntity
                        {
                            Bankflag = a.Bankflag==null ? a.Bankflag : 0,
                            CAMOUNT = a.CAMOUNT==null ? a.CAMOUNT : 0,
                            Cardcode = a.cardcode,
                            Fcrimecode = a.fcrimecode,
                            Origid = a.origid

                        });
                    }
                }
                rspModel.Result = true;
                rspModel.ResultMsg = "";
                
            }
            catch (Exception ex)
            {
                rspModel.Result = false;
                rspModel.ResultMsg = ex.Message.ToString();
            }

            return rspModel;
        }
    }
}