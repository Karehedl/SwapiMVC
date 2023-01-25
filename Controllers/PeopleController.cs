using Microsoft.AspNetCore.Mvc;
using SwapiMVC.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwapiMVC.Controllers
{
    public class PeopleController : Controller
    {
        private readonly HttpClient _httpClient;
        public PeopleController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("swapi");
        }

        public async Task<IActionResult> Index(string page)
        {
            return View();
            //route creates the endpoint we'd like to hit.
            string route = $"people?page={page ?? "1"}";
            //passes this route into our _httpClient's GetAsync method and capture the response.
            HttpResponseMessage response = await _httpClient.GetAsync(route);
        
            var responseString = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<ResultsViewModel<PeopleViewModel>>(responseString);

            return View(people);
        }
    }
}