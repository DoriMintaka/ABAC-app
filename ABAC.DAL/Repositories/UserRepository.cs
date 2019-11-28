using ABAC.DAL.Context;
using ABAC.DAL.Entities;
using ABAC.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABAC.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users.SingleOrDefaultAsync(i => i.Login == login);
        }

        public async Task CreateOrUpdateAsync(User entity)
        {
            var item = await _context.Users.FindAsync(entity.Id);

            if (item == null)
            {
                _context.Users.Add(entity);
            }
            else
            {
                item.Name = entity.Name;
                item.Login = entity.Login;
                item.Password = entity.Password;
                item.Attributes = entity.Attributes;

                _context.Users.Update(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await _context.Users.FindAsync(id);

            if (item == null)
            {
                throw new ArgumentException();
            }

            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
