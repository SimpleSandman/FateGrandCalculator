using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FateGrandOrderPOC.AtlasAcademy.Json
{
    public class EquipNiceJson
    {
        /// <summary>
        /// Unique database ID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Equipment ID
        /// </summary>
        [JsonProperty("collectionNo")]
        public int CollectionNo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("lvMax")]
        public int LvMax { get; set; }

        [JsonProperty("extraAssets")]
        public ExtraAssets ExtraAssets { get; set; }

        /// <summary>
        /// Level 1 attack
        /// </summary>
        [JsonProperty("atkBase")]
        public int AtkBase { get; set; }

        /// <summary>
        /// Equipment max level attack
        /// </summary>
        [JsonProperty("atkMax")]
        public int AtkMax { get; set; }

        /// <summary>
        /// Level 1 health
        /// </summary>
        [JsonProperty("hpBase")]
        public int HpBase { get; set; }

        /// <summary>
        /// Equipment max level health
        /// </summary>
        [JsonProperty("hpMax")]
        public int HpMax { get; set; }

        [JsonProperty("growthCurve")]
        public int GrowthCurve { get; set; }

        /// <summary>
        /// Attack numbers from level 0 to max rarity level (base 0)
        /// </summary>
        [JsonProperty("atkGrowth")]
        public List<int> AtkGrowth { get; set; }

        /// <summary>
        /// Health numbers from level 0 to max rarity level (base 0)
        /// </summary>
        [JsonProperty("hpGrowth")]
        public List<int> HpGrowth { get; set; }

        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }

    public class Equip
    {
        /// <summary>
        /// Display the asset link using .GetValue(Id)
        /// </summary>
        [JsonExtensionData]
        public JObject AssetLink { get; set; }
    }

    public class CharaGraph
    {
        [JsonProperty("equip")]
        public Equip Equip { get; set; }
    }


    public class Faces
    {
        [JsonProperty("equip")]
        public Equip Equip { get; set; }
    }

    public class NarrowFigure { }

    public class CharaFigure { }

    public class Commands { }

    public class Status { }

    public class EquipFace
    {
        [JsonProperty("equip")]
        public Equip Equip { get; set; }
    }

    public class ExtraAssets
    {
        [JsonProperty("charaGraph")]
        public CharaGraph CharaGraph { get; set; }

        [JsonProperty("faces")]
        public Faces Faces { get; set; }

        [JsonProperty("narrowFigure")]
        public NarrowFigure NarrowFigure { get; set; }

        [JsonProperty("charaFigure")]
        public CharaFigure CharaFigure { get; set; }

        [JsonProperty("commands")]
        public Commands Commands { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("equipFace")]
        public EquipFace EquipFace { get; set; }
    }

    public class ActIndividuality
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FuncQuestTval
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Buff
    {
        [JsonProperty("script")]
        public Script Script { get; set; }
    }

    public partial class Sval
    {
        [JsonProperty("OnField")]
        public int OnField { get; set; }

        [JsonProperty("Value2")]
        public int? Value2 { get; set; }

        [JsonProperty("UseRate")]
        public int? UseRate { get; set; }

        [JsonProperty("AddCount")]
        public int? AddCount { get; set; }

        [JsonProperty("ShowState")]
        public int? ShowState { get; set; }

        [JsonProperty("Individuality")]
        public int? Individuality { get; set; }

        [JsonProperty("EventId")]
        public int? EventId { get; set; }

        [JsonProperty("RateCount")]
        public int? RateCount { get; set; }

        [JsonProperty("ParamAdd")]
        public int? ParamAdd { get; set; }

        [JsonProperty("ParamMax")]
        public int? ParamMax { get; set; }
    }

    public class TraitVal
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FollowerVal
    {
        [JsonProperty("RateCount")]
        public int RateCount { get; set; }
    }

    public partial class Function
    {
        [JsonProperty("traitVals")]
        public List<TraitVal> TraitVals { get; set; }

        [JsonProperty("followerVals")]
        public List<FollowerVal> FollowerVals { get; set; }
    }

    public class Costume { }

    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("condMessage")]
        public string CondMessage { get; set; }

        [JsonProperty("comment")]
        public string CommentText { get; set; }

        [JsonProperty("condType")]
        public string CondType { get; set; }

        [JsonProperty("condValues")]
        public List<int> CondValues { get; set; }

        [JsonProperty("condValue2")]
        public int CondValue2 { get; set; }
    }

    public class Profile
    {
        [JsonProperty("cv")]
        public string Cv { get; set; }

        [JsonProperty("illustrator")]
        public string Illustrator { get; set; }

        [JsonProperty("costume")]
        public Costume Costume { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("voices")]
        public List<object> Voices { get; set; }
    }
}
