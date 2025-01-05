namespace Personal.Dara.Hub.Server.Models.Data_transfer_object
{
    public class RegisterDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string Email { get; set; }
    }
}
