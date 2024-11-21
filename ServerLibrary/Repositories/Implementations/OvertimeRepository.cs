using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations;

public class OvertimeRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<Overtime>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await _dbContext.Overtimes.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
        if (item == null) return NotFound();

        _dbContext.Overtimes.Remove(item);
        await Commit();
        return Success();
    }

    public async Task<Overtime> Get(int id) => await _dbContext
        .Overtimes.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
    

    public async Task<List<Overtime>> GetAll() => await _dbContext
        .Overtimes
        .AsNoTracking().Include(t => t.OvertimeType)
        .ToListAsync();

    public async Task<GeneralResponse> Insert(Overtime item)
    {
        _dbContext.Overtimes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Overtime item)
    {
        var obj = await _dbContext.Overtimes
            .FirstOrDefaultAsync(eid => eid.EmployeeId == item.EmployeeId);
        if (obj == null) return NotFound();
        obj.StartDate = item.StartDate;
        obj.EndDate = item.EndDate;
        obj.OvertimeTypeId = item.OvertimeTypeId;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry data not found.");
    private static GeneralResponse Success() => new(true, "Process completed");


    private async Task Commit() => await _dbContext.SaveChangesAsync();
}
