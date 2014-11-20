using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FriendsDb.Models.Mapping
{
    public class EventInvitedMap : EntityTypeConfiguration<EventInvited>
    {
        public EventInvitedMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.EventId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EventInvited");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EventId).HasColumnName("EventId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Attending).HasColumnName("Attending");
            this.Property(t => t.Time).HasColumnName("Time");

            // Relationships
            this.HasRequired(t => t.Event)
                .WithMany(t => t.EventInviteds)
                .HasForeignKey(d => d.EventId);
            this.HasRequired(t => t.UserCredential)
                .WithMany(t => t.EventInviteds)
                .HasForeignKey(d => d.UserId);

        }
    }
}
