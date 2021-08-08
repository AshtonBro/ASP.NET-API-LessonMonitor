using LessonMonitor.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LessonMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {

        // POST auth
        [HttpPost("registration")]
        public async Task SignUp(UserCredentials credentials)
        {

        }

        // POST auth/token
        [HttpPost("login")]
        public async Task SignIn(UserCredentials credentials)
        {

        }

        //var bodyStream = Context.Request.Body;
        //var userCredentials = await JsonSerializer.DeserializeAsync<UserCredentials>(bodyStream);
    }
}
