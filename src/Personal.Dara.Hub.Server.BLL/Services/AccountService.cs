using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Data.Repositories;
using Personal.Dara.Hub.Server.Models;
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

        public async Task<string> Login(LoginDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity provided is Empty.");

            if (string.IsNullOrEmpty(entity.Username) && string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentNullException("Both Username or Email cannot be null or empty.");
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
                    throw new UnauthorizedAccessException("Password isn't corrent");
                }

                // TODO: Token creation and return it
                return string.Empty;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Register(RegisterUserDTO entity)
        {

            if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            if (await _userRepository.DoesEmailAndUsernameExists(entity.Email, entity.Username))
                throw new InvalidOperationException("Email or Username already exists!");

            if (!_passwordService.VerifyIdenticalForConfirm(entity.Password, entity.ConfirmPassword))
                throw new InvalidOperationException("Password nad confirm password doesn't match");
            try
            {

                var userToAdd = new User()
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Username = entity.Username,
                    Email = entity.Email,
                    Password = _passwordService.HashPassword(entity.Password),
                    Age = entity.Age,
                    Gender = entity.Gender,
                    UserRole = entity.Role
                };

                // TODO: Implement email confirmation system

                await _userRepository.Add(userToAdd);

                if (await _userRepository.SaveAsync() == false)
                    throw new InvalidOperationException("The server isnt working at the moment, try later!");

                return true;
            }
            catch
            {
                throw;
            }
        }

    }
}
