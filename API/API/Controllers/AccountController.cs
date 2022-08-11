using API.Models;
using API.Repositories.Data;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository accountRepository;
        private readonly IConfiguration config;

        public AccountController(AccountRepository accountRepository, IConfiguration config)
        {
            this.accountRepository = accountRepository;
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginVM login)
        {
            var result = accountRepository.Post(login.Email, login.Password);
            var token = new JwtService(config);

            if (result != null)
            {
                var idToken = token.GenerateSecurityToken(result);

                return Ok(new
                {
                    status = 200,
                    message = "Account Validated",
                    token = idToken,
                });
            }
            return BadRequest(new { status = 400, message = "Invalid Credentials" });

        }
    }
}
