using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IEnumerable<RuleInfo>> GetRulesAsync()
        {
            return await service.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<RuleInfo> GetRuleAsync([FromRoute] int id)
        {
            return await service.GetAsync(id);
        }

        [HttpPost("")]
        public async Task CreateOrUpdateRuleAsync([FromBody] RuleInfo rule)
        {
            await service.CreateOrUpdateAsync(rule);
        }

        [HttpDelete("{id}")]
        public async Task DeleteRuleAsync([FromRoute] int id)
        {
            await service.DeleteAsync(id);
        }
    }
}