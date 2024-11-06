﻿using harkka.models;
using Microsoft.EntityFrameworkCore;

namespace harkka.Reporsitories
{
    public class UserRepository : IUserRepository
    {
        MessageServiceContext _context;

        public UserRepository(MessageServiceContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<User?> GetUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _context.Users.Where(user => user.username == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> NewUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}