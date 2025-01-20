using System.Text.Json.Serialization;

namespace Personal.Dara.Hub.Server.Models.Models.Sections
{
    public class EmailConfigurationSection
    {
        public class EmailSettings
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public bool EnableSsl { get; set; } = true;
        }
    }
}
