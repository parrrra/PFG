using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}