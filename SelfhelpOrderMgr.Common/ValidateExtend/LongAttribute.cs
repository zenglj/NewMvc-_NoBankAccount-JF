using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Common.ValidateExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LongAttribute : AbstractValidateAttribute
    {
        private long _Min = 0;
        private long _Max = 0;
        public LongAttribute(long min, long max)
        {
            this._Min = min;
            this._Max = max;
        }

        //public override bool Validate(object oValue)
        public override ValidateErrorModle Validate(object oValue)

        {
            if (oValue == null)
            {
                //return false;
                return new ValidateErrorModle()
                {
                    Result = false,
                    Message = $"{nameof(EmailAttribute)} oValue is null"
                };
            }
            else if (string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                //return false;
                return new ValidateErrorModle()
                {
                    Result = false,
                    Message = $"{nameof(EmailAttribute)} oValue is 空白"
                };
            }
            else if (long.TryParse(oValue.ToString(), out long lValue)
                && lValue >= this._Min
                && lValue <= this._Max)
            {
                return new ValidateErrorModle()
                {
                    Result = false,
                    Message = $"{nameof(EmailAttribute)} oValue 不满足格式约束"
                };
            }
            else
            {
                return new ValidateErrorModle()
                {
                    Result = true,
                    Message = $"{nameof(EmailAttribute)} 校验成功"
                };
            }
            //return oValue != null
            //    && long.TryParse(oValue.ToString(), out long lValue)
            //    && lValue >= this._Min
            //    && lValue <= this._Max;
        }

    }
}
