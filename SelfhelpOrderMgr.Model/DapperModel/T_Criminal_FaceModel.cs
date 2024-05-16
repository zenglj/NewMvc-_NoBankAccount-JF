using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Criminal_FaceModel:BaseModel
    {
        public string fcrimeCode { get; set; }
        public string fcrimeName { get; set; }
        public string imagePath { get; set; }
        public byte[] feature { get; set; }
        public int featureSize { get; set; }
        public DateTime createDate { get; set; }
        public int typeFlag { get; set; }

    }
}