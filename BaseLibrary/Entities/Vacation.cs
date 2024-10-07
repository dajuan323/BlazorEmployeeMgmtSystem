using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Vacation : OtherBaseEntity
{
    [Required, DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }
    [Required]
    public int NumberOfDays { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime EndDate => StartDate.AddDays(NumberOfDays);

    // Many to one relationship with Vacation Type
    public VacationType? VacationType { get; set; }
    [Required]
    public int VacationTypeId { get; set; }
}