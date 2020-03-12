using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using dbSettings.DataAccess;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderstatusController : ControllerBase
    {
        OrderStatusDB order = new OrderStatusDB();
        [Route("update")]
        [HttpPut]
        public ActionResult Updatee(OrderStatus ord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orders= order.Update(ord);

                    if (orders == null) return NotFound();
                    return Ok(orders);
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
