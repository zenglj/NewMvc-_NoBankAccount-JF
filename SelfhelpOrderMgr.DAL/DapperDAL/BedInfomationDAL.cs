using Dapper;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public class BedInfomationDAL : BaseDapperDAL
    {

        /// <summary>
        /// 按级别ID（楼，房，床）查找床位记录,按两种FAreaCode 或 FID 的结合情况
        /// </summary>
        /// <param name="levelid"></param>
        /// <param name="fareacode"></param>
        /// <param name="fid"></param>
        /// <param name="loginname"></param>
        /// <returns></returns>
        public IEnumerable<BedInfomation> GetBedByMulSearch( int levelid, string fareacode, int fid, string loginname)
        {
            string sql = @"select ID,FName,LevelName,LevelID,FAreaCode,FAreaName,FID,FCrimeCode,FCrimeName,FFlag,FRemark from bedInfomation where LevelID=@LevelID and (isnull(FAreaCode,'')=@FAreaCode or isnull(FID,0)=@FID)
                                            and FAreaCode in (select a.fareacode from t_czy_area a,t_czy b where a.fflag=2 and b.fname =@FLoginName and a.fcode=b.fcode)";
            var param = new { LevelID = levelid, FAreaCode = fareacode, FID = fid, FLoginName = loginname };
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                IEnumerable<BedInfomation> _s = SqlMapper.Query<BedInfomation>(conn, sql, param).AsEnumerable<BedInfomation>();
                return _s;
            }

        }

        /// <summary>
        /// 按级别ID（楼，房，床）、犯人姓名、床位名称、监区、大楼号、状态等信息查找床位记录
        /// </summary>
        /// <param name="levelid"></param>
        /// <param name="fcrimename"></param>
        /// <param name="bedname"></param>
        /// <param name="fareacode"></param>
        /// <param name="fhousename"></param>
        /// <param name="fflag"></param>
        /// <param name="loginname"></param>
        /// <returns></returns>
        public IEnumerable<BedInfomation> GetBedListSearch(int levelid, string fcrimename, string bedname, string fareacode, string fhousename, int fflag, string loginname)
        {
            string strSql = "";
            var param = new DynamicParameters();
            strSql = @"select ID,FName,LevelName,LevelID,FAreaCode,FAreaName,FID,FCrimeCode,FCrimeName,FFlag,FRemark from bedInfomation where LevelID=" + levelid.ToString();
            strSql = strSql + " and FAreaCode in (select a.fareacode from t_czy_area a,t_czy b where a.fflag=2 and b.fname ='" + loginname + "' and a.fcode=b.fcode)";
            if (fcrimename != "")
            {
                strSql = strSql + " and FCrimeName like '%@FCrimeName%'";
                param.Add("FCrimeName", fcrimename, System.Data.DbType.String);
            }
            if (bedname != "")
            {
                strSql = strSql + " and FName like '%@BedName%'";
                param.Add("FCrimeName", bedname, System.Data.DbType.String);
            }
            if (fareacode != "")
            {
                strSql = strSql + " and FAreaCode =@FAreaCode'";
                param.Add("FAreaCode", fareacode, System.Data.DbType.String);
            }
            if (fflag != 2)
            {
                strSql = strSql + " and isnull(FFlag,0) =@FFlag";
                param.Add("FFlag", fflag, System.Data.DbType.Int32);
            }
            if (fhousename != "")
            {
                strSql = strSql + " and id in (select id from bedInfomation where  fid in(select id from bedInfomation where fid=@FId ))";
                param.Add("@FId", fhousename, System.Data.DbType.String);
            }

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                IEnumerable<BedInfomation> _s = SqlMapper.Query<BedInfomation>(conn, strSql, param).AsEnumerable<BedInfomation>();
                return _s;
            }
        }

        /// <summary>
        /// 按床位状态查找记录,查
        /// </summary>
        /// <param name="fareacode"></param>
        /// <param name="fid"></param>
        /// <param name="fflag"></param>
        /// <param name="loginname"></param>
        /// <returns></returns>
        public IEnumerable<BedInfomation> GetBedByFFlag( string fareacode, int fid, int fflag, string loginname)
        {
            string sql = @"select ID, FName, LevelName, LevelID, FAreaCode, FAreaName, FID, FCrimeCode, FFlag, FRemark from bedInfomation where (isnull(FAreaCode, '') = @FAreaCode or isnull(FID,0)= @FID) and FFlag = @FFlag and LevelID = 3
                            and FAreaCode in (select a.fareacode from t_czy_area a, t_czy b where a.fflag = 2 and b.fname = @FLoginName and a.fcode = b.fcode)";
            var param = new { FAreaCode =fareacode, FLoginName =loginname, FFlag =fflag};
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                IEnumerable<BedInfomation> _s = SqlMapper.Query<BedInfomation>(conn, sql, param).AsEnumerable<BedInfomation>();
                return _s;
            }
        }


        /// <summary>
        /// 按FID更新队别下所对应的所有床位队别编号
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public int UpdateFAreaCodeByFid( BedInfomation op)
        {
            string sql = @"update bedInfomation set FAreaCode=@FAreaCode where FID=@FID";
            var param = new { FID = op.FId, FAreaCode = op.FAreaCode};
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int _s = SqlMapper.Execute(conn, sql, param);
                return _s;
            }
        }


    }
}