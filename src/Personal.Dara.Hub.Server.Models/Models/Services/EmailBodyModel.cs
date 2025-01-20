namespace Personal.Dara.Hub.Server.Models.Models.Email
{
    public class EmailBodyModel
    {
        public required string Email { get; set; }
        public required string Subject { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
