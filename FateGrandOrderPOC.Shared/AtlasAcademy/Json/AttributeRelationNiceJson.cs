using Newtonsoft.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy.Json
{
    public class AttributeRelationNiceJson
    {
        [JsonProperty("human")]
        public Human Human { get; set; }

        [JsonProperty("sky")]
        public Sky Sky { get; set; }

        [JsonProperty("earth")]
        public Earth Earth { get; set; }

        [JsonProperty("star")]
        public Star Star { get; set; }

        [JsonProperty("beast")]
        public Beast Beast { get; set; }
    }

    public class Human
    {
        [JsonProperty("human")]
        public int HumanMultiplier { get; set; }

        [JsonProperty("sky")]
        public int SkyMultiplier { get; set; }

        [JsonProperty("earth")]
        public int EarthMultiplier { get; set; }

        [JsonProperty("star")]
        public int StarMultiplier { get; set; }

        [JsonProperty("beast")]
        public int BeastMultiplier { get; set; }
    }

    public class Sky
    {
        [JsonProperty("human")]
        public int HumanMultiplier { get; set; }

        [JsonProperty("sky")]
        public int SkyMultiplier { get; set; }

        [JsonProperty("earth")]
        public int EarthMultiplier { get; set; }

        [JsonProperty("star")]
        public int StarMultiplier { get; set; }

        [JsonProperty("beast")]
        public int BeastMultiplier { get; set; }
    }

    public class Earth
    {
        [JsonProperty("human")]
        public int HumanMultiplier { get; set; }

        [JsonProperty("sky")]
        public int SkyMultiplier { get; set; }

        [JsonProperty("earth")]
        public int EarthMultiplier { get; set; }

        [JsonProperty("star")]
        public int StarMultiplier { get; set; }

        [JsonProperty("beast")]
        public int BeastMultiplier { get; set; }
    }

    public class Star
    {
        [JsonProperty("human")]
        public int HumanMultiplier { get; set; }

        [JsonProperty("sky")]
        public int SkyMultiplier { get; set; }

        [JsonProperty("earth")]
        public int EarthMultiplier { get; set; }

        [JsonProperty("star")]
        public int StarMultiplier { get; set; }

        [JsonProperty("beast")]
        public int BeastMultiplier { get; set; }
    }

    public class Beast
    {
        [JsonProperty("human")]
        public int HumanMultiplier { get; set; }

        [JsonProperty("sky")]
        public int SkyMultiplier { get; set; }

        [JsonProperty("earth")]
        public int EarthMultiplier { get; set; }

        [JsonProperty("star")]
        public int StarMultiplier { get; set; }

        [JsonProperty("beast")]
        public int BeastMultiplier { get; set; }
    }
}
