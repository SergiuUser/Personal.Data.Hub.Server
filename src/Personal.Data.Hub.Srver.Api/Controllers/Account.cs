using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal.Dara.Hub.Server.BLL.Services;
using Personal.Dara.Hub.Server.Data.Context;
using Personal.Dara.Hub.Server.Models;
using Personal.Dara.Hub.Server.Models.Data_transfer_object;
using Personal.Dara.Hub.Server.Models.Models;

namespace Personal.Data.Hub.Srver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly EmailService _emailService;

        public Account(AccountService accountService, EmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
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

        // DELETE: api/Account/5
        [HttpGet]
        public async Task<IActionResult> Login([FromBody] LoginDTO entity)
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
    }
}
