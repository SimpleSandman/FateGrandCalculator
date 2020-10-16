using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy.Calculations;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Extensions;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared
{
    public class CombatFormula
    {
        private readonly AttributeRelation _attributeRelation = new AttributeRelation();
        private readonly ClassRelation _classRelation = new ClassRelation();
        private readonly ClassAttackRate _classAttackRate;
        private readonly ConstantRate _constantRate;        

        public CombatFormula(IAtlasAcademyClient client)
        {
            _constantRate = new ConstantRate(client);
            _classAttackRate = new ClassAttackRate(client);
        }

        /// <summary>
        /// Simulate noble phantasms against a wave of enemies
        /// </summary>
        /// <param name="party"></param>
        /// <param name="enemyMobs"></param>
        /// <param name="waveNumber"></param>
        /// <param name="npGainUp"></param>
        /// <param name="defenseDownModifier"></param>
        public async Task NoblePhantasmChainSimulator(List<PartyMember> party, List<EnemyMob> enemyMobs, WaveNumberEnum waveNumber, 
            float npGainUp, float defenseDownModifier)
        {
            List<PartyMember> npChainList = party
                .FindAll(p => p.NpChainOrder != NpChainOrderEnum.None)
                .OrderBy(p => p.NpChainOrder)
                .ToList();

            // Go through each party member's NP attack in the order of the NP chain provided
            foreach (PartyMember partyMember in npChainList)
            {
                // Check if any enemies are alive
                if (enemyMobs.Count == 0)
                {
                    Console.WriteLine("Node cleared!!");
                    return;
                }

                float totalNpRefund = 0.0f, cardNpTypeUp = 0.0f, attackUp = 0.0f, powerModifier = 0.0f;

                // TODO: Create a method that handles more buff types using a switch statement
                // Calculate card buff for NP if same card type
                foreach (ActiveStatus activeStatus in partyMember.ActiveStatuses)
                {
                    BuffServant buff = activeStatus.StatusEffect.Buffs[0];

                    if (buff.Type == "upCommandall" && buff.CkSelfIndv.Any(f => f.Name == ($"card{partyMember.NoblePhantasm.Card.ToUpperFirstChar()}")))
                    {
                        cardNpTypeUp += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / 1000.0f;
                    }
                    else if (buff.Type == "upAtk")
                    {
                        attackUp += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / 1000.0f;
                    }
                    else if (buff.Type == "upNpdamage") // TODO: Add more power modifers
                    {
                        powerModifier += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / 1000.0f;
                    }
                }

                // Add defense down to the overall attack up to simplify the math below
                attackUp += defenseDownModifier;

                // Go through each enemy mob grouped by their wave number
                for (int i = 0; i < enemyMobs.FindAll(e => e.WaveNumber == waveNumber).Take(3).Count(); i++)
                {
                    EnemyMob enemyMob = enemyMobs[i];

                    //Console.WriteLine(">>>>>>>> Stats <<<<<<<<");
                    //Console.WriteLine($"Attribute Multiplier ({enemyMob.Name}): {AttributeModifier(partyMember, enemyMob)}x");
                    //Console.WriteLine($"Class Advantage Multiplier ({enemyMob.Name}): {ClassModifier(partyMember, enemyMob)}x");

                    float baseNpDamage = await BaseNpDamage(partyMember);
                    //Console.WriteLine($"{partyMember.Servant.ServantInfo.Name}'s base NP damage: {baseNpDamage}");

                    float totalPowerDamageModifier = (1.0f + attackUp) * (1.0f + cardNpTypeUp) * (1.0f + powerModifier);
                    //Console.WriteLine($"Total power damage modifier: {totalPowerDamageModifier}");

                    float modifiedNpDamage = baseNpDamage * totalPowerDamageModifier;
                    //Console.WriteLine($"Modified NP damage: {modifiedNpDamage}\n");

                    float npDamageForEnemyMob = await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage);
                    //Console.WriteLine($"Average NP damage towards {enemyMob.Name}: {npDamageForEnemyMob}");

                    List<float> npDistributionPercentages = NpDistributionPercentages(partyMember);

                    // Grab NP refund from current enemy
                    totalNpRefund += NpGainedFromEnemy(partyMember, enemyMob, npGainUp, cardNpTypeUp, npDamageForEnemyMob, npDistributionPercentages);

                    // Check health of enemy
                    float chanceToKillEnemyMob = await ChanceToKill(partyMember, enemyMob, modifiedNpDamage);
                    enemyMob.Health = HealthRemaining(enemyMob, npDamageForEnemyMob);

                    Console.WriteLine($"Chance to kill {enemyMob.Name}: {chanceToKillEnemyMob}%\n");
                    Console.WriteLine($"Health remaining: {enemyMob.Health}\n");
                }

                // Replace old charge with newly refunded NP
                totalNpRefund /= 100.0f;
                partyMember.NpCharge = (float)Math.Floor(totalNpRefund);
                partyMember.NpChainOrder = NpChainOrderEnum.None;

                enemyMobs.RemoveAll(e => e.Health <= 0.0f); // remove dead enemies in preparation for next NP

                Console.WriteLine($"Total NP refund for {partyMember.Servant.ServantInfo.Name}: {partyMember.NpCharge}%\n");
            }

            return;
        }

        #region Private Methods
        /// <summary>
        /// NP gain modifier based on enemy class and special (server-side data)
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        private float EnemyClassModifier(string className)
        {
            switch (className.ToLower())
            {
                case "berserker":
                    return 0.8f;
                case "assassin":
                    return 0.9f;
                case "saber":
                case "archer":
                case "lancer":
                case "ruler":
                case "avenger":
                case "alterEgo":
                case "foreigner":
                case "shielder":
                    return 1.0f;
                case "rider":
                    return 1.1f;
                case "caster":
                case "moonCancer":
                    return 1.2f;
                default:
                    return 0.0f;
            }
        }

        private float NpGainedFromEnemy(PartyMember partyMember, EnemyMob enemyMob, float npGainUp, float cardNpTypeUp,
            float npDamageForEnemyMob, List<float> npDistributionPercentages)
        {
            float effectiveHitModifier, npRefund = 0.0f;

            foreach (float npHitPerc in npDistributionPercentages)
            {
                if (0.9f * npDamageForEnemyMob * npHitPerc / 100.0f > enemyMob.Health)
                {
                    effectiveHitModifier = 1.5f; // overkill (includes killing blow)
                }
                else
                {
                    effectiveHitModifier = 1.0f; // regular hit
                }

                npRefund += (float)Math.Floor(effectiveHitModifier * CalculatedNpPerHit(partyMember, enemyMob, cardNpTypeUp, npGainUp));

                //Console.WriteLine($"NP Hit Perc: {npHitPerc}% || Effective hit: {effectiveHitModifier} || Accumulated NP refund: {npRefund / 100.0f}");
            }

            Console.WriteLine($"Total NP refund from {enemyMob.Name}: {npRefund / 100.0f}%");
            return npRefund;
        }

        private float CalculatedNpPerHit(PartyMember partyMember, EnemyMob enemyMob, float cardNpTypeUp, float npGainUp)
        {
            return EnemyClassModifier(enemyMob.ClassName.ToString())
                * partyMember.NoblePhantasm.NpGain.Np[partyMember.Servant.NpLevel - 1]
                * (1.0f + cardNpTypeUp)
                * (1.0f + npGainUp);
        }

        private List<float> NpDistributionPercentages(PartyMember partyMember)
        {
            float perc, lastNpHitPerc = 0.0f;
            List<float> percNpHitDistribution = new List<float>();

            /* Get NP distribution values and percentages */
            foreach (int npHit in partyMember.NoblePhantasm.NpDistribution)
            {
                perc = npHit + lastNpHitPerc;

                //Console.WriteLine($"NP Hit: {npHit}, Perc: {perc}%");
                percNpHitDistribution.Add(perc);

                lastNpHitPerc = perc;
            }

            return percNpHitDistribution;
        }

        private float HealthRemaining(EnemyMob enemyMob, float npDamageForEnemyMob)
        {
            float healthRemaining = enemyMob.Health - (npDamageForEnemyMob * 0.9f);
            if (healthRemaining < 0.0f)
            {
                healthRemaining = 0.0f;
            }

            return healthRemaining;
        }

        private async Task<float> BaseNpDamage(PartyMember partyMember)
        {
            int npValue = partyMember.NoblePhantasm
                .Functions.Find(f => f.FuncType.Contains("damageNp"))
                .Svals[partyMember.Servant.NpLevel - 1].Value;

            //Console.WriteLine($"Total attack: {partyMember.TotalAttack}");
            //Console.WriteLine($"Class modifier: {_classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)}");
            //Console.WriteLine($"NP type modifier: {_constantRate.GetAttackMultiplier("enemy_attack_rate_" + partyMember.NoblePhantasm.Card)}");
            //Console.WriteLine($"NP value: {npValue / 1000.0f}");

            // Base NP damage = ATTACK_RATE * Servant total attack * Class modifier * NP type modifier * NP damage
            return await _constantRate.GetAttackMultiplier("ATTACK_RATE")
                * partyMember.TotalAttack
                * await _classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)
                * await _constantRate.GetAttackMultiplier($"ENEMY_ATTACK_RATE_{partyMember.NoblePhantasm.Card}")
                * (npValue / 1000.0f);
        }

        private async Task<float> AverageNpDamage(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage)
        {
            return modifiedNpDamage * await AttributeModifier(partyMember, enemyMob) * await ClassModifier(partyMember, enemyMob);
        }

        private async Task<float> ChanceToKill(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage)
        {
            if (0.9f * await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) > enemyMob.Health)
            {
                return 100.0f; // perfect clear
            }
            else if (1.1f * await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) < enemyMob.Health)
            {
                return 0.0f; // never clear
            }
            else // show chance of success
            {
                return (1.0f - ((enemyMob.Health / await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) - 0.9f) / 0.2f)) * 100.0f;
            }
        }

        /// <summary>
        /// Compares the relationship between two monster's attributes and shows the resulting modifier
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <returns></returns>
        private async Task<float> AttributeModifier(PartyMember partyMember, EnemyMob enemyMob)
        {
            return await _attributeRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.Attribute, enemyMob.AttributeName.ToString().ToLower());
        }

        /// <summary>
        /// Compares the relationship between two monster's classes and shows the resulting modifier
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <returns></returns>
        private async Task<float> ClassModifier(PartyMember partyMember, EnemyMob enemyMob)
        {
            return await _classRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName, enemyMob.ClassName.ToString().ToLower());
        }
        #endregion
    }
}
