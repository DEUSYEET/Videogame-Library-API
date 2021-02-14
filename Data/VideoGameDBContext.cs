using Microsoft.EntityFrameworkCore;

namespace VGDBAPI.Models
{
    public class VideoGameDBContext : DbContext
    {
        public VideoGameDBContext(DbContextOptions<VideoGameDBContext> options) : base(options){}
        public DbSet<Game> games { get; set; }

    }
}
