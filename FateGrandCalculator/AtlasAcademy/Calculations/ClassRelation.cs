using System;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;

namespace FateGrandCalculator.AtlasAcademy.Calculations
{
    public class ClassRelation : IBaseRelation
    {
        private const float CLASS_DENOMINATOR = 1000.0f;
        private readonly ClassRelationNiceJson _classRelationNiceJson;

        public ClassRelation(ClassRelationNiceJson classRelationNiceJson) 
        {
            _classRelationNiceJson = classRelationNiceJson;
        }

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
        private float[,] GetListDamageMultiplier()
        {
            return new float[,]
            {
                {
                    _classRelationNiceJson.Saber.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Saber.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Saber.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Saber.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Saber.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Saber.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Saber?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Saber?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Archer.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Archer.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Archer.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Archer.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Archer.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Archer.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Archer?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Archer?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Lancer.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Lancer.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Lancer.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Lancer.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Lancer.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Lancer.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Lancer?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Lancer?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Rider.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Rider.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Rider.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Rider.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Rider.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Rider.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Rider?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Rider?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Caster.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Caster.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Caster.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Caster.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Caster.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Caster.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Caster?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Caster?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Assassin.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Assassin.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Assassin.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Assassin.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Assassin.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Assassin.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Assassin?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Assassin?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Berserker.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Berserker.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Berserker.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Berserker.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Berserker.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Berserker.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Berserker?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Berserker?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Shielder.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Shielder.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Shielder.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Shielder.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Shielder.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Shielder.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Shielder?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Shielder?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Ruler.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Ruler.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Ruler.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Ruler.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Ruler.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Ruler.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Ruler?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Ruler?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Avenger.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Avenger.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Avenger.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Avenger.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Avenger.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Avenger.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Avenger?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Avenger?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.DemonGodPillar.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.DemonGodPillar.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.DemonGodPillar.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.DemonGodPillar.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.DemonGodPillar.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.DemonGodPillar.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.DemonGodPillar?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.DemonGodPillar?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.BeastII.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastII.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastII.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastII.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastII.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastII.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastII?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.BeastII?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.BeastI.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastI.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastI.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastI.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastI.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastI.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastI?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.BeastI?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.MoonCancer.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.MoonCancer.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.MoonCancer.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.MoonCancer.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.MoonCancer.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.MoonCancer.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.MoonCancer?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.MoonCancer?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.BeastIIIR.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastIIIR.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastIIIR.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastIIIR.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastIIIR.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.BeastIIIR.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.BeastIIIR?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIR?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson.Foreigner.SaberMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.ArcherMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.LancerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Foreigner.RiderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.CasterMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.AssassinMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Foreigner.BerserkerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.ShielderMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.RulerMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Foreigner.AvengerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.DemonGodPillarMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.BeastIIMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Foreigner.BeastIMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.MoonCancerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner.BeastIIIRMultiplier / CLASS_DENOMINATOR,
                    _classRelationNiceJson.Foreigner.ForeignerMultiplier / CLASS_DENOMINATOR, _classRelationNiceJson.Foreigner?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson.Foreigner?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson?.BeastIIIL?.SaberMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.ArcherMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.LancerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastIIIL?.RiderMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.CasterMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.AssassinMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastIIIL?.BerserkerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.ShielderMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.RulerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastIIIL?.AvengerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.DemonGodPillarMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.BeastIIMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastIIIL?.BeastIMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.MoonCancerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.BeastIIIRMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastIIIL?.ForeignerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastIIIL?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                },
                {
                    _classRelationNiceJson?.BeastUnknown?.SaberMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.ArcherMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.LancerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastUnknown?.RiderMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.CasterMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.AssassinMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastUnknown?.BerserkerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.ShielderMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.RulerMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastUnknown?.AvengerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.DemonGodPillarMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.BeastIIMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastUnknown?.BeastIMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.MoonCancerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.BeastIIIRMultiplier / CLASS_DENOMINATOR ?? 0.0f,
                    _classRelationNiceJson?.BeastUnknown?.ForeignerMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.BeastIIILMultiplier / CLASS_DENOMINATOR ?? 0.0f, _classRelationNiceJson?.BeastUnknown?.BeastUnknownMultiplier / CLASS_DENOMINATOR ?? 0.0f
                }
            };
        }
    }
}
