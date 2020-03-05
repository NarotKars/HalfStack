using Microsoft.AspNetCore.Mvc;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using dbSettings.DataAccess;
namespace back_end.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderManager manager = new OrderManager();

        [HttpGet("/manager/orders")]
        public IEnumerable<Order> ManagerOrders()
        {
            Orderdb orderManager=new Orderdb();
            List<Order> managers= orderManager.GetAllOrdersAsGenericList();
            return  managers;
        }

         [HttpGet("manager/orders/{status}")]
        public IEnumerable<Order> OrderListByStatus(int num)
        {
            OrderManager manager = new OrderManager();
            List<Order> orders = manager.GetOrdersAsGenericList(num);
            return orders;
        }

        [HttpGet("/customer/orders/{id}")]
        public IEnumerable<Order> Orders(int id)
        {
            Orderdb orderdb=new Orderdb();
            List<Order> orders= orderdb.GetOrdersAsGenericList(id);
            return  orders;
        }

        [HttpGet("/customer/orders/details/{id}")]
        public IEnumerable<OrderDetails> OrdersDetails(int id)
        {
            OrderDetailsdb orderdDetailsdb=new OrderDetailsdb();
            List<OrderDetails> orderDetails= orderdDetailsdb.GetOrderDetailsAsGenericList(id);
            return  orderDetails;
        }

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