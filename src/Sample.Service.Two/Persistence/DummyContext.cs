using Microsoft.EntityFrameworkCore;
using Sample.Service.Two.Models;

namespace Sample.Service.Two.Persistence;



public class DummyContext : DbContext
{
    public DummyContext(DbContextOptions<DummyContext> options) : base(options)
    {
    }
    public DbSet<DummyEntity> SampleEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DummyContext).Assembly);
    }

}
