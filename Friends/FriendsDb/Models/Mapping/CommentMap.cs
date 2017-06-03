using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentId);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CommentId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ForPostId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CommentMessage)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Comment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommentId).HasColumnName("CommentId");
            this.Property(t => t.ForPostId).HasColumnName("ForPostId");
            this.Property(t => t.CommentMessage).HasColumnName("CommentMessage");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithOptional(t => t.Comment);

        }
    }
}
