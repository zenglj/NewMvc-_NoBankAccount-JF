using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_Czy_areaDAL
    {

        public int BatchInsert(string fusercode)
        {
            return SqlHelper.ExecuteSql(@"insert into T_Czy_area(seqno,fcode,FareaCode,FFlag) select id,@FUserCode,Fcode,fflag=0  from t_Area ",
                new SqlParameter("@FUserCode", fusercode));
        }
        /// <summary>
        /// 按Seqno（自动编号）删除T_Czy_area(队别)数据
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="seqno"></param>
        public int DeleteBySeqno(int seqno)
        {
            return SqlHelper.ExecuteSql(@"delete from T_Czy_area where seqno=@seqno",
                                                   new SqlParameter("seqno", seqno)
                                                   );

        }

        public int DeleteByFCode(string fcode)
        {
            return SqlHelper.ExecuteSql(@"delete from T_Czy_area where FCode=@FCode",
                                                   new SqlParameter("@FCode", fcode)
                                                   );

        }

        /// <summary>
        /// 根据用户'Id'Update用户数据
        /// </summary>
        /// <param name="op"></param>
        public int UpdateBySeqno(T_Czy_area op)
        {
            return SqlHelper.ExecuteSql(@"UPDATE T_Czy_area SET fcode = @fcode,fareacode = @fareacode,fflag = @fflag
                                    WHERE seqno= @seqno",
                                                new SqlParameter("fcode", SqlHelper.ToDbNull(op.fcode)),
                                                new SqlParameter("fareacode", SqlHelper.ToDbNull(op.fareacode)),
                                                new SqlParameter("fflag", SqlHelper.ToDbNull(op.fflag)),
                                                new SqlParameter("seqno", SqlHelper.ToDbNull(op.seqno))
                                               );
        }

        /// <summary>
        /// 根据用户'Text'Update用户数据
        /// </summary>
        /// <param name="op"></param>
        public int UpdateByFCode(T_Czy_area op)
        {
            return SqlHelper.ExecuteSql(@"UPDATE T_Czy_area
                     SET fareacode = @fareacode,fflag = @fflag WHERE fcode = @fcode",
                                               new SqlParameter("fareacode", SqlHelper.ToDbNull(op.fareacode)),
                                               new SqlParameter("fflag", SqlHelper.ToDbNull(op.fflag)),
                                               new SqlParameter("fcode", SqlHelper.ToDbNull(op.fcode))
                                               );
        }

        public int UpdateByNodesAndUserCode(string fusercode, int fflag, params string[] nodes)
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
            string sql = "update T_Czy_area set fflag=@fflag from("
                    + "select fcode,id from t_Area where FName in"
                    + "(" + strNodes + ")) b"
                    + " where T_Czy_area.fcode=@fusercode and T_Czy_area.FAreaCode=b.FCode";
            return SqlHelper.ExecuteSql(sql, new SqlParameter("@fusercode", fusercode),
                 new SqlParameter("@fflag", fflag)
                );
        }

        /// <summary>
        /// 按'FCode'查找管辖监区记录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<T_Czy_area> GetAreaByFCode(string fcode)
        {
            DataSet ds = SqlHelper.Query("select * from T_Czy_area where FCode=@FCode",
                            new SqlParameter("@FCode", fcode));
            DataTable dt = ds.Tables[0];
            List<T_Czy_area> list = new List<T_Czy_area>();
            foreach (DataRow row in dt.Rows)
            {
                T_Czy_area op = SetModelInfo(row);//设定用户记录的值
                list.Add(op);
            }
            return list;
        }

        public IEnumerable<T_Czy_area> GetAreaByFCodeAndFlag(string fcode, int fflag)
        {
            DataSet dt = SqlHelper.Query(@"select * from T_Czy_area where FCode=@FCode and FFlag=@FFlag",
                                new SqlParameter("@FCode", fcode),
                                new SqlParameter("@FFlag", fflag));

            List<T_Czy_area> list = new List<T_Czy_area>();
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                T_Czy_area op = SetModelInfo(row);//设定用户记录的值
                list.Add(op);
            }
            return list;
        }

        public IEnumerable<T_Czy_area> GetAll()
        {
            DataSet dt = SqlHelper.Query(@"select * from T_Czy_area");
            List<T_Czy_area> list = new List<T_Czy_area>();
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                T_Czy_area op = SetModelInfo(row);//设定用户记录的值
                list.Add(op);
            }
            return list;
        }

        private static T_Czy_area SetModelInfo(DataRow row)
        {
            T_Czy_area op = new T_Czy_area();
            op.seqno = (int)SqlHelper.FromDbNull(row["Seqno"]);
            op.fcode = (string)SqlHelper.FromDbNull(row["FCode"]);
            op.fareacode = (string)SqlHelper.FromDbNull(row["FAreaCode"]);
            op.fflag = (int)Convert.ToInt32(SqlHelper.FromDbNull(row["fflag"]));
            T_AREA tarea = new T_AREADAL().GetModel(op.fareacode);
            op.TArea = tarea;
            return op;
        }
    }
}