using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Presentation.ViewModels.BodyPartViewModels;

public class BodyPartViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Name { get; set; } = string.Empty;
}
