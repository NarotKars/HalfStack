using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Managers;
using backend.Models;

namespace backend.Controllers
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