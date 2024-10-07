using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Sanction : OtherBaseEntity
{
    [Required, DataType(DataType.DateTime)]
    public DateTime Date { get; set; }
    [Required]
    public string CorrectiveAction { get; set; } = string.Empty;
    [Required, DataType(DataType.DateTime)]
    public DateTime CorrectiveActionDate { get; set; }

    // Many to one relationship with Sanction Type
    public SanctionType? SanctionType { get; set; }
    [Required]
    public int SanctionTypeId { get; set; }

}