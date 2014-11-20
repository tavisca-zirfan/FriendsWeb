using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.EventId);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EventId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EventCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Place)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Purpose)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EventId).HasColumnName("EventId");
            this.Property(t => t.EventCode).HasColumnName("EventCode");
            this.Property(t => t.EventType).HasColumnName("EventType");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.EventTime).HasColumnName("EventTime");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");

            // Relationships
            this.HasRequired(t => t.EventType1)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.EventType);
            this.HasRequired(t => t.UserCredential)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.CreatedBy);

        }
    }
}
