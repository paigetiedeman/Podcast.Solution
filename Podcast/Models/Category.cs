using System.Collections.Generic;

namespace Podcast.Models
{
    public class Category
    {
        public Category()
        {
            this.JoinEntities = new HashSet<CategoryTitle>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CategoryTitle> JoinEntities { get; set; }
    }
}