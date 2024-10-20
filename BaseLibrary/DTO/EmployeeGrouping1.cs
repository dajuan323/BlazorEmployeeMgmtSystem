using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTO;
public record EmployeeGrouping1
{
    [Required]
    public string? Name { get; set; } = string.Empty;
    [Required]
    public string? Address { get; set; } = string.Empty;
    [Required, DataType(DataType.PhoneNumber)]
    public string? TelephoneNumber { get; set; } = string.Empty;
    [Required]
    public string? Photo { get; set; } = string.Empty;
    [Required]
    public string? CivilId { get; set; } = string.Empty;
    [Required]
    public string? FileNumber { get; set; } = string.Empty;
}
