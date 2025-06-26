using System.ComponentModel.DataAnnotations;
using GymPlanner.Application.TrainingLevel.DTOs;
using GymPlanner.Application.Users.DTOs;
using GymPlanner.Common.Validation;

namespace GymPlanner.Presentation.ViewModels.RegisterUserViewModels;

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "El nombre de usuario es requerido.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "El email es requerido.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "La contraseña es requerida.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "La confirmación de contraseña es requerida.")]
    [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
    public string? ConfirmPassword { get; set; }

    [GuidNotEmptyAttribute(ErrorMessage = "Debes seleccionar un nivel de entrenamiento.")]
    public Guid TrainingLevelId { get; set; }
    public List<TrainingLevelDto> TrainingLevels { get; set; } = new();
}