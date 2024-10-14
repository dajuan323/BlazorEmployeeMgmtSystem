using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class City : BaseEntity
{
    // Many to one relationship with Country
    public Country? Country { get; set; }
    public int CountryId { get; set; }


    // One to many relationship to Town
    [JsonIgnore]
    public List<Town>? Towns { get; set; }
}