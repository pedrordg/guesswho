using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuessWho.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetCardById(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCards()
        {
            return Ok();
        }
    }
}
