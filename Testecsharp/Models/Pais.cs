namespace Testecsharp.Models
{
    public class Pais
    {
        public string OfficialName { get; set; } = string.Empty;
        public string Cca2 { get; set; } = string.Empty;
        public string FlagUrl { get; set; } = string.Empty;
    }

    public class CountryApiResponse
    {
        public Name? name { get; set; }
        public string? cca2 { get; set; }
        public Flags? flags { get; set; }
    }

    public class Name
    {
        public string? official { get; set; }
    }

    public class Flags
    {
        public string? png { get; set; }
    }
}
