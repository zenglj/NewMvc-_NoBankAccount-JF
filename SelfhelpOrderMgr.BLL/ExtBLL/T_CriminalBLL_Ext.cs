using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_CriminalBLL
    {
        public void Add(SelfhelpOrderMgr.Model.T_Criminal model, string param)
        {
            new T_CriminalDAL().Add(model,param);
        }
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal model, string param)
        {
            return new T_CriminalDAL().Update(model, param);
        }
        public T_Criminal GetCriminalXE_info(string fcode, int saleTypeId)
        {
            /*T_Criminal扩展属性说明
                AmountAmoney //存款账户上金额
                AmountBmoney //劳酬账户上金额
                Xiaofeimoney //本月已经消费金额
                NoXiaofeimoney //当前可消费金额
             *  CanUseMoneyA   //A账户能用金额
             *  CanUseMoneyB   //B账户能用金额
                MonthStandard //月消费金额标准
                XianEMethod//限额方法：
                        //0是总额限额，同时存款账户也限额;
                        //1是存款账户限额;
                        //2是报酬账户限额;
                        //3是总额限额;
                        //4是存款和报酬两个分别限;
             * 
             * TP_YingYangCan_Money   //领导特批营养餐金额
             * ChinaFestival_Money    //中国传统节日月金额
                  */

            

            #region 查找不同类型的消费限额参数
            //查找不同类型的消费限额参数
            T_SHO_SaleType saleType = new T_SHO_SaleTypeBLL().GetModel(saleTypeId);
            //int saleTypeId = 7;
            int firstPaymentAccount = 0;
            int canconsumeAccount = 0;
            if (saleType != null)
            {
                firstPaymentAccount = saleType.FirstPaymentAccount;
                canconsumeAccount = saleType.CanconsumeAccount;
            }
            #endregion

            T_Criminal model = GetModel(fcode);
            if (model == null)
            {
                return model;
            }
            model.ErrInfo = "";
            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(fcode);
            if (card == null)
            {
                model.ErrInfo = "用户IC卡不存在";
                return model;
            }
            T_CY_TYPE cy = new T_CY_TYPEDAL().GetModel(model.FCYCode);//处遇信息
            if (cy == null)
            {
                model.ErrInfo = "用户所处处遇等级代码不正确";
                return model;
            }

            //营养餐和其他消费分开单独限额标志
            T_SHO_ManagerSet yyMset = new T_SHO_ManagerSetBLL().GetModel("YingyangcanXianE_StartFlag");
            //if (yyMset != null)
            //{
            //    if (yyMset.MgrValue == "1")
            //    {
            //        if (yyMset.KeyMode == saleTypeId)
            //        {
            //            cy.ftotamtmonth = cy.ftotamtmonth - 0 > 0 ? cy.totpct : 0;
            //            cy.famtmonth = cy.famtmonth - 0 > 0 ? cy.totpct : 0;
            //            cy.FBamtMonth = cy.FBamtMonth - 0 > 0 ? cy.totpct : 0;
            //            if (cy.ftotamtmonth > 0 && cy.famtmonth > 0 && cy.FBamtMonth > 0)
            //            {
            //                model.ErrInfo = "用户所处处遇等级限额不正确";
            //            }
            //        }
            //    }
            //}

            //List<T_AREA> areas = new T_AREABLL().GetModelList( "fcode='"+model.FAreaCode+"'").Single();//队别编号
            T_AREA area = new T_AREABLL().GetModel(model.FAreaCode);//队别编号
            if(area==null)
            {
                model.ErrInfo = "用户所处队别代码错误";
                return model;
            }

            

            model.CyName = cy.FName;
            decimal dongjieMoney = 0;//被冻结金额

            #region 获得犯人被冻结金额
            if (model.flimitamt == null)
            {
                model.flimitamt = 0;
            }
            if (model.flimitflag == null)
            {
                model.flimitflag = 0;
            }
            dongjieMoney = (decimal)model.flimitflag * (decimal)model.flimitamt; //被冻结金额
            #endregion

            //账户当前可用总金额
            //model.OkUseAllMoney = card.AmountA + card.AmountB - dongjieMoney;

            model.AmountA = card.AmountA;
            model.AmountB = card.AmountB;
            model.AmountC = card.AmountC;
            model.dongjieMoney = dongjieMoney;//斌值冻结金额
            model.OkUseAllMoney = 0;
            if (card.AmountA >= dongjieMoney)
            {
                model.AmountAmoney = card.AmountA - dongjieMoney;
                model.AmountBmoney = card.AmountB;
            }
            else
            {
                model.AmountAmoney = 0;
                model.AmountBmoney = card.AmountB - (dongjieMoney - card.AmountA);
            }

            //if(card.AmountA - dongjieMoney<=0)
            //{
            //    model.AmountAmoney = 0;
            //}
            //else
            //{
            //    model.AmountAmoney = card.AmountA - dongjieMoney;
            //}


            model.AmountCmoney = card.AmountC;
            model.BankCardNo = card.BankAccNo;
            model.CardCode = card.cardcodea;//设定卡号
            model.FAreaName = area.FName;//队别名称

            #region 判断是否是在特殊的消费时段里

            //初始相关参数的值
            string startDate = "";
            string endDate = "";
            decimal beiShu = 1;//默认消费的倍数为1，如果在特定时段内再根据相关值调整
            startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
            endDate = DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-1";

            //判断是否在消费限额之内
            T_SHO_ManagerSet xfDateRect = new T_SHO_ManagerSetBLL().GetModel("XFDateRect");
            if (xfDateRect.MgrValue == "1")
            {

                //if (DateTime.Today <= new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime)
                //{
                //    startDate = new T_SHO_ManagerSetBLL().GetModel("XFDateStart").StartTime.ToString();
                //    endDate = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime.ToString();
                //    T_SHO_ManagerSet xfBeiShu = new T_SHO_ManagerSetBLL().GetModel("XFBeiShu");
                //    beiShu = Convert.ToDecimal(xfBeiShu.MgrValue);
                //}
                /*
                 由于出现闽西提前设定消费时段，并且消费起始日期大于当天，倒致小于起始日期的消费不受系统限额控制。
                 * 现改为：凡是大于或小于的都进入标准处遇控制
                 */
                if (DateTime.Today >= new T_SHO_ManagerSetBLL().GetModel("XFDateStart").StartTime && DateTime.Today < new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime.AddDays(1))
                {
                    startDate = new T_SHO_ManagerSetBLL().GetModel("XFDateStart").StartTime.ToString();
                    endDate = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime.ToString();
                    T_SHO_ManagerSet xfBeiShu = new T_SHO_ManagerSetBLL().GetModel("XFBeiShu");
                    beiShu = Convert.ToDecimal(xfBeiShu.MgrValue);
                }
            }

            #endregion
            CrimeXFinfo xfmoney = new CrimeXFinfo();//限额相关信息

            #region 获得消费相相情况


            string strWhere = " FCrimeCode='" + fcode + "' and OrderDate>='" + startDate + "' and OrderDate<'" + endDate + "' and flag=1";

            //if (yyMset != null)
            //{
            //    if (yyMset.MgrValue == "1")
            //    {
            //        if (yyMset.KeyMode == saleTypeId)
            //        {
            //            strWhere = strWhere + "and typeflag in(select TypeFlagId from T_SHO_SaleType where ID=" + yyMset.KeyMode.ToString() + ")";
            //        }
            //        //else
            //        //{
            //        //    strWhere = strWhere + "and typeflag not in(select TypeFlagId from T_SHO_SaleType where ID=" + yyMset.KeyMode.ToString() + ")";
            //        //}
            //    }
            //}
            
            List<T_Invoice> invLists = new T_InvoiceBLL().GetModelList(strWhere);


            decimal xfYYCmoneyAmoney=0, xfYYCmoneyBmoney=0, xfYYCmoneyFreeAmoney=0, xfYYCmoneyFreeBmoney=0;
            if (invLists != null)
            {
                foreach (T_Invoice vs in invLists)
                {
                    xfmoney.Amoney = xfmoney.Amoney + (vs.AmountA * vs.Fifoflag * -1);
                    xfmoney.Bmoney = xfmoney.Bmoney + (vs.AmountB * vs.Fifoflag * -1);
                    xfmoney.FreeAmoney = xfmoney.FreeAmoney + (vs.FreeAmountA * vs.Fifoflag * -1);
                    xfmoney.FreeBmoney = xfmoney.FreeBmoney + (vs.FreeAmountB * vs.Fifoflag * -1);
                    if (yyMset != null)
                    {
                        if (yyMset.MgrValue == "1")
                        {
                            if (yyMset.KeyMode == saleTypeId)
                            {
                                if (saleType.TypeFlagId == vs.TypeFlag)
                                {
                                    xfYYCmoneyAmoney = xfYYCmoneyAmoney + (vs.AmountA * vs.Fifoflag * -1);
                                    xfYYCmoneyBmoney = xfYYCmoneyBmoney + (vs.AmountB * vs.Fifoflag * -1);
                                    xfYYCmoneyFreeAmoney = xfYYCmoneyFreeAmoney + (vs.FreeAmountA * vs.Fifoflag * -1);
                                    xfYYCmoneyFreeBmoney = xfYYCmoneyFreeBmoney + (vs.FreeAmountB * vs.Fifoflag * -1);
                                }                                
                            }
                        }
                    }

                    if (vs.FTZSP_Money != null)//特种商品消费金额
                    {
                        xfmoney.FTZSP_Money = xfmoney.FTZSP_Money + +(vs.FTZSP_Money * vs.Fifoflag * -1);
                        decimal areaXfMoney = ((vs.FTZSP_Money - vs.AmountB) * vs.Fifoflag * -1) > 0 ? ((vs.FTZSP_Money - vs.AmountB) * vs.Fifoflag * -1) : 0;
                        xfmoney.FTZSP_AreaXFMoney = xfmoney.FTZSP_AreaXFMoney + areaXfMoney;
                    }
                }
            }

            

            //if (yyMset != null)
            //{
            //    if (yyMset.MgrValue == "1")
            //    {
            //        if (yyMset.KeyMode == saleTypeId)
            //        {
            //            xfmoney.Amoney = xfYYCmoneyAmoney;
            //            xfmoney.Bmoney = xfYYCmoneyBmoney;
            //            xfmoney.FreeAmoney = xfYYCmoneyFreeAmoney;
            //            xfmoney.FreeBmoney = xfYYCmoneyFreeBmoney;
            //        }
            //    }
            //}
            #endregion

            #region 判断账户的可消费情况
            //判断账户的可消费情况
            //首先判断账是哪个账户可以消费
            switch (canconsumeAccount)
            {
                case 0://A和B都可以消费
                    {
                        model.CanUseMoneyA = model.AmountAmoney;
                        model.CanUseMoneyB = model.AmountBmoney;
                    } break;
                case 1:
                    {
                        model.CanUseMoneyA = model.AmountAmoney;
                        model.CanUseMoneyB = 0;
                        model.AmountBmoney = 0;
                    } break;
                case 2:
                    {
                        model.CanUseMoneyA = 0;
                        model.AmountAmoney = 0;
                        if (model.AmountBmoney - dongjieMoney <= 0)
                        {
                            model.CanUseMoneyB = 0;
                        }
                        else
                        {
                            model.CanUseMoneyB = model.AmountBmoney - dongjieMoney;
                        }
                    } break;
                default:
                    break;
            }

            model.OkUseAllMoney = model.CanUseMoneyA + model.CanUseMoneyB;//根据可消费账户判断结果再相加

            #endregion

            //==========================================================================
            //2018-11-02  zenglj
            //增加节假日判断，如是传统节日增加相应的金额            
            List<T_CY_ChinaFestival> festivalDates =new T_CY_ChinaFestivalBLL().GetModelList("FDate>='" + startDate + "' and FDate<'" + endDate + "'");
            if (festivalDates.Count <= 0)
            {
                cy.JaRi_Cy_Money=0;
            }
            //if (yyMset != null)
            //{
            //    if (yyMset.MgrValue == "1")
            //    {
            //        if (yyMset.KeyMode == saleTypeId)
            //        {
            //            cy.JaRi_Cy_Money = 0;
            //        }                    
            //    }
            //}
            model.JaRi_Cy_Money = cy.JaRi_Cy_Money;


            //增加个人营养餐特批金额
            //只要领导有审批则个人增加相应的金额，可购买食品和日用品
            decimal TP_YingYangCan_Money = 0;
            decimal TP_CY_YingYangCan_Money = 0;

            List<T_Criminal_TPList> yingyangcaMoneys = new T_Criminal_TPListBLL().GetModelList("EffectiveDate>=getdate() and isnull(FifoFlag,0)=0 and FCode='"+ model.FCode +"'");
            if (yingyangcaMoneys.Count > 0)
            {
                TP_YingYangCan_Money = model.TP_YingYangCan_Money;
                TP_CY_YingYangCan_Money = model.TP_YingYangCan_Money;
                if (yingyangcaMoneys[0].MoneyUseFlag == 1)
                {
                    TP_CY_YingYangCan_Money = 0;
                }
            }

            //if (yyMset != null)
            //{
            //    if (yyMset.MgrValue == "1")
            //    {
            //        if (yyMset.KeyMode == saleTypeId)
            //        {
            //            TP_CY_YingYangCan_Money = 0;
            //        }
            //    }
            //}
            //===========================================================================

            #region 获取消费限额数据及可消费金额
            //启用特殊时段的倍数参数 * beiShu
            //根据处遇级别 乘以 倍数beiShu
            if (cy.ftotamtmonth != 0 && cy.famtmonth != 0)//如果启用总额限制,同时启动存款账户限额
            {
                //判断是否是传统节日月和领导特批营养餐金额
                model.UserCyDesc = "总额限制处遇" + cy.ftotamtmonth.ToString() + ",领导特批" + TP_CY_YingYangCan_Money.ToString() + ",节日" + cy.JaRi_Cy_Money;
                cy.ftotamtmonth = cy.ftotamtmonth + TP_CY_YingYangCan_Money + cy.JaRi_Cy_Money;
                
                //判断限额验证，如总额度，小于A账户额度，则是错的，那么直将AB都设为0
                if (cy.ftotamtmonth < cy.famtmonth)
                {
                    model.CanUseMoneyA = 0;
                    model.CanUseMoneyB = 0;
                }
                else
                {
                    #region 启用总额限制计算，同时启动存款账户限额相关设定
                    model.MonthStandard = cy.ftotamtmonth * beiShu;//启用特殊时段的倍数参数
                    //增加传统节日+领导特批
                    cy.famtmonth = cy.famtmonth + TP_CY_YingYangCan_Money + cy.JaRi_Cy_Money;

                    //判断A账户是否大于“处遇管理”设定的最大金额，如果大于，则只能是用CY的最大金额
                    if (model.CanUseMoneyA > cy.famtmonth - xfmoney.Amoney)//需要减去本月已经消费的金额
                    {
                        model.CanUseMoneyA = cy.famtmonth - xfmoney.Amoney;
                    }
                    if (model.CanUseMoneyA < 0)
                    {
                        model.CanUseMoneyA = 0;
                    }

                    model.Xiaofeimoney = xfmoney.Amoney + xfmoney.Bmoney - xfmoney.FreeAmoney - xfmoney.FreeBmoney;
                    model.XianEMethod = 0;//是总额限额同时存款账户也限额;
                    if ((model.MonthStandard - model.Xiaofeimoney) > (model.CanUseMoneyA + model.CanUseMoneyB))
                    {//如是消费限额大于当前账户余额
                        model.NoXiaofeimoney = model.CanUseMoneyA + model.CanUseMoneyB;
                    }
                    else
                    {//如是消费限额小于当前账户余额
                        model.NoXiaofeimoney = model.MonthStandard - model.Xiaofeimoney;
                        if (firstPaymentAccount == 0)//如果A优先，否则A全扣，余下B扣
                        {
                            if (model.CanUseMoneyA > model.NoXiaofeimoney)
                            {
                                model.CanUseMoneyA = model.NoXiaofeimoney;
                                model.CanUseMoneyB = 0;
                            }
                            else
                            {
                                model.CanUseMoneyB = model.NoXiaofeimoney - model.CanUseMoneyA;
                            }
                        }
                        else//否则B全扣，余下A扣
                        {
                            if (model.CanUseMoneyB > model.NoXiaofeimoney)
                            {
                                model.CanUseMoneyB = model.NoXiaofeimoney;
                                model.CanUseMoneyA = 0;
                            }
                            else
                            {
                                model.CanUseMoneyA = model.NoXiaofeimoney - model.CanUseMoneyB;
                            }
                        }
                    }
                    #endregion
                }

            }
            else if (cy.ftotamtmonth != 0)//如果启用总额限制
            {
                //判断是否是传统节日月和领导特批营养餐金额
                model.UserCyDesc = "总额限制处遇" + cy.ftotamtmonth.ToString() + ",领导特批" + TP_CY_YingYangCan_Money.ToString() + ",节日" + cy.JaRi_Cy_Money;
                cy.ftotamtmonth = cy.ftotamtmonth + TP_CY_YingYangCan_Money + cy.JaRi_Cy_Money;

                #region 启用总额限制计算相关设定
                model.MonthStandard = cy.ftotamtmonth * beiShu;//启用特殊时段的倍数参数
                model.Xiaofeimoney = xfmoney.Amoney + xfmoney.Bmoney - xfmoney.FreeAmoney - xfmoney.FreeBmoney;
                model.XianEMethod = 3;//是总额限额;
                if ((model.MonthStandard - model.Xiaofeimoney) > (model.CanUseMoneyA + model.CanUseMoneyB))
                {//如是消费限额大于当前账户余额
                    model.NoXiaofeimoney = model.CanUseMoneyA + model.CanUseMoneyB;
                }
                else
                {//如是消费限额小于当前账户余额
                    model.NoXiaofeimoney = model.MonthStandard - model.Xiaofeimoney;
                    if (firstPaymentAccount == 0)//如果A优先，否则A全扣，余下B扣
                    {
                        if (model.CanUseMoneyA > model.NoXiaofeimoney)
                        {
                            model.CanUseMoneyA = model.NoXiaofeimoney;
                            model.CanUseMoneyB = 0;
                        }
                        else
                        {
                            model.CanUseMoneyB = model.NoXiaofeimoney - model.CanUseMoneyA;
                        }
                    }
                    else//否则B全扣，余下A扣
                    {
                        if (model.CanUseMoneyB > model.NoXiaofeimoney)
                        {
                            model.CanUseMoneyB = model.NoXiaofeimoney;
                            model.CanUseMoneyA = 0;
                        }
                        else
                        {
                            model.CanUseMoneyA = model.NoXiaofeimoney - model.CanUseMoneyB;
                        }
                    }
                }
                ////--可设定劳酬最大金额
                ////2018-10-15在莆田把修改
                ///*
                // 在总额限制下，可以设定处遇的劳动报酬最大可用金额（用于购买食品）
                // */
                //if(model.CanUseMoneyB>cy.FBamtMonth && cy.FBamtMonth>0)
                //{
                //    model.CanUseMoneyB = cy.FBamtMonth;
                //}
                #endregion
            }
            else if (cy.famtmonth != 0 && cy.FBamtMonth != 0)//A账户限额，B账户也限额
            {
                //判断是否是传统节日月和领导特批营养餐金额
                model.UserCyDesc = "A限额" + cy.famtmonth.ToString() + ",B限额" + cy.FBamtMonth.ToString() + ",领导特批" + TP_CY_YingYangCan_Money.ToString() + ",节日" + cy.JaRi_Cy_Money;
                cy.famtmonth = cy.famtmonth + TP_CY_YingYangCan_Money + cy.JaRi_Cy_Money;

                #region A账户限额，B账户也限额
                model.MonthStandard = (cy.FBamtMonth + cy.famtmonth) * beiShu;//启用特殊时段的倍数参数
                model.Xiaofeimoney = xfmoney.Amoney + xfmoney.Bmoney - xfmoney.FreeAmoney - xfmoney.FreeBmoney;
                model.XianEMethod = 4;//是存款和报酬两个分别限;
                if (cy.famtmonth - (xfmoney.Amoney - xfmoney.FreeAmoney) < model.CanUseMoneyA)
                {
                    if (xfmoney.Amoney - xfmoney.FreeAmoney > 0)
                    {
                        model.CanUseMoneyA = cy.famtmonth - (xfmoney.Amoney - xfmoney.FreeAmoney);
                    }
                    else
                    {
                        model.CanUseMoneyA = cy.famtmonth;
                    }
                }
                if (cy.FBamtMonth - (xfmoney.Bmoney - xfmoney.FreeBmoney) < model.CanUseMoneyB)
                {
                    if (xfmoney.Bmoney - xfmoney.FreeBmoney > 0)
                    {
                        model.CanUseMoneyB = cy.FBamtMonth - (xfmoney.Bmoney - xfmoney.FreeBmoney);
                    }
                    else
                    {
                        model.CanUseMoneyB = cy.FBamtMonth;
                    }
                }
                model.NoXiaofeimoney = model.CanUseMoneyA + model.CanUseMoneyB;

                //if (model.MonthStandard - model.Xiaofeimoney > model.CanUseMoneyA + model.CanUseMoneyB)
                //{//如是消费限额大于当前账户余额
                //    model.NoXiaofeimoney = model.CanUseMoneyA + model.CanUseMoneyB;
                //}
                //else
                //{//如是消费限额小于当前账户余额
                //    model.NoXiaofeimoney = model.MonthStandard - model.Xiaofeimoney;
                //    if (firstPaymentAccount == 0)//如果A优先，否则A全扣，余下B扣
                //    {
                //        if (model.CanUseMoneyA > model.NoXiaofeimoney)
                //        {
                //            model.CanUseMoneyA = model.NoXiaofeimoney;
                //            model.CanUseMoneyB = 0;
                //        }
                //        else
                //        {
                //            model.CanUseMoneyB = model.NoXiaofeimoney - model.CanUseMoneyA;
                //        }
                //    }
                //    else//否则B全扣，余下A扣
                //    {
                //        if (model.CanUseMoneyB > model.NoXiaofeimoney)
                //        {
                //            model.CanUseMoneyB = model.NoXiaofeimoney;
                //            model.CanUseMoneyA = 0;
                //        }
                //        else
                //        {
                //            model.CanUseMoneyA = model.NoXiaofeimoney - model.CanUseMoneyB;
                //        }
                //    }
                //} 
                #endregion

            }
            else if (cy.famtmonth != 0 && cy.FBamtMonth == 0)//A账户限额，B账户不限额
            {
                //判断是否是传统节日月和领导特批营养餐金额
                model.UserCyDesc = "A限额" + cy.famtmonth.ToString() + ",B不限额,领导特批" + TP_CY_YingYangCan_Money.ToString() + ",节日" + cy.JaRi_Cy_Money;
                cy.famtmonth = cy.famtmonth + TP_CY_YingYangCan_Money + cy.JaRi_Cy_Money;

                #region A账户限额，B账户不限额
                model.MonthStandard = cy.famtmonth * beiShu;//启用特殊时段的倍数参数
                model.Xiaofeimoney = xfmoney.Amoney - xfmoney.FreeAmoney;
                model.XianEMethod = 1;//是存款账户限额;
                if (model.MonthStandard - model.Xiaofeimoney > model.CanUseMoneyA)
                {//如是消费限额大于当前账户余额
                    model.NoXiaofeimoney = model.CanUseMoneyA + model.CanUseMoneyB;
                }
                else
                {//如是消费限额小于当前账户余额
                    model.NoXiaofeimoney = model.MonthStandard + model.CanUseMoneyB - model.Xiaofeimoney;
                    model.CanUseMoneyA = model.MonthStandard - model.Xiaofeimoney;
                }
                #endregion
            }
            else if (cy.famtmonth == 0 && cy.FBamtMonth != 0)//B账户限额，A账户不限额
            {
                //判断是否是传统节日月和领导特批营养餐金额
                model.UserCyDesc = "B限额" + cy.FBamtMonth.ToString() + ",A不限额,领导特批" + TP_CY_YingYangCan_Money.ToString() + ",节日" + cy.JaRi_Cy_Money;
                cy.FBamtMonth = cy.FBamtMonth + TP_CY_YingYangCan_Money + cy.JaRi_Cy_Money;

                #region B账户限额，A账户不限额
                model.MonthStandard = cy.FBamtMonth * beiShu;//启用特殊时段的倍数参数
                model.Xiaofeimoney = xfmoney.Bmoney - xfmoney.FreeBmoney;
                model.XianEMethod = 2;//是报酬账户限额;
                if (model.MonthStandard - model.Xiaofeimoney > model.CanUseMoneyB)
                {//如是消费限额大于当前账户余额
                    model.NoXiaofeimoney = model.CanUseMoneyB - model.Xiaofeimoney + model.CanUseMoneyA;
                }
                else
                {//如是消费限额小于当前账户余额
                    model.NoXiaofeimoney = model.MonthStandard - model.Xiaofeimoney + model.CanUseMoneyA;
                    model.CanUseMoneyB = model.MonthStandard - model.Xiaofeimoney;
                }
                #endregion
            }

            #endregion


            #region 计算特种商品的购买最大金额
            /*
             * 计算处遇可用的特种物品购买金额
             * 如果有开启劳酬消费标志则：
             *          特种商品的最大购买金额=所在队别每个可用金额+处遇可用特种金额+劳报报酬的金额
             * 如果没有开启劳酬消费标志则：
             *          特种商品的最大购买金额=所在队别每个可用金额+处遇可用特种金额
             *          
             */

            if (yyMset != null)
            {
                if (yyMset.MgrValue == "1")
                {
                    if (yyMset.KeyMode == saleTypeId)
                    {
                        //TP_CY_YingYangCan_Money = 0;
                        model.MonthStandard =cy.totpct;
                        model.Xiaofeimoney = xfYYCmoneyAmoney + xfYYCmoneyBmoney - xfYYCmoneyFreeAmoney - xfYYCmoneyFreeBmoney;
                        model.NoXiaofeimoney = model.NoXiaofeimoney > (cy.totpct - model.Xiaofeimoney) ? (cy.totpct - model.Xiaofeimoney) : model.NoXiaofeimoney;
                        //cy.JaRi_Cy_Money=0;
                        //model.JaRi_Cy_Money = 0;

                    }
                }
            }

            decimal lcFTZSP_Money = 0;//劳酬可用特种商品可用金额
            decimal areaFTZSP_Money = 0;//队别可用特种商品可用金额
            decimal cyFTZSP_Money = 0;//处遇可用特种商品可用金额
            if (area.FTZSP_Money != null)
            {
                areaFTZSP_Money = area.FTZSP_Money;
            }
            if (cy.FTZSP_Money != null)
            {
                cyFTZSP_Money = cy.FTZSP_Money;
            }
            //计算特种商品的购买最大金额
            /*
             * 如果有开启劳酬消费标志则：
             *          特种商品的最大购买金额=所在队别每个可用金额+处遇可用特种金额+劳报报酬的金额
             * 如果没有开启劳酬消费标志则：
             *          特种商品的最大购买金额=所在队别每个可用金额+处遇可用特种金额
             */
            decimal ftzspMoney = 0;
            T_SHO_ManagerSet tzspMst = new T_SHO_ManagerSetBLL().GetModel("TZSP_LaochouXF");

            //2018-11-02漳州监狱增加领导特批营养金额功能
            //只要领导有审批则个人增加相应的金额，可购买食品和日用品
            //传统节日加的金额不能算是劳酬特购的产品
            //应扣特种商品金额
            decimal yingkouTzsp_Money = xfmoney.FTZSP_Money - xfmoney.Bmoney > 0 ? xfmoney.FTZSP_Money - xfmoney.Bmoney : 0;
            //ftzspMoney = areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money - xfmoney.FTZSP_Money > 0 ? areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money - xfmoney.FTZSP_Money : 0;
            ftzspMoney = areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money - yingkouTzsp_Money > 0 ? areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money - yingkouTzsp_Money : 0;

            //如果节假日金额设定为可以购买特种商品，则金额加上节日金额
            T_SHO_ManagerSet jjrMSet = new T_SHO_ManagerSetBLL().GetModel("JieJiaRi_Money_TZSPFlag");
            if(jjrMSet!=null)
            {
                if (jjrMSet.MgrValue == "1")
                {
                    //ftzspMoney = areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money + cy.JaRi_Cy_Money - xfmoney.FTZSP_Money > 0 ? areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money + cy.JaRi_Cy_Money - xfmoney.FTZSP_Money : 0;
                    ftzspMoney = areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money + cy.JaRi_Cy_Money - yingkouTzsp_Money > 0 ? areaFTZSP_Money + cyFTZSP_Money + TP_YingYangCan_Money + cy.JaRi_Cy_Money - yingkouTzsp_Money : 0;
                }
            }


            if (tzspMst == null)
            {
                //ftzspMoney = areaFTZSP_Money + cyFTZSP_Money  -xfmoney.FTZSP_Money>0? areaFTZSP_Money + cyFTZSP_Money  -xfmoney.FTZSP_Money :0;
            }
            else if (tzspMst.MgrValue == "1")
            {
                //ftzspMoney = areaFTZSP_Money + cyFTZSP_Money + model.AmountBmoney;
                ftzspMoney = ftzspMoney + model.AmountBmoney;
                ////2018-10-15在莆田把修改
                ///*
                // 在总额限制下，可以设定处遇的劳动报酬最大可用金额（用于购买食品）
                // */
                //ftzspMoney = ftzspMoney + model.CanUseMoneyB;
                lcFTZSP_Money = model.AmountBmoney;
            }
            else
            {
                //ftzspMoney = areaFTZSP_Money + cyFTZSP_Money;
                lcFTZSP_Money = 0;
            }


            //如果ftzspMoney大于最大可用金额，则以最大可用金额为准
            if (ftzspMoney > (model.CanUseMoneyA + model.CanUseMoneyB))
            {
                ftzspMoney = (model.CanUseMoneyA + model.CanUseMoneyB);
            }

            if (model.CanUseMoneyA >= areaFTZSP_Money + cy.FTZSP_Money)
            {
                model.TZSP_cyMoney = cy.FTZSP_Money;//处遇可用特种商品的金额
                model.TZSP_AreaMoney = areaFTZSP_Money;//监区可用特种商品的金额
            }
            else
            {
                if (model.CanUseMoneyA >= areaFTZSP_Money)
                {
                    model.TZSP_AreaMoney = areaFTZSP_Money;//监区可用特种商品的金额
                    model.TZSP_cyMoney = model.CanUseMoneyA - areaFTZSP_Money;//处遇可用特种商品的金额
                }
                else
                {
                    model.TZSP_AreaMoney = model.CanUseMoneyA;//监区可用特种商品的金额
                    model.TZSP_cyMoney = 0;//处遇可用特种商品的金额
                }

            }
            try
            {
                if (area.SaleCloseFlag != null)
                {
                    model.SaleCloseFlag = area.SaleCloseFlag;
                }
            }
            catch
            {

            }
            model.FTZSP_CanUseMoney = ftzspMoney; //ftzspMoney大于最大可用金额
            model.FTZSP_AreaXFMoney = xfmoney.FTZSP_AreaXFMoney;//ftzspMoney监区部份使用的金额

            #endregion

            //2018-10-15在莆田把修改
            //当特种商品为零标志FTZSP_Zero_Flag=1时，不让购买特种商品（食品额度变为0）
            if (cy.FTZSP_Zero_Flag == 1)
            {
                model.FTZSP_CanUseMoney = 0;
            }

            //if (yyMset != null)
            //{
            //    if (yyMset.MgrValue == "1")
            //    {
            //        if (yyMset.KeyMode == saleTypeId)
            //        {
            //            decimal canAllUseMoney1 = 0;
            //            if (saleType.FirstPaymentAccount == 0)
            //            {
            //                canAllUseMoney1 = model.CanUseMoneyA + model.CanUseMoneyB;
            //                model.CanUseMoneyA = model.CanUseMoneyA - cy.totpct > 0 ? cy.totpct : model.CanUseMoneyA;
            //                model.CanUseMoneyB = canAllUseMoney1 - cy.totpct > 0 ? cy.totpct - model.CanUseMoneyA : canAllUseMoney1 - model.CanUseMoneyA;
            //            }
            //            else
            //            {
            //                canAllUseMoney1 = model.CanUseMoneyA + model.CanUseMoneyB;
            //                model.CanUseMoneyB = model.CanUseMoneyB - cy.totpct > 0 ? cy.totpct : model.CanUseMoneyB;
            //                model.CanUseMoneyA = canAllUseMoney1 - cy.totpct > 0 ? cy.totpct - model.CanUseMoneyB : canAllUseMoney1 - model.CanUseMoneyB;

            //            }
                        
            //        }
            //    }
            //}

            //2018-11-02漳州监狱增加领导特批营养金额功能
            //只要领导有审批则个人增加相应的金额，可购买食品和日用品
            //model.FTZSP_CanUseMoney = model.FTZSP_CanUseMoney + TP_YingYangCan_Money;
            //model.TZSP_cyMoney = model.TZSP_cyMoney + TP_YingYangCan_Money;
            
            return model;
        }

        public IEnumerable<T_Criminal> GetPageListOfIEnumerable(int page, int pageRow, string strWhere)
        {
            return new T_CriminalDAL().GetPageListOfIEnumerable(page, pageRow, strWhere);
        }

        public List<T_Criminal> GetCrimeBySearch( string fcode, string fname, string areaname, string usercode)
        {

            DataTable dt= new T_CriminalDAL().GetCrimeBySearch( fcode, fname, areaname, usercode);
            return DataTableToList(dt);
        }

        /// <summary>
        /// 统计获取消费后的余额
        /// </summary>
        /// <param name="fcode"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public T_Criminal_card GetInvoiceBalance(string fcode, string invoiceNo)
        {
            return new T_CriminalDAL().GetInvoiceBalance(fcode, invoiceNo);
        }

        /// <summary>
        /// 扩展的犯人信息，包括队别名称，处遇名称，罪名
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRow"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderByField"></param>
        /// <returns></returns>
        public List<T_Criminal> GetPageCriminalExtInfo(int page, int pageRow, string strWhere, string orderByField)
        {
            return new T_CriminalDAL().GetPageCriminalExtInfo(page,pageRow,strWhere,orderByField);
        }

        public int GetCriminalsCount(string strWhere)
        {
            return new T_CriminalDAL().GetCriminalsCount(strWhere);

        }
        public List<T_UserInfoExt> GetUserInfo(string strWhere)
        {
            return new T_CriminalDAL().GetUserInfo(strWhere); 
        }
        public List<T_UserInfoExt> GetUserInfo(int page, int pageRow, string strWhere)
        {
            return new T_CriminalDAL().GetUserInfo(page, pageRow, strWhere);
        }

        public int GetUserInfoCount(string strWhere)
        {
            return new T_CriminalDAL().GetUserInfoCount(strWhere);
        }

        //批量Excel导入
        public string PLExcelImport(string crtby,int modeId) 
        {
            return new T_CriminalDAL().PLExcelImport(crtby, modeId);
        }

        public string PLExcelChangeEduImport(string crtby)
        {
            return new T_CriminalDAL().PLExcelChangeEduImport(crtby);
        }

        public string Rsb_KouKuan(string fcrimecode, string crtby)
        {
            decimal rsb_money=0;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("RSB_KouKuan_Money");

            if (mset == null)
            {
                return ("Err|对不起,系统未设定入监/所包扣款的功能");
            }
            if (mset.KeyMode == 0)
            {
                return ("Err|对不起,系统未开启入监/所包扣款的功能");
            }

            try
            {
                rsb_money = Convert.ToDecimal(mset.MgrValue);
            }
            catch
            {
                return ("Err|管理表的入所包设定值不是有效的金额，请与管理员联系");
            }


            T_Criminal criminal = new T_CriminalBLL().GetModel(fcrimecode);
            if (criminal == null)
            {
                return ("Err|该编号人员信息不存在");
            }
            if (criminal.fflag == 1)
            {
                return ("Err|该编号人员已经办理解除（离监）结算，不能重复扣");
            }
            if (criminal.RSB_Flag == 1)
            {
                return ("Err|该编号人员已经扣过入监/所包了，不能重复扣");
            }

            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(fcrimecode);
            if (card == null)
            {
                return ("Err|该编号人员IC卡不存在");
            }
            if (card.cardflaga == 4)
            {
                return ("Err|该编号人员已经办理解除（离监）结算，不能重复扣");
            }

            return new T_CriminalDAL().Rsb_KouKuan(fcrimecode,crtby, rsb_money);
        }

    }

    public class CrimeXFinfo
    {
        public decimal Amoney { get; set; }
        public decimal Bmoney { get; set; }
        public decimal FreeAmoney { get; set; }
        public decimal FreeBmoney { get; set; }
        public decimal FTZSP_Money { get; set; }//特种商品消费额度
        public decimal FTZSP_AreaXFMoney { get; set; }//监区特种商品额度已经使用消费
    }


    
}