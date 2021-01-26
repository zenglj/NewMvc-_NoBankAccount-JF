using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Common.ValidateExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringLengthAttribute : AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;
        public StringLengthAttribute(int min, int max)
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
            else if (oValue.ToString().Length >= this._Min && oValue.ToString().Length <= this._Max)
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
            //    && oValue.ToString().Length >= this._Min
            //    && oValue.ToString().Length <= this._Max;
        }

    }
}
