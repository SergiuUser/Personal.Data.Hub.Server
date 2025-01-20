using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Org.BouncyCastle.Tls;
using Personal.Dara.Hub.Server.BLL.Services;
using Personal.Dara.Hub.Server.BLL.Services.Interfaces;
using Personal.Dara.Hub.Server.Models;
using Personal.Dara.Hub.Server.Models.Data_transfer_object;
using Personal.Dara.Hub.Server.Models.Models.Database;

namespace Personal.Data.Hub.Srver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public Account(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        [HttpPost("/Register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterUserDTO entity)
        {
            try
            {
                bool succed = await _accountService.Register(entity);

                // Send an email for confirmation

                return Ok(new {Succed = succed});
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("/Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO entity)
        {
            try
            {
                string token = await _accountService.Login(entity);

                // Implement Two factor authentication

                return Ok(new { Token = token });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            string message = "hello";
            return Ok(new { Token = message });
        }
    }
}
