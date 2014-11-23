using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Event
    {
        public Event()
        {
            this.EventInviteds = new List<EventInvited>();
        }

        public int Id { get; set; }
        public string EventId { get; set; }
        public string EventCode { get; set; }
        public int EventType { get; set; }
        public string Place { get; set; }
        public string Purpose { get; set; }
        public System.DateTime EventTime { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<EventInvited> EventInviteds { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
