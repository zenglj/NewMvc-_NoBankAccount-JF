using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 罪犯基本信息
    /// </summary>
    public class YzglzfdkJbxx: YzglBaseModel
    {
        public string aflb { get; set; }//案犯类别 对应编码 yzgl_aflb
        public string bhm { get; set; }//别化名
        public string bqhy { get; set; }//捕前婚姻 对应编码 yzgl_hyqk
        public string bqmm { get; set; }//案犯类别 对应编码 yzgl_zzmm
        public string bqwhcd { get; set; }//捕前文化程度 对应编码 yzgl_whcd
        public string bqzy { get; set; }//捕前职业 对应编码 yzgl_zyfl
        public string bznx { get; set; }//剥政年限 /驱逐出境
        public string bznxsfzs { get; set; }//剥政年限是否终身 1 是
        public string cnbz { get; set; }//是否未成年犯罪 1是，0否
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string csqh { get; set; }//出生地 对应编码：sys_gjxzqh
        public string csrq { get; set; }//出生日期
        public string dah { get; set; }//档案号
        public string dbmx { get; set; }//逮捕机关称谓 对应编码： 1X
        public string dbqh { get; set; }//逮捕机关区划 对应编码：sys_gjxzqh
        public string dbrq { get; set; }//逮捕日期
        public string deptId { get; set; }//部门Id
        public string deptName { get; set; }//部门名称
        public string drrq { get; set; }//调入日期
        public string fgdj { get; set; }//分管等级
        public string fj { get; set; }//罚金及没收财产
        public string fjjn { get; set; }//罚金缴纳
        public string fylx { get; set; }//分押类型
        public string fzrq { get; set; }//犯罪日期
        public string fzss { get; set; }//犯罪事实
        public string gw { get; set; }//岗位
        public string gz { get; set; }//工种
        public string hcdr { get; set; }//何处调入
        public string hjmx { get; set; }//户籍地址
        public string hjqh { get; set; }//户籍区划    对应编码：sys_gjxzqh
        public string hkfl { get; set; }//户口分类
        //public string id { get; set; }//Id 主键
        public string jgqh { get; set; }//籍贯区划/国籍(汉字) 对应编码：sys_gjxzqh
        public string jp { get; set; }//罚金及没收财产
        public string jtmx { get; set; }//家庭地址
        public string jtqh { get; set; }//家庭地址区划 对应编码：sys_gjxzqh
        public string jxcd { get; set; }//减刑尺度
        public string jxxz { get; set; }//限制减刑
        public string jyDb { get; set; }//寄押队别
        public string jyJy { get; set; }//寄押监狱
        public string jyrq { get; set; }//羁押日期
        public string lgf { get; set; }//累惯犯
        public string mscc { get; set; }//没收财产
        public string mspc { get; set; }//民事赔偿
        public string mz { get; set; }//民族 对应编码 yzgl_mz
        public string pjlx { get; set; }//判决类型 对应编码 sys_pjlx
        public string pjqh { get; set; }//判决区划 对应编码 yzgl_pjjg
        public string pjrq { get; set; }//判决日期
        public string pjzh { get; set; }//判决字号
        public string qkcs { get; set; }//前科次数
        public string qp { get; set; }//全拼
        public string qsqh { get; set; }//起诉区划 对应编码 zfxt_jcy
        public string qszh { get; set; }//起诉字号
        public string qzcj { get; set; }//是否驱逐出境 对应编码 sys_yes_no
        public string rjrq { get; set; }//入监日期
        public string rjsfcn { get; set; }//入监是否成年 对应编码 sys_yes_no
        public string sfSd { get; set; }//三涉涉毒(0否1是)
        public string sfSe { get; set; }//三涉涉恶(0否1是)
        public string sfSh { get; set; }//三涉涉黑(0否1是)
        public string sfSk { get; set; }//三涉涉恐(0否1是)
        public string sfSq { get; set; }//三涉涉枪(0否1是)
        public string sfbm { get; set; }//是否不明 对应编码 sys_yes_no
        public string sfcn { get; set; }//是否成年 对应编码 sys_yes_no
        public string ywss { get; set; }//有无上诉
        public string sybq { get; set; }//所有标签
        public string sylb { get; set; }//收押类别 对应编码 yzgl_sylb
        public string syxq { get; set; }//剩余刑期
        public string syzm { get; set; }//所有罪名
        public string tc { get; set; }//特长
        public string tcwfsd { get; set; }//退出违法所得
        public string thfz { get; set; }//团伙犯罪 对应编码：yzgl_gtfzlb
        public string thrs { get; set; }//团伙人数
        public string tts { get; set; }//四史_逃脱史(0否1是)
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间
        public string xb { get; set; }//性别 对应编码：yzgl_xb
        public string xds { get; set; }//四史_吸毒史(0否1是)
        public string xjs { get; set; }//四史_袭警史(0否1是)
        public string xq { get; set; }//刑期
        public string xqqr { get; set; }//刑期起日
        public string xqzr { get; set; }//刑期止日
        public string xwhcd { get; set; }//现文化程度 对应编码：yzgl_whcd
        public string xwhcdd { get; set; }//现文化程度(中文)
        public string xz { get; set; }//刑种 对应编码：yzgl_xz
        public string ypbznx { get; set; }//原判剥政年限
        public string ypbznxsfzs { get; set; }//原判剥政年限是否终身 0否1是
        public string ypxq { get; set; }//原判刑期
        public string ypxqqr { get; set; }//原判刑期起日
        public string ypxqzr { get; set; }//原判刑期止日
        public string ypxz { get; set; }//原判刑种 对应编码：yzgl_xz
        public string zdxq { get; set; }//折抵刑期
        public string zfZt { get; set; }//罪犯状态 对应编码：sys_zf_zt
        public string zfbh { get; set; }//罪犯编号
        public string zfxm { get; set; }//罪犯姓名
        public string zj { get; set; }//追缴
        public string zjHm { get; set; }//证件号码
        public string zjLx { get; set; }//证件类型 对应编码：yzgl_zjlx
        public string zltp { get; set; }//责令退赔
        public string zmm { get; set; }//所有罪名标签
        public string zmmc { get; set; }//罪名名称
        public string zrsy { get; set; }//止日顺延
        public string zsqk { get; set; }//终审情况
        public string zss { get; set; }//四史_自杀史(0否1是)
        public string zsxm { get; set; }//真实姓名

    }
}