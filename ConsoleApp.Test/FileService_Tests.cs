using ClassLibrary.Shared.Interfaces;
using ClassLibrary.Shared.Services;

namespace ConsoleApp.Test
{
    public class FileService_Tests
    {
        [Fact]
        public void SaveToFileShould_SaveContentToFile_ThenReturnTrue()
        {
            // Arrange 
            IFileService fileService = new FileService();
            string filePath = @"..\..\..\..\..\contacts_test.json";
            string content = "Test content";

            // Act
            bool result = fileService.SaveContentToFile(filePath, content);

            // Assert
            Assert.True(result);
        }
    }
}
