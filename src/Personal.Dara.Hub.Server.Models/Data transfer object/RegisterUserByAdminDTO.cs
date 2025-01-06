using Personal.Dara.Hub.Server.Models.Helpers;

namespace Personal.Dara.Hub.Server.Models
{
    public class RegisterUserByAdminDTO
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public required int Age { get; set; }
        public UserGenderHelper Gender { get; set; } = UserGenderHelper.NotSpecified;
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required UserRoleHelper Role { get; set; } = UserRoleHelper.Default;

    }
}
