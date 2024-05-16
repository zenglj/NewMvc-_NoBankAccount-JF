using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SelfhelpOrderMgr.Common;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using SelfhelpOrderMgr.Web.Filters;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [CustomActionFilterAttribute]
    [MyLogActionFilterAttribute]
    public class JfLaobaoController : Controller
    {
        JifenMgrService _jifenMgrService = new JifenMgrService();
        //
        // GET: /Laobao/

        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index(int id=1)
        {
            //监区队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;

            //操作员
            List<T_CZY> czies = new T_CZYBLL().GetModelList("");
            ViewData["czies"] = czies;

            List<T_Savetype> savetypes = new T_SavetypeBLL().GetModelList(" typeflag=0  and UseType=1");
            ViewData["savetypes"] = savetypes;

            ViewData["PowerId"] = id;//1是一般录入，2是审核，3是复核，4是财务入账,5是所有功能都有,6是莆田

            //判断劳动报酬Excel的格式
            int excelModel_Id = 1;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            if (mset != null)
            {
                excelModel_Id = Convert.ToInt32(mset.MgrValue);
            }

            ViewData["excelModel_Id"] = excelModel_Id;
            
            return View();
        }
        public ActionResult getBonus(int id = 1)
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string action = Request["action"];
            List<T_JF_BONUS> bonuies;
            StringBuilder strSql = new StringBuilder();
            if (action == "LoginIn")
            {
                switch (id)
                {
                    case 1://新建待提交的记录
                        {
                            strSql.Append("isnull(flag,0)=0 and CrtBy='" + strLoginName + "'"); 
                        } break;
                    case 2://已提交待审核的记录
                        {
                            strSql.Append("isnull(flag,0)=0 and isnull(FCheckFlag,0)=1 and isnull(AuditFlag,0)=0 and (TargetExaminerBy='"+ strLoginName + "' or AuditBy='"+ strLoginName +"')");
                        } break;
                    case 3://已审核待复核的记录
                        {
                            strSql.Append("isnull(flag,0)=0 and isnull(auditFlag,0)=1 and isnull(FDbCheckFlag,0)=0 and (TargetExaminerBy='" + strLoginName + "' or FDbCheckBy='" + strLoginName + "')");
                        } break;
                    case 4://已复核待入账的记录
                        {
                            strSql.Append("isnull(flag,0)=0 and isnull(FDbCheckFlag,0)=1 and (TargetExaminerBy='" + strLoginName + "' or FPostBy='" + strLoginName + "')");
                        } break;
                    default://为入账的地址
                        {
                            strSql.Append("isnull(flag,0)=0 "); 
                        }
                        break;
                }
                bonuies = _jifenMgrService.QueryList<T_JF_BONUS>(strSql.ToString());
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
                //获取查询条件的SQL
                strSql = GetSearchSql(strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName);
                switch(id)
                {
                    case 1:
                        {
                            strSql.Append(" and FAreaCode in(select fareacode from t_Czy_Area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "') ");
                        }break;
                    case 2: 
                        {
                            strSql.Append(" and FCheckFlag=1 and (auditBy='" + strLoginName + "' or TargetExaminerBy='"+ strLoginName +"')");
                        }break;
                    case 3:
                        {
                            strSql.Append(" and auditFlag=1 and (FDbCheckBy='" + strLoginName + "' or TargetExaminerBy='" + strLoginName + "')");
                        } break;
                    case 4:
                        {
                            strSql.Append(" and FDbCheckFlag=1 and (FPostBy='" + strLoginName + "' or TargetExaminerBy='" + strLoginName + "')");
                        } break;
                    default:
                        break;
                }
                List<T_JF_BONUS> list;
                list = _jifenMgrService.QueryList<T_JF_BONUS>( strSql.ToString());
                if ( list==null)
                {
                    list = new List<T_JF_BONUS>();
                    T_JF_BONUS m1 = new T_JF_BONUS();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                return Content(jss.Serialize(list));
            }
        else
        {
            bonuies=new  List<T_JF_BONUS>();
        }
        return Content(jss.Serialize(bonuies));
    }

        private static StringBuilder GetSearchSql(string strAreaName, string strStartDate, string strEndDate, string strRemark, string strFCrimeCode, string strFCrimeName)
        {
            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();
            //队别
            if (string.IsNullOrEmpty(strAreaName) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
                                    select id from t_area where fname='" + strAreaName + "') ) ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
                                    select id from t_area where fname='" + strAreaName + "') ) ");
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
                    strSql.Append(" Remark like '%" + strRemark + "%' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and Remark like '%" + strRemark + "%' ");
                }
            }

            if (string.IsNullOrEmpty(strFCrimeCode) == false || string.IsNullOrEmpty(strFCrimeName) == false)
            {
                int inWhereFlag = 0;
                if (whereFlag == 0)
                {
                    strSql.Append(@"  bid in (
                                select bid from t_Bonusdtl ");
                }
                else
                {
                    strSql.Append(@"and bid in (
                                select bid from t_Bonusdtl ");
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

        public ActionResult AddMainOrder()//增加主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strFAreaName = Request["sAreaName"];//队别名称
            string strFYear = Request["syear"];//年
            string strFMonth = Request["smonth"];//月 

            //获得月最大发放次数
             /*2020-01-13 zenglj
              * 修改删除主单验证
              */
            //T_SETTINGS sModel = new T_SETTINGSBLL().GetModelList("TYPE=606")[0];
            //int sendCount = new T_BONUSBLL().GetSendCountByArea( strFAreaName, strFYear + "-" + strFMonth + "-1");
            //if (Convert.ToInt32(sModel.VALUE) <= sendCount)
            //{
            //    return Content("Error|各队别每月发放次数，不能超过" + sModel.VALUE + "次");
            //}
            //else
            //{
                
            //}
            //生成设定一个主单信息
            return AddNewBonusMainOrder(strLoginName, strFYear, strFMonth);
        }

        private ActionResult AddNewBonusMainOrder(string strLoginName, string strFYear, string strFMonth)
        {
            //生成设定一个主单信息
            bonusInfo bonusinfo = new bonusInfo();
            bonusinfo.strDType= Request["sDtype"];//类型
            List<T_Savetype> stypes = new T_SavetypeBLL().GetModelList("typeflag=0  and UseType=1");
            var subtypes=stypes.Where(e => e.fname == bonusinfo.strDType).ToList();
            if(bonusinfo.strDType!="劳动积分" && subtypes.Count <= 0)
            {
                return Content("Error|请选择一个正确的主单");
            }
            if (Request["sTypeFlag"].ToString() == "4")
            {
                bonusinfo.iTypeFlag = Convert.ToInt32(Request["sTypeFlag"]);//类型编码
                bonusinfo.iSubTypeFlag = 0;//类型子编码
            }
            else
            {
                bonusinfo.iTypeFlag = 0;//类型编码
                bonusinfo.iSubTypeFlag = Convert.ToInt32(Request["sSubTypeFlag"]);//类型子编码
            }
            bonusinfo.strApplyBy = Request["sapplyby"];//申请人
            bonusinfo.strFAreaName = Request["sAreaName"];//队别名称
            bonusinfo.strFAreaCode = Request["sAreaCode"];//队别编号
            bonusinfo.strFYear = Request["syear"];//年
            bonusinfo.strFMonth = Request["smonth"];//月 
            bonusinfo.strRemark = Request["sremark"];//备注
            bonusinfo.strCrtDate = strFYear + "-" + strFMonth + "-1";
            //T_BONUS model = SetMainOrderModelInfo(bonusinfo, strLoginName);
            //if (new T_BONUSBLL().Add(model, "part"))
            //{
            //    T_BONUS newModel = new T_BONUSBLL().GetModel(model.BID);
            //    return Content("OK|" + jss.Serialize(newModel));
            //}
            //else
            //{
            //    return Content("Error|创建主单失败");
            //}

            try
            {
                T_JF_BONUS model = SetMainOrderModelInfoExt(bonusinfo, strLoginName);

                var rs = new BaseDapperBLL().Insert<T_JF_BONUS>(model);
                return Content("OK|" + jss.Serialize(rs));
            }
            catch (Exception e)
            {

                return Content("Error|创建主单失败"+e.Message);
            }
            
        }

        private  T_JF_BONUS SetMainOrderModelInfo(bonusInfo bonusinfo, string LoginUserName)
        {
            T_JF_BONUS model = new T_JF_BONUS();
            model.BID = _jifenMgrService.CreateOrderId( "BNS");
            model.ApplyBy = bonusinfo.strApplyBy;
            model.crtdt = DateTime.Today;
            model.FCheckFlag = 0;
            model.FLAG = 0;
            model.udate =Convert.ToDateTime( bonusinfo.strCrtDate);
            model.Remark = bonusinfo.strRemark;
            model.fareaName = bonusinfo.strFAreaName;
            model.FAREACODE = bonusinfo.strFAreaCode;
            model.Crtby = LoginUserName;
            model.fAMOUNT = 0;
            model.cnt = 0;
            model.Applydt = DateTime.Today;
            model.ptype = bonusinfo.strRemark;
            return model;
        }

        private  T_JF_BONUS SetMainOrderModelInfoExt(bonusInfo bonusinfo, string LoginUserName)
        {
            T_JF_BONUS model = new T_JF_BONUS()
            {
                BID = _jifenMgrService.CreateOrderId("BNS"),
                ApplyBy = bonusinfo.strApplyBy,
                crtdt = DateTime.Today,
                FCheckFlag = 0,
                FLAG = 0,
                udate = Convert.ToDateTime(bonusinfo.strCrtDate),
                Remark = bonusinfo.strRemark,
                fareaName = bonusinfo.strFAreaName,
                FAREACODE = bonusinfo.strFAreaCode,
                Crtby = LoginUserName,
                fAMOUNT = 0,
                cnt = 0,
                Applydt = DateTime.Today,
                ptype = bonusinfo.strRemark,
                DType = bonusinfo.strDType,
                TypeFlag = bonusinfo.iTypeFlag,
                SubTypeFlag = bonusinfo.iSubTypeFlag,
                auditby = "",
                auditflag = 0,
                auditdate = Convert.ToDateTime("1900-01-01"),
                CHECKBY = "",
                CheckDate = Convert.ToDateTime("1900-01-01"),
                FDbCheckBY = "",
                Fdbcheckdate = Convert.ToDateTime("1900-01-01"),
                Fdbcheckflag = 0,
                FPostBy = "",
                FPostDate = Convert.ToDateTime("1900-01-01"),
                FPostFlag = 0,
                Frealareacode = "",
                FrealAreaName = ""
            };
            

            return model;
        }

        public ActionResult getLaobaoDetailByBid()
        {
            //分页信息
            string action = Request["action"];
            string strPage = Request["page"];
            string strRow = Request["rows"];
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
            string FBid = Request["FBid"];
            if (string.IsNullOrWhiteSpace(FBid))
            {
                FBid = "";
            }
            
            //List<T_JF_BONUSDTL> bonusDtls = new T_BONUSBLL().GetDtlPageList(page,row,"BID='" + FBid + "'","seqno");
            PageResult<T_JF_BONUSDTL> bonusDtls = _jifenMgrService.GetPageList<T_JF_BONUSDTL>("Id asc", jss.Serialize( new { BID= FBid }),page, row, "");

            //return Content(jss.Serialize(bonusDtls));
            sss = "{\"total\":" + bonusDtls.total.ToString() + ",\"rows\":" + jss.Serialize(bonusDtls.rows) + "}";
            return Content(sss);
        }

        //删除明细记录
        public ActionResult DelOrderDetail()
        {
            string strFBid = Request["sbid"];//主单编号
            string strSeqno = Request["seqno"];//流水号
            T_JF_BONUS bonus = _jifenMgrService.Query<T_JF_BONUS>( "Bid='"+ strFBid+"'");
            if (bonus.FCheckFlag==1)
            {
                return Content("Error.主单已经确认明细不能删除");
            }
            if (_jifenMgrService.Delete<T_JF_BONUSDTL>(Convert.ToInt32(strSeqno)))
            {
                _jifenMgrService.UpdateBonusByCountMoney(strFBid);

                T_JF_BONUS model = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
                return Content("OK." + model.BID + "|" + model.cnt + "|" + model.fAMOUNT);
            }
            else
            {
                return Content("Error.删除失败");
            }
        }

        //全部删除明细记录
        public ActionResult DelAllOrderDetail()
        {
            string strFBid = Request["sbid"];//主单编号
            //string strSeqno = Request["seqno"];//流水号
            T_JF_BONUS bonus = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
            if (bonus.FCheckFlag==1)
            {
                return Content("Error.主单已经确认明细不能删除");
            }
            else
            {
                try
                {
                    string strSql = "delete from t_JF_BonusDtl where bid='" + strFBid + "';update t_JF_Bonus set famount=0,cnt=0 where bid='" + strFBid + "'";
                    if (new CommTableInfoBLL().ExecSql(strSql) > 0)
                    {
                        return Content("OK." + strFBid + "|" + 0 + "|" + 0);
                    }
                    else
                    {
                        return Content("Error.删除失败");
                    }
                }
                catch 
                {
                    return Content("Error.删除操作时执行失败");
                }
                
            }
            
            
        }

        
        public ActionResult PostMainOrder(int id=1)//提交主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string strFBid = Request["sbid"];//主单编号
            

            if (string.IsNullOrEmpty(strFBid))
            {
                return Content("Err|主单号不能为空");
            }

            string targetUser = Request["TargetUser"];//目标审核人
            if (string.IsNullOrWhiteSpace(targetUser))
            {
                return Content("Err|目标审核人不能为空");
            }
            T_JF_BONUS model = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
            if(model==null)
            {
                return Content("Err|该主单号不存在，请确认是否有误");
            }
            if (model.FLAG == 1)
            {
                return Content("Err|该主单已经入账了，不能再提交");
            }
            if (model.Fdbcheckflag == 1)
            {
                return Content("Err|该主单已经复核了，不能再提交");
            }
            if (model.auditflag == 1)
            {
                return Content("Err|该主单已经审核了，不能再提交");
            }
            if (model.FCheckFlag == 1)
            {
                return Content("Err|该主单已经提交过了，不能再提交");
            }

            model.CHECKBY = strLoginName;
            model.CheckDate = DateTime.Today;
            model.FCheckFlag = 1;
            model.BID = strFBid;
            model.TargetExaminerBy = targetUser;
            model.MainStatus = 1;
            model.FrealAreaName = "";
            //id=6是莆田模式的提交，首先将FCheckFlag、AuditFlag、FDbCheckFlag都设1
            //其次是把记录写到T_Vcrd中，并将BankFlag设定1， 本提交只执行这步
            //最后是根据银行反馈的结果，更新T_Criminal_Card表
            //如果失败就删除T_Vcrd记录,并将T_BonusDTL记录移到ErrList中
            //同时更新主单的成功数量及金额
            if (id == 6 || id == 7)
            {   //6是莆田模式，提交写入到Vcrd中，7是一般模式，只改变主单到已经复核
                //审核状态
                model.auditby = strLoginName;
                model.auditdate = DateTime.Today;
                model.auditflag = 1;
                //复核状态
                model.FDbCheckBY = strLoginName;
                model.Fdbcheckdate = DateTime.Today;
                model.Fdbcheckflag = 1;

                //提交状态
                model.FPostBy = "";
                model.FPostDate = DateTime.Today;
                model.FPostFlag = 0;

                model.MainStatus = 3;
                if (_jifenMgrService.Update(model))//如果更新状态成功，则开写入到VCRD
                {
                    T_JF_BONUS ttbbs = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
                    if (id == 7)//一般模式不要执行写VCrd数据
                    {
                        return Content("OK|提交写入数据成功|" + jss.Serialize(ttbbs));
                    }
                    if(new T_BONUSBLL().UpdateInDbOnlyVcrd(strFBid, strLoginName))
                    {                        
                        return Content("OK|提交写入数据成功|" + jss.Serialize(ttbbs));
                    }else
                    {
                        //回滚主单的状态
                        model.FCheckFlag = 1;
                        model.auditflag = 1;
                        model.Fdbcheckflag = 1;
                        model.FPostFlag = 1;
                        _jifenMgrService.Update(model);
                        return Content("Err|写入VCrd时失败");                        
                    }
                    
                }
            }else
            {
                if (_jifenMgrService.Update(model))
                {
                    T_JF_BONUS ttbbs = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
                    return Content("OK|提交成功|" + jss.Serialize(ttbbs));
                    
                }
                else
                {
                    return Content("Err|提交失败");
                }
            }
            return Content("Err|提交失败");
        }

        public ActionResult AuditMainOrder()//审核主单
        {
            string strFBid = Request["sbid"];//主单编号
            string targetUser = Request["TargetUser"];//目标审核人
            if (string.IsNullOrWhiteSpace(targetUser))
            {
                return Content("Err.目标审核人不能为空");
            }
            if (string.IsNullOrEmpty(strFBid) == false)
            {
                T_JF_BONUS model = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
                if (model != null)
                {
                    if (model.FCheckFlag < 1)
                    {
                        return Content("Err.主单未‘确认’，不能审核");
                    }
                }
                string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
                model.auditby = strLoginName;
                model.auditdate = DateTime.Today;
                model.auditflag = 1;
                model.BID = strFBid;
                model.TargetExaminerBy = targetUser;
                model.MainStatus = 2;

                string rtnStr= _jifenMgrService.CheckOutPrisonList(strFBid);
                //new T_BONUSBLL().UpdateByAuditFlag(model)
                if (_jifenMgrService.Update(model))
                {
                    return Content("OK.提交成功," + rtnStr);
                }
                else
                {
                    return Content("Err.提交失败," + rtnStr);
                }
            }
            else
            {
                return Content("Err.Bid主单号不能为空");
            }
            
        }

        public ActionResult DbCheckMainOrder()//财务复核主单
        {
            string strFBid = Request["sbid"];//主单编号
            string targetUser = Request["TargetUser"];//目标审核人
            if (string.IsNullOrWhiteSpace(targetUser))
            {
                return Content("Err|目标审核人不能为空");
            }
            if (string.IsNullOrEmpty(strFBid)==false)
            {
                T_JF_BONUS model = _jifenMgrService.Query<T_JF_BONUS>($"Bid='{strFBid}'");
                if (model != null)
                {
                    if (model.auditflag < 1)
                    {
                        return Content("Err.主单未‘审核’，不能复核");
                    }
                }
                string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

                model.FDbCheckBY = strLoginName;
                model.Fdbcheckdate = DateTime.Today;
                model.Fdbcheckflag = 1;
                model.BID = strFBid;
                model.TargetExaminerBy = targetUser;
                model.MainStatus = 3;
                if (_jifenMgrService.Update(model))
                {
                    return Content("OK.提交成功");
                }
                else
                {
                    return Content("Err.提交失败");
                }

            }
            else
            {
                return Content("Err.Bid主单号不能为空");
            }
            
        }

        public ActionResult InDbMainOrder()//财务入账主单
        {
            string strFBid = Request["sbid"];//主单编号

            //判断劳动报酬Excel的格式
            int id = 1;
            //T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("JiFenModel");
            if (mset != null)
            {
                id = Convert.ToInt32(mset.MgrValue);
            }

            if(string.IsNullOrEmpty(strFBid)==false)
            {
                T_JF_BONUS bonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strFBid);
                if(bonus!=null)
                {
                    if(bonus.Fdbcheckflag<1)
                    {
                        return Content("Err|财务未复核，不能入账");
                    }
                    if (bonus.FLAG >= 1)
                    {
                        return Content("Err|该主单已经入账过了，不能重复入账");
                    }
                }
                string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
                T_SHO_ManagerSet lbset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoIsCheckFlag");
                int checkflag = 1;
                if (lbset != null)
                {
                    checkflag = Convert.ToInt32(lbset.MgrValue);
                }

                bool upFlag = false;
                if (id == 2)
                {//厦门的模板 
                    upFlag=_jifenMgrService.UpdateInDbFlag(strFBid, strLoginName, checkflag,id);
                }
                else
                {//标准的模板
                    upFlag=_jifenMgrService.UpdateInDbFlag(strFBid, strLoginName, checkflag);
                }

                if (upFlag)
                {
                    List < T_ImportList > imports= new T_ImportListBLL().GetModelList("pc='" + strFBid + "' and Remark='该记录财务入账时，已离监销户了'");
                    if (imports.Count>0)
                    {
                        return Content("OK|提交成功,但有" + imports.Count.ToString() + "条已销IC卡，不能入账");
                    }
                    else
                    {
                        return Content("OK|全部提交成功");
                    }
                }
                else
                {
                    return Content("Err|提交失败");
                }

            }
            else
            {
                return Content("Err.Bid主单号不能为空");
            }
            
        }

        [MyLogActionFilterAttribute]
        public ActionResult DelMainOrder(string sbid)//删除主单
        {
            string strFBid = sbid;// Request["sbid"];//主单编号
            T_JF_BONUS model = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid", strFBid);
            if (model.auditflag != 1)//判断是否已经审核
            {
                if (_jifenMgrService.Delete<T_JF_BONUS>( model.Id))
                {
                    //new T_BONUSBLL().DeleteDtlByBid(strFBid);
                    _jifenMgrService.Delete<T_JF_BONUSDTL>("Bid", strFBid);
                    return Content("OK.主单删除成功");
                }
                else
                {
                    return Content("Error.删除主单失败");
                }
            }
            else
            {
                return Content("Error.已经审核不能删除");
            }
        }

        [MyLogActionFilterAttribute]
        public ActionResult UndoMainOrder(string sbid)//主单退账
        {
            string strFBid = sbid;// Request["sbid"];//主单编号
            T_JF_BONUS model = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid", strFBid);

            //如果已经发送到银行不能退账
            List<T_JF_Vcrd> vcrds = _jifenMgrService.QueryList<T_JF_Vcrd>(" flag=0 and Origid='" + strFBid + "' and isnull(bankflag,0)>=1");
            if (vcrds.Count > 0)
            {
                return Content("Error.该主单数据已经发送到银行付款，不能退账");
            }

            List<T_JF_BONUSDTL> dtls = _jifenMgrService.QueryList<T_JF_BONUSDTL>(" bid='" + strFBid + "' and fcrimecode in(select fcode from t_criminal where fflag=1)");
            if (dtls.Count > 0)
            {
                return Content($"Error.有罪犯{dtls[0].fcriminal}等{dtls.Count}人已经离监了，不能退账");
            }


            Log4NetHelper.logger.Info("劳动报酬退账,操作员：" + Session["loginUserName"].ToString() + ",主单号：ID=" + model.BID + ",积分：" + model.fAMOUNT.ToString() + "");

            if (_jifenMgrService.UndoMainOrder(strFBid))
            {
                return Content("OK.主单退账成功");
            }
            else
            {
                return Content("Error.主单退账失败");
            }
        }

        public ActionResult ExcelOut(int id=1)//Excel导出成功记录
        {
            
            string strFBid = Request["FBidExcel"];

            //判断劳动报酬Excel的格式
            int excelModel_Id = 1;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            if (mset != null)
            {
                excelModel_Id = Convert.ToInt32(mset.MgrValue);
            }

            if(id==6) //莆田劳动报酬导出格式      
            {
//                DataTable dt = new CommTableInfoBLL().GetDataTable(@"select ROW_NUMBER() OVER (ORDER BY a.seqno) AS 序号,a.fcrimecode ID,a.fcriminal 姓名,b.bankaccno 银行卡号,a.famount 录卡 from t_bonusdtl a,t_criminal_card b where bid='"+ strFBid +@"' and a.fcrimecode=b.fcrimecode
//                    order by a.seqno");
                DataTable dtMain = new CommTableInfoBLL().GetDataTable(@"select  * from t_JF_bonus b where bid='" + strFBid + @"' ");
                if(dtMain.Rows[0]["FCheckFlag"].ToString()!="1")
                {
                    return Content("Err|主单未提交确认不能导出");
                }
                DataTable dt = new CommTableInfoBLL().GetDataTable(@"SELECT  a.FCRIMECODE AS 编号, a.fcriminal AS 姓名, b.BankAccNo AS 银行卡号, a.FAMOUNT AS 录卡, a.BID
                    FROM dbo.T_JF_BONUSDTL AS a INNER JOIN
                    dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode where Bid='"+ strFBid + "'");
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                ExcelRender.RenderToExcel(dt, "劳动报酬录入积分", 3, strFileName);
            }
            else
            {

                if (excelModel_Id == 1)
                {
                    //DataTable dt = new T_BONUSBLL().GetDtlDataTableByBid(strFBid);
                    DataTable dt = new CommTableInfoBLL().GetDataTable(@"SELECT   a.FCRIMECODE AS 编号, a.fcriminal AS 姓名, b.BankAccNo AS 积分卡号, a.FAMOUNT AS 录卡积分, a.BID
                    FROM dbo.T_JF_BONUSDTL AS a INNER JOIN
                    dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode where Bid='" + strFBid + "'");
                    string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoList.xls");
                    strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                    //ExcelRender.RenderToExcel(dt, context, strFileName);
                    //ExcelRender.RenderToExcel(dt, strFileName);
                    ExcelRender.RenderToExcel(dt, "劳动报酬录入积分", 3, strFileName);
                }
                else if (excelModel_Id==2)
                {
                    //DataTable dt = new T_BONUSBLL().GetDtlDataTableByBid(strFBid);
                    DataTable dt = new CommTableInfoBLL().GetDataTable(@"select  ROW_NUMBER() OVER (ORDER BY 编号) AS 序号,* from (SELECT     a.FCRIMECODE AS 编号, a.fcriminal AS 姓名, b.BankAccNo AS 积分卡号, a.AmountA AS 一盘劳酬, a.AmountA AS 超常奖, a.AmountA AS 留存积分, (a.AmountA+a.AmountB+a.AmountC) AS 总录积分, a.BID as 主单号
                        FROM dbo.T_BONUSDTL AS a INNER JOIN
                      dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode) b where bid='" + strFBid + @"' ");
                    string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoList.xls");
                    strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                    //ExcelRender.RenderToExcel(dt, context, strFileName);
                    //ExcelRender.RenderToExcel(dt, strFileName);
                    ExcelRender.RenderToExcel(dt, "劳动报酬录入积分", 7, strFileName);
                }
                else if (excelModel_Id == 3)
                {
                    //DataTable dt = new T_BONUSBLL().GetDtlDataTableByBid(strFBid);
                    DataTable dt = new CommTableInfoBLL().GetDataTable(@"select  ROW_NUMBER() OVER (ORDER BY 编号) AS 序号,* from (SELECT     a.FCRIMECODE AS 编号, a.fcriminal AS 姓名, b.BankAccNo AS 积分卡号, a.AmountB AS 劳酬积分, a.AmountA AS 留存积分, (a.AmountA+a.AmountB+a.AmountC) AS 总录积分, a.BID as 主单号
                        FROM dbo.T_BONUSDTL AS a INNER JOIN
                      dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode) b where [主单号]='" + strFBid + @"' ");
                    string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoList.xls");
                    strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                    //ExcelRender.RenderToExcel(dt, context, strFileName);
                    //ExcelRender.RenderToExcel(dt, strFileName);
                    ExcelRender.RenderToExcel(dt, "劳动报酬录入积分", 6, strFileName);
                }
                else if (excelModel_Id == 4)
                {
                    //DataTable dt = new T_BONUSBLL().GetDtlDataTableByBid(strFBid);
                    DataTable dt = new CommTableInfoBLL().GetDataTable(@"SELECT   a.FCRIMECODE AS 编号, a.fcriminal AS 姓名, b.CardCodeA AS 积分卡号, a.FAMOUNT AS 录卡积分, a.BID
                    FROM dbo.T_JF_BONUSDTL AS a INNER JOIN
                    dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode where Bid='" + strFBid + "'");
                    string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoList.xls");
                    strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                    //ExcelRender.RenderToExcel(dt, context, strFileName);
                    //ExcelRender.RenderToExcel(dt, strFileName);
                    ExcelRender.RenderToExcel(dt, "劳动报酬录入积分", 3, strFileName);
                }

            }
            
            
            return Content("OK|"+strFBid + "_LaobaoList.xls");
        }

        public ActionResult ErrorListOutport()//Excel导出失败记录
        {
            string strFBid = Request["excelBid"];

            int excelModel_Id = 1;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            if (mset != null)
            {
                excelModel_Id = Convert.ToInt32(mset.MgrValue);
            }
            if (excelModel_Id == 1)
            {
                DataTable dt = new T_BONUSBLL().GetErrListDataTable(strFBid);
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoErrList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, strFileName);
                ExcelRender.RenderToExcel(dt, "失败记录清单", 3, strFileName);
            }
            else if (excelModel_Id == 2)
            {
                DataTable dt = new CommTableInfoBLL().GetDataTable("select pc 主单流水号,fcrimecode 编号,fname 姓名,AmountA 一般劳酬,AmountB 超常奖,AmountC 留存积分 ,Amount 总录积分,Crtdt 导入日期,Remark 失败原因  FROM t_ImportList where pc='" + strFBid + "' ");
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoErrList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, strFileName);
                ExcelRender.RenderToExcel(dt, "失败记录清单", 6, strFileName);
                
            }
            else if (excelModel_Id == 3)
            {
                DataTable dt = new CommTableInfoBLL().GetDataTable("select pc 主单流水号,fcrimecode 编号,fname 姓名,AmountB 劳酬积分,AmountC 留存积分 ,Amount 总录积分,Crtdt 导入日期,Remark 失败原因  FROM t_ImportList where pc='" + strFBid + "' ");
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoErrList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, strFileName);
                ExcelRender.RenderToExcel(dt, "失败记录清单", 5, strFileName);

            }
            else if (excelModel_Id == 4)
            {
                DataTable dt = new CommTableInfoBLL().GetDataTable("select pc 主单流水号,fcrimecode 编号,fname 姓名,AmountB 劳酬积分,AmountC 留存积分 ,Amount 总录积分,Crtdt 导入日期,Remark 失败原因  FROM t_ImportList where pc='" + strFBid + "' ");
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LaobaoErrList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, strFileName);
                ExcelRender.RenderToExcel(dt, "失败记录清单", 5, strFileName);

            }
            else
            {
                return Content("Err|您传入的Excel模板格式不正确");
            }
            return Content("OK|" + strFBid + "_LaobaoErrList.xls");
        }

        public ActionResult ExcelInport()//Excel表格导入
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string strReSaveFlag = Request["reSaveFlag"];
            int id = 1;
            //T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            //积分模板的方式
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("JiFenModel");
            if (mset!=null)
            {
                id = Convert.ToInt32(mset.MgrValue);
            }
            if (Request.Files.Count > 0)
            {
                string strFBid = Request["excelBid"];

                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/Upload/" + fname);
                f.SaveAs(savePath);
                //context.Response.Write("Success!");
                DateTime sdt;
                //DateTime edt;
                //TimeSpan tspan;
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    sdt = DateTime.Now;
                    //XSSFWorkbook workbook = new XSSFWorkbook(stream);
                    IWorkbook workbook = null;
                    try
                    {
                        workbook = new XSSFWorkbook(stream); // 2007版本  
                    }
                    catch
                    {
                        workbook = new HSSFWorkbook(stream); // 2003版本  
                    }
                    //HSSFSheet sheet = workbook.GetSheetAt(0);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                    //NPOI.SS.UserModel.Sheet
                    int rows = sheet.LastRowNum;
                    //int ErrNums = 0;
                    if (rows < 1)
                    {
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        //=======2017-03-17 修正 by zenglj===================
                        //如果该订单已经提交则不能再导入  
                        T_JF_BONUS bons = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strFBid);
                        if (bons.FCheckFlag == 1)
                        {
                            return Content("Err|该订单已经提交过了，不能重复提交");
                        }
                        //=======================================================


                        #region 定义DataTable
                        DataTable dtUserAdd = new DataTable();
                        dtUserAdd.Columns.Add(new DataColumn("BID", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FCrimeCode", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FCriminal", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FMoney", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("FRemark", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("Notes", typeof(string)));                        
                        dtUserAdd.Columns.Add(new DataColumn("AmountA", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("AmountB", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("AmountC", typeof(decimal)));
                        
                        dtUserAdd.Columns.Add(new DataColumn("cqbt", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("gwjt", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("ldjx", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("tbbz", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("grkj", typeof(decimal)));
                        DataRow drTemp = null;
                        #endregion  
                        T_JF_BONUS bonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid", strFBid);
                        if (bonus == null)
                        {
                            return Content("Err|主单编号不存在");
                        }
                        else if(bonus.TypeFlag==4)
                        {
                            if (id == 2)
                            {
                                drTemp = lbExcelModel_Two(strFBid, sheet, rows, dtUserAdd, drTemp);
                            }
                            else
                            {
                                drTemp = lbExcelModel_One(strFBid, sheet, rows, dtUserAdd, drTemp);
                            }


                        }
                        else
                        {
                            drTemp = lbExcelModel_Five(strFBid, sheet, rows, dtUserAdd, drTemp);
                        }

                        #region 写入到数据库中
                        //将DataTabe dtUserAdd 写入到数据库中

                        string rstInfo = "";
                        int reSaveFlag = 0;
                        if (string.IsNullOrEmpty(strReSaveFlag)==false)
                        {
                            reSaveFlag = 1;
                        }
                        rstInfo = lbWriteToDb(id, strLoginName, strFBid, sdt, dtUserAdd, reSaveFlag);
                        return Content(rstInfo);

                        #endregion

                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }

        private string lbWriteToDb(int id, string strLoginName, string strFBid, DateTime sdt, DataTable dtUserAdd, int reSaveFlag=0)
        {
            string rstInfo = "";
            DateTime edt;
            TimeSpan tspan;
            new CommTableInfoBLL().ExecSql("Delete from t_Bonus_Temp where Bid='" + strFBid + "'");
            string strAddResult=new CommTableInfoBLL().AddDataTableToDB(dtUserAdd, "dbo.[T_Bonus_Temp]");
            if (strAddResult=="1")
            {
                bool blDoFlag = false;
                //if (id == 1)
                //{
                //    //reSaveFlag，表示可以重复发放的标志
                //    blDoFlag = _jifenMgrService.PLWriteBonusDtl(strFBid, strLoginName, reSaveFlag);
                //}

                //reSaveFlag，表示可以重复发放的标志
                blDoFlag = _jifenMgrService.PLWriteBonusDtl(strFBid, strLoginName, reSaveFlag);

                if (blDoFlag)
                {
                    rtnLaobaoExcel<T_JF_BONUS,T_JF_BONUSDTL> rtn = new rtnLaobaoExcel<T_JF_BONUS, T_JF_BONUSDTL>();
                    rtn.bonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strFBid);

                    rtn.dtls = _jifenMgrService.GetPageList<T_JF_BONUSDTL>("Id asc", jss.Serialize(new { BID=strFBid}), 1, 10, "").rows;
                    //T_BONUS tbouns = new T_BONUSBLL().GetModel(strFBid);
                    List<T_ImportList> imports = new T_ImportListBLL().GetModelList("pc='" + strFBid + "'");
                    edt = DateTime.Now;
                    tspan = edt - sdt;
                    if (imports.Count > 0)
                    {
                        rstInfo = ("OK|导入完成,失败记录" + imports.Count.ToString() + "条,用时" + tspan.TotalSeconds.ToString() + "秒|" + jss.Serialize(rtn));
                    }
                    else
                    {
                        rstInfo = ("OK|导入完成,用时" + tspan.TotalSeconds.ToString() + "秒|" + jss.Serialize(rtn));
                    }
                }
                else
                {
                    rstInfo = ("Err|劳酬更新积分数据时失败");
                }
            }
            else
            {
                rstInfo = ("Err|写入银行Excel文件时失败." + strAddResult);
            }
            return rstInfo;
        }






        /// <summary>
        /// 标准劳酬Excel格式  ：编号、姓名、积分、备注
        /// </summary>
        /// <param name="strFBid"></param>
        /// <param name="sheet"></param>
        /// <param name="rows"></param>
        /// <param name="dtUserAdd"></param>
        /// <param name="drTemp"></param>
        /// <returns></returns>
        private static DataRow lbExcelModel_One(string strFBid, NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
            #region 标准劳酬Excel格式  ：编号、姓名、积分、备注
            for (int i = 1; i <= rows; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                NPOI.SS.UserModel.CellType iType = 0;
                try
                {
                    iType = row.GetCell(0).CellType;
                }
                catch
                {
                    break;
                }
                string FCode = "";
                if (iType == 0)
                {
                    FCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                }
                else
                {
                    FCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                }
                string FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似
                string FMoney = "0";  //积分
                try
                {
                    FMoney = Convert.ToString(row.GetCell(2).NumericCellValue);
                }
                catch 
                {
                    
                }

                string FRemark = "";  //开始日期
                try
                {
                    FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                }
                catch { }


                #region Excel行写入到DataTabel中
                decimal rstMoney = 0;
                try
                {//如果积分有
                    rstMoney = Convert.ToDecimal(FMoney);
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["BID"] = strFBid;
                        drTemp["FCrimeCode"] = FCode;
                        drTemp["FCriminal"] = FName;
                        drTemp["FMoney"] = FMoney;
                        drTemp["FRemark"] = FRemark;
                        drTemp["Notes"] = "";
                        drTemp["AmountA"] = FMoney;
                        drTemp["AmountB"] = 0;
                        drTemp["AmountC"] = 0;
                        drTemp["cqbt"] = 0;
                        drTemp["gwjt"] = 0;
                        drTemp["ldjx"] = 0;
                        drTemp["tbbz"] = 0;
                        drTemp["grkj"] = 0;

                        dtUserAdd.Rows.Add(drTemp);
                    }

                }
                catch
                { }
                #endregion
            }
            #endregion
            return drTemp;
        }


        /// <summary>
        /// 厦门劳酬Excel格式  ：编号、姓名、积分、备注
        /// </summary>
        /// <param name="strFBid"></param>
        /// <param name="sheet"></param>
        /// <param name="rows"></param>
        /// <param name="dtUserAdd"></param>
        /// <param name="drTemp"></param>
        /// <returns></returns>
        private static DataRow lbExcelModel_Two(string strFBid, NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
            #region 厦门劳酬Excel格式  ：编号、姓名、生产积分、改造积分、总结积分、备注
            for (int i = 1; i <= rows; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                NPOI.SS.UserModel.CellType iType = 0;
                try
                {
                    iType = row.GetCell(0).CellType;
                }
                catch
                {
                    break;
                }
                string FCode = "";
                if (iType == 0)
                {
                    FCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                }
                else
                {
                    FCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                }
                string FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似

                string shengChanJifen = "0";  //生产积分
                try
                {
                    shengChanJifen = Convert.ToString(row.GetCell(2).NumericCellValue);
                }
                catch
                {

                }

                string gaiZhaoJifen = "0";  //改造积分
                try
                {
                    gaiZhaoJifen = Convert.ToString(row.GetCell(3).NumericCellValue);
                }
                catch
                {

                }

                

                string allJifen = "0";  //总积分
                try
                {
                    allJifen = Convert.ToString(row.GetCell(4).NumericCellValue);
                }
                catch
                {

                }

                string FRemark = "";  //备注
                try
                {
                    FRemark = Convert.ToString(row.GetCell(5).StringCellValue);
                }
                catch { }


                #region Excel行写入到DataTabel中
                decimal rstMoney = 0;
                try
                {//如果积分有
                    rstMoney = Convert.ToDecimal(shengChanJifen);
                    rstMoney = Convert.ToDecimal(gaiZhaoJifen);
                    //rstMoney = Convert.ToDecimal(kouchuJifen);
                    rstMoney = Convert.ToDecimal(allJifen);
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["BID"] = strFBid;
                        drTemp["FCrimeCode"] = FCode;
                        drTemp["FCriminal"] = FName;
                        drTemp["FMoney"] = allJifen;
                        drTemp["FRemark"] = FRemark;
                        drTemp["Notes"] = "";
                        drTemp["AmountA"] = shengChanJifen;
                        drTemp["AmountB"] = gaiZhaoJifen;
                        drTemp["AmountC"] = 0;
                        drTemp["cqbt"] = 0;
                        drTemp["gwjt"] = 0;
                        drTemp["ldjx"] = 0;
                        drTemp["tbbz"] = 0;
                        drTemp["grkj"] = 0;

                        dtUserAdd.Rows.Add(drTemp);
                    }

                }
                catch
                { }
                #endregion
            }
            #endregion
            return drTemp;
        }




        private static DataRow lbExcelModel_Five(string strFBid, NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
            T_BONUS bonus = new BaseDapperBLL().GetModelFirst<T_BONUS, T_BONUS>("{\"BID\":\""+strFBid+"\"}");
            List<T_Savetype> savetypes = new T_SavetypeBLL().GetModelList("typeflag=0  and UseType=1 and fcode='" + bonus.SubTypeFlag.ToString() + "'");
            int accTypeFlag = 0;
            if (savetypes.Count > 0)
            {
                accTypeFlag = savetypes[0].AccType;
            }
            #region 其他存款格式  ：编号、姓名、积分、备注
            for (int i = 1; i <= rows; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                NPOI.SS.UserModel.CellType iType = 0;
                try
                {
                    iType = row.GetCell(0).CellType;
                }
                catch
                {
                    break;
                }
                string FCode = "";
                if (iType == 0)
                {
                    FCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                }
                else
                {
                    FCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                }
                string FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似
                string FMoney = "0";  //积分
                try
                {
                    FMoney = Convert.ToString(row.GetCell(2).NumericCellValue);
                }
                catch
                {

                }

                string FRemark = "";  //开始日期
                try
                {
                    FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                }
                catch { }


                #region Excel行写入到DataTabel中
                decimal rstMoney = 0;
                try
                {//如果积分有
                    rstMoney = Convert.ToDecimal(FMoney);
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["BID"] = strFBid;
                        drTemp["FCrimeCode"] = FCode;
                        drTemp["FCriminal"] = FName;
                        drTemp["AmountA"] = 0;
                        drTemp["AmountB"] = 0;
                        drTemp["AmountC"] = 0;
                        if (accTypeFlag == 1)
                        {
                            drTemp["AmountB"] = FMoney;
                        }
                        else if (accTypeFlag == 2)
                        {
                            drTemp["AmountC"] = FMoney;
                        }
                        else
                        {
                            drTemp["AmountA"] = FMoney;
                        }
                        drTemp["FMoney"] = FMoney;
                        drTemp["FRemark"] = FRemark;
                        drTemp["Notes"] = "";
                        drTemp["cqbt"] = 0;
                        drTemp["gwjt"] = 0;
                        drTemp["ldjx"] = 0;
                        drTemp["tbbz"] = 0;
                        drTemp["grkj"] = 0;
                        dtUserAdd.Rows.Add(drTemp);
                    }

                }
                catch
                { }
                #endregion
            }
            #endregion
            return drTemp;
        }

        private string CheckAndAddRecord(string LoginUserName, string LoginUserCode, string strFCode, string strFName, string strFMoney, decimal AmountA, decimal AmountB, decimal AmountC, string strFBid, out string okInfo, out int strFlag, int reSaveFlag=0)
        {
            int excelModel_Id = 1;//默认的劳酬格式
            T_SHO_ManagerSet excelSet = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            if (excelSet != null)
            {
                excelModel_Id = Convert.ToInt32(excelSet.MgrValue);
            }

            string strRusult = "";//返回处理结果信息
            decimal cpctMoney = 0;//留存积分
            okInfo = "";//成功的信息
            T_JF_BONUSDTL model = new T_JF_BONUSDTL();

            //犯人信息不正确或是无权管理
            //List<T_CrimeList> criminals = (List<T_CrimeList>)new T_CriminalBLL().GetCrimeBySearch(dbname, strFCode, strFName, "", LoginUserCode);
            List<T_Criminal> criminals = new T_CriminalBLL().GetModelList("fcode='" + strFCode + "' and fareacode in(select fareacode from t_czy_area where fcode='" + LoginUserCode + "' and fflag=2)");
            strFlag = 0;
            if (criminals.Count <= 0)
            {
                strFlag = 1;//犯人信息不正确或是无权管理
                strRusult = "编号不存在或是无权管理";
            }
            else
            {
                T_Criminal criminal = criminals[0];
                if (criminal.fflag == 1)
                {
                    strFlag = 2;//犯人已经离监结算了
                    strRusult = "犯人已经离监结算了";
                }
                else if (criminal.FName != strFName)
                {
                    strFlag = 6;//姓名与编号不对一致
                    strRusult = "姓名出错：" + strFCode + "对应的姓名是" + criminal.FName + ",而不是" + strFName;
                }
                else
                {
                    T_JF_BONUS bonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strFBid);
                    if (bonus == null)
                    {
                        strFlag = 3;//主单号不存在
                        strRusult = "主单号不存在";
                    }
                    else
                    {
                        //获取处遇信息
                        T_CY_TYPE cy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
                        cpctMoney = Convert.ToDecimal(strFMoney) * (decimal)cy.cpct / 100;

                        //获取队别信息
                        T_AREA area = new T_AREABLL().GetModel(criminal.FAreaCode);

                        //获取卡号信息
                        T_Criminal_card card = new T_Criminal_cardBLL().GetModel(strFCode);
                        if (card == null)
                        {
                            strFlag = 5;//未办理IC卡
                            strRusult = "该犯人未办理IC卡";
                            return strRusult;
                        }
                        T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LyjBankCardCheckFlag");
                        if(mset!=null)
                        {
                            if(mset.MgrValue=="1")
                            {
                                if (card.RegFlag != 1)
                                {
                                    strFlag = 5;//未办理IC卡
                                    return "该犯未办理银行卡签约，不能入账";
                                }
                            }
                        }                        

                        //获得系统最大奖金发放次数
                        T_SETTINGS sModel = new T_SETTINGSBLL().GetModelList("TYPE=606")[0];

                        string strUdate = Convert.ToString(bonus.udate);
                        string strWhere = " UDate='" + strUdate + "' and FCrimeCode='" + strFCode + "'";
                        int userCount = _jifenMgrService.GetModelList<T_JF_BONUSDTL>( jss.Serialize( new { FCRIMECODE = strFCode, udate = strUdate }),"Id asc",10).Count;
                        if (reSaveFlag == 0)//默认是不允许重复发放的
                        {
                            if (Convert.ToInt32(sModel.VALUE) <= userCount)
                            {
                                strFlag = 7;//超过每月最大发放次数
                                strRusult = strFCode + "," + strFName + ",超过每月最大发放次数:" + sModel.VALUE;
                            }
                            else
                            {//发放明细记录
                                StartAddBonusDetail(LoginUserName, strFCode, strFMoney, AmountA, AmountB, AmountC, strFBid, ref okInfo, ref strFlag, excelModel_Id, ref strRusult, cpctMoney, model, criminal, bonus, area, card);
                            }
                        }
                        else
                        {//发放明细记录
                            StartAddBonusDetail(LoginUserName, strFCode, strFMoney, AmountA, AmountB, AmountC, strFBid, ref okInfo, ref strFlag, excelModel_Id, ref strRusult, cpctMoney, model, criminal, bonus, area, card);
                        }
                        
                    }
                }
            }
            return strRusult;
        }

        private void StartAddBonusDetail(string LoginUserName, string strFCode, string strFMoney, decimal AmountA, decimal AmountB, decimal AmountC, string strFBid, ref string okInfo, ref int strFlag, int excelModel_Id, ref string strRusult, decimal cpctMoney, T_JF_BONUSDTL model, T_Criminal criminal, T_JF_BONUS bonus, T_AREA area, T_Criminal_card card)
        {
            //设定Model信息

            if (excelModel_Id == 1)
            {
                SetBonusDetailModel(LoginUserName, strFCode, strFMoney, strFBid, 0, 0, cpctMoney, model, criminal, bonus, area, card);

            }
            else
            {
                SetBonusDetailModel(LoginUserName, strFCode, strFMoney, strFBid, AmountA, AmountB, AmountC, model, criminal, bonus, area, card);

            }

            if (_jifenMgrService.Insert<T_JF_BONUSDTL>(model) !=null)
            {
                //更新主单积分及数量
                if (_jifenMgrService.UpdateBonusByCountMoney(strFBid))
                {
                    T_JF_BONUS nbonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strFBid);
                    okInfo = model.FCRIMECODE + "|" + model.fcriminal + "|" + model.fareaName + "|" + model.FAMOUNT.ToString() + "|" + model.AmountA.ToString() + "|" + model.AmountB.ToString() + "|" + model.AmountC.ToString() + "|" + bonus.Remark + "|" + nbonus.cnt.ToString() + "|" + nbonus.fAMOUNT.ToString();
                }
            }
            else
            {
                strFlag = 4;//明细单保存失败
                strRusult = "明细单保存失败";
            }
        }

        private static void SetBonusDetailModel(string LoginUserName, string strFCode, string strFMoney, string strFBid, decimal cpctMoneyA, decimal cpctMoneyB, decimal cpctMoneyC, T_JF_BONUSDTL model, T_Criminal criminal, T_JF_BONUS bonus, T_AREA area, T_Criminal_card card)
        {
            model.BID = strFBid;
            model.FCRIMECODE = strFCode;
            model.fcriminal = criminal.FName;
            model.CARDCODE = card.cardcodea;
            model.Frealareacode = "";
            model.FrealAreaName = "";
            model.fareacode = area.FCode;
            model.fareaName = area.FName;
            model.FAMOUNT = Convert.ToDecimal(strFMoney);
            model.AmountA = cpctMoneyA;
            model.AmountB = cpctMoneyB;
            model.AmountC = cpctMoneyC;
            model.cardtype = 0;
            model.acctype = 1;
            model.crtby = LoginUserName;
            model.crtdt = Convert.ToDateTime(bonus.crtdt);
            model.udate = Convert.ToDateTime(bonus.udate);
            model.ptype = bonus.Remark;
            model.remark = bonus.Remark;
            model.cqbt = 0;
            model.grkj = 0;
            model.gwjt = 0;
            model.ldjx = 0;
            model.tbbz = 0;
            model.FLAG = 0;
            model.vouno = "";
            model.applyby = "";
        }

        public ActionResult QueryCrimeCode()//查询账号
        {
            #region 查询犯人编号信息
            string strFCrimeCode = Request["FCode"];//编号
            T_Criminal model = new T_CriminalBLL().GetModel( strFCrimeCode);
            if (model != null)
            {
                T_AREA area = new T_AREABLL().GetModel(model.FAreaCode);
                List<T_Criminal> list = new T_CriminalBLL().GetCrimeBySearch(strFCrimeCode, "", area.FName, Session["loginUserCode"].ToString());
                if (list == null)
                {
                    return Content("OK." + model.FCode + "|" + model.FName + "|" + model.fflag + "|" + "-1" + "|" + area.FName + "的犯人，您无权管理");
                }
                else
                {
                    T_CY_TYPE cyType = new T_CY_TYPEBLL().GetModel(model.FCYCode);
                    return Content("OK." + model.FCode + "|" + model.FName + "|" + model.fflag + "|" + "1" + "|" + cyType.cpct.ToString());
                }
            }
            else
            {
                return Content("Error.编号不存在");
            }
            #endregion
        }

        public ActionResult AddOrderDetail()//增加明细记录
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string strReSaveFlag=Request["reSaveFlag"];//允许重复发放标志
            string strFCode = Request["FCode"];//编号
            string strFName = Request["FName"];//姓名
            string strFMoney = Request["FMoney"];//积分
            string strAmountA = Request["AmountA"];//积分
            string strAmountB = Request["AmountB"];//积分
            string strAmountC = Request["AmountC"];//积分

            strFMoney = string.IsNullOrEmpty(strAmountA) == true ? "0" : strFMoney;
            decimal AmountA = string.IsNullOrEmpty(strAmountA) == true ? 0 : Convert.ToDecimal(strAmountA);
            decimal AmountB = string.IsNullOrEmpty(strAmountA) == true ? 0 : Convert.ToDecimal(strAmountB);
            decimal AmountC = string.IsNullOrEmpty(strAmountA) == true ? 0 : Convert.ToDecimal(strAmountC);
            if(AmountA+AmountB+AmountC!=0){
                strFMoney=Convert.ToString(AmountA+AmountB+AmountC);
            }
            string strFBid = Request["FBid"];//主单号
            try 
            {
                Decimal myMoney=Convert.ToDecimal(strFMoney);
            }
            catch
            {
                return Content("Error.积分必须是数值的");
            }

            int reSaveFlag = 0;
            if (string.IsNullOrEmpty(strReSaveFlag) == true)
            {
                reSaveFlag = 0;
            }
            else
            {
                reSaveFlag = Convert.ToInt16(strReSaveFlag);
            }
            T_JF_BONUS tbonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strFBid);
            if (tbonus == null)
            {
                return Content("Error.主单" + strFBid + "不存在");
            }
            if (tbonus.FCheckFlag == 1)
            {
                return Content("Error.主单" + strFBid + "已经确认提交不可再录入");
            }
            if (tbonus.FLAG == 1)
            {
                return Content("Error.主单" + strFBid + "已经入账不可再录入");
            }
            string okInfo;
            int strFlag;
            //检测并增加一条记录（劳动报酬）
            string strResult = CheckAndAddRecord(strLoginName, Session["loginUserCode"].ToString(), strFCode, strFName, strFMoney, AmountA, AmountB, AmountC, strFBid, out okInfo, out strFlag, reSaveFlag);
            if (strResult == "")
            {
                return Content("OK." + okInfo);
            }
            else
            {
                return Content("Error." + strResult);
            }
        }


        public ActionResult PrintReportList()//打印报表
        {
            string strbid = Request["bid"];
            List<T_JF_BONUSDTL> lists = _jifenMgrService.QueryList<T_JF_BONUSDTL>("bid='"+ strbid +"'");
            ViewData["lists"] = lists;

            T_JF_BONUS bonus = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid", strbid );
            ViewData["bonus"] = bonus;

            int excelModel_Id = 1;//默认的劳酬格式
            T_SHO_ManagerSet excelSet = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            if (excelSet != null)
            {
                excelModel_Id = Convert.ToInt32(excelSet.MgrValue);
            }

            ViewData["excelModel_Id"] = excelModel_Id;

            return View();
        }


        //批量生成劳报记录
        public ActionResult CreateAreaList()
        {
            string strbid = Request["sbid"];
            T_JF_BONUS bon = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",strbid);
            if(bon!=null)
            {
                if(bon.FCheckFlag==1)
                {
                    return Content("Err|主单已确认，不能再增加");
                }
            }
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if(_jifenMgrService.BatchCreateAreaList(strbid,strLoginName))
            {
                List<T_JF_BONUSDTL> lists = _jifenMgrService.QueryList<T_JF_BONUSDTL>("bid='" + strbid + "'");
                return Content("OK|"+jss.Serialize(lists));
            }
            return Content("Err|创建失败");

        }

        //批量保存Dtl记录
        public ActionResult BatchSaveDtlList()
        {
            //Request.("UTF-8");
            //取编辑数据 这里获取到的是json字符串

            string deleted = Request["deleted"];

            string inserted = Request["inserted"];

            string updated = Request["updated"];


            if (updated != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(updated);
                string bid = ja[0]["BID"].ToString();
                T_JF_BONUS bon = _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",bid);
                if(bon!=null)
                {
                    if(bon.FCheckFlag!=1)
                    {
                        if (ja.Count > 0)
                        {
                            T_JF_BONUS model = DoBatchDtlInfoAdd(ja);
                            
                            return Content("OK|"+jss.Serialize(model));
                        }
                    }
                    else
                    {
                        return Content("Err|已确认，不能再保存成功！");
                    }
                }
                
            }


            return Content("");
        }

        //劳酬主单状态状操作
        public ActionResult undoYuanYingTJ(int id=1)
        {
            string undoAction = Request["undoAction"];
            string undoRemark = Request["undoRemark"];//取消备注的原因
            string strBid = Request["Bid"];
            if(string.IsNullOrEmpty(undoAction))
            {
                return Content("Err|执行的退回动作不能为空");
            }
            if (string.IsNullOrEmpty(strBid))
            {
                return Content("Err|主单号不能为空");
            }
            if (string.IsNullOrEmpty(undoRemark))
            {
                return Content("Err|请填写退回的原因");
            }
            T_JF_BONUS bonus = _jifenMgrService.GetModelFirst<T_JF_BONUS>( jss.Serialize(new { BID = strBid}));
            if(bonus==null)
            {
                return Content("Err|找不到该主单号的信息");
            }
            if (bonus.FLAG == 1)
            {
                return Content("Err|该主单已经成功入账了，不能做退回操作");
            }

            bonus.FPostFlag = 0;
            bonus.FPostDate = DateTime.Today;
            bonus.FPostBy = "";

            bonus.FLAG = 0;
            bonus.Frealareacode = "";
            switch(undoAction)
            {
                case "undoSubmit":
                    {
                        if (bonus.FCheckFlag == 0)
                        {
                            return Content("Err|该主单未提交了，不用做退回操作");
                        }
                        if (bonus.auditflag == 1)
                        {
                            return Content("Err|该主单已经审核了，不能做退回操作");
                        }
                        bonus.FCheckFlag = 0;
                        bonus.FrealAreaName = "（生产科）" + undoRemark;

                        bonus.auditflag = 0;
                        bonus.auditdate = DateTime.Today;
                        bonus.auditby = "";

                        bonus.Fdbcheckflag = 0;
                        bonus.Fdbcheckdate = DateTime.Today;
                        bonus.FDbCheckBY = "";

                        bonus.Fdbcheckflag = 0;
                        bonus.Fdbcheckdate = DateTime.Today;
                        bonus.FDbCheckBY = "";

                    }break;
                case "undoAudit":
                    {
                        if (bonus.auditflag == 0)
                        {
                            return Content("Err|该主单未审核了，不用做退回操作");
                        }
                        if (bonus.Fdbcheckflag == 1)
                        {
                            return Content("Err|该主单已经复核了，不能做退回操作");
                        }
                        bonus.FCheckFlag = 1;
                        bonus.auditflag = 0;
                        bonus.FrealAreaName = "（财务科）" + undoRemark;

                        bonus.Fdbcheckflag = 0;
                        bonus.Fdbcheckdate = DateTime.Today;
                        bonus.FDbCheckBY = "";

                        bonus.Fdbcheckflag = 0;
                        bonus.Fdbcheckdate = DateTime.Today;
                        bonus.FDbCheckBY = "";
                    } break;
                case "undoDbCheck":
                    {
                        if (bonus.Fdbcheckflag == 0)
                        {
                            return Content("Err|该主单未复核了，不用做退回操作");
                        }
                        bonus.FCheckFlag = 1;
                        bonus.auditflag = 1;
                        bonus.Fdbcheckflag = 0;
                        bonus.FrealAreaName = "（出纳员）" + undoRemark;

                        bonus.Fdbcheckflag = 0;
                        bonus.Fdbcheckdate = DateTime.Today;
                        bonus.FDbCheckBY = "";
                    } break;
                default:
                    {
                        return Content("Err|不正确的操作类型");
                    }                    
            }
            bonus.FPostDate = DateTime.Now;
            if(_jifenMgrService.Update(bonus))
            {
                return Content("OK|" + jss.Serialize(bonus));
            }
            else
            {
                return Content("Err|执行退回时失败");
            }
        }
        //执行限量DTL写入操作
        private  T_JF_BONUS DoBatchDtlInfoAdd(JArray ja)
        {
            string bid = "";
            foreach (JObject o in ja)
            {
                T_JF_BONUSDTL m = _jifenMgrService.GetModel<T_JF_BONUSDTL>(Convert.ToInt32(o["seqno"].ToString()));
                if (m != null)
                {
                    m.FAMOUNT = Convert.ToDecimal( o["FAMOUNT"].ToString());
                    //获取处遇信息
                    T_Criminal criminal = new T_CriminalBLL().GetModel(o["FCRIMECODE"].ToString());
                    T_CY_TYPE cy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
                    m.AmountC = m.FAMOUNT * (decimal)cy.cpct / 100;
                    bool b = _jifenMgrService.Update(m);
                    bid = m.BID;
                }
            }
            _jifenMgrService.UpdateBonusByCountMoney(bid);
            //List<T_BONUSDTL> models = new T_BONUSDTLBLL().GetModelList("Bid='"+ bid + "'");

            return _jifenMgrService.QueryModel<T_JF_BONUS>("Bid",bid); ;
        }

        
        

	}

}