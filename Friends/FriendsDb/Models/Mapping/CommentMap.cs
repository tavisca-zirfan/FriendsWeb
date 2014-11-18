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

            this.Property(t => t.TypeId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Comment1)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Comment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommentId).HasColumnName("CommentId");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.Comment1).HasColumnName("Comment");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.CommentTime).HasColumnName("CommentTime");

            // Relationships
            this.HasRequired(t => t.UserCredential)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.UserId);

        }
    }
}
