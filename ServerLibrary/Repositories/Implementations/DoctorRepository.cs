using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations;

public class DoctorRepository(AppDbContext _dbContext) : IGenericRepositoryInterface<Doctor>
{
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var item = await _dbContext.Doctors.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
        if (item is null) return NotFound();

        _dbContext.Doctors.Remove(item);
        await Commit();
        return Success();
    }

    public async Task<Doctor> Get(int id) => await _dbContext
        .Doctors.FirstOrDefaultAsync(eid => eid.EmployeeId == id);



    public async Task<List<Doctor>> GetAll() => await _dbContext
        .Doctors
        .AsNoTracking()
        .ToListAsync();


    public async Task<GeneralResponse> Insert(Doctor item)
    {
        _dbContext.Doctors.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Doctor item)
    {
        var obj = await _dbContext .Doctors
            .FirstOrDefaultAsync(eid => eid.EmployeeId == item.EmployeeId);
        if (obj is null) return NotFound();
        obj.MedicalRecommendation = item.MedicalRecommendation;
        obj.Diagnosis = item.Diagnosis;
        obj.Date = item.Date;
        await Commit();
        return Success();
    }

    private static GeneralResponse NotFound() => new(false, "Sorry branch not found.");
    private static GeneralResponse Success() => new(true, "Process completed");


    private async Task Commit() => await _dbContext.SaveChangesAsync();
}

