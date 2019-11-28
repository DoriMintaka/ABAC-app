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
        public async Task<IEnumerable<Attribute>> GetUserAttributesAsync([FromRoute] int id)
        {
            return await userService.GetAttributesAsync(id);
        }

        [HttpPost("user/{id}")]
        public async Task UpdateUserAttributesAsync([FromRoute] int id, [FromBody] IEnumerable<Attribute> attributes)
        {
            await userService.AddAttributesAsync(id, attributes);
        }

        [HttpDelete("user/{id}")]
        public async Task DeleteUserAttributeAsync([FromRoute] int id, [FromBody] string attribute)
        {
            await userService.DeleteAttributeAsync(id, attribute);
        }

        [HttpGet("resource/{id}")]
        public async Task<IEnumerable<Attribute>> GetResourceAttributesAsync([FromRoute] int id)
        {
            return await resourceService.GetAttributesAsync(id);
        }

        [HttpPost("resource/{id}")]
        public async Task UpdateResourceAsync([FromRoute] int id, [FromBody] IEnumerable<Attribute> attributes)
        {
            await resourceService.AddAttributesAsync(id, attributes);
        }

        [HttpDelete("user/{id}")]
        public async Task DeleteResourceAttributeAsync([FromRoute] int id, [FromBody] string attribute)
        {
            await resourceService.DeleteAttributeAsync(id, attribute);
        }
    }
}