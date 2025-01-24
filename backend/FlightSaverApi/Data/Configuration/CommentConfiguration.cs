using FlightSaverApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightSaverApi.Data.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .IsRequired();
        
        builder
            .HasOne(c => c.SocialPost)
            .WithMany(s => s.Comments)
            .HasForeignKey(c => c.SocialPostId)
            .IsRequired();

        builder.Property(c => c.PostedAt)
            .IsRequired();
        
        builder.Property(c => c.UpdatedAt)
            .IsRequired(false);
        
        builder.Property(c => c.Content)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(c => c.LikesCount)
            .IsRequired();
    }
}