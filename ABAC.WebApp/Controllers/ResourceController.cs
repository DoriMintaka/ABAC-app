using ABAC.DAL.Exceptions;
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
        private readonly IRuleService ruleService;

        public ResourceController(IService<ResourceInfo> service, IRuleService ruleService)
        {
            this.service = service;
            this.ruleService = ruleService;
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
            if (!await ruleService.Validate(HttpContext.Session.GetInt32("userId").GetValueOrDefault(-1), id))
            {
                throw new ForbiddenException();
            }

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
            if (!await ruleService.Validate(HttpContext.Session.GetInt32("userId").GetValueOrDefault(-1), id))
            {
                throw new ForbiddenException();
            }

            await service.DeleteAsync(id);
            return new OkResult();
        }
    }
}