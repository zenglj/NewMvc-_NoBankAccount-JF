using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace SelfhelpOrderMgr.Common.CustomExtend
{
    //静态类+静态方法 + 参数前加this 进行修饰 就变成类的扩展了

    /// <summary>
    /// 是给枚举用  提供一个额外信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Method)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            this.Remark = remark;
        }

        public string Remark { get; private set; }
    }

    public static class RemarkExtend
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            FieldInfo field = type.GetField(enumValue.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
                return remarkAttribute.Remark;
            }
            else
            {
                return enumValue.ToString();
            }
        }
    }

    [Remark("用户状态")]
    public enum UserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("正常")]
        Normal = 0,
        /// <summary>
        /// 冻结
        /// </summary>
        [Remark("冻结")]
        Frozen = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Remark("删除")]
        Deleted = 2
    }

    [Remark("设备状态")]
    public enum DeviceState
    {
        /// <summary>
        /// 未激活
        /// </summary>
        [Remark("未激活")]
        NoActive = 0,
        /// <summary>
        /// 在线
        /// </summary>
        [Remark("在线")]
        OnLine = 1,
        /// <summary>
        /// 离线
        /// </summary>
        [Remark("离线")]
        OffLine = 2,
        /// <summary>
        /// 授权过期
        /// </summary>
        [Remark("授权过期")]
        AuthOver = 3
    }

    [Remark("授权状态")]
    public enum DeviceAuthState
    {
        /// <summary>
        /// 未授权
        /// </summary>
        [Remark("未授权")]
        NoAuth = 0,
        /// <summary>
        /// 授权
        /// </summary>
        [Remark("授权")]
        IsAuth = 1        
    }


    [Remark("转账方式")]
    public  enum InterFaceTranType:int
    {
        /// <summary>
        /// 公对私
        /// </summary>
        [Remark("公对私")]
        BtoC = 0,
        /// <summary>
        /// 公对公
        /// </summary>
        [Remark("公对公")]
        BtoB = 1,
        /// <summary>
        /// 快捷代发
        /// </summary>
        [Remark("快捷代发")]
        FastTran = 2
    }

    [Remark("转账方式")]
    public  enum MoneyPayMode:int
    {
        /// <summary>
        /// 网点支取
        /// </summary>
        [Remark("网点支取")]
        Outlets = 0,
        /// <summary>
        /// 现金领取
        /// </summary>
        [Remark("现金领取")]
        Cash = 1,
        /// <summary>
        /// 转账
        /// </summary>
        [Remark("转账支付")]
        TranAccount = 2
    }

}
