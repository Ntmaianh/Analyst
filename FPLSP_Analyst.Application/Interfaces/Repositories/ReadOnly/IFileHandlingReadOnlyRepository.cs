namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IFileHandlingReadOnlyRepository
    {

        // phương thuwsd này để lấy 
        Task<FileStream?> GetFileStreamAsync(string fileName, string folder);
    }
}
