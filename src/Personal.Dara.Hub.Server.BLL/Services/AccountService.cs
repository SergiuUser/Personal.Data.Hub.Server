using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Data.Repositories;
using Personal.Dara.Hub.Server.Models.Data_transfer_object;
using Personal.Dara.Hub.Server.Models.Models;

namespace Personal.Dara.Hub.Server.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;

        #region constructor

        public AccountService(UserRepository userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        #endregion

        public async Task<(bool loginSucced, string token)> Login(LoginDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity provided is Empty.");

            if (string.IsNullOrEmpty(entity.Username) && string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentException("Both Username or Email cannot be null or empty.");
            }

            User? entityDB = null;

            try
            {
                if (!String.IsNullOrEmpty(entity.Username))
                {
                    entityDB = await _userRepository.GetByExpression(e => e.Username == entity.Username);
                }
                else if (!String.IsNullOrEmpty(entity.Email)) {
                    entityDB = await _userRepository.GetByExpression(e => e.Email == entity.Email);
                }

                if (entityDB == null)
                {
                    throw new InvalidOperationException("No user found with the provided Username or Email.");
                }

                if (String.IsNullOrEmpty(entity.Password))
                {
                    throw new InvalidOperationException("Password cannot be null");
                }

                if (!_passwordService.VerifyPassword(entity.Password, entityDB.Password))
                {
                    return (false, string.Empty);
                }

                // TODO: Token creation and return it
                return (false, string.Empty);

            }
            catch
            {
                throw;
            }
        }
    }
}
