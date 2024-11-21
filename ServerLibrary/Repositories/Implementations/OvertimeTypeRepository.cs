using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class OvertimeTypeRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<OvertimeType>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await _dbContext.OvertimeTypes.FindAsync(id);
        if (item == null) return NotFound();

        _dbContext.OvertimeTypes.Remove(item);
        await Commit();
        return Success();
    }

    public async Task<OvertimeType> Get(int id) => await _dbContext.OvertimeTypes.FindAsync(id);

    public async Task<List<OvertimeType>> GetAll() => await _dbContext
        .OvertimeTypes
        .AsNoTracking()
        .ToListAsync();

    public async Task<GeneralResponse> Insert(OvertimeType item)
    {
        if (!await CheckName(item.Name!))
            return new GeneralResponse(false, "Overtime Type already exists");
        _dbContext.OvertimeTypes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(OvertimeType item)
    {
        OvertimeType? ot = await _dbContext.OvertimeTypes.FindAsync(item.Id);
        if (ot == null) return NotFound();

        ot.Name = item.Name;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry data not found.");
    private static GeneralResponse Success() => new(true, "Process completed");
    private async Task Commit() => await _dbContext.SaveChangesAsync();
    private async Task<bool> CheckName(string name)
    {
        OvertimeType? ot = await _dbContext.OvertimeTypes
            .FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
        return ot is null;
    }
}
