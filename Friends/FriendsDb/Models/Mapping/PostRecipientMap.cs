using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class PostRecipientMap : EntityTypeConfiguration<PostRecipient>
    {
        public PostRecipientMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecipientId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PostId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PostType)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PostRecipient");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RecipientId).HasColumnName("RecipientId");
            this.Property(t => t.PostId).HasColumnName("PostId");
            this.Property(t => t.PostType).HasColumnName("PostType");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.PostRecipients)
                .HasForeignKey(d => d.PostId);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.PostRecipients)
                .HasForeignKey(d => d.RecipientId);

        }
    }
}
