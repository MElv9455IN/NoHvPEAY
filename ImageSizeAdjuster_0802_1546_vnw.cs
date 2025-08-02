// 代码生成时间: 2025-08-02 15:46:09
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

// 图片尺寸批量调整器
public class ImageSizeAdjuster
{
    // 构造函数，接收源目录和目标目录路径
    public ImageSizeAdjuster(string sourceDirectory, string targetDirectory)
    {
        this.SourceDirectory = sourceDirectory;
        this.TargetDirectory = targetDirectory;
    }

    // 源目录路径
    private string SourceDirectory { get; }

    // 目标目录路径
    private string TargetDirectory { get; }

    // 调整图片尺寸的方法
    public void AdjustImageSizes(int width, int height)
    {
        // 检查目标目录是否存在，不存在则创建
        if (!Directory.Exists(TargetDirectory))
        {
            Directory.CreateDirectory(TargetDirectory);
        }

        // 获取源目录下所有图片
        var imageFiles = Directory.GetFiles(SourceDirectory, "*").Where(f => IsImageFile(f));

        foreach (var file in imageFiles)
        {
            try
            {
                // 调整图片尺寸
                AdjustImageSize(file, Path.Combine(TargetDirectory, Path.GetFileName(file)), width, height);
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        }
    }

    // 判断文件是否为图片
    private bool IsImageFile(string filePath)
    {
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        return imageExtensions.Any(ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }

    // 调整单个图片尺寸
    private void AdjustImageSize(string sourcePath, string targetPath, int width, int height)
    {
        using (Image image = Image.FromFile(sourcePath))
        {
            using (Bitmap newImage = new Bitmap(width, height))
            {
                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    graphics.DrawImage(image, 0, 0, width, height);

                    // 保存调整后的图片
                    newImage.Save(targetPath, image.RawFormat);
                }
            }
        }
    }
}

// 使用示例
public class Program
{
    public static void Main()
    {
        string sourceDirectory = @"C:\source_images";
        string targetDirectory = @"C:\adjusted_images";
        int width = 800;
        int height = 600;

        var adjuster = new ImageSizeAdjuster(sourceDirectory, targetDirectory);
        adjuster.AdjustImageSizes(width, height);
    }
}