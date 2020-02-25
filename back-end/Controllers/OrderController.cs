using Microsoft.AspNetCore.Mvc;
using back_end.Models;
using back_end.Managers;
using Microsoft.AspNetCore.Http;

namespace back_end.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderManager manager = new OrderManager();

        [HttpGet]
        [Route("api/Orders/Feedback/{id}")]
        public IActionResult GetFeedback(int id)
        {
            try
            {
                var feedback = manager.Get(id);

                return Ok(feedback);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("api/Orders/Feedback")]
        public IActionResult PutFeedback(OrderFeedbackModel model)
        {
            try
            {
                var feedback = manager.PutFeedback(model);

                return Ok(feedback);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}