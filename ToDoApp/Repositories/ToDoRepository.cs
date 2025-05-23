using ToDoApp.Models;
using ToDoApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Repositories;

public class ToDoRepository
{
    private readonly ApplicationDbContext _context;
    public ToDoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ToDoItem>> GetAll()
    {
        return await _context.ToDoItems.OrderBy(i => i.Id).ToListAsync();
    }

    public async Task<ToDoItem?> GetById(int id)
    {
        return await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task Add(ToDoItem item)
    {
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ToDoItem item)
    {
        _context.ToDoItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var itemToRemove = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
        if (itemToRemove != null)
        {
            _context.ToDoItems.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }
    }
}