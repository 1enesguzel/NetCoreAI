using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeAI.Dtos;
using Newtonsoft.Json;
using System.Text;

namespace NetCoreAI.Project02_ApiConsumeAI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public CustomerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> CustomerList()
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
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customerDto)
        {
            var client = _clientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(customerDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7107/api/Customers", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7107/api/Customers?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7107/api/Customers/GetCustomer?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdCustomerDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            var client = _clientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateCustomerDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7107/api/Customers", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }
    }
}
