using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using dbSettings.DataAccess;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreferanceController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public PreferanceController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        //get customer's preference by customer's id
        [HttpGet("/customer/preferences/{id}")]
        public IEnumerable<Preference> Preferences(int id)
        {
            theAlgorithm algo=new theAlgorithm();
            int[,] a=algo.algorithm(11);
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
                Preferencedb preferencedb = new Preferencedb();
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
                Preferencedb preferencedb = new Preferencedb();
                preferencedb.DeletePreference(id);
            }
            catch
            {
                return BadRequest("somthing went wrong");
            }
            return Ok();
        }
    }
}