using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            // Primary Key
            builder.HasKey(n => n.Id);

            // Property configurations
            builder.Property(n => n.UserId)
                .IsRequired();

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(2000);

            // Enum conversions
            builder.Property(n => n.Type)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (NotificationType)Enum.Parse(typeof(NotificationType), v));

            builder.Property(e => e.Status)
                     .HasConversion(
                         v => v.ToString(),
                         v => (NotificationStatus)Enum.Parse(typeof(NotificationStatus), v)
                     )
                     .HasMaxLength(10);

            // DateTime configurations
            builder.Property(n => n.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(n => n.SentAt)
                .IsRequired(false);

            builder.Property(n => n.ReadAt)
                .IsRequired(false);

            // Related entity properties
            builder.Property(n => n.RelatedEntityId)
                .IsRequired(false);

            builder.Property(n => n.RelatedEntityType)
                .IsRequired(false)
                .HasMaxLength(100);

            // Indexes for query performance
            builder.HasIndex(n => n.UserId);
            builder.HasIndex(n => n.Status);
            builder.HasIndex(n => n.CreatedAt);
            builder.HasIndex(n => new { n.RelatedEntityId, n.RelatedEntityType });
        }
    }
}