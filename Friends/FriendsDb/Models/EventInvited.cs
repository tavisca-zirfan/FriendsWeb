using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class EventInvited
    {
        public int Id { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public Nullable<int> Attending { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public virtual Event Event { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
