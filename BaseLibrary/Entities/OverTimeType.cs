
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class OverTimeType : BaseEntity
{
    // One to many relationship with Overtime
    [JsonIgnore]
    public List<Overtime>? Overtimes { get; set; }
}
