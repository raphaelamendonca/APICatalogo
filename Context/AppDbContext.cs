using Microsoft.EntityFrameworkCore;

namespace APICatalago.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    {

    }
    
}