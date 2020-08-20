using System.Collections.Generic;

using Newtonsoft.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademyJson
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

        [JsonProperty("bondEquip")]
        public int BondEquip { get; set; }

        [JsonProperty("ascensionMaterials")]
        public AscensionMaterials AscensionMaterials { get; set; }

        [JsonProperty("skillMaterials")]
        public SkillMaterials SkillMaterials { get; set; }

        [JsonProperty("skills")]
        public List<SkillServant> Skills { get; set; }

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

        [JsonProperty("5")]
        public ItemMaterials GrailAsc { get; set; }
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
        public ItemMaterials EigthSkill { get; set; }

        [JsonProperty("9")]
        public ItemMaterials NinthSkill { get; set; }
    }

    public class ItemMaterials
    {
        [JsonProperty("items")]
        public List<ItemParent> Items { get; set; }

        [JsonProperty("qp")]
        public int Qp { get; set; }
    }

    public class ItemParent
    {
        [JsonProperty("item")]
        public ItemChild ItemObject { get; set; }

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

    public class ScriptServant { }

    public class ValServant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TvalServant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class CkSelfIndvServant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class BuffServant
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
        public List<ValServant> Vals { get; set; }

        [JsonProperty("tvals")]
        public List<TvalServant> Tvals { get; set; }

        [JsonProperty("ckSelfIndv")]
        public List<CkSelfIndvServant> CkSelfIndv { get; set; }

        [JsonProperty("ckOpIndv")]
        public List<object> CkOpIndv { get; set; }

        [JsonProperty("maxRate")]
        public int MaxRate { get; set; }
    }

    public class SvalServant
    {
        [JsonProperty("Rate")]
        public int Rate { get; set; }

        [JsonProperty("Turn")]
        public int Turn { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }
    }

    public class FunctionServant
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
        public List<object> FuncTvals { get; set; }

        [JsonProperty("funcquestTvals")]
        public List<object> FuncQuestTvals { get; set; }

        [JsonProperty("buffs")]
        public List<BuffServant> Buffs { get; set; }

        [JsonProperty("svals")]
        public List<SvalServant> Svals { get; set; }
    }

    public class SkillServant
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
        public List<int> Cooldown { get; set; }

        [JsonProperty("actIndividuality")]
        public List<object> ActIndividuality { get; set; }

        [JsonProperty("script")]
        public ScriptServant Script { get; set; }

        [JsonProperty("functions")]
        public List<FunctionServant> Functions { get; set; }
    }

    public class CkOpIndvServant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
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
        public ScriptServant Script { get; set; }

        [JsonProperty("functions")]
        public List<FunctionServant> Functions { get; set; }
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
        public List<FunctionServant> Functions { get; set; }
    }
}
