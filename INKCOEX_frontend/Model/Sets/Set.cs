using System.Text.Json.Serialization;

namespace INKCOEX_frontend.Model.Sets;

public class Set
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; }
    [JsonPropertyName("image_data")] 
    public string? ImageData { get; set; }
}