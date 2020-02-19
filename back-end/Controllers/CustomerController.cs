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

        [HttpGet("{id}")]
        public IEnumerable<Order> GetOrders(int id)
        {
            Orderdb orderdb=new Orderdb();
            List<Order> orders= orderdb.GetOrdersAsGenericList(id);
            return  orders;
        }
    }
}
