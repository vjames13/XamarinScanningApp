using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MonoAndroidDemo.Models.Ticket
{
    public class GroupTicketFieldResponse : GroupResponseBase
    {
        [JsonProperty("ticket_fields")]
        public IList<TicketField> TicketFields { get; set; }
    }
}
