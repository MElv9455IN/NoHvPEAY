// 代码生成时间: 2025-09-07 02:31:16
 * into their respective folders based on file extensions.
 *
 * It follows C# best practices, has clear code structure, includes error handling,
 * and is well-documented for maintainability and extensibility.
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FolderOrganizer
{
    public class FolderStructureOrganizer
    {
        private readonly string _sourceDirectory;
        private readonly Dictionary<string, string> _fileExtensionToFolder;

        public FolderStructureOrganizer(string sourceDirectory)
        {
            _sourceDirectory = sourceDirectory;
            _fileExtensionToFolder = new Dictionary<string, string>();
        }

        // Add file extension to folder mapping
        public void AddExtensionMapping(string extension, string folderName)
        {
            _fileExtensionToFolder[extension] = folderName;
        }

        // Organize the folder structure
        public void Organize()
        {
            if (!Directory.Exists(_sourceDirectory))
            {
                throw new DirectoryNotFoundException($"The source directory {_sourceDirectory} was not found.");
            }

            var files = Directory.GetFiles(_sourceDirectory);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var extension = fileInfo.Extension;

                if (_fileExtensionToFolder.ContainsKey(extension))
                {
                    var folderPath = Path.Combine(_sourceDirectory, _fileExtensionToFolder[extension]);
                    Directory.CreateDirectory(folderPath); // Create folder if it does not exist

                    var destinationPath = Path.Combine(folderPath, fileInfo.Name);
                    File.Move(file, destinationPath); // Move file to the new folder
                }
                else
                {
                    Console.WriteLine($"No folder mapping found for file: {fileInfo.Name}. Skipping...
