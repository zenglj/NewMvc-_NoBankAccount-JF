using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Common.ValidateExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneAttribute : AbstractValidateAttribute
    {
        private string _PhoneRegular = @"^[1]+[3,5]+\d{9}";

        //public override bool  Validate(object oValue)
        public override  ValidateErrorModle Validate(object oValue)
        {
            //throw new Exception();

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
            else if (Regex.IsMatch(oValue.ToString(), this._PhoneRegular))
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
            //    && !string.IsNullOrWhiteSpace(oValue.ToString())
            //    && Regex.IsMatch(oValue.ToString(), this._PhoneRegular);
        }

    }
}
