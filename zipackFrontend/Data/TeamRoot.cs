using System.Text.Json.Serialization;

public class TeamMember
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Ranks")]
    public List<string> Ranks { get; set; }

    [JsonPropertyName("DescEN")]
    public string DescEN { get; set; }

    [JsonPropertyName("DescDE")]
    public string DescDE { get; set; }

    [JsonPropertyName("PFP")]
    public string PFP { get; set; }

    [JsonPropertyName("Sorting")]
    public int Sorting { get; set; }
    
    [JsonPropertyName("Link")]
    public string? Link { get; set; }
}

public class TeamRoot
{
    [JsonPropertyName("data")]
    public List<TeamMember> Data { get; set; }
}