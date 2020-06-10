namespace VisualShare.Shared
{
    public class MediaUpload    
    {
        private MediaUpload()
        {
        }
        
        public MediaUpload(byte[] content, string extension, string author, string category)
        {
            Content = content;
            Extension = extension;
            Author = author;
            Category = category;
        }

        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public string Author { get; set; }
        public string Category { get; set;  }
    }
}