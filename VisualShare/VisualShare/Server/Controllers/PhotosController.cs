using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace VisualShare.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public PhotosController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<FileContentResult> Get(int id)
        {
            var image = await _dbContext.Photos.FindAsync(id);
            string type;
            new FileExtensionContentTypeProvider().TryGetContentType(image.Extension, out type);
            return new FileContentResult(image.Content, type);
        }
    }
}
