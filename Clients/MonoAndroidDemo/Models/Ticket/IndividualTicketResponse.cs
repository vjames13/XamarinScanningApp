using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MonoAndroidDemo.Models.Shared;

namespace MonoAndroidDemo.Models.Ticket
{
    public class IndividualTicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }

        [JsonProperty("audit")]
        public Audit Audit { get; set; }
    }
}
