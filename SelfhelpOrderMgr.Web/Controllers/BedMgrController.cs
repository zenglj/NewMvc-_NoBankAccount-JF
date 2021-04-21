using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [CustomActionFilterAttribute]
    public class BedMgrController : Controller
    {
        // GET: BedMgr
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAreaList()
        {
            string strLoginUserCode = Session["loginUserCode"].ToString();

            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in( select fareaCode from t_czy_area where fflag=2 and fcode='" + strLoginUserCode + "')");
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(areas));

        }
        public ActionResult GetAllList()
        {
            string strLoginUserName = Session["loginUserName"].ToString();
            BedInfomation op = new BedInfomation();
            List<BedInfomation> list;
            try
            {
                list = (List<BedInfomation>)new BedInfomationBLL().GetBedByMulSearch( 1, "", 0, strLoginUserName);
                if (list.Count <= 0)
                {
                    BedInfomation m1 = new BedInfomation();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                //return Json(new ResultInfo()
                //{
                //    DataInfo = list,
                //    Flag = true,
                //    ReMsg = "OK|成功"
                //});
            }
            catch
            {
                return Json(new ResultInfo()
                {
                    DataInfo = null,
                    Flag = false,
                    ReMsg = "Err|参数不正确"
                });
            }
        }

        /// <summary>
        /// 获取号房列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoomList(int fid)
        {
            string strLoginUserName = Session["loginUserName"].ToString();
            BedInfomation op = new BedInfomation();
            List<BedInfomation> list;
            try
            {
                //int fid = Convert.ToInt32(Request["FID"].ToString());
                list = (List<BedInfomation>)new BedInfomationBLL().GetBedByMulSearch( 2, "", fid, strLoginUserName);
                if (list.Count <= 0)
                {
                    BedInfomation m1 = new BedInfomation();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                //context.Response.Write(jss.Serialize(list));
                return Json(new ResultInfo() { 
                    Flag=true,
                    DataInfo=list,
                    ReMsg="OK|成功"
                });
            }
            catch
            {
                return Json(new ResultInfo()
                {
                    Flag = false,
                    DataInfo = null,
                    ReMsg = "Err|参数不正确"
                });
            }
        }

        public ActionResult GetBedList(int fid)
        {
            string strLoginUserName = Session["loginUserName"].ToString();
            BedInfomation op = new BedInfomation();
            List<BedInfomation> list;
            try
            {
                //int fid = Convert.ToInt32(context.Request["FID"].ToString());
                list = (List<BedInfomation>)new BedInfomationBLL().GetBedByMulSearch( 3, "", fid, strLoginUserName);
                if (list.Count <= 0)
                {
                    BedInfomation m1 = new BedInfomation();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                //context.Response.Write(jss.Serialize(list));
                return Json(new ResultInfo() { 
                    Flag=true,
                    DataInfo=list,
                    ReMsg="OK|成功"
                });
            }
            catch
            {
                return Json(new ResultInfo()
                {
                    Flag = false,
                    DataInfo = null,
                    ReMsg = "Err|参数不正确"
                });
            }
        }

        public ActionResult SaveGoodsList(string Dotype, BedInfomation op )
        {
            //string Dotype = context.Request["Dotype"].ToString();

            //BedInfomation m1 = new BedInfomation();
            //m1.ID = Convert.ToInt32(context.Request["ID"].ToString());
            //m1.FName = context.Request["FName"].ToString();
            //m1.LevelID = Convert.ToInt32(context.Request["LevelID"].ToString());
            //m1.LevelName = context.Request["LevelName"].ToString();
            //m1.FAreaCode = context.Request["FAreaCode"].ToString();
            //m1.FAreaName = context.Request["FAreaName"].ToString();
            //m1.FRemark = context.Request["FRemark"].ToString();
            BedInfomation m1 = op;

            switch (m1.LevelId)
            {
                case 1:
                    {
                        m1.FId = 0;
                        m1.FCrimeCode = "";
                        m1.FCrimeName = "";
                    }
                    break;
                case 2:
                    {
                        m1.FId = op.FId;
                        m1.FCrimeCode = "";
                        m1.FCrimeName = "";
                    }
                    break;
                case 3:
                    {
                        
                        if (m1.FCrimeCode == "")
                        {
                            m1.FFlag = 0;
                        }
                        else
                        {
                            m1.FFlag = 1;
                        }

                    }
                    break;
                default:
                    break;
            }
            try
            {
                if (Dotype == "update")
                {
                    new BedInfomationBLL().Update<BedInfomation>(m1);
                }
                else
                {
                    new BedInfomationBLL().Insert<BedInfomation>(m1);
                }
                return Json(new ResultInfo()
                {
                    Flag = true,
                    ReMsg = "OK|保存成功",
                    DataInfo = m1
                });
            }
            catch (Exception ex)
            {
                return Json(new ResultInfo()
                {
                    Flag = false,
                    ReMsg = "Err|"+ ex.Message,
                    DataInfo = null
                });
            }
            
        }
        public ActionResult BatchAddBed(string FID,string FAreaCode,string FAreaName, int BedNum)
        {
            try
            {
                List<BedInfomation> list=new List<BedInfomation>();
                string saveinfo = "";
                string Dotype = "Add";//固定为增加
                //string strfid = context.Request["FID"].ToString();
                //string fareacode = context.Request["FAreaCode"].ToString();
                //string fareaname = context.Request["FAreaName"].ToString();
                //int bednum = Convert.ToInt32(context.Request["BedNum"].ToString());

                string strfid = FID;
                string fareacode = FAreaCode;
                string fareaname = FAreaName;
                int bednum = BedNum;
                int levelid = 3;
                string levelname = "床位";
                string strTmpFid = "";
                for (int i = 1; i <= bednum; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        strTmpFid = strfid + Convert.ToString(i) + Convert.ToString(j);
                        int id = Convert.ToInt32(strTmpFid);
                        var r = new BedInfomationBLL().GetModel<BedInfomation>( id);
                        if (r==null)
                        {
                            BedInfomation m1 = new BedInfomation();
                            m1.Id = id;
                            if (j == 1)
                            {
                                m1.FName = strfid.Substring(0, 1).ToString() + "-" + strfid.Substring(1) + "-" + i.ToString() + "上铺";
                            }
                            else
                            {
                                m1.FName = strfid.Substring(0, 1).ToString() + "-" + strfid.Substring(1) + "-" + i.ToString() + "下铺";
                            }
                            m1.LevelId = levelid;
                            m1.LevelName = levelname;
                            m1.FAreaCode = fareacode;
                            m1.FAreaName = fareaname;
                            m1.FId = Convert.ToInt32(strfid);
                            m1.FCrimeCode = "";
                            m1.FCrimeName = "";
                            m1.FFlag = 0;
                            m1.FRemark = "";
                            if (Dotype == "update")
                            {
                                new BedInfomationBLL().Update<BedInfomation>(m1);
                            }
                            else
                            {
                                new BedInfomationBLL().Insert(m1);
                            }
                            list.Add(m1);
                        }
                    }
                }
                return Json(new ResultInfo()
                {
                    Flag = true,
                    ReMsg = "OK|批量生成成功",
                    DataInfo = null
                });
            }
            catch(Exception ex)
            {
                return Json(new ResultInfo()
                {
                    Flag = false,
                    ReMsg = "Err|" + ex.Message,
                    DataInfo = null
                });
            }
        }
        public ActionResult BatchAddHouse(string FID,string FAreaCode,string FAreaName,int Flood, int RoomNum)
        {
            try
            {
                List<BedInfomation> list=new List<BedInfomation>();
                //string saveinfo = "";
                //string strimg = "";
                string Dotype = "Add";//固定为增加
                //string strfid = context.Request["FID"].ToString();
                //string fareacode = context.Request["FAreaCode"].ToString();
                //string fareaname = context.Request["FAreaName"].ToString();
                //int flood = Convert.ToInt32(context.Request["Flood"].ToString());
                //int roomnum = Convert.ToInt32(context.Request["RoomNum"].ToString());

                string strfid = FID;
                string fareacode = FAreaCode;
                string fareaname = FAreaName;
                int flood = Flood;
                int roomnum = RoomNum;

                //int levelid = 2;
                //string levelname = "号房";
                string strTmpFid = "";
                string strRoomNum = "";
                for (int i = 1; i <= flood; i++)
                {
                    for (int j = 1; j <= roomnum; j++)
                    {
                        if (j < 10)
                        {
                            strTmpFid = strfid + Convert.ToString(i) + "0" + Convert.ToString(j);
                            strRoomNum = Convert.ToString(i) + "0" + Convert.ToString(j);
                        }
                        else
                        {
                            strTmpFid = strfid + Convert.ToString(i) + Convert.ToString(j);
                            strRoomNum = Convert.ToString(i) + Convert.ToString(j);
                        }
                        int id = Convert.ToInt32(strTmpFid);
                        var r = new BedInfomationBLL().GetModel<BedInfomation>( id);
                        if (r==null)
                        {
                            BedInfomation m1 = new BedInfomation();
                            m1.Id = Convert.ToInt32(strTmpFid);
                            m1.FName = strfid.Substring(0) + "-" + strRoomNum + "号房";
                            m1.LevelId = 2;
                            m1.LevelName = "号房";
                            m1.FAreaCode = fareacode;
                            m1.FAreaName = fareaname;
                            m1.FId = Convert.ToInt32(strfid);
                            m1.FCrimeCode = "";
                            m1.FCrimeName = "";
                            m1.FFlag = 0;
                            m1.FRemark = "";
                            if (Dotype == "update")
                            {
                                new BedInfomationBLL().Update<BedInfomation>( m1);
                            }
                            else
                            {
                                new BedInfomationBLL().Insert<BedInfomation>( m1);
                            }
                            list.Add(m1);
                        }
                    }
                }
                return Json(new ResultInfo() { 
                    Flag=true,
                    ReMsg= "OK|批量生成成功!",
                    DataInfo=list
                });
            }
            catch
            {
                return Json(new ResultInfo()
                {
                    Flag = false,
                    ReMsg = "OK|批量生成失败!",
                    DataInfo = null
                });
            }
        }

        public ActionResult Delete(int ID)
        {
            string strLoginUserName = Session["loginUserName"].ToString();

            BedInfomation m1 = new BedInfomation();
            List<BedInfomation> list;
            //m1.Id = Convert.ToInt32(context.Request["ID"].ToString());
            m1.Id = ID;
            list = (List<BedInfomation>)new BedInfomationBLL().GetBedByMulSearch( 2, "", m1.Id, strLoginUserName);
            if (list.Count > 0)
            {
                //返回失败
                return Json(new ResultInfo() { 
                    Flag=false,
                    ReMsg= "Err|该大楼下有相应的号房不能删除!",
                    DataInfo=null
                });
            }
            else
            {
                List<BedInfomation> bedList;
                m1.Id = ID;
                bedList = (List<BedInfomation>)new BedInfomationBLL().GetBedByMulSearch( 3, "", m1.Id, strLoginUserName);
                if (list.Count > 0)
                {
                    //返回失败
                    return Json(new ResultInfo()
                    {
                        Flag = false,
                        ReMsg = "Err|该号房下有相应的床位不能删除!",
                        DataInfo = null
                    });
                }
                else
                {
                    new BedInfomationBLL().Delete<BedInfomation>( m1.Id);
                    return Json(new ResultInfo()
                    {
                        Flag = true,
                        ReMsg = "OK|删除成功!",
                        DataInfo = null
                    });
                }

            }
        }
    }
}