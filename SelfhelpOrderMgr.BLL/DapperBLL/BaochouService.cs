using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfhelpOrderMgr.DAL.DapperDAL;
using SelfhelpOrderMgr.Model;

namespace SelfhelpOrderMgr.BLL
{
    public class BaochouMgrService : BaseDapperBLL
    {
        public readonly decimal chuqinRation = 2;//出勤积分系数
        decimal xingshiWanchengBili = (decimal)0.7;//刑释完成比例值
        public decimal shuilv = (decimal)0.7;//增值税率
        decimal houqinMarkToMoney = 1;//后勤积分兑换金额比例
        BaochouMgrDapperDAL _dal = new BaochouMgrDapperDAL();
        public List<T_BC_ManagerParam> workPost;//工作岗位
        public List<T_BC_ManagerParam> workComments;//岗位评议
        public List<T_BC_ManagerParam> dutyComments;//岗位评议
        List<T_BC_ManagerParam> workLevel;//工作等级
        List<T_BC_ManagerParam> completeRatio;//完成率

        public Dictionary<int, decimal> dictRanking = new Dictionary<int, decimal>();//排名参数
        public BaochouMgrService()
        {
            //获取控制参数
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "2A" });
            var _controlPam = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(strJson, "Id asc", 100);

            //获取出工积分系数  G
            if (_controlPam != null)
            {
                this.chuqinRation = Convert.ToDecimal(_controlPam.Where(o => o.FCode == "G").FirstOrDefault().MgrValue);
            }

            //刑释完成比例值  H
            if (_controlPam != null)
            {
                this.xingshiWanchengBili = Convert.ToDecimal(_controlPam.Where(o => o.FCode == "H").FirstOrDefault().MgrValue);
            }

            //增值税率  C
            if (_controlPam != null)
            {
                this.shuilv = Convert.ToDecimal(_controlPam.Where(o => o.FCode == "C").FirstOrDefault().MgrValue);
            }

            //工作岗位
            this.workPost = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "1A" }), "Id asc", 500);


            
            

            //工作等级
            this.workLevel = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "1B" }), "Id asc", 500);

            //完成率
            this.completeRatio = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "3A" }), "Id asc", 500);
            //夜值评议
            this.dutyComments = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "3B" }), "Id asc", 500);

            //岗位评议
            this.workComments = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "3C" }), "Id asc", 500);


            //排名奖励提取参数
            dictRanking.Add(1, 19);
            dictRanking.Add(2, 17);
            dictRanking.Add(3, 15);
            dictRanking.Add(4, 13);
            dictRanking.Add(5, 11);
            dictRanking.Add(6, 9);
            dictRanking.Add(7, 7);
            dictRanking.Add(8, 5);
            dictRanking.Add(9, 3);
            dictRanking.Add(10, 1);

        }


        #region 监狱到分监区分配相关计算
        /// <summary>
        /// 根据日均产值计算提成比例
        /// </summary>
        /// <param name="chanzhi"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public decimal GetRijunChanzhi(decimal chanzhi, decimal days)
        {
            decimal tiquzhi = 0;
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { TypeCode = "4A" });
            var list = _dal.GetModelList<T_BC_ManagerParam, T_BC_ManagerParam>(strJson, "FCode desc", 100);

            var ss = list.OrderByDescending(o => o.FCode);
            decimal rijunCz = chanzhi / days;
            foreach (var item in ss)
            {
                if (rijunCz >= Convert.ToDecimal(item.AreaStart))
                {
                    tiquzhi = Convert.ToDecimal(item.MgrValue);
                    break;
                }
            }
            return tiquzhi;
        }

        #endregion



        #region 技术岗位相关计算

        /// <summary>
        /// 计算技术岗位个人绩效积分
        /// </summary>
        /// <param name="wanchengbi"></param>
        /// <returns></returns>
        public decimal GetJishuGangWeiJixiaoGongfen(decimal wanchengbi)
        {
            //完成率系数  
            var ss = completeRatio.OrderByDescending(o => o.FCode);
            decimal fenshu = 0;
            decimal yulv = wanchengbi;
            foreach (var item in ss)
            {
                if (yulv >= Convert.ToDecimal(item.AreaStart))
                {
                    fenshu += Convert.ToDecimal(item.MgrValue) + (yulv - Convert.ToDecimal(item.AreaStart)) * Convert.ToDecimal(item.RatioValue);
                    yulv = Convert.ToDecimal(item.AreaStart);
                }
            }

            return fenshu;

        }



        /// <summary>
        /// 根据OrderId获取该单技术岗位的平均绩效积分
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="jishuPostCode"></param>
        /// <returns></returns>
        public decimal GetJishuGangweiAvgByOrderId(string orderId, string jishuPostCode = "A")
        {
            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkPostCode = jishuPostCode })
                , "Id asc", 500);

            //如果记录为0条则直接返回0
            if (list.Count <= 0)
                return 0;

            //技术岗位总积分
            decimal sumJifen = list.Sum(o => o.PerformanceMark);
            //平均绩效积分
            decimal avgJifen = sumJifen / list.Count;
            return avgJifen;

        }

        /// <summary>
        /// 根据OrderId获取非技术岗位的总积分
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workpostCode"></param>
        /// <param name="jishuPostCode"></param>
        /// <returns></returns>
        public decimal GetWorkPostSumJifenByOrderId(string orderId, string workpostCode, string jishuPostCode = "A")
        {
            if (workpostCode == jishuPostCode)
            {
                throw (new Exception("需要统计的岗位，不能是技术岗位"));
            }

            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkPostCode = workpostCode }), "Id asc", 500);
            //如果没有记录则返回0；
            if (list.Count <= 0)
                return 0;

            //技术岗位总积分      岗位人数 ×  岗位比例 × 技术岗位平均绩效分
            decimal sumJifen = list.Count
                                * Convert.ToDecimal(workPost.Where(o => o.FCode == list[0].WorkPostCode).FirstOrDefault().MgrValue)
                                * GetJishuGangweiAvgByOrderId(orderId, jishuPostCode);
            return sumJifen;
        }

        /// <summary>
        /// 根据OrderId获取非技术岗位积分系数
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workpostCode"></param>
        /// <param name="jishuPostCode"></param>
        /// <returns></returns>
        public decimal GetWorkPostJifenRatioByOrderId(string orderId, string workpostCode, string jishuPostCode = "A")
        {
            /*
             1、岗位总积分等于=分监区技术岗位平均绩效工分×岗位系数×岗位人数
             2、积分系数=(岗位总积分)/∑(出勤天数×工作评议×工作等级系数)
             3、个人绩效积分=个人出勤天数×工作评议×工作等级系数×(积分系数)
             */

            if (workpostCode == jishuPostCode)
            {
                throw (new Exception("需要统计的岗位，不能是技术岗位"));
            }
            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkPostCode = workpostCode }), "Id asc", 500);


            //如果没有记录则返回0；
            if (list.Count <= 0)
                return 0;

            var query = from s in list
                        join c in workLevel on s.WorkLevel equals c.FCode
                        join e in workComments on s.WorkCommentCode equals e.FCode
                        where s.WorkPostCode == workpostCode
                        select new
                        {
                            Id = s.Id,
                            OrderId = s.OrderId,
                            FCrimeCode = s.FCrimeCode,
                            FCrimeName = s.FCrimeName,
                            workdays = s.WorkDays,
                            workLevelRatio = c.MgrValue,
                            postRetio = e.MgrValue,
                        };


            //岗位总积分=技术岗位平均绩效工分*岗位比例*辅助人数
            decimal sumJifen = GetJishuGangweiAvgByOrderId(orderId, jishuPostCode)
                            * Convert.ToDecimal(workPost.Where(o => o.FCode == workpostCode).FirstOrDefault().MgrValue)
                            * list.Count;

            //积分系数=(岗位总积分)/∑(出勤天数×工作评议×工作等级系数)


            decimal _days = query.Sum(o => o.workdays * Convert.ToDecimal(o.workLevelRatio)
                                                      * Convert.ToDecimal(o.postRetio)
            );
            //绩效系数=总积分/工作日(系数)
            decimal perRatio = sumJifen / _days;


            return perRatio;
        }

        /// <summary>
        /// 根据OrderId设置技术岗位个人的绩效、考勤、夜值工分
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="jishuPostCode"></param>
        /// <returns></returns>
        public ResultInfo SetJishuPostJixiaoByOrderId(string orderId, string jishuPostCode = "A")
        {
            ResultInfo _r = new ResultInfo();


            /*
             非技术工作岗位积分=积分系数×出勤天数×岗位评议×等级系数
             */

            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkPostCode = jishuPostCode }), "Id asc", 500);
            //如果没有记录则返回0；
            if (list.Count <= 0)
            {
                _r.ReMsg = "记录数为0条";
                return _r;
            }

            for (int i = 0; i < list.Count; i++)
            {
                //技术岗根据完成率来计算绩效积分
                list[i].PerformanceMark = this.GetJishuGangWeiJixiaoGongfen(list[i].CompleteRatio);
                //出勤工分=出勤天数×出勤系数(默认每天是2分)
                list[i].WorkMark = list[i].WorkDays * chuqinRation;
                list[i].DutyMark = list[i].DutyDays * Convert.ToDecimal(dutyComments.Where(o => o.FCode == list[i].DutyCommentsCode).FirstOrDefault().MgrValue);
            }
            //更新数据
            _dal.Update<T_BC_BaochouDetail>(list);

            _r.Flag = true;
            _r.ReMsg = "更新成功";
            _r.DataInfo = list;
            return _r;
        }

        /// <summary>
        /// 根据OrderId设置非技术岗位个人的绩效、考勤、夜值工分
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workpostCode"></param>
        /// <param name="jishuPostCode"></param>
        /// <returns></returns>
        public ResultInfo SetNotJishuPostJixiaoByOrderId(string orderId, string workpostCode, string jishuPostCode = "A")
        {
            ResultInfo _r = new ResultInfo();
            if (workpostCode == jishuPostCode)
            {
                throw (new Exception("需要统计的岗位，不能是技术岗位"));
            }

            /*
             个人总积分=考勤积分+绩效积分+夜值星积分
             1、考勤积分=考勤积分*考勤系数
             2、绩效积分=根据不同岗位计算绩效
                1）技术岗绩效积分=完成率计算积分
                2）生产辅助岗总积分=技术岗位平均绩效×1.1×辅助人数
                3）一般岗位总积分=技术岗位平均绩效×0.3×一般岗位人数
                4)非技术岗位绩效积分=生产辅助岗总积分/(出勤天数×岗位评议系数)
            3、夜值星积分=夜值天数*夜值评议系数
             
             */

            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkPostCode = workpostCode }), "Id asc", 500);
            //如果没有记录则返回0；
            if (list.Count <= 0)
            {
                _r.ReMsg = "记录数为0条";
                return _r;
            }

            decimal jifenXishu = this.GetWorkPostJifenRatioByOrderId(orderId, workpostCode, jishuPostCode);

            for (int i = 0; i < list.Count; i++)
            {

                //非工作岗位工分=积分系数×出勤天数×岗位评议×工作等级系数
                list[i].PerformanceMark = jifenXishu * list[i].WorkDays
                    * Convert.ToDecimal(workLevel.Where(o => o.FCode == list[i].WorkLevel).FirstOrDefault().MgrValue)
                    * Convert.ToDecimal(workComments.Where(o => o.FCode == list[i].WorkPostCode).FirstOrDefault().MgrValue);
                //出勤工分
                list[i].WorkMark = list[i].WorkDays * chuqinRation;
                list[i].DutyMark = list[i].DutyDays * Convert.ToDecimal(dutyComments.Where(o => o.FCode == list[i].WorkPostCode).FirstOrDefault().MgrValue);

            }
            //更新数据
            _dal.Update<T_BC_BaochouDetail>(list);

            _r.Flag = true;
            _r.ReMsg = "更新成功";
            _r.DataInfo = list;
            return _r;
        }


        /// <summary>
        /// 计算[生产监区]工分兑换金额系数
        /// </summary>
        /// <param name="orderId">主单编号</param>
        /// <param name="mode">模式:1,模式:2</param>
        /// <param name="list">明细集</param>
        /// <returns></returns>
        private decimal Compute_Scjq_MarkRatio(string orderId, int mode, List<T_BC_BaochouDetail> list)
        {
            var mainOrder = _dal.GetModelFirst<T_BC_BaochouMain, T_BC_BaochouMain>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId }));
            decimal ratio = 0;
            if (mode == 1)
            {
                //系数=(基本报酬+专项报酬 - ∑个人专项报酬)/ ∑(出勤积分+绩效积分+夜值星积分)
                ratio = (mainOrder.BaseAmount - list.Sum(o => o.ExtAmount)) / list.Sum(o => o.WorkMark + o.PerformanceMark + o.DutyMark);
            }
            else if (mode == 2)
            {
                //系数=(基本报酬+专项报酬 - ∑(个人专项报酬-扣减金额))/ ∑(出勤积分+绩效积分+夜值星积分)
                ratio = (mainOrder.BaseAmount - list.Sum(o => o.ExtAmount - o.DeductAmount)) / list.Sum(o => o.WorkMark + o.PerformanceMark + o.DutyMark);
            }

            return ratio;
        }

        /// <summary>
        /// 获取生产监区积分兑换系数
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal GetSCJQ_MarkToMoneyRatio(string orderId, int mode = 1)
        {
            List<T_BC_BaochouDetail> list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId }), "Id asc", 800);

            decimal ratio = Compute_Scjq_MarkRatio(orderId, mode, list);
            return ratio;
        }

        /// <summary>
        /// 设置生产监区的积分兑换系数和金额
        /// </summary>
        /// <param name="orderId">主单号</param>
        /// <param name="mode">模式1：基本报酬+专项报酬 - ∑个人专项报酬，模式2：基本报酬+专项报酬 - ∑(个人专项报酬-扣减金额)</param>
        /// <returns></returns>
        public ResultInfo SetSCJQ_RatioAndAmount(string orderId, int mode = 1)
        {
            ResultInfo _rs = new ResultInfo();
            List<T_BC_BaochouDetail> list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId }), "Id asc", 800);

            decimal ratio = Compute_Scjq_MarkRatio(orderId, mode, list);


            for (int i = 0; i < list.Count; i++)
            {
                decimal _baseAmount = ratio * (list[i].WorkMark + list[i].PerformanceMark + list[i].DutyMark);
                decimal _sumAmount = _baseAmount + list[i].ExtAmount - list[i].DeductAmount;
                decimal _shifaAmount = _sumAmount;
                decimal _workPostSalaryEnd = Convert.ToDecimal(workPost.Where(o => o.FCode == list[i].WorkPostCode).FirstOrDefault().AreaEnd);
                decimal _overrunAmount = (decimal)0;
                if (_sumAmount > _workPostSalaryEnd)
                {
                    _shifaAmount = _workPostSalaryEnd;
                    _overrunAmount = _sumAmount - _workPostSalaryEnd;
                }
                //积分兑换金额系统
                list[i].WorkRatio = ratio;
                //根据兑换系数--》计算基本劳酬
                list[i].BaseAmount = _baseAmount;
                //计算应发合计金额
                list[i].SumAmount = _sumAmount;
                list[i].OverrunAmount = _overrunAmount;
                list[i].Amount = _shifaAmount;
            }
            _dal.Update<T_BC_BaochouDetail>(list);

            _rs.Flag = true;
            _rs.ReMsg = "更新成功";
            _rs.DataInfo = list;

            return _rs;
        }



        #endregion



        #region 非生产监区后勤岗位相关计算


        /// <summary>
        /// 获取上年度全监罪犯平均绩效工分
        /// </summary>
        /// <returns></returns>
        private decimal GetLastYearAvgMark()
        {
            ResultInfo _rs = new ResultInfo();

            string startMonth = DateTime.Now.AddYears(-1).ToString("yyyy01");
            string endMonth = DateTime.Now.AddYears(-1).ToString("yyyy12");


            string otherQuery = $"YearMonth>='{startMonth}' and YearMonth<='{endMonth}'";
            decimal monthNum = _dal.GetPageList<T_BC_BaochouMain, T_BC_BaochouMain>(
                "Id asc", "", 1, 1000, otherQuery, "*").rows.GroupBy(o => o.YearMonth).Count();
            decimal diffNum = 12 - monthNum;
            if (diffNum > 0)
            {
                endMonth = DateTime.Now.ToString("yyyy") + diffNum.ToString("00");
                otherQuery = $"YearMonth>='{startMonth}' and YearMonth<='{endMonth}'";
            }

            var list = _dal.GetPageList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                "Id asc", "", 1, 60000, otherQuery, "*").rows;

            decimal _avgMark = list.Sum(o => o.PerformanceMark) / list.Count;
            return _avgMark;
        }
        /// <summary>
        /// 获取后勤监区总金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal GetHouqinJianQuSumAmount(string orderId, string workPostCode)
        {
            decimal _avgMark = GetLastYearAvgMark();

            var ls = _dal.GetPageList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                "Id asc", Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkPostCode = workPostCode })
                 , 1, 60000, "", "*").rows;
            //平均绩效*比例*数量
            decimal sumMark = _avgMark
                                * Convert.ToDecimal(workPost.Where(o => o.FCode == workPostCode).FirstOrDefault().MgrValue)
                                * ls.Count;
            return sumMark;
        }

        /// <summary>
        /// 计算后勤监区工分及基本金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ResultInfo SetHouqinJianQuSumAmount(string orderId)
        {
            ResultInfo _rs = new ResultInfo();
            //获取上年度平均绩效
            decimal _avgMark = GetLastYearAvgMark();

            //先找出这个订单的所有明细
            var ls = _dal.GetPageList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                "Id asc", Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId })
                 , 1, 60000, "", "*").rows;

            var items = ls.GroupBy(o => o.WorkPostCode);

            //总绩效 = 平均绩效 * 比例 * 数量
            decimal sumMark = 0;
            foreach (var item in items)
            {
                sumMark = _avgMark
                                * Convert.ToDecimal(workPost.Where(o => o.FCode == item.Key).FirstOrDefault().MgrValue)
                                * ls.Where(o => o.WorkPostCode == item.Key).Count();
                //计算每个人的绩效金额
                var subls = ls.Where(o => o.WorkPostCode == item.Key).ToList();
                decimal jifenRatio = sumMark / ls.Sum(o => o.WorkDays * Convert.ToDecimal(workComments.Where(p => p.FCode == o.WorkCommentCode).FirstOrDefault().MgrValue));
                for (int i = 0; i < subls.Count; i++)
                {
                    //岗位评议系数
                    decimal _workCommentsRatio = Convert.ToDecimal(workComments.Where(p => p.FCode == subls[i].WorkCommentCode).FirstOrDefault().MgrValue);
                    //夜值评议系数
                    decimal _dutyCommentsRatio = Convert.ToDecimal(dutyComments.Where(p => p.FCode == subls[i].DutyCommentsCode).FirstOrDefault().MgrValue);

                    decimal _workMark = subls[i].WorkDays * chuqinRation;//出勤工分
                    decimal _dutyMark = subls[i].DutyDays * _workCommentsRatio;//夜值工分
                    decimal _perMark = subls[i].WorkDays * _workCommentsRatio * jifenRatio;//工作绩效


                    decimal _baseAmount = (_perMark //工作绩效
                                            + _workMark  //出勤工分
                                            + _dutyMark)//夜值工分
                                            * houqinMarkToMoney;

                    decimal _sumAmount = _baseAmount + subls[i].ExtAmount - subls[i].DeductAmount;
                    decimal _shifaAmount = _sumAmount;
                    decimal _workPostSalaryEnd = Convert.ToDecimal(workPost.Where(o => o.FCode == subls[i].WorkPostCode).FirstOrDefault().AreaEnd);
                    decimal _overrunAmount = (decimal)0;

                    //绩效积分  houqinMarkToMoney
                    subls[i].PerformanceMark = _perMark;
                    subls[i].WorkMark = _workMark;
                    subls[i].DutyMark = _dutyMark;
                    subls[i].WorkRatio = houqinMarkToMoney;
                    //基本劳酬
                    subls[i].BaseAmount = _baseAmount;
                    //专项劳酬不计算在内

                    subls[i].SumAmount = _sumAmount;
                    subls[i].OverrunAmount = _overrunAmount;
                    subls[i].Amount = _shifaAmount;
                }
                //更新明细中的：出勤工分、绩效工分、夜值工分、基本劳酬金额
                _dal.Update<T_BC_BaochouDetail>(subls);
            }

            var rows = _dal.GetPageList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                "Id asc", Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId })
                 , 1, 60000, "", "*");

            _rs.Flag = true;
            _rs.ReMsg = "成功";
            _rs.DataInfo = rows;

            return _rs;
        }



        #endregion


        #region 数据验证功能

        /// <summary>
        /// 获取岗位评议比例
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetPostAssignRation(string orderId, string workCommentCode)
        {
            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId, WorkCommentCode = workCommentCode })
                , "", 1000);
            decimal _num = list.Count;
            var ss = list.GroupBy(o => o.WorkCommentCode);
            foreach (var item in ss)
            {
                if (!string.IsNullOrWhiteSpace(item.Key))
                {
                    dict.Add(item.Key, item.Count() / _num * 100);//乘以100 按百分数显示
                }
            }

            return dict;
        }

        /// <summary>
        /// 验证岗位评价分配比例是否符合规定
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="workCommentCode"></param>
        /// <returns></returns>
        public ResultInfo ChkPostAssignRation(string orderId, string workCommentCode)
        {
            ResultInfo _rs = new ResultInfo()
            {
                Flag = true,
                ReMsg = "",
                DataInfo = null
            };

            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                Newtonsoft.Json.JsonConvert.SerializeObject(new { OrderId = orderId })
                , "", 1000);

            //先按岗位分类
            var ss = list.GroupBy(o => o.WorkPostCode);
            foreach (var item in ss)
            {
                decimal _num = item.Count();//岗位的总人数

                string commentAssignRatio = workPost.Where(o => o.FCode == item.Key).FirstOrDefault().AreaStart;
                Dictionary<string, string> dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(commentAssignRatio);


                //岗位子集
                var postSubList = list.Where(o => o.WorkPostCode == item.Key);
                //再按岗位评价分类
                var commentGroupbySubList = postSubList.GroupBy(o => o.WorkCommentCode);
                foreach (var row in commentGroupbySubList)
                {
                    if (!string.IsNullOrWhiteSpace(row.Key))
                    {
                        decimal bili = row.Count() / _num * 100;
                        string[] _sets = dict[item.Key].Split((char)124);
                        if (bili > Convert.ToDecimal(_sets[0]))
                        {
                            _rs.Flag = false;
                            _rs.ReMsg = $"岗位:{item.Key},评价为{row.Key}的占比为{bili},不符合规定";
                            _rs.DataInfo = commentAssignRatio;
                            return _rs;
                        }
                    }
                }
            }



            return _rs;


        }
        #endregion


        #region 刑释劳酬计算

        /// <summary>
        /// 计算刑释前第一个月劳酬日均工资
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="completeValue"></param>
        /// <returns></returns>
        public ResultInfo GetXingshiDayAvgMoney(string fcrimecode, decimal completeValue)
        {
            ResultInfo _rs = new ResultInfo();
            //个人刑释前第二个月和第三个月的平均值计算，如果低于70%的不发放
            var list = _dal.GetModelList<T_BC_BaochouDetail, T_BC_BaochouDetail>(
                Newtonsoft.Json.JsonConvert.SerializeObject(
                    new
                    {
                        FCrimeCode = fcrimecode
                    ,
                        OrderStatus = 1
                    })
                , "Id desc", 2);
            decimal _completeAvgValue = list.Sum(o => o.CompleteRatio) / list.Count;
            if (completeValue < _completeAvgValue * this.xingshiWanchengBili)
            {
                _rs.ReMsg = "完成比例低于70%，不发放";
                _rs.Flag = false;
                _rs.DataInfo = 0;

                return _rs;
            }

            decimal dayAvgMoney = list.Sum(o => o.BaseAmount) / list.Sum(o => o.WorkDays);

            _rs.ReMsg = "日均工资计算完成";
            _rs.Flag = true;
            _rs.DataInfo = dayAvgMoney;

            return _rs;
        }

        /// <summary>
        /// 计算刑释前第一个月劳酬工资
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="workDays"></param>
        /// <param name="completeValue"></param>
        /// <returns></returns>
        public ResultInfo GetXingshiMoney(string fcrimecode, int workDays, decimal completeValue)
        {
            ResultInfo _rs = new ResultInfo();
            _rs = GetXingshiDayAvgMoney(fcrimecode, completeValue);
            if (_rs.Flag == true)
            {
                _rs.DataInfo = workDays * Convert.ToDecimal(_rs.DataInfo);
            }
            return _rs;
        }

        #endregion

    }
}