using FlightSaverApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration;

public class SocialPostConfiguration : IEntityTypeConfiguration<SocialPost>
{
    public void Configure(EntityTypeBuilder<SocialPost> builder)
    {
        builder
            .HasOne(s => s.User)
            .WithMany(u => u.SocialPosts)
            .HasForeignKey(s => s.UserId)
            .IsRequired();

        builder
            .HasMany(s => s.Comments)
            .WithOne(c => c.SocialPost)
            .HasForeignKey(s => s.SocialPostId)
            .IsRequired(false);
        
        builder.Property(s => s.PostedAt)
            .IsRequired();
        
        builder.Property(s => s.UpdatedAt)
            .IsRequired();
        
        builder.Property(s => s.Location)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(s => s.Content)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(s => s.ImageUrl)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder.Property(s => s.LikesCount)
            .IsRequired();

        builder.Property(s => s.CommentsCount)
            .IsRequired();
        
    }
}