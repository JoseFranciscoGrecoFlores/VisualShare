using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VisualShare.Shared
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        [Key]
        public string Name { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Video> Videos { get; set; }
    }
}