using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using NovaBot.Repositories.interfaces;
using NovaBot.Data;

namespace NovaBot.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<string> AddUserAsync(UserViewModel user)
        {
            try
            {
                await _context.User.AddAsync(new UserModel(user));
                await _context.SaveChangesAsync();
                return user.UserId;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            try
            {
                var toRemove = _context.User.FirstOrDefault(
                   u=>u.UserId == userId
                    );
                _context.Remove(toRemove);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<UserViewModel> GetUserAsync(string userId)
        {
            try
            {
                var user = await _context.User.AsNoTracking()
                    .FirstOrDefaultAsync(u=>u.UserId==userId);
                return user.ToViewModel();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            try
            {
                var users = await _context.User.AsNoTracking().ToListAsync(); ;
                return users.ConvertAll(u => u.ToViewModel()); 
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task UpdateUserAsync(UserViewModel user)
        {
            try
            {
                var userToUpdate = await _context.User.FirstOrDefaultAsync(u => u.UserId == user.UserId);
                if(userToUpdate is null)
                {
                    throw new KeyNotFoundException("Usuario não encontrado.");
                }
                userToUpdate.FromViewModel(user);
                await _context.SaveChangesAsync();
                return;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}