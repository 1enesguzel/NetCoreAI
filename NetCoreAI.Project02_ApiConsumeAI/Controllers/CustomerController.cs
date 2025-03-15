using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeAI.Dtos;
using Newtonsoft.Json;

namespace NetCoreAI.Project02_ApiConsumeAI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CustomerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> CreateCostumer()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7107/api/Customers");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
