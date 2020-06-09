namespace VisualShare.Shared
{
    public class Like
    {
        public Like()
        {
        }
        
        public Like(string author)
        {
            Author = author;
        }

        public string Author { get; set; }
        public int Id { get; set; }
    }
}