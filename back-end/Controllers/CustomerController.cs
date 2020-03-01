using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using dbSettings.DataAccess;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        
        UserCustomerDB user = new UserCustomerDB();

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


        [Route("updatee")]
        [HttpPut]
        public ActionResult UpDatee(Update us)
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
