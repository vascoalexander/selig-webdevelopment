using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Repositories;

public class AppRepository
{
    private readonly AppDbContext _context;

    public AppRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Article?> GetById(int id)
    {
        return await _context.Articles.FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task Add(Article item)
    {
        _context.Articles.Add(item);
        await _context.SaveChangesAsync();
    }
}