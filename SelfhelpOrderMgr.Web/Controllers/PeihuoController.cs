using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class PeihuoController : BaseController
    {
        //
        // GET: /Peihuo/
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public ActionResult Index(int id=7)
        {
            //如果登录权限为：0，则只能查看
            string loginPower = Request["loginPower"];
            if(string.IsNullOrEmpty(loginPower))
            {
                loginPower = "";
            }
            //监区队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct Crtby from t_Vcrd where TypeFlag="+ id.ToString() +" order by CrtBy");
            List<string> crtbys =new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string crtby = row["Crtby"].ToString();
                crtbys.Add(crtby);
            }
            ViewData["loginPower"] = loginPower;
            ViewData["crtbys"] = crtbys;

            ViewData["XFType"] = id;
            return View();
        }

        public ActionResult getInvOuts(int id=7)
        {
            string action = Request["action"];
            //string typeFlag = Request["typeFlag"];
            //if(string.IsNullOrEmpty(typeFlag)==true)
            //{
            //    typeFlag = "7";
            //}
            string typeFlag = id.ToString();
            List<T_Invoice_out> bonuies;
            if (action == "LoginIn")
            {
                //bonuies = new T_Invoice_outBLL().GetModelList("isnull(flag,0)<2 and TypeFlag=" + typeFlag +" ");
                bonuies = new List<T_Invoice_out>(); 
            }
            else if (action == "GetSearchMainOrder")
            {
                string strAreaName = Request["fAreaName"];//队别名称
                if (strAreaName == "请选择队别")
                {
                    strAreaName = "";
                }
                string strStartDate = Request["startDate"];//开始日期
                string strEndDate = Request["endDate"];//结束日期
                string strRemark = Request["fRemark"];//结束日期
                string strFCrimeCode = Request["fCrimeCode"];//编号
                string strFCrimeName = Request["fCrimeName"];//姓名   
                string searchFlag = Request["searchFlag"];//主单状态
                
                //获取查询条件的SQL
                StringBuilder strSql = GetSearchSql(strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName, typeFlag,searchFlag);
                List<T_Invoice_out> list;
                list = (List<T_Invoice_out>)new T_Invoice_outBLL().GetModelList(strSql.ToString());
                if (list == null)
                {
                    list = new List<T_Invoice_out>();
                    T_Invoice_out m1 = new T_Invoice_out();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                return Content(jss.Serialize(list));
            }
            else
            {
                bonuies = new List<T_Invoice_out>();
            }

            return Content(jss.Serialize(bonuies));
        }

        public ActionResult getInvDetails(int id=1)
        {
            string action = Request["action"];
            if (action == "GetSearchMainOrder")
            {
                string strAreaName = Request["fAreaName"];//队别名称
                if (strAreaName == "请选择队别")
                {
                    strAreaName = "";
                }
                string strStartDate = Request["startDate"];//开始日期
                string strEndDate = Request["endDate"];//结束日期
                string strRemark = Request["fRemark"];//结束日期
                string strFCrimeCode = Request["fCrimeCode"];//编号
                string strFCrimeName = Request["fCrimeName"];//姓名                
                string wselCrtBy = Request["wselCrtBy"];//消费机号
                string rtnBankFlag = Request["rtnBankFlag"];//回款状态
                
                //string typeFlag = Request["typeFlag"];
                //if (string.IsNullOrEmpty(typeFlag) == true)
                //{
                //    typeFlag = "7";
                //}
                string typeFlag = id.ToString();
                
                //获取查询条件的SQL
                StringBuilder strSql = GetInvoiceSearchSql(wselCrtBy, strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName, typeFlag);
                strSql.Append(" and Flag=1 and isnull(CheckFlag,0)=0 ");
                switch (rtnBankFlag)
                {
                    case "1":
                        {
                            strSql.Append(" and invoiceNo in (select distinct Origid from t_vcrd where flag=0 and isnull(Origid,0)<>'' and isnull(bankFlag,0)<2 and typeFlag='" + typeFlag + "') ");
                        }
                        break;
                    case "2":
                        {
                            strSql.Append(" and invoiceNo in (select distinct Origid from t_vcrd where flag=0 and isnull(Origid,0)<>'' and isnull(bankFlag,0)>=2 and typeFlag='" + typeFlag + "') ");
                        }
                        break;
                    default:
                        break;
                }
                    
                List<T_Invoice> list;
                list = (List<T_Invoice>)new T_InvoiceBLL().GetModelList(strSql.ToString(), 1, Convert.ToDateTime(strStartDate), Convert.ToDateTime(strEndDate));
                if (list == null)
                {
                    list = new List<T_Invoice>();
                    T_Invoice m1 = new T_Invoice();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                return Content(jss.Serialize(list));
            }     
       
            return Content(null);
        }
        public ActionResult AddMainOrder(int id=7)//增加主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strFAreaName = Request["sAreaName"];//队别名称
            //string strFAreaCode = context.Request["sAreaCode"];//队别编号
            //string typeFlag = Request["typeFlag"];
            //if (string.IsNullOrEmpty(typeFlag) == true)
            //{
            //    typeFlag = "7";
            //}
            string typeFlag = id.ToString();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @vouno varchar(30);");
            strSql.Append("exec  CREATESEQNO  'OUT',1,@vouno output;");
            strSql.Append("select @vouno='OUT'+@vouno;");
            strSql.Append("select @vouno;");

            DataTable dt=new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string fsn=dt.Rows[0][0].ToString();

            T_Invoice_out outInfo = new T_Invoice_out();
            //outInfo.seqno = Request["sapplyby"];//申请人
            outInfo.fsn = fsn;//配货单号
            outInfo.Amount = 0;//金额
            outInfo.CrtBy = strLoginName;//年
            outInfo.crtdt = DateTime.Now;//月 
            outInfo.RcvBy = "";//备注
            outInfo.Flag = 0;
            outInfo.typeflag = Convert.ToInt32( typeFlag);
            outInfo.remark = Request["sremark"];//队别名称;
            int seqno = new T_Invoice_outBLL().Add(outInfo);

            if (seqno > 0)
            {
                T_Invoice_out modelOut = new T_Invoice_outBLL().GetModel(seqno);
                return Content("OK|" + jss.Serialize(modelOut));
            }
            else
            {
                return Content("Error|创建主单失败");
            }
        }

        public ActionResult AddOrderDetail()
        {
            string outId = Request["outId"];
            string invoiceNos = Request["invoiceNos"];
            char ee = (char)124;
            string[] invNos = invoiceNos.Split(ee);
            string strInvs="";
            foreach(string invno in invNos)
            {
                if(strInvs=="")
                {
                    strInvs = "'" + invno + "'";
                }
                else
                {
                    strInvs = strInvs+",'" + invno + "'";
                }
            }


            //用户是否设定了需要成功收款了，才能配货
            int peihuoFlag = 0;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("PeihuoVcrdBankFlag");
            if (mset == null)
            {
                peihuoFlag = 0;
            }
            else
            {
                peihuoFlag =Convert.ToInt32( mset.MgrValue.ToString());
            }
            int succCount=new T_Invoice_outBLL().LoadInvs(outId, strInvs, peihuoFlag);
            if (succCount>0)
            {
                T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='"+ outId +"'")[0];
                int errCount = invNos.Length - succCount;
                if (errCount > 0)
                {
                    return Content("OK|载入完成,失败" + errCount.ToString() + "条|" + invOut.Amount.ToString());

                }
                else
                {
                    return Content("OK|载入成功|" + invOut.Amount.ToString());
                }
            }
            else
            {
                return Content("Err|载入失败");
            }
        }

        //按主单号fsn获取OutDtl明细记录
        public ActionResult GetOutDetailsById()
        {
            string strFsn = Request["FBid"];
            List<T_Invoice_outdtl> dtls;
            if(string.IsNullOrEmpty(strFsn)==false)
            {
                dtls = new T_Invoice_outdtlBLL().GetModelList(" fsn='"+ strFsn +"'");
            }
            else 
            {
                dtls = new List<T_Invoice_outdtl>();
            }
            return Content(jss.Serialize(dtls));
        }

        //确认配货或是收货
        public ActionResult PostMainOrder()//提交主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strseqno = Request["sbid"];
            string strSetFlag = Request["setFlag"];
            string strRcvby = Request["srcvby"];
            T_Invoice_out inv = new T_Invoice_outBLL().GetModel(Convert.ToInt32(strseqno));
            inv.Flag = Convert.ToInt32(strSetFlag);
            if (strSetFlag=="2")
            {
                if (string.IsNullOrEmpty(strRcvby)==true)
                {
                    inv.RcvBy = strLoginName;
                }
                else
                {
                    inv.RcvBy = strRcvby;
                }
            }
            if(new T_Invoice_outBLL().Update(inv))
            {
                /*查询管理表中的Vcrd checkFlag 配置值，如果结算时是-1则要更新，
                                                       如果是0则不用更新*/
                T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCheckFlag");
                if(mgrSet.MgrValue=="-1")
                {
                    if (new T_VcrdBLL().UpdateCheckFlag(inv.fsn))
                    {
                        return Content("OK|成功，完成更新配货状态及Vcrd状态");
                    }
                    else
                    {
                        return Content("Err|更新Vcrd状态失败");
                    } 
                }
                else
                {
                    return Content("OK|成功，完成更新配货状态");
                }
                             
            }
            else
            {
                return Content("Err|更新失败");
            }

        }

        public ActionResult DelMainOrder()//删除主单
        {
            string status = "Err|删除失败";
            string strseqno = Request["sbid"];
            if(string.IsNullOrEmpty(strseqno)==false)
            {
                if(new T_Invoice_outBLL().DeleteMainInfo(strseqno))
                {
                    status="OK|删除成功";
                }
            }
            return Content(status);
        }

        public ActionResult DelOrderDetail()//删除明细记录
        {
            string status = "Err|失败";
            string strSeqno = Request["seqno"];
            if(string.IsNullOrEmpty(strSeqno)==false)
            {
                T_Invoice_outdtl dtl=new T_Invoice_outdtlBLL().GetModel(Convert.ToInt32(strSeqno));
                T_Invoice_out invOut2 = new T_Invoice_outBLL().GetModelList("fsn='"+ dtl.fsn +"'")[0];
                if (invOut2.Flag > 0)
                {
                    return Content("Err|配货主单已经确认不能删除");    
                }
                if(new T_Invoice_outBLL().DeleteDetailInfo(strSeqno))
                {
                    T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='"+ dtl.fsn +"'")[0];
                    status = "OK|成功|" + invOut.Amount.ToString();
                }
            }
            return Content(status);            
        }

        public ActionResult DelDetailAllList()//删除全部明细记录
        {
            string status = "Err|失败";
            string strSeqno = Request["seqno"];
            string strSbid = Request["sbid"];
            
            if (string.IsNullOrEmpty(strSeqno) == false)
            {
                T_Invoice_outdtl dtl = new T_Invoice_outdtlBLL().GetModel(Convert.ToInt32(strSeqno));
                T_Invoice_out invOut2 = new T_Invoice_outBLL().GetModelList("fsn='" + dtl.fsn + "'")[0];
                if (invOut2.Flag > 0)
                {
                    return Content("Err|配货主单已经确认不能删除");
                }
                if (new T_Invoice_outBLL().DeleteAllDetailInfo(strSbid))
                {
                    T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + dtl.fsn + "'")[0];
                    status = "OK|成功|" + invOut.Amount.ToString();
                }
            }
            return Content(status);
        }

        /// <summary>
        /// 2020年03-12改为合并导出方式
        /// Excel导出配货记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExcelSumOrder(int id=0)//按分监区生成Excel订货单
        {
            string strFsn = Request["FBidExcel"];
            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
            if (string.IsNullOrEmpty(strFsn) == false)
            {
                StringBuilder strSql = new StringBuilder();
                string strTitle = "";//Excel文件标题
                int sumColumn = 1;//汇总列号
                int groupbyColumn = -1;//分组列号
                switch (id)
                {
                    case 0://按队别导出
                        {
                            strSql.Append(@"select b.FAreaName 队别,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,sum(a.qty) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                            where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                            and c.fsn='" + strFsn + @"' group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
                                order by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");
                            sumColumn = 7;
                            groupbyColumn = -1;
                            strTitle = "按队别商品订货信息";
                        } break;
                    case 1://按分号房生成Excel订货单
                        {
                            strSql.Append(@"select b.FAreaName 队别,isnull(b.roomNo,'') 房号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,sum(a.qty) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno  and a.flag=1
                and c.fsn='" + strFsn + @"' group by b.FAreaName,isnull(b.roomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
                            order by b.FAreaName,isnull(b.roomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");
                            sumColumn = 8;
                            groupbyColumn =1;
                            strTitle = "按房号商品订货信息";
                        }break;
                    case 2: //按消费机用户号生成Excel订货单
                        {
                            strSql.Append(@"select b.crtby 机号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,sum(a.qty) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                and c.fsn='" + strFsn + "' group by b.crtby,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");
                            strTitle = "按消费机商品订货汇总信息";
                            sumColumn = 7;
                            groupbyColumn = 0;
                        } break;
                    case 3://按个人汇总生成Excel订货单
                        {
                            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
                            string strPointNum = "2";
                            if (mset != null)
                            {
                                strPointNum = mset.MgrValue;
                            }
                            strSql.Append(@"select b.fareaname 队别,a.fcrimecode 编号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,convert(numeric(18," + strPointNum + @"),sum(a.qty)) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                and c.fsn='" + strFsn + @"' group by b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
				order by b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                            sumColumn = 8;
                            groupbyColumn = 1;
                            strTitle = "按个人汇总订货信息";
                        } break;
                    case 4://按订单号汇总生成Excel订货单
                        {
                            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
                            string strPointNum = "2";
                            if (mset != null)
                            {
                                strPointNum = mset.MgrValue;
                            }
                            strSql.Append(@"select b.fareaname 队别,a.invoiceno 单号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.GDJ 单价,a.gtxm 条码,convert(numeric(18," + strPointNum + @"),sum(a.qty)) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                and c.fsn='" + strFsn + @"' group by b.fareaname,a.invoiceno,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
				order by b.fareaname,a.invoiceno,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                            sumColumn = 8;
                            groupbyColumn = 1;
                            strTitle = "商品订单号订货信息";
                        } break;
                    case 5://按订单号汇总领货表格（每单一条）
                        {
                            strSql.Append(@"select a.fareaname 队别,a.FCrimeCode 编号,a.FCriminal 姓名,a.invoiceno 流水号,sum(a.AMount) 金额,'' 领货日期,'' 签字
                    from t_invoice a,t_invoice_outDtl b
                    where a.invoiceno=b.invoiceno and a.flag=1
                    and b.fsn='" + strFsn + @"' group by a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno
				    order by a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno");
                            sumColumn = 4;
                            groupbyColumn = -1;
                            strTitle = "商品订单号汇总领货表";
                        } break;
                    case 6:
                        {
                            strSql.Append(@"select 队别
                                , 货号,品名 , 规格, 单位, 单价 
                                ,case sum( [1号房] ) when 0 then '' else convert(varchar(10), sum( [1号房] )) end as [1号房]
                                ,case sum( [2号房] ) when 0 then '' else convert(varchar(10), sum( [2号房] )) end as [2号房]
                                ,case sum( [3号房] ) when 0 then '' else convert(varchar(10), sum( [3号房] )) end as [3号房]
                                ,case sum( [4号房] ) when 0 then '' else convert(varchar(10), sum( [4号房] )) end as [4号房]
                                ,case sum( [5号房] ) when 0 then '' else convert(varchar(10), sum( [5号房] )) end as [5号房]
                                ,case sum( [6号房] ) when 0 then '' else convert(varchar(10), sum( [6号房] )) end as [6号房]
                                ,case sum( [7号房] ) when 0 then '' else convert(varchar(10), sum( [7号房] )) end as [7号房]
                                ,case sum( [8号房] ) when 0 then '' else convert(varchar(10), sum( [8号房] )) end as [8号房]
                                ,case sum( [9号房] ) when 0 then '' else convert(varchar(10), sum( [9号房] )) end as [9号房]
                                ,case sum( [10号房] ) when 0 then '' else convert(varchar(10), sum( [10号房] )) end as [10号房]
                                ,case sum( [11号房] ) when 0 then '' else convert(varchar(10), sum( [11号房] )) end as [11号房]
                                ,case sum( [12号房] ) when 0 then '' else convert(varchar(10), sum( [12号房] )) end as [12号房]
                                ,case sum( [13号房] ) when 0 then '' else convert(varchar(10), sum( [13号房] )) end as [13号房]
                                ,case sum( [14号房] ) when 0 then '' else convert(varchar(10), sum( [14号房] )) end as [14号房]
                                ,case sum( [15号房] ) when 0 then '' else convert(varchar(10), sum( [15号房] )) end as [15号房]
                                ,case sum( [16号房] ) when 0 then '' else convert(varchar(10), sum( [16号房] )) end as [16号房]
                                ,case sum( [未知号房] ) when 0 then '' else convert(varchar(10), sum( [未知号房] )) end as [未知号房]
                                ,sum([1号房]+[2号房]+[3号房]+[4号房]+[5号房]+[6号房]+[7号房]+[8号房]+[9号房]+[10号房]+[11号房]+[12号房]+[13号房]+[14号房]+[15号房]+[16号房]+[未知号房]) as 数量
                                ,convert(numeric(18,2),sum([1号房]+[2号房]+[3号房]+[4号房]+[5号房]+[6号房]+[7号房]+[8号房]+[9号房]+[10号房]+[11号房]+[12号房]+[13号房]+[14号房]+[15号房]+[16号房]+[未知号房])*[单价]) as 金额
                                from (select b.FAreaName 队别,a.SPShortCode 货号,a.gname 品名 ,isnull(a.Remark,'') 规格,a.SPShortCode 简码,a.gtxm 条码,d.GUnit 单位,a.gdj 单价 
	                            ,case isnull(roomno,'') when 1 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '1号房' 
                                ,case isnull(roomno,'') when 2 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '2号房' 
                                ,case isnull(roomno,'') when 3 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '3号房' 
                                ,case isnull(roomno,'') when 4 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '4号房' 
                                ,case isnull(roomno,'') when 5 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '5号房' 
                                ,case isnull(roomno,'') when 6 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '6号房' 
                                ,case isnull(roomno,'') when 7 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '7号房' 
                                ,case isnull(roomno,'') when 8 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '8号房' 
                                ,case isnull(roomno,'') when 9 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '9号房' 
                                ,case isnull(roomno,'') when 10 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '10号房' 
                                ,case isnull(roomno,'') when 11 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '11号房' 
                                ,case isnull(roomno,'') when 12 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '12号房' 
                                ,case isnull(roomno,'') when 13 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '13号房' 
                                ,case isnull(roomno,'') when 14 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '14号房' 
                                ,case isnull(roomno,'') when 15 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '15号房' 
                                ,case isnull(roomno,'') when 16 then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '16号房' 
                                ,case isnull(roomno,'') when '' then convert(numeric(18,0),abs(sum(a.qty * b.fifoflag)))  else 0 end as '未知号房' 
                            ,abs(sum(a.amount * b.fifoflag)) 金额  
                            from t_invoicedtl a 
                            inner join (select x.* from t_invoice x left join t_invoice_outDtl y on x.invoiceno=y.invoiceno where  Flag=1 and y.fsn='"+ strFsn + @"') b   on a.invoiceno=b.invoiceno  
                            left join t_goods d  on a.gtxm=d.gtxm  
                            left outer join t_goodstype e  on d.gtype=e.fcode  
                            where b.flag=1 
                            group by b.FAreaName,isnull(roomno,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm,d.GUnit,a.gdj ) as t 
                            group by 队别, 货号,品名 , 规格, 单位, 单价 
                            order by 队别,  货号
                            ");
                            sumColumn = 24;
                            groupbyColumn = 0;
                        } break;
                    default: 
                        {
                            return Content("Err|未知报表类型，导出Excel失败");
                        }                        
                }

                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_ExcelPeihuoInfo.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                if (groupbyColumn >= 0)
                {
                    ExcelRender.RenderToExcel(dt, invOut.remark + strTitle, sumColumn, strFileName, groupbyColumn);
                }
                else
                {
                    ExcelRender.RenderToExcel(dt, invOut.remark + strTitle, sumColumn, strFileName);
                }
                return Content("OK|" + strFsn + "_ExcelPeihuoInfo.xls");

            }
            return Content("Err|导出Excel失败");

        }

        //20200312停用单个方法模式
//        public ActionResult ExcelOutArea()//按分监区生成Excel订货单
//        {
//            string strFsn = Request["FBidExcel"];
//            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
//            if(string.IsNullOrEmpty(strFsn)==false)
//            {
//                StringBuilder strSql = new StringBuilder();
//                strSql.Append(@"select b.FAreaName 队别,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,sum(a.qty) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
//                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
//                and c.fsn='" + strFsn + "' group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");

//                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
//                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_AreaGoodInfo.xls");
//                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
//                //ExcelRender.RenderToExcel(dt, context, strFileName);
//                ExcelRender.RenderToExcel(dt,invOut.remark+"商品订货信息",7, strFileName);
//                return Content("OK|" + strFsn + "_AreaGoodInfo.xls");
//            }
//            return Content("Err|导出Excel失败");
            
//        }

//        public ActionResult ExcelOutRoom()//按分号房生成Excel订货单
//        {
//            string strFsn = Request["FBidExcel"];
//            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
//            if (string.IsNullOrEmpty(strFsn) == false)
//            {                
//                StringBuilder strSql = new StringBuilder();
//                strSql.Append(@"select b.FAreaName 队别,isnull(b.roomNo,'') 房号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,sum(a.qty) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
//                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno  and a.flag=1
//                and c.fsn='" + strFsn + "' group by b.FAreaName,isnull(b.roomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");

//                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
//                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_RoomGoodInfo.xls");
//                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
//                //ExcelRender.RenderToExcel(dt, context, strFileName);
//                ExcelRender.RenderToExcel(dt, invOut.remark + "商品订货信息", 8, strFileName, 1);
//                return Content("OK|" + strFsn + "_RoomGoodInfo.xls");
//            }
//            return Content("Err|导出Excel失败");

//        }

//        public ActionResult ExcelMacNoRoom()//按消费机用户号生成Excel订货单
//        {
//            string strFsn = Request["FBidExcel"];
//            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
//            if (string.IsNullOrEmpty(strFsn) == false)
//            {
//                StringBuilder strSql = new StringBuilder();
//                strSql.Append(@"select b.crtby 机号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,sum(a.qty) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
//                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
//                and c.fsn='" + strFsn + "' group by b.crtby,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");

//                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
//                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_MacNoGoodInfo.xls");
//                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
//                //ExcelRender.RenderToExcel(dt, context, strFileName);
//                ExcelRender.RenderToExcel(dt, invOut.remark + "商品订货信息", 7, strFileName);
//                return Content("OK|" + strFsn + "_MacNoGoodInfo.xls");
//            }
//            return Content("Err|导出Excel失败");

//        }

//        public ActionResult ExcelOutPerson()//按个人汇总生成Excel订货单
//        {

//            string strFsn = Request["FBidExcel"];
//            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
//            if (string.IsNullOrEmpty(strFsn) == false)
//            {
//                T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
//                string strPointNum = "2";
//                if (mset != null)
//                {
//                    strPointNum = mset.MgrValue;
//                }
//                StringBuilder strSql = new StringBuilder();
//                strSql.Append(@"select b.fareaname 队别,a.fcrimecode 编号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.gdj 单价,a.gtxm 条码,convert(numeric(18,"+ strPointNum +@"),sum(a.qty)) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
//                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
//                and c.fsn='" + strFsn + @"' group by b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
//				order by b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");

//                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
//                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_PersonGoodInfo.xls");
//                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
//                //ExcelRender.RenderToExcel(dt, context, strFileName);
//                ExcelRender.RenderToExcel(dt, invOut.remark + "商品订货信息",8, strFileName, 1);
//                return Content("OK|" + strFsn + "_PersonGoodInfo.xls");
//            }
//            return Content("Err|导出Excel失败");

//        }

//        public ActionResult ExcelOutByInvoiceNo()//按订单号汇总生成Excel订货单
//        {

//            string strFsn = Request["FBidExcel"];
//            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
//            if (string.IsNullOrEmpty(strFsn) == false)
//            {
//                T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
//                string strPointNum = "2";
//                if (mset != null)
//                {
//                    strPointNum = mset.MgrValue;
//                }
//                StringBuilder strSql = new StringBuilder();
//                strSql.Append(@"select b.fareaname 队别,a.invoiceno 单号,a.SPShortCode 简码,a.gname 品名,isnull(a.Remark,'') 规格,a.GDJ 单价,a.gtxm 条码,convert(numeric(18," + strPointNum + @"),sum(a.qty)) 数量,sum(a.amount) 金额 from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
//                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
//                and c.fsn='" + strFsn + @"' group by b.fareaname,a.invoiceno,a.SPShortCode,a.gname,isnull(a.Remark,''),'' gdj,a.gtxm
//				order by b.fareaname,a.invoiceno,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");

//                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
//                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_PersonGoodInfo.xls");
//                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
//                //ExcelRender.RenderToExcel(dt, context, strFileName);
//                ExcelRender.RenderToExcel(dt, invOut.remark + "商品订货信息", 8, strFileName, 1);
//                return Content("OK|" + strFsn + "_PersonGoodInfo.xls");
//            }
//            return Content("Err|导出Excel失败");

//        }

//        public ActionResult ExcelOutByInvoiceList()//按订单号汇总领货表格（每单一条）
//        {

//            string strFsn = Request["FBidExcel"];
//            T_Invoice_out invOut = new T_Invoice_outBLL().GetModelList("fsn='" + strFsn + "'")[0];
//            if (string.IsNullOrEmpty(strFsn) == false)
//            {
//                T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
//                string strPointNum = "2";
//                if (mset != null)
//                {
//                    strPointNum = mset.MgrValue;
//                }
//                StringBuilder strSql = new StringBuilder();
//                strSql.Append(@"select a.fareaname 队别,a.FCrimeCode 编号,a.FCriminal 姓名,a.invoiceno 流水号,sum(a.AMount) 金额,'' 领货日期,'' 签字
//                    from t_invoice a,t_invoice_outDtl b
//                    where a.invoiceno=b.invoiceno and a.flag=1
//                    and b.fsn='" + strFsn + @"' group by a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno
//				    order by a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno");

//                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
//                string strFileName = new CommonClass().GB2312ToUTF8(strFsn + "_ExcelOutByInvoiceList.xls");
//                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
//                //ExcelRender.RenderToExcel(dt, context, strFileName);
//                //ExcelRender.RenderToExcel(dt, invOut.remark + "商品领货表", 4, strFileName, 1);
//                ExcelRender.RenderToExcel(dt, invOut.remark + "商品领货表", 4, strFileName);
//                return Content("OK|" + strFsn + "_ExcelOutByInvoiceList.xls");
//            }
//            return Content("Err|导出Excel失败");

//        }


        public ActionResult PrintOutArea()
        {
            string strFsn = Request["FBidExcel"];
            string roomNoFlag = Request["roomNoFlag"];
            if (string.IsNullOrEmpty(roomNoFlag))
            {
                roomNoFlag = "0";
            }
            ViewData["roomNoFlag"] = roomNoFlag;
            if (string.IsNullOrEmpty(strFsn) == false)
            {
                StringBuilder strSql = new StringBuilder();
                if (roomNoFlag == "0")
                {
                    strSql.Append(@"select b.FAreaName FAreaName,a.SPShortCode SPShortCode,a.gname GName,isnull(a.Remark,'') Remark,a.gdj,a.gtxm GTXM,sum(a.qty) FCount,sum(a.amount) FMoney from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                and c.fsn='" + strFsn + "' group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");
                }
                else if (roomNoFlag == "1")
                {
                    strSql.Append(@"select b.FAreaName FAreaName,isnull(RoomNo,'') RoomNo,a.SPShortCode SPShortCode,a.gname GName,isnull(a.Remark,'') Remark,a.gdj,a.gtxm GTXM,sum(a.qty) FCount,sum(a.amount) FMoney from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                and c.fsn='" + strFsn + "' group by b.FAreaName,isnull(RoomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");
                }
                else if (roomNoFlag == "3")
                {
                    string strPointNum = GetCountPointNum();
                    //StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"select b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,'') Remark,a.gdj,a.gtxm as Gtxm,convert(numeric(18," + strPointNum + @"),sum(a.qty)) FCount,sum(a.amount) FMoney from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                        where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                        and c.fsn='" + strFsn + @"' group by b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
				        order by b.fareaname,a.fcrimecode,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                }
                else if(roomNoFlag=="4")
                {
                    string strPointNum = GetCountPointNum();
                    strSql.Append(@"select b.fareaname,a.invoiceno InvoiceNo,a.SPShortCode,a.gname,isnull(a.Remark,'') Remark,a.gdj,a.gtxm,convert(numeric(18," + strPointNum + @"),sum(a.qty)) FCount,sum(a.amount) FMoney from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                        where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                        and c.fsn='" + strFsn + @"' group by b.fareaname,a.invoiceno,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm
				        order by b.fareaname,a.invoiceno,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                }
                else if (roomNoFlag == "5")
                {
                    strSql.Append(@"select a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno as InvoiceNo,sum(a.AMount) FMoney,'' Remark,'0' gdj,'' SPShortCode
                        from t_invoice a,t_invoice_outDtl b
                        where a.invoiceno=b.invoiceno and a.flag=1
                        and b.fsn='" + strFsn + @"' group by a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno
				        order by a.fareaname,a.FCrimeCode,a.FCriminal,a.invoiceno");

                }
                else
                {
                    strSql.Append(@"select b.crtby FAreaName,a.SPShortCode SPShortCode,a.gname GName,isnull(a.Remark,'') Remark,a.gdj,a.gtxm GTXM,sum(a.qty) FCount,sum(a.amount) FMoney from t_invoicedtl a,t_invoice b,t_invoice_outDtl c
                where a.invoiceno=b.invoiceno and b.invoiceno=c.invoiceno and a.flag=1
                and c.fsn='" + strFsn + "' group by b.crtby,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gdj,a.gtxm");
                }

                List<PeihuoDanPrintList> phds =new CommTableInfoBLL().GetListData(strSql.ToString());
                ViewData["phds"] = phds;
            }
            return View();
        }

        private static string GetCountPointNum()
        {
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
            string strPointNum = "2";
            if (mset != null)
            {
                strPointNum = mset.MgrValue;
            }
            return strPointNum;
        }
        
        private static StringBuilder GetSearchSql(string strAreaName, string strStartDate, string strEndDate, string strRemark, string strFCrimeCode, string strFCrimeName, string typeFlag, string searchFlag)
        {
            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" TypeFlag=" + typeFlag + " ");
            whereFlag = 1;
            //队别
//            if (string.IsNullOrEmpty(strAreaName) == false)
//            {
//                if (whereFlag == 0)
//                {
//                    strSql.Append(@" FAreaName in( select fname from t_area where fname='"+ strAreaName +@"' or fid in(
//                                    select id from t_area where fname='" + strAreaName + "') ) ");
//                    whereFlag = 1;
//                }
//                else
//                {
//                    strSql.Append(" and FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
//                                    select id from t_area where fname='" + strAreaName + "') ) ");
//                }
//            }
            //状态
            if (string.IsNullOrEmpty(searchFlag) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" Flag=" + searchFlag + " ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and Flag=" + searchFlag + " ");
                }
            }
            //开始日期
            if (string.IsNullOrEmpty(strStartDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtdt>='" + strStartDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtdt>='" + strStartDate + "' ");
                }
            }
            //结束日期
            if (string.IsNullOrEmpty(strEndDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtdt<'" + strEndDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtdt<'" + strEndDate + "' ");
                }
            }
            //备注
            if (string.IsNullOrEmpty(strRemark) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" remark like '%" + strRemark + "%' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and remark like '%" + strRemark + "%' ");
                }
            }

            if (string.IsNullOrEmpty(strFCrimeCode) == false || string.IsNullOrEmpty(strFCrimeName) == false)
            {
                int inWhereFlag = 0;
                if (whereFlag == 0)
                {
                    strSql.Append(@"  bid in (
                                select bid from t_invoice_outdtl ");
                }
                else
                {
                    strSql.Append(@"and fsn in (
                                select fsn from t_invoice_outdtl ");
                }
                if (string.IsNullOrEmpty(strFCrimeCode) == false)
                {
                    if (inWhereFlag == 0)
                    {
                        strSql.Append(@" where FCrimeCode='" + strFCrimeCode + "'");
                        inWhereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(@" and FCrimeCode='" + strFCrimeCode + "'");
                    }
                }

                if (string.IsNullOrEmpty(strFCrimeName) == false)
                {
                    if (inWhereFlag == 0)
                    {
                        strSql.Append(@" where fcriminal like '%" + strFCrimeName + "%'");
                        inWhereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(@" and fcriminal like '%" + strFCrimeName + "%'");
                    }
                }
                strSql.Append(@" )");

            }
            return strSql;
        }

        private static StringBuilder GetInvoiceSearchSql(string wselCrtBy, string strAreaName, string strStartDate, string strEndDate, string strRemark, string strFCrimeCode, string strFCrimeName, string typeFlag)
        {
            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();
            if (string.IsNullOrEmpty(typeFlag) == false)
            {
                strSql.Append(" TypeFlag=" + typeFlag + " ");
                whereFlag = 1;
            }
            
            //队别
            if (string.IsNullOrEmpty(strAreaName) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
                                    select id from t_area where fname='" + strAreaName + "') ) ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
                                    select id from t_area where fname='" + strAreaName + "') ) ");
                }
            }
            //开始日期
            if (string.IsNullOrEmpty(strStartDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" OrderDate>='" + strStartDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and OrderDate>='" + strStartDate + "' ");
                }
            }
            //结束日期
            if (string.IsNullOrEmpty(strEndDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" OrderDate<'" + strEndDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and OrderDate<'" + strEndDate + "' ");
                }
            }
            
            if (string.IsNullOrEmpty(strFCrimeCode) == false || string.IsNullOrEmpty(strFCrimeName) == false)
            {
                
                if (string.IsNullOrEmpty(strFCrimeCode) == false)
                {
                    if (whereFlag == 0)
                    {
                        strSql.Append(@" FCrimeCode='" + strFCrimeCode + "'");
                        whereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(@" and FCrimeCode='" + strFCrimeCode + "'");
                    }
                }

                if (string.IsNullOrEmpty(strFCrimeName) == false)
                {
                    if (whereFlag == 0)
                    {
                        strSql.Append(@" fcriminal like '%" + strFCrimeName + "%'");
                        whereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(@" and fcriminal like '%" + strFCrimeName + "%'");
                    }
                }                
            }
            if (string.IsNullOrEmpty(wselCrtBy) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" crtby = '" + wselCrtBy + "'");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and crtby = '" + wselCrtBy + "'");
                }
            }

            

            return strSql;
        }

	}
}