// 代码生成时间: 2025-09-17 23:58:34
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace BulkRenameUtility
{
    /// <summary>
    /// Class responsible for renaming files in bulk.
    /// </summary>
    public class BulkFileRenamer
    {
        private readonly string _sourceDirectory;
        private readonly string _targetDirectory;
        private readonly string _extension;
        private readonly string _prefix;
        private readonly string _dateFormat;
        private readonly int _startNumber;
        private readonly bool _overwriteExisting;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkFileRenamer"/> class.
        /// </summary>
        /// <param name="sourceDirectory">Directory containing files to rename.</param>
        /// <param name="targetDirectory">Directory to move renamed files to.</param>
        /// <param name="extension">File extension to target for renaming.</param>
        /// <param name="prefix">Prefix to add to the new file names.</param>
        /// <param name="dateFormat">Format for the date to be appended to the file names.</param>
        /// <param name="startNumber">Starting number for the file sequence.</param>
        /// <param name="overwriteExisting">Flag to indicate whether to overwrite existing files.</param>
        public BulkFileRenamer(string sourceDirectory, string targetDirectory, string extension, string prefix, string dateFormat, int startNumber, bool overwriteExisting)
        {
            _sourceDirectory = sourceDirectory;
            _targetDirectory = targetDirectory;
            _extension = extension;
            _prefix = prefix;
            _dateFormat = dateFormat;
            _startNumber = startNumber;
            _overwriteExisting = overwriteExisting;
        }

        /// <summary>
        /// Renames files in the source directory according to the specified parameters.
        /// </summary>
        public void RenameFiles()
        {
            var files = Directory.GetFiles(_sourceDirectory).Where(f => Path.GetExtension(f).Equals(_extension, StringComparison.OrdinalIgnoreCase));

            foreach (var file in files)
            {
                try
                {
                    var newFileName = GenerateNewFileName(file);
                    var targetFilePath = Path.Combine(_targetDirectory, newFileName);

                    if (File.Exists(targetFilePath) && !_overwriteExisting)
                    {
                        Console.WriteLine($"File {targetFilePath} already exists and overwriting is not allowed. Skipping...");
                        continue;
                    }

                    File.Move(file, targetFilePath);
                    Console.WriteLine($"Renamed {file} to {targetFilePath}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error renaming file {file}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Generates the new file name based on the prefix, date, and sequence number.
        /// </summary>
        /// <param name="filePath">Current file path.</param>
        /// <returns>New file name as a string.</returns>
        private string GenerateNewFileName(string filePath)
        {
            var dateTimeNow = DateTime.Now;
            var formattedDate = dateTimeNow.ToString(_dateFormat);
            var newFileNameWithoutExtension = $