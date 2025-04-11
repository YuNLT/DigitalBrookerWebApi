﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBrooker.Domain.Entities.Request
{
    public record LoginRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
