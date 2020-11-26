using Newtonsoft.Json;

namespace FateGrandOrderPOC.AtlasAcademy.Json
{
    /// <summary>
    /// Damage class modifier
    /// </summary>
    public class ClassAttackRateNiceJson
    {
        [JsonProperty("saber")]
        public int Saber { get; set; }

        [JsonProperty("archer")]
        public int Archer { get; set; }

        [JsonProperty("lancer")]
        public int Lancer { get; set; }

        [JsonProperty("rider")]
        public int Rider { get; set; }

        [JsonProperty("caster")]
        public int Caster { get; set; }

        [JsonProperty("assassin")]
        public int Assassin { get; set; }

        [JsonProperty("berserker")]
        public int Berserker { get; set; }

        [JsonProperty("shielder")]
        public int Shielder { get; set; }

        [JsonProperty("ruler")]
        public int Ruler { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgo { get; set; }

        [JsonProperty("avenger")]
        public int Avenger { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillar { get; set; }

        [JsonProperty("grandCaster")]
        public int GrandCaster { get; set; }

        [JsonProperty("beastII")]
        public int BeastII { get; set; }

        [JsonProperty("beastI")]
        public int BeastI { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancer { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIR { get; set; }

        [JsonProperty("foreigner")]
        public int Foreigner { get; set; }

        [JsonProperty("beastIIIL")]
        public int BeastIIIL { get; set; }

        [JsonProperty("beastUnknown")]
        public int BeastUnknown { get; set; }

        [JsonProperty("unknown")]
        public int Unknown { get; set; }

        [JsonProperty("ALL")]
        public int All { get; set; }
    }
}
