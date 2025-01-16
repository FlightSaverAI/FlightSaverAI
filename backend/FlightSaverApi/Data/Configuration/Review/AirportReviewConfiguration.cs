using FlightSaverApi.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration.Review;

public class AirportReviewConfiguration : IEntityTypeConfiguration<AirportReview>
{
    public void Configure(EntityTypeBuilder<AirportReview> builder)
    {
        builder.Property(u => u.Rating)
            .IsRequired();

        builder.Property(u => u.Comment)
            .HasMaxLength(200);

        builder
            .HasOne(a => a.Airport)
            .WithMany(a => a.AirportReviews)
            .HasForeignKey(a => a.AirportId)
            .IsRequired();

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.AirportReviews)
            .HasForeignKey(r => r.UserId)
            .IsRequired();

        builder
            .HasOne(r => r.Flight)
            .WithMany(r => r.AirportReviews)
            .HasForeignKey(r => r.FlightId)
            .IsRequired();
    }
}