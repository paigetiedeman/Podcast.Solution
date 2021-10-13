using Microsoft.EntityFrameworkCore;

namespace Podcast.Models
{
  public class PodcastContext : DbContext
  {

    public DbSet<Category> Categories { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<CategoryTitle> CategoryTitle { get; set; }
    public PodcastContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}