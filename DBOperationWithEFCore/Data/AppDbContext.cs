using Microsoft.EntityFrameworkCore;

namespace DBOperationWithEFCore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}