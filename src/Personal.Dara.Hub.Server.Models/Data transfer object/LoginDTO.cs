namespace Personal.Dara.Hub.Server.Models.Data_transfer_object
{
    public class LoginDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool RemeberUser { get; set; } = false;
    }
}
