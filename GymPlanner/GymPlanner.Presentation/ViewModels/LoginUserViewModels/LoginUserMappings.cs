using GymPlanner.Application.Users.DTOs;

namespace GymPlanner.Presentation.ViewModels.LoginUserViewModels
{
    public static class LoginUserMappings
    {
        public static LoginUserViewModel ToViewModel(this LoginUserDto dto)
            => new()
            {
                UserOrEmail = dto.UserOrEmail,
                Password = dto.Password
            };

        public static LoginUserDto ToDto(this LoginUserViewModel vm)
            => new()
            {
                UserOrEmail = vm.UserOrEmail,
                Password = vm.Password
            };
    }
}