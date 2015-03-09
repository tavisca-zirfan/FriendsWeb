using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            // Primary Key
            this.HasKey(t => t.Pid);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Pid)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Post");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Pid).HasColumnName("Pid");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Time).HasColumnName("Time");
            this.Property(t => t.Type).HasColumnName("Type");

            // Relationships
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Posts)
                .HasForeignKey(d => d.Author);

        }
    }
}
