using System.Collections.Generic;

using Newtonsoft.Json;

namespace FateGrandCalculator.AtlasAcademy.Json
{
    public class ServantBasicJson
    {
        /// <summary>
        /// Unique database ID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Servant ID
        /// </summary>
        [JsonProperty("collectionNo")]
        public int CollectionNo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("atkMax")]
        public int AtkMax { get; set; }

        [JsonProperty("hpMax")]
        public int HpMax { get; set; }

        [JsonProperty("face")]
        public string Face { get; set; }
    }

    [JsonArray]
    public class ServantBasicJsonCollection : List<ServantBasicJson> { }
}
