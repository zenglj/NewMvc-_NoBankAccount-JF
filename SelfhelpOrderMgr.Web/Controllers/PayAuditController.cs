using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [MyLogActionFilterAttribute]
    public class PayAuditController : BaseController
    {
        //
        // GET: /PayAudit/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index(int id=1)
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in( select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')");
            ViewData["areas"] = areas;

            List<T_CY_TYPE> cys = new T_CY_TYPEBLL().GetModelList("");
            ViewData["cys"] = cys;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct CrtBy from t_Vcrd Order by CrtBy");
            List<string> crtbys = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    crtbys.Add(row[0].ToString());
                }
            }
            ViewData["crtbys"] = crtbys;

            dt = new CommTableInfoBLL().GetDataTable("select distinct Dtype from t_Vcrd where damount<>0 Order by Dtype");
            List<string> cashtypes = new List<string>();//存款类型
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cashtypes.Add(row[0].ToString());
                }
            }
            ViewData["cashtypes"] = cashtypes;

 

            dt = new CommTableInfoBLL().GetDataTable("select distinct Dtype from t_Vcrd where camount<>0 Order by Dtype");
            List<string> paytypes = new List<string>();//存款类型
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    paytypes.Add(row[0].ToString());
                }
            }
            ViewData["paytypes"] = paytypes;


            ViewData["idOperationType"] = id;

            return View();
        }

        /// <summary>
        /// 发送数据到银行
        /// </summary>
        /// <returns></returns>
        public ActionResult SendBankDataAll()
        {
            string FCrimeCode = Request["FCrimeCode"];
            if (string.IsNullOrEmpty(FCrimeCode))
            {
                FCrimeCode = "";
            }
            string strSql = @"declare @RelateSeqNo varchar(20);
                            declare @rseqno int ;
                            exec CreateBatNo 'ALL', @RelateSeqNo output ;
                            select @rseqno=convert(int,@relateSeqno);
                            /* 代付数据生成*/
                            EXEC CreateBankData 0,'" + FCrimeCode + "',@relateSeqNo ;";
            try
            {
                new CommTableInfoBLL().ExecSql(strSql);
                return Content("OK|发送成功");
            }
            catch
            {
                return Content("Err|发送失败");
            }
            
        }
        public ActionResult GetSearchVcrds()
        {

            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

            //分页信息
            string action = Request["action"];
            string strPage = Request["page"];
            string strRow = Request["rows"];
            //操作类型：id=1 表示存款,id=2表示取款
            string idOperationType = Request["idOperationType"];
            int page = 1;
            int row = 10;
            decimal listRows = 0;
            string sss = "";
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }

            if (string.IsNullOrEmpty(strRow) == false)
            {
                row = Convert.ToInt32(strRow);
            }
            //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）
            //strWhere = strWhere + " and DAmount<>0 and TypeFlag in(0,1,3,4)";

            if (idOperationType == "1")
            {
                //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）            
                strWhere = strWhere + " and DAmount<>0 and Flag in(0,-2) and TypeFlag in(0,1,3,4)";
            }
            else if (idOperationType == "2")
            {
                //存款类型 及子类(TypeFlag=2)     
                strWhere = strWhere + " and CAmount<>0 and TypeFlag=2";
            }
            else
            {
                //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）            
                strWhere = strWhere + " and DAmount<>0 and Flag in(0,-2) and TypeFlag in(0,1,3,4)";
            }

            listRows = new T_VcrdBLL().GetListCount(strWhere.ToString())[0];

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetPageList(page, row, strWhere, "CrtDate,FCrimeCode");

            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(vcrds) + "}";
            return Content(sss);
        }

        public ActionResult GetVcrdsMoney()
        {

            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

            //分页信息
            string action = Request["action"];
            string strPage = Request["page"];
            string strRow = Request["rows"];
            //操作类型：id=1 表示存款,id=2表示取款
            string idOperationType = Request["idOperationType"];
            

            int page = 1;
            int row = 10;
            decimal sumMoney = 0;
            string sss = "";
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }

            if (string.IsNullOrEmpty(strRow) == false)
            {
                row = Convert.ToInt32(strRow);
            }
            
            //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）            
            //strWhere = strWhere + " and DAmount<>0 and TypeFlag in(0,1,3,4)";
            if (idOperationType == "1")
            {
                //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）            
                strWhere = strWhere + " and DAmount<>0 and Flag in(0,-2) and TypeFlag in(0,1,3,4)";
            }
            else if (idOperationType=="2")
            {
                //存款类型 及子类(TypeFlag=2)     
                strWhere = strWhere + " and CAmount<>0 and TypeFlag=2";
            }
            else
            {
                //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）            
                strWhere = strWhere + " and DAmount<>0 and Flag in(0,-2) and TypeFlag in(0,1,3,4)";
            }
             
            decimal[] schMoneies= new T_VcrdBLL().GetListCount(strWhere.ToString());
            sumMoney = schMoneies[2];
            if (idOperationType == "2")
            {
                sumMoney = schMoneies[1];
            }
                

            return Content("OK|" + sumMoney.ToString());
        }

        //将Request参数传入GetSearchWhere函数，并生成查询条件
        private string SetRequestSearchWhere(string LoginCode)
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            string cyName = Request["cyName"];
            string CrtBy = Request["CrtBy"];
            string CriminalFlag = Request["CriminalFlag"];//是否在押
            string CashTypes = Request["CashTypes"];//存款类型
            string PayTypes = Request["PayTypes"];//取款类型
            string AccTypes = Request["AccTypes"];//账户类型
            string BankFlags = Request["BankFlags"];//银行状态类型

            string strWhere = GetSearchWhere(LoginCode, ref startTime, ref endTime, areaName, FName, FCode, CrtBy, CriminalFlag, CashTypes, PayTypes, AccTypes, BankFlags);
            return strWhere;
        }

        //生成Vcrd查询条件
        private static string GetSearchWhere(string LoginCode, ref string startTime, ref string endTime, string areaName, string FName, string FCode, string CrtBy, string CriminalFlag, string CashTypes, string PayTypes, string AccTypes, string BankFlags)
        {
            string strWhere = "Flag in(0,-2) ";
            if (string.IsNullOrEmpty(startTime) == false)
            {
                DateTime sdt = Convert.ToDateTime(startTime);
                startTime = sdt.Year.ToString() + "-" + sdt.Month.ToString() + "-" + sdt.Day.ToString() + " 00:00:00";
            }
            if (string.IsNullOrEmpty(endTime) == false)
            {
                DateTime edt = Convert.ToDateTime(endTime);
                endTime = edt.Year.ToString() + "-" + edt.Month.ToString() + "-" + edt.Day.ToString() + " 23:59:00";
            }

            if (string.IsNullOrEmpty(startTime) == false)
            {
                strWhere = strWhere + " and  CrtDate>='" + startTime + "'";
            }
            if (string.IsNullOrEmpty(startTime) == false)
            {
                strWhere = strWhere + " and  CrtDate<'" + endTime + "'";
            }

            if (string.IsNullOrEmpty(areaName) == false)
            {
                if (areaName != "请选择队别")
                {
                    //strWhere = strWhere + " and FAreaName='" + areaName + "'";
                    strWhere = strWhere + " and fcrimecode in( select fcode from t_Criminal where FAreaCode='" + areaName + "')";
                }
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and FCriminal like '%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strWhere = strWhere + " and FCrimeCode='" + FCode + "'";
            }
            if (string.IsNullOrEmpty(CrtBy) == false)
            {
                strWhere = strWhere + " and CrtBy='" + CrtBy + "'";
            }
            string savePays = "";
            if (string.IsNullOrEmpty(CashTypes) == false)
            {
                savePays = CashTypes;
            }
            if (string.IsNullOrEmpty(PayTypes) == false)
            {
                if (savePays == "")
                {
                    savePays = PayTypes;
                }
                else
                {
                    savePays = savePays + "," + PayTypes;
                }

            }

            if (string.IsNullOrEmpty(savePays) == false)
            {
                strWhere = strWhere + " and Dtype in (" + savePays + ")";
            }

            if (string.IsNullOrEmpty(AccTypes) == false)
            {
                strWhere = strWhere + " and AccType in (" + AccTypes + ")";
            }

            if (string.IsNullOrEmpty(BankFlags) == false)
            {
                strWhere = strWhere + " and isnull(BankFlag,0) in (" + BankFlags + ")";
            }
            if (string.IsNullOrEmpty(CriminalFlag) == false)
            {
                strWhere = strWhere + " and FCrimeCode in ( Select FCode from T_Criminal where isnull(FFlag,0)=" + CriminalFlag + ")";
            }
            strWhere = strWhere + " and FCrimeCode in ( Select FCode from T_Criminal where FAreaCode in ( select fareaCode from t_czy_area where fflag=2 and fcode='" + LoginCode + "'))";
            return strWhere;
        }

        public ActionResult auditMenuBtn(int idOperationType=1)
        {
            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

            //获取ManagerSet表里设定的VcrdFlag个管理值
            int ivcrdFlag = GetVcrdFlagForSysManagerset();
            //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）
            if (idOperationType == 1)
            {
                strWhere = strWhere + " and DAmount<>0 and Flag in(0,-2) and TypeFlag in(0,1,3,4)";
            }
            else if (idOperationType == 2)
            {
                strWhere = strWhere + " and CAmount<>0 and TypeFlag =2";
            }
            else
            {
                return Content("Err|审核失败，未知审核类型");
            }
            

            string AuditMode = Request["AuditMode"];
            int i = 0;
            if (idOperationType == 1)
            {
                if (AuditMode == "all")
                {
                    string sql = "";
                    if (ivcrdFlag == -2)
                    {
                        sql = @"update t_criminal_card set AmountA=a.AmountA+b.A,AmountB=a.AmountB+b.B,AmountC=a.AmountC+b.C
                        from t_criminal_card a,(
                        select fcrimecode, sum(case acctype when 0 then DAMOUNT else 0 end) A
                        ,sum(case acctype when 1 then DAMOUNT else 0 end) B
                        ,sum(case acctype when 2 then DAMOUNT else 0 end) C
                        from  T_Vcrd where " + strWhere + @" and DAmount>0 and checkflag=0 and flag = -2  group by fcrimecode) b
                             where a.fcrimecode = b.fcrimecode; ";
                    }
                    

                     sql = sql+ "Update T_Vcrd set CheckFlag=1,flag=0,checkdate=getdate(),checkby='" + Session["loginUserName"].ToString() + "' where DAmount>0 and checkflag=0 and " + strWhere;


                    //i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=1,checkdate=getdate(),checkby='" + Session["loginUserName"].ToString() + "' where DAmount>0 and checkflag=0 and " + strWhere);
                    i = new CommTableInfoBLL().ExecSql(sql);

                }
                else
                {
                    string sql = "";
                    if (ivcrdFlag == -2)
                    {
                        sql = @"update t_criminal_card set AmountA=a.AmountA+b.A,AmountB=a.AmountB+b.B,AmountC=a.AmountC+b.C
                        from t_criminal_card a,(
                        select fcrimecode, sum(case acctype when 0 then DAMOUNT else 0 end) A
                        ,sum(case acctype when 1 then DAMOUNT else 0 end) B
                        ,sum(case acctype when 2 then DAMOUNT else 0 end) C
                        from  T_Vcrd where DAmount>0 and checkflag=0 and flag = -2 and seqno in(" + AuditMode + @") group by fcrimecode) b
                             where a.fcrimecode = b.fcrimecode; ";
                    }
                    sql = sql + "Update T_Vcrd set CheckFlag = 1,flag=0, checkdate = getdate(), checkby = '" + Session["loginUserName"].ToString() + "' where DAmount> 0 and checkflag = 0 and seqno in(" + AuditMode + ")";

                    //i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=1,checkdate=getdate(),checkby='" + Session["loginUserName"].ToString() + "' where DAmount>0 and checkflag=0 and seqno in(" + AuditMode + ")");
                    i = new CommTableInfoBLL().ExecSql(sql);

                }
                if (i > 0)
                {
                    if (ivcrdFlag == -2)
                    {
                        return Content("OK|成功审核，请刷新！");
                    }
                    else
                    {
                        return Content("OK|成功审核" + i.ToString() + "条记录，请刷新！");
                    }
                        
                }
                else
                {
                    return Content("Err|审核失败，0条记录成功");
                }
            }
            else if (idOperationType == 2)
            {
                if (AuditMode == "all")
                {
                    i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=0,checkdate=getdate(),checkby='" + Session["loginUserName"].ToString() + "' where CAmount>0 and checkflag=-1 and " + strWhere);
                }
                else
                {
                    i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=0,checkdate=getdate(),checkby='" + Session["loginUserName"].ToString() + "'  where CAmount>0 and checkflag=-1 and seqno in(" + AuditMode + ")");

                }
                if (i > 0)
                {
                    return Content("OK|成功审核" + i.ToString() + "条记录，请刷新！");
                }
                else
                {
                    return Content("Err|审核失败，0条记录成功");
                }
            }
            
            return Content("Err|审核失败，未知审核类型");
            
            
        }

        public ActionResult unAuditMenuBtn(int idOperationType=1)
        {
            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

            //获取ManagerSet表里设定的VcrdFlag个管理值
            int ivcrdFlag = GetVcrdFlagForSysManagerset();

            //14 亲属存款,3 零花钱发放,4 奖金发放,1 会见存款，0 存款（各类）
            //strWhere = strWhere + " and DAmount<>0 and TypeFlag in(0,1,3,4)";
            if (idOperationType == 1)
            {
                strWhere = strWhere + " and DAmount<>0 and Flag in(0,-2) and TypeFlag in(0,1,3,4)";
            }
            else if (idOperationType == 2)
            {
                strWhere = strWhere + " and CAmount<>0 and TypeFlag =2";
            }
            else
            {
                return Content("Err|撤销审核失败，未知审核类型");
            }

            string AuditMode = Request["AuditMode"];
            int i = 0;
            if (idOperationType == 1)
            {
                if (AuditMode == "all")
                {
                    string sql = "";
                    if (ivcrdFlag == -2)
                    {
                        sql = @"update t_criminal_card set AmountA=a.AmountA-b.A,AmountB=a.AmountB-b.B,AmountC=a.AmountC-b.C
                            from t_criminal_card a,(
                            select fcrimecode, sum(case acctype when 0 then DAMOUNT else 0 end) A
                            ,sum(case acctype when 1 then DAMOUNT else 0 end) B
                            ,sum(case acctype when 2 then DAMOUNT else 0 end) C
                            from  T_Vcrd where " + strWhere + @" and DAmount <> 0 and checkflag = 1 and flag = 0 group by fcrimecode) b
                                 where a.fcrimecode = b.fcrimecode;";
                    }

                    sql = sql + "Update T_Vcrd set CheckFlag=0,Flag=" + ivcrdFlag.ToString() + " where " + strWhere + " and DAmount>0 and Checkflag=1 and isnull(BankFlag,0)=0";

                    //i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=0 where " + strWhere + " and DAmount>0 and Checkflag=1 and isnull(BankFlag,0)=0");
                    i = new CommTableInfoBLL().ExecSql(sql);

                }
                else
                {
                    string sql = "";
                    if (ivcrdFlag == -2)
                    {
                        sql = @"update t_criminal_card set AmountA=a.AmountA-b.A,AmountB=a.AmountB-b.B,AmountC=a.AmountC-b.C
                            from t_criminal_card a,(
                            select fcrimecode, sum(case acctype when 0 then DAMOUNT else 0 end) A
                            ,sum(case acctype when 1 then DAMOUNT else 0 end) B
                            ,sum(case acctype when 2 then DAMOUNT else 0 end) C
                            from  T_Vcrd where seqno in(" + AuditMode + @") and DAmount <> 0 and checkflag = 1 and flag = 0 group by fcrimecode) b
                                 where a.fcrimecode = b.fcrimecode;";
                    }
                    sql = sql + "Update T_Vcrd set CheckFlag=0,Flag=" + ivcrdFlag.ToString() + " where seqno in(" + AuditMode + ")  and DAmount>0 and Checkflag=1 and isnull(BankFlag,0)=0";

                    //i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=0 where seqno in(" + AuditMode + ")  and DAmount>0 and Checkflag=1 and isnull(BankFlag,0)=0");
                    i = new CommTableInfoBLL().ExecSql(sql);

                }
                if (i > 0)
                {
                    if (ivcrdFlag == -2)
                    {
                        return Content("OK|成功撤消，请刷新！");
                    }
                    else
                    {
                        return Content("OK|成功撤消" + i.ToString() + "条记录，请刷新！");
                    }

                }
                else
                {
                    return Content("Err|撤消失败，0条记录成功");
                }
            }
            else if (idOperationType == 2)
            {
                if (AuditMode == "all")
                {
                    i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=-1 where " + strWhere + "  and CAmount>0 and Checkflag=0 and isnull(BankFlag,0)=0");
                }
                else
                {
                    i = new CommTableInfoBLL().ExecSql("Update T_Vcrd set CheckFlag=-1 where seqno in(" + AuditMode + ")  and CAmount>0 and Checkflag=0 and isnull(BankFlag,0)=0");

                }
                if (i > 0)
                {
                    return Content("OK|成功撤消" + i.ToString() + "条记录，请刷新！");
                }
                else
                {
                    return Content("Err|撤消失败，0条记录成功");
                }
            }

            return Content("Err|撤销审核失败，未知审核类型");

        }

        /// <summary>
        /// 获取ManagerSet表里设定的VcrdFlag个管理值
        /// </summary>
        /// <returns></returns>
        private static int GetVcrdFlagForSysManagerset()
        {
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("DepositInVcrdFlag");
            int ivcrdFlag = 0;
            if (mset != null)
            {
                if (mset.MgrValue == "-2")
                {
                    ivcrdFlag = -2;
                }
            }

            return ivcrdFlag;
        }
    }
}