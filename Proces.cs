/*************************************************************************************
 * CLR版本：       4.0.30319.42000
 * 类 名 称：      Proces
 * 机器名称：      9GX1UOWROPIAEJ4
 * 命名空间：      ImageToPDF
 * 文 件 名：      Proces
 * 创建时间：      2020/11/26 11:51:27
 * 作    者：      Richard Liu
 * 说   明：。。。。。
 * 修改时间：      2020/11/26 11:51:27
 * 修 改 人：      Richard Liu
*************************************************************************************/


using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ImageToPDF
{
    class Proces : IDisposable
    {
        public Proces()
        {

        }

        public void ImageToPDF(string[] files, string pdfPath)
        {
            // 边距
            int margin = 20;

            // 创建一个新的文档对象
            Document document = new Document(PageSize.A4, margin, margin, margin, margin);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.Read));

                document.Open();
                Image image = null;

                for (int i = 0; i < files.Length; i++)
                {
                    if (String.IsNullOrEmpty(files[i]))
                    {
                        Console.WriteLine("目录不存在！");
                        break;
                    }

                    image = Image.GetInstance(files[i]);
                    // 宽度大于高度
                    float srcWidth = image.Width;
                    float srcHeight = image.Height;
                    if (image.Width > image.Height)
                    {
                        // 旋转
                        image.RotationDegrees = 90;
                        image.Rotate();

                        float tmp = srcHeight;
                        srcHeight = srcWidth;
                        srcWidth = tmp;
                    }

                    float A4_WIDTH = 595.27563f;
                    float A4_HEIGHT = 841.8898f;
                    float baseScale = 0.70707f;// width/height
                    float picScale = srcWidth / srcHeight;

                    float x = 0;
                    float y = 0;
                    float expectWidth = 0.0f;
                    float expectHeight = 0.0f;
                    if (picScale > baseScale)
                    {
                        // 左右填满，上下等比缩放
                        x = margin;
                        expectWidth = A4_WIDTH - 2 * margin;
                        expectHeight = expectWidth / picScale;
                        y = (A4_HEIGHT - expectHeight) / 2;// margin top
                    }
                    else
                    {
                        // 上下填满，左右等比缩放
                        y = margin;
                        expectHeight = A4_HEIGHT - 2 * margin;
                        expectWidth = expectHeight * picScale;
                        x = (A4_WIDTH - expectWidth) / 2;
                    }
                    image.SetAbsolutePosition(x, y);
                    image.ScaleToFit(expectWidth, expectHeight);

                    //image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - margin * 2, iTextSharp.text.PageSize.A4.Height - margin * 2);
                    //image.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    // 设置DPI
                    //image.SetDpi(72, 72);

                    document.NewPage();
                    document.Add(image);
                }
                Console.WriteLine("转换成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("转换失败，原因：" + ex.Message);
            }

            document.Close();
        }

        public void Dispose()
        {

        }
    }
}
