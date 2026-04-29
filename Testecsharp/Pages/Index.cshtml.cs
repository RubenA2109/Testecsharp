using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Testecsharp.Models;

namespace Testecsharp.Pages;

public class IndexModel : PageModel

{
    private readonly IHttpClientFactory _httpClientFactory;
    public IndexModel(IHttpClientFactory httpClientFactory)

    {
        _httpClientFactory = httpClientFactory;
    }


    public List<Pais> Paises { get; set; } = new();

    public async Task OnGetAsync()

    {
        //var client = _httpClientFactory.CreateClient();
        //var response = await client.GetAsync("https://restcountries.com/v3.1/all");
        var client = _httpClientFactory.CreateClient("RestCountries");
        var response = await client.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,currencies,cca2,flags");

        if (response.IsSuccessStatusCode)

        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options);

            if (dados != null)
            {
                Paises = dados.Select(d => new Pais
                {
                    OfficialName = d.name?.official ?? string.Empty,
                    Cca2 = d.cca2 ?? string.Empty,
                    FlagUrl = d.flags?.png ?? string.Empty
                }).ToList();
            }

        }

    }


    /*

private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)

    {
        _logger = logger;
    }


    public void OnGet()

    {

    }

*/

}