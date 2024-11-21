using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class Overtime : OtherBaseEntity
{
    [Required, DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required, DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
    public int NumberOfDays => (EndDate-StartDate).Days;

    // Many to one relationshipt with Vacation Type
    public OvertimeType? OvertimeType { get; set; }

    [Required]
    public int OvertimeTypeId { get; set; }

}
