using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class FaceCheckResult
    {
        /// <summary>
        /// 节点信息
        /// </summary>
        public string EndPointInfo { get; set; }

        /// <summary>
        /// 人脸顺序号
        /// </summary>
        public string FaceNumber { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// IC卡号
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 用户类别，0犯人，1民警
        /// </summary>
        public int typeFlag { get; set; }
    }
}