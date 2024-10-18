using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations;

public class TownRepository(AppDbContext _context) : IGenericRepositoryInterface<Town>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var town = await _context.Towns.FindAsync(id);
        if (town is null) return NotFound();

        _context.Towns.Remove(town);
        await Commit();
        return Success();
    }

    public async Task<Town> Get(int id) => await _context.Towns.FindAsync(id);

    public async Task<List<Town>> GetAll() => await _context
        .Towns
        .AsNoTracking()
        .Include(t => t.City)
        .ToListAsync();

    public async Task<GeneralResponse> Insert(Town item)
    {
        if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Sorry, town already exists.");
        _context.Towns.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Town item)
    {
        var town = await _context.Towns.FindAsync(item.Id);
        if (town is null) return NotFound();

        town.Name = item.Name;
        town.CityId = item.CityId;
        await Commit();
        return Success();
        {
            
        }
    }

    private static GeneralResponse NotFound() => new(false, "Sorry town not found.");
    private static GeneralResponse Success() => new(true, "Process completed");


    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Towns.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}
