using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.PostId);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PostId)
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

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PostId).HasColumnName("PostId");
            this.Property(t => t.EventCode).HasColumnName("EventCode");
            this.Property(t => t.EventType).HasColumnName("EventType");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.EventTime).HasColumnName("EventTime");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithOptional(t => t.Event);

        }
    }
}
