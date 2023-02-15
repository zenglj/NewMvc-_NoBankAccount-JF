using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [CustomActionFilterAttribute]
    [MyLogActionFilterAttribute]
    public class CashPayController : Controller
    {
        //
        // GET: /CashPay/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        private int auditFlag ;
        
        public ActionResult Index(int id=1)
        {
            
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;
            List<T_Savetype> saveTypes=new List<T_Savetype>();
            if (id == 1)//存款
            {
                saveTypes = new T_SavetypeBLL().GetModelList("typeFlag=0");
            }
            else if (id == 2)//取款
            {
                saveTypes = new T_SavetypeBLL().GetModelList("typeFlag=1");
            }
            ViewData["saveTypes"] = saveTypes;

            

            ViewData["saveTypeId"] = id;
            return View();
        }

        //获取存取款是否要审核的标志
        private void GetCashPayAduitFlag()
        {
            T_SHO_ManagerSet kkAuditMset = new T_SHO_ManagerSetBLL().GetModel("YibanQukouKuanShenhe_Flag");
            if (kkAuditMset != null)
            {
                if (kkAuditMset.MgrValue == "1")
                {
                    auditFlag = -1;//如果需要审核则T_Vcrd把这个BankFlag标志设为-1
                }
            }
        }
        public ActionResult GetUserInfo()
        {
            string strLoginName = Session["loginUserCode"].ToString();

            string strRows = Request["rows"];
            string strPage = Request["page"];
            int rows = 5,page = 1;
            if(string.IsNullOrEmpty(strRows)==false)
            {
                rows = Convert.ToInt32(strRows);
            }
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }

            string strFCode = Request["FCode"];
            string strFName = Request["FName"];
            string strAreaName= Request["AreaName"];
            string strFFlag = Request["FFlag"];


            StringBuilder strSql = GetStrSqlWhere(strFCode, strFName, strAreaName, strFFlag, strLoginName);
            List<T_UserInfoExt> lists=new T_CriminalBLL().GetUserInfo(page,rows,strSql.ToString());
            int listRows = new T_CriminalBLL().GetUserInfoCount(strSql.ToString());

            string sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(lists) + "}";
            return Content(sss);
        }

        public ActionResult GetFcrimeInfo()
        {
            string strFCode = Request["FCode"];
            T_Criminal list = new T_CriminalBLL().GetCriminalXE_info(strFCode, 1);
            if (list.ErrInfo != "")
            {
                return Content("Err|" + list.ErrInfo + "，请与管理人员联系");
            }

            if(list==null)
            {
                return Content("Err|找不到该人员信息");
            }
            return Content("OK|"+ jss.Serialize(list));
        }

        public ActionResult GetFNameInfo()
        {
            string strFName = Request["FName"];
            List<T_Criminal> list = new T_CriminalBLL().GetModelList("FName = '" + strFName + "' and isnull(FFlag,0)=0 ");
            if (list.Count == 0)
            {
                return Content("Err|找不到该人员信息");
            }
            else if (list.Count > 1)
            {
                return Content("Err|您输入的姓名不是唯一的，请改用编号");
            }
            return Content("OK|" + jss.Serialize(list[0]));
        }
        private static StringBuilder GetStrSqlWhere(string strFCode, string strFName, string strAreaName, string strFFlag, string strLoginName)
        {

            StringBuilder strSql = new StringBuilder();
            bool whereFlag = false;
            if (string.IsNullOrEmpty(strFCode) == false)
            {
                if (whereFlag)
                {
                    strSql.Append(" and FCode='" + strFCode + "'");
                }
                else
                {
                    strSql.Append(" FCode='" + strFCode + "'");
                }
                whereFlag = true;
            }
            if (string.IsNullOrEmpty(strFName) == false)
            {
                if (whereFlag)
                {
                    strSql.Append(" and FName like '%" + strFName + "%'");
                }
                else
                {
                    strSql.Append(" FName like '%" + strFName + "%'");
                }
                whereFlag = true;
            }
            if (strAreaName == "==请选择==")
            {
                strAreaName = "";
            }
            if (string.IsNullOrEmpty(strAreaName) == false)
            {
                if (whereFlag)
                {
                    strSql.Append(" and FAreaName ='" + strAreaName + "'");
                }
                else
                {
                    strSql.Append(" FAreaName ='" + strAreaName + "'");
                }
                whereFlag = true;
            }
            //状态
            if (string.IsNullOrEmpty(strFFlag) == false)
            {
                if (whereFlag)
                {
                    strSql.Append(" and FFlag ='" + strFFlag + "'");
                }
                else
                {
                    strSql.Append(" FFlag ='" + strFFlag + "'");
                }
                whereFlag = true;
            }
            if (string.IsNullOrEmpty(strLoginName) == false)
            {

                if (whereFlag)
                {
                    strSql.Append(" and FAreaCode in (select fareacode from t_czy_area where fflag=2 and fcode ='" + strLoginName + "')");
                }
                else
                {
                    strSql.Append(" FAreaCode in (select fareacode from t_czy_area where fflag=2 and fcode ='" + strLoginName + "')");
                }
                whereFlag = true;
            }
            return strSql;
        }

        public ActionResult SavePayDetail(int id = 1)
        {
            string strFCode = Request["FCode"];
            string strFName = Request["FName"];
            string strDType = Request["DType"];
            string strFMoney = Request["FMoney"];
            string strApply = Request["Apply"];
            string strRemark = Request["Remark"];
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            //获取存取款是否要审核的标志
            GetCashPayAduitFlag();

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(strFCode, 7);
            if (criminal.ErrInfo != "")
            {
                return Content("Err|" + criminal.ErrInfo + "，请与管理人员联系");
            }
            decimal dongjeJinE = 0;
            #region 验证用户输入的正确性与否
            if (string.IsNullOrEmpty(strApply))
            {
                //不做必输项了
                //return Content("Err|申请人不能为空");
                strApply = "";
            }
            if (string.IsNullOrEmpty(strFCode))
            {
                return Content("Err|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(strFName))
            {
                return Content("Err|用户姓名不能为空");
            }
            if (criminal == null)
            {
                return Content("Err|用户不存在");
            }
            if (criminal.FName != strFName)
            {
                return Content("Err|用户姓名与编号不一致");
            }
            if (criminal.fflag == 1)
            {
                return Content("Err|用户已经离监");
            }
            try
            {
                if (criminal.flimitamt == null)
                {
                    criminal.flimitamt = 0;
                }
                if (criminal.flimitflag == null)
                {
                    criminal.flimitamt = 0;
                }
                dongjeJinE = criminal.flimitamt * criminal.flimitflag;
            }
            catch
            {

            }

            #endregion

            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(strFCode);
            if (card == null)
            {
                return Content("Err|用户没有办理IC卡，不能做存取款操作");
            }
            //flag 是存扣款的标志，1是存款，-1是扣款
            List<T_Savetype> savetypes = new List<T_Savetype>();
            int savePayFlag = 1;
            if (id == 1)
            {
                savetypes = new T_SavetypeBLL().GetModelList("typeflag=0 and FCode=" + strDType);
                savePayFlag = 1;
            }
            else if (id == 2)
            {
                savetypes = new T_SavetypeBLL().GetModelList("typeflag=1 and FCode=" + strDType);
                if (savetypes.Count < 0)
                {
                    return Content("Err|你传入的扣款类型不存在，请核实");
                }

                //增加判断留存账户是否可用的，默认是可用的，0是不可用，1是可用
                T_SHO_ManagerSet fmset = new T_SHO_ManagerSetBLL().GetModel("LiucunJineKeyongBiaozhi");
                if(fmset!=null && fmset.MgrValue == "0")
                {
                    if (card.AmountA + card.AmountB - (dongjeJinE) < Convert.ToDecimal(strFMoney))
                    {
                        if (savetypes[0].FuShuFlag == 0)//判断是否可以透支
                        {
                            //2020年改为：可以扣二个账户
                            if (card.AmountA + card.AmountB > Convert.ToDecimal(strFMoney))
                            {
                                return Content("Err|账户余额不足,有冻结金额:" + dongjeJinE.ToString() + "，不可用");
                            }
                            return Content("Err|账户余额不足(不含留存)");
                        }
                    }
                }
                

                    //验证金额是否够扣
                if (card.AmountA + card.AmountB + card.AmountC - (dongjeJinE) < Convert.ToDecimal(strFMoney))
                {
                    if (savetypes[0].FuShuFlag == 0)//判断是否可以透支
                    {
                        //2020年改为：可以扣三个账户
                        if (card.AmountA + card.AmountB + card.AmountC > Convert.ToDecimal(strFMoney))
                        {
                            return Content("Err|账户余额不足,有冻结金额:" + dongjeJinE.ToString() + "，不可用");
                        }
                        return Content("Err|账户余额不足");
                    }
                }
                //验证金额是否够扣,（批量消费扣款只能用A+B账户）
                if (savetypes[0].PLXE_Flag == 1)//判断是否只能用A+B（批量消费扣款只能用A+B账户）
                {
                    if (card.AmountA + card.AmountB < Convert.ToDecimal(strFMoney))
                    {
                        return Content("Err|批量消费扣款的A+B账户余额不足,有冻结金额:" + dongjeJinE.ToString() + "，不可用");
                    }
                }

                //如果批量限额标志为1（真）,则判断可消费余额是否有够
                string checkResutl = "";
                checkResutl = checkXiaoFeiEdu(strFMoney, criminal, savetypes[0].PLXE_Flag, checkResutl);

                if (checkResutl != "")
                {
                    return Content("Err|超出本月最大限额，可消费余额不足");
                }
                savePayFlag = -1;
            }
            else
            {
                return Content("Err|你传的是错误的参数");
            }

            List<T_Vcrd> vcrd = new T_VcrdBLL().UserCunKouKuan(strFCode, savePayFlag, Convert.ToDecimal(strFMoney), savetypes[0], strLoginName, strRemark, strApply, "", auditFlag);

            //strFCode;
            List<T_UserInfoExt> users = new T_CriminalBLL().GetUserInfo("FCode='" + strFCode + "'");

            return Content("OK|" + jss.Serialize(vcrd) + "|" + jss.Serialize(users[0]));

        }

        private static string checkXiaoFeiEdu(string strFMoney, T_Criminal criminal, int? PLXE_Flag, string checkResutl)
        {
            if (PLXE_Flag == null)
            {
                PLXE_Flag = 0;
            }
            if (PLXE_Flag == 1)
            {
                if (criminal.NoXiaofeimoney < Convert.ToDecimal(strFMoney))
                {

                    checkResutl = "Err|超出本月最大限额，可消费余额不足";
                }
            }
            return checkResutl;
        }

        //删除扣款记录
        [MyLogActionFilterAttribute]
        public ActionResult DelDetailList(string FCode, string seqno, int id = 1)
        {
            //strUserLoginName 用户名称
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            return DelCashPayDetail(FCode, seqno, strLoginName, id);
        }

        
        private ActionResult DelCashPayDetail(string FCode, string seqno, string strLoginName, int id)
        {
            //string FCode = Request["FCode"];
            //string seqno = Request["seqno"];
            //string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;


            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(seqno))
            {
                return Content("Err|存/取款记录号不能为空");
            }

            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(FCode);
            if (card == null)
            {
                return Content("Err|用户IC卡账户不存在，无法操作");
            }
            if (card.cardflaga == 4)
            {
                return Content("Err|用户IC卡已经离监或是停止，不能删除");
            }

            T_Vcrd vcrd = new T_VcrdBLL().GetModel(Convert.ToInt32(seqno));



            if (vcrd == null)
            {
                return Content("Err|" + seqno + ",该存/取款记录号不存在");
            }
            if (vcrd.FCrimeCode != FCode)
            {
                return Content("Err|存/取款记录号:" + seqno + "的用户编号与" + FCode + "不一致，不能删除");
            }
            string strYM = "";
            if (vcrd.CrtDate.Month < 10)
            {
                strYM = vcrd.CrtDate.Year.ToString() + "0" + vcrd.CrtDate.Month.ToString();
            }
            else
            {
                strYM = vcrd.CrtDate.Year.ToString() + vcrd.CrtDate.Month.ToString();
            }
            AC_SUBYR ac = new AC_SUBYRBLL().GetModel(strYM);
            if (ac == null)
            {
                return Content("Err|请先增加新年度的财务期间，不能删除");
            }
            if (ac.AccStatus == "关")
            {
                return Content("Err|财务期间已经关闭，不能删除");
            }
            if (vcrd.BankFlag == null)
            {
                vcrd.BankFlag = 0;
            }
            if (vcrd.BankFlag != 0)
            {
                return Content("Err|该存/取款记录号已经发送给银行处理了，不能删除");
            }
            if (id == 1)
            {

                switch (vcrd.AccType)
                {
                    case 0:
                        {
                            if (vcrd.DAmount > card.AmountA && vcrd.Flag == 0)
                            {
                                return Content("Err|A账户资金已经被消费，余额不足，不能删除");
                            }
                        }
                        break;
                    case 1:
                        {
                            if (vcrd.DAmount > card.AmountB && vcrd.Flag == 0)
                            {
                                return Content("Err|B账户资金已经被消费，余额不足，不能删除");
                            }
                        }
                        break;
                    case 2:
                        {
                            if (vcrd.DAmount > card.AmountC && vcrd.Flag==0)
                            {
                                return Content("Err|C账户资金已经被消费，余额不足，不能删除");
                            }
                        }
                        break;
                    default:
                        {
                            return Content("Err|Vcrd记的账户类型AccType不正确，不能删除");
                        }
                }
            }

            if (new T_VcrdBLL().SoftDeleteVcrd(FCode, Convert.ToInt32(seqno), strLoginName))
            {
                List<T_UserInfoExt> users = new T_CriminalBLL().GetUserInfo("FCode='" + FCode + "'");

                Log4NetHelper.logger.Warn("删除存取款记录,操作员：" + Session["loginUserName"].ToString() + ",删除信息 ID=" + vcrd.seqno + ",录入员：" + vcrd.CrtBy + ",流水号为：" + vcrd.Vouno + ",记录创建日期:" + vcrd.CrtDate.ToString() + ",类型为：" + vcrd.DType + ",金额:" + (vcrd.DAmount + vcrd.CAmount).ToString() + ",摘要:" + vcrd.Remark);

                return Content("OK|删除存/取款成功|" + jss.Serialize(users[0]));
            }
            else
            {
                return Content("Err|删除存/取款失败");
            }
        }

        //打印存取款记录
        public ActionResult PrintSavePayList(int id=1)
        {
            string savePayFlag = Request["savePayFlag"];
            //if (string.IsNullOrEmpty(savePayFlag))
            //{
            //    return Content("Err|打印的存取款类型不能为空");
            //}
            string seqno = Request["seqno"];
            
            if (string.IsNullOrEmpty(seqno))
            {
                return Content("Err|存取款流水记录号不能为空");
            }
            T_Vcrd vcrd = new T_VcrdBLL().GetModel(Convert.ToInt32(seqno));
            ViewData["vcrd"] = vcrd;
            string savePayTitle = "";
            switch(id)
            {
                case 1:
                    {
                        savePayTitle = "用户存款记录单";
                        
                    }break;
                case 2: 
                    {
                    savePayTitle = "用户取款记录单";
                    } break;
                default:
                    savePayTitle = "存/取款记录单";
                    break;
            }
            ViewData["savePayTitle"] = savePayTitle;
            return View();
        }
        public ActionResult getVcrds(int id=1)
        {
            string strFCode = Request["FCode"];
            List<T_Vcrd> vcrds = new List<T_Vcrd>();
            if (id == 1)
            {
                vcrds = new T_VcrdBLL().GetModelList("FCrimecode='" + strFCode + "' and Flag in (0,-2) and TypeFlag=0");

            }
            else if (id == 2)
            {
                vcrds = new T_VcrdBLL().GetModelList("FCrimecode='" + strFCode + "' and Flag in (0,-2) and TypeFlag=2");

            }
            return Content(jss.Serialize(vcrds));

        }

        public ActionResult IndexExcelDR(int id=1)
        {
            List<T_Savetype> saveTypes = new List<T_Savetype>();
            if(id==1)
            {
                saveTypes = new T_SavetypeBLL().GetModelList("typeflag=0");
            }
            else if(id==2)
            {
                saveTypes = new T_SavetypeBLL().GetModelList("typeflag=1");

            }


            ViewData["saveTypes"] = saveTypes;
            ViewData["saveTypeId"] = id;
            return View();
        }
        public ActionResult ExcelInport(int id=1)//Excel表格导入
        {
            string saveTypeId = Request["saveTypeId"];
            string onlyCheckFlag = Request["onlyCheckFlag"];
            if(string.IsNullOrEmpty(saveTypeId))
            {
                return Content("Err|存取标志的id不能为空");
            }
            id = Convert.ToInt32(saveTypeId);
            string strLoginName = "";
            try {
                strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            }            
            catch{
                Redirect("/Admin/Index");
            }

            //获取存取款是否要审核的标志
            GetCashPayAduitFlag();

            if (Request.Files.Count > 0)
            {
                
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

                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
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
                    int ErrNums = 0;
                    if (rows < 2)
                    {
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        string strDTypeCode = Request["selSaveType"];
                        string strDTypeName = Request["selSaveName"];                        
                        int FMoneyInOutFlag = 0;
                        List<T_Savetype> subSaveTypes=new List<T_Savetype>(); 
                        T_Savetype subSaveType = new T_Savetype();
                        if(id==1)
                        {
                            FMoneyInOutFlag = 1;
                            subSaveTypes = new T_SavetypeBLL().GetModelList(" TypeFlag=0 and FName='" + strDTypeName + "' and FCode=" + strDTypeCode + "");

                        }
                        else if (id == 2)
                        {
                            FMoneyInOutFlag = -1;
                            subSaveTypes = new T_SavetypeBLL().GetModelList(" TypeFlag=1 and FName='" + strDTypeName + "' and FCode=" + strDTypeCode + "");
                        }
                        if (subSaveTypes.Count <= 0)
                        {
                            return Content("Err|您选择的批扣类型不存在请再次确认");
                        }
                        subSaveType = subSaveTypes[0];

                        

                        //string strFBid = Request["excelBid"];
                        string strFBid = new T_BONUSBLL().CreateOrderId("PLK");//批量扣款主单号
                        
                        //首先创建一个主单
                        T_BatchMoneyTrade trade = new T_BatchMoneyTrade();
                        trade.Bid = strFBid;
                        trade.FCourseCode =Convert.ToInt32( strDTypeCode);
                        trade.FCourseType = strDTypeName;
                        trade.FMoneyInOutFlag = FMoneyInOutFlag;
                        trade.FrealAreaCode = "";
                        trade.FAmount = 0;
                        if (onlyCheckFlag == "0")
                        {
                            trade.Remark = "WEB批量导入";
                        }
                        else
                        {
                            trade.Remark = "WEB批量校验";
                        }
                        trade.ApplyBy = strLoginName;
                        trade.Applydt = DateTime.Today;
                        trade.Crtby = strLoginName;
                        trade.crtdt = DateTime.Today;
                        trade.CheckBy = "";
                        trade.CheckDate = DateTime.Today;
                        trade.Flag = 0;
                        trade.FAreaCode = "";
                        trade.FAreaName = "";
                        trade.UDate = DateTime.Today;
                        trade.PType = "";
                        trade.cnt = 0;
                        trade.AuditBy = "";
                        trade.AuditFlag = 0;
                        trade.AuditDate = DateTime.Today;
                        trade.FDbCheckBY = "";
                        trade.FdbCheckDate = DateTime.Today;
                        trade.FdbCheckFlag = 0;
                        trade.FPostBy = "";
                        trade.FPostDate = DateTime.Today;
                        trade.FPoestFlag = 0;
                        trade.FrealAreaCode = "";
                        trade.FrealAreaName = "";
                        new T_BatchMoneyTradeBLL().Add(trade);


                        #region 批量导入判断写入的方式 --2020-03-01 起， 开始使用

                        DataTable dtUserAdd = new DataTable();
                        //获取导入的Excel数据
                        AddExcel_MulRowCheckMode(sheet, rows, dtUserAdd);
                        //执行存储过程
                        string result = new T_BatchMoneyTradeBLL().PLExcelImport(strFBid, onlyCheckFlag);
                        #endregion
                        

                        //#region 单条判断写入的方式 --2020-03-01 注释停用
                        ////逐条验证模式 2020-03-01之前的模式
                        //ErrNums = AddExcel_SingleCheckMode(id, onlyCheckFlag, strLoginName, sheet, rows, ErrNums, subSaveType, strFBid);

                        //#endregion

                        

                        rtnTrades rtn = new rtnTrades();
                        rtn.trade = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                        rtn.dtls = new T_BatchMoneyTrade_DTLBLL().GetModelList("Bid='" + strFBid + "'");
                        DataTable dt = new CommTableInfoBLL().GetDataTable("select isnull(sum(famount),0) fmoney from T_BatchMoneyTrade_DTL where Bid='" + strFBid + "'");
                        
                         
                        DataTable dtErrs = new CommTableInfoBLL().GetDataTable("select isnull(count(1),0) from T_BatchMoneyTrade_ErrList where pc='" + strFBid + "'");

                        ErrNums = Convert.ToInt16(dtErrs.Rows[0][0].ToString());
                        
                        decimal allMoney = 0;
                        if (dt.Rows.Count > 0)
                        {
                            allMoney = Convert.ToDecimal(dt.Rows[0][0].ToString());
                        }
                        if (onlyCheckFlag == "0")
                        {
                            //如果是导入数据，则清空之前的所有校验数据
                            string myDelsql = "delete from T_BatchMoneyTrade_ErrList where pc in(select bid from T_BatchMoneyTrade where remark='WEB批量校验') and pc<>'" + strFBid + "';delete from T_BatchMoneyTrade_Dtl where bid in(select bid from T_BatchMoneyTrade where remark='WEB批量校验')  and bid<>'" + strFBid + "';delete from T_BatchMoneyTrade where remark='WEB批量校验'  and bid<>'" + strFBid + "';";
                            new CommTableInfoBLL().ExecSql(myDelsql);
                        }

                        if (ErrNums > 0)
                        {
                            if(onlyCheckFlag=="0")
                            {
                                return Content("OK|导入完成,成功金额：" + allMoney.ToString() + "，失败记录：" + ErrNums.ToString() + "条|" + jss.Serialize(rtn));
                            }
                            else
                            {

                                return Content("OK|校验完成,成功金额：" + allMoney.ToString() + "，失败记录：" + ErrNums.ToString() + "条|" + jss.Serialize(rtn));
                            }
                        }
                        else
                        {
                            if (onlyCheckFlag == "0")
                            {
                                return Content("OK|导入完成|" + jss.Serialize(rtn));
                            }
                            else
                            {
                                return Content("OK|校验完成，金额：" + allMoney.ToString() + "|" + jss.Serialize(rtn));
                            }
                        }
                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }

        private static void AddExcel_MulRowCheckMode(NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd)
        {
            #region 定义DataTable

            dtUserAdd.Columns.Add(new DataColumn("id", typeof(int)));
            dtUserAdd.Columns.Add(new DataColumn("FCode", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FMoney", typeof(decimal)));
            dtUserAdd.Columns.Add(new DataColumn("FRemark", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("Notes", typeof(string)));
            DataRow drTemp = null;
            #endregion

            for (int i = 1; i <= rows; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                NPOI.SS.UserModel.CellType iType;
                try
                {
                    iType = row.GetCell(0).CellType;
                }
                catch
                {
                    continue;
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
                string FMoney = "0";  //金额
                try
                {
                    FMoney = Convert.ToString(row.GetCell(2).NumericCellValue);
                }
                catch { }

                string FRemark = "";  //开始日期
                try
                {
                    FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                }
                catch { }

                //写入行
                drTemp = SetDatarowInfo(dtUserAdd, drTemp, i, FCode, FName, FMoney, FRemark);

            }
            //将Excel写入到数据库中
            string StrSql = "";
            StrSql=(@"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[t_tmpMoneyList]') AND type in (N'U'))
                DROP TABLE [t_tmpMoneyList]; ");
            new CommTableInfoBLL().ExecSql(StrSql.ToString());
            StrSql=(@"CREATE TABLE [dbo].[t_tmpMoneyList](
	                [id] [int] IDENTITY(1,1) NOT NULL,
	                [fcode] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                [fname] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	                [fmoney] [numeric](18,2) NOT NULL,
	                [fremark] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                [Notes] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL
                ) ON [PRIMARY] ;");

            new CommTableInfoBLL().ExecSql(StrSql.ToString());
            string strAddResult = new CommTableInfoBLL().AddDataTableToDB(dtUserAdd, "[t_tmpMoneyList]");
        }

        private int AddExcel_SingleCheckMode(int id, string onlyCheckFlag, string strLoginName, NPOI.SS.UserModel.ISheet sheet, int rows, int ErrNums, T_Savetype subSaveType, string strFBid)
        {
            for (int i = 1; i <= rows; i++)
            {
                NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                NPOI.SS.UserModel.CellType iType;
                try
                {
                    iType = row.GetCell(0).CellType;
                }
                catch
                {
                    continue;
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
                string FMoney = "0";  //金额
                try
                {
                    FMoney = Convert.ToString(row.GetCell(2).NumericCellValue);
                }
                catch { }

                string FRemark = "";  //开始日期
                try
                {
                    FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                }
                catch { }


                //2020-01-14注销下面的方法，改批量验证方式
                string okInfo;
                int strFlag;
                //检测并增加一条记录
                //增加 auditFlag 用于区分 一般取款是否为需要审核才能到银行，如要auditFlag=-1,如不要aduitFlag=0
                //               用于对应T_Vcrd表中的 checkFlag字段
                string strDoResult = CheckAndAddRecord(Convert.ToInt32(onlyCheckFlag), auditFlag, id, subSaveType, FRemark, strLoginName, Session["loginUserCode"].ToString(), FCode, FName, FMoney, strFBid, out okInfo, out strFlag);
                //string strDoResult="";
                #region 判断处理情况
                //switch (strFlag)
                //{
                //    case 0://成功
                //        { strDoResult="";} break;
                //    case 1://犯人信息不正确或是无权管理
                //        { strDoResult = "犯人信息不正确或是无权管理"; 
                //        } break;
                //    case 2://犯人已经离监结算了
                //        { strDoResult = "犯人已经离监结算了"; 
                //        } break;
                //    case 3://主单号不存在
                //        { strDoResult = "主单号不存在"; 
                //        } break;
                //    case 4://明细单保存失败
                //        {
                //            strDoResult = "明细单保存失败";
                //        } break;
                //    case 5: //未办理IC卡
                //        {
                //            strDoResult = "未办理IC卡";
                //        } break;
                //    default:
                //        break;
                //} 
                #endregion
                if (strDoResult != "")
                {
                    T_BatchMoneyTrade_ErrList importList = new T_BatchMoneyTrade_ErrList();
                    importList.ImportType = 4;
                    importList.fcrimecode = FCode;
                    importList.fname = FName;
                    importList.Amount = Convert.ToDecimal(FMoney);
                    importList.Crtdt = DateTime.Now;
                    importList.CrtBy = strLoginName;
                    importList.Remark = FRemark;
                    importList.pc = strFBid;
                    importList.notes = strDoResult;
                    //插入失败记录
                    new T_BatchMoneyTrade_ErrListBLL().Add(importList);
                    ErrNums++;
                }

                //========注销结束========

            }
            return ErrNums;
        }

        private static DataRow SetDatarowInfo(DataTable dtUserAdd, DataRow drTemp, int i, string FCode, string FName, string FMoney, string FRemark)
        {
            #region Excel行写入到DataTabel中
            try
            {//如果金额有
                if (FCode != "")
                {
                    drTemp = dtUserAdd.NewRow();
                    drTemp["id"] = i;
                    drTemp["FCode"] = FCode;
                    drTemp["FName"] = FName;
                    drTemp["FMoney"] = FMoney;
                    drTemp["FRemark"] = FRemark;
                    drTemp["Notes"] = "";

                    dtUserAdd.Rows.Add(drTemp);
                }

            }
            catch
            { }
            #endregion
            return drTemp;
        }

        public ActionResult ExcelOutSucList(int id=1)//Excel导出成功记录
        {
            string strFBid = Request["strFBid"];
            if(string.IsNullOrEmpty(strFBid)==false)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select fcrimecode 编号,fcriminal 姓名,FAmount 金额,Remark 备注 from T_BatchMoneyTrade_dtl ");
                strSql.Append(" where Bid='" + strFBid + "'");
                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                string strFileName = new CommonClass().GB2312ToUTF8("CashPaySucList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); 
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                //ExcelRender.RenderToExcel(dt, strFileName);
                ExcelRender.RenderToExcel(dt, "成功记录清单", 2, strFileName);
                return Content("OK|CashPaySucList.xls");
            }
            else
            {
                return Content("Err|批次单不能为空");
            }
        }



        public ActionResult ExcelOutErrList(int id = 1)//Excel导出失败记录
        {
            string strFBid = Request["strFBid"];
            if (string.IsNullOrEmpty(strFBid) == false)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select fcrimecode 编号,fname 姓名,Amount 金额,Remark 备注,notes 错误原因 from T_BatchMoneyTrade_ErrList ");
                strSql.Append(" where pc='"+ strFBid +"'");
                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                string strFileName = new CommonClass().GB2312ToUTF8("CashPayErrList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                ExcelRender.RenderToExcel(dt, "错误记录清单",2,strFileName);
                return Content("OK|CashPayErrList.xls");
            }
            else
            {
                return Content("Err|批次单不能为空");
            }
        }
        public ActionResult GetBatchTradeDtls()//按Bid加载成功记录
        {
            string strFBid = Request["strFBid"];
            if(string.IsNullOrEmpty(strFBid)==false)
            {
                List<T_BatchMoneyTrade_DTL> dtls = new T_BatchMoneyTrade_DTLBLL().GetModelList("Bid='" + strFBid + "'");
                return Content(jss.Serialize(dtls));
            }
            return Content(jss.Serialize(new List<T_BatchMoneyTrade_DTL>()));
        }
        public ActionResult GetBatchTradeErrs()//按Bid加载失败记录
        {
            string strFBid = Request["strFBid"];
            if (string.IsNullOrEmpty(strFBid) == false)
            {
                List<T_BatchMoneyTrade_ErrList> dtls = new T_BatchMoneyTrade_ErrListBLL().GetModelList("pc='" + strFBid + "'");
                return Content(jss.Serialize(dtls));
            }
            return Content(jss.Serialize(new List<T_BatchMoneyTrade_ErrList>()));
        }




        public ActionResult GetTrades(int id=1)//查询批量导入主单记录
        {

            string searchType = Request["searchType"];
            string searchTypeName = Request["searchTypeName"];
            string searchPCH = Request["searchPCH"];
            string searchStartDate = Request["searchStartDate"];
            string searchEndDate = Request["searchEndDate"];

            StringBuilder strSql = new StringBuilder();
            strSql.Append("CrtDt>='" + searchStartDate + "' and CrtDt<'" + searchEndDate + "'");
            if (string.IsNullOrEmpty(searchPCH) == false)
            {
                strSql.Append(" and Bid='" + searchPCH + "'");
            }
            if (string.IsNullOrEmpty(searchType) == false)
            {
                strSql.Append(" and FCourseType='" + searchTypeName + "'");
            }
            if(id==1)
            {
                strSql.Append(" and FMoneyInOutFlag=1");

            }
            else
            {
                strSql.Append(" and FMoneyInOutFlag=-1");
            }
            List<T_BatchMoneyTrade> dtls = new T_BatchMoneyTradeBLL().GetModelList(strSql.ToString());
            return Content(jss.Serialize(dtls));
        }


        public ActionResult plDeleteByPKId(int id=1)//按Bid批量删除已经导入的数据
        {
            string pkId = Request["pkId"];
            if (string.IsNullOrEmpty(pkId) ==true)
            {
                return Content("Err|主单号不能为空");
            }

            T_CZY czy=new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
            if(czy ==null){
                return Content("Err|操作员账户不能为空,请用管理员登录");
            }
            if(czy.FPRIVATE !=1){
                return Content("Err|不是管理员不能删除,请用管理员登录");
            }
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            int typeflag=9999;
            if(id==1)
            {
                typeflag = 0;//表示为存款
            } if (id == 2)
            {
                typeflag = 2;//表示为取款
            }
            //List<T_Vcrd> checkVcrds = new T_VcrdBLL().GetModelList(" flag=0 and typeflag='" + typeflag.ToString() + "' and Origid='" + pkId + "'");
            //if (checkVcrds.Count <= 0)
            //{
            //    return Content("Err|该主单号没有对应的Vcrd记录，无法批量删除！");
            //}

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList(" flag in(0,-2) and typeflag='" + typeflag.ToString() + "' and Origid='" + pkId + "' and isnull(bankflag,0)>=1");
            if (vcrds.Count > 0)
            {
                return Content("Err|该主单号的数据已经发送到银行了，不能删除！");
            }

            //开始删除数据

            if(new T_BatchMoneyTradeBLL().plDeleteByPKId(pkId, strLoginName, typeflag))
            {
                return Content("OK|删除成功！");
            }
            else
            {
                return Content("OK|对不起，删除失败！");
            }

        }

        private static string CheckAndAddRecord(int onlyCheckFlag,int auditFlag,int id,T_Savetype subSavetype,string strRemark,string LoginUserName, string LoginUserCode, string strFCode, string strFName, string strFMoney, string strFBid, out string okInfo, out int strFlag)
        {
            string strRusult = "";//返回处理结果信息
            decimal cpctMoney = 0;//留存金额
            okInfo = "";//成功的信息
            T_BatchMoneyTrade_DTL model = new T_BatchMoneyTrade_DTL();

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

                T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(criminals[0].FCode,7);
                if (criminal.ErrInfo != "")
                {
                    strFlag = 1;//犯人信息不正确或是无权管理
                    strRusult = criminal.ErrInfo + "，请与管理人员联系";
                }
                if (criminal.flimitamt == null)
                {
                    criminal.flimitamt = 0;
                }
                if (criminal.flimitflag == null)
                {
                    criminal.flimitamt = 0;
                }
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
                    T_BatchMoneyTrade bonus = new T_BatchMoneyTradeBLL().GetModel(strFBid);
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
                        }
                        else
                        {
                            //获得系统最大奖金发放次数
                            T_SETTINGS sModel = new T_SETTINGSBLL().GetModelList("TYPE=606")[0];

                            string strUdate = Convert.ToString(bonus.UDate);
                            string strWhere = " UDate='" + strUdate + "' and FCrimeCode='" + strFCode + "'";
                            int userCount = new T_BONUSBLL().GetSendCountByBid(strFCode, strUdate);
                            if (Convert.ToInt32(sModel.VALUE) <= userCount)
                            {
                                strFlag = 7;//超过每月最大发放次数
                                strRusult = strFCode + "," + strFName + ",超过每月最大发放次数:" + sModel.VALUE;
                            }
                            else
                            {
                                //==========================================================
                                // flag 标志，1是存款，-1是扣款
                                //如果是扣款则要先判断一下金额是否够扣
                                if (id == 2)
                                {                                    
                                    //如果A+B+C账户的金额-冻结的金额够扣就执行扣款
                                    if ((card.AmountA + card.AmountB + card.AmountC - (criminal.flimitamt * criminal.flimitflag)) < Convert.ToDecimal(strFMoney))
                                    {
                                        if (subSavetype.FuShuFlag == 0)//判断是否可以透支
                                        {
                                            if (card.AmountA + card.AmountB + card.AmountC < Convert.ToDecimal(strFMoney))
                                            {
                                                return "账户金额不足扣，A账户:" + card.AmountA.ToString() + ",B账户:" + card.AmountB.ToString() +",C账户:" + card.AmountC.ToString(); 
                                            }
                                            else
                                            {
                                                return "该账户有冻结金额：" + criminal.flimitamt.ToString() + ",账户金额不足扣，A账户:" + card.AmountA.ToString() + ",B账户:" + card.AmountB.ToString()+ ",C账户:" + card.AmountC.ToString();
                                            }
                                        }                                        
                                    }
                                    //如果A+B账户的金额-冻结的金额够扣就执行扣款,（批量消费扣款只能用A+B账户）
                                    if (subSavetype.PLXE_Flag == 1)//判断是否只能用A+B（批量消费扣款只能用A+B账户）
                                    {
                                        if (card.AmountA + card.AmountB < Convert.ToDecimal(strFMoney))
                                        {
                                            return "Err|批量消费扣款的A+B账户余额不足,有冻结金额:" + criminal.flimitamt.ToString() + "，不可用";
                                        }
                                    }

                                    //如果批量限额标志为1（真）,则判断可消费余额是否有够
                                    string checkResutl = "";
                                    checkResutl = checkXiaoFeiEdu(strFMoney, criminal, subSavetype.PLXE_Flag, checkResutl);
                                    if (checkResutl != "")
                                    {
                                        return ("超出本月最大限额，可消费余额不足");
                                    }                                    
                                }


                                
                                //==========================================================


                                //设定Model信息
                                SetBonusDetailModel(LoginUserName, strFCode, strFMoney, strFBid, cpctMoney, model, criminal, bonus, area, card,strRemark);

                                //StringBuilder strSql = new StringBuilder();
                                //strSql.Append("insert into T_BatchMoneyTrade_DTL(");
                                //strSql.Append("FCriminal,Vouno,FrealareaCode,FrealAreaName,Remark,PType,UDate,CrtBy,CrtDt,ApplyBy,Bid,AccType,CardType,AmountC,cqbt,gwjt,ldjx,tbbz,grkj,FMoneyInOutFlag,FCrimeCode,CardCode,FAmount,Flag,FareaCode,FareaName");
                                //strSql.Append(") values (");
                                //strSql.Append("'" + model.FCriminal + "','" + model.Vouno + "','" + model.FrealareaCode + "','" + model.FrealAreaName + "','" + model.Remark + "','" + model.PType + "','" + model.UDate.ToShortDateString() + "','" + model.CrtBy + "','" + model.CrtDt.ToString() + "','" + model.ApplyBy + "','" + model.Bid + "','" + model.AccType + "','" + model.CardType + "','" + model.AmountC + "','" + model.cqbt + "','" + model.gwjt + "','" + model.ldjx + "','" + model.tbbz + "','" + model.grkj + "','" + model.FMoneyInOutFlag + "','" + model.FCrimeCode + "','" + model.CardCode + "','" + model.FAmount + "','" + model.Flag + "','" + model.FareaCode + "','" + model.FareaName + "'");
                                //strSql.Append(") ");
                                //strSql.Append(";");            





                                if (new T_BatchMoneyTrade_DTLBLL().Add(model) > 0)
                                {
                                    if (onlyCheckFlag != 1)//如果“只检测”标志不等1，则导入数据
                                    {
                                        //插入到T_Vcrd中，1加入到Vcrd，2增加余额
                                        List<T_Vcrd> vcrd = new List<T_Vcrd>();
                                        if (id == 1)
                                        {
                                            vcrd = new T_VcrdBLL().UserCunKouKuan(strFCode, 1, Convert.ToDecimal(strFMoney), subSavetype, LoginUserName, strRemark, "", strFBid);
                                        }
                                        else
                                        {//取款
                                            
                                            vcrd = new T_VcrdBLL().UserCunKouKuan(strFCode, -1, Convert.ToDecimal(strFMoney), subSavetype, LoginUserName, strRemark, "", strFBid,auditFlag);
                                        }
                                        //更新主单金额及数量
                                        if (new T_BatchMoneyTradeBLL().UpdateByCountMoney(strFBid))
                                        {
                                            T_BatchMoneyTrade nbonus = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                                            okInfo = model.FCrimeCode + "|" + model.FCriminal + "|" + model.FareaName + "|" + model.FAmount.ToString() + "|" + model.AmountC.ToString() + "|" + bonus.Remark + "|" + nbonus.cnt.ToString() + "|" + nbonus.FAmount.ToString();
                                        }
                                    }                                    
                                }
                                else
                                {
                                    strFlag = 4;//明细单保存失败
                                    strRusult = "明细单保存失败";
                                }
                            }
                        }

                    }
                }
            }
            return strRusult;
        }


        private static void SetBonusDetailModel(string LoginUserName, string strFCode, string strFMoney, string strFBid, decimal cpctMoney, T_BatchMoneyTrade_DTL model, T_Criminal criminal, T_BatchMoneyTrade bonus, T_AREA area, T_Criminal_card card,string strRemark)
        {
            model.Bid = strFBid;
            model.FMoneyInOutFlag = -1;
            model.FCrimeCode = criminal.FCode;
            model.FCriminal = criminal.FName;
            model.CardCode = card.cardcodea;
            model.FrealareaCode = "";
            model.FrealAreaName = "";
            model.FareaCode = area.FCode;
            model.FareaName = area.FName;
            model.FAmount = Convert.ToDecimal(strFMoney);
            model.AmountC = cpctMoney;
            model.CardType = 0;
            model.AccType = 0;
            model.CrtBy = LoginUserName;
            model.CrtDt = Convert.ToDateTime(bonus.crtdt);
            model.UDate = Convert.ToDateTime(bonus.UDate);
            model.PType = bonus.Remark;
            model.Remark = strRemark;
            model.cqbt = 0;
            model.grkj = 0;
            model.gwjt = 0;
            model.ldjx = 0;
            model.tbbz = 0;
            model.Flag = 0;
            model.Vouno = "";
            model.ApplyBy = "";
        }

        //取款
        public ActionResult Payment(int id=1)
        {

            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;

            List<T_Savetype> saveTypes = new T_SavetypeBLL().GetModelList("typeFlag=0");
            ViewData["saveTypes"] = saveTypes;
            return View();
        }
	}

    public class rtnTrades
    {
        public T_BatchMoneyTrade trade { get; set; }
        public List<T_BatchMoneyTrade_DTL> dtls { get; set; }
    }
}