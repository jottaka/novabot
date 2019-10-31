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
    public class QuotesController : ControllerBase
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuoteRepository _quotesRepository;

        public QuotesController(
            ILogger<QuotesController> logger,
            IQuoteRepository quotesRepository)
        {
            _logger = logger;
            _quotesRepository = quotesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuote([FromBody] QuoteModel quote)
        {
            try
            {
                var quoteId = await _quotesRepository.AddQuoteAsync(quote);
                return Ok(quoteId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Não foi possivel adicionar quote: {e}");
                return BadRequest();
            }
        }

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
