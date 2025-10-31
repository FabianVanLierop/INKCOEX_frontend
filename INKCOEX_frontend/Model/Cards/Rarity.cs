using System.Text.Json.Serialization;

namespace INKCOEX_frontend.Model.Cards;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    DoubleRare,
    IllustrationRare,
    SpecialIllustrationRare,
    HyperRare,
    Promo,
}