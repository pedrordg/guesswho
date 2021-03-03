using GuessWho.Execution.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuessWho.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class DeckController : ControllerBase
    {
        private readonly IDeckFetcher _deckFetcher;

        public DeckController(IDeckFetcher deckFetcher)
        {
            _deckFetcher = deckFetcher;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _deckFetcher.GetDeckById(id);
            return Ok(result);
        }
    }
}
