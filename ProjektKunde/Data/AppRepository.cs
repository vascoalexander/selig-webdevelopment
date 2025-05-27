using ProjektKunde.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjektKunde.Data;

public class AppRepository
{
    private readonly AppDbContext _context;

    public AppRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _context.Projects
            .Include(c => c.Customer)
            .OrderBy(i => i.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _context.Customers
            .Include(c => c.Address)
            .OrderBy(i => i.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddresses()
    {
        return await _context.Addresses
            .Include(a => a.Customer)
            .OrderBy(i => i.Id)
            .ToListAsync();
    }

    public async Task<Project?> GetProjectById(int id)
    {
        return await _context.Projects.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Address?> GetAddressById(int id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task Add(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Project project)
    {
        _context.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Project project)
    {
        _context.Remove(project);
        await _context.SaveChangesAsync();
    }
}