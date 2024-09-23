using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
  // Linking up database to the code
  public DbSet<Stock> Stocks { get; set; }
  public DbSet<Comment> Comments { get; set; }
}
