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
    public class ImagesController : ControllerBase
    {
        private readonly IImageFetcher _imageFetcher;

        public ImagesController(IImageFetcher imageFetcher)
        {
            _imageFetcher = imageFetcher;
        }

        [HttpGet]
        [Route("{imageName}")]
        public async Task<IActionResult> GetImageByName(string imageName)
        {
            var result = await _imageFetcher.GetImage(imageName);
            return Ok(result);
        }
    }
}
