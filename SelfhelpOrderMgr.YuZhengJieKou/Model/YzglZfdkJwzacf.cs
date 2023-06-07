using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 既往治安处罚对象
    /// </summary>
    public class YzglZfdkJwzacf:YzglBaseModel
    {
        public string ay { get; set; }//案由
        public string bz { get; set; }//备注
        public string cfqx { get; set; }//处罚期限
        public string cfrq { get; set; }//处罚日期
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string deptId { get; set; }//部门Id
        public string deptName { get; set; }//部门名称
        //public string id { get; set; }//
        public string rkType { get; set; }//
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间
        public string zacflb { get; set; }//治安处罚类别 对应编码：yzgl_zaclb
        public string zc { get; set; }//罪错
        public string zfbh { get; set; }//罪犯编号
        public string zfxm { get; set; }//罪犯姓名
        public string zxjgjgcw { get; set; }//执行机关(机关称谓)
        public string zxjglb { get; set; }//执行机关类别 对应编码：yzgl_zxjglb
        public string zxjgssdq { get; set; }//执行机关所属地区 对应编码：yzgl_dq
        public string zxjgxzqhmc { get; set; }//单位地址(行政区划) 对应编码:sys_jgxzqh
        public string zxqr { get; set; }//执行起日
        public string zxzr { get; set; }//执行止日


    }
}