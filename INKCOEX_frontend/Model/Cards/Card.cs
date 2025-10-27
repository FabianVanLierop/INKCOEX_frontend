using System.Text.Json.Serialization;

namespace INKCOEX_frontend.Model.Cards;

public class Card
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Number { get; set; }
    public string? Rarity { get; set; }
    [JsonPropertyName("image_data")]
    public string? ImageData { get; set; }

    public bool Owned { get; set; }
}