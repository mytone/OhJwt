using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace OhJwt.Controllers
{
    [Route("[controller]")]
    //[ApiController]
    [Authorize]
    public class ProductController : Controller
    {


        [HttpGet("AdminProduct")]
        [Authorize(Roles="Admin")]
        public ActionResult AdminProduct() 
        { 
             return Ok("Admin and Token Access!");
        }



        [HttpGet("MainProduct")]
        [Authorize(Roles = "Main")]
        public ActionResult MainProduct()
        {
            return Ok("Main and Token Access!");
        }

    }
}
