
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Web.CommonHeler
{

    public class Base64ToImageHelper
    {

        //图片 转为    base64编码的文本
        public static void ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                //this.pictureBox1.Image = bmp;
                FileStream fs = new FileStream(Imagefilename + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                sw.Write(strbaser64);

                sw.Close();
                fs.Close();
                // MessageBox.Show("转换成功!");
            }
            catch (Exception )
            {
                //MessageBox.Show("ImgToBase64String 转换失败\nException:" + ex.Message);
            }
        }

        public static string ImgToBase64StringByReturn(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                //this.pictureBox1.Image = bmp;
                FileStream fs = new FileStream(Imagefilename + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                sw.Write(strbaser64);

                sw.Close();
                fs.Close();
                return strbaser64;
                // MessageBox.Show("转换成功!");
            }
            catch (Exception)
            {
                return "Err|转换失败";
                //MessageBox.Show("ImgToBase64String 转换失败\nException:" + ex.Message);
            }
        }

        //base64编码的文本 转为    图片
        public static ResultInfo Base64StringToImage(string base64String, string txtFileName)
        {
            ResultInfo rs = new ResultInfo();
            try
            {

                base64String = base64String.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "")
                    .Replace("data:image/jpeg;base64,", "").Replace("data:image/png;", "");//

                byte[] arr = Convert.FromBase64String(base64String);

                MemoryStream ms = new MemoryStream(arr);
                //Bitmap bmp = new Bitmap(ms);
                Image mImage = Image.FromStream(ms);
                Bitmap bmp = new Bitmap(mImage);
                string strExtName = FileNameHelper.GetFileExtName(txtFileName);

                ImageFormat _imageFormat= ImageFormat.Png;
                switch (strExtName)
                {
                    case "jpg":
                        {
                            _imageFormat = ImageFormat.Jpeg;
                        }
                        break;
                    case "bmp":
                        {
                            _imageFormat = ImageFormat.Bmp;

                        }
                        break;
                    case "gif":
                        {
                            _imageFormat = ImageFormat.Gif;
                        }
                        break;
                    case "png":
                        {
                            _imageFormat = ImageFormat.Png;
                        }
                        break;
                }

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                bmp.Save(txtFileName , _imageFormat);
                ms.Close();

                rs.Flag = true;
                rs.ReMsg = "转换成功";
                rs.DataInfo = txtFileName;

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
                rs.ReMsg = "Err|" + ex.Message;
            }
            return rs;
        }


        public static ResultInfo Base64StringToBitmap(string base64String)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                //base64String = base64String.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "")
                //    .Replace("data:image/jpeg;base64,", "").Replace("data:image/png;", "");//
                //                                                                           //String inputStr = sr.ReadToEnd();
                string strExtName = "png";
                ImageFormat _imageFormat = ImageFormat.Png;
                if (base64String.StartsWith("data:image/png;"))
                {
                    base64String = base64String.Replace("data:image/png;base64,", "").Replace("data:image/png;", "");//
                    strExtName = "png";
                    _imageFormat = ImageFormat.Png;
                }
                else if (base64String.StartsWith("data:image/jpeg;"))
                {
                    base64String = base64String.Replace("data:image/jpeg;base64,", "").Replace("data:image/jpeg;", "");//
                    strExtName = "jpg";
                    _imageFormat = ImageFormat.Jpeg;

                }
                else if(base64String.StartsWith("data:image/jpg;"))
                {
                    base64String = base64String.Replace("data:image/jpg;base64,", "").Replace("data:image/jpg;", "");//
                    strExtName = "jpg";
                    _imageFormat = ImageFormat.Jpeg;

                }
                else if (base64String.StartsWith("data:image/bmp;"))
                {
                    base64String = base64String.Replace("data:image/bmp;base64,", "").Replace("data:image/bmp;", "");//
                    strExtName = "bmp";
                    _imageFormat = ImageFormat.Bmp;
                }
                else
                {
                    rs.ReMsg = "图片只支持png、jpg、bmp格式";
                }
                //这个显示日志文件太大了
                //LogUtil.LogInfo(typeof(string), $"识别到的base64String：{base64String}");

                byte[] arr = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(arr);
                Image mImage = Image.FromStream(ms);
                Bitmap bmp = new Bitmap(ms);

                string filePath = System.IO.Directory.GetCurrentDirectory() + "\\TempFaceDir\\";
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }

                //Guid _guid = Guid.NewGuid();
                string tempFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //string image1Path = filePath + "TempCheckImage."+ strExtName;
                string image1Path = filePath + $"{tempFileName}." + strExtName;
                bmp.Save(image1Path, _imageFormat);
                CustomBimapInfo bimapInfo = new CustomBimapInfo() { 
                    bmp=bmp,
                    ExtName=strExtName,
                    tmpFaceFileName= image1Path
                };

                rs.Flag = true;
                rs.ReMsg = "转换成功";
                //rs.DataInfo = bmp;
                rs.DataInfo = bimapInfo;
                ms.Close();
                //return bmp;          
                
            }
            catch (Exception ex)
            {
                //throw ex;
                //MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
                rs.Flag = false;
                rs.ReMsg = "转换失败:"+ex.Message;
                rs.DataInfo = null;
            }
            return rs;
        }


        /// <summary>
        /// Bitmap转字节流
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap b)
        {
            MemoryStream ms = new MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
            ms.Close();
            return bytes;
        }

        /// <summary>
        /// 获取图片的字节大小
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Int32 GetBitmapSize(Bitmap b)
        {
            byte[] bytelist=BitmapToBytes(b);
            return bytelist.Length;
        }

        /// <summary>
        /// 字节流数组转Bitmap
        /// </summary>
        /// <param name="bytelist"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] bytelist)
        {
             MemoryStream   ms1   =   new   MemoryStream(bytelist); 
             Bitmap   bm   =   (Bitmap)Image.FromStream(ms1); 
             ms1.Close();
            return bm;
        }

        
    }
}
