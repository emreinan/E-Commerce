﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Fetaures.Auth.Commands.Login;

public class UserForLoginDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
