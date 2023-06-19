using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController: ControllerBase
    {
        //GET api/response/100
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetAReponse(int id)
        {
            Random rnd = new Random();
            var rndInteger = rnd.Next(1,101);
            if(rndInteger >= id)
            {
                Console.WriteLine("--> Failure - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            Console.WriteLine("--> Success - Generate a HTTP 200");
            return Ok();
        } 
    }
}
