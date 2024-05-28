using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal sealed class ReservationConfiguration: IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(t => t.FirstName)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.LastName)
            .HasMaxLength(200)
            .IsRequired();
    }
}