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
    public async Task<Article?> GetArticleById(int id)
    {
        return await _context.Articles.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Author?> GetAuthorById(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        return await _context.Articles
            .Include(a => a.Author)
            .OrderBy(i => i.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.OrderBy(i => i.Id).ToListAsync();
    }
    public async Task Add(Article item)
    {
        _context.Articles.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Article article)
    {
        _context.Update(article);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Article article)
    {
        _context.Remove(article);
        await _context.SaveChangesAsync();
    }
}