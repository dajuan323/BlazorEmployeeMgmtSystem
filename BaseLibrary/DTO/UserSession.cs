using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTO;

public record UserSession
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}
