using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class FaceController : Controller
    {
        // GET: Face
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 注册脸谱
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="pic"></param>
        /// <returns></returns>
        //[LoginActionFilter(ignore=true)]

        public ActionResult  FaceGather(string fcrimecode, HttpPostedFileBase pic)
        {
            ResultInfo rs = new ResultInfo();
            string fname = pic.FileName;
            string extName = Path.GetExtension(pic.FileName);

            if (!(extName == ".jpg" || extName == ".png"))
            {
                rs.ReMsg = $"Err|只支持jpg和png格式的图片";
                rs.DataInfo = null;
                return Json(rs);
            }

            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Server.MapPath("~/Upload/UserImages/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = fcrimecode + "." + extName;
            var filePath = Path.Combine(path, fileName);

            pic.SaveAs(filePath);

            // 转换图片为Base64字符串
            using (var memoryStream = new MemoryStream())
            {
                pic.InputStream.CopyTo(memoryStream);
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // 返回Base64字符串（或进行其他处理）
                rs.DataInfo = base64String;

                T_Criminal _criminal = null;
                if (!string.IsNullOrWhiteSpace(fcrimecode))
                {
                    _criminal = new BaseDapperBLL().QueryModel<T_Criminal>("fcode", fcrimecode);
                    if (_criminal == null)
                    {
                        rs.ReMsg = "Err|编号不存在";
                        return Json(rs);
                    }
                }

                rs = FaceCheckService.SendAndCheckFace(fcrimecode, base64String, "0002", _criminal, 0, null);

                return Json(rs);
            }


        }

        public ActionResult FaceGatherByBase64(FaceRegBase faceRegBase)
        {
            ResultInfo rs = new ResultInfo();

            Log4NetHelper.logger.Info($"接收到同步接口人脸注册请求：fcrimecode:{faceRegBase.fcrimecode},base64Image:{faceRegBase.base64Image}");//写入连接记录
            // 转换图片为Base64字符串
            using (var memoryStream = new MemoryStream())
            {

                // 返回Base64字符串（或进行其他处理）
                rs.DataInfo = faceRegBase.base64Image;

                T_Criminal _criminal = null;
                if (!string.IsNullOrWhiteSpace(faceRegBase.fcrimecode))
                {
                    _criminal = new BaseDapperBLL().QueryModel<T_Criminal>("fcode", faceRegBase.fcrimecode);
                    if (_criminal == null)
                    {
                        rs.ReMsg = "Err|编号不存在";
                        return Json(rs);
                    }
                }

                rs = FaceCheckService.SendAndCheckFace(faceRegBase.fcrimecode, faceRegBase.base64Image, "0002", _criminal, 0, null);
                Log4NetHelper.logger.Info($"请求结果：{Newtonsoft.Json.JsonConvert.SerializeObject(rs)}");//写入连接记录

                return Json(rs);
            }


        }

        public class FaceRegBase
        {
            public string fcrimecode { get; set; }
            public string base64Image { get; set; }

        }
    }

 
}