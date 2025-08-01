using Microsoft.EntityFrameworkCore;

namespace CliNotes
{
    public class NotesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CliNotesDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
