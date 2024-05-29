using Domain.Entities;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal sealed class ReservationConfiguration: IEntityTypeConfiguration<ReservationDatabase>
{
    public void Configure(EntityTypeBuilder<ReservationDatabase> builder)
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
        builder.Property(t => t.Email)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Date)
            .IsRequired();
        builder.Property(t => t.NumberOfPeople)
            .IsRequired();
        builder.HasMany(t => t.Tables)
            .WithOne(t => t.Reservation)
            .HasForeignKey(t => t.ReservationId);
    }
}