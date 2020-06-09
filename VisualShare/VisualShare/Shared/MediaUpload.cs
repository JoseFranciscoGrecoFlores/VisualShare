namespace VisualShare.Shared
{
    public class MediaUpload    
    {
        private MediaUpload()
        {
        }
        
        public MediaUpload(byte[] content, string extension, string author)
        {
            Content = content;
            Extension = extension;
            Author = author;
        }

        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public string Author { get; set; }
    }
}