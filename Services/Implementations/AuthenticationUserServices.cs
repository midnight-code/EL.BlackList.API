using Azure;
using EL.BlackList.API.IdentityAuth;
using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EL.BlackList.API.Services.Implementations
{
    public class AuthenticationUserServices : IAuthenticationUserServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationUserServices(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IBaseResponse<string>> GetRegisterUserAsync(RegisterModel model)
        {
            var UserExists = await _userManager.FindByNameAsync(model.UserName);
            if (UserExists != null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.InternalServverErroe,
                    Data = "User already exists"
                });
            AppUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.InternalServverErroe,
                    Data = result.ToString()
                });
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Success",
                StatusCode = Enum.StatusCode.OK,
                Data = "User created"
            });
        }

        public async Task<IBaseResponse<string>> GetRegisterAdminAsync(RegisterModel model)
        {
            var UserExists = await _userManager.FindByNameAsync(model.UserName);
            if (UserExists != null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.InternalServverErroe,
                    Data = "User already exists"
                });
            AppUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.InternalServverErroe,
                    Data = result.ToString()
                });
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Success",
                StatusCode = Enum.StatusCode.OK,
                Data = "User create success full."
            });
        }

        public async Task<IBaseResponse<TokenModel>> GetLoginAsync(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userroles in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userroles));
                }

                var authSingningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(30),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSingningKey, SecurityAlgorithms.HmacSha256)
                    );
                TokenModel model = new()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    DateAdd = token.ValidTo,
                    Users = user.Id
                };
                return await Task.Run(() => new BaseResponse<TokenModel>()
                {
                    Description = $"Succes",
                    StatusCode = Enum.StatusCode.OK,
                    Data = model
                });
            }

            return await Task.Run(() => new BaseResponse<TokenModel>()
            {
                Description = $"Error",
                StatusCode = Enum.StatusCode.InternalServverErroe,
            });
        }

        public async Task<IBaseResponse<string>> GetChangePasswordAsync(ChangePassowrdModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "User does not exists"
                });
            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "The new password and confirm new password does not match!"
                });
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.InternalServverErroe,
                    Data = string.Join("", "", errors)
                });
            }
            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Success",
                StatusCode = Enum.StatusCode.OK,
                    Data = "Password successfully changed."
            });
        }

        public async Task<IBaseResponse<string>> GetResetPasswordAdminAsync(ResetPasswordAdminModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data= "User does not exists"
                });
            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "The new password and confirm new password does not match!"
                });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.InternalServverErroe,
                    Data = string.Join("", "", errors)
                });
            }
            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Success",
                StatusCode = Enum.StatusCode.OK,
                Data = "Password successfully reseted."
            });
        }

        public async Task<IBaseResponse<string>> GetResetPasswordTokenAsync(ResetPasswordTokenModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "User does not exists"
                });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Success",
                StatusCode = Enum.StatusCode.OK,
                Data = token
            });
        }

        public async Task<IBaseResponse<string>> GetResetPasswordUserAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "User does not exists"
                });
            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "The new password and confirm new password does not match!"
                });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrEmpty(model.Token))
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = "Invalide token!"
                });
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Error",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data = string.Join("", "", errors)
                });
            }
            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Success",
                StatusCode = Enum.StatusCode.OK,
                Data = "Password successfully reseted."
            });
        }
    }
}
