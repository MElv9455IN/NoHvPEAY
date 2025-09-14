// 代码生成时间: 2025-09-14 15:06:25
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageProcessing
{
    // 图片尺寸批量调整器类
    public class ImageResizer
    {
        private const int DefaultWidth = 800; // 默认宽度
        private const int DefaultHeight = 600; // 默认高度

        // 调整单个图片尺寸的方法
        public void ResizeImage(string imagePath, string outputPath, int width, int height)
        {
            try
            {
                using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
                {
                    image.Mutate(x => x.Resize(width, height));
                    image.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error resizing image {imagePath}: {ex.Message}");
            }
        }

        // 批量调整图片尺寸的方法
        public void ResizeMultipleImages(string directoryPath, int width, int height)
        {
            try
            {
                // 获取目录下所有图片文件
                var files = Directory.GetFiles(directoryPath, "*.*")
                    .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg"));

                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string outputPath = Path.Combine(directoryPath, $"resized_{fileName}");

                    // 调整单个图片尺寸
                    ResizeImage(file, outputPath, width, height);
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error processing directory {directoryPath}: {ex.Message}");
            }
        }
    }
}
