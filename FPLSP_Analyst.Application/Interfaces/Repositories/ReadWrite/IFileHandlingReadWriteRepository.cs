using FPLSP_Analyst.Application.ViewModels.Excels.Mics;
using Microsoft.AspNetCore.Http;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IFileHandlingReadWriteRepository
    {
        Task<ExcelOutputVM> ExcelImport(ExcelImportInputVM obj);
        Task<ExcelOutputVM> ExcelExport(ExcelExportVM obj);
        Task<string> SaveFileAsync(IFormFile file, string folder);
        string SaveFile(MemoryStream memoryStream, string fileName, string folder);
        bool RemoveFile(string relativeFilePath);
        List<string> RemoveAllFileFromFolder(string relativeFolderPath);

    }
}
