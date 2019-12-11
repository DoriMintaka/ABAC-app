using ABAC.DAL.Entities;
using ABAC.DAL.Exceptions;
using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABAC.WebApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IUserService service;

        public SessionController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] UserCredentials credentials, [FromForm] UserInfo info)
        {
            try
            {
                await service.GetCredentialsAsync(credentials.Login);
            }
            catch (NotFoundException)
            {
                await service.CreateAsync(info, credentials);
                var user = await service.GetAsync(credentials.Login);
                await service.AddAttributesAsync(user.Id, new[] { new Attribute { Name = "id", Value = user.Id.ToString() } });

                return new OkResult();
            }

            throw new AlreadyExistsException();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromForm] UserCredentials credentials)
        {
            var expected = await service.GetCredentialsAsync(credentials.Login);
            if (expected.Password != credentials.Password)
            {
                throw new InvalidCredentialsException();
            }

            var info = await service.GetAsync(credentials.Login);
            HttpContext.Session.SetInt32("userId", info.Id);

            return new OkObjectResult(info.Name);
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return new OkResult();
        }
    }
}