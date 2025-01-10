using Personal.Dara.Hub.Server.BLL.Services.Interfaces;

namespace Personal.Dara.Hub.Server.BLL.Services
{
    public class PasswordService : IPasswordService
    {

        // TODO: Implement a better method
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public bool VerifyIdenticalForConfirm(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

    }
}
