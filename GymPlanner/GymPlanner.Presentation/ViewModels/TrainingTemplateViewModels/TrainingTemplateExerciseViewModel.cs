using System.ComponentModel.DataAnnotations;
using GymPlanner.Common.Validation;

namespace GymPlanner.Presentation.ViewModels.TrainingTemplateViewModels;

public class TrainingTemplateExerciseViewModel
{
    public Guid Id { get; set; }

    [GuidNotEmptyAttribute(ErrorMessage = "Debes seleccionar un ejercicio.")]
    public Guid ExerciseId { get; set; }

    public string ExerciseName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecciona una parte del cuerpo.")]
    public Guid BodyPartId { get; set; }

    public string BodyPartName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduce el número de series.")]
    [Range(1, 20, ErrorMessage = "Series debe estar entre 1 y 20.")]
    public int Sets { get; set; }

    [Required(ErrorMessage = "Introduce el número de repeticiones.")]
    [Range(1, 50, ErrorMessage = "Repeticiones debe estar entre 1 y 50.")]
    public int Repetitions { get; set; }

    [Required(ErrorMessage = "Introduce el peso inicial.")]
    [Range(0, 1000, ErrorMessage = "El peso inicial debe ser 0 o mayor.")]
    public double StartWeight { get; set; }

    [RequiredIf(nameof(IsKeyExercise), true, ErrorMessage = "Introduce el peso objetivo.")]
    [Range(0, 1000, ErrorMessage = "El peso objetivo debe ser 0 o mayor.")]
    public double? TargetWeight { get; set; }

    public bool IsKeyExercise { get; set; }

    [Range(1, 100, ErrorMessage = "El orden debe ser 1 o superior.")]
    public int Order { get; set; }

    public string TempId { get; set; } = string.Empty;

    public int InitialOrder { get; set; }
}
