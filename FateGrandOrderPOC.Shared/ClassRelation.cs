using System;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;
using FateGrandOrderPOC.Shared.Extensions;

namespace FateGrandOrderPOC.Shared
{
    public enum ClassRelationEnum
    {
        Saber = 0,
        Archer = 1,
        Lancer = 2,
        Rider = 3,
        Caster = 4,
        Assassin = 5,
        Berserker = 6,
        Shielder = 7,
        Ruler = 8,
        AlterEgo = 9,
        DemonGodPillar = 10,
        BeastII = 11,
        BeastI = 12,
        MoonCancer = 13,
        BeastIIIR = 14,
        Foreigner = 15,
        BeastIIIL = 16,
        BeastUnknown = 17
    }

    public static class ClassRelation
    {
        public static decimal GetAttackMultiplier(string atkClassName, string defClassName)
        {
            decimal[,] damageMultiplier = GetListDamageMultiplier();

            bool validAtkClass = Enum.TryParse(atkClassName.ToUpperFirstChar(), out ClassRelationEnum atkClass);
            bool validDefClass = Enum.TryParse(defClassName.ToUpperFirstChar(), out ClassRelationEnum defClass);

            if (!validAtkClass || !validDefClass)
            {
                return 0.0m; // invalid attribute found
            }

            return damageMultiplier[(int)atkClass, (int)defClass];
        }

        /// <summary>
        /// Get Atlas Academy's class relations to manage class damage multipliers
        /// </summary>
        /// <returns></returns>
        private static decimal[,] GetListDamageMultiplier()
        {
            ClassRelationNiceJson classRelations = ApiRequest.GetDesearlizeObjectAsync<ClassRelationNiceJson>("https://api.atlasacademy.io/export/NA/NiceClassRelation.json").Result;

            return new decimal[,]
            {
                {
                    classRelations.Saber.SaberMultiplier/1000m, classRelations.Saber.ArcherMultiplier/1000m, classRelations.Saber.LancerMultiplier/1000m,
                    classRelations.Saber.RiderMultiplier/1000m, classRelations.Saber.CasterMultiplier/1000m, classRelations.Saber.AssassinMultiplier/1000m,
                    classRelations.Saber.BerserkerMultiplier/1000m, classRelations.Saber.ShielderMultiplier/1000m, classRelations.Saber.RulerMultiplier/1000m,
                    classRelations.Saber.AvengerMultiplier/1000m, classRelations.Saber.DemonGodPillarMultiplier/1000m, classRelations.Saber.BeastIIMultiplier/1000m,
                    classRelations.Saber.BeastIMultiplier/1000m, classRelations.Saber.MoonCancerMultiplier/1000m, classRelations.Saber.BeastIIIRMultiplier/1000m,
                    classRelations.Saber.ForeignerMultiplier/1000m, classRelations.Saber?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Saber?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Archer.SaberMultiplier/1000m, classRelations.Archer.ArcherMultiplier/1000m, classRelations.Archer.LancerMultiplier/1000m,
                    classRelations.Archer.RiderMultiplier/1000m, classRelations.Archer.CasterMultiplier/1000m, classRelations.Archer.AssassinMultiplier/1000m,
                    classRelations.Archer.BerserkerMultiplier/1000m, classRelations.Archer.ShielderMultiplier/1000m, classRelations.Archer.RulerMultiplier/1000m,
                    classRelations.Archer.AvengerMultiplier/1000m, classRelations.Archer.DemonGodPillarMultiplier/1000m, classRelations.Archer.BeastIIMultiplier/1000m,
                    classRelations.Archer.BeastIMultiplier/1000m, classRelations.Archer.MoonCancerMultiplier/1000m, classRelations.Archer.BeastIIIRMultiplier/1000m,
                    classRelations.Archer.ForeignerMultiplier/1000m, classRelations.Archer?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Archer?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Lancer.SaberMultiplier/1000m, classRelations.Lancer.ArcherMultiplier/1000m, classRelations.Lancer.LancerMultiplier/1000m,
                    classRelations.Lancer.RiderMultiplier/1000m, classRelations.Lancer.CasterMultiplier/1000m, classRelations.Lancer.AssassinMultiplier/1000m,
                    classRelations.Lancer.BerserkerMultiplier/1000m, classRelations.Lancer.ShielderMultiplier/1000m, classRelations.Lancer.RulerMultiplier/1000m,
                    classRelations.Lancer.AvengerMultiplier/1000m, classRelations.Lancer.DemonGodPillarMultiplier/1000m, classRelations.Lancer.BeastIIMultiplier/1000m,
                    classRelations.Lancer.BeastIMultiplier/1000m, classRelations.Lancer.MoonCancerMultiplier/1000m, classRelations.Lancer.BeastIIIRMultiplier/1000m,
                    classRelations.Lancer.ForeignerMultiplier/1000m, classRelations.Lancer?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Lancer?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Rider.SaberMultiplier/1000m, classRelations.Rider.ArcherMultiplier/1000m, classRelations.Rider.LancerMultiplier/1000m,
                    classRelations.Rider.RiderMultiplier/1000m, classRelations.Rider.CasterMultiplier/1000m, classRelations.Rider.AssassinMultiplier/1000m,
                    classRelations.Rider.BerserkerMultiplier/1000m, classRelations.Rider.ShielderMultiplier/1000m, classRelations.Rider.RulerMultiplier/1000m,
                    classRelations.Rider.AvengerMultiplier/1000m, classRelations.Rider.DemonGodPillarMultiplier/1000m, classRelations.Rider.BeastIIMultiplier/1000m,
                    classRelations.Rider.BeastIMultiplier/1000m, classRelations.Rider.MoonCancerMultiplier/1000m, classRelations.Rider.BeastIIIRMultiplier/1000m,
                    classRelations.Rider.ForeignerMultiplier/1000m, classRelations.Rider?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Rider?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Caster.SaberMultiplier/1000m, classRelations.Caster.ArcherMultiplier/1000m, classRelations.Caster.LancerMultiplier/1000m,
                    classRelations.Caster.RiderMultiplier/1000m, classRelations.Caster.CasterMultiplier/1000m, classRelations.Caster.AssassinMultiplier/1000m,
                    classRelations.Caster.BerserkerMultiplier/1000m, classRelations.Caster.ShielderMultiplier/1000m, classRelations.Caster.RulerMultiplier/1000m,
                    classRelations.Caster.AvengerMultiplier/1000m, classRelations.Caster.DemonGodPillarMultiplier/1000m, classRelations.Caster.BeastIIMultiplier/1000m,
                    classRelations.Caster.BeastIMultiplier/1000m, classRelations.Caster.MoonCancerMultiplier/1000m, classRelations.Caster.BeastIIIRMultiplier/1000m,
                    classRelations.Caster.ForeignerMultiplier/1000m, classRelations.Caster?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Caster?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Assassin.SaberMultiplier/1000m, classRelations.Assassin.ArcherMultiplier/1000m, classRelations.Assassin.LancerMultiplier/1000m,
                    classRelations.Assassin.RiderMultiplier/1000m, classRelations.Assassin.CasterMultiplier/1000m, classRelations.Assassin.AssassinMultiplier/1000m,
                    classRelations.Assassin.BerserkerMultiplier/1000m, classRelations.Assassin.ShielderMultiplier/1000m, classRelations.Assassin.RulerMultiplier/1000m,
                    classRelations.Assassin.AvengerMultiplier/1000m, classRelations.Assassin.DemonGodPillarMultiplier/1000m, classRelations.Assassin.BeastIIMultiplier/1000m,
                    classRelations.Assassin.BeastIMultiplier/1000m, classRelations.Assassin.MoonCancerMultiplier/1000m, classRelations.Assassin.BeastIIIRMultiplier/1000m,
                    classRelations.Assassin.ForeignerMultiplier/1000m, classRelations.Assassin?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Assassin?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Berserker.SaberMultiplier/1000m, classRelations.Berserker.ArcherMultiplier/1000m, classRelations.Berserker.LancerMultiplier/1000m,
                    classRelations.Berserker.RiderMultiplier/1000m, classRelations.Berserker.CasterMultiplier/1000m, classRelations.Berserker.AssassinMultiplier/1000m,
                    classRelations.Berserker.BerserkerMultiplier/1000m, classRelations.Berserker.ShielderMultiplier/1000m, classRelations.Berserker.RulerMultiplier/1000m,
                    classRelations.Berserker.AvengerMultiplier/1000m, classRelations.Berserker.DemonGodPillarMultiplier/1000m, classRelations.Berserker.BeastIIMultiplier/1000m,
                    classRelations.Berserker.BeastIMultiplier/1000m, classRelations.Berserker.MoonCancerMultiplier/1000m, classRelations.Berserker.BeastIIIRMultiplier/1000m,
                    classRelations.Berserker.ForeignerMultiplier/1000m, classRelations.Berserker?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Berserker?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Shielder.SaberMultiplier/1000m, classRelations.Shielder.ArcherMultiplier/1000m, classRelations.Shielder.LancerMultiplier/1000m,
                    classRelations.Shielder.RiderMultiplier/1000m, classRelations.Shielder.CasterMultiplier/1000m, classRelations.Shielder.AssassinMultiplier/1000m,
                    classRelations.Shielder.BerserkerMultiplier/1000m, classRelations.Shielder.ShielderMultiplier/1000m, classRelations.Shielder.RulerMultiplier/1000m,
                    classRelations.Shielder.AvengerMultiplier/1000m, classRelations.Shielder.DemonGodPillarMultiplier/1000m, classRelations.Shielder.BeastIIMultiplier/1000m,
                    classRelations.Shielder.BeastIMultiplier/1000m, classRelations.Shielder.MoonCancerMultiplier/1000m, classRelations.Shielder.BeastIIIRMultiplier/1000m,
                    classRelations.Shielder.ForeignerMultiplier/1000m, classRelations.Shielder?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Shielder?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Ruler.SaberMultiplier/1000m, classRelations.Ruler.ArcherMultiplier/1000m, classRelations.Ruler.LancerMultiplier/1000m,
                    classRelations.Ruler.RiderMultiplier/1000m, classRelations.Ruler.CasterMultiplier/1000m, classRelations.Ruler.AssassinMultiplier/1000m,
                    classRelations.Ruler.BerserkerMultiplier/1000m, classRelations.Ruler.ShielderMultiplier/1000m, classRelations.Ruler.RulerMultiplier/1000m,
                    classRelations.Ruler.AvengerMultiplier/1000m, classRelations.Ruler.DemonGodPillarMultiplier/1000m, classRelations.Ruler.BeastIIMultiplier/1000m,
                    classRelations.Ruler.BeastIMultiplier/1000m, classRelations.Ruler.MoonCancerMultiplier/1000m, classRelations.Ruler.BeastIIIRMultiplier/1000m,
                    classRelations.Ruler.ForeignerMultiplier/1000m, classRelations.Ruler?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Ruler?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Avenger.SaberMultiplier/1000m, classRelations.Avenger.ArcherMultiplier/1000m, classRelations.Avenger.LancerMultiplier/1000m,
                    classRelations.Avenger.RiderMultiplier/1000m, classRelations.Avenger.CasterMultiplier/1000m, classRelations.Avenger.AssassinMultiplier/1000m,
                    classRelations.Avenger.BerserkerMultiplier/1000m, classRelations.Avenger.ShielderMultiplier/1000m, classRelations.Avenger.RulerMultiplier/1000m,
                    classRelations.Avenger.AvengerMultiplier/1000m, classRelations.Avenger.DemonGodPillarMultiplier/1000m, classRelations.Avenger.BeastIIMultiplier/1000m,
                    classRelations.Avenger.BeastIMultiplier/1000m, classRelations.Avenger.MoonCancerMultiplier/1000m, classRelations.Avenger.BeastIIIRMultiplier/1000m,
                    classRelations.Avenger.ForeignerMultiplier/1000m, classRelations.Avenger?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Avenger?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.DemonGodPillar.SaberMultiplier/1000m, classRelations.DemonGodPillar.ArcherMultiplier/1000m, classRelations.DemonGodPillar.LancerMultiplier/1000m,
                    classRelations.DemonGodPillar.RiderMultiplier/1000m, classRelations.DemonGodPillar.CasterMultiplier/1000m, classRelations.DemonGodPillar.AssassinMultiplier/1000m,
                    classRelations.DemonGodPillar.BerserkerMultiplier/1000m, classRelations.DemonGodPillar.ShielderMultiplier/1000m, classRelations.DemonGodPillar.RulerMultiplier/1000m,
                    classRelations.DemonGodPillar.AvengerMultiplier/1000m, classRelations.DemonGodPillar.DemonGodPillarMultiplier/1000m, classRelations.DemonGodPillar.BeastIIMultiplier/1000m,
                    classRelations.DemonGodPillar.BeastIMultiplier/1000m, classRelations.DemonGodPillar.MoonCancerMultiplier/1000m, classRelations.DemonGodPillar.BeastIIIRMultiplier/1000m,
                    classRelations.DemonGodPillar.ForeignerMultiplier/1000m, classRelations.DemonGodPillar?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.DemonGodPillar?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.BeastII.SaberMultiplier/1000m, classRelations.BeastII.ArcherMultiplier/1000m, classRelations.BeastII.LancerMultiplier/1000m,
                    classRelations.BeastII.RiderMultiplier/1000m, classRelations.BeastII.CasterMultiplier/1000m, classRelations.BeastII.AssassinMultiplier/1000m,
                    classRelations.BeastII.BerserkerMultiplier/1000m, classRelations.BeastII.ShielderMultiplier/1000m, classRelations.BeastII.RulerMultiplier/1000m,
                    classRelations.BeastII.AvengerMultiplier/1000m, classRelations.BeastII.DemonGodPillarMultiplier/1000m, classRelations.BeastII.BeastIIMultiplier/1000m,
                    classRelations.BeastII.BeastIMultiplier/1000m, classRelations.BeastII.MoonCancerMultiplier/1000m, classRelations.BeastII.BeastIIIRMultiplier/1000m,
                    classRelations.BeastII.ForeignerMultiplier/1000m, classRelations.BeastII?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.BeastII?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.BeastI.SaberMultiplier/1000m, classRelations.BeastI.ArcherMultiplier/1000m, classRelations.BeastI.LancerMultiplier/1000m,
                    classRelations.BeastI.RiderMultiplier/1000m, classRelations.BeastI.CasterMultiplier/1000m, classRelations.BeastI.AssassinMultiplier/1000m,
                    classRelations.BeastI.BerserkerMultiplier/1000m, classRelations.BeastI.ShielderMultiplier/1000m, classRelations.BeastI.RulerMultiplier/1000m,
                    classRelations.BeastI.AvengerMultiplier/1000m, classRelations.BeastI.DemonGodPillarMultiplier/1000m, classRelations.BeastI.BeastIIMultiplier/1000m,
                    classRelations.BeastI.BeastIMultiplier/1000m, classRelations.BeastI.MoonCancerMultiplier/1000m, classRelations.BeastI.BeastIIIRMultiplier/1000m,
                    classRelations.BeastI.ForeignerMultiplier/1000m, classRelations.BeastI?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.BeastI?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.MoonCancer.SaberMultiplier/1000m, classRelations.MoonCancer.ArcherMultiplier/1000m, classRelations.MoonCancer.LancerMultiplier/1000m,
                    classRelations.MoonCancer.RiderMultiplier/1000m, classRelations.MoonCancer.CasterMultiplier/1000m, classRelations.MoonCancer.AssassinMultiplier/1000m,
                    classRelations.MoonCancer.BerserkerMultiplier/1000m, classRelations.MoonCancer.ShielderMultiplier/1000m, classRelations.MoonCancer.RulerMultiplier/1000m,
                    classRelations.MoonCancer.AvengerMultiplier/1000m, classRelations.MoonCancer.DemonGodPillarMultiplier/1000m, classRelations.MoonCancer.BeastIIMultiplier/1000m,
                    classRelations.MoonCancer.BeastIMultiplier/1000m, classRelations.MoonCancer.MoonCancerMultiplier/1000m, classRelations.MoonCancer.BeastIIIRMultiplier/1000m,
                    classRelations.MoonCancer.ForeignerMultiplier/1000m, classRelations.MoonCancer?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.MoonCancer?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.BeastIIIR.SaberMultiplier/1000m, classRelations.BeastIIIR.ArcherMultiplier/1000m, classRelations.BeastIIIR.LancerMultiplier/1000m,
                    classRelations.BeastIIIR.RiderMultiplier/1000m, classRelations.BeastIIIR.CasterMultiplier/1000m, classRelations.BeastIIIR.AssassinMultiplier/1000m,
                    classRelations.BeastIIIR.BerserkerMultiplier/1000m, classRelations.BeastIIIR.ShielderMultiplier/1000m, classRelations.BeastIIIR.RulerMultiplier/1000m,
                    classRelations.BeastIIIR.AvengerMultiplier/1000m, classRelations.BeastIIIR.DemonGodPillarMultiplier/1000m, classRelations.BeastIIIR.BeastIIMultiplier/1000m,
                    classRelations.BeastIIIR.BeastIMultiplier/1000m, classRelations.BeastIIIR.MoonCancerMultiplier/1000m, classRelations.BeastIIIR.BeastIIIRMultiplier/1000m,
                    classRelations.BeastIIIR.ForeignerMultiplier/1000m, classRelations.BeastIIIR?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIR?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations.Foreigner.SaberMultiplier/1000m, classRelations.Foreigner.ArcherMultiplier/1000m, classRelations.Foreigner.LancerMultiplier/1000m,
                    classRelations.Foreigner.RiderMultiplier/1000m, classRelations.Foreigner.CasterMultiplier/1000m, classRelations.Foreigner.AssassinMultiplier/1000m,
                    classRelations.Foreigner.BerserkerMultiplier/1000m, classRelations.Foreigner.ShielderMultiplier/1000m, classRelations.Foreigner.RulerMultiplier/1000m,
                    classRelations.Foreigner.AvengerMultiplier/1000m, classRelations.Foreigner.DemonGodPillarMultiplier/1000m, classRelations.Foreigner.BeastIIMultiplier/1000m,
                    classRelations.Foreigner.BeastIMultiplier/1000m, classRelations.Foreigner.MoonCancerMultiplier/1000m, classRelations.Foreigner.BeastIIIRMultiplier/1000m,
                    classRelations.Foreigner.ForeignerMultiplier/1000m, classRelations.Foreigner?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations.Foreigner?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations?.BeastIIIL?.SaberMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.ArcherMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.LancerMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastIIIL?.RiderMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.CasterMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.AssassinMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastIIIL?.BerserkerMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.ShielderMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.RulerMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastIIIL?.AvengerMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.DemonGodPillarMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.BeastIIMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastIIIL?.BeastIMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.MoonCancerMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.BeastIIIRMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastIIIL?.ForeignerMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations?.BeastIIIL?.BeastUnknownMultiplier/1000m ?? 0.0m
                },
                {
                    classRelations?.BeastUnknown?.SaberMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.ArcherMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.LancerMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastUnknown?.RiderMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.CasterMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.AssassinMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastUnknown?.BerserkerMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.ShielderMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.RulerMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastUnknown?.AvengerMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.DemonGodPillarMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.BeastIIMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastUnknown?.BeastIMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.MoonCancerMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.BeastIIIRMultiplier/1000m ?? 0.0m,
                    classRelations?.BeastUnknown?.ForeignerMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.BeastIIILMultiplier/1000m ?? 0.0m, classRelations?.BeastUnknown?.BeastUnknownMultiplier/1000m ?? 0.0m
                }
            };
        }
    }
}
