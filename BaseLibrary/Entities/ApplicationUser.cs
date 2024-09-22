using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
