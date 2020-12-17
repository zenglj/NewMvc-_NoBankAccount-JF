
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.BLL
{
    public class T_Bank_CardPoolBLL:BaseDapperBLL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        /// <summary>
        /// 分配新银行卡号
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <returns></returns>
        public ResultInfo DistributeNewCard(string fcrimecode)
        {
            T_Criminal_cardBLL _cardBll = new T_Criminal_cardBLL();
            var rs = new ResultInfo() { ReMsg = "未处理", DataInfo = null, Flag = false };
            //判断犯人是否已经有分配了银行卡号
            string whereCard=jss.Serialize( new{fcrimecode=fcrimecode});
            var list = _cardBll.GetModelList("fcrimecode='" +fcrimecode+ "'");

            if (list.Count== 0)
            {
                rs.ReMsg =fcrimecode+ "没有IC卡号，不能分配银行结算卡号";
                return rs;
            }

            T_Criminal_card card = list[0];
            if (!string.IsNullOrWhiteSpace(card.SecondaryBankCard))
            {
                rs.ReMsg = fcrimecode + "已经有分配了银行卡号:" + card.SecondaryBankCard + "，不能重新分配";
                return rs;
            }
            else if (card.cardflaga == 4)
            {
                rs.ReMsg = fcrimecode + "已经离监，不能分配银行结算卡号";
                return rs;
            }

            //判断卡池里是否已经有分配了该犯人的银行卡号
            whereCard = jss.Serialize(new { FCrimeCode = fcrimecode });
            T_Bank_CardPool bankCard = GetModelFirst<T_Bank_CardPool, T_Bank_CardPool>(whereCard);
            if (bankCard != null)
            {
                rs.ReMsg = fcrimecode + "卡池已经有分配了银行卡号:" + bankCard.BankCardNoNew + "，不能重新分配";
                return rs;
            }

            //判断卡池里是否还有可用的卡号
            int count = base.GetTableCount<T_Bank_CardPool, T_Bank_CardPool>("{}", " UseFlag=0 ");

            if (count <= 0)
            {
                rs.ReMsg = "卡池里没有可用于分配的银行卡号了";
                return rs;
            }

            Random r1 = new Random();

            if (count > 500)
            {
                count = 500;
            }
            int rowNo = r1.Next(count+1);
            var result=base.GetPageList<T_Bank_CardPool, T_Bank_CardPool>("Id", "{}", rowNo, 1, " UseFlag=0 ");
            
            //开始分配银行结算卡号
            StartDistributionCardNo(rs, list, result);
            
            return rs;

        }

        /// <summary>
        /// 批量分配银行卡号
        /// </summary>
        /// <returns></returns>
        public ResultInfo BatchDistributeNewCard()
        {
            T_Criminal_cardBLL _cardBll = new T_Criminal_cardBLL();
            var rs = new ResultInfo() { ReMsg = "未处理", DataInfo = null, Flag = false };

            var list = base.GetPageList<T_Criminal_card,T_Criminal_card>("seqno","{}",1,500," cardflaga<>4 and SecondaryCardFlag=0 and  isnull(SecondaryBankCard,'')='' ").rows;

            //判断卡池里是否还有可用的卡号
            int count = base.GetTableCount<T_Bank_CardPool, T_Bank_CardPool>("{}", " UseFlag=0 ");

            if (count <= 0)
            {
                rs.ReMsg = "抱歉，卡池里没有可用于分配的银行卡号了";
                return rs;
            }

            if (list.Count > count)
            {
                rs.ReMsg = "抱歉，卡池里只有" + count + " 张,不够" + list.Count + " 人分配。";
                return rs;
            }
            var result = base.GetPageList<T_Bank_CardPool, T_Bank_CardPool>("Id", "{}", 1, list.Count, " UseFlag=0 ");
            
            //开始分配银行结算卡号
            StartDistributionCardNo(rs, list, result);
            
            return rs;
        }

        /// <summary>
        /// 开始分配银行结算卡号
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="list"></param>
        /// <param name="result"></param>
        private void StartDistributionCardNo(ResultInfo rs, List<T_Criminal_card> list, PageResult<T_Bank_CardPool> result)
        {
            if (result.rows.Count == list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //设置IC的信息
                    list[i].SecondaryBankCard = result.rows[i].BankCardNoNew;
                    list[i].SecondaryCardFlag = 1;
                    list[i].RegFlag = 1;
                    //设置卡号池的信息
                    result.rows[i].FCrimeCode = list[i].fcrimecode;
                    result.rows[i].UseDate = DateTime.Now;
                    result.rows[i].UseFlag = 1;
                }
                var _json = new { Id = 0, FCrimeCode = "ww", UseFlag = 1, UseDate = DateTime.Now };

                //调用部分更新的方法
                if (base.Update<T_Bank_CardPool>(result.rows, jss.Serialize(_json), " UseFlag=0 "))
                {
                    if (base.Update<T_Criminal_card>(list, jss.Serialize(new { SecondaryBankCard = "11", SecondaryCardFlag = 1 }), " fcrimecode=@fcrimecode ", false))
                    {
                        rs.ReMsg = "分配成功，人数" + list.Count;
                        rs.Flag = true;
                        if (list.Count == 1)
                        {
                            rs.DataInfo = result.rows[0].BankCardNoNew;
                        }
                        else
                        {
                            rs.DataInfo = "成功分配人数" + list.Count;
                        }
                    }
                }
            }
            else
            {
                rs.ReMsg = "分配银行卡号失败";
            }
        }


    }
}