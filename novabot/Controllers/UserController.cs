using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using NovaBot.Repositories.interfaces;

namespace NovaBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(
            ILogger<UserController> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserViewModel user)
        {
            try
            {
                var userId = await _userRepository.AddUserAsync(user);
                return Ok(userId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel adicionar usuario: {e}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserViewModel user)
        {
            try
            {
                await _userRepository.UpdateUserAsync(user);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel modificar usuario: {e}");
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _userRepository.GetUsersAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel listar de usuarios: {e}");
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteQuote([FromBody] string userId)
        {
            try
            {
                await _userRepository.DeleteUserAsync(userId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel deletar usuario: {e}");
                return BadRequest();
            }
        }

    }
}
