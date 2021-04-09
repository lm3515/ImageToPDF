/*************************************************************************************
 * CLR版本：       4.0.30319.42000
 * 类 名 称：      Program
 * 机器名称：      9GX1UOWROPIAEJ4
 * 命名空间：      ImageToPDF
 * 文 件 名：      Program
 * 创建时间：      2020/11/26 11:51:27
 * 作    者：      Richard Liu
 * 说   明：。。。。。
 * 修改时间：      2020/11/26 11:51:27
 * 修 改 人：      Richard Liu
*************************************************************************************/


using System;


namespace ImageToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            Version();

            // 输入文件路径
            string[] inputFiles = null;
            // 输出文件路径
            string outputFile = null;

            if (args == null || args.Length < 2)
            {
                Console.WriteLine("文件路径错误");
                Environment.Exit(5);
                return;
            }

            if (args.Length > 10)
            {
                Console.WriteLine("一次最多支持9张图片");
                Environment.Exit(5);
                return;
            }

            // 输入文件路径
            inputFiles = new string[args.Length - 1];
            Array.Copy(args, inputFiles, args.Length - 1);

            // 输出文件路径
            outputFile = args[args.Length - 1];
            ImageTransferPDF(inputFiles, outputFile);
        }

        private static void ImageTransferPDF(string[] files, string pdfPath)
        {
            // 转换
            int exitCode = 0;
            Proces imageToPdf = null;

            try
            {
                imageToPdf = new Proces();
                imageToPdf.ImageToPDF(files, pdfPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                exitCode = 13;
            }
            finally
            {
                // 不管转换是否成功都退出WPS
                if (imageToPdf != null) { imageToPdf.Dispose(); }
            }

            if (exitCode != 0) Environment.Exit(exitCode);
        }

        static void Version()
        {
            Console.WriteLine(@"ImageToPDF - 将图片转换为PDF");
            Console.WriteLine(@"Copyright (c) 2020 印记（深圳）网络有限公司");
            Console.WriteLine(@"Copyright (c) 2020 Richard Liu");
            Console.WriteLine(@"版本：1.0");
            Console.WriteLine(@"支持的格式有：png、jpg、gif、bmp");
        }
    }
}
