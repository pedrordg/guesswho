using GuessWho.Execution.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GuessWho.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CardsController : ControllerBase
    {
        private readonly IIdolFetcher _idolFetcher;

        public CardsController(IIdolFetcher idolFetcher)
        {
            _idolFetcher = idolFetcher;
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetCardById(string id)
        {
            var result = await _idolFetcher.GetIdolById(Constants.OurBiasTheme, id);
            return Ok(result);
        }

        [HttpGet]
        [Route("deck/{deckId}")]
        public async Task<IActionResult> GetCardsByDeck(string deckId)
        {
            var result = await _idolFetcher.GetIdolsByDeck(Constants.OurBiasTheme);
            return Ok(result);
        }
    }
}
