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

        [HttpGet("/delivery/tobeaccepted/{id}")]
        public IEnumerable<Order> ToBeAccepted(int id)
        {
            UserDeliveryDB deliveryWorker=new UserDeliveryDB();
            List<Order> tobeaccepted= deliveryWorker.GetToBeAcceptedOrders(id);
            return tobeaccepted;
        }

        [HttpPut("/deliveryworker/accept/{id}")]

        public ActionResult PutWorkerId(OrderDelivery order)
        {
            try
            {
                UserDeliveryDB orderdelivery = new UserDeliveryDB();
                orderdelivery.deliveryIsAccepted(order);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }



        [HttpPut("/deliveryworker/status/{id}")]

        public ActionResult PutDeliveryWorkerStatus(DeliveryStatus order)
        {
            try
            {
                UserDeliveryDB orderdelivery = new UserDeliveryDB();
                orderdelivery.deliveryStatus(order);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
    }
}
