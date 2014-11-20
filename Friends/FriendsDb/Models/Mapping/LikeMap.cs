using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class LikeMap : EntityTypeConfiguration<Like>
    {
        public LikeMap()
        {
            // Primary Key
            this.HasKey(t => t.LikeId);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.LikeId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TypeId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Like");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LikeId).HasColumnName("LikeId");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LikeType).HasColumnName("LikeType");
            this.Property(t => t.Time).HasColumnName("Time");

            // Relationships
            this.HasRequired(t => t.UserCredential)
                .WithMany(t => t.Likes)
                .HasForeignKey(d => d.UserId);

        }
    }
}
