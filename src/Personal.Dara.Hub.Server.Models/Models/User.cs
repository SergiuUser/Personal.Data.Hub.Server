using Personal.Dara.Hub.Server.Models.Helpers;
using Personal.Dara.Hub.Server.Models.Models.Relationships;

namespace Personal.Dara.Hub.Server.Models.Models
{
    public class User
    {
        public int Id { get; set; } // Auto generation
        public required string? Username { get; set; }
        public required string FirstName { get; set; } = "User";
        public string? LastName { get; set; } = string.Empty;
        public required int Age { get; set; }
        public UserGenderHelper Gender { get; set; } = UserGenderHelper.NotSpecified;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserRoleHelper UserRole { get; set; } = UserRoleHelper.Default;
        public bool IsActivated { get; set; } = false;
        public ICollection<UserWorkspace>? Workspaces { get; set; }
    }
}
