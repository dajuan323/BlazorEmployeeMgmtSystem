
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class BranchRepository(AppDbContext _context) : IGenericRepositoryInterface<Branch>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var branch = await _context.Branches.FindAsync(id);
        if (branch is null) return NotFound();

        _context.Branches.Remove(branch);
        await Commit();
        return Success();
    }



    public async Task<Branch> Get(int id) => await _context.Branches.FindAsync(id);

    public async Task<List<Branch>> GetAll() => await _context.Branches.ToListAsync();

    public async Task<GeneralResponse> Insert(Branch item)
    {
        if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Branch already exists");
        _context.Branches.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Branch item)
    {
        var branch = await _context.Branches.FindAsync(item.Id);
        if (branch is null) return NotFound();
        branch.Name = item.Name;
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
