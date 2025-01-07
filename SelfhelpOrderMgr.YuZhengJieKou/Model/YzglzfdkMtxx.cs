using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    class YzglzfdkMtxx: YzglBaseModel
    {
        public string unitName { get; set; }
        public string deptName { get; set; }
        public string zfbh { get; set; }
        public string zfxm { get; set; }
        public string unitId { get; set; }
        public string deptId { get; set; }
        public List<YzglFileList> fileList { get; set; }

    }
    class YzglFileList 
    {
        public string fileName { get; set; }
        public string format { get; set; }
        public string base64Data { get; set; }

    }
}
