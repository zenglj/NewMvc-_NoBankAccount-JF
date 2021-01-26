using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Common.ValidateExtend
{
    public static class AttributeExtend
    {
        public static bool Validate<T>(this T t)
        {
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    object oValue = prop.GetValue(t);
                    foreach (AbstractValidateAttribute attribute in prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true))
                    {
                        if (!attribute.Validate(oValue))
                            return false;
                    }
                }

                    //object oValue = prop.GetValue(t);
                //if (prop.IsDefined(typeof(LongAttribute), true))
                //{
                //    LongAttribute attribute = (LongAttribute)prop.GetCustomAttribute(typeof(LongAttribute), true);
                //    if (!attribute.Validate(oValue))
                //        return false;
                //}

                //if (prop.IsDefined(typeof(RequiredAttribute), true))
                //{
                //    RequiredAttribute attribute = (RequiredAttribute)prop.GetCustomAttribute(typeof(RequiredAttribute), true);
                //    if (!attribute.Validate(oValue))
                //        return false;
                //}
            }
            return true;
        }
    }
}
