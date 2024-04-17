using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.GRPC.Server.API.Models;

namespace Sample.GRPC.Server.API.Persistence;

internal class DummyConfiguration : IEntityTypeConfiguration<DummyEntity>
{
    public void Configure(EntityTypeBuilder<DummyEntity> builder)
    {
        builder.HasKey(t => t.Id);
    }
}

