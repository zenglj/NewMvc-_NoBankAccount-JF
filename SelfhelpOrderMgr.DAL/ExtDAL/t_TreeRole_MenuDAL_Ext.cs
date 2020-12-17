using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class t_TreeRole_MenuDAL
    {
        public t_TreeRole_Menu Insert(t_TreeRole_Menu op)
        {
            var res = SqlHelper.Query(@"insert into t_TreeRole_Menu (RoleID, TreeId, flag)
                    values(@RoleID, @TreeId, @flag);SELECT @@IDENTITY",
                                               new SqlParameter("RoleID", op.RoleID),
                                               new SqlParameter("TreeId", SqlHelper.ToDbNull(op.TreeId)),
                                               new SqlParameter("flag", SqlHelper.ToDbNull(op.flag))
                                               );
            int rowid = res == null ? 0 : Convert.ToInt32(res);

            DataSet dtMain = SqlHelper.Query(@" select a.*,b.Text,b.FId,b.url from t_TreeRole_Menu a,t_TreeMeun b where a.ID=@ID",
                                               new SqlParameter("ID", rowid)
                                               );

            t_TreeRole_Menu list = new t_TreeRole_Menu();
            DataRow row = dtMain.Tables[0].Rows[0];

            t_TreeRole_Menu m2 = SetModelInfo(row);//设定用户记录的值
            return m2;
        }

        public int InsertRoleMenu(int roleId)
        {
            string sql = @"insert into t_TreeRole_Menu(RoleID,treeid,flag) select @roleId,id,flag=0  from t_treemeun ";
            return SqlHelper.ExecuteSql(sql,
                                               new SqlParameter("@roleId", roleId)
                                               );
        }
        /// <summary>
        /// 根据用户'Id'Update用户数据
        /// </summary>
        /// <param name="op"></param>
        public int UpdateByID(t_TreeRole_Menu op)
        {
            return SqlHelper.ExecuteSql(@"UPDATE t_TreeRole_Menu
                               SET RoleID = @RoleID
                                  ,TreeId = @TreeId
                                  ,flag = @flag
                             WHERE ID = @ID",
                            new SqlParameter("Id", op.id),
                            new SqlParameter("RoleID", SqlHelper.ToDbNull(op.RoleID)),
                            new SqlParameter("TreeId", SqlHelper.ToDbNull(op.TreeId)),
                            new SqlParameter("flag", SqlHelper.ToDbNull(op.flag))
                            );
        }

        public int UpdateByRoleMenu(int flag, string strRoleID, string NodesSql)
        {
            string sql = @"update t_TreeRole_Menu set flag=@flag 
                from(select fcode,id from t_treemeun where text in ( {0})) b 
                where t_TreeRole_Menu.RoleID=@RoleID and t_TreeRole_Menu.treeid=b.id";
            sql = string.Format(sql, NodesSql);
            return SqlHelper.ExecuteSql(sql,
                                               new SqlParameter("@flag", flag),
                                               new SqlParameter("@RoleID", strRoleID)
                                               );
        }

        /// 按Id查找用户记录
        public t_TreeRole_Menu GetTableById(int id)
        {
            DataSet dt = SqlHelper.Query(@"select a.*,b.Text,b.FId,b.url from t_TreeRole_Menu a,t_TreeMeun b where a.Id=@Id",
                                new SqlParameter("Id", id));
            if (dt.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else if (dt.Tables[0].Rows.Count == 1)
            {
                DataRow row = dt.Tables[0].Rows[0];
                t_TreeRole_Menu op = SetModelInfo(row);//设定用户记录的值
                return op;
            }
            else
            {
                throw new Exception("不好了,有出现重名的记录了");
            }
        }

        public int DeleteById(int id)
        {
            return SqlHelper.ExecuteSql(@"delete from t_TreeRole_Menu where Id=@Id",
                                 new SqlParameter("Id", id));
        }

        public int DeleteByRoleId(int roleId)
        {
            return SqlHelper.ExecuteSql(@"delete from t_TreeRole_Menu where RoleId=@RoleId",
                                new SqlParameter("@RoleId", roleId));
        }
        public IEnumerable<t_TreeRole_Menu> GetAll()
        {
            DataSet dt = SqlHelper.Query(@"select a.*,b.Text,b.FId,b.url from t_TreeRole_Menu a,t_TreeMeun b from t_TreeRole_Menu");
            List<t_TreeRole_Menu> list = new List<t_TreeRole_Menu>();
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                t_TreeRole_Menu op = SetModelInfo(row);//设定用户记录的值
                list.Add(op);
            }
            return list;
        }

        public IEnumerable<t_TreeRole_Menu> GetTableByRoleId(string RoleId, params int[] flags)
        {
            string sql = @"select a.*,b.Text,b.FId,b.url from t_TreeRole_Menu a,t_TreeMeun b where RoleID=@RoleID";
            if (flags != null)
            {
                sql += " and flag in (";
                foreach (int flag in flags)
                {
                    sql += flag.ToString() + ",";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";
            }
            DataSet dt = SqlHelper.Query(sql,
                                         new SqlParameter("RoleId", RoleId));
            if (dt.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<t_TreeRole_Menu> list = new List<t_TreeRole_Menu>();
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    t_TreeRole_Menu op = SetModelInfo(row);//设定用户记录的值
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
        private static t_TreeRole_Menu SetModelInfo(DataRow row)
        {
            t_TreeRole_Menu op = new t_TreeRole_Menu();
            op.id = (int)row["Id"];
            op.RoleID = (int)SqlHelper.FromDbNull(row["RoleID"]);
            op.TreeId = (int)SqlHelper.FromDbNull(row["TReeID"]);
            op.flag = (int)SqlHelper.FromDbNull(row["Flag"]);
            op.FId = (int)SqlHelper.FromDbNull(row["FID"]);
            op.Text = (string)SqlHelper.FromDbNull(row["TEXT"]);
            op.URL = (string)SqlHelper.FromDbNull(row["URL"]);
            //t_TreeMeun rm = new t_TreeMeunDAL().GetModel(op.TreeId);
            //op.Menu = rm;
            return op;
        }
    }
}