using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Product()
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7156/api/Products/GetProducts";
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var productList = JsonConvert.DeserializeObject<List<Product>>(content);
                var viewModel = new ProductViewModel
                {
                    Products = productList
                };
                return View(viewModel);
            }

            return View();
        }
        public async Task<IActionResult> GetFilteredProducts(int categoryId)
        {
            HttpClient client = new HttpClient();
            string filteredURL = "https://localhost:7156/api/Products/GetProductsByCategory?categoryId=1";
            var filteredResponse = client.GetAsync(filteredURL).Result;
            if (filteredResponse.IsSuccessStatusCode)
            {
                var content = filteredResponse.Content.ReadAsStringAsync().Result;
                var productList = JsonConvert.DeserializeObject<List<Product>>(content);
                var viewModel = new ProductViewModel
                {
                    Products = productList
                };
                var filteredProducts = viewModel;
                return Json(filteredProducts);
            }
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}