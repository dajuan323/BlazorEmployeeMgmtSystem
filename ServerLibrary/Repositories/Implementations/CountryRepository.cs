using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class CountryRepository(AppDbContext _context) : IGenericRepositoryInterface<Country>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null) return NotFound();

        _context.Countries.Remove(country);
        await Commit();
        return Success();
    }

    public async Task<Country> Get(int id) => await _context.Countries.FindAsync(id);

    public async Task<List<Country>> GetAll() => await _context.Countries.ToListAsync();

    public async Task<GeneralResponse> Insert(Country item)
    {
        if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Country already exists");
        _context.Countries.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Country item)
    {
        var country = await _context.Countries.FindAsync(item.Id);
        if (country == null) return NotFound();
        country.Name = item.Name;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry branch not found.");
    private static GeneralResponse Success() => new(true, "Process completed");


    private async Task Commit() => await _context.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        var item = await _context.Branches.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
        return item is null;
    }
}
