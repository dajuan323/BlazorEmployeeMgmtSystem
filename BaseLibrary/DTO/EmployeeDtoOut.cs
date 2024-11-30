using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTO;

public record EmployeeDtoOut
(
    int Id,
    string Name,
    string CivilId,
    string FileNumber,
    string JobName,
    string Address,
    string TelephoneNumber,
    string Photo,
    string Other,

    int BranchId,
    string Branch,
    string Department,
    string GeneralDepartment,
    
    int TownId,
    string Town,
    string City,
    string Country
    
);
