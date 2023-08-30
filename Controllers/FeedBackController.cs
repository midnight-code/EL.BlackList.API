using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Repositore;
using EL.BlackList.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackServices _feedBackServices;
        public FeedBackController(IFeedBackServices feedBackServices) => _feedBackServices = feedBackServices;

        [HttpGet("/feedBackByID/{id}", Name = "GetFeedBackByID")]
        public async Task<ActionResult<FeedBacks>> GetFeedBackByID(int id)
        {
            var result = await _feedBackServices.GetFeedBackByIdAsync(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }


        [HttpPost("/savefeedbacks/{feedbacks}", Name = "SaveFeedBack")]
        public async Task<ActionResult<int>> SaveFeedBack(FeedBacks feedbacks)
        {
            if (feedbacks is not null)
            {
                return Ok(await _feedBackServices.SaveFeedBacksAsync(feedbacks));
            }
            else
                return 0;
        }

        [HttpPut("/updatefeedback/{feedbacks}", Name = "UpdateFeedBack")]
        public async Task<ActionResult<int>> UpdateFeedBack(FeedBacks feedbacks)
        {
            if (feedbacks is not null)
                return Ok(await _feedBackServices.SaveFeedBacksAsync(feedbacks));
            else
                return NotFound();

        }

        [HttpDelete("/delfeedback/{id}", Name = "DeleteFeedBack")]
        public async Task<ActionResult<bool>> DeleteFeedBack(int id)
        {
            if (id > 0)
            {
                var result = await _feedBackServices.DeleteFeedBacksByAsync(id);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
