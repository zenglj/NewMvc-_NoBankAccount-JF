using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class BedInfomationBLL:BaseDapperBLL
    {

        /// <summary>
        /// 按级别ID（楼，房，床）查找床位记录,按两种FAreaCode 或 FID 的结合情况
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<BedInfomation> GetBedByMulSearch (int levelid, string fareacode, int fid, string loginname)
        {
            return new BedInfomationDAL().GetBedByMulSearch( levelid, fareacode, fid, loginname);
        }

        /// <summary>
        /// 按级别ID（楼，房，床）、犯人姓名、床位名称、监区、大楼号、状态等信息查找床位记录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<BedInfomation> GetBedListSearch( int levelid, string fcrimename, string bedname, string fareacode, string fhousename, int fflag, string loginname)
        {
            return new BedInfomationDAL().GetBedListSearch( levelid, fcrimename, bedname, fareacode, fhousename, fflag, loginname);
        }



        /// <summary>
        /// 按床位状态查找记录,查
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<BedInfomation> GetBedByFFlag( string fareacode, int fid, int fflag, string loginname)
        {
            return new BedInfomationDAL().GetBedByFFlag( fareacode, fid, fflag, loginname);
        }


        /// <summary>
        /// 按FID更新队别下所对应的所有床位队别编号
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="op"></param>
        public int UpdateFAreaCodeByFid( BedInfomation op)
        {
            return new BedInfomationDAL().UpdateFAreaCodeByFid(op);
        }
    }
}