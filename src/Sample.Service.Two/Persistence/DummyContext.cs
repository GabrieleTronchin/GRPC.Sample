using Microsoft.EntityFrameworkCore;
using Sample.GRPC.Server.API.Models;

namespace Sample.GRPC.Server.API.Persistence;



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
