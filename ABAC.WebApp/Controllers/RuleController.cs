using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABAC.WebApp.Controllers
{
    [Route("api/rules")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService service;

        public RuleController(IRuleService service)
        {
            this.service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetRulesAsync()
        {
            var result = await service.GetAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRuleAsync([FromRoute] int id)
        {
            var result = await service.GetAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateOrUpdateRuleAsync([FromBody] RuleInfo rule)
        {
            await service.CreateOrUpdateAsync(rule);
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleAsync([FromRoute] int id)
        {
            await service.DeleteAsync(id);
            return new OkResult();
        }
    }
}