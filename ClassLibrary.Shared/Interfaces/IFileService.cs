namespace ClassLibrary.Shared.Interfaces;

internal interface IFileService
{
    /// <summary>
    /// Save content to a specified file path
    /// </summary>
    /// <param name="filePath">Enter the file path with extension (eg. c:\projects\newfile.json)</param>
    /// <param name="content">Enter your content as a string</param>
    /// <returns>Returns true if saved, else false if failed</returns>
    bool SaveContentToFile(string filePath, string content);

    /// <summary>
    /// Get content as a string from a specified file path
    /// </summary>
    /// <param name="filePath">Enter the file path with extension (eg. c:\projects\newfile.json</param>
    /// <returns>Returns file content as a string if file exists, else returns null</returns>
    string GetContentFromFile(string filePath);
}
