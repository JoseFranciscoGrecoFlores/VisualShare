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
        
        [HttpGet("{categoryName}")]
        public async Task<List<Post>> GetAllPosts(string categoryName)
        {
            var category = await _dbContext.Categories
                .Include(category => category.Photos)
                    .ThenInclude(photo => photo.Likes)
                .Include(category => category.Photos)
                    .ThenInclude(photo => photo.Comments)
                        .ThenInclude(comment => comment.Author)
                .Include(category => category.Photos)
                    .ThenInclude(photo => photo.Author)
                .Include(category => category.Videos)
                    .ThenInclude(video => video.Likes)
                .Include(category => category.Videos)
                    .ThenInclude(video => video.Comments)
                        .ThenInclude(comment => comment.Author)
                .Include(category => category.Videos)
                    .ThenInclude(photo => photo.Author)
                .SingleAsync(category => category.Name == categoryName);
            
            var imagePosts = category.Photos
                .Select(photo => new Post(photo.PublishedDate, $"photos/{photo.Id}", photo.Comments, photo.Id, true, photo.Likes, photo.Author.Name))
                .ToList();

            var videoPosts = category.Videos
                .Select(video => new Post(video.PublishedDate, $"videos/{video.Id}", video.Comments, video.Id, false, video.Likes, video.Author.Name))
                .ToList();

            var posts = imagePosts.Concat(videoPosts).OrderByDescending(post => post.PublishedDate).ToList();
            
            foreach (var post in posts)
                post.Comments = post.Comments.OrderByDescending(comment => comment.PublishedDate).ToList();
            
            return posts;
        }
             
        [HttpPost]
        public async Task<ActionResult> Upload(MediaUpload media)
        {
            var category = await _dbContext.Categories
                .Include(categroy => categroy.Photos)
                .Include(category => category.Videos)
                .SingleAsync(category => category.Name == media.Category);

            if (category == null)
                return StatusCode(403);

            var author = await _dbContext.Authors.FindAsync(media.Author);

            if (author == null)
            {
                author = new Author(media.Author);
                _dbContext.Authors.Add(author);
            }
            
            if (media.Extension.IsExtensionImage())
            {
                var photo = new Photo(media.Content, media.Extension, author);
                _dbContext.Photos.Add(photo);
                category.Photos.Add(photo);
            }
            else
            {
                var video = new Video(media.Content, media.Extension, author);
                _dbContext.Videos.Add(video);
                category.Videos.Add(video);
            }
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}