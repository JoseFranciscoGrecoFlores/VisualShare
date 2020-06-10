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
    public class CommentsController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public CommentsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<Comment> GetComment(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }
        
        [HttpPost]
        public async Task Upload(PostCommentUpload commentUpload)
        {
            var author = await _dbContext.Authors.FindAsync(commentUpload.Author);

            if (author == null)
            {
                author = new Author(commentUpload.Author);
                _dbContext.Authors.Add(author);
            }
            
            var comment = new Comment(commentUpload.Commment, author);
            _dbContext.Comments.Add(comment);
            if (commentUpload.IsPhoto)
            {
                var photo = await _dbContext.Photos.Include(photo => photo.Comments).SingleAsync(photo => photo.Id == commentUpload.PostId);
                photo.Comments.Add(comment);
            }
            else
            {
                var video = await _dbContext.Videos.Include(video => video.Comments).SingleAsync(video => video.Id == commentUpload.PostId);
                video.Comments.Add(comment);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}