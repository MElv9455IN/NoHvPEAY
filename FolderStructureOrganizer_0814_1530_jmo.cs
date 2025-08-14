// 代码生成时间: 2025-08-14 15:30:12
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FolderStructureOrganizer
{
    /// <summary>
    /// Class responsible for organizing the folder structure.
    /// </summary>
    public class FolderStructureOrganizer
    {
        private readonly string _rootFolderPath;

        /// <summary>
        /// Initializes a new instance of the FolderStructureOrganizer class.
        /// </summary>
        /// <param name="rootFolderPath">The root folder path to organize.</param>
        public FolderStructureOrganizer(string rootFolderPath)
        {
            _rootFolderPath = rootFolderPath;
        }

        /// <summary>
        /// Organizes the folder structure by moving files into subfolders based on their extensions.
        /// </summary>
        public void Organize()
        {
            try
            {
                if (!Directory.Exists(_rootFolderPath))
                {
                    throw new DirectoryNotFoundException($"The directory {_rootFolderPath} was not found.");
                }

                var files = Directory.GetFiles(_rootFolderPath);

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    var extension = fileInfo.Extension;

                    // Skip directories and hidden files
                    if (fileInfo.Attributes.HasFlag(FileAttributes.Directory) || fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        continue;
                    }

                    var subfolderPath = Path.Combine(_rootFolderPath, extension.TrimStart('.'));

                    if (!Directory.Exists(subfolderPath))
                    {
                        Directory.CreateDirectory(subfolderPath);
                    }

                    var destFile = Path.Combine(subfolderPath, fileInfo.Name);
                    File.Move(file, destFile);
                }
            }
            catch (Exception ex)
            {
                // Here you can handle different types of exceptions and log them as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var organizer = new FolderStructureOrganizer("C:\PathToYourRootFolder");
            organizer.Organize();
        }
    }
}
