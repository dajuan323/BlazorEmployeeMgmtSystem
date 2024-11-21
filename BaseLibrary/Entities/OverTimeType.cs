
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class OvertimeType : BaseEntity
{
    // One to many relationship with Overtime
    [JsonIgnore]
    public List<Overtime>? Overtimes { get; set; }
}
