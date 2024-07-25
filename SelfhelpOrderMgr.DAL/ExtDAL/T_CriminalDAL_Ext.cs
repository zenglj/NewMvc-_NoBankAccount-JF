using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_CriminalDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Criminal model, string param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Criminal(");
            strSql.Append("FCode,FName,FIdenNo,FSex,FAddr,FCrimeCode,FCYCode,FTerm,FInDate,FOuDate,FAreaCode,FDesc,FCZY,fflag,flimitflag,flimitamt,Frealareacode");
            strSql.Append(") values (");
            strSql.Append("@FCode,@FName,@FIdenNo,@FSex,@FAddr,@FCrimeCode,@FCYCode,@FTerm,@FInDate,@FOuDate,@FAreaCode,@FDesc,@FCZY,@fflag,@flimitflag,@flimitamt,@Frealareacode");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,8) ,
                        new SqlParameter("@FCYCode", SqlDbType.VarChar,3) ,
                        new SqlParameter("@FTerm", SqlDbType.VarChar,20) ,
                        new SqlParameter("@FInDate", SqlDbType.DateTime) ,
                        new SqlParameter("@FOuDate", SqlDbType.DateTime) ,
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,
                        new SqlParameter("@FDesc", SqlDbType.VarChar,100) ,
                        new SqlParameter("@FCZY", SqlDbType.VarChar,20) ,
                        new SqlParameter("@fflag", SqlDbType.Int,4) ,
                        new SqlParameter("@flimitflag", SqlDbType.Int,4) ,
                        new SqlParameter("@flimitamt", SqlDbType.Decimal,5) ,
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20)

            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FName;
            parameters[2].Value = model.FIdenNo;
            parameters[3].Value = model.FSex;
            parameters[4].Value = model.FAddr;
            parameters[5].Value = model.FCrimeCode;
            parameters[6].Value = model.FCYCode;
            parameters[7].Value = model.FTerm;
            parameters[8].Value = model.FInDate;
            parameters[9].Value = model.FOuDate;
            parameters[10].Value = model.FAreaCode;
            parameters[11].Value = model.FDesc;
            parameters[12].Value = model.FCZY;
            parameters[13].Value = model.fflag;
            parameters[14].Value = model.flimitflag;
            parameters[15].Value = model.flimitamt;
            parameters[16].Value = model.Frealareacode;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal model, string param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Criminal set ");

            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" FIdenNo = @FIdenNo , ");
            strSql.Append(" FSex = @FSex , ");
            strSql.Append(" FAddr = @FAddr , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" FCYCode = @FCYCode , ");
            strSql.Append(" FTerm = @FTerm , ");
            strSql.Append(" FInDate = @FInDate , ");
            strSql.Append(" FOuDate = @FOuDate , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FDesc = @FDesc , ");
            strSql.Append(" FCZY = @FCZY , ");
            strSql.Append(" fflag = @fflag , ");
            strSql.Append(" flimitflag = @flimitflag , ");
            strSql.Append(" flimitamt = @flimitamt , ");            
            strSql.Append(" Frealareacode = @Frealareacode , ");
            strSql.Append(" amount = @amount  ");
            strSql.Append(" where FCode=@FCode  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@FCode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@FIdenNo", SqlDbType.VarChar,20) ,
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,
                        new SqlParameter("@FAddr", SqlDbType.VarChar,128) ,
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,8) ,
                        new SqlParameter("@FCYCode", SqlDbType.VarChar,3) ,
                        new SqlParameter("@FTerm", SqlDbType.VarChar,20) ,
                        new SqlParameter("@FInDate", SqlDbType.DateTime) ,
                        new SqlParameter("@FOuDate", SqlDbType.DateTime) ,
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,
                        new SqlParameter("@FDesc", SqlDbType.VarChar,100) ,
                        new SqlParameter("@FCZY", SqlDbType.VarChar,20) ,
                        new SqlParameter("@fflag", SqlDbType.Int,4) ,
                        new SqlParameter("@flimitflag", SqlDbType.Int,4) ,
                        new SqlParameter("@flimitamt", SqlDbType.Decimal,5) ,
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20),
                        new SqlParameter("@amount", SqlDbType.Decimal,5)

            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FName;
            parameters[2].Value = model.FIdenNo;
            parameters[3].Value = model.FSex;
            parameters[4].Value = model.FAddr;
            parameters[5].Value = model.FCrimeCode;
            parameters[6].Value = model.FCYCode;
            parameters[7].Value = model.FTerm;
            parameters[8].Value = model.FInDate;
            parameters[9].Value = model.FOuDate;
            parameters[10].Value = model.FAreaCode;
            parameters[11].Value = model.FDesc;
            parameters[12].Value = model.FCZY;
            parameters[13].Value = model.fflag;
            parameters[14].Value = model.flimitflag;
            parameters[15].Value = model.flimitamt;
            parameters[16].Value = model.Frealareacode;
            parameters[17].Value = model.amount;
            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<T_Criminal> GetPageListOfIEnumerable(int page, int pageRow, string strWhere)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY fcode) AS RowNumber,a.*,b.cardcodea CardCode, b.cardFlaga FCardStatus  from t_criminal a,t_criminal_card b");
                if (strWhere != "")
                {
                    strSql.Append(" where a.Fcode=b.FcrimeCode and " + strWhere);
                }
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                return conn.Query<T_Criminal>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }

        public DataTable GetCrimeBySearch(string fcode, string fname, string areaname, string usercode)
        {
            string strSql = @"select * 
                            from t_criminal a,T_Area b,T_Cy_Type c 
                            where a.fcycode=c.fcode and a.fareacode=b.fcode";
            if (fcode != "")
            {
                strSql = strSql + " and a.fcode like '%" + fcode + "%'";
            }
            if (fname != "")
            {
                strSql = strSql + " and a.fname like '%" + fname + "%'";
            }
            if (areaname != "")
            {
                strSql = strSql + " and b.fname='" + areaname + "'";
            }
            strSql = strSql + " and a.fareacode in(select fareacode from dbo.t_czy_area where fflag=2 and fcode='" + usercode + "')";
            return SqlHelper.Query(strSql).Tables[0];

        }

        public T_Criminal_card GetInvoiceBalance(string fcode, string invoiceNo)
        {

            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {

                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append($"select top 1 * from T_Vcrd where flag=0 and fcrimecode=@fcrimecode and origid=@origid order by crtdate desc");

                object parmUserInfo;
                parmUserInfo = new { fcrimecode = fcode, origid = invoiceNo };
                T_Vcrd vcrd = (T_Vcrd)conn.Query<T_Vcrd>(strSql.ToString(), parmUserInfo).FirstOrDefault();

                string sql = @"select fcrimecode,sum(damount-camount) as BankAmount
                                ,sum(case acctype when 0 then DAMOUNT-CAMOUNT else 0 end ) AmountA
                                ,sum(case acctype when 1 then DAMOUNT-CAMOUNT else 0 end ) AmountB
                                ,sum(case acctype when 2 then DAMOUNT-CAMOUNT else 0 end ) AmountC
                                from T_Vcrd where flag=0 and fcrimecode=@fcrimecode and CRTDATE<= @crtdate
                              group by fcrimecode";
                object parmVcrdInfo;
                parmVcrdInfo = new { fcrimecode = fcode, crtdate = vcrd.CrtDate };
                return (T_Criminal_card)conn.Query<T_Criminal_card>(sql, parmVcrdInfo).FirstOrDefault();

            }
        }
        public List<T_Criminal> GetPageCriminalExtInfo(int page, int pageRow, string strWhere, string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ( ");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY fcode) AS RowNumber,* from ( ");
                strSql.Append(@"select a.FCode,a.FName,[FIdenNo],[FAge],[FSex],[FAddr],a.FCrimeCode,[FCYCode]
                        ,[FTerm],isnull(FInDate,'1900-01-01') as [FInDate],isnull(FOuDate,'1900-01-01') as [FOuDate],[FAreaCode],[FSubArea],a.FDesc,[FStatus]
                        ,[FStatus2],[FAddr_tmp],a.FCZY,a.fflag,a.flimitflag,a.flimitamt
                        ,[Frealareacode],a.amount,a.TP_YingYangCan_Money,a.RSB_Flag");
                strSql.Append(",b.FName FAreaName,c.FName CyName,d.FName CrimeName,isnull(f.CardCodea,'') CardCode,isnull(f.BankAccNo,'') BankCardNo ");
                strSql.Append(@" from t_criminal a left outer join t_area b on a.fareacode=b.fcode 
                    left outer join t_cy_type c on a.fcycode=c.fcode 
                    left outer join T_CRIME d on a.FCrimeCode=d.FCode 
                    left outer join T_Criminal_Card f on a.FCode=f.FCrimeCode ");
                strSql.Append(") d ");
                if (string.IsNullOrEmpty(strWhere) == false)
                {
                    strSql.Append("where " + strWhere);
                }
                strSql.Append(") e");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                if (string.IsNullOrEmpty(orderByField) == false)
                {
                    strSql.Append(" Order by " + orderByField);
                }
                object parmUserInfo;
                parmUserInfo = new { startNumber = startNumber, endNumber = endNumber };
                return (List<T_Criminal>)conn.Query<T_Criminal>(strSql.ToString(), parmUserInfo);
            }


        }

        public int GetCriminalsCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(count(*),0) fcount from  ");
            strSql.Append("(");
            strSql.Append(@"select a.*,b.FName FAreaName,c.FName CyName,d.FName CrimeName,isnull(f.CardCodea,'') CardCode,isnull(f.BankAccNo,'') BankCardNo
                from t_criminal a left outer join t_area b on a.fareacode=b.fcode 
                left outer join t_cy_type c on a.fcycode=c.fcode 
                left outer join T_CRIME d on a.FCrimeCode=d.FCode 
                left outer join T_Criminal_Card f on a.FCode=f.FCrimeCode");
            strSql.Append(") d ");
            if (string.IsNullOrEmpty(strWhere) == false)
            {
                strSql.Append(" where " + strWhere);
            }
            return (int)SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][0];

        }
        public List<T_UserInfoExt> GetUserInfo(int page, int pageRow, string strWhere)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ( ");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY fcode) AS RowNumber,* from ( ");
                strSql.Append("select a.fcode FCode,a.fname FName,b.fName FAreaName,a.FAreaCode FAreaCode,isnull(a.fflag,0) FFlag,c.CardCodea CardCode,c.AmountA AmountA,c.AmountB AmountB,c.AmountC AmountC,(c.AmountA+c.AmountB+c.AmountC) AllMoney,c.BankAccNo BankAccNo,c.AccPoints AccPoints ");
                strSql.Append("from t_criminal a left outer join t_area b on a.fareacode=b.fcode ");
                strSql.Append(" left outer join t_criminal_card c on a.fcode=c.fcrimecode  ");
                strSql.Append(") d ");
                if (string.IsNullOrEmpty(strWhere) == false)
                {
                    strSql.Append("where " + strWhere);
                }
                strSql.Append(") e");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                object parmUserInfo;
                parmUserInfo = new { startNumber = startNumber, endNumber = endNumber };
                return (List<T_UserInfoExt>)conn.Query<T_UserInfoExt>(strSql.ToString(), parmUserInfo);
            }
        }
        public List<T_UserInfoExt> GetUserInfo(string strWhere)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ( ");
                strSql.Append("select a.fcode FCode,a.fname FName,b.fName FAreaName,isnull(a.fflag,0) FFlag,c.CardCodea CardCode,c.AmountA AmountA,c.AmountB AmountB,c.AmountC AmountC,(c.AmountA+c.AmountB+c.AmountC) AllMoney,c.BankAccNo BankAccNo ,c.AccPoints as AccPoints ");
                strSql.Append("from t_criminal a,t_area b,t_criminal_card c ");
                strSql.Append("where a.fareacode=b.fcode and a.fcode=c.fcrimecode ");
                strSql.Append(") d ");
                if (string.IsNullOrEmpty(strWhere) == false)
                {
                    strSql.Append("where " + strWhere);
                }
                return (List<T_UserInfoExt>)conn.Query<T_UserInfoExt>(strSql.ToString());
            }
        }
        public int GetUserInfoCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(count(*),0) fcount from ( ");
            strSql.Append("select a.fcode FCode,a.fname FName,b.fName FAreaName,a.FareaCode,isnull(a.fflag,0) FFlag,c.CardCodea CardCode,c.AmountA AmountA,c.AmountB AmountB,c.AmountC AmountC,(c.AmountA+c.AmountB+c.AmountC) AllMoney,c.BankAccNo BankAccNo ,c.AccPoints as AccPoints ");
            strSql.Append("from t_criminal a left outer join t_area b on a.fareacode=b.fcode ");
            strSql.Append(" left outer join t_criminal_card c on a.fcode=c.fcrimecode  ");
            strSql.Append(") d ");
            if (string.IsNullOrEmpty(strWhere) == false)
            {
                strSql.Append("where " + strWhere);
            }
            return (int)SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][0];

        }

        public string PLExcelImport(string crtby, int modeId)
        {
            string rtnStr = "Err|更新失败,未执入数据处理";
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                StringBuilder strSql = new StringBuilder();
                int i;
                switch (modeId)
                {
                    case 0://标准模板：编号,姓名,性别,队别,处遇,身份证号,备注
                        {
                            strSql.Append(@"Update T_Criminal set FName=d.FName,FSex=d.FSex,FAreaCode=d.FAreaCode,FCYCode=d.FCYCode,FIdenNo=d.FIdenNo,FDesc=d.FDesc from (
                                select a.FCode,a.FName,a.FSex, b.FCode FAreaCode,c.FCode FCYCode,a.FIdenNo,a.Remark FDesc,0 FAge,0 FFlag,0 FlimitAmt,0 FlimitFlag from T_Criminal_MyTmp a,t_Area b,T_Cy_Type c
                                where a.FAreaName=b.FName and a.FCyName=c.FName) d
                                where T_Criminal.FCode=d.FCode;");
                            i = conn.Execute(strSql.ToString());
                            strSql = new StringBuilder();
                            rtnStr = "OK|更新记录 " + i.ToString() + "条";
                            strSql.Append(@"insert into T_Criminal (FCode,FName,FSex,FAreaCode,FCYCode,FIdenNo,FDesc,FAge,FFlag,FlimitAmt,FlimitFlag,FCZY )
                                select a.FCode,a.FName,a.FSex, b.FCode FAreaCode,c.FCode FCYCode,a.FIdenNo,a.Remark FDesc,0 FAge,0 FFlag,0 FlimitAmt,0 FlimitFlag,'" + crtby + @"' FCZY from T_Criminal_MyTmp a,t_Area b,T_Cy_Type c
                                where a.FAreaName=b.FName and a.FCyName=c.FName
                                and a.FCode not in(select FCode from t_Criminal);");
                            i = 0;
                            i = conn.Execute(strSql.ToString());
                            rtnStr = rtnStr + ",插入记录 " + i.ToString() + "条";

                        }
                        break;
                    case 1: //福州监狱模板：编号，姓名，性别，地址，罪行，处遇级别，刑期，入监时间，出监时间，队别，身份证号，备注
                        {
                            strSql.Append(@"Update T_Criminal set FName=d.FName,FSex=d.FSex,FAreaCode=d.FAreaCode,FCYCode=d.FCYCode,FIdenNo=d.FIdenNo,FDesc=d.FDesc,FInDate=d.FInDate,FOuDate=d.FOuDate,FTerm=d.FTerm,FAddr=d.FAddr,FCrimeCode=d.FCrimeCode from (
                                select a.FCode,a.FName,a.FSex, b.FCode FAreaCode,c.FCode FCYCode,a.FIdenNo,a.Remark FDesc,0 FAge,0 FFlag,0 FlimitAmt,0 FlimitFlag,FInDate,FOuDate,FTerm,FAddr,FCrimeCode from T_Criminal_MyTmp a,t_Area b,T_Cy_Type c
                                where a.FAreaName=b.FName and a.FCyName=c.FName) d
                                where T_Criminal.FCode=d.FCode;");
                            i = conn.Execute(strSql.ToString());
                            strSql = new StringBuilder();
                            rtnStr = "OK|更新记录 " + i.ToString() + "条";
                            strSql.Append(@"insert into T_Criminal (FCode,FName,FSex,FAreaCode,FCYCode,FIdenNo,FDesc,FAge,FFlag,FlimitAmt,FlimitFlag,FCZY,FInDate,FOuDate,FTerm,FAddr,FCrimeCode )
                                select a.FCode,a.FName,a.FSex, b.FCode FAreaCode,c.FCode FCYCode,a.FIdenNo,a.Remark FDesc,0 FAge,0 FFlag,0 FlimitAmt,0 FlimitFlag,'" + crtby + @"'  FCZY,FInDate,FOuDate,FTerm,FAddr,FCrimeCode from T_Criminal_MyTmp a,t_Area b,T_Cy_Type c
                                where a.FAreaName=b.FName and a.FCyName=c.FName
                                and a.FCode not in(select FCode from t_Criminal);");
                            i = 0;
                            i = conn.Execute(strSql.ToString());
                            rtnStr = rtnStr + ",插入记录 " + i.ToString() + "条";
                        }
                        break;

                    case 2://漳州监狱模板：编号,姓名,性别,队别,处遇,身份证号,罪名,案号,备注
                        {
                            strSql.Append(@"Update T_Criminal set FName=d.FName,FSex=d.FSex,FAreaCode=d.FAreaCode,FCYCode=d.FCYCode,FIdenNo=d.FIdenNo,FDesc=d.FDesc,FCrimeCode=d.FCrimeCode ,FrealAreaCode=d.FrealAreaCode
                                from (select a.FCode,a.FName,a.FSex, b.FCode FAreaCode,c.FCode FCYCode,a.FIdenNo,a.Remark FDesc,0 FAge,0 FFlag,0 FlimitAmt,0 FlimitFlag, isnull(e.fcode,'') as FCrimeCode ,a.fcrimename+'('+FAnHao+')' as FrealAreaCode
                                from T_Criminal_MyTmp a left join t_Area b on a.FAreaName=b.FName
                                left join T_Cy_Type c on a.FCyName=c.FName 
                                left outer join t_crime e on a.fcrimename=e.fname
                                ) d where T_Criminal.FCode=d.FCode;");
                            i = conn.Execute(strSql.ToString());
                            strSql = new StringBuilder();
                            rtnStr = "OK|更新记录 " + i.ToString() + "条";
                            strSql.Append(@"insert into T_Criminal (FCode,FName,FSex,FAreaCode,FCYCode,FIdenNo,FDesc,FAge,FFlag,FlimitAmt,FlimitFlag,FCZY,FCrimeCode,FrealAreaCode )
                                select a.FCode,a.FName,a.FSex, b.FCode FAreaCode,c.FCode FCYCode,a.FIdenNo,a.Remark FDesc,0 FAge,0 FFlag,0 FlimitAmt,0 FlimitFlag,'" + crtby + @"'  FCZY,isnull(e.fcode,'') as FCrimeCode,a.fcrimename+'('+FAnHao+')' as FrealAreaCode
                                from T_Criminal_MyTmp a left join t_Area b on a.FAreaName=b.FName left join T_Cy_Type c on a.FCyName=c.FName
                                left outer join t_crime e on a.fcrimename=e.fname
                                                                where a.FCode not in(select FCode from t_Criminal);");
                            i = 0;
                            i = conn.Execute(strSql.ToString());
                            rtnStr = rtnStr + ",插入记录 " + i.ToString() + "条";
                        }
                        break;

                    default:
                        break;
                }


                strSql = new StringBuilder();
                strSql.Append(@"drop table T_Criminal_MyTmp;");
                i = conn.Execute(strSql.ToString());
                return rtnStr;
            }
        }


        public string PLExcelChangeEduImport(string crtby)
        {
            string rtnStr = "Err|更新失败,未执入数据处理";
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                StringBuilder strSql = new StringBuilder();
                int i;
                strSql.Append(@"Update T_Criminal set TP_YingYangCan_Money=b.FMoney from T_Criminal a,T_Criminal_TmpMoney b where a.fcode=b.fcode and a.fname=b.fname and isnull(a.fflag,0)=0;");
                i = conn.Execute(strSql.ToString());
                rtnStr = "OK|更新记录 " + i.ToString() + "条";

                strSql = new StringBuilder();
                strSql.Append(@"delete  from T_Criminal_TPList where [EffectiveDate]=dateadd(month, datediff(month, 0, dateadd(month, 1, getdate())), -1) 
                    and fcode in(select fcode from T_Criminal_TmpMoney);");
                i = 0;
                i = conn.Execute(strSql.ToString());

                strSql = new StringBuilder();
                strSql.Append(@"insert into dbo.T_Criminal_TPList (
                    [FCode],[FName],[TPMoney],[CrtBy],[CrtDate],[EffectiveDate]
                    ,[FifoFlag],[Remark],[SrcFileName],[DelBy],[DelDate],[MoneyUseFlag])
                    select a.[FCode],a.[FName],fmoney as [TPMoney],'" + crtby + @"' as [CrtBy],getdate() as [CrtDate]
                ,dateadd(month, datediff(month, 0, dateadd(month, 1, getdate())), -1) as [EffectiveDate],case when fmoney>=0 then 1 else -1 end [FifoFlag],a.FRemark as [Remark],'' as [SrcFileName],'' [DelBy],'' [DelDate],1 as [MoneyUseFlag]
                 from T_Criminal_TmpMoney a,t_criminal b where a.fcode=b.fcode and a.fname=b.fname and isnull(b.fflag,0)=0;");
                i = 0;
                i = conn.Execute(strSql.ToString());
                //rtnStr = rtnStr + ",插入记录 " + i.ToString() + "条";


                strSql = new StringBuilder();
                strSql.Append(@"drop table T_Criminal_TmpMoney;");
                i = conn.Execute(strSql.ToString());
                return rtnStr;
            }
        }

        public string Rsb_KouKuan(string fcrimecode, string crtby, decimal rsb_money)
        {
            string rtnStr = "";
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                var parems = new DynamicParameters();//建立一个parem对象
                parems.Add("@fcrimecode", fcrimecode);
                parems.Add("@crtby", crtby);
                parems.Add("@rsb_money", rsb_money);
                parems.Add("@result", "", DbType.String, ParameterDirection.Output);//输出返回值
                //注意 parems.Add("@res",ParameterDirection.Output);//这样写返回值可能会出错，切记！！！
                SqlMapper.Execute(conn, "P_Rsb_Insert_KouKuan", parems, null, null, CommandType.StoredProcedure);
                rtnStr = parems.Get<string>("@result");//获取数据库输出的值
                return rtnStr;
            }

        }
    }
}