using System.ComponentModel.DataAnnotations;
using GymPlanner.Common.Validation;

namespace GymPlanner.Presentation.ViewModels.RoutineViewModels;

public class RoutineViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }

    [MinLengthList(1, ErrorMessage = "La plantilla debe contener al menos un ejercicio.")]
    public List<RoutineDayViewModel> Days { get; set; } = [];
}