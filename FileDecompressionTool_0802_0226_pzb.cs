// 代码生成时间: 2025-08-02 02:26:58
using System;
using System.IO;
using System.IO.Compression;

/// <summary>
/// A utility class for decompressing files.
/// </summary>
public class FileDecompressionTool
{
    /// <summary>
    /// Decompresses a file from a ZIP archive to a specified destination directory.
    /// </summary>
    /// <param name="zipFilePath">The path to the ZIP file.</param>
    /// <param name="destinationDirectory">The directory to extract the files to.</param>
    /// <returns>True if decompression is successful, otherwise False.</returns>
    public bool DecompressFile(string zipFilePath, string destinationDirectory)
    {
        try
        {
            // Ensure the ZIP file exists
            if (!File.Exists(zipFilePath))
            {
                throw new FileNotFoundException("The specified ZIP file does not exist.", zipFilePath);
            }

            // Ensure the destination directory exists
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            // Decompress the ZIP file to the destination directory
            ZipFile.ExtractToDirectory(zipFilePath, destinationDirectory);
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception details (in a real-world scenario, use a logging framework or service)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Main method for testing the decompression tool.
    /// </summary>
    static void Main(string[] args)
    {
        // Create an instance of the decompression tool
        FileDecompressionTool decompressor = new FileDecompressionTool();

        // Define the path to the ZIP file and the destination directory
        string zipFilePath = "path/to/your.zip";
        string destinationDirectory = "path/to/destination";

        // Attempt to decompress the file
        bool success = decompressor.DecompressFile(zipFilePath, destinationDirectory);

        // Output the result
        Console.WriteLine(success ? "Decompression successful." : "Decompression failed.");
    }
}