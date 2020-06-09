using System;
using System.Collections.Generic;

namespace VisualShare.Shared
{
    public class Post
    {
        public Post()
        {
        }
        
        public Post(DateTimeOffset publishedDate, string contentUrl, List<Comment> comments, int postId, bool isPhoto, List<Like> likes, string authorName)
        {
            PublishedDate = publishedDate;
            ContentURL = contentUrl;
            Comments = comments;
            PostId = postId;
            IsPhoto = isPhoto;
            Likes = likes;
            AuthorName = authorName;
        }

        public DateTimeOffset PublishedDate { get; set; }
        public string ContentURL { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public int PostId { get; set; }
        public bool IsPhoto { get; set; }
        public string AuthorName { get; set; }
    }
}