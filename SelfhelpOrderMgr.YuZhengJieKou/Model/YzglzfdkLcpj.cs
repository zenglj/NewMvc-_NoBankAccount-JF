using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 历次判决对象
    /// </summary>
    public class YzglzfdkLcpj: YzglBaseModel
    {
        public string aflb { get; set; }//案犯类别
        public string byxjs { get; set; }//不允许假释
        public string bznx { get; set; }//剥夺政治权利期限
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string deptId { get; set; }//部门Id
        public string deptName { get; set; }//部门名称
        public string fileList { get; set; }//文件列表
        public string fjje { get; set; }//罚金金额
        public string fjjeMz { get; set; }//罚金汉字显示
        //public string id { get; set; }//部门名称
        public string jxxz { get; set; }//减刑限制
        public string mscc { get; set; }//部门名称
        public string  mspcje { get; set; }//民事赔偿金额
        public string mspj { get; set; }//民事判决
        public string pcnd { get; set; }//判决案号(年度)
        public string pcqh { get; set; }//判决法院 对应编号yzgl_pjjg
        public string pcxh { get; set; }//案号(序号)
        public string pczh { get; set; }//法院简称及字号
        public string pjlx { get; set; }//判决类型 对应编号sys_pjlx
        public string pjrq { get; set; }//判决日期
        public string qzcj { get; set; }//是否驱逐出境 对应编码 sys_yes_no
        public string sfzscd { get; set; }//是否终审裁定 sys_yes_no
        public string sgtz { get; set; }//手工调整
        public string tazh { get; set; }//同案字号
        public string tcwfsd { get; set; }//退出违法所得
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updteBy { get; set; }//更新者
        public string updteById { get; set; }//更新者Id
        public string updteTime { get; set; }//更新时间
        public string xq { get; set; }//刑期
        public string xqqr { get; set; }//刑期起日
        public string xqzr { get; set; }//刑期止日
        public string xz { get; set; }//刑种 对应编码yzgl_xz
        public string ywss { get; set; }//有无上述 对应编码yzgl_xz
        public string[] yzglzfdkLcpjfblist { get; set; }//列表
        public string zfxm { get; set; }//罪犯姓名
        public string zj { get; set; }//追缴
        public string zltp { get; set; }//责令退赔
        public string zmbm { get; set; }//罪名编码 对应编码：yzgl_zm
        public string zmmc { get; set; }//罪名名称
        public string zsqk { get; set; }//终审情况 yzgl_zsqk

    }
}