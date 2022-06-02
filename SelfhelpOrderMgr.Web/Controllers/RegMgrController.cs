using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class RegMgrController : Controller
    {
        //
        // GET: /RegMgr/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLicenseCode()
        {
            return View();
        }

        public ActionResult GetRegInfo()
        {
            return Content(LicenseHelper.GetRegInfo());
        }

        public ActionResult GetLicense()
        {
            string CheckCode = Request["CheckCode"];
            string RegCode = Request["RegCode"];
            string RegDays = Request["RegDays"];

            var _r = LicenseHelper.GetLicense(CheckCode, RegCode, Convert.ToInt32(RegDays));
            return Content(_r);
        }

        public ActionResult StartReg()
        {
            if (Request.Files.Count > 0)
            {
                //string strFBid = Request["excelBid"];
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/JvdLicenseCode.rar");
                f.SaveAs(savePath);
            }
            else
            {
                return Content("Err&没有上传授权文件");
            }

            //读取授权文件
            string licenseCode;
            try
            {
                string strFileName = Server.MapPath("~/JvdLicenseCode.rar");
                StreamReader sr = new StreamReader(strFileName);
                licenseCode = sr.ReadToEnd();
                sr.Close();
            }
            catch
            {
                return Content("Error|找不到授权文件");
            }
            if (licenseCode == null)
            {
                return Content("Error|无效授权文件");
            }
            char[] aa = "|".ToCharArray();
            string[] licenses = licenseCode.Split(aa);
            string regDate = licenses[1];
            HardwareInfo hwi = new HardwareInfo();
            string RegCode = hwi.GetHardDiskID();
            if (RegCode == "")
            {
                RegCode = "jdxxkj_059183754355";
            }
            try
            {

                DateTime dt = Convert.ToDateTime(regDate);
                string rcode = MD5Process.LicenseMD5_Date(RegCode, dt);
                if (rcode != licenseCode)
                {
                    return Content("Err&您输入的是无效授权码");
                }
                else
                {
                    T_SHO_ManagerSet tset = new T_SHO_ManagerSetBLL().GetModel("LicenseCode");
                    if(tset==null)
                    {
                        T_SHO_ManagerSet model = new T_SHO_ManagerSet();
                        model.KeyName = "LicenseCode";
                        model.KeyMode = 1;
                        model.MgrName = "注册码";
                        model.MgrValue = licenseCode;
                        model.StartTime = DateTime.Now;
                        model.Remark = "";
                        new T_SHO_ManagerSetBLL().Add(model);
                        return Content("OK&注册成功");                        
                    }
                    else
                    {
                        tset.MgrValue = licenseCode;
                        tset.StartTime = DateTime.Now;
                        tset.Remark = "";
                        if(new T_SHO_ManagerSetBLL().Update(tset))
                        {
                            return Content("OK&注册成功");
                        }
                        return Content("Err&注册失败");
                    }
                }
            }
            catch
            {
                return Content("Err&您输入的是无效授权码");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="ipAddr"></param>
        /// <returns></returns>
        public ActionResult GetBankMd5Key(string doPwd,string pwd, string ipAddr)
        {
            if (doPwd == "zenglj4355")
            {
                return Content(MD5ProcessHelper.GetMD5Token(pwd, ipAddr));
            }
            else
            {
                return Content("Err|验证码不正确");
            }
        }
	}
}