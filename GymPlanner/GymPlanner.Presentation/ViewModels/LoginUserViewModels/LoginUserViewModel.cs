using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymPlanner.Presentation.ViewModels.LoginUserViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string UserOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Password { get; set; } = string.Empty;
    }
}