
using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_Czy_areaBLL
    {

        public int BatchInsert(string fusercode)
        {
            return new T_Czy_areaDAL().BatchInsert(fusercode);
        }

        /// <summary>
        /// 按Seqno（自动编号）删除T_Czy_area(队别)数据
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="seqno"></param>
        public int DeleteBySeqno(int seqno)
        {
            return new T_Czy_areaDAL().DeleteBySeqno(seqno);
        }

        public int DeleteByFCode(string fcode)
        {
            return new T_Czy_areaDAL().DeleteByFCode(fcode);
        }

        /// <summary>
        /// 根据用户'Id'Update用户数据
        /// </summary>
        /// <param name="op"></param>
        public int UpdateBySeqno(T_Czy_area op)
        {
            return new T_Czy_areaDAL().UpdateBySeqno(op);
        }

        /// <summary>
        /// 根据用户'Text'Update用户数据
        /// </summary>
        /// <param name="op"></param>
        public int UpdateByFCode(T_Czy_area op)
        {
            return new T_Czy_areaDAL().UpdateByFCode(op);
        }

        public int UpdateByNodesAndUserCode(string fusercode, int fflag, params string[] nodes)
        {
            return new T_Czy_areaDAL().UpdateByNodesAndUserCode(fusercode, fflag, nodes);
        }

        /// <summary>
        /// 按'FCode'查找管辖监区记录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<T_Czy_area> GetAreaByFCode(string fcode)
        {
            return new T_Czy_areaDAL().GetAreaByFCode(fcode);
        }

        public IEnumerable<T_Czy_area> GetAreaByFCodeAndFlag(string fcode, int fflag)
        {
            return new T_Czy_areaDAL().GetAreaByFCodeAndFlag(fcode, fflag);
        }
    }
}