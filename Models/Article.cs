using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MeraServer.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Text { get; set; }
    }

    public class ArticlesDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
    }
}