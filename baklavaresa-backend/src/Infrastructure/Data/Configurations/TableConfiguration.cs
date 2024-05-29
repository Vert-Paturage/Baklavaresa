using Domain.Entities;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal sealed class TableConfiguration: IEntityTypeConfiguration<TableDatabase>
{
    public void Configure(EntityTypeBuilder<TableDatabase> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Capacity)
            .IsRequired();
    }
}