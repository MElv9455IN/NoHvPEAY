// 代码生成时间: 2025-09-03 22:18:40
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Collections.Generic;

namespace ImageProcessing
{
    /// <summary>
    /// 图片尺寸批量调整器，用于调整文件夹内所有图片的尺寸。
    /// </summary>
    public class ImageResizer
    {
        private const string InputFolderPath = @"C:\Images\In"; // 输入文件夹路径
        private const string OutputFolderPath = @"C:\Images\Out"; // 输出文件夹路径
        private const int TargetWidth = 1024; // 目标宽度
        private const int TargetHeight = 768; // 目标高度

        /// <summary>
        /// 调整文件夹内所有图片的尺寸。
        /// </summary>
        public void ResizeImages()
        {
            if (!Directory.Exists(InputFolderPath))
            {
                Console.WriteLine(\$"输入文件夹 {InputFolderPath} 不存在。");
                return;
            }

            var images = Directory.GetFiles(InputFolderPath, "*.*", SearchOption.AllDirectories)
                .Where(image => IsImageFile(image));

            foreach (var image in images)
            {
                try
                {
                    ResizeImage(image, Path.Combine(OutputFolderPath, Path.GetFileName(image)));
                    Console.WriteLine($"图片 {Path.GetFileName(image)} 已调整尺寸并保存。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"图片 {Path.GetFileName(image)} 调整尺寸时出错：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 判断文件是否为图片文件。
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否为图片文件</returns>
        private bool IsImageFile(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp";
        }

        /// <summary>
        /// 调整图片尺寸。
        /// </summary>
        /// <param name="inputPath">图片输入路径</param>
        /// <param name="outputPath">图片输出路径</param>
        private void ResizeImage(string inputPath, string outputPath)
        {
            using (var image = Image.FromFile(inputPath))
            {
                var widthRatio = TargetWidth / (double)image.Width;
                var heightRatio = TargetHeight / (double)image.Height;
                var ratio = Math.Min(widthRatio, heightRatio);

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                newImage.Save(outputPath, image.RawFormat);
            }
        }
    }
}
