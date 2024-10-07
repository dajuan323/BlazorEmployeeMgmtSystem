using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTO;

public sealed record Register : AccountBase
{
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Required]
    public string? ConfirmPassword { get; set; }
}
