using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class LikeMap : EntityTypeConfiguration<Like>
    {
        public LikeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PostId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Like");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PostId).HasColumnName("PostId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LikeType).HasColumnName("LikeType");
            this.Property(t => t.Time).HasColumnName("Time");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.Likes)
                .HasForeignKey(d => d.PostId);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Likes)
                .HasForeignKey(d => d.UserId);

        }
    }
}
