using Microsoft.EntityFrameworkCore;
using Patienten.Models;

namespace Patienten.Data;

public class AppRepository
{
    private readonly AppDbContext _context;

    public AppRepository(AppDbContext context)
    {
        _context = context;
    }
}