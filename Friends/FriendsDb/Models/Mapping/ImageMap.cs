using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageId);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ImageId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Text)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Image");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageId).HasColumnName("ImageId");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.UploadTime).HasColumnName("UploadTime");

            // Relationships
            this.HasRequired(t => t.UserCredential)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.UserId);

        }
    }
}
