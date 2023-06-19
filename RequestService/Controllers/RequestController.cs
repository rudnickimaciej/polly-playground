using Microsoft.AspNetCore.Mvc;
using Polly;
using RequestService.Policies;

namespace RequestService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolict;
        private readonly IHttpClientFactory _clientFactory;

        public RequestController(ClientPolicy clientPolict, IHttpClientFactory clientFactory)
        {
            _clientPolict = clientPolict;
            _clientFactory = clientFactory;
        }

        //GET api/request
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var rateLimit = Policy.RateLimitAsync(3,TimeSpan.FromSeconds(3));
           
            var client = _clientFactory.CreateClient("github");
          
            var response = await rateLimit.ExecuteAsync(()=>  _clientPolict.ImmediateHttpRetry.ExecuteAsync(
                () => client.GetAsync("https://localhost:7057/api/Response/1")));
            // var client = new HttpClient();
            //var response = await client.GetAsync("https://localhost:7057/api/Response/25");
            // var response = await  _clientPolict.ImmediateHttpRetry.ExecuteAsync(
            //     () => client.GetAsync("https://localhost:7057/api/Response/25"));

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> ResponseService returned Success");
                return Ok();
            }
            Console.WriteLine("--> ResponseService returned Failure");
            return StatusCode(StatusCodes.Status500InternalServerError);
        } 
    }
}