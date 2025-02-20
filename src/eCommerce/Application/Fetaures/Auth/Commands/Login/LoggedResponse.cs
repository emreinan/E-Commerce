using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Fetaures.Auth.Commands.Login;

public class LoggedResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
