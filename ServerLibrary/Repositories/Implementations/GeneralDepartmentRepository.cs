using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class GeneralDepartmentRepository(AppDbContext _context) : IGenericRepositoryInterface<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await _context.GeneralDepartments.FindAsync(id);
            if (dep is null) return NotFound();

            _context.GeneralDepartments.Remove(dep);
            await Commit();
            return Success();
                
        
        }

        public async Task<GeneralDepartment> Get(int id) => await _context.GeneralDepartments.FindAsync(id);

        public async Task<List<GeneralDepartment>> GetAll() => await _context.GeneralDepartments.ToListAsync();

        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            var checkIfNull = await CheckName(item.Name);
            if (!checkIfNull)
                return new GeneralResponse(false, "General Department already exists");
            _context.GeneralDepartments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var dep = await _context.GeneralDepartments.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, general department not found.");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task Commit() => await _context.SaveChangesAsync();
        private async Task<bool> CheckName(string name)
        {
            var item = await _context.GeneralDepartments.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
            return item is null ? true : false;
        }
    }
}
