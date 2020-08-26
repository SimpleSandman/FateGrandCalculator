using System;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;
using FateGrandOrderPOC.Shared.Enums;

namespace FateGrandOrderPOC.Shared
{
    public class ClassRelation : BaseRelation
    {
        public override float GetAttackMultiplier(string attack)
        {
            throw new NotImplementedException();
        }

        public override float GetAttackMultiplier(string atkClassName, string defClassName)
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
            ClassRelationNiceJson classRelations = ApiRequest.GetDesearlizeObjectAsync<ClassRelationNiceJson>("https://api.atlasacademy.io/export/NA/NiceClassRelation.json").Result;

            return new float[,]
            {
                {
                    classRelations.Saber.SaberMultiplier/1000f, classRelations.Saber.ArcherMultiplier/1000f, classRelations.Saber.LancerMultiplier/1000f,
                    classRelations.Saber.RiderMultiplier/1000f, classRelations.Saber.CasterMultiplier/1000f, classRelations.Saber.AssassinMultiplier/1000f,
                    classRelations.Saber.BerserkerMultiplier/1000f, classRelations.Saber.ShielderMultiplier/1000f, classRelations.Saber.RulerMultiplier/1000f,
                    classRelations.Saber.AvengerMultiplier/1000f, classRelations.Saber.DemonGodPillarMultiplier/1000f, classRelations.Saber.BeastIIMultiplier/1000f,
                    classRelations.Saber.BeastIMultiplier/1000f, classRelations.Saber.MoonCancerMultiplier/1000f, classRelations.Saber.BeastIIIRMultiplier/1000f,
                    classRelations.Saber.ForeignerMultiplier/1000f, classRelations.Saber?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Saber?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Archer.SaberMultiplier/1000f, classRelations.Archer.ArcherMultiplier/1000f, classRelations.Archer.LancerMultiplier/1000f,
                    classRelations.Archer.RiderMultiplier/1000f, classRelations.Archer.CasterMultiplier/1000f, classRelations.Archer.AssassinMultiplier/1000f,
                    classRelations.Archer.BerserkerMultiplier/1000f, classRelations.Archer.ShielderMultiplier/1000f, classRelations.Archer.RulerMultiplier/1000f,
                    classRelations.Archer.AvengerMultiplier/1000f, classRelations.Archer.DemonGodPillarMultiplier/1000f, classRelations.Archer.BeastIIMultiplier/1000f,
                    classRelations.Archer.BeastIMultiplier/1000f, classRelations.Archer.MoonCancerMultiplier/1000f, classRelations.Archer.BeastIIIRMultiplier/1000f,
                    classRelations.Archer.ForeignerMultiplier/1000f, classRelations.Archer?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Archer?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Lancer.SaberMultiplier/1000f, classRelations.Lancer.ArcherMultiplier/1000f, classRelations.Lancer.LancerMultiplier/1000f,
                    classRelations.Lancer.RiderMultiplier/1000f, classRelations.Lancer.CasterMultiplier/1000f, classRelations.Lancer.AssassinMultiplier/1000f,
                    classRelations.Lancer.BerserkerMultiplier/1000f, classRelations.Lancer.ShielderMultiplier/1000f, classRelations.Lancer.RulerMultiplier/1000f,
                    classRelations.Lancer.AvengerMultiplier/1000f, classRelations.Lancer.DemonGodPillarMultiplier/1000f, classRelations.Lancer.BeastIIMultiplier/1000f,
                    classRelations.Lancer.BeastIMultiplier/1000f, classRelations.Lancer.MoonCancerMultiplier/1000f, classRelations.Lancer.BeastIIIRMultiplier/1000f,
                    classRelations.Lancer.ForeignerMultiplier/1000f, classRelations.Lancer?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Lancer?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Rider.SaberMultiplier/1000f, classRelations.Rider.ArcherMultiplier/1000f, classRelations.Rider.LancerMultiplier/1000f,
                    classRelations.Rider.RiderMultiplier/1000f, classRelations.Rider.CasterMultiplier/1000f, classRelations.Rider.AssassinMultiplier/1000f,
                    classRelations.Rider.BerserkerMultiplier/1000f, classRelations.Rider.ShielderMultiplier/1000f, classRelations.Rider.RulerMultiplier/1000f,
                    classRelations.Rider.AvengerMultiplier/1000f, classRelations.Rider.DemonGodPillarMultiplier/1000f, classRelations.Rider.BeastIIMultiplier/1000f,
                    classRelations.Rider.BeastIMultiplier/1000f, classRelations.Rider.MoonCancerMultiplier/1000f, classRelations.Rider.BeastIIIRMultiplier/1000f,
                    classRelations.Rider.ForeignerMultiplier/1000f, classRelations.Rider?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Rider?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Caster.SaberMultiplier/1000f, classRelations.Caster.ArcherMultiplier/1000f, classRelations.Caster.LancerMultiplier/1000f,
                    classRelations.Caster.RiderMultiplier/1000f, classRelations.Caster.CasterMultiplier/1000f, classRelations.Caster.AssassinMultiplier/1000f,
                    classRelations.Caster.BerserkerMultiplier/1000f, classRelations.Caster.ShielderMultiplier/1000f, classRelations.Caster.RulerMultiplier/1000f,
                    classRelations.Caster.AvengerMultiplier/1000f, classRelations.Caster.DemonGodPillarMultiplier/1000f, classRelations.Caster.BeastIIMultiplier/1000f,
                    classRelations.Caster.BeastIMultiplier/1000f, classRelations.Caster.MoonCancerMultiplier/1000f, classRelations.Caster.BeastIIIRMultiplier/1000f,
                    classRelations.Caster.ForeignerMultiplier/1000f, classRelations.Caster?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Caster?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Assassin.SaberMultiplier/1000f, classRelations.Assassin.ArcherMultiplier/1000f, classRelations.Assassin.LancerMultiplier/1000f,
                    classRelations.Assassin.RiderMultiplier/1000f, classRelations.Assassin.CasterMultiplier/1000f, classRelations.Assassin.AssassinMultiplier/1000f,
                    classRelations.Assassin.BerserkerMultiplier/1000f, classRelations.Assassin.ShielderMultiplier/1000f, classRelations.Assassin.RulerMultiplier/1000f,
                    classRelations.Assassin.AvengerMultiplier/1000f, classRelations.Assassin.DemonGodPillarMultiplier/1000f, classRelations.Assassin.BeastIIMultiplier/1000f,
                    classRelations.Assassin.BeastIMultiplier/1000f, classRelations.Assassin.MoonCancerMultiplier/1000f, classRelations.Assassin.BeastIIIRMultiplier/1000f,
                    classRelations.Assassin.ForeignerMultiplier/1000f, classRelations.Assassin?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Assassin?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Berserker.SaberMultiplier/1000f, classRelations.Berserker.ArcherMultiplier/1000f, classRelations.Berserker.LancerMultiplier/1000f,
                    classRelations.Berserker.RiderMultiplier/1000f, classRelations.Berserker.CasterMultiplier/1000f, classRelations.Berserker.AssassinMultiplier/1000f,
                    classRelations.Berserker.BerserkerMultiplier/1000f, classRelations.Berserker.ShielderMultiplier/1000f, classRelations.Berserker.RulerMultiplier/1000f,
                    classRelations.Berserker.AvengerMultiplier/1000f, classRelations.Berserker.DemonGodPillarMultiplier/1000f, classRelations.Berserker.BeastIIMultiplier/1000f,
                    classRelations.Berserker.BeastIMultiplier/1000f, classRelations.Berserker.MoonCancerMultiplier/1000f, classRelations.Berserker.BeastIIIRMultiplier/1000f,
                    classRelations.Berserker.ForeignerMultiplier/1000f, classRelations.Berserker?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Berserker?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Shielder.SaberMultiplier/1000f, classRelations.Shielder.ArcherMultiplier/1000f, classRelations.Shielder.LancerMultiplier/1000f,
                    classRelations.Shielder.RiderMultiplier/1000f, classRelations.Shielder.CasterMultiplier/1000f, classRelations.Shielder.AssassinMultiplier/1000f,
                    classRelations.Shielder.BerserkerMultiplier/1000f, classRelations.Shielder.ShielderMultiplier/1000f, classRelations.Shielder.RulerMultiplier/1000f,
                    classRelations.Shielder.AvengerMultiplier/1000f, classRelations.Shielder.DemonGodPillarMultiplier/1000f, classRelations.Shielder.BeastIIMultiplier/1000f,
                    classRelations.Shielder.BeastIMultiplier/1000f, classRelations.Shielder.MoonCancerMultiplier/1000f, classRelations.Shielder.BeastIIIRMultiplier/1000f,
                    classRelations.Shielder.ForeignerMultiplier/1000f, classRelations.Shielder?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Shielder?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Ruler.SaberMultiplier/1000f, classRelations.Ruler.ArcherMultiplier/1000f, classRelations.Ruler.LancerMultiplier/1000f,
                    classRelations.Ruler.RiderMultiplier/1000f, classRelations.Ruler.CasterMultiplier/1000f, classRelations.Ruler.AssassinMultiplier/1000f,
                    classRelations.Ruler.BerserkerMultiplier/1000f, classRelations.Ruler.ShielderMultiplier/1000f, classRelations.Ruler.RulerMultiplier/1000f,
                    classRelations.Ruler.AvengerMultiplier/1000f, classRelations.Ruler.DemonGodPillarMultiplier/1000f, classRelations.Ruler.BeastIIMultiplier/1000f,
                    classRelations.Ruler.BeastIMultiplier/1000f, classRelations.Ruler.MoonCancerMultiplier/1000f, classRelations.Ruler.BeastIIIRMultiplier/1000f,
                    classRelations.Ruler.ForeignerMultiplier/1000f, classRelations.Ruler?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Ruler?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Avenger.SaberMultiplier/1000f, classRelations.Avenger.ArcherMultiplier/1000f, classRelations.Avenger.LancerMultiplier/1000f,
                    classRelations.Avenger.RiderMultiplier/1000f, classRelations.Avenger.CasterMultiplier/1000f, classRelations.Avenger.AssassinMultiplier/1000f,
                    classRelations.Avenger.BerserkerMultiplier/1000f, classRelations.Avenger.ShielderMultiplier/1000f, classRelations.Avenger.RulerMultiplier/1000f,
                    classRelations.Avenger.AvengerMultiplier/1000f, classRelations.Avenger.DemonGodPillarMultiplier/1000f, classRelations.Avenger.BeastIIMultiplier/1000f,
                    classRelations.Avenger.BeastIMultiplier/1000f, classRelations.Avenger.MoonCancerMultiplier/1000f, classRelations.Avenger.BeastIIIRMultiplier/1000f,
                    classRelations.Avenger.ForeignerMultiplier/1000f, classRelations.Avenger?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Avenger?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.DemonGodPillar.SaberMultiplier/1000f, classRelations.DemonGodPillar.ArcherMultiplier/1000f, classRelations.DemonGodPillar.LancerMultiplier/1000f,
                    classRelations.DemonGodPillar.RiderMultiplier/1000f, classRelations.DemonGodPillar.CasterMultiplier/1000f, classRelations.DemonGodPillar.AssassinMultiplier/1000f,
                    classRelations.DemonGodPillar.BerserkerMultiplier/1000f, classRelations.DemonGodPillar.ShielderMultiplier/1000f, classRelations.DemonGodPillar.RulerMultiplier/1000f,
                    classRelations.DemonGodPillar.AvengerMultiplier/1000f, classRelations.DemonGodPillar.DemonGodPillarMultiplier/1000f, classRelations.DemonGodPillar.BeastIIMultiplier/1000f,
                    classRelations.DemonGodPillar.BeastIMultiplier/1000f, classRelations.DemonGodPillar.MoonCancerMultiplier/1000f, classRelations.DemonGodPillar.BeastIIIRMultiplier/1000f,
                    classRelations.DemonGodPillar.ForeignerMultiplier/1000f, classRelations.DemonGodPillar?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.DemonGodPillar?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.BeastII.SaberMultiplier/1000f, classRelations.BeastII.ArcherMultiplier/1000f, classRelations.BeastII.LancerMultiplier/1000f,
                    classRelations.BeastII.RiderMultiplier/1000f, classRelations.BeastII.CasterMultiplier/1000f, classRelations.BeastII.AssassinMultiplier/1000f,
                    classRelations.BeastII.BerserkerMultiplier/1000f, classRelations.BeastII.ShielderMultiplier/1000f, classRelations.BeastII.RulerMultiplier/1000f,
                    classRelations.BeastII.AvengerMultiplier/1000f, classRelations.BeastII.DemonGodPillarMultiplier/1000f, classRelations.BeastII.BeastIIMultiplier/1000f,
                    classRelations.BeastII.BeastIMultiplier/1000f, classRelations.BeastII.MoonCancerMultiplier/1000f, classRelations.BeastII.BeastIIIRMultiplier/1000f,
                    classRelations.BeastII.ForeignerMultiplier/1000f, classRelations.BeastII?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.BeastII?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.BeastI.SaberMultiplier/1000f, classRelations.BeastI.ArcherMultiplier/1000f, classRelations.BeastI.LancerMultiplier/1000f,
                    classRelations.BeastI.RiderMultiplier/1000f, classRelations.BeastI.CasterMultiplier/1000f, classRelations.BeastI.AssassinMultiplier/1000f,
                    classRelations.BeastI.BerserkerMultiplier/1000f, classRelations.BeastI.ShielderMultiplier/1000f, classRelations.BeastI.RulerMultiplier/1000f,
                    classRelations.BeastI.AvengerMultiplier/1000f, classRelations.BeastI.DemonGodPillarMultiplier/1000f, classRelations.BeastI.BeastIIMultiplier/1000f,
                    classRelations.BeastI.BeastIMultiplier/1000f, classRelations.BeastI.MoonCancerMultiplier/1000f, classRelations.BeastI.BeastIIIRMultiplier/1000f,
                    classRelations.BeastI.ForeignerMultiplier/1000f, classRelations.BeastI?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.BeastI?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.MoonCancer.SaberMultiplier/1000f, classRelations.MoonCancer.ArcherMultiplier/1000f, classRelations.MoonCancer.LancerMultiplier/1000f,
                    classRelations.MoonCancer.RiderMultiplier/1000f, classRelations.MoonCancer.CasterMultiplier/1000f, classRelations.MoonCancer.AssassinMultiplier/1000f,
                    classRelations.MoonCancer.BerserkerMultiplier/1000f, classRelations.MoonCancer.ShielderMultiplier/1000f, classRelations.MoonCancer.RulerMultiplier/1000f,
                    classRelations.MoonCancer.AvengerMultiplier/1000f, classRelations.MoonCancer.DemonGodPillarMultiplier/1000f, classRelations.MoonCancer.BeastIIMultiplier/1000f,
                    classRelations.MoonCancer.BeastIMultiplier/1000f, classRelations.MoonCancer.MoonCancerMultiplier/1000f, classRelations.MoonCancer.BeastIIIRMultiplier/1000f,
                    classRelations.MoonCancer.ForeignerMultiplier/1000f, classRelations.MoonCancer?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.MoonCancer?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.BeastIIIR.SaberMultiplier/1000f, classRelations.BeastIIIR.ArcherMultiplier/1000f, classRelations.BeastIIIR.LancerMultiplier/1000f,
                    classRelations.BeastIIIR.RiderMultiplier/1000f, classRelations.BeastIIIR.CasterMultiplier/1000f, classRelations.BeastIIIR.AssassinMultiplier/1000f,
                    classRelations.BeastIIIR.BerserkerMultiplier/1000f, classRelations.BeastIIIR.ShielderMultiplier/1000f, classRelations.BeastIIIR.RulerMultiplier/1000f,
                    classRelations.BeastIIIR.AvengerMultiplier/1000f, classRelations.BeastIIIR.DemonGodPillarMultiplier/1000f, classRelations.BeastIIIR.BeastIIMultiplier/1000f,
                    classRelations.BeastIIIR.BeastIMultiplier/1000f, classRelations.BeastIIIR.MoonCancerMultiplier/1000f, classRelations.BeastIIIR.BeastIIIRMultiplier/1000f,
                    classRelations.BeastIIIR.ForeignerMultiplier/1000f, classRelations.BeastIIIR?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIR?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations.Foreigner.SaberMultiplier/1000f, classRelations.Foreigner.ArcherMultiplier/1000f, classRelations.Foreigner.LancerMultiplier/1000f,
                    classRelations.Foreigner.RiderMultiplier/1000f, classRelations.Foreigner.CasterMultiplier/1000f, classRelations.Foreigner.AssassinMultiplier/1000f,
                    classRelations.Foreigner.BerserkerMultiplier/1000f, classRelations.Foreigner.ShielderMultiplier/1000f, classRelations.Foreigner.RulerMultiplier/1000f,
                    classRelations.Foreigner.AvengerMultiplier/1000f, classRelations.Foreigner.DemonGodPillarMultiplier/1000f, classRelations.Foreigner.BeastIIMultiplier/1000f,
                    classRelations.Foreigner.BeastIMultiplier/1000f, classRelations.Foreigner.MoonCancerMultiplier/1000f, classRelations.Foreigner.BeastIIIRMultiplier/1000f,
                    classRelations.Foreigner.ForeignerMultiplier/1000f, classRelations.Foreigner?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations.Foreigner?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations?.BeastIIIL?.SaberMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.ArcherMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.LancerMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastIIIL?.RiderMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.CasterMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.AssassinMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastIIIL?.BerserkerMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.ShielderMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.RulerMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastIIIL?.AvengerMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.DemonGodPillarMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.BeastIIMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastIIIL?.BeastIMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.MoonCancerMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.BeastIIIRMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastIIIL?.ForeignerMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations?.BeastIIIL?.BeastUnknownMultiplier/1000f ?? 0.0f
                },
                {
                    classRelations?.BeastUnknown?.SaberMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.ArcherMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.LancerMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastUnknown?.RiderMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.CasterMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.AssassinMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastUnknown?.BerserkerMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.ShielderMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.RulerMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastUnknown?.AvengerMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.DemonGodPillarMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.BeastIIMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastUnknown?.BeastIMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.MoonCancerMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.BeastIIIRMultiplier/1000f ?? 0.0f,
                    classRelations?.BeastUnknown?.ForeignerMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.BeastIIILMultiplier/1000f ?? 0.0f, classRelations?.BeastUnknown?.BeastUnknownMultiplier/1000f ?? 0.0f
                }
            };
        }
    }
}
