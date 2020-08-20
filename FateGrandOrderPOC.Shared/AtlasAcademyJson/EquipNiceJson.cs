using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FateGrandOrderPOC.Shared.AtlasAcademyJson
{
    public class EquipNiceJson
    {
        [JsonProperty("id")]
        public int Id { get; set; }

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

        [JsonProperty("atkBase")]
        public int AtkBase { get; set; }

        [JsonProperty("atkMax")]
        public int AtkMax { get; set; }

        [JsonProperty("hpBase")]
        public int HpBase { get; set; }

        [JsonProperty("hpMax")]
        public int HpMax { get; set; }

        [JsonProperty("growthCurve")]
        public int GrowthCurve { get; set; }

        [JsonProperty("atkGrowth")]
        public List<int> AtkGrowth { get; set; }

        [JsonProperty("hpGrowth")]
        public List<int> HpGrowth { get; set; }

        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }

    public class Equip
    {
        [JsonExtensionData]
        public JObject EquipLink { get; set; }
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

    public class Script { }

    public class Functval
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FuncquestTval
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Val
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Tval
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class CkSelfIndv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class CkOpIndv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Buff
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("buffGroup")]
        public int BuffGroup { get; set; }

        [JsonProperty("script")]
        public Script Script { get; set; }

        [JsonProperty("vals")]
        public List<Val> Vals { get; set; }

        [JsonProperty("tvals")]
        public List<Tval> Tvals { get; set; }

        [JsonProperty("ckSelfIndv")]
        public List<CkSelfIndv> CkSelfIndv { get; set; }

        [JsonProperty("ckOpIndv")]
        public List<CkOpIndv> CkOpIndv { get; set; }

        [JsonProperty("maxRate")]
        public int MaxRate { get; set; }
    }

    public class Sval
    {
        [JsonProperty("Rate")]
        public int Rate { get; set; }

        [JsonProperty("Turn")]
        public int Turn { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }

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

    public class Function
    {
        [JsonProperty("funcId")]
        public int FuncId { get; set; }

        [JsonProperty("funcType")]
        public string FuncType { get; set; }

        [JsonProperty("funcTargetType")]
        public string FuncTargetType { get; set; }

        [JsonProperty("funcTargetTeam")]
        public string FuncTargetTeam { get; set; }

        [JsonProperty("funcPopupText")]
        public string FuncPopupText { get; set; }

        [JsonProperty("funcPopupIcon")]
        public string FuncPopupIcon { get; set; }

        [JsonProperty("functvals")]
        public List<Functval> Functvals { get; set; }

        [JsonProperty("funcquestTvals")]
        public List<FuncquestTval> FuncquestTvals { get; set; }

        [JsonProperty("buffs")]
        public List<Buff> Buffs { get; set; }

        [JsonProperty("svals")]
        public List<Sval> Svals { get; set; }

        [JsonProperty("traitVals")]
        public List<TraitVal> TraitVals { get; set; }

        [JsonProperty("followerVals")]
        public List<FollowerVal> FollowerVals { get; set; }
    }

    public class Skill
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("num")]
        public int Num { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("strengthStatus")]
        public int StrengthStatus { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("condQuestId")]
        public int CondQuestId { get; set; }

        [JsonProperty("condQuestPhase")]
        public int CondQuestPhase { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("coolDown")]
        public List<int> CoolDown { get; set; }

        [JsonProperty("actIndividuality")]
        public List<ActIndividuality> ActIndividuality { get; set; }

        [JsonProperty("script")]
        public Script Script { get; set; }

        [JsonProperty("functions")]
        public List<Function> Functions { get; set; }
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
