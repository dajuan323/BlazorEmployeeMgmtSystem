using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class VacationRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<Vacation>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await _dbContext.Vacations.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
        if (item == null) return NotFound();

        _dbContext.Vacations.Remove(item);
        await Commit();
        return Success();

    }

    public async Task<Vacation> Get(int id) => await _dbContext
        .Vacations.FirstOrDefaultAsync(eid => eid.EmployeeId == id);

    public async Task<List<Vacation>> GetAll() => await _dbContext
        .Vacations
        .AsNoTracking().Include(t => t.VacationType)
        .ToListAsync();

    public async Task<GeneralResponse> Insert(Vacation item)
    {
        _dbContext.Vacations.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Vacation item)
    {
        var obj = _dbContext.Vacations.FirstOrDefault(eid => eid.EmployeeId == item.EmployeeId);
        if (obj is null) return NotFound();

        obj.StartDate = item.StartDate;
        obj.NumberOfDays = item.NumberOfDays;
        obj.VacationTypeId = item.VacationTypeId;
        await Commit();
        return Success();


    }

    private static GeneralResponse NotFound() => new(false, "Sorry data not found.");
    private static GeneralResponse Success() => new(true, "Process completed");
    private async Task Commit() => await _dbContext.SaveChangesAsync();
}
