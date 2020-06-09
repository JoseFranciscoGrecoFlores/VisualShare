using System;
using System.Collections.Generic;

namespace VisualShare.Shared
{
    public class Video
    {
        public Video(byte[] content, string extension, string authorName)
        {
            Content = content;
            Extension = extension;
            AuthorName = authorName;
        }

        public DateTimeOffset PublishedDate { get; private set; }
        public byte[] Content { get; private set; }
        public string Extension { get; private set; }
        public int Id { get; set; }
        public List<Comment> Comments { get; private set; }
        public List<Like> Likes { get; private set; }
        public string AuthorName { get; private set; }
    }
}