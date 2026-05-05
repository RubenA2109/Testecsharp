using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Testecsharp.Models;

namespace Testecsharp.Pages;

public class DragonBallpersonagem : PageModel

{
    private readonly IHttpClientFactory _httpClientFactory;
    public DragonBallpersonagem(IHttpClientFactory httpClientFactory)

    {
        _httpClientFactory = httpClientFactory;
    }


    public List<DragonBallModel> Dragonball { get; set; } = new();

    public async Task OnGetAsync()

    {
        var client = _httpClientFactory.CreateClient("RestCountries");

        var response = await client.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,currencies,cca2,flags");

        var response1 = await client.GetAsync("https://web.dragonball-api.com/documentation");

        if (response.IsSuccessStatusCode)

        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options);

            if (dados != null)
            {
                // Dragonball = dados.Select(d => new DragonBallpersonagem
                // {
                //     name = d.name?? string.Empty,
                //     description = d.description ?? string.Empty,
                //     image = d.image?? string.Empty
                //     affliation = d.affliation?? string.Empty
                // }).ToList();
            }

        }

    }

}