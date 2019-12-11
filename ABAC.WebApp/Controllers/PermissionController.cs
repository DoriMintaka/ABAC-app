using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attribute = ABAC.DAL.Entities.Attribute;

namespace ABAC.WebApp.Controllers
{
    [Route("api/management")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly IService<ResourceInfo> resourceService;

        public PermissionController(IUserService userService, IService<ResourceInfo> resourceService)
        {
            this.userService = userService;
            this.resourceService = resourceService;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserAttributesAsync([FromRoute] int id)
        {
            var result = await userService.GetAttributesAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPost("user/{id}")]
        public async Task<IActionResult> UpdateUserAttributesAsync([FromRoute] int id, [FromBody] Attribute attributes)
        {
            await userService.AddAttributesAsync(id, new []{attributes});
            return new OkResult();
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUserAttributeAsync([FromRoute] int id, [FromQuery(Name = "attribute")] string attribute)
        {
            await userService.DeleteAttributeAsync(id, attribute);
            return new OkResult();
        }

        [HttpGet("resource/{id}")]
        public async Task<IActionResult> GetResourceAttributesAsync([FromRoute] int id)
        {
            var result = await resourceService.GetAttributesAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPost("resource/{id}")]
        public async Task<IActionResult> UpdateResourceAsync([FromRoute] int id, [FromBody] Attribute attributes)
        {
            await resourceService.AddAttributesAsync(id, new []{attributes});
            return new OkResult();
        }

        [HttpDelete("resource/{id}")]
        public async Task<IActionResult> DeleteResourceAttributeAsync([FromRoute] int id, [FromQuery(Name = "attribute")] string attribute)
        {
            await resourceService.DeleteAttributeAsync(id, attribute);
            return new OkResult();
        }
    }
}