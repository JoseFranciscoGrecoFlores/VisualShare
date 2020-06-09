namespace VisualShare.Shared
{
    public class PostCommentUpload
    {
        public PostCommentUpload()
        {
        }

        public PostCommentUpload(string commment, string author, int postId, bool isPhoto)
        {
            Commment = commment;
            Author = author;
            PostId = postId;
            IsPhoto = isPhoto;
        }

        public string Commment { get; set; }
        public string Author { get; set; }
        public int PostId { get; set; }
        public bool IsPhoto { get; set; }
    }
}