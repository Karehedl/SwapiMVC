using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            //route creates the endpoint we'd like to hit.
            string route = $"people?page={page ?? "1"}";
            //passes this route into our _httpClient's GetAsync method and capture the response.
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress+route);
        
            var responseString = await response.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<ResultsViewModel<PeopleViewModel>>(responseString);

            return View(people);
        }

        public async Task<IActionResult> Person(string id)
        {
            var response = await _httpClient.GetAsync($"people/{id}");
                if (id is null || response.IsSuccessStatusCode == false)
                    return RedirectToAction(nameof(Index));

            var responseString = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<PeopleViewModel>(responseString);
            return View(person);
        }
    }
}