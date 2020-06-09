using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisualShare.Shared;

namespace VisualShare.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public LikesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        public async Task Upload(UploadLike uploadLike)
        {
            var like = new Like(uploadLike.Author);
            _dbContext.Likes.Add(like);
            if (uploadLike.IsPostPhoto)
            {
                var photo = await _dbContext.Photos.Include(photo => photo.Likes).SingleAsync(photo => photo.Id == uploadLike.PostId);
                photo.Likes.Add(like);
            }
            else
            {
                var video = await _dbContext.Videos.Include(video => video.Likes).SingleAsync(video => video.Id == uploadLike.PostId);
                video.Likes.Add(like);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}