using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace OpenroomDapr.Server.Services
{
    public class FirstPartyAnalyticsService
    {
        public FirstPartyAnalyticsService()
        {
        }

        [Route("[controller]")]
        public async Task<bool> PostAsync(string key, string value)
        {
            HttpClient client = new();
            string request = $"[{{\"key\":\"{key}\",\"value\":\"{value}\"}}]";
            using StringContent jsonContent = new(request, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync(
                "http://localhost:3500/v1.0/state/statestore",
                jsonContent);
            return true;
        }
    }
}
