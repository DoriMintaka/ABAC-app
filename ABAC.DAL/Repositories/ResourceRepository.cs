using ABAC.DAL.Context;
using ABAC.DAL.Entities;
using ABAC.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABAC.DAL.Repositories
{
    public class ResourceRepository : IEntityRepository<Resource>
    {
        private readonly AppDbContext context;

        public ResourceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Resource>> GetAsync()
        {
            return await context.Resources.ToListAsync();
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            return await context.Resources.FindAsync(id);
        }

        public async Task CreateOrUpdateAsync(Resource entity)
        {
            var item = await context.Resources.FindAsync(entity.Id);

            if (item == null)
            {
                context.Resources.Add(entity);
            }
            else
            {
                item.Name = entity.Name;
                item.Value = entity.Value;
                item.Attributes = entity.Attributes;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await context.Resources.FindAsync(id);

            if (item == null)
            {
                return;
            }

            context.Resources.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
