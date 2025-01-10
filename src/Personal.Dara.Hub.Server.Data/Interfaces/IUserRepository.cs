namespace Personal.Dara.Hub.Server.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> DoesEmailAndUsernameExists(string email, string username);
    }
}
