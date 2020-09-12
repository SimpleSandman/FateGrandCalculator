using System;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Enums;

namespace FateGrandOrderPOC.Shared.AtlasAcademy.Calculations
{
    public class ClassRelation : IBaseRelation
    {
        private const float CLASS_DENOMINATOR = 1000.0f;

        public float GetAttackMultiplier(string attack)
        {
            throw new NotImplementedException();
        }

        public float GetAttackMultiplier(string atkClassName, string defClassName)
        {
            float[,] damageMultiplier = GetListDamageMultiplier();

            bool validAtkClass = Enum.TryParse(atkClassName, true, out ClassRelationEnum atkClass);
            bool validDefClass = Enum.TryParse(defClassName, true, out ClassRelationEnum defClass);

            if (!validAtkClass || !validDefClass)
            {
                return 0.0f; // invalid attribute found
            }

            return damageMultiplier[(int)atkClass, (int)defClass];
        }

        /// <summary>
        /// Get Atlas Academy's class relations to manage class damage multipliers
        /// </summary>
        /// <returns></returns>
        private static float[,] GetListDamageMultiplier()
        {
            ClassRelationNiceJson classRelations = ApiRequest.GetDeserializeObjectAsync<ClassRelationNiceJson>("https://api.atlasacademy.io/export/NA/NiceClassRelation.json").Result;

            return new float[,]
            {
                {
                    classRelations.Saber.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Saber.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Saber.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Saber.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Saber.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Saber.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Saber.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Saber.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Saber.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Saber.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Saber.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Saber.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Saber.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Saber.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Saber.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Saber.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Saber?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Saber?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Archer.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Archer.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Archer.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Archer.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Archer.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Archer.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Archer.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Archer.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Archer.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Archer.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Archer.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Archer.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Archer.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Archer.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Archer.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Archer.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Archer?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Archer?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Lancer.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Lancer.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Lancer.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Lancer.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Lancer.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Lancer.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Lancer.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Lancer?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Lancer?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Rider.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Rider.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Rider.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Rider.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Rider.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Rider.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Rider.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Rider.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Rider.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Rider.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Rider.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Rider.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Rider.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Rider.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Rider.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Rider.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Rider?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Rider?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Caster.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Caster.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Caster.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Caster.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Caster.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Caster.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Caster.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Caster.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Caster.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Caster.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Caster.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Caster.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Caster.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Caster.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Caster.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Caster.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Caster?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Caster?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Assassin.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Assassin.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Assassin.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Assassin.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Assassin.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Assassin.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Assassin.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Assassin?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Assassin?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Berserker.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Berserker.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Berserker.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Berserker.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Berserker.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Berserker.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Berserker.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Berserker?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Berserker?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Shielder.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Shielder.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Shielder.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Shielder.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Shielder.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Shielder.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Shielder.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Shielder?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Shielder?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Ruler.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Ruler.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Ruler.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Ruler.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Ruler.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Ruler.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Ruler.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Ruler?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Ruler?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Avenger.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Avenger.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Avenger.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Avenger.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Avenger.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Avenger.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Avenger.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Avenger?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Avenger?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.DemonGodPillar.SaberMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.DemonGodPillar.RiderMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.CasterMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.DemonGodPillar.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.DemonGodPillar.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.DemonGodPillar.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.DemonGodPillar.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.DemonGodPillar?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.DemonGodPillar?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.BeastII.SaberMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastII.RiderMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.CasterMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastII.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastII.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastII.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.BeastII.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastII.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.BeastII?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.BeastII?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.BeastI.SaberMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastI.RiderMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.CasterMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastI.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastI.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastI.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.BeastI.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastI.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.BeastI?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.BeastI?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.MoonCancer.SaberMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.MoonCancer.RiderMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.CasterMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.MoonCancer.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.MoonCancer.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.MoonCancer.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.MoonCancer.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.MoonCancer?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.MoonCancer?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.BeastIIIR.SaberMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastIIIR.RiderMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.CasterMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastIIIR.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastIIIR.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastIIIR.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.BeastIIIR.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.BeastIIIR?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIR?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations.Foreigner.SaberMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.ArcherMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.LancerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Foreigner.RiderMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.CasterMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.AssassinMultiplier / CLASS_DENOMINATOR,
                    classRelations.Foreigner.BerserkerMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.ShielderMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.RulerMultiplier / CLASS_DENOMINATOR,
                    classRelations.Foreigner.AvengerMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.DemonGodPillarMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.BeastIIMultiplier / CLASS_DENOMINATOR,
                    classRelations.Foreigner.BeastIMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.MoonCancerMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    classRelations.Foreigner.ForeignerMultiplier / CLASS_DENOMINATOR, classRelations.Foreigner?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations.Foreigner?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations?.BeastIIIL?.SaberMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.ArcherMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.LancerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastIIIL?.RiderMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.CasterMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.AssassinMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastIIIL?.BerserkerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.ShielderMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.RulerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastIIIL?.AvengerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.DemonGodPillarMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.BeastIIMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastIIIL?.BeastIMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.MoonCancerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.BeastIIIRMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastIIIL?.ForeignerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastIIIL?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    classRelations?.BeastUnknown?.SaberMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.ArcherMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.LancerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastUnknown?.RiderMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.CasterMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.AssassinMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastUnknown?.BerserkerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.ShielderMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.RulerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastUnknown?.AvengerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.DemonGodPillarMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.BeastIIMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastUnknown?.BeastIMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.MoonCancerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.BeastIIIRMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    classRelations?.BeastUnknown?.ForeignerMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, classRelations?.BeastUnknown?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                }
            };
        }
    }
}
