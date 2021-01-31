using Newtonsoft.Json;

namespace FateGrandCalculator.AtlasAcademy.Json
{
    public class GrailCostNiceJson
    {
        [JsonProperty("0")]
        public ZeroRarity ZeroRarity { get; set; }

        [JsonProperty("1")]
        public OneRarity OneRarity { get; set; }

        [JsonProperty("2")]
        public TwoRarity TwoRarity { get; set; }

        [JsonProperty("3")]
        public ThreeRarity ThreeRarity { get; set; }

        [JsonProperty("4")]
        public FourRarity FourRarity { get; set; }

        [JsonProperty("5")]
        public FiveRarity FiveRarity { get; set; }
    }

    public class GrailInfo
    {
        [JsonProperty("qp")]
        public int Qp { get; set; }

        [JsonProperty("addLvMax")]
        public int AddLevelMax { get; set; }

        [JsonProperty("frameType")]
        public string FrameType { get; set; }
    }

    public class ZeroRarity
    {
        [JsonProperty("1")]
        public GrailInfo FirstGrail { get; set; }

        [JsonProperty("2")]
        public GrailInfo SecondGrail { get; set; }

        [JsonProperty("3")]
        public GrailInfo ThirdGrail { get; set; }

        [JsonProperty("4")]
        public GrailInfo FourthGrail { get; set; }

        [JsonProperty("5")]
        public GrailInfo FifthGrail { get; set; }

        [JsonProperty("6")]
        public GrailInfo SixthGrail { get; set; }

        [JsonProperty("7")]
        public GrailInfo SeventhGrail { get; set; }

        [JsonProperty("8")]
        public GrailInfo EighthGrail { get; set; }

        [JsonProperty("9")]
        public GrailInfo NinthGrail { get; set; }

        [JsonProperty("10")]
        public GrailInfo TenthGrail { get; set; }
    }

    public class OneRarity
    {
        [JsonProperty("1")]
        public GrailInfo FirstGrail { get; set; }

        [JsonProperty("2")]
        public GrailInfo SecondGrail { get; set; }

        [JsonProperty("3")]
        public GrailInfo ThirdGrail { get; set; }

        [JsonProperty("4")]
        public GrailInfo FourthGrail { get; set; }

        [JsonProperty("5")]
        public GrailInfo FifthGrail { get; set; }

        [JsonProperty("6")]
        public GrailInfo SixthGrail { get; set; }

        [JsonProperty("7")]
        public GrailInfo SeventhGrail { get; set; }

        [JsonProperty("8")]
        public GrailInfo EighthGrail { get; set; }

        [JsonProperty("9")]
        public GrailInfo NinthGrail { get; set; }

        [JsonProperty("10")]
        public GrailInfo TenthGrail { get; set; }
    }

    public class TwoRarity
    {
        [JsonProperty("1")]
        public GrailInfo FirstGrail { get; set; }

        [JsonProperty("2")]
        public GrailInfo SecondGrail { get; set; }

        [JsonProperty("3")]
        public GrailInfo ThirdGrail { get; set; }

        [JsonProperty("4")]
        public GrailInfo FourthGrail { get; set; }

        [JsonProperty("5")]
        public GrailInfo FifthGrail { get; set; }

        [JsonProperty("6")]
        public GrailInfo SixthGrail { get; set; }

        [JsonProperty("7")]
        public GrailInfo SeventhGrail { get; set; }

        [JsonProperty("8")]
        public GrailInfo EighthGrail { get; set; }

        [JsonProperty("9")]
        public GrailInfo NinthGrail { get; set; }

        [JsonProperty("10")]
        public GrailInfo TenthGrail { get; set; }
    }

    public class ThreeRarity
    {
        [JsonProperty("1")]
        public GrailInfo FirstGrail { get; set; }

        [JsonProperty("2")]
        public GrailInfo SecondGrail { get; set; }

        [JsonProperty("3")]
        public GrailInfo ThirdGrail { get; set; }

        [JsonProperty("4")]
        public GrailInfo FourthGrail { get; set; }

        [JsonProperty("5")]
        public GrailInfo FifthGrail { get; set; }

        [JsonProperty("6")]
        public GrailInfo SixthGrail { get; set; }

        [JsonProperty("7")]
        public GrailInfo SeventhGrail { get; set; }

        [JsonProperty("8")]
        public GrailInfo EighthGrail { get; set; }

        [JsonProperty("9")]
        public GrailInfo NinthGrail { get; set; }
    }

    public class FourRarity
    {
        [JsonProperty("1")]
        public GrailInfo FirstGrail { get; set; }

        [JsonProperty("2")]
        public GrailInfo SecondGrail { get; set; }

        [JsonProperty("3")]
        public GrailInfo ThirdGrail { get; set; }

        [JsonProperty("4")]
        public GrailInfo FourthGrail { get; set; }

        [JsonProperty("5")]
        public GrailInfo FifthGrail { get; set; }

        [JsonProperty("6")]
        public GrailInfo SixthGrail { get; set; }

        [JsonProperty("7")]
        public GrailInfo SeventhGrail { get; set; }
    }

    public class FiveRarity
    {
        [JsonProperty("1")]
        public GrailInfo FirstGrail { get; set; }

        [JsonProperty("2")]
        public GrailInfo SecondGrail { get; set; }

        [JsonProperty("3")]
        public GrailInfo ThirdGrail { get; set; }

        [JsonProperty("4")]
        public GrailInfo FourthGrail { get; set; }

        [JsonProperty("5")]
        public GrailInfo FifthGrail { get; set; }
    }
}
