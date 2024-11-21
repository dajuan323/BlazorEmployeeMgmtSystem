using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class VacationTypeRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<VacationType>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        VacationType? vt = await _dbContext.VacationTypes.FindAsync(id);
        if (vt is null) return NotFound();

        _dbContext.VacationTypes.Remove(vt);
        await Commit();
        return Success();
    }

    public async Task<VacationType?> Get(int id) => await _dbContext
        .VacationTypes.FindAsync(id);

    public async Task<List<VacationType>> GetAll() => await _dbContext
        .VacationTypes
        .AsNoTracking()
        .ToListAsync();

    public async Task<GeneralResponse> Insert(VacationType item)
    {
        if (!await CheckName(item.Name!))
            return new GeneralResponse(false, "Vacation Type already exists.");

        _dbContext.VacationTypes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(VacationType item)
    {
        VacationType? vt = await _dbContext.VacationTypes.FindAsync(item.Id);
        if (vt is null) return NotFound();

        vt.Name = item.Name;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry data not found.");
    private static GeneralResponse Success() => new(true, "Process completed");
    private async Task Commit() => await _dbContext.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        VacationType? vt = await _dbContext.VacationTypes
            .FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        return vt is null ? true : false; 
    }
}
