namespace Personal.Dara.Hub.Server.BLL.Services.Interfaces
{
    public interface IPasswordService
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hashedPassword);
        public bool VerifyIdenticalForConfirm(string password, string confirmPassword)
    }
}
