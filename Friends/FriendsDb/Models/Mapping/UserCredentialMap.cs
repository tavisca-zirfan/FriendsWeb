using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class UserCredentialMap : EntityTypeConfiguration<UserCredential>
    {
        public UserCredentialMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("UserCredential");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.LastSeen).HasColumnName("LastSeen");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
