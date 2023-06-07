using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 字典数据对象
    /// </summary>
    public class SysDictData: YzglBaseModel
    {
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者ID
        public string createTime { get; set; }//创建时间
        public Boolean Default { get; set; }//创建者
        public string dictCode { get; set; }//字典编码
        public string dictLabel { get; set; }//字典标签
        public Int64 dictSort { get; set; }//字典排序
        public string dictType { get; set; }//字典类型
        public string dictValue { get; set; }//字典键值
        public string parentCode { get; set; }//父级
        public string status { get; set; }//状态，0是正常，1是停用
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间


    }
}