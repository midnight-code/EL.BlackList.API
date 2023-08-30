using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBaseController : ControllerBase
    {
        private readonly IUserBaseService _userBaseService;
        public UserBaseController(IUserBaseService userBaseService)
        {
            _userBaseService = userBaseService;
        }
        [HttpGet("/userbase/GetUserByID/{userid}", Name ="GetUserBaseByUserId")]
        public async Task<ActionResult<UserBase>> GetUserBaseByUserId(string userid)
        {
            if(!string.IsNullOrWhiteSpace(userid))
            {
                return Ok(await _userBaseService.GetUserBaseByUserIdAsync(userid));
            }
            return BadRequest();
        }

        [HttpPost("/userbase/saveuserbase", Name ="SaveUserBase")]
        public async Task<ActionResult<string>> SaveUserBase(UserBase userBase)
        {
            if(userBase == null) 
                return BadRequest();
            return Ok(await _userBaseService.SaveUserBaseAsync(userBase));
        }

        [HttpDelete("/userbase/deleteuserbyuserid/{userid}", Name ="DeleteUserBaseByUserId")]
        public async Task<ActionResult<bool>> DeleteUserBaseByUserId(string userid)
        {
            if(!string.IsNullOrWhiteSpace(userid))
            {
                return Ok(await _userBaseService.DeleteUserBase(userid));
            }
            return BadRequest();
        }
    }
}
