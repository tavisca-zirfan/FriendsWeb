using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class EventType
    {
        public EventType()
        {
            this.Events = new List<Event>();
        }

        public int EventTypeId { get; set; }
        public string EventType1 { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
