using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<IEnumerable<Termin>> GetAllTermine()
    {
        return await _context.Termine
            .Include(t => t.Arzt)
            .Include(t => t.Patient)
            .OrderBy(t => t.TerminZeit)
            .ToListAsync();
    }

    public async Task<IEnumerable<Arzt>> GetAllAerzte()
    {
        return await _context.Aerzte
            .Include(a => a.Termine)
            .OrderBy(a => a.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Patient>> GetAllPatienten()
    {
        return await _context.Patienten
            .Include(p => p.Krankenkasse)
            .Include(p => p.Termine)
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
    public async Task<Arzt?> GetArztById(int id)
    {
        return await _context.Aerzte.FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<Patient?> GetPatientById(int id)
    {
        return await _context.Patienten.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Krankenkasse?> GetKrankenkasseById(int id)
    {
        return await _context.Krankenkassen.FirstOrDefaultAsync(k => k.Id == id);
    }
    public async Task<Termin?> GetTerminById(int id)
    {
        return await _context.Termine
            .Include(t => t.Arzt)
            .Include(t => t.Patient)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddTermin(Termin termin)
    {
        _context.Termine.Add(termin);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTermin(Termin termin)
    {
        _context.Termine.Update(termin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTermin(Termin termin)
    {
        _context.Termine.Remove(termin);
        await _context.SaveChangesAsync();
    }


}