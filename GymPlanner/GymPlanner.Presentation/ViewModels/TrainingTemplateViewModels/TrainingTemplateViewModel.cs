using System.ComponentModel.DataAnnotations;
using GymPlanner.Common.Validation;

namespace GymPlanner.Presentation.ViewModels.TrainingTemplateViewModels;

public class TrainingTemplateViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [MinLengthList(1, ErrorMessage = "La plantilla debe contener al menos un ejercicio.")]
    public List<TrainingTemplateExerciseViewModel> Exercises { get; set; } = [];
}
