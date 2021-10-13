namespace Podcast.Models
{
  public class CategoryTitle
    {       
        public int CategoryTitleId { get; set; }
        public int TitleId { get; set; }
        public int CategoryId { get; set; }
        public virtual Title Title { get; set; }
        public virtual Category Category { get; set; }
    }
}