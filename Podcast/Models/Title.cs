using System.Collections.Generic;

namespace Podcast.Models
{
  public class Title 
  {
    public Title()
    {
      this.JoinEntities = new HashSet<CategoryTitle>();
    }

    public int TitleId { get; set; }
    public string PodcastName { get; set; }
    public string Description { get; set; }
    public virtual ICollection<CategoryTitle> JoinEntities { get; set; }
  }
}