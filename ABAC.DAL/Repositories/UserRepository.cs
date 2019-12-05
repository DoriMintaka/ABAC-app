using ABAC.DAL.Context;
using ABAC.DAL.Entities;
using ABAC.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABAC.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.Where(u => u.Id == id).Include(u => u.Attributes).SingleOrDefaultAsync();
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await context.Users.Where(u => u.Login == login).Include(u => u.Attributes).SingleOrDefaultAsync();
        }

        public async Task CreateOrUpdateAsync(User entity)
        {
            var item = await context.Users.FindAsync(entity.Id);

            if (item == null)
            {
                context.Users.Add(entity);
            }
            else
            {
                item.Name = entity.Name;
                item.Login = entity.Login;
                item.Password = entity.Password;
                item.Attributes = entity.Attributes;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await context.Users.FindAsync(id);

            if (item != null)
            {
                context.Users.Remove(item);
                await context.SaveChangesAsync();
            }
        }
    }
}
