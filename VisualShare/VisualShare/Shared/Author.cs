using System.ComponentModel.DataAnnotations;

namespace VisualShare.Shared
{
    public class Author
    {
        [Key]
        public string Name { get; set; }

        public Author()
        {
        }

        public Author(string name)
        {
            Name = name;
        }
    }
}