using Personal.Dara.Hub.Server.Models.Helpers;

namespace Personal.Dara.Hub.Server.Models.Models.Relationships
{
    public class UserWorkspace
    {
            public int UserId { get; set; }
            public required User User { get; set; }

            public int WorkspaceId { get; set; }
            public required Workspace Workspace { get; set; }

            public WorkSpaceRoleHelper UserRoleForWorkspace { get; set; }
    }
}
