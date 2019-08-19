using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MonoAndroidDemo.Models
{
    public class To
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
