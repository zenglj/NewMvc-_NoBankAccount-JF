using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 社会关系对象
    /// </summary>
    public class YzglzfdkShgx: YzglBaseModel
    {
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string csrq { get; set; }//出生日期
        public string deptId { get; set; }//部门Id
        public string deptName { get; set; }//部门名称
        public string dh { get; set; }//电话
        public string dwmx { get; set; }//单位地址(详细地址)
        public string dwqh { get; set; }//单位区划(行政区划) 对应编码:sys_jgxzqh
        public string gxlb { get; set; }//关系类别 对应编码:yzgl_gxcw(关系称谓)
        //public string id { get; set; }//
        public string jtmx { get; set; }//家庭地址(详细地址)
        public string jtqh { get; set; }//家庭区划(行政区划) 对应编码：yzgl_gjxzqh
        public string mm { get; set; }//政治面貌 对应编码：yzgl_zzmm
        public string qqdh { get; set; }//是否是亲情电话 对应编码:sys_yes_no
        public string remark { get; set; }//备注
        public string rkType { get; set; }//
        public string sh { get; set; }//是否审核 对应编码：sys_yes_no
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间
        public string xb { get; set; }//关系人性别 对应编码:yzgl_xb
        public string xm { get; set; }//关系人姓名
        public string yzbm { get; set; }//家庭地址邮政编码
        public string zfbh { get; set; }//罪犯编号
        public string zfxm { get; set; }//罪犯姓名
        public string zjhm { get; set; }//证件号码
        public string zjlx { get; set; }//证件类型 对应编码：yzgl_zjlx
        public string zlxr { get; set; }//是否主联系人 对应编码：sys_yes_no
        public string zw { get; set; }//职位 对应编码：yzgl_zw
        public string zy { get; set; }//职业 对应编码：yzgl_zyfl

    }
}