using Microsoft.EntityFrameworkCore;

namespace DBOperationWithEFCore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().HasData(
            new Currency() { Id = 1, Title = "INR", Description = "Indian INR" },
            new Currency() { Id = 2, Title = "USD", Description = "US Dollar" },
            new Currency() { Id = 3, Title = "EUR", Description = "Euro Currencies" },
            new Currency() { Id = 4, Title = "GBP", Description = "GB Dollar" },
            new Currency() { Id = 5, Title = "Dinar", Description = "Dinar" }
        );


        modelBuilder.Entity<Language>().HasData(
            new Language() { Id = 1, Title = "English", Description = "English" },
            new Language() { Id = 2, Title = "French", Description = "French" },
            new Language() { Id = 3, Title = "Dutch", Description = "Dutch" }
        );
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<BookPrice> BookPrices { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Author> Authors { get; set; }
}