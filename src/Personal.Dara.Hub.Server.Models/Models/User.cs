using Personal.Dara.Hub.Server.Models.Helpers;
using Personal.Dara.Hub.Server.Models.Models.Relationships;

namespace Personal.Dara.Hub.Server.Models.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string? Username { get; set; }
        public string? FirstName { get; set; } = "User";
        public string? LastName { get; set; } = "User";
        public int Age { get; set; } = 0;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserRoleHelper UserRole { get; set; } = UserRoleHelper.None;
        public ICollection<UserWorkspace>? Workspaces { get; set; }
    }
}
