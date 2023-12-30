using ClassLibrary.Shared.Interfaces;
using System.Diagnostics;

namespace ClassLibrary.Shared.Services;

public class FileService : IFileService
{
    public string GetContentFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath)) // Check if file exists
            {
                return File.ReadAllText(filePath); // Read the content of the file
            }
        }
        catch (Exception ex) { Debug.WriteLine("FileService - ReadContentFromFile:: " + ex.Message); }
        return null!;
    }

    public bool SaveContentToFile(string filePath, string content)
    {
        try
        {
            using var sw = new StreamWriter(filePath); // Open a StreamWriter to write to the file
            sw.Write(content); // Write the content to the file
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("FileService - SaveContentToFile:: " + ex.Message); }
        return false;
    }
}
