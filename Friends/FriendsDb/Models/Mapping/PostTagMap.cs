using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class PostTagMap : EntityTypeConfiguration<PostTag>
    {
        public PostTagMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.PostId, t.UserId });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PostId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PostTag");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PostId).HasColumnName("PostId");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.PostTags)
                .HasForeignKey(d => d.PostId);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.PostTags)
                .HasForeignKey(d => d.UserId);

        }
    }
}
