using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using dbSettings.DataAccess;

namespace back_end.Controllers


{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        UserDeliveryDB user = new UserDeliveryDB();

        [Route("getbyid/{id}")]
        [HttpGet]
        public ActionResult GetById(int id)
        {
            try
            {

                var users = user.ReadById(id);

                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Route("update")]
        [HttpPut]
        public ActionResult Updatee(Update us)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var users = user.Updatet(us);

                    if (users == null) return NotFound();
                    return Ok(users);
                }
            }
            catch
            {
                return BadRequest();


            }
            return BadRequest(ModelState);
        }


    }
}
