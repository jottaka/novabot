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
    [Route("api/[controller]/[action]")]
    public class QuotesController : ControllerBase
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuoteRepository _quotesRepository;
        private readonly ISlackRepository _slackRepository;


        public QuotesController(
            ILogger<QuotesController> logger,
            IQuoteRepository quotesRepository,
            ISlackRepository slackRepository
            )
        {
            _logger = logger;
            _quotesRepository = quotesRepository;
            _slackRepository = slackRepository;
        }

    
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> AddQuote([FromForm] SlackEventRequestModel quote)
        {
            try
            {
                 var quoteVoteUid = await _quotesRepository.ReceiveNewQuoteEvent(quote);
                new Task(async () => await _slackRepository.SendMessage(quote, quoteVoteUid));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel adicionar quote: {e}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> UpvoteQuote([FromForm] SlackEventRequestModel upvote)
        {
            try
            {
                await _quotesRepository.UpvoteAsync(upvote);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel adicionar voto para quote: {e}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Downvote([FromForm] SlackEventRequestModel upvote)
        {
            try
            {
                await _quotesRepository.DownvoteAsync(upvote);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel adicionar voto para quote: {e}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> List([FromBody] ListQuoteRequestModel request)
        {
            try
            {
                var response = await _quotesRepository.GetListAsync(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel listar quotes: {e}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ListByUser([FromBody] ListQuoteRequestModel request, [FromQuery] string userId)
        {
            try
            {
                var response = await _quotesRepository.GetListByUserAsync(request, userId);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel listar quotes: {e}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ListBySnitch([FromBody] ListQuoteRequestModel request, [FromQuery] string snitchId)
        {
            try
            {
                var response = await _quotesRepository.GetListBySnitchAsync(request, snitchId);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel listar quotes: {e}");
                return BadRequest();
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> AddQuote([FromBody] QuoteModel quote)
        //{
        //    try
        //    {
        //        var quoteId = await _quotesRepository.AddQuoteAsync(quote);
        //        return Ok(quoteId);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError($"Não foi possivel adicionar quote: {e}");
        //        return BadRequest();
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> UpdateQuote([FromBody] QuoteModel quote)
        {
            try
            {
                await _quotesRepository.UpdateQuoteAsync(quote);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel modificar quote: {e}");
                return BadRequest();
            }
        }

        


        [HttpGet]
        public async Task<IActionResult> DeleteQuote([FromBody] string quoteId)
        {
            try
            {
                await _quotesRepository.DeleteQuoteAsync(quoteId);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel deletar quote: {e}");
                return BadRequest();
            }
        }

    }
}
