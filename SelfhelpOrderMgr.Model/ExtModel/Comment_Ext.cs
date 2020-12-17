using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model.ExtModel
{
    public class Comment
    {
        public int id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string state { get; set; }
        public List<attr> attributes = new List<attr>();
        public List<Comment> children = new List<Comment>();
    }
    public class attr
    {
        public string url { get; set; }
    }
}