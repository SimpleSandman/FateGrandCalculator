using System.Collections.Generic;

using Newtonsoft.Json;

namespace FateGrandCalculator.AtlasAcademy.Json
{
    public class MysticCodeNiceJson
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("maxLv")]
        public int MaxLv { get; set; }

        [JsonProperty("extraAssets")]
        public ExtraAssetsMysticCode ExtraAssets { get; set; }

        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; }

        [JsonProperty("expRequired")]
        public List<int> ExpRequired { get; set; }
    }

    public class Item
    {
        [JsonProperty("male")]
        public string Male { get; set; }

        [JsonProperty("female")]
        public string Female { get; set; }
    }

    public class MasterFace
    {
        [JsonProperty("male")]
        public string Male { get; set; }

        [JsonProperty("female")]
        public string Female { get; set; }
    }

    public class MasterFigure
    {
        [JsonProperty("male")]
        public string Male { get; set; }

        [JsonProperty("female")]
        public string Female { get; set; }
    }

    public class ExtraAssetsMysticCode
    {
        [JsonProperty("item")]
        public Item Item { get; set; }

        [JsonProperty("masterFace")]
        public MasterFace MasterFace { get; set; }

        [JsonProperty("masterFigure")]
        public MasterFigure MasterFigure { get; set; }
    }
}
