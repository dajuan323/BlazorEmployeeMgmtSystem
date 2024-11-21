using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class SanctionRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<Sanction>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        Sanction sanction = await _dbContext.Sanctions.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
        if (sanction == null) return NotFound();

        _dbContext.Sanctions.Remove(sanction);
        await Commit();
        return Success();
    }

    public async Task<Sanction> Get(int id) =>
        await _dbContext.Sanctions.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
    

    public async Task<List<Sanction>> GetAll() => await _dbContext
        .Sanctions
        .AsNoTracking().Include(t=> t.SanctionType)
        .ToListAsync();

    public async Task<GeneralResponse> Insert(Sanction item)
    {
        _dbContext.Sanctions.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Sanction item)
    {
        var obj = await _dbContext.Sanctions.FirstOrDefaultAsync(eid => eid.EmployeeId == item.EmployeeId);
        if (obj == null) return NotFound();

        obj.CorrectiveActionDate = item.CorrectiveActionDate;
        obj.CorrectiveAction = item.CorrectiveAction;
        obj.Date = item.Date;
        obj.SanctionType = item.SanctionType;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry data not found.");
    private static GeneralResponse Success() => new(true, "Process completed");
    private async Task Commit() => await _dbContext.SaveChangesAsync();
}
