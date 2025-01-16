using FlightSaverApi.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration.Review;

public class AirlineReviewConfiguration : IEntityTypeConfiguration<AirlineReview>
{
    public void Configure(EntityTypeBuilder<AirlineReview> builder)
    {
        builder.Property(u => u.Rating)
            .IsRequired();

        builder.Property(u => u.Comment)
            .HasMaxLength(200);

        builder
            .HasOne(a => a.Airline)
            .WithMany(a => a.AirlineReviews)
            .HasForeignKey(a => a.AirlineId)
            .IsRequired();

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.AirlineReviews)
            .HasForeignKey(r => r.UserId)
            .IsRequired();
                
        builder
            .HasOne(r => r.Flight)
            .WithOne(r => r.AirlineReview)
            .HasForeignKey<AirlineReview>(r => r.FlightId)
            .IsRequired();
    }
}