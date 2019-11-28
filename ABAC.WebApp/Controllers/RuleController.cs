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
        public RuleController()
        {

        }

        [HttpGet("")]
        public async Task<IEnumerable<RuleInfo>> GetRulesAsync()
        {
        }

        [HttpGet("{id}")]
        public async Task<RuleInfo> GetRuleAsync([FromRoute] int id)
        {
        }

        [HttpPost("")]
        public async Task CreateOrUpdateRuleAsync([FromBody] RuleInfo resource)
        {
        }

        [HttpDelete("{id}")]
        public async Task DeleteRuleAsync([FromRoute] int id)
        {
        }
    }
}