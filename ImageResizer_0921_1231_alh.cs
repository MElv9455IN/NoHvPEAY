// 代码生成时间: 2025-09-21 12:31:14
using System;
using System.IO;
using System.Drawing;
# 增强安全性
using System.Drawing.Imaging;
using System.Linq;

namespace ImageBatchResizer
{
    public class ImageResizer
    {
        private string sourceFolder;
        private string destinationFolder;
        private int newWidth;
# NOTE: 重要实现细节
        private int newHeight;
# 添加错误处理

        // Constructor to initialize the ImageResizer with folders and dimensions.
        public ImageResizer(string sourceFolder, string destinationFolder, int newWidth, int newHeight)
# FIXME: 处理边界情况
        {
            this.sourceFolder = sourceFolder;
            this.destinationFolder = destinationFolder;
# 优化算法效率
            this.newWidth = newWidth;
            this.newHeight = newHeight;
        }
# NOTE: 重要实现细节

        // Resize all images in the source folder to the specified dimensions.
# TODO: 优化性能
        public void ResizeImages()
        {
            // Check if the source and destination folders exist.
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException($"Source folder not found: {sourceFolder}");
            }
            if (!Directory.Exists(destinationFolder))
            {
# 扩展功能模块
                Directory.CreateDirectory(destinationFolder); // Create the destination folder if it doesn't exist.
            }

            // Get all image files from the source folder.
            var imageFiles = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories)
                                .Where(file => IsImageFile(file));

            foreach (var filePath in imageFiles)
            {
                try
# 优化算法效率
                {
# FIXME: 处理边界情况
                    // Load the image from the file path.
                    using (var image = Image.FromFile(filePath))
                    {
                        // Create a new bitmap to hold the resized image.
# 优化算法效率
                        using (var newBitmap = new Bitmap(newWidth, newHeight))
                        {
                            // Draw the resized image onto the new bitmap.
                            using (var graphics = Graphics.FromImage(newBitmap))
                            {
                                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                                // Save the resized image to the destination folder.
                                var newFilePath = Path.Combine(destinationFolder, Path.GetFileName(filePath));
# NOTE: 重要实现细节
                                newBitmap.Save(newFilePath, image.RawFormat);
# NOTE: 重要实现细节
                            }
                        }
# TODO: 优化性能
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the resizing process.
                    Console.WriteLine($"Error resizing image {filePath}: {ex.Message}");
                }
            }
        }

        // Check if the file is an image based on its extension.
        private bool IsImageFile(string file)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return validExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
    }

    // Example usage of the ImageResizer class.
    class Program
    {
        static void Main(string[] args)
        {
            try
# 扩展功能模块
            {
# FIXME: 处理边界情况
                string sourceFolder = @"C:\Images\Source";
                string destinationFolder = @"C:\Images\Resized";
                int newWidth = 800;
                int newHeight = 600;

                ImageResizer resizer = new ImageResizer(sourceFolder, destinationFolder, newWidth, newHeight);
                resizer.ResizeImages();

                Console.WriteLine("Image resizing completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
# FIXME: 处理边界情况
}