using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    public class SysDept: YzglBaseModel
    {
        /// <summary>
        /// 祖级列表
        /// </summary>
        public string ancestors { get; set; }
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string delFlag { get; set; }//删除标志，0表示存在，2表示删除
        public string deptId { get; set; }//部门ID
        public string deptName { get; set; }//部门名称
        public string deptType { get; set; }//部门类型，对应编码sys_dept_type
        public int orderNum { get; set; }//显示顺序
        public string parentId { get; set; }//父部门Id
        public string parentName { get; set; }//父部门名称
        public string status { get; set; }//状态，0正常，1停用
        public string unitId { get; set; }//单位ID
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间

    }
}