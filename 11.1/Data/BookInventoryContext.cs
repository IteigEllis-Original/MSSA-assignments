using BookInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Data
{
    public class BookInventoryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookInventoryContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=BookInventory.db");
            }
        }
    }
}
