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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerFetcher _fetcher;

        public PlayerController(IPlayerFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        [HttpGet]
        [Route("{deckId}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _fetcher.GetById(id);
            return Ok(result);
        }
    }
}
