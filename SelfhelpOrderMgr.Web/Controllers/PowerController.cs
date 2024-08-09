using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Model.ExtModel;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;//正则表达式
using SelfhelpOrderMgr.Web.CommonHeler;
using Newtonsoft.Json;
using System.IO;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [CustomActionFilterAttribute]
    
    public class PowerController : Controller
    {
        BaseDapperBLL _baseDapperBLL = new BaseDapperBLL();
        #region 用户管理操作
        // GET: /Power/  //用户管理
        public ActionResult Index()
        {
            #region 加载队别信处
            List<T_AREA> areas = (List<T_AREA>)new T_AREABLL().GetModelList("");
            Dictionary<string, string> userAreas = new Dictionary<string, string>();
            foreach (T_AREA area in areas)
            {
                userAreas.Add(area.FName, area.FCode);
            }
            ViewData["userAreas"] = userAreas;
            #endregion

            #region 加载角色信息
            List<t_TreeRole> roles = (List<t_TreeRole>)new t_TreeRoleBLL().GetModelList("");
            Dictionary<string, string> userRoles = new Dictionary<string, string>();
            foreach (t_TreeRole role in roles)
            {
                userRoles.Add(role.RoleName, role.RoleID.ToString());
            }
            ViewData["userRoles"] = userRoles;
            #endregion

            //加载操作员用户信息
            List<T_CZY> czys = (List<T_CZY>)new T_CZYBLL().GetModelList("");
            for (int i = 0; i < czys.Count; i++)
            {
                czys[i].FPwd = "**********";
            }

            ViewData["czys"] = czys;

            //加载监区信息

            return View();
        }

        public ActionResult GetUserList()
        {
            //加载操作员用户信息
            List<T_CZY> czys = (List<T_CZY>)new T_CZYBLL().GetModelList("");
            //ViewData["czys"] = czys;
            for (int i = 0; i < czys.Count; i++)
            {
                czys[i].FPwd = "**********";
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Content(jss.Serialize(czys));

        }

        //返回Tree方式的监区信息
        public ActionResult GetTreeArea()
        {
            List<T_AREA> fids = (List<T_AREA>)new T_AREABLL().GetModelList("FId is null and Id=0");
            List<Comment> lists = new List<Comment>();
            if (null != fids)
            {
                foreach (T_AREA fid in fids)
                {
                    Comment list = new Comment() { id = Convert.ToInt32(fid.ID), text = fid.FName, iconCls = "icon-ok", state = "open" };
                    DiguiGetTreeArea(fid, ref list);
                    lists.Add(list);
                }

            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string ddd = jss.Serialize(lists);
            return Content(ddd);

        }

        //保存操作员用户
        [MyLogActionFilterAttribute]
        public ActionResult SaveTree(string selectTree,string UserCode,string UserName,string UserArea,string UserRole,string FManagerCard,int selectRadio,string FUserChinaName)
        {
            //string selectTree = Request["selectTree"].ToString();
            string strUserCode = UserCode;// Request["UserCode"].ToString();
            string strUserName = UserName;//Request["UserName"].ToString();
            //string strUserPwd = UserPwd;//Request["UserPwd"].ToString();
            string strUserPwd =Request["UserPwd"].ToString();
            string strUserArea = UserArea;//Request["UserArea"].ToString();
            string strUserRole = UserRole;//Request["UserRole"].ToString();
            //string FManagerCard = Request["FManagerCard"].ToString();
            int fprivate = selectRadio;// Request["selectRadio"] == null ? 0 : Convert.ToInt32(Request["selectRadio"]);

            //string FUserChinaName = Request["FUserChinaName"] == null ? "" : Request["FUserChinaName"];
            

                string decPwd = strUserPwd;
            if (strUserPwd != "**********")
            {

                #region 正则表达式验证密码
                string regexResult = RegexHelper.RegexPasswordCheck(strUserPwd);
                if (!string.IsNullOrWhiteSpace(regexResult))
                {
                    return Content($"Err|{regexResult}");
                }
                #endregion
            }

            //#region 正则表达式验证密码
            //string regexCheckResult = RegexHelper.RegexPasswordCheck(strUserPwd);

            //if (!string.IsNullOrWhiteSpace(regexCheckResult))
            //{
            //    return Content("Error."+regexCheckResult);
            //}

            //#endregion


            if (selectTree == "")
            {
                return Content("Error.请至少选择一个管理队别");
            }
            char ee = (char)124;
            string[] TreeNodes = selectTree.Split(ee);
            string selectAreas = "";
            foreach(string node in TreeNodes)
            {
                if (selectAreas == "")
                {
                    selectAreas = "'" + node + "'";
                }
                else
                {
                    selectAreas = selectAreas+",'" + node + "'";
                }
            }
            //string selectAreas=selectTree.Replace((char)124,(char)44);
            //string NodesSql = "'仓山监狱'";

            List<T_AREA> areas = (List<T_AREA>)new T_AREABLL().GetAreaByNodeNames(TreeNodes);
            List<string> userAreas = new List<string>();
            if (areas != null)
            {
                foreach (T_AREA area in areas)
                {
                    userAreas.Add(area.FName);
                }
            }

            //首先是保存用户信息,如T_czy
            //1、判断记录是否存在
            string strRoleName = "";
            List<t_TreeRole> roles = new t_TreeRoleBLL().GetModelList("RoleId="+strUserRole +"");
            if (roles.Count > 0)
            {
                strRoleName = roles[0].RoleID.ToString();
            }
            T_CZY czy = new T_CZYBLL().GetModel(strUserCode);
            T_CZY op = new T_CZY();
            if (czy != null)
            {
                op = czy;
            }
            else
            {
                op = new T_CZY();
            }

            T_AREA tarea = new T_AREABLL().GetModel(strUserArea);
            if(tarea!=null)
            {
                strUserArea = tarea.FName;
            }

            op.FCode = strUserCode;
            op.FName = strUserName;

            //不为"**********"才修改
            if (strUserPwd != "**********")
            {
                if (strUserPwd.Length < 20)
                {
                    op.FPwd = MD5ProcessHelper.GetMd5Password(strUserPwd);
                }
                else
                {
                    op.FPwd = strUserPwd;
                }
            }

            
            op.FUserArea = strUserArea;
            t_TreeRole trl = new t_TreeRole();
            trl.RoleID = strUserRole == "" ? 0 : Convert.ToInt32(strUserRole);
            trl.RoleName = strRoleName;
            //op.FRole = trl;
            op.FRole = strUserRole;
            op.FFlag = 0;
            op.FPRIVATE = fprivate;
            op.FUserChinaName = FUserChinaName;
            op.ver = "";
            op.rolecode = "";
            op.FManagerCard = FManagerCard;
            HttpPostedFileBase photo;
            string photoFileName = "";
            string imageSrc = "";
            if (Request.Files.Count > 0)
            {
                photo = Request.Files[0];
                if (photo != null && !string.IsNullOrWhiteSpace( photo.FileName))
                {
                    //byte[] imageBytes = null;

                    //using (var ms = new MemoryStream())
                    //{
                    //    photo.InputStream.CopyTo(ms);
                    //    imageBytes = ms.GetBuffer();
                    //}

                    //string base64String = $"data:image/{Path.GetExtension(photo.FileName).Substring(1)};base64,{Convert.ToBase64String(imageBytes)}" ;
                    //return base64String;

                    photoFileName = photo.FileName;

                    string savePath = Server.MapPath("~/Upload/" + photoFileName);
                    photo.SaveAs(savePath);
                    string base64String =Base64ToImageHelper.ImgToBase64StringByReturn(savePath);
                    imageSrc = $"data:image/{Path.GetExtension(photoFileName).Substring(1)};base64,{base64String}" ;
                    
                    op.Photo = photoFileName;

                }
                else
                {
                    op.Photo = "";
                }
            }

            
            string strRes = "";
            if (czy != null)
            {
                new T_CZYBLL().Update(op);
                strRes = "Update";
                Log4NetHelper.logger.Info("操作员：" + Session["loginUserName"].ToString() + ",修改了一个用户，ID=" + op.FCode + ",用户名为：" + op.FName);

            }
            else
            {
                new T_CZYBLL().Add(op);
                strRes = "Insert";
                Log4NetHelper.logger.Info("操作员：" + Session["loginUserName"].ToString() + ",新增了一个用户，ID=" + op.FCode + ",用户名为：" + op.FName);

            }

            //接下来是保存用户权限区域,
            //先删除所有该用户监区的权限

            int res = new T_Czy_areaBLL().DeleteByFCode(strUserCode);
            //再插入所有该功能,权限都为0
            res = new T_Czy_areaBLL().BatchInsert(strUserCode);

            //第三修改该用户所拥有该功能,权限设为1
            res = new T_Czy_areaBLL().UpdateByNodesAndUserCode(strUserCode, 1, userAreas.ToArray());

            //最后修改该用户所拥有该功能,权限设为2
            res = new T_Czy_areaBLL().UpdateByNodesAndUserCode(strUserCode, 2, TreeNodes);

            

            if (res > 0)
            {
                //注册人脸
                if (imageSrc.Length > 2000 && op.FManagerCard.Length == 7)
                {
                    //PhotoEntity photoEntity = new PhotoEntity();
                    //photoEntity.fcrimeCode = op.FManagerCard;
                    //photoEntity.photoBase64Data = imageSrc;
                    //photoEntity.photoName = photoFileName;
                    //photoEntity.TypeFlag = 1;
                    
                    this.RegFaceModel(op.FManagerCard, imageSrc, "0003", 1);
                }
                strRes = "OK." + strRes;
            }
            else
            {
                strRes = "Error." + strRes;
            }
            //Log4NetHelper.logger.Info("操作员：" + Session["loginUserName"].ToString() + ",修改一个用户信息，ID=" + strUserCode + ",用户名为：" + strUserName + ",结果为：" + strRes);

            return Content(strRes);

        }


        //public ActionResult CheckFace()
        private ResultInfo RegFaceModel(string fcrimecode, string imageSrc, string faceMode = "0003", int loginCheck = 1)
        {
            ResultInfo rs = new ResultInfo();

            if (string.IsNullOrWhiteSpace(faceMode))
            {
                faceMode = "0003";
            }

            if (string.IsNullOrWhiteSpace(imageSrc))
            {
                rs.ReMsg = "Err|图片不能为空";
                return rs;
            }

            int typeFlag = 0;
            if (loginCheck == 2)
            {
                typeFlag = 1;
            }

            rs = FaceCheckService.SendAndCheckFace(fcrimecode, imageSrc, faceMode, null, typeFlag, "");


            return (rs);
        }



        //删除操作员用户
        [MyLogActionFilterAttribute]
        public ActionResult DelTreeUser(string FUserID,string FUserName)
        {
            string UserID = FUserID;// Request["FUserID"];
            string UserName = FUserName;//Request["FUserName"];
            T_CZY czy = new T_CZYBLL().GetModel(UserID);
            string res = "删除失败";
            if (czy != null)
            {
                if (czy.FName == UserName)
                {
                    int rs = new T_Czy_areaBLL().DeleteByFCode(UserID);
                    bool bl = new T_CZYBLL().Delete(UserID);
                    if (bl ==true)
                    {
                        res = "删除成功";
                        Log4NetHelper.logger.Info("操作员：" + Session["loginUserName"].ToString() + ",删除了一个用户，ID=" + UserID + ",用户名为：" + UserName);
                    }
                }
            }
            return Content(res);
        }

        //通过递归方式 把目录树生成队别树,并反回EasyUI的Tree
        private Comment DiguiGetTreeArea(T_AREA m1, ref Comment list)
        {
            List<T_AREA> tms1 = (List<T_AREA>)new T_AREABLL().GetModelList("FID =" + m1.ID.ToString());
            if (tms1 != null)
            {
                List<Comment> cren = new List<Comment>();
                foreach (T_AREA subarea in tms1)
                {
                    Comment Subcren = new Comment();

                    Subcren.id = Convert.ToInt32(subarea.ID);
                    Subcren.text = subarea.FName;
                    Subcren.iconCls = "icon-ok";
                    //Subcren.children = Subcren;
                    Subcren.state = "open";

                    List<attr> Latrr = new List<attr>();
                    Latrr.Add(new attr() { url = subarea.URL });
                    Subcren.attributes = Latrr;

                    DiguiGetTreeArea(subarea, ref Subcren);
                    cren.Add(Subcren);
                }
                list.children = cren;

            }
            return list;
        }
        public ActionResult GetAreaPower()
        {
            string strUserID = Request["FUserID"].ToString();
            //sql = "select seqno from t_Czy_area where fcode='" + strUserID + "' and fflag=2 order by seqno";
            //var data = MyDb.GetDS(sql, dbname);
            List<T_Czy_area> areas = (List<T_Czy_area>)new T_Czy_areaBLL().GetAreaByFCodeAndFlag(strUserID, 2);
            string selRole = "";
            if (areas.Count > 0)
            {
                foreach (T_Czy_area area in areas)
                {
                    if ("" == selRole)
                    {
                        selRole = area.seqno.ToString();
                    }
                    else
                    {
                        selRole = selRole + "|" + area.seqno.ToString();
                    }
                }
            }
            //返回角色权限            
            return Content(selRole);
        }

        #endregion

        public ActionResult ChangePassword()
        {
            return View();
        }

        [MyLogActionFilterAttribute]
        //修改密码
        public ActionResult SavePassword()
        {
            string result = "";
            string userName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string oldPwd = Request["oldPassword"];
            string newPwd = Request["newPassword"];
            string chkPwd = Request["checkPassword"];
            if (string.IsNullOrEmpty(oldPwd) == true)
            {
                result = "原密码不会为空，请重输";
                return Content(result);
            }
            if (string.IsNullOrEmpty(newPwd) == true)
            {
                result = "新密码不能为空";
                return Content(result);
            }
            if (newPwd != chkPwd)
            {
                result = "两次新密码不一致";
                return Content(result);
            }


            #region 正则表达式验证密码
            string regexCheckResult = RegexHelper.RegexPasswordCheck(newPwd);

            if (!string.IsNullOrWhiteSpace(regexCheckResult)){
                return Content(regexCheckResult);
            }

            #endregion



            List<T_CZY> czys = new T_CZYBLL().GetModelList("FName='"+  userName +"'");
            if (czys.Count > 0)
            {
                T_CZY czy = czys[0];
                if (czy.FPwd == oldPwd  || czy.FPwd == MD5ProcessHelper.GetMd5Password(oldPwd))
                {
                    czy.FPwd = MD5ProcessHelper.GetMd5Password(newPwd); //密码加密存储
                    czy.PwdUpdateTime = DateTime.Now;
                    new T_CZYBLL().Update(czy);
                    result = "密码|保存成功";
                }
                else
                {
                    result = "用户信息或是密码不正确";
                }
            }
            return Content(result);
        }
        #region 角色管理
        //角色管理
        public ActionResult RoleMgr()
        {
            return View();
        }
        public ActionResult GetAllTree()
        {
            //string LoginUserName = Request.Cookies["person_Users"]["sysLoginName"].ToString();
            string LoginUserCode = "102";
            //string LoginUserCode = Request.Cookies["person_Users"]["sysLoginCode"].ToString();
            List<t_TreeMeun> menus = new t_TreeMeunBLL().GetModelList("id=0");
            t_TreeMeun m1 = menus[0];
            if (m1.id.ToString() != null)
            {
                //T_CZY czy = new T_CZYBLL().GetModel(LoginUserCode);
                //List<Comment> list = GetTreeMenuList(m1, czy.FRole.ToString());//把目录树生成菜单树,并反回EasyUI的Tree
                //JavaScriptSerializer jss = new JavaScriptSerializer();

                T_CZY czy = new T_CZYBLL().GetModel(LoginUserCode);
                List<Comment> list = GetSysTreeMenuList(m1, czy.FRole.ToString());//把目录树生成菜单树,并反回EasyUI的Tree
                JavaScriptSerializer jss = new JavaScriptSerializer();

                string ddd = jss.Serialize(list);
                Response.Write(ddd);
            }
            else
            {
                return Content("信息不存在");
            }
            return Content("");
        }
        public ActionResult GetRoleList()
        {
            List<t_TreeRole> roles;
            roles = (List<t_TreeRole>)new t_TreeRoleBLL().GetModelList("");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (roles.Count <= 0)
            {
                roles.Add(new t_TreeRole());
            }
            return Content(jss.Serialize(roles));
        }
        public ActionResult GetRolePower()
        {
            //SqlDataBase MyDb = new SqlDataBase();
            string strRoleID = Request["RoleID"].ToString();
            List<t_TreeRole_Menu> rolemenus = (List<t_TreeRole_Menu>)new t_TreeRole_MenuBLL().GetModelList("RoleID=" + strRoleID +" and Flag=2");
            string selRole = "";
            if (rolemenus != null)
            {
                if (rolemenus.Count > 0)
                {
                    foreach (t_TreeRole_Menu rolemenu in rolemenus)
                    {
                        if ("" == selRole)
                        {
                            selRole = rolemenu.TreeId.ToString();
                        }
                        else
                        {
                            selRole = selRole + "|" + rolemenu.TreeId.ToString();
                        }
                    }
                }
            }
            //返回角色权限

            return Content(selRole);
        }
        //保存角色的功能权限
        [MyLogActionFilterAttribute]
        public ActionResult SaveRoleTree(string RoleID,string RoleName)
        {
            //string LoginUserCode = "102";
            //增加或是修改角色信息
            string strRoleID = RoleID;// Request["RoleID"].ToString();
            string strRoleName = RoleName;// Request["RoleName"].ToString();
            //int levelid = Request["LevelId"] == null ? 0 : Convert.ToInt32(Request["LevelId"]);

            //首先判断下角色是否存在,如果存在就修改它,如果不存在就增加它
            string sql = "select * from t_TreeRole where RoleID='" + strRoleID + "'";
            List<t_TreeRole> trs = new t_TreeRoleBLL().GetModelList("RoleId=" + strRoleID);
            string doType = "Insert";
            t_TreeRole tr ;
            if (trs.Count > 0)
            {
                tr = trs[0];
                doType = "Update";
                tr.RoleName = strRoleName;
                //tr.LevelId = levelid;
                new t_TreeRoleBLL().Update(tr);
            }
            else
            {
                tr = new t_TreeRole();
                tr.RoleName = strRoleName;
                tr.RoleID = Convert.ToInt32(strRoleID);
                //tr.LevelId = levelid;
                new t_TreeRoleBLL().Add(tr);
            }

            //保存权限Tree
            string selectTree = Request["selectTree"].ToString();
            char ee = (char)124;
            string[] TreeNodes = selectTree.Split(ee);
            string NodesSql = "'首页'";
            string OwnerNodesSql = "";
            for (int i = 0; i < TreeNodes.Length; i++)
            {
                NodesSql = NodesSql + ",'" + TreeNodes[i] + "'";
                if ("" == OwnerNodesSql)
                {
                    OwnerNodesSql = "'" + TreeNodes[i] + "'";
                }
                else
                {
                    OwnerNodesSql = OwnerNodesSql + ",'" + TreeNodes[i] + "'";
                }
            }
            List<t_TreeMeun> menus = (List<t_TreeMeun>)new t_TreeMeunBLL().GetModelList("text in(" + NodesSql + ") or id in(select fid from t_TreeMeun where text in(" + NodesSql + "))");
            NodesSql = "";
            foreach (t_TreeMeun menu in menus)
            {
                if ("" == NodesSql)
                {
                    NodesSql = "'" + menu.Text.ToString() + "'";
                }
                else
                {
                    NodesSql = NodesSql + ",'" + menu.Text.ToString() + "'";
                }
            }
            //先删除所有该角色的权限
            int res = new t_TreeRole_MenuBLL().DeleteByRoleId(Convert.ToInt32(strRoleID));
            //再插入所有该功能,权限都为0
            res = new t_TreeRole_MenuBLL().InsertRoleMenu(Convert.ToInt32(strRoleID));
            //第三修改该角所拥有该功能,权限设为1
            res = new t_TreeRole_MenuBLL().UpdateByRoleMenu(1, strRoleID, NodesSql);
            //最后修改该角所拥有该功能,权限设为2
            res = new t_TreeRole_MenuBLL().UpdateByRoleMenu(2, strRoleID, OwnerNodesSql);
            return Content("OK." + doType);
        }
        //删除角色
        [MyLogActionFilterAttribute]
        public ActionResult DelRoleTrees(string RoleID,string RoleName)
        {
            string strRoleID = RoleID;// Request["RoleID"];
            string strRoleName = RoleName;// Request["RoleName"];

            string result = "删除失败";
            List<t_TreeRole> trs = new t_TreeRoleBLL().GetModelList("RoleId="+strRoleID);
            t_TreeRole tr = trs[0];
            if (tr != null)
            {
                if (tr.RoleName == strRoleName)
                {
                    new t_TreeRole_MenuBLL().DeleteByRoleId(Convert.ToInt32(strRoleID));
                    bool res = new t_TreeRoleBLL().Delete(Convert.ToInt32(strRoleID));
                    if (res ==true)
                    {
                        result = "删除成功";
                    }
                }
            }



            return Content(result);
        }
        // 把目录树生成菜单树,并反回EasyUI的Tree
        private List<Comment> GetTreeMenuList(t_TreeMeun m1, string RoleId)
        {
            List<Comment> list = new List<Comment>();
            //try
            //{
            t_TreeMeun[] tms1 = new t_TreeMeunBLL().GetTableByFId(m1.id, RoleId).ToArray();

            List<Comment> cren = new List<Comment>();
            for (int i = 0; i < tms1.Count(); i++)
            {
                List<Comment> Subcren = new List<Comment>();
                try
                {
                    t_TreeMeun[] tms2 = new t_TreeMeunBLL().GetTableByFId(tms1[i].id, RoleId).ToArray();

                    for (int j = 0; j < tms2.Count(); j++)
                    {
                        if (null != tms2[j].Text)
                        {
                            List<attr> Latrr = new List<attr>();
                            Latrr.Add(new attr() { url = tms2[j].URL });
                            Subcren.Add(new Comment() { id = tms2[j].id, attributes = Latrr, text = tms2[j].Text });
                        }
                        else
                        {
                            Subcren.Add(new Comment() { id = tms2[j].id, text = tms2[j].Text });
                        }
                    }
                    cren.Add(new Comment() { id = tms1[i].id, text = tms1[i].Text, iconCls = "icon-ok", children = Subcren, state = "open" });
                }
                catch { }
            }
            list.Add(new Comment() { id = m1.id, text = m1.Text, iconCls = "icon-ok", children = cren, state = "open" });
            //}
            //catch { }
            return list;
        }



        /// <summary>
        /// 获取系统权限列表
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        private List<Comment> GetSysTreeMenuList(t_TreeMeun m1, string RoleId="1")
        {
            List<Comment> list = new List<Comment>();
            t_TreeMeun[] tms1 = new t_TreeMeunBLL().GetModelList($"FId={m1.id}" ).ToArray();

            List<Comment> cren = new List<Comment>();
            for (int i = 0; i < tms1.Count(); i++)
            {
                List<Comment> Subcren = new List<Comment>();
                try
                {
                    t_TreeMeun[] tms2 = new t_TreeMeunBLL().GetModelList($"FId={tms1[i].id}").ToArray();

                    for (int j = 0; j < tms2.Count(); j++)
                    {
                        if (null != tms2[j].Text)
                        {
                            List<attr> Latrr = new List<attr>();
                            Latrr.Add(new attr() { url = tms2[j].URL });
                            Subcren.Add(new Comment() { id = tms2[j].id, attributes = Latrr, text = tms2[j].Text });
                        }
                        else
                        {
                            Subcren.Add(new Comment() { id = tms2[j].id, text = tms2[j].Text });
                        }
                    }
                    cren.Add(new Comment() { id = tms1[i].id, text = tms1[i].Text, iconCls = "icon-ok", children = Subcren, state = "open" });
                }
                catch { }
            }
            list.Add(new Comment() { id = m1.id, text = m1.Text, iconCls = "icon-ok", children = cren, state = "open" });
            return list;
        }


        #endregion


        #region 日志查询

        public ActionResult LogMgr()
        {

            return View();
        }

        public ActionResult GetLogInfoList(string strJsonWhere, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { Id = 0 };
                strJsonWhere = Newtonsoft.Json.JsonConvert.SerializeObject(sWhere);
            }

            PageResult<T_SysOperationLog> list = new BaseDapperBLL().GetPageList<T_SysOperationLog, T_SysOperationLog_Search>("Id", strJsonWhere, page, rows);

            EasyUiPageResult<T_SysOperationLog> rs = new EasyUiPageResult<T_SysOperationLog>()
            {
                total = list.total,
                rows = list.rows
            };
            return Content(JsonConvert.SerializeObject(rs));
        }



        #endregion
    }
}