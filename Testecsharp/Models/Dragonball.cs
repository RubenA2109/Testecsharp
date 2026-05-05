namespace Testecsharp.Models
{
public class DragonBallModel    
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
    }

    public class DragonBallApiResponse
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
    }
}
