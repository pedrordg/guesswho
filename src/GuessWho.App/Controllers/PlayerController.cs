using GuessWho.Execution.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [Route("")]
        public async Task<IActionResult> GetById()
        {
            var result = await _fetcher.GetById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(result);
        }

        [HttpGet]
        [Route("friends")]
        public async Task<IActionResult> GetFriends(string id)
        {
            var result = await _fetcher.GetPlayerFriends(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(result);
        }
    }
}
