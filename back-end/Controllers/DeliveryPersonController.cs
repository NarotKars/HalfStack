using back_end.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private DeliveryPersonManager manager = new DeliveryPersonManager();

        [HttpGet]
        [Route("api/DeliveryPerson/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var info = manager.Get(id);

                return Ok(info);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
