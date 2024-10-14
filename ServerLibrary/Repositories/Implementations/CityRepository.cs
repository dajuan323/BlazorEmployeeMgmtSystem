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

public class CityRepository(AppDbContext _context) : IGenericRepositoryInterface<City>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city == null ) return NotFound();

        _context.Cities.Remove(city);
        await Commit();
        return Success();
    }

    public async Task<City> Get(int id) => await _context.Cities.FindAsync(id);

    public async Task<List<City>> GetAll() => await _context.Cities.ToListAsync();

    public async Task<GeneralResponse> Insert(City item)
    {
        if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Sorry, city already exists");
        _context.Cities.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(City item)
    {
        var city = await _context.Cities.FindAsync(item.Id);
        if (city == null ) return NotFound();

        city.Name = item.Name;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry city not found.");
    private static GeneralResponse Success() => new(true, "Process completed");


    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Cities.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}
