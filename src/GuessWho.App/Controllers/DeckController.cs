using GuessWho.Execution.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWho.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckFetcher _idolFetcher;

        public DeckController(IDeckFetcher idolFetcher)
        {
            _idolFetcher = idolFetcher;
        }

        [HttpGet]
        [Route("{deckId}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _idolFetcher.GetDeckById(Constants.OurBiasTheme);
            return Ok(result);
        }
    }
}
