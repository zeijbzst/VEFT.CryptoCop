using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterInputModel newUser)
        {
            var user = _accountService.CreateUser(newUser);
            if (user == null) { return Conflict($"User with that email already exists."); }
            _tokenService.GenerateJwtToken(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public IActionResult SignIn([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null) { return Unauthorized("Invalid email or password. Passwords are case-sensitive."); }
            return Ok(_tokenService.GenerateJwtToken(user));
        }

        [HttpGet]
        [Route("signout")]
        public IActionResult SignOut()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId")?.Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }
    }
}