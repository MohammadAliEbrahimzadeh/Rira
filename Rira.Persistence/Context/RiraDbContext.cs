using Microsoft.EntityFrameworkCore;
using Rira.Domain.Models;
using Rira.Persistence.Configurations;

namespace Rira.Persistence.Context;

public class RiraDbContext : DbContext
{
    public RiraDbContext(DbContextOptions<RiraDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
