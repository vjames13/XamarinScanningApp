﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MonoAndroidDemo.Models.Requests
{
    public class IndividualRequestResponse
    {
        [JsonProperty("request")]
        public Request Request { get; set; }
    }
}
