using Microsoft.EntityFrameworkCore;
using Tech.Domain.Entities;

namespace Tech.DAL.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
