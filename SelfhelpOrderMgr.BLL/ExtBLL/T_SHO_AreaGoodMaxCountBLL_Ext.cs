using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_SHO_AreaGoodMaxCountBLL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount GetModel(string fareacode,string fgtxm)
        {

            List<T_SHO_AreaGoodMaxCount> lists= GetModelList("FAreaCode='"+ fareacode +"' and FGtxm='"+ fgtxm +"'");
            if(lists.Count==0)
            {
                return null;
            }
            else
            {
                return lists[0];
            }
        }

        public List<T_SHO_AreaGoodMaxCount> GetGoodsByTypeModel(string goodtype)
        {
            DataSet ds=new T_SHO_AreaGoodMaxCountDAL().GetGoodsByType(goodtype);
            return DataTableToList(ds.Tables[0]);
        }


        public List<T_SHO_AreaGoodMaxCount> GetModelList(int page,int pageSiza,string  strWhere)
        {
            DataSet ds = new T_SHO_AreaGoodMaxCountDAL().GetPageRowList(page,pageSiza,strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        //按队别名称来验证是否超过本月最大的购买数量
        public bool GetAreaBuyCountByGtxm(string fcrimecode, decimal buyCount, string gtxm, string fareaCode)
        {
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
                if (DateTime.Today <= new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime)
                {
                    startDate = new T_SHO_ManagerSetBLL().GetModel("XFDateStart").StartTime.ToString();
                    endDate = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime.ToString();
                    T_SHO_ManagerSet xfBeiShu = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd");
                    beiShu = Convert.ToDecimal(xfBeiShu.MgrValue);
                }
            }
            //获取队别已经购买的数量
            decimal buiedCount = new T_InvoiceBLL().GetAreaBuyGoodCount(fcrimecode,gtxm, fareaCode, startDate, endDate);
            //获取队别最大可购买的数量
            T_SHO_AreaGoodMaxCount areaGoodModel=new T_SHO_AreaGoodMaxCountBLL().GetModel(fareaCode, gtxm);
            if (areaGoodModel == null)
            {
                return true;
            }
            else
            {
                if (Convert.ToDecimal(areaGoodModel.FGoodMaxCount) * beiShu >= buiedCount + buyCount)
                {
                    return true;
                }
            }
            return false;
        }

        //按用户的登录名称来验证是否超过本月最大的购买数量
        public bool GetLoginNameBuyCountByGtxm(string crtby, decimal buyCount, string gtxm, string fareaCode)
        {
            string startDate;
            string endDate;
            decimal beiShu;
            //取得购买日期的倍数
            GetBuyDate_BeiShu(out startDate, out endDate, out beiShu);

            //获取队别已经购买的数量
            decimal buiedCount = new T_InvoiceBLL().GetLoginNameBuyGoodCount(crtby, gtxm,  startDate, endDate);
            //获取队别最大可购买的数量
            T_SHO_AreaGoodMaxCount areaGoodModel = new T_SHO_AreaGoodMaxCountBLL().GetModel(fareaCode, gtxm);
            if (areaGoodModel == null)
            {
                return true;
            }
            else
            {
                //设定为-1就表示为不限购
                if (Convert.ToDecimal(areaGoodModel.FGoodMaxCount) * beiShu >= buiedCount + buyCount)
                {
                    return true;
                }
            }
            return false;
        }


        //系统提交时验证登录名来统计商品是否超过限购买的数量
        public bool SubmitCheckLoginNameBuyGoodCountStatus(string crtby, int orderId)
        {
            string startDate;
            string endDate;
            decimal beiShu;
            //取得购买日期的倍数
            GetBuyDate_BeiShu(out startDate, out endDate, out beiShu);

            //获取队别已经购买的数量
            return new T_InvoiceBLL().GetLoginNameCheckBuyGoodCountStatus( crtby,  startDate,  endDate,  orderId,  beiShu);
            
        }

        private static void GetBuyDate_BeiShu(out string startDate, out string endDate, out decimal beiShu)
        {
            //初始相关参数的值
            startDate = "";
            endDate = "";
            beiShu = 1;//默认消费的倍数为1，如果在特定时段内再根据相关值调整
            startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
            endDate = DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-1";

            //判断是否在消费限定日期之内
            T_SHO_ManagerSet xfDateRect = new T_SHO_ManagerSetBLL().GetModel("XFDateRect");
            if (xfDateRect.MgrValue == "1")
            {
                if (DateTime.Today <= new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime)
                {
                    startDate = new T_SHO_ManagerSetBLL().GetModel("XFDateStart").StartTime.ToString();
                    endDate = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd").StartTime.ToString();
                    T_SHO_ManagerSet xfBeiShu = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd");
                    beiShu = Convert.ToDecimal(xfBeiShu.MgrValue);
                }
            }
        }

        //复制一个监区的设定到其他所有监区去
        public bool CopyInfoToAears(string srcArea)
        {
            return new T_SHO_AreaGoodMaxCountDAL().CopyInfoToAears(srcArea);
        }
    }
}