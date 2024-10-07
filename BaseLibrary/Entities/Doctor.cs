
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class Doctor : OtherBaseEntity
{
    [Required, DataType(DataType.DateTime)]
    public DateTime Date { get; set; }
    [Required]
    public string Diagnosis { get; set; } = string.Empty;
    [Required]
    public string MedicalRecommendation {  get; set; } = string.Empty;
    
}
