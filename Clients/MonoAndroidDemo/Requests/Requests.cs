using System;
using System.Collections.Generic;
using System.Text;
using MonoAndroidDemo.Models.Requests;
using System.Threading.Tasks;

namespace MonoAndroidDemo.Requests
{
    public class Requests : Core
    {
        public Requests(string yourZendeskUrl, string user, string password, string apiToken)
           : base(yourZendeskUrl, user, password, apiToken)
        {
        }
        public async Task<IndividualRequestResponse> CreateRequestAsync(Request request)
        {
            var body = new { request };
            return await GenericPostAsync<IndividualRequestResponse>("requests.json", body);
        }
    }
}
