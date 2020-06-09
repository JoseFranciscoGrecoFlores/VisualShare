using System;

namespace VisualShare.Shared
{
    public class Comment
    {
        public Comment()
        {
        }
        
        public Comment(string content, string author)
        {
            Content = content;
            Author = author;
        }

        public string Content { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
        public DateTimeOffset PublishedDate { get; set; }
    }
}