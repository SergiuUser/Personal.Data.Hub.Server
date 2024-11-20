using Personal.Dara.Hub.Server.Models.Models.Relationships;

namespace Personal.Dara.Hub.Server.Models.Models
{
    public class Workspace
    {
        public required int Id { get; set; }
        public string Name { get; set; } = "WorkSpace";
        public required ICollection<UserWorkspace> Users { get; set; }
    }
}
