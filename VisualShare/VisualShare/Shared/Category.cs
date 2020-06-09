using System.Collections.Generic;

namespace VisualShare.Shared
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public int Id { get; set; }
    }
}