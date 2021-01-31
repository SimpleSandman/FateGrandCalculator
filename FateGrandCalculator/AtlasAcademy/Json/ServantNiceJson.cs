using System.Collections.Generic;

using Newtonsoft.Json;

namespace FateGrandCalculator.AtlasAcademy.Json
{
    public class ServantNiceJson
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

        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rarity")]
        public int Rarity { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        /// <summary>
        /// Max level before grail
        /// </summary>
        [JsonProperty("lvMax")]
        public int LvMax { get; set; }

        [JsonProperty("extraAssets")]
        public ExtraAssetsServant ExtraAssets { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Human (man), Earth, Sky, Star, or Beast
        /// </summary>
        [JsonProperty("attribute")]
        public string Attribute { get; set; }

        [JsonProperty("traits")]
        public List<Trait> Traits { get; set; }

        [JsonProperty("starAbsorb")]
        public int StarAbsorb { get; set; }

        [JsonProperty("starGen")]
        public int StarGen { get; set; }

        [JsonProperty("instantDeathChance")]
        public int InstantDeathChance { get; set; }

        /// <summary>
        /// Card deck for a servant (Arts, Buster, and Quick)
        /// </summary>
        [JsonProperty("cards")]
        public List<string> Cards { get; set; }

        [JsonProperty("hitsDistribution")]
        public HitsDistribution HitsDistribution { get; set; }

        /// <summary>
        /// Level 1 attack
        /// </summary>
        [JsonProperty("atkBase")]
        public int AtkBase { get; set; }

        /// <summary>
        /// Attack max before grail
        /// </summary>
        [JsonProperty("atkMax")]
        public int AtkMax { get; set; }

        /// <summary>
        /// Level 1 health
        /// </summary>
        [JsonProperty("hpBase")]
        public int HpBase { get; set; }

        /// <summary>
        /// Health max before grail
        /// </summary>
        [JsonProperty("hpMax")]
        public int HpMax { get; set; }

        [JsonProperty("growthCurve")]
        public int GrowthCurve { get; set; }

        /// <summary>
        /// Attack numbers from level 0-99 (base 0)
        /// </summary>
        [JsonProperty("atkGrowth")]
        public List<int> AtkGrowth { get; set; }

        /// <summary>
        /// Health numbers from level 0-99 (base 0)
        /// </summary>
        [JsonProperty("hpGrowth")]
        public List<int> HpGrowth { get; set; }

        [JsonProperty("bondGrowth")]
        public List<int> BondGrowth { get; set; }

        [JsonProperty("expGrowth")]
        public List<int> ExpGrowth { get; set; }

        [JsonProperty("bondEquip")]
        public int BondEquip { get; set; }

        [JsonProperty("ascensionMaterials")]
        public AscensionMaterials AscensionMaterials { get; set; }

        [JsonProperty("skillMaterials")]
        public SkillMaterials SkillMaterials { get; set; }

        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; }

        [JsonProperty("classPassive")]
        public List<ClassPassive> ClassPassive { get; set; }

        [JsonProperty("noblePhantasms")]
        public List<NoblePhantasm> NoblePhantasms { get; set; }
    }

    public class ExtraAssetsServant
    {
        [JsonProperty("charaGraph")]
        public CharaGraphServant CharaGraph { get; set; }

        [JsonProperty("faces")]
        public FacesServant Faces { get; set; }

        [JsonProperty("narrowFigure")]
        public NarrowFigureServant NarrowFigure { get; set; }

        [JsonProperty("charaFigure")]
        public CharaFigureServant CharaFigure { get; set; }

        [JsonProperty("commands")]
        public CommandsServant Commands { get; set; }

        [JsonProperty("status")]
        public ServantStatus Status { get; set; }

        [JsonProperty("equipFace")]
        public EquipFaceServant EquipFace { get; set; }
    }

    public class Trait
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class HitsDistribution
    {
        [JsonProperty("arts")]
        public List<int> Arts { get; set; }

        [JsonProperty("buster")]
        public List<int> Buster { get; set; }

        [JsonProperty("quick")]
        public List<int> Quick { get; set; }

        [JsonProperty("extra")]
        public List<int> Extra { get; set; }
    }

    public class AscensionMaterials
    {
        [JsonProperty("1")]
        public ItemMaterials FirstAsc { get; set; }

        [JsonProperty("2")]
        public ItemMaterials SecondAsc { get; set; }

        [JsonProperty("3")]
        public ItemMaterials ThirdAsc { get; set; }

        [JsonProperty("4")]
        public ItemMaterials FourthAsc { get; set; }
    }

    public class SkillMaterials
    {
        [JsonProperty("1")]
        public ItemMaterials FirstSkill { get; set; }

        [JsonProperty("2")]
        public ItemMaterials SecondSkill { get; set; }

        [JsonProperty("3")]
        public ItemMaterials ThirdSkill { get; set; }

        [JsonProperty("4")]
        public ItemMaterials FourthSkill { get; set; }

        [JsonProperty("5")]
        public ItemMaterials FifthSkill { get; set; }

        [JsonProperty("6")]
        public ItemMaterials SixthSkill { get; set; }

        [JsonProperty("7")]
        public ItemMaterials SeventhSkill { get; set; }

        [JsonProperty("8")]
        public ItemMaterials EighthSkill { get; set; }

        [JsonProperty("9")]
        public ItemMaterials NinthSkill { get; set; }
    }

    public class ItemMaterials
    {
        [JsonProperty("items")]
        public List<ItemParent> Items { get; set; }

        /// <summary>
        /// Amount of QP needed to level up an ascension or skill
        /// </summary>
        [JsonProperty("qp")]
        public int Qp { get; set; }
    }

    public class ItemParent
    {
        /// <summary>
        /// Item details
        /// </summary>
        [JsonProperty("item")]
        public ItemChild ItemObject { get; set; }

        /// <summary>
        /// Item quantity
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }

    public class ItemChild
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("background")]
        public string Background { get; set; }
    }

    public class Ascension
    {
        [JsonProperty("1")]
        public string AscensionImage1 { get; set; }

        [JsonProperty("2")]
        public string AscensionImage2 { get; set; }

        [JsonProperty("3")]
        public string AscensionImage3 { get; set; }

        [JsonProperty("4")]
        public string AscensionImage4 { get; set; }
    }

    public class CharaGraphServant
    {
        [JsonProperty("ascension")]
        public Ascension Ascension { get; set; }
    }

    public class FacesServant
    {
        [JsonProperty("ascension")]
        public Ascension Ascension { get; set; }
    }

    public class NarrowFigureServant
    {
        [JsonProperty("ascension")]
        public Ascension Ascension { get; set; }
    }

    public class CharaFigureServant
    {
        [JsonProperty("ascension")]
        public Ascension Ascension { get; set; }
    }

    public class CommandsServant
    {
        [JsonProperty("ascension")]
        public Ascension Ascension { get; set; }
    }

    public class ServantStatus
    {
        [JsonProperty("ascension")]
        public Ascension Ascension { get; set; }
    }

    public class EquipFaceServant { }

    public class Script { }

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

        [JsonProperty("script")]
        public Script Script { get; set; }
    }    

    public partial class Function
    {
        [JsonProperty("funcId")]
        public int FuncId { get; set; }

        [JsonProperty("funcType")]
        public string FuncType { get; set; }

        /// <summary>
        /// Targeted party or enemy member from the (player or enemy-controlled) servant's perspective
        /// (self, ptOne, ptAll, enemy, enemyAll, etc.)
        /// </summary>
        [JsonProperty("funcTargetType")]
        public string FuncTargetType { get; set; }

        /// <summary>
        /// Player-controlled or enemy-controlled servant or both
        /// (player, enemy, playerAndEnemy)
        /// </summary>
        [JsonProperty("funcTargetTeam")]
        public string FuncTargetTeam { get; set; }

        [JsonProperty("funcPopupText")]
        public string FuncPopupText { get; set; }

        [JsonProperty("funcPopupIcon")]
        public string FuncPopupIcon { get; set; }

        [JsonProperty("functvals")]
        public List<FuncTval> FuncTvals { get; set; }

        [JsonProperty("funcquestTvals")]
        public List<FuncQuestTval> FuncQuestTvals { get; set; }

        [JsonProperty("buffs")]
        public List<Buff> Buffs { get; set; }

        [JsonProperty("svals")]
        public List<Sval> Svals { get; set; }

        [JsonProperty("svals2")]
        public List<Sval> Svals2 { get; set; }

        [JsonProperty("svals3")]
        public List<Sval> Svals3 { get; set; }

        [JsonProperty("svals4")]
        public List<Sval> Svals4 { get; set; }

        [JsonProperty("svals5")]
        public List<Sval> Svals5 { get; set; }

        [JsonProperty("traitVals")]
        public List<TraitVal> TraitVals { get; set; }

        [JsonProperty("followerVals")]
        public List<FollowerVal> FollowerVals { get; set; }
    }

    public class Skill
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Skill position number (one-based)
        /// </summary>
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
        public List<int> Cooldown { get; set; }

        [JsonProperty("actIndividuality")]
        public List<ActIndividuality> ActIndividuality { get; set; }

        [JsonProperty("script")]
        public Script Script { get; set; }

        [JsonProperty("functions")]
        public List<Function> Functions { get; set; }
    }

    public class ClassPassive
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("coolDown")]
        public List<int> Cooldown { get; set; }

        [JsonProperty("actIndividuality")]
        public List<object> ActIndividuality { get; set; }

        [JsonProperty("script")]
        public Script Script { get; set; }

        [JsonProperty("functions")]
        public List<Function> Functions { get; set; }
    }

    public class NpGain
    {
        [JsonProperty("buster")]
        public List<int> Buster { get; set; }

        [JsonProperty("arts")]
        public List<int> Arts { get; set; }

        [JsonProperty("quick")]
        public List<int> Quick { get; set; }

        [JsonProperty("extra")]
        public List<int> Extra { get; set; }

        [JsonProperty("defence")]
        public List<int> Defence { get; set; }

        [JsonProperty("np")]
        public List<int> Np { get; set; }
    }

    public class Individuality
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class NoblePhantasm
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("num")]
        public int Num { get; set; }

        [JsonProperty("card")]
        public string Card { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("npGain")]
        public NpGain NpGain { get; set; }

        [JsonProperty("npDistribution")]
        public List<int> NpDistribution { get; set; }

        [JsonProperty("strengthStatus")]
        public int StrengthStatus { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("condQuestId")]
        public int CondQuestId { get; set; }

        [JsonProperty("condQuestPhase")]
        public int CondQuestPhase { get; set; }

        [JsonProperty("individuality")]
        public List<Individuality> Individuality { get; set; }

        [JsonProperty("functions")]
        public List<Function> Functions { get; set; }
    }

    public class FuncTval
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
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

        [JsonProperty("Correction")]
        public int Correction { get; set; }

        /// <summary>
        /// Target monster by trait enum ID
        /// </summary>
        [JsonProperty("Target")]
        public int Target { get; set; }

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
}
