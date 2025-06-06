using FileServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FileServer.Repositories;

public class UserFileRepository : IUserFileRepository
{
    private readonly AppDbContext _context;

    public UserFileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserFile>> GetFilesByUserIdAsync(string userId)
    {
        return await _context.UserFiles
            .Where(f => f.UserId == userId)
            .OrderByDescending(f => f.UploadDate)
            .ToListAsync();
    }

    public async Task<UserFile?> GetFileByIdAsync(int fileId, string userId)
    {
        return await _context.UserFiles
            .FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == userId);
    }

    public async Task AddFileAsync(UserFile file)
    {
        await _context.UserFiles.AddAsync(file);
    }

    public Task DeleteFileAsync(UserFile file)
    {
        _context.UserFiles.Remove(file);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}