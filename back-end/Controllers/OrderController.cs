using Microsoft.AspNetCore.Mvc;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using dbSettings.DataAccess;
using System;
namespace back_end.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderManager manager = new OrderManager();
        //get all orders
        [HttpGet("/manager/orders")]
        public IEnumerable<Order> ManagerOrders()
        {
            Orderdb orderManager=new Orderdb();
            List<Order> managers= orderManager.GetAllOrdersAsGenericList();
            return  managers;
        }

        [HttpGet("manager/orders/{status}")]
        public IEnumerable<Order> OrderListByStatus(string status)
        {
            OrderManager manager = new OrderManager();
            List<Order> orders = manager.GetOrdersAsGenericList(status);
            return orders;
        }

        //get orders by customer's id
        [HttpGet("/customer/orders/{id}")]
        public IEnumerable<Order> Orders(int id)
        {
            Orderdb orderdb=new Orderdb();
            List<Order> orders= orderdb.GetOrdersAsGenericList(id);
            return  orders;
        }
        //get order details by order id
        [HttpGet("/customer/orders/details/{id}")]
        public IEnumerable<OrderDetails> OrdersDetails(int id)
        {
            OrderDetailsdb orderdDetailsdb=new OrderDetailsdb();
            List<OrderDetails> orderDetails= orderdDetailsdb.GetOrderDetailsOfOneCustomerAsGenericList(id);
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

        [HttpGet("manager/order/details/{id}")]
        public List<OrderProduct> Details(int id)
        {
            theAlgorithm item = new theAlgorithm();
            int[,] a=item.algorithm(id);
            List<OrderProduct> orderDetails = item.GetAllProductsAsGenericList(id);
            return orderDetails;
        }
        //reordering by order id
        [HttpPost("/customer/reorder/{id}")]
        public ActionResult PostOrder(Reorder order)
        {
            try
            {
                //When reordering
                //1. A new transaction is created
                Transactiondb transactiondb=new Transactiondb();
                Transaction newTransaction =new Transaction();
                newTransaction=transactiondb.CreateTransaction(order.amount,order.orderDate);
                //2. Based on newly created transaction a new order is created
                Orderdb orderdb=new Orderdb();
                orderdb.InsertOrder(order,newTransaction.transactionId);
                orderdb.updateOrderAddress(order.Address, newTransaction.transactionId);
                //3.The products of the new order is added
                OrderDetailsdb orderDetailsdb=new OrderDetailsdb();
                List<OrderDetails> orderDetails =new List<OrderDetails>();
                orderDetails=orderDetailsdb.GetOrderDetailsAsGenericListByOrderId(order.orderId);
                Console.WriteLine(orderDetails[0]);
                foreach (var item in orderDetails)
                {
                    orderDetailsdb.InsertProduct(item,newTransaction.transactionId);
                }

            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
    }
}