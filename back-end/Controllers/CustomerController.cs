using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("/customer/orders/{id}")]
        public IEnumerable<Order> Orders(int id)
        {
            Orderdb orderdb=new Orderdb();
            List<Order> orders= orderdb.GetOrdersAsGenericList(id);
            return  orders;
        }
        //get customer's preference by customer's id
        [HttpGet("/customer/preferences/{id}")]
        public IEnumerable<Preference> Preferences(int id)
        {
            Preferencedb preferencedb=new Preferencedb();
            List<Preference> preferences= preferencedb.GetPreferencesAsGenericList(id);
            return preferences;
        }
        //post: add customer's preference by customer's id
        [HttpPost("/customer/preferences/{id}")]
        public ActionResult PostPref(Preference pref)
        {
            try
            {
                Preferencedb preferencedb=new Preferencedb();
                preferencedb.InsertPreference(pref);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
        
        //put: update customer's preference by customer's id
        [HttpPut("/customer/preferences/{id}")]
        public ActionResult PutPref(Preference pref)
        {
            try
            {
                Preferencedb preferencedb=new Preferencedb();
                preferencedb.UpdatePreference(pref);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }

        //delete: detele one of preferences by preference id
        [HttpDelete("/customer/preferences/{id}")]
        public ActionResult DeletePref(int id)
        {
            try
            {
                Preferencedb preferencedb =new Preferencedb();
                preferencedb.DeletePreference(id);
            }
            catch
            {
                return BadRequest("somthing went wrong");
            }
            return Ok();
        }


        [HttpGet("/customer/orders/details/{id}")]
        public IEnumerable<OrderDetails> OrdersDetails(int id)
        {
            OrderDetailsdb orderdDetailsdb=new OrderDetailsdb();
            List<OrderDetails> orderDetails= orderdDetailsdb.GetOrderDetailsAsGenericList(id);
            return  orderDetails;
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
        
    }
}
