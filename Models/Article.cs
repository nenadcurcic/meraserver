using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MeraServer.Models
{
    public class Article
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Text { get; set; }
    }

    public class ArticlesDbContext : DbContext
    {
        public DbSet<Article> TextContainers { get; set; }
    }
}