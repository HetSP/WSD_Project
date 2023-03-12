using Microsoft.EntityFrameworkCore;
namespace NoteTakingSystem.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Notes> Notes { get; set; } = null!;
    }
}
