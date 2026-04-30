using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Testecsharp.Models;

namespace Testecsharp.Pages;

public class InfopaisModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InfopaisModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Pais InfoPais { get; set; } = new();
    public string CodigoPais { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string cod)
    {
        CodigoPais = cod ?? string.Empty;
        if (string.IsNullOrWhiteSpace(cod))
        {
            return Page();
        }

        var client = _httpClientFactory.CreateClient("RestCountries");
        var response = await client.GetAsync($"v3.1/alpha/{cod}?fields=name,cca2,flags");

        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var country = JsonSerializer.Deserialize<CountryApiResponse>(json, options);

        if (country == null)
        {
            return NotFound();
        }

        InfoPais = new Pais
        {
            OfficialName = country.name?.official ?? string.Empty,
            Cca2 = country.cca2 ?? string.Empty,
            FlagUrl = country.flags?.png ?? string.Empty
        };

        return Page();
    }
}
