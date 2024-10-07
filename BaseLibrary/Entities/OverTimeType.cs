
namespace BaseLibrary.Entities;

public class OverTimeType : BaseEntity
{
    // One to many relationship with Overtime
    public List<Overtime>? Overtimes { get; set; }
}
