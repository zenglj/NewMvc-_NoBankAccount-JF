using SelfhelpOrderMgr.Common.CustomExtend;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfhelpOrderMgrCommon.CustomExtend
{
    /// <summary>
    /// 自定义返回值类型
    /// </summary>
    public class EnumberCreditType
    {
        /// <summary>  
        /// 枚举对象的值  
        /// </summary>  
        public int Value { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Name { get; set; }

    }
    public static class CustomEnumToList
    {

        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumberCreditType> EnumToList<T>()
        {
            List<EnumberCreditType> list = new List<EnumberCreditType>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumberCreditType m = new EnumberCreditType();

                //Display
                object[] disArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(RemarkAttribute), true);
                if (disArr != null && disArr.Length > 0)
                {
                    RemarkAttribute da = disArr[0] as RemarkAttribute;
                    m.Name = da.Remark;
                }

                m.Value = Convert.ToInt32(e);
                list.Add(m);
            }
            return list;
        }
    }
}
