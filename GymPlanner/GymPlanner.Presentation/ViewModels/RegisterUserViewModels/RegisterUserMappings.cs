using GymPlanner.Application.Users.DTOs;

namespace GymPlanner.Presentation.ViewModels.RegisterUserViewModels
{
    public static class RegisterUserMappings
    {
        public static RegisterUserViewModel ToViewModel(this RegisterUserDto dto)
            => new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password
            };

        public static RegisterUserDto ToDto(this RegisterUserViewModel vm)
            => new()
            {
                UserName = vm.UserName ?? string.Empty,
                Email = vm.Email ?? string.Empty,
                Password = vm.Password ?? string.Empty
            };
    }
}