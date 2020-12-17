using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_AREADAL
    {
        //按用户的监区的节点数
        public IEnumerable<T_AREA> GetAreaByNodeNames(params string[] nodes)
        {
            string strNodes = "";
            if (nodes != null)
            {
                foreach (string node in nodes)
                {
                    strNodes += "'" + node + "',";
                }
                strNodes = strNodes.Substring(0, strNodes.Length - 1);
            }
            string sql = "select distinct * from t_Area where FName in(" + strNodes + ") or id in(select fid from t_Area where FName in(" + strNodes + "))";
            DataSet dt = SqlHelper.Query(sql);

            if (dt.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<T_AREA> list = new List<T_AREA>();
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    T_AREA op = SetAreaInfo(row);//设定用户记录的值
                    list.Add(op);
                }
                return list;
            }
        }
        private static T_AREA SetAreaInfo(DataRow row)
        {
            T_AREA op = new T_AREA();

            op.FCode = (string)SqlHelper.FromDbNull(row["FCode"]);
            op.FName = (string)SqlHelper.FromDbNull(row["FName"]);
            //op.FMeetdate = (string)SqlHelper.FromDbNull(row["FMeetdate"]);            
            op.ID = (string)SqlHelper.FromDbNull(row["Id"]);
            op.FID = (string)SqlHelper.FromDbNull(row["FId"]);
            op.URL = (string)SqlHelper.FromDbNull(row["URL"]);
            //op.LevelNo = (string)SqlHelper.FromDbNull(row["LevelNo"]);
            //op.seqno = (int)SqlHelper.FromDbNull(row["seqno"]);

            return op;
        }

    }
}