using AgainHandbook.Models;
using Microsoft.EntityFrameworkCore;

namespace Again.DataAcess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
        
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Library> Libraries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "SOP", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Circular", DisplayOrder = 2 }
                );

            modelBuilder.Entity<Library>().HasData(
                new Library { Id = 1, Name = "Sos file test", CategoryId = 1},
                new Library { Id = 2, Name = "circular file test", CategoryId = 2 },
                new Library { Id = 3, Name = " I am new file test", CategoryId = 1 }

                );
        }

    }
}
