using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    /// <summary>
    /// 用户数据对象
    /// </summary>
    public class SysUser: YzglBaseModel
    {
        public Boolean admin { get; set; }//是否管理员
        public string avatar { get; set; }//用户头像
        public string cjgzsj { get; set; }//参加工作时间
        public string createBy { get; set; }//创建者
        public string createById { get; set; }//创建者Id
        public string createTime { get; set; }//创建时间
        public string delFlag { get; set; }//删除标志(0表示存在 ，2表示删除)
        public SysDept dept { get; set; }//部门数组
        public string deptId { get; set; }//部门Id
        public string deptName { get; set; }//部门名称
        public string deptType { get; set; }//部门类型
        public string email { get; set; }//电子邮箱
        public string jh { get; set; }//警号
        public string loginName { get; set; }//登录名称
        public string parentId { get; set; }//部门父Id
        public string phonenumber { get; set; }//手机号码
        public string rdrq { get; set; }//入党日期
        public string roleId { get; set; }//角色Id
        public string sex { get; set; }//性别  0表示男  1表示女  2未知
        public string sfzh { get; set; }//身份证号
        public string status { get; set; }//账号状态 0=正常 ，1=停用
        public string unitId { get; set; }//单位Id
        public string unitName { get; set; }//单位名称
        public string updateBy { get; set; }//更新者
        public string updateById { get; set; }//更新者Id
        public string updateTime { get; set; }//更新时间
        public string userId { get; set; }//用户序号
        public string userName { get; set; }//用户名称
        public string userType { get; set; }//用户类型
        public string zw { get; set; }//职位








    }
}