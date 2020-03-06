using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using dbSettings.DataAccess;

namespace back_end.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(ILogger<FeedbackController> logger)
        {
            _logger = logger;
        }

        [HttpPost("feedback/{id}")]
        public IActionResult PostFeed(Feedback feedback)
        {

            try
            {
                FeedbackDAL feedbackDAL = new FeedbackDAL();
                feedbackDAL.InsertFeedback(feedback);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }

    }
}
