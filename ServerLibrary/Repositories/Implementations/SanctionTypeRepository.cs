using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class SanctionTypeRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<SanctionType>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        SanctionType? st = await _dbContext.SanctionTypes.FindAsync(id);
        if (st == null) return NotFound();

        _dbContext.SanctionTypes.Remove(st);
        await Commit();
        return Success();
    }

    public async Task<SanctionType?> Get(int id) => await _dbContext
        .SanctionTypes.FindAsync(id);

    public async Task<List<SanctionType>> GetAll() => await _dbContext
        .SanctionTypes
        .AsNoTracking()
        .ToListAsync();

    public async Task<GeneralResponse> Insert(SanctionType item)
    {
        if (!await CheckName(item.Name!))
            return new GeneralResponse(false, "Sanction Type already exists.");
        _dbContext.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(SanctionType item)
    {
        SanctionType? st = await _dbContext.SanctionTypes.FindAsync(item.Id);
        if (st is null) return NotFound();

        st.Name = item.Name;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry data not found.");
    private static GeneralResponse Success() => new(true, "Process completed");
    private async Task Commit() => await _dbContext.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        SanctionType? st = await _dbContext.SanctionTypes
            .FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        /*return st != null*/;
        return st is null ? true : false;
    }
}
