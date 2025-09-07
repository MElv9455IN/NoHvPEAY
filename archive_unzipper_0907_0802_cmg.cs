// 代码生成时间: 2025-09-07 08:02:45
using System;
using System.IO;
using System.IO.Compression;

/// <summary>
/// A utility class to handle decompression of archives using the .NET Framework's classes.
/// </summary>
public class ArchiveUnzipper
{
    /// <summary>
    /// Extracts the contents of a compressed archive to a specified directory.
    /// </summary>
    /// <param name="archivePath">The path to the compressed archive file.</param>
    /// <param name="destinationDirectory">The path to the destination directory where the files will be extracted.</param>
    public void Unzip(string archivePath, string destinationDirectory)
    {
        // Check if the archive file exists
        if (!File.Exists(archivePath))
        {
            throw new FileNotFoundException($"The file {archivePath} does not exist.");
        }

        // Ensure the destination directory exists
        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }

        try
        {
            // Open the archive file for reading
            using (var archive = ZipFile.OpenRead(archivePath))
            {
                // Loop over all the files in the archive and extract them
                foreach (var entry in archive.Entries)
                {
                    // Construct the full path to where the file will be extracted
                    var fullOutputPath = Path.Combine(destinationDirectory, entry.FullName);

                    // Ensure the directory for the file exists
                    var directoryName = Path.GetDirectoryName(fullOutputPath);
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    // Extract the file
                    entry.ExtractToFile(fullOutputPath, true);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the extraction process
            throw new InvalidOperationException($"An error occurred while unzipping the file: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Example usage of the ArchiveUnzipper class.
/// </summary>
public class Program
{
    public static void Main()
    {
        try
        {
            var unzipper = new ArchiveUnzipper();
            var archivePath = "path_to_your_zip_file.zip"; // Replace with the actual path to the zip file.
            var destinationDirectory = "path_to_destination_directory"; // Replace with the actual path to the destination directory.

            unzipper.Unzip(archivePath, destinationDirectory);
            Console.WriteLine("Unzipping completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}