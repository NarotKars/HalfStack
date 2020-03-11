using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dbSettings.DataAccess;
using back_end.Models;

namespace back_end.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        UserManagerDB user = new UserManagerDB();

        [Route("manager/{id}")]
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
        public ActionResult UpDateEmail(UserManager us)
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