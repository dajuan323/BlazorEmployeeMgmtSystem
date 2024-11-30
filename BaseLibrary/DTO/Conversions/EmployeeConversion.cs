using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTO.Conversions;

public static class EmployeeConversion
{
    public static EmployeeDtoOut? ToEntityDTO(Employee? employee)
    {
        EmployeeDtoOut? singleEmp = null;
        if (employee is not null)
        {
            singleEmp = new
                (
                    employee.Id,
                    employee.Name!,
                    employee.CivilId!,
                    employee.FileNumber!,
                    employee.JobName!,
                    employee.Address!,
                    employee.TelephoneNumber!,
                    employee.Photo!,
                    employee.Other!,
                    employee.BranchId,
                    employee.Branch!.Name!,
                    employee.Branch!.Department!.Name!,
                    employee.Branch!.Department!.GeneralDepartment!.Name!,
                    employee.TownId,
                    employee.Town!.Name!,
                    employee.Town!.City!.Name!,
                    employee.Town!.City!.Country!.Name!
                );

            
        }
        return (singleEmp);

        //if (employee is null && employees is not null)
        //{
        //    var _employees = employees?.Select(_ =>
        //        new EmployeeDtoOut(
        //            _.Id,
        //            _.Name!,
        //            _.CivilId!,
        //            _.FileNumber!,
        //            _.JobName!,
        //            _.Address!,
        //            _.TelephoneNumber!,
        //            _.Photo!,
        //            _.Other!,
        //            _.BranchId,
        //            _.Branch!.Name!,
        //            _.Branch!.Department!.Name!,
        //            _.Branch!.Department!.GeneralDepartment!.Name!,
        //            _.TownId,
        //            _.Town!.Name!,
        //            _.Town!.City!.Name!,
        //            _.Town!.City!.Country!.Name!));

        //    return (_employees);
        //}

        //return (null, null);
    }
}


//public int Id { get; set; }
//public string? Name { get; set; } = string.Empty;
//public string? CivilId { get; set; } = string.Empty;
//public string? FileNumber { get; set; } = string.Empty;
//public string? JobName { get; set; } = string.Empty;
//public string? Address { get; set; } = string.Empty;
//public string? TelephoneNumber { get; set; } = string.Empty;
//public string? Photo { get; set; } = string.Empty;
//public string? Other { get; set; } = string.Empty;

//public string? Branch { get; set; } = string.Empty;
//public string? Department { get; set; } = string.Empty;
//public string? GeneralDepartment { get; set; } = string.Empty;

//public string? Town { get; set; } = string.Empty;
//public string? City { get; set; } = string.Empty;
//public string? Country { get; set; } = string.Empty;




// EMPLOYEE

//[Required]
//public string? CivilId { get; set; } = string.Empty;
//[Required]
//public string? FileNumber { get; set; } = string.Empty;
//[Required]
//public string? JobName { get; set; } = string.Empty;
//[Required]
//public string? Address { get; set; } = string.Empty;
//[Required, DataType(DataType.PhoneNumber)]
//public string? TelephoneNumber { get; set; } = string.Empty;
//[Required]
//public string? Photo { get; set; } = string.Empty;
//public string? Other { get; set; } = string.Empty;

//public Branch? Branch { get; set; }
//[Required]
//public int BranchId { get; set; }
//public Town? Town { get; set; }
//public int TownId { get; set; }