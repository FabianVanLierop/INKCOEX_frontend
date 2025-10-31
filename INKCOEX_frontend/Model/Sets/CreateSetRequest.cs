using INKCOEX_frontend.JsonConverters;
using System.Text.Json.Serialization;

namespace INKCOEX_frontend.Model.Sets;

public class CreateSetRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    [JsonPropertyName("release_date")]
    [JsonConverter(typeof(DateOnlyConverter))] 
    public DateTime ReleaseDate { get; set; }
    /*[JsonPropertyName("image_data")]
    public string? ImageData { get; set; }*/
}