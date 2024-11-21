using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTO;

public record EmployeeDTO
{
    [Required]
    public string? CivilId { get; set; } = string.Empty;
    [Required]
    public string? FileNumber { get; set; } = string.Empty;
    [Required]
    public string? JobName { get; set; } = string.Empty;
    [Required]
    public string? Address { get; set; } = string.Empty;
    [Required, DataType(DataType.PhoneNumber)]
    public string? TelephoneNumber { get; set; } = string.Empty;
    [Required]
    public string? Photo { get; set; } = string.Empty;
    public string? Other { get; set; } = string.Empty;

    public Branch? Branch { get; set; }
    [Required]
    public int BranchId { get; set; }
    public Town? Town { get; set; }
    public int TownId { get; set; }
}
