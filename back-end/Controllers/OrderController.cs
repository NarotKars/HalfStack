using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using System.Data.SqlClient;

namespace back_end.Controllers
{
    [ApiController]
    [RoutePrefix("api/Orders/Feedback")]
    public class OrderController : ControllerBase
    {
        [HttpPut]
        public IHttpActionResult PutFeedback(OrderFeedbackModel model)
        {
            try
            {
                var feedback = OrderManager.PutFeedback(model);
               
                return Ok(feedback);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}