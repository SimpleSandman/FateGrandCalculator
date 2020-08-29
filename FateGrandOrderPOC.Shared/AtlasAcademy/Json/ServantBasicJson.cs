using Newtonsoft.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy.Json
{
    public class ServantBasicJson
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("collectionNo")]
        public int CollectionNo { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("face")]
        public string Face { get; set; }
    }
}
