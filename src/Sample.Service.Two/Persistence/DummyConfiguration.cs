using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Service.Two.Models;

namespace Sample.Service.Two.Persistence;

internal class DummyConfiguration : IEntityTypeConfiguration<DummyEntity>
{
    public void Configure(EntityTypeBuilder<DummyEntity> builder)
    {
        builder.HasKey(t => t.Id);
    }
}

