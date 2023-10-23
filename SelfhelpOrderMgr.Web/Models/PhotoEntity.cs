using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    /// <summary>
    /// 相片信息
    /// </summary>
    public class PhotoEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string fcrimeCode { get; set; }
        /// <summary>
        /// 相片名称
        /// </summary>
        public string photoName { get; set; }

        /// <summary>
        /// 相片Base64位数据
        /// </summary>
        public string photoBase64Data { get; set; }

        /// <summary>
        /// 注册类型，0是犯人，1是民警
        /// </summary>
        public int TypeFlag { get; set; }
    }
}