using ABAC.DAL.Entities;
using ABAC.DAL.Repositories.Contracts;
using ABAC.DAL.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ABAC.DAL.Repositories
{
    public class ResourceRepository : IEntityRepository<Resource>
    {
		private readonly AppDbContext _context;

		public ResourceRepository(AppDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<Resource>> GetAsync()
        {
			return await _context.Resources.ToListAsync();
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
			return await _context.Resources.SingleOrDefaultAsync(i => i.Id == id);
		}

        public async Task CreateOrUpdateAsync(Resource entity)
        {
			var item = await _context.Resources.FindAsync(entity.Id);

			if (item == null)
			{
				_context.Resources.Add(entity);
			}
			else
			{
				item.Name = entity.Name;
				item.Value = entity.Value;
				item.Attributes = entity.Attributes;

				_context.Resources.Update(item);
			}

			await _context.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
        {
			var item = await _context.Resources.FindAsync(id);

			if (item == null)
			{
				throw new ArgumentException();
			}

			_context.Resources.Remove(item);
			await _context.SaveChangesAsync();
		}
    }
}
