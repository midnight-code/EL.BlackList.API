using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Repositore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBacksRepositore _feedBackRepositore;
        public FeedBackController(IFeedBacksRepositore feedBackRepositore) => _feedBackRepositore = feedBackRepositore;


        [HttpPost("/savefeedbacks/{feedbacks}")]
        public async Task<ActionResult<int>> SaveFeedBack(FeedBacks feedbacks)
        {
            if (feedbacks is not null)
            {
                return await _feedBackRepositore.SaveFeedBackAsync(feedbacks);
            }
            else
                return 0;
        }

        [HttpGet("/feedBackByID/{id}", Name = "GetFeedBackByID")]
        public ActionResult<FeedBacks> GetFeedBackByID(int id)
        {
            var result = _feedBackRepositore.GetFeedBackById(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }







        [HttpPut("/updatefeedback/{feedbacks}")]
        public async Task<ActionResult<int>> UpdateFeedBack(FeedBacks feedbacks)
        {
            if (feedbacks is not null)
                return await _feedBackRepositore.SaveFeedBackAsync(feedbacks);
            else
                return NotFound();

        }

        [HttpDelete("/delfeedback/{id}", Name = "DeleteFeedBack")]
        public async Task<ActionResult<bool>> DeleteFeedBack(int id)
        {
            if (id > 0)
            {
                var result = await _feedBackRepositore.DeleteFeedBackAsync(id);
                return result;
            }
            return BadRequest();
        }
    }
}
