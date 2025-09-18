// 代码生成时间: 2025-09-18 20:17:33
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

// 文件备份和同步工具
public class FileBackupAndSyncTool
{
    // 源文件夹路径
    private readonly string sourceDirectory;
    // 目标文件夹路径
    private readonly string targetDirectory;

    // 构造函数，初始化源文件夹和目标文件夹路径
    public FileBackupAndSyncTool(string sourceDir, string targetDir)
    {
        sourceDirectory = sourceDir;
        targetDirectory = targetDir;
    }

    // 备份文件方法
    public void BackupFiles()
    {
        try
        {
            // 检查源文件夹是否存在
            if (!Directory.Exists(sourceDirectory))
            {
                throw new DirectoryNotFoundException($"The source directory {sourceDirectory} was not found.");
            }

            // 确保目标文件夹存在
            Directory.CreateDirectory(targetDirectory);

            // 获取源文件夹中的所有文件
            var files = Directory.GetFiles(sourceDirectory);

            // 循环遍历文件进行备份
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(targetDirectory, fileName);

                // 如果目标文件夹中不存在文件，则复制
                if (!File.Exists(destFile))
                {
                    File.Copy(file, destFile);
                }
                // 如果目标文件夹中存在文件，则更新（即覆盖）文件
                else
                {
                    File.Copy(file, destFile, true);
                }
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // 同步文件夹方法
    public void SyncFolders()
    {
        try
        {
            // 确保源文件夹和目标文件夹都存在
            if (!Directory.Exists(sourceDirectory) || !Directory.Exists(targetDirectory))
            {
                throw new DirectoryNotFoundException("You must specify both source and target directories.");
            }

            // 获取源文件夹和目标文件夹中的所有文件
            var sourceFiles = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            var targetFiles = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

            // 填充源文件夹文件信息
            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                string fileName = Path.GetFileName(file);
                sourceFiles[fileName] = File.GetLastWriteTime(file);
            }

            // 填充目标文件夹文件信息
            foreach (var file in Directory.GetFiles(targetDirectory))
            {
                string fileName = Path.GetFileName(file);
                targetFiles[fileName] = File.GetLastWriteTime(file);
            }

            // 同步文件
            foreach (var file in sourceFiles)
            {
                string fileName = file.Key;
                if (!targetFiles.ContainsKey(fileName))
                {
                    // 文件在目标文件夹中不存在，复制过去
                    File.Copy(Path.Combine(sourceDirectory, fileName), Path.Combine(targetDirectory, fileName));
                }
                else if (file.Value > targetFiles[fileName])
                {
                    // 文件在目标文件夹中存在，但是比源文件夹中的旧，覆盖它
                    File.Copy(Path.Combine(sourceDirectory, fileName), Path.Combine(targetDirectory, fileName), true);
                }
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
