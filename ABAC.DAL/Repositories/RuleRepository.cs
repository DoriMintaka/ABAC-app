using ABAC.DAL.Context;
using ABAC.DAL.Entities;
using ABAC.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABAC.DAL.Repositories
{
    public class RuleRepository : IEntityRepository<Rule>
    {
        private readonly AppDbContext context;

        public RuleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Rule>> GetAsync()
        {
            return await context.Rules.ToListAsync();
        }

        public async Task<Rule> GetByIdAsync(int id)
        {
            return await context.Rules.FindAsync(id);
        }

        public async Task CreateOrUpdateAsync(Rule rule)
        {
            var found = await context.Rules.FindAsync(rule.Id);

            if (found == null)
            {
                await context.Rules.AddAsync(rule);
            }
            else
            {
                found.Value = rule.Value;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var found = await context.Rules.FindAsync(id);

            if (found != null)
            {
                context.Rules.Remove(found);
                await context.SaveChangesAsync();
            }
        }
    }
}
