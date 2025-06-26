using GymPlanner.Application.BodyParts.DTOs;

namespace GymPlanner.Presentation.ViewModels.BodyPartViewModels
{
    public static class BodyPartMappings
    {
        public static BodyPartViewModel ToViewModel(this BodyPartDto dto)
            => new() { Id = dto.Id, Name = dto.Name };

        public static BodyPartDto ToDto(this BodyPartViewModel vm)
            => new() { Id = vm.Id, Name = vm.Name };
    }
}