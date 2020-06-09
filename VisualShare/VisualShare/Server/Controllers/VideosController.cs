using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using VisualShare.Shared;

namespace VisualShare.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public VideosController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<FileContentResult> Get(int id)
        {
            var video = await _dbContext.Videos.FindAsync(id);
            string type;
            new FileExtensionContentTypeProvider().TryGetContentType(video.Extension, out type);
            return new FileContentResult(video.Content, type) {EnableRangeProcessing = true};
        }
    }
}