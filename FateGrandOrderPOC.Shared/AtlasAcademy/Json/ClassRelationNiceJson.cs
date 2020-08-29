using Newtonsoft.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy.Json
{
    public class ClassRelationNiceJson
    {
        [JsonProperty("saber")]
        public Saber Saber { get; set; }

        [JsonProperty("archer")]
        public Archer Archer { get; set; }

        [JsonProperty("lancer")]
        public Lancer Lancer { get; set; }

        [JsonProperty("rider")]
        public Rider Rider { get; set; }

        [JsonProperty("caster")]
        public Caster Caster { get; set; }

        [JsonProperty("assassin")]
        public Assassin Assassin { get; set; }

        [JsonProperty("berserker")]
        public Berserker Berserker { get; set; }

        [JsonProperty("shielder")]
        public Shielder Shielder { get; set; }

        [JsonProperty("ruler")]
        public Ruler Ruler { get; set; }

        [JsonProperty("alterEgo")]
        public AlterEgo AlterEgo { get; set; }

        [JsonProperty("avenger")]
        public Avenger Avenger { get; set; }

        [JsonProperty("demonGodPillar")]
        public DemonGodPillar DemonGodPillar { get; set; }

        [JsonProperty("beastII")]
        public BeastII BeastII { get; set; }

        [JsonProperty("beastI")]
        public BeastI BeastI { get; set; }

        [JsonProperty("moonCancer")]
        public MoonCancer MoonCancer { get; set; }

        [JsonProperty("beastIIIR")]
        public BeastIIIR BeastIIIR { get; set; }

        [JsonProperty("foreigner")]
        public Foreigner Foreigner { get; set; }

        [JsonProperty("beastIIIL")]
        public BeastIIIL BeastIIIL { get; set; }

        [JsonProperty("beastUnknown")]
        public BeastUnknown BeastUnknown { get; set; }
    }

    public class Saber
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Archer
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Lancer
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Rider
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Caster
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Assassin
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Berserker
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Shielder
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Ruler
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class AlterEgo
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Avenger
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class DemonGodPillar
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class BeastII
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class BeastI
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class MoonCancer
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class BeastIIIR
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class Foreigner
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class BeastIIIL
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }

    public class BeastUnknown
    {
        [JsonProperty("saber")]
        public int SaberMultiplier { get; set; }

        [JsonProperty("archer")]
        public int ArcherMultiplier { get; set; }

        [JsonProperty("lancer")]
        public int LancerMultiplier { get; set; }

        [JsonProperty("rider")]
        public int RiderMultiplier { get; set; }

        [JsonProperty("caster")]
        public int CasterMultiplier { get; set; }

        [JsonProperty("assassin")]
        public int AssassinMultiplier { get; set; }

        [JsonProperty("berserker")]
        public int BerserkerMultiplier { get; set; }

        [JsonProperty("shielder")]
        public int ShielderMultiplier { get; set; }

        [JsonProperty("ruler")]
        public int RulerMultiplier { get; set; }

        [JsonProperty("alterEgo")]
        public int AlterEgoMultiplier { get; set; }

        [JsonProperty("avenger")]
        public int AvengerMultiplier { get; set; }

        [JsonProperty("demonGodPillar")]
        public int DemonGodPillarMultiplier { get; set; }

        [JsonProperty("beastII")]
        public int BeastIIMultiplier { get; set; }

        [JsonProperty("beastI")]
        public int BeastIMultiplier { get; set; }

        [JsonProperty("moonCancer")]
        public int MoonCancerMultiplier { get; set; }

        [JsonProperty("beastIIIR")]
        public int BeastIIIRMultiplier { get; set; }

        [JsonProperty("foreigner")]
        public int ForeignerMultiplier { get; set; }

        [JsonProperty("beastIIIL")]
        public int? BeastIIILMultiplier { get; set; } = null;

        [JsonProperty("beastUnknown")]
        public int? BeastUnknownMultiplier { get; set; } = null;
    }
}
