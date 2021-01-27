using Newtonsoft.Json;

namespace FateGrandCalculator.AtlasAcademy.Json
{
    public class SvtGrailCostNiceJson
    {
        [JsonProperty("0")]
        public ZeroStar ZeroStar { get; set; }

        [JsonProperty("1")]
        public OneStar OneStar { get; set; }

        [JsonProperty("2")]
        public TwoStar TwoStar { get; set; }

        [JsonProperty("3")]
        public ThreeStar ThreeStar { get; set; }

        [JsonProperty("4")]
        public FourStar FourStar { get; set; }

        [JsonProperty("5")]
        public FiveStar FiveStar { get; set; }
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

    public class ZeroStar
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

    public class OneStar
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

    public class TwoStar
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

    public class ThreeStar
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

    public class FourStar
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

    public class FiveStar
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
