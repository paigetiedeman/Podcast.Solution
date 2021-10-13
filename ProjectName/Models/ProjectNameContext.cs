using Microsoft.EntityFrameworkCore;

namespace ProjectName.Models
{
  public class ProjectNameContext : DbContext
  {

    public ProjectNameContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}