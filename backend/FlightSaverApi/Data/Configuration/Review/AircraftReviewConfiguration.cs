using FlightSaverApi.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration.Review;

public class AircraftReviewConfiguration : IEntityTypeConfiguration<AircraftReview>
{
    public void Configure(EntityTypeBuilder<AircraftReview> builder)
    {
        builder.Property(u => u.Rating)
            .IsRequired();

        builder.Property(u => u.Comment)
            .HasMaxLength(200);

        builder
            .HasOne(a => a.Aircraft)
            .WithMany(a => a.AircraftReviews)
            .HasForeignKey(a => a.AircraftId)
            .IsRequired();

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.AircraftReviews)
            .HasForeignKey(r => r.UserId)
            .IsRequired();
                
        builder
            .HasOne(r => r.Flight)
            .WithOne(r => r.AircraftReview)
            .HasForeignKey<AircraftReview>(r => r.FlightId)
            .IsRequired();
    }
}