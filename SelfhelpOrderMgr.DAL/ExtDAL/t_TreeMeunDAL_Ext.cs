using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class t_TreeMeunDAL
    {
        public IEnumerable<t_TreeMeun> GetTableByFId(int? fid, string RoleId)
        {
            DataSet dt = SqlHelper.Query(@"select a.* from t_TreeMeun a,t_TreeRole_Menu b 
                                        where a.id=b.treeid and b.roleid=@RoleID and FId=@FId and b.Flag>0 order by a.id",
                                         new SqlParameter("FId", fid),
                                         new SqlParameter("RoleID", RoleId));
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{ 

            //}
            if (dt.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<t_TreeMeun> list = new List<t_TreeMeun>();
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    t_TreeMeun op = SetTreeMenuInfo(row);//设定用户记录的值
                    list.Add(op);
                }
                return list;
            }
        }
        /// <summary>
        /// 根据数据行,设定用户T_CZY记录的值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static t_TreeMeun SetTreeMenuInfo(DataRow row)
        {
            t_TreeMeun op = new t_TreeMeun();
            op.id = (int)row["Id"];
            op.fcode = (string)SqlHelper.FromDbNull(row["FCode"]);
            op.flag = (int)SqlHelper.FromDbNull(row["Flag"]);
            op.Text = (string)SqlHelper.FromDbNull(row["Text"]);
            op.FId = (int)SqlHelper.FromDbNull(row["FId"]);
            op.URL = (string)SqlHelper.FromDbNull(row["URL"]);

            return op;
        }
    }
}