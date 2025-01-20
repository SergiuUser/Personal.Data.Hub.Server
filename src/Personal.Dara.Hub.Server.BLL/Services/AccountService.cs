using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Data.Interfaces;
using Personal.Dara.Hub.Server.Models;
using Personal.Dara.Hub.Server.Models.Data_transfer_object;
using Personal.Dara.Hub.Server.Models.Helpers;
using Personal.Dara.Hub.Server.Models.Models.Database;
using Personal.Dara.Hub.Server.Models.Models.Services;

namespace Personal.Dara.Hub.Server.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        #region constructor

        public AccountService(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        #endregion

        public async Task<string> Login(LoginDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity provided is Empty.");

            if (string.IsNullOrEmpty(entity.Username) && string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentNullException("Username or Email must be provided.");
            }

            if (String.IsNullOrEmpty(entity.Password))
            {
                throw new InvalidOperationException("Password cannot be null");
            }

            try
            {

                string? searchTerm = !string.IsNullOrEmpty(entity.Username) ? entity.Username : entity.Email;

                var entityDB = await _userRepository.GetByExpression(e => e.Username == searchTerm || e.Email == searchTerm);


                if (entityDB == null)
                {
                    throw new InvalidOperationException("No user found with the provided Username or Email.");
                }

                if (!_passwordService.VerifyPassword(entity.Password, entityDB.Password))
                {
                    throw new UnauthorizedAccessException("Password isn't corrent");
                }

                var userInfoForGeneratingToken = new UserInfoToGenerateJwtToken
                {
                    Email = entity.Email,
                    Role = UserRoleHelper.Default,
                };
                return _tokenService.GenerateToken(userInfoForGeneratingToken);

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
