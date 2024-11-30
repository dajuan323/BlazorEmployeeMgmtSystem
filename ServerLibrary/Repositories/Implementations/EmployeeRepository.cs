

using BaseLibrary.DTO;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations;

public class EmployeeRepository(AppDbContext _appDbContext) : IGenericRepositoryInterface<Employee>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        Employee? emp = await _appDbContext.Employees.FindAsync(id);
        if (emp is null) return NotFound();

        _appDbContext.Employees.Remove(emp);
        await Commit();
        return Success();
    }

    public async Task<List<Employee>> GetAll()
    {
        List<Employee>? employees = await _appDbContext.Employees
                .AsNoTracking()
                .Include(_ => _.Town)
                    .ThenInclude(_ => _.City)
                        .ThenInclude(_ => _.Country)
                .Include(_ => _.Branch)
                    .ThenInclude(_ => _.Department)
                        .ThenInclude(_ => _.GeneralDepartment)
                //.Select(_ => new EmployeeDTO
                //{
                //    _.Id,
                //    _.Name,
                //    _.CivilId,
                //    _.FileNumber,
                //    _.JobName,
                //    _.Address,
                //    _.TelephoneNumber,
                //    _.Photo,
                //    _.Other,
                //    BranchId = _.Branch.Id,
                //    BranchName = _.Branch.Name,
                //    DepartmentName = _.Branch.Department.Name,
                //    GeneralDepartmentName = _.Branch.Department.GeneralDepartment.Name,
                //    TownId = _.Town.Id,
                //    Town = _.Town.Name,
                //    City = _.Town.City.Name,
                //    Country = _.Town.City.Country.Name
                //})

                .ToListAsync();

        return employees;
    }

    public async Task<Employee> Get(int id)
    {
        Employee? emp = await _appDbContext.Employees
            .Include(_ => _.Town)
            .ThenInclude(_ => _.City)
            .ThenInclude(_ => _.Country)
            .Include(_ => _.Branch)
            .ThenInclude(_ => _.Department)
            .ThenInclude(_ => _.GeneralDepartment).FirstOrDefaultAsync(_=> _.Id == id)!;


        return emp!;
    }

    public async Task<GeneralResponse> Insert(Employee emp)
    {
        if (!await CheckName(emp.Name!)) return new GeneralResponse(false, "Employee already exists.");

        _appDbContext.Employees.Add(emp);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Employee emp)
    {
        var findUser = await _appDbContext.Employees.FirstOrDefaultAsync(_ => _.Id == emp.Id);
        if (findUser is null) return new GeneralResponse(false, "Employee does not exist.");

        findUser.Name = emp.Name;
        findUser.Other = emp.Other;
        findUser.Address = emp.Address;
        findUser.TelephoneNumber = emp.TelephoneNumber;
        findUser.BranchId = emp.BranchId;
        findUser.TownId = emp.TownId;
        findUser.CivilId = emp.CivilId;
        findUser.FileNumber = emp.FileNumber;
        findUser.JobName = emp.JobName;
        findUser.Photo = emp.Photo;
        await _appDbContext.SaveChangesAsync();
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry, employee not found.");

    private async Task Commit() => await _appDbContext.SaveChangesAsync();

    private static GeneralResponse Success() => new(true, "Process complete.");
    private async Task<bool> CheckName(string name)
    {
        Employee? item = await _appDbContext.Employees.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
        return item is null ? true : false;
    }
}
