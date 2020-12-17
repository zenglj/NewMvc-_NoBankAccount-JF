using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class MessageController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        int top = 3;
        //
        // GET: /Message/
        public ActionResult Index()
        {
            List<T_NotifyFile> newNotifies = (List<T_NotifyFile>)new T_NotifyFileBLL().GetPageListOfIEnumerable(0, 1, top, "");
            ViewData["newNotifies"] = newNotifies;
            List<T_NotifyFile> notifies = (List<T_NotifyFile>)new T_NotifyFileBLL().GetPageListOfIEnumerable(top, 1, 5, "");
            ViewData["notifies"] = notifies;
            return View();
        }
        public ActionResult MsgDetail(int id=1)
        {
            T_NotifyFile newNotify = new T_NotifyFileBLL().GetModel(id);
            ViewData["newNotify"] = newNotify;
            return View();
        }

        public ActionResult GetNotifiesByPage()
        {
            string page = Request["page"];
            if(string.IsNullOrEmpty(page))
            {
                page = "1";
            }
            List<T_NotifyFile> notifies = (List<T_NotifyFile>)new T_NotifyFileBLL().GetPageListOfIEnumerable(top, Convert.ToInt32( page), 5, "");
            
            return Content(jss.Serialize(notifies));
        }

        #region 发布信息通知

        public ActionResult PublishMessage()
        {
            return View();
        }
        public ActionResult GetNotifyFileStr()
        {
            List<T_NotifyFile> vocs = new T_NotifyFileBLL().GetModelList("");
            return Content(jss.Serialize(vocs));
        }

        public JsonResult GetNotifyFiles()
        {
            List<T_NotifyFile> vocs = new T_NotifyFileBLL().GetModelList("");
            return Json(vocs);
        }

        //删除通知文件
        public ActionResult DeleteNotify()
        {
            string result = "";
            string ID = Request["ID"];
            if (string.IsNullOrEmpty(ID))
            {
                return Content("Err|ID号不能为空");
            }
            if (new T_NotifyFileBLL().Delete(Convert.ToInt32(ID)))
            {
                return Content("OK|删除成功" + result);
            }
            else
            {
                return Content("Err|删除失败" + result);
            }
        }

        //保存通知信息
        public ActionResult SaveNotifyFile()
        {
            string ID = Request["ID"];
            string FTitle = Request["FTitle"];
            string FAbstract = Request["FAbstract"];
            string FAuthor = Request["FAuthor"];
            //string FDate = Request["FDate"];
            //string LinkWebFile = Request["LinkWebFile"];
            string Remark = Request["Remark"];

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                string extName = Path.GetExtension(f.FileName);
                if (!((".mht" == extName) || (".doc" == extName)))
                {
                    return Content("Err|文件类型不对,仅支持.mht和.doc，两种格式文件");
                }
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/Upload/" + fname);
                f.SaveAs(savePath);

                //获取MD5的文件名称
                string md5FileName = MD5Process.GetMD5HashFromFile(savePath);
                string lastName = Server.MapPath("~/Upload/") + md5FileName + extName;
                f.SaveAs(lastName);
                System.IO.File.Delete(savePath);

                T_NotifyFile model = new T_NotifyFile()
                {
                    FTitle = FTitle,
                    FAbstract = FAbstract,
                    FAuthor = FAuthor,
                    FDate = DateTime.Now,
                    LinkWebFile = "Upload/" + md5FileName + extName,
                    Remark = Remark
                };
                string addFlag = "|Update";
                if (string.IsNullOrEmpty(ID) == false)
                {
                    model.ID = Convert.ToInt32(ID);
                    new T_NotifyFileBLL().Update(model);
                }
                else
                {
                    addFlag = "|Add";
                    int i = new T_NotifyFileBLL().Add(model);
                    model.ID = i;
                }
                return Content("OK|保存成功" + addFlag + "|" + jss.Serialize(model));
            }

            return Content("Err|没有上传相应的通知文件");
        }
        #endregion
	}
}