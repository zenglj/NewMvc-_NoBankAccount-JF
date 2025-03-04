using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

namespace SelfhelpOrderMgr.YuZhengJieKou
{
    public class Base64ConvertHelper
    {
        public static void ConvertFromBase64ToImage(string base64String, string filePath)
        {
            // 将Base64字符串转换为字节数组
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // 使用MemoryStream将字节数组转换为图片
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // 使用Bitmap类从MemoryStream创建图片
                using (Bitmap image = new Bitmap(ms))
                {
                    // 保存图片到文件
                    image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
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

                //MemoryStream ms = new MemoryStream(arr);
                ////Bitmap bmp = new Bitmap(ms);
                //Image mImage = Image.FromStream(ms);
                //Bitmap bmp = new Bitmap(mImage);
                //string strExtName = FileNameHelper.GetFileExtName(txtFileName);

                //ImageFormat _imageFormat = ImageFormat.Png;
                //switch (strExtName)
                //{
                //    case "jpg":
                //        {
                //            _imageFormat = ImageFormat.Jpeg;
                //        }
                //        break;
                //    case "bmp":
                //        {
                //            _imageFormat = ImageFormat.Bmp;

                //        }
                //        break;
                //    case "gif":
                //        {
                //            _imageFormat = ImageFormat.Gif;
                //        }
                //        break;
                //    case "png":
                //        {
                //            _imageFormat = ImageFormat.Png;
                //        }
                //        break;
                //}

                ////bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                ////bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                ////bmp.Save(txtFileName + ".gif", ImageFormat.Gif);

                //bmp.Save(txtFileName, _imageFormat);
                //ms.Close();

                File.WriteAllBytes(txtFileName, arr);

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

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="base64Image">base64原图</param>
        /// <param name="newWidth">新宽度</param>
        /// <param name="newHeight">新高度</param>
        /// <returns></returns>
        public static string ConvertBase64ImageToSmallerSize(string base64Image, int newWidth, int newHeight)
        {
            // 将Base64字符串转换为字节数组
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // 使用MemoryStream将字节数组转换为Image对象
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                using (Image image = Image.FromStream(ms))
                {
                    // 创建一个新的Bitmap对象，指定新的宽度和高度
                    using (Bitmap bitmap = new Bitmap(image, newWidth, newHeight))
                    {
                        // 使用MemoryStream将Bitmap对象转换回字节数组
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            // 将Bitmap对象保存为JPEG格式（或其他格式）到MemoryStream中
                            bitmap.Save(outputStream, ImageFormat.Jpeg);

                            // 将MemoryStream转换为字节数组
                            byte[] outputBytes = outputStream.ToArray();

                            // 将字节数组编码为Base64字符串
                            return Convert.ToBase64String(outputBytes);
                        }
                    }
                }
            }
        }
        //调用方法
        //string base64SmallImage = ConvertBase64ImageToSmallerSize(base64LargeImage, 200, 200); // 设定新图像的宽度和高度
        //Console.WriteLine("小图的Base64字符串：");
        //Console.WriteLine(base64SmallImage);
    }
}
// 使用方法
//string base64String = "你的Base64字符串";
//string filePath = "图片保存路径";
//Base64ToImage.ConvertFromBase64ToImage(base64String, filePath);