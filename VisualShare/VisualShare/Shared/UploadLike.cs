namespace VisualShare.Shared
{
    public class UploadLike
    {
        public UploadLike()
        {
        }
        
        public UploadLike(string author, int postId, bool isPostPhoto)
        {
            Author = author;
            PostId = postId;
            IsPostPhoto = isPostPhoto;
        }

        public string Author { get; set; }
        public int PostId { get; set; }
        public bool IsPostPhoto { get; set; }
    }
}