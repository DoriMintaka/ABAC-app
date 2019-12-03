using ABAC.DAL.Entities;
using ABAC.DAL.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABAC.DAL.Services.Contracts
{
    public interface IRuleService
    {
        Task<IEnumerable<RuleInfo>> GetAsync();

        Task<RuleInfo> GetAsync(int id);

        Task CreateOrUpdateAsync(RuleInfo rule);

        Task DeleteAsync(int id);

        Task<bool> Validate(int userId, int resourceId);

        Task<bool> Validate(User user, Resource resource);

        Task LoadRulesAsync();
    }
}
