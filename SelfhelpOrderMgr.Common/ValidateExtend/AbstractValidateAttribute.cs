using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Common.ValidateExtend
{
    public abstract class AbstractValidateAttribute : Attribute
    {
        //public abstract bool Validate(object oValue);
        public abstract ValidateErrorModle Validate(object oValue);
    }
}
