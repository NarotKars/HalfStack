using Microsoft.AspNetCore.Mvc;
using back_end.Models;
using back_end.Managers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using dbSettings.DataAccess;
namespace back_end.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderManager manager = new OrderManager();
        private theAlgorithm item=new theAlgorithm();

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

        [HttpGet("/manager/orders")]
        public IEnumerable<Order> ManagerOrders()
        {
            Orderdb orderManager=new Orderdb();
            List<Order> managers= orderManager.GetAllOrdersAsGenericList();
            return  managers;
        }

        [HttpGet("/products/{id}")]
        public IEnumerable<OrderProduct> OrdersDetails(int id)
        {
            theAlgorithm algorithm=new theAlgorithm();
            List<OrderProduct> orderDetails= algorithm.GetAllProductsAsGenericList(id);
            return  orderDetails;
        }

    }
}