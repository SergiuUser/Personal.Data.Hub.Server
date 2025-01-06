using Personal.Dara.Hub.Server.Models.Models.Relationships;

namespace Personal.Dara.Hub.Server.Models.Models
{
    public class Workspace
    {
        public required int Id { get; set; } // Database only
        public required string Identifier { get; set; } // Display name + unique tag
        public required string DisplayName { get; set; }

        // TODO: Add files/photos/notes

        public required ICollection<UserWorkspace> Users { get; set; }
    }
}
