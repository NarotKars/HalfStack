//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using back_end.Models;
//using back_end.DataAccess;
//using back_end.Controllers;

//namespace back_end.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class FeedbackController : ControllerBase
//    {
//        private readonly ILogger<FeedbackController> _logger;

//        public FeedbackController(ILogger<FeedbackController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpPost]
//        public IActionResult PostFeed(Feedback feedback)
//        {

//            try
//            {
//                FeedbackDAL feedbackDAL= new FeedbackDAL();
//                feedbackDAL.InsertFeedback(feedback);
//            }
//            catch
//            {
//                return BadRequest("something went wrong");
//            }
//            return Ok();
//        }

//    }
//}
//////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Models;
using back_end.DataAccess;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(ILogger<FeedbackController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult PostFeed(Feedback feedback)
        {

            try
            {
                FeedbackDAL feedbackDAL = new FeedbackDAL();
                feedbackDAL.InsertFeedback(feedback);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }

    }
}



