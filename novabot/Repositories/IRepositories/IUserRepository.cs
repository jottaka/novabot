using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaBot.Models;
using NovaBot.Models.ViewModels;

namespace NovaBot.Repositories.interfaces
{

    public interface IUserRepository
    {

        Task<List<UserViewModel>> GetUsersAsync();
        Task<UserViewModel> GetUserAsync(string userId);
        Task<string> AddUserAsync(UserViewModel user);
        Task UpdateUserAsync(UserViewModel user);
        Task DeleteUserAsync(string userId);
    }

}
