using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using System.Data.SqlClient;

namespace SelfhelpOrderMgr.DAL
{
    public partial class JXHospitalDAL
    {
        public string WriteJXInvoice(InvoiceEntity reqModel)
        {
            //IDbConnection conn = new SqlConnection(SqlHelper.getConnstr());
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                StringBuilder strSql;
                int saleTypeId = 7;

                string invoiceno = reqModel.InvoiceNo;
                strSql = new StringBuilder();
                string ipLastCode = "_JX";
                T_Criminal criminal=new T_CriminalDAL().GetModel(reqModel.FCrimeCode);

                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    T_Vcrd vcrd = new T_Vcrd();

                    
                    int checkflag = 0;//配货检测状态，-1为未配货，0为已配货,是默认是-1
                    //从管理表中获取VCRD CheckFlag 的设定值
                    //T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetDAL().GetModel("JsVcrdCheckFlag");
                    //if (mgrSet != null)
                    //{
                    //    checkflag = Convert.ToInt32(mgrSet.MgrValue);
                    //}


                    #region 获得Invoice消费订单号

                    #endregion
                    #region 插入消费主单
                    strSql = new StringBuilder();
                    strSql.Append("insert into T_Invoice(");
                    strSql.Append("InvoiceNo,CardCode,FCrimeCode,Amount,OrderDate,PayDate,PType,Flag,Remark,ServAmount,Crtby,Crtdate,fsn,FAreaCode,FAreaName,FCriminal,Frealareacode,FrealAreaName,TypeFlag,CardType,AmountA,AmountB,Fifoflag,FreeAmountA,FreeAmountB,Checkflag,RoomNo,OrderId");
                    strSql.Append(") values (");
                    strSql.Append("@InvoiceNo,@CardCode,@FCrimeCode,@Amount,@OrderDate,@PayDate,@PType,@Flag,@Remark,@ServAmount,@Crtby,@Crtdate,@fsn,@FAreaCode,@FAreaName,@FCriminal,@Frealareacode,@FrealAreaName,@TypeFlag,@CardType,@AmountA,@AmountB,@Fifoflag,@FreeAmountA,@FreeAmountB,@Checkflag,@RoomNo,@OrderId");
                    strSql.Append(") ");
                    object paramInvoice = new { InvoiceNo = invoiceno, CardCode = reqModel.CardCode, FCrimeCode = reqModel.FCrimeCode, Amount = reqModel.Amount, OrderDate = reqModel.OrderDate, PayDate = reqModel.PayDate, PType = "建新医院消费", Flag = 1, Remark = reqModel.Remark, ServAmount = reqModel.ServAmount, Crtby = reqModel.Crtby, Crtdate = reqModel.Crtdate, fsn = reqModel.Fsn, FAreaCode = reqModel.FCrimeCode, FAreaName = reqModel.FAreaName, FCriminal = reqModel.FCriminal, Frealareacode = "", FrealAreaName = "", TypeFlag = saleTypeId, CardType = 0, AmountA = reqModel.AmountA, AmountB = reqModel.AmountB, Fifoflag = -1, FreeAmountA = reqModel.FreeAmountA, FreeAmountB = reqModel.FreeAmountB, Checkflag = 0, RoomNo = reqModel.RoomNo, OrderId = reqModel.OrderId };

                    #endregion
                    int i = conn.Execute(strSql.ToString(), paramInvoice, myTran);
                    if (i <= 0)//如果成功，开始插入明细
                    {
                        myTran.Rollback();
                        return ("Error|插入消费主单记录出错，已回滚。");
                    }


                    #region 处理插入Vcrd记及更新余额表



                    decimal invAmountA = reqModel.AmountA;
                    decimal invAmountB = reqModel.AmountB;

                    //先获取当前AB账户余额,实现插入消费记录时，将当前的账户余额一并写入到curUserAmout
                    T_Criminal_card tcard = new T_Criminal_cardDAL().GetModel(criminal.FCode);
                    decimal curUserAmountA = tcard.AmountA;
                    decimal curUserAmountB = tcard.AmountB;
                    decimal curAllAmount = tcard.AmountA + tcard.AmountB + tcard.AmountC;
                    //插入VCrd记录及更新余额
                    strSql = new StringBuilder();
                    strSql.Append("declare @VOUNO varchar(30);");
                    strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                    strSql.Append("select @VOUNO='VOU'+@VOUNO + @IpLastCode;");
                    strSql.Append("insert into T_Vcrd(");
                    strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount");
                    strSql.Append(") values (");
                    strSql.Append("@VOUNO,@cardcode,@fcrimecode,@DAMOUNT,@CAMOUNT,@crtBy,@CRTDATE,@DTYPE,@DEPOSITER,@REMARK,@flag,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@ptype,@udate,@origid,@cardtype,@TYPEFLAG,@acctype,@Bankflag,@checkflag,@checkby,@pc,@curUserAmount,@curAllAmount");
                    strSql.Append(");");
                    object paramVcrd;
                    int x = 0;
                    if (invAmountA > 0)//如果A账户大于0
                    {
                        //设定账户额
                        curUserAmountA = curUserAmountA - invAmountA;
                        //curUserAmountB = curUserAmountB - invAmountB;
                        curAllAmount = curAllAmount - invAmountA;
                        paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountA, crtBy = reqModel.Crtby, CRTDATE = DateTime.Now, DTYPE = "建新医院消费", DEPOSITER = "", REMARK = "住院期间消费", flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 0, Bankflag = 0, checkflag = checkflag, checkby = reqModel.Crtby, pc = 0, curUserAmount = curUserAmountA, curAllAmount = curAllAmount };
                        x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                        if (x <= 0)
                        {
                            myTran.Rollback();
                            return ("Error|插入VCrd记录出错，已回滚。");
                        }
                    }
                    if (invAmountB > 0)//如果A账户大于0
                    {
                        //设定账户额
                        //curUserAmountA = curUserAmountA - invAmountA;
                        curUserAmountB = curUserAmountB - invAmountB;
                        curAllAmount = curAllAmount - invAmountB;
                        paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountB, crtBy = reqModel.Crtby, CRTDATE = DateTime.Now, DTYPE = "建新医院消费", DEPOSITER = "", REMARK = "住院期间消费", flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 1, Bankflag = 0, checkflag = checkflag, checkby = reqModel.Crtby, pc = 0, curUserAmount = curUserAmountB, curAllAmount = curAllAmount };
                        x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                        if (x <= 0)
                        {
                            myTran.Rollback();
                            return ("Error|插入VCrd记录出错，已回滚。");
                        }
                    }

                    //更新金额
                    strSql = new StringBuilder();
                    strSql.Append("Update t_criminal_card set AmountA=AmountA-@AmountA,AmountB=AmountB-@AmountB where fcrimecode=@fcrimecode");
                    paramVcrd = new { fcrimecode = criminal.FCode, AmountA = invAmountA, AmountB = invAmountB };
                    x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                    if (x <= 0)
                    {
                        myTran.Rollback();
                        return ("Error|更新余额出错，已回滚。");
                    }
                    #endregion
                    myTran.Commit();
                    return ("OK|结算成功。");
                }
                catch (Exception ex)
                {
                    myTran.Rollback();

                    //订单状态复位为0“未提交”
                    return ("Error|数据执行过程中异常，已回滚。但订单状态没有复位" + ex.ToString());

                }
            }
        }

    }
}