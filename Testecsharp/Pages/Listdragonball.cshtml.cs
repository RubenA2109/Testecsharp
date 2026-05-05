using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Testecsharp.Models;

namespace Testecsharp.Pages;

public class Draggonballmodel : PageModel

{
    private readonly IHttpClientFactory _httpClientFactory;
    public Draggonballmodel(IHttpClientFactory httpClientFactory)

    {
        _httpClientFactory = httpClientFactory;
    }


    public List<Pais> Draggon { get; set; } = new();

    public async Task OnGetAsync()

    {
        var client = _httpClientFactory.CreateClient("dragonball");
        var response = await client.GetAsync("https://dragonball-api.com/ ");

        if (response.IsSuccessStatusCode)

        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options) ?? new List<CountryApiResponse>();

            if (dados != null)
            {
                Draggon = dados.Select(d => new Pais
                {
                    OfficialName = d.name?.official ?? string.Empty,
                    Cca2 = d.cca2 ?? string.Empty,
                    FlagUrl = d.flags?.png ?? string.Empty
                }).ToList();
            }

        }

    }

}