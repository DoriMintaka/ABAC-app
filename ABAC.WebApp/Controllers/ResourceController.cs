using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABAC.WebApp.Controllers
{
    [Route("api/resources")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IService<ResourceInfo> service;

        public ResourceController(IService<ResourceInfo> service)
        {
            this.service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetResourcesAsync()
        {
            var result = await service.GetAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceAsync([FromRoute] int id)
        {
            var result = await service.GetAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateOrUpdateResourceAsync([FromBody] ResourceInfo resource)
        {
            var id = ControllerContext.HttpContext.Session.GetInt32("id") ?? 0;
            await service.UpdateAsync(resource, id);
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResourceAsync([FromRoute] int id)
        {
            await service.DeleteAsync(id);
            return new OkResult();
        }
    }
}