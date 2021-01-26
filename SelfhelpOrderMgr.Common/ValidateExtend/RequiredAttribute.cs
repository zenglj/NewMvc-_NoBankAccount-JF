using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Common.ValidateExtend
{
    public class RequiredAttribute : AbstractValidateAttribute
    {
        public override ValidateErrorModle Validate(object oValue)
        {
            throw new Exception();
            //return oValue != null
            //    && !string.IsNullOrWhiteSpace(oValue.ToString());
        }
    }
}
