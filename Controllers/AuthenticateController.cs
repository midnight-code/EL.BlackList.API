using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationUserServices _userServices;
        public AuthenticateController(IAuthenticationUserServices userServices) => _userServices = userServices;

        [HttpPost, Route("/login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (login is not null)
                return Ok(await _userServices.GetLoginAsync(login));
            return BadRequest();
        }
        [HttpPost]
        [Route("/registeruser", Name = "RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            if(model is not null)
                return Ok( await _userServices.GetRegisterUserAsync(model));
            return BadRequest();
        }

        [HttpPost, Route("/registeradmin", Name = "RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (model is not null)
                return Ok(await _userServices.GetRegisterAdminAsync(model));
            return BadRequest();
        }

        [HttpPost]
        [Route("/change-password", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassowrdModel model)
        {
            if(model is not null)
                return Ok(await _userServices.GetChangePasswordAsync(model));
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("/reset-password-admin", Name = "ResetPasswordAdmin")]
        public async Task<IActionResult> ResetPasswordAdmin([FromBody] ResetPasswordAdminModel model)
        {
            if(model is not null)
                return Ok(await _userServices.GetResetPasswordAdminAsync(model));
            return BadRequest();
        }

        [HttpPost, Route("/reset-password-token", Name = "ResetPasswordToken")]
        public async Task<IActionResult> ResetPasswordToken([FromBody] ResetPasswordTokenModel model)
        {
            if (model is not null)
                return Ok(await _userServices.GetResetPasswordTokenAsync(model));
            return BadRequest();
        }

        [HttpPost, Route("/reset-password", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (model is not null)
                return Ok(await _userServices.GetResetPasswordUserAsync(model));
            return BadRequest();
        }
    }
}
