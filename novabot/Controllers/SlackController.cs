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
    public class SlackController : ControllerBase
    {
        private readonly ILogger<SlackController> _logger;
        private readonly ISlackRepository _slackRepository;
        public SlackController(
            ISlackRepository slackRepository,
            ILogger<SlackController> logger)
        {
            _slackRepository = slackRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SlackVerification([FromBody] SlackEventRequestModel request)
        {
            try
            {
                return Ok( _slackRepository.ProcessRequest(request));
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao receber evento do slack: {e}");
                return BadRequest();
            }
        }


    }
}
