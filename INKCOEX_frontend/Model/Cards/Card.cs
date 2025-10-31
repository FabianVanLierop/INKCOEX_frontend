using System.Text.Json.Serialization;

namespace INKCOEX_frontend.Model.Cards;

public class Card
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Number { get; set; }
    public Rarity Rarity { get; set; }
    [JsonPropertyName("image_data")]
    public string? ImageData { get; set; }
    [JsonPropertyName("set_id")]
    public int SetId { get; set; }

    public bool Owned { get; set; }
}