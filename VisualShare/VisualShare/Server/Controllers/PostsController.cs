using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using VisualShare.Shared;

namespace VisualShare.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public PostsController(DatabaseContext dbContext)
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

        [HttpGet]
        public List<Post> GetAllPosts()
        {
            var imagePosts = _dbContext.Photos.Include(photo => photo.Comments)
                .Include(photo => photo.Likes)
                .Select(photo => new Post(photo.PublishedDate, $"photos/{photo.Id}", photo.Comments, photo.Id, true, photo.Likes, photo.AuthorName))
                .ToList();

            var videoPosts = _dbContext.Videos.Include(video => video.Comments)
                .Include(video => video.Likes)
                .Select(video => new Post(video.PublishedDate, $"videos/{video.Id}", video.Comments, video.Id, false, video.Likes, video.AuthorName))
                .ToList();

            var posts = imagePosts.Concat(videoPosts).OrderByDescending(post => post.PublishedDate).ToList();
            
            foreach (var post in posts)
                post.Comments = post.Comments.OrderByDescending(comment => comment.PublishedDate).ToList();
            
            return posts;
        }
             
        [HttpPost]
        public async Task Upload(MediaUpload media)
        {
            if(media.Extension.IsExtensionImage()) 
                _dbContext.Photos.Add(new Photo(media.Content, media.Extension, media.Author));
            else
                _dbContext.Videos.Add(new Video(media.Content, media.Extension, media.Author));
            await _dbContext.SaveChangesAsync();
        }
    }
}