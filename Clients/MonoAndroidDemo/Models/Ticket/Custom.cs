﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MonoAndroidDemo.Models.Ticket
{
    public class Custom
    {
        [JsonProperty("time_spent")]
        public string TimeSpent { get; set; }
    }
}
