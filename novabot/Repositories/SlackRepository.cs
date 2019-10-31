using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using NovaBot.Repositories.interfaces;
using NovaBot.Data;
using Microsoft.AspNetCore.Mvc;

namespace NovaBot.Repositories
{
    public class SlackRepository : ISlackRepository
    {
        private readonly ApplicationDbContext _context;
        public SlackRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<string> ProcessRequest(SlackEventRequestModel request)
        {
            try
            {
                string response = null;
                switch (request.type)
                {
                    case "url_verification":
                        response = request.challenge;
                        break;
                    case "message":
                        break;
                    default:
                        break;
                }
                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        public async Task GetUserList()
        {
            try
            {
                
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<string> AddQuoteAsync(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}