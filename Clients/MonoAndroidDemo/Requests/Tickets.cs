using System;
using System.Collections.Generic;
using System.Text;
using MonoAndroidDemo.Models.Ticket;
using MonoAndroidDemo;
using System.Threading.Tasks;

namespace MonoAndroidDemo.Requests
{
    public class Tickets : Core
    {
        private const string _tickets = "tickets";
        private const string _views = "views";
        private const string _organizations = "organizations";


        public Tickets(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }
        public async Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket)
        {
            var body = new { ticket };
            return await GenericPostAsync<IndividualTicketResponse>(_tickets + ".json", body);
        }
        public async Task<IndividualTicketResponse> GetTicketAsync(long id)
        {
            return await GenericGetAsync<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }
        public async Task<GroupTicketFieldResponse> GetTicketFieldsAsync()
        {
            return await GenericGetAsync<GroupTicketFieldResponse>("ticket_fields.json");
        }
    }
}
