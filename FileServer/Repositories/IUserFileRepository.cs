using FileServer.Models;

namespace FileServer.Repositories;

public interface IUserFileRepository
{
    Task<IEnumerable<UserFile>> GetFilesByUserIdAsync(string userId);
    Task<UserFile?> GetFileByIdAsync(int fileId, string userId);
    Task AddFileAsync(UserFile file);
    Task DeleteFileAsync(UserFile file);
    Task SaveChangesAsync();
}