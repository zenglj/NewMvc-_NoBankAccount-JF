using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 法律文书对象
    /// </summary>
    public class YzglzfdkFlws: YzglBaseModel
    {
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string deptId { get; set; }//队别ID
        public string deptName { get; set; }//队别名称
        public string[] fileList { get; set; }//文件列表
        //public string id { get; set; }//id
        public string lxBm { get; set; }//类型编码
        public string lxMc { get; set; }//类型名称
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间
        public string zfbh { get; set; }//罪犯编码
        public string zfxm { get; set; }//罪犯姓名

    }
}