// 代码生成时间: 2025-09-17 13:35:27
// ImageResizer.cs
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageBatchResizer
{
    /// <summary>
    /// Class responsible for resizing images in a batch.
    /// </summary>
    public class ImageResizer
    {
        private readonly string _sourceFolder;
        private readonly string _destinationFolder;
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// Initializes a new instance of the ImageResizer class with specified parameters.
        /// </summary>
        /// <param name="sourceFolder">The folder where the original images are located.</param>
        /// <param name="destinationFolder">The folder where the resized images will be saved.</param>
        /// <param name="width">The width of the resized images.</param>
        /// <param name="height">The height of the resized images.</param>
        public ImageResizer(string sourceFolder, string destinationFolder, int width, int height)
        {
            _sourceFolder = sourceFolder;
            _destinationFolder = destinationFolder;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Resizes all images in the source folder and saves them to the destination folder.
        /// </summary>
        public void ResizeImages()
        {
            var imageFiles = Directory.GetFiles(_sourceFolder, "*.*").Where(f => IsImageFile(f));
            foreach (var file in imageFiles)
            {
                try
                {
                    ResizeImage(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while resizing the image {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Resizes a single image and saves it to the destination folder.
        /// </summary>
        /// <param name="imageFile">The file path of the image to resize.</param>
        private void ResizeImage(string imageFile)
        {
            using (var image = Image.FromFile(imageFile))
            {
                var resizedImage = new Bitmap(_width, _height);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    graphics.DrawImage(image, 0, 0, _width, _height);
                }
                string destinationPath = Path.Combine(_destinationFolder, Path.GetFileName(imageFile));
                resizedImage.Save(destinationPath, image.RawFormat);
            }
        }

        /// <summary>
        /// Checks if the file is an image based on its extension.
        /// </summary>
        /// <param name="filePath">The file path to check.</param>
        /// <returns>True if the file is an image, otherwise false.</returns>
        private bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif";
        }
    }
}
