using System;
using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademy.Calculations;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared
{
    public class CombatFormula
    {
        private readonly AttributeRelation _attributeRelation = new AttributeRelation();
        private readonly ClassRelation _classRelation = new ClassRelation();
        private readonly ClassAttackRate _classAttackRate = new ClassAttackRate();
        private readonly ConstantRate _constantRate = new ConstantRate();

        /// <summary>
        /// Simulate a wave inside of a node
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="attackUp"></param>
        /// <param name="cardNpTypeUp"></param>
        /// <param name="powerModifier"></param>
        /// <param name="enemyMob"></param>
        /// <param name="enemyMob1"></param>
        /// <param name="enemyMob2"></param>
        public void WaveBattleSimulator(PartyMember partyMember, float npGainUp, float attackUp, float cardNpTypeUp, float powerModifier,
            EnemyMob enemyMob, EnemyMob enemyMob1 = null, EnemyMob enemyMob2 = null)
        {
            //Console.WriteLine(">>>>>>>> Stats <<<<<<<<");
            //Console.WriteLine($"Attribute Multiplier ({enemyMob.Name}): {AttributeModifier(partyMember, enemyMob)}x");
            //Console.WriteLine($"Attribute Multiplier ({enemyMob1.Name}): {AttributeModifier(partyMember, enemyMob1)}x");
            //Console.WriteLine($"Attribute Multiplier ({enemyMob2.Name}): {AttributeModifier(partyMember, enemyMob2)}x");
            //Console.WriteLine($"Class Advantage Multiplier ({enemyMob.Name}): {ClassModifier(partyMember, enemyMob)}x");
            //Console.WriteLine($"Class Advantage Multiplier ({enemyMob1.Name}): {ClassModifier(partyMember, enemyMob1)}x");
            //Console.WriteLine($"Class Advantage Multiplier ({enemyMob2.Name}): {ClassModifier(partyMember, enemyMob2)}x\n");

            float baseNpDamage = BaseNpDamage(partyMember);
            //Console.WriteLine($"{partyMember.Servant.ServantInfo.Name}'s base NP damage: {baseNpDamage}");

            float totalPowerDamageModifier = (1.0f + attackUp) * (1.0f + cardNpTypeUp) * (1.0f + powerModifier);
            //Console.WriteLine($"Total power damage modifier: {totalPowerDamageModifier}");

            float modifiedNpDamage = baseNpDamage * totalPowerDamageModifier;
            //Console.WriteLine($"Modified NP damage: {modifiedNpDamage}\n");

            float npDamageForEnemyMob = AverageNpDamage(partyMember, enemyMob, modifiedNpDamage);
            float npDamageForEnemyMob1 = AverageNpDamage(partyMember, enemyMob1, modifiedNpDamage);
            float npDamageForEnemyMob2 = AverageNpDamage(partyMember, enemyMob2, modifiedNpDamage);

            //Console.WriteLine($"Average NP damage towards {enemyMob.Name}: {npDamageForEnemyMob}");
            //Console.WriteLine($"Average NP damage towards {enemyMob1.Name}: {npDamageForEnemyMob1}");
            //Console.WriteLine($"Average NP damage towards {enemyMob2.Name}: {npDamageForEnemyMob2}\n");

            float chanceToKillEnemyMob = ChanceToKill(partyMember, enemyMob, modifiedNpDamage);
            float chanceToKillEnemyMob1 = ChanceToKill(partyMember, enemyMob1, modifiedNpDamage);
            float chanceToKillEnemyMob2 = ChanceToKill(partyMember, enemyMob2, modifiedNpDamage);

            Console.WriteLine($"Chance to kill {enemyMob.Name}: {chanceToKillEnemyMob}%");
            Console.WriteLine($"Chance to kill {enemyMob1.Name}: {chanceToKillEnemyMob1}%");
            Console.WriteLine($"Chance to kill {enemyMob2.Name}: {chanceToKillEnemyMob2}%\n");

            Console.WriteLine($"Health remaining: {HealthRemaining(enemyMob, npDamageForEnemyMob)}");
            Console.WriteLine($"Health remaining: {HealthRemaining(enemyMob1, npDamageForEnemyMob1)}");
            Console.WriteLine($"Health remaining: {HealthRemaining(enemyMob2, npDamageForEnemyMob2)}\n");

            List<float> npDistributionPercentages = NpDistributionPercentages(partyMember);

            float totalNpRefund = NpGainedFromEnemy(partyMember, enemyMob, npGainUp, cardNpTypeUp, npDamageForEnemyMob, npDistributionPercentages)
                + NpGainedFromEnemy(partyMember, enemyMob1, npGainUp, cardNpTypeUp, npDamageForEnemyMob1, npDistributionPercentages)
                + NpGainedFromEnemy(partyMember, enemyMob2, npGainUp, cardNpTypeUp, npDamageForEnemyMob2, npDistributionPercentages);

            totalNpRefund /= 100.0f;

            Console.WriteLine($"Total NP refund: {totalNpRefund}%\n");
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

                Console.WriteLine($"NP Hit Perc: {npHitPerc}% || Effective hit: {effectiveHitModifier} || Accumulated NP refund: {npRefund / 100.0f}");
            }

            Console.WriteLine($"\nTotal NP refund from {enemyMob.Name}: {npRefund / 100.0f}%\n");
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

        private float BaseNpDamage(PartyMember partyMember)
        {
            int npValue = partyMember.NoblePhantasm
                .Functions.Find(f => f.FuncType.Contains("damageNp"))
                .Svals[partyMember.Servant.NpLevel - 1].Value;

            //Console.WriteLine($"Total attack: {partyMember.TotalAttack}");
            //Console.WriteLine($"Class modifier: {_classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)}");
            //Console.WriteLine($"NP type modifier: {_constantRate.GetAttackMultiplier("enemy_attack_rate_" + partyMember.NoblePhantasm.Card)}");
            //Console.WriteLine($"NP value: {npValue / 1000.0f}");

            // Base NP damage = ATTACK_RATE * Servant total attack * Class modifier * NP type modifier * NP damage
            return _constantRate.GetAttackMultiplier("ATTACK_RATE")
                * partyMember.TotalAttack
                * _classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)
                * _constantRate.GetAttackMultiplier($"ENEMY_ATTACK_RATE_{partyMember.NoblePhantasm.Card}")
                * (npValue / 1000.0f);
        }

        private float AverageNpDamage(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage)
        {
            return modifiedNpDamage * AttributeModifier(partyMember, enemyMob) * ClassModifier(partyMember, enemyMob);
        }

        private float ChanceToKill(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage)
        {
            if (0.9f * AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) > enemyMob.Health)
            {
                return 100.0f; // perfect clear
            }
            else if (1.1f * AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) < enemyMob.Health)
            {
                return 0.0f; // never clear
            }
            else // show chance of success
            {
                return (1.0f - ((enemyMob.Health / AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) - 0.9f) / 0.2f)) * 100.0f;
            }
        }

        /// <summary>
        /// Compares the relationship between two monster's attributes and shows the resulting modifier
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <returns></returns>
        private float AttributeModifier(PartyMember partyMember, EnemyMob enemyMob)
        {
            return _attributeRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.Attribute, enemyMob.AttributeName.ToString().ToLower());
        }

        /// <summary>
        /// Compares the relationship between two monster's classes and shows the resulting modifier
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <returns></returns>
        private float ClassModifier(PartyMember partyMember, EnemyMob enemyMob)
        {
            return _classRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName, enemyMob.ClassName.ToString().ToLower());
        }
        #endregion
    }
}
