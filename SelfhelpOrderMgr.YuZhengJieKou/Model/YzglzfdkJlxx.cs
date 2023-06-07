using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 简历信息对象
    /// </summary>
    public class YzglzfdkJlxx: YzglBaseModel
    {
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string deptId { get; set; }//部门Id
        public string deptName { get; set; }//部门名称
        public string dwmx { get; set; }//单位地址(详细住址)
        public string dwqh { get; set; }//单位区划(行政区划) 对应编码:sys_gjxzqh
        //public string id { get; set; }//id
        public string qr { get; set; }//起日
        public string remark { get; set; }//备注
        public string rkType { get; set; }//
        public string sfbqdw { get; set; }//是否捕前单位 对应编码:sys_yes_no
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间
        public string zc { get; set; }//职称
        public string zhbh { get; set; }//罪犯编号
        public string zhxm { get; set; }//罪犯姓名
        public string zr { get; set; }//止日
        public string zw { get; set; }//职务 对应编码:yzgl_zw
        public string zy { get; set; }//职业 对应编码:yzgl_zy

    }
}