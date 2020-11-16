using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Calculations;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Extensions;
using FateGrandOrderPOC.Shared.Models;

using Newtonsoft.Json.Linq;

namespace FateGrandOrderPOC.Shared
{
    public class CombatFormula : ICombatFormula
    {
        private readonly IAtlasAcademyClient _aaClient;
        private readonly AttributeRelation _attributeRelation = new AttributeRelation();
        private readonly ClassRelation _classRelation = new ClassRelation();
        private readonly ClassAttackRate _classAttackRate;
        private readonly ConstantRate _constantRate;

        public CombatFormula(IAtlasAcademyClient client)
        {
            _aaClient = client;
            _constantRate = new ConstantRate(client);
            _classAttackRate = new ClassAttackRate(client);
        }

        /// <summary>
        /// Simulate noble phantasms against a wave of enemies
        /// </summary>
        /// <param name="party"></param>
        /// <param name="enemyMobs"></param>
        /// <param name="waveNumber"></param>
        /// <param name="enemyPosition"></param>
        public async Task NoblePhantasmChainSimulator(List<PartyMember> party, List<EnemyMob> enemyMobs, WaveNumberEnum waveNumber, int enemyPosition)
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
                    return;
                }

                int npChainPosition = 0; // used to calculate overcharge

                // Determine active party member buffs
                float totalNpRefund = 0.0f, cardNpTypeUp = 0.0f, attackUp = 0.0f, powerModifier = 0.0f, npGainUp = 0.0f;

                // Grab the targets on the field (max: 3)
                List<EnemyMob> enemyTargets = enemyMobs.FindAll(e => e.WaveNumber == waveNumber).Take(3).ToList();

                foreach (Function partyMemberFunction in partyMember.NoblePhantasm.Functions)
                {
                    if (partyMemberFunction.FuncType == "damageNp")
                    {
                        Tuple<float, float, float, float> activePartyMemberEffects = SetStatusEffects(partyMember, cardNpTypeUp, attackUp, powerModifier, npGainUp);
                        cardNpTypeUp = activePartyMemberEffects.Item1;
                        attackUp = activePartyMemberEffects.Item2;
                        powerModifier = activePartyMemberEffects.Item3;
                        npGainUp = activePartyMemberEffects.Item4;
                        
                        if (partyMemberFunction.FuncTargetType == "enemy") // single target
                        {
                            totalNpRefund += await DamagePhase(partyMember, enemyTargets[enemyPosition], powerModifier, npChainPosition, 
                                attackUp, cardNpTypeUp, npGainUp, partyMemberFunction);
                        }
                        else
                        {
                            // Go through each enemy mob grouped by their wave number
                            for (int i = 0; i < enemyTargets.Count; i++)
                            {
                                totalNpRefund += await DamagePhase(partyMember, enemyTargets[i], powerModifier, npChainPosition, 
                                    attackUp, cardNpTypeUp, npGainUp, partyMemberFunction);
                            }
                        }
                    }
                    else
                    {
                        NoblePhantasmSkillActivation.SkillActivation(partyMemberFunction, partyMember, party, enemyTargets, enemyPosition);
                    }
                }

                // Replace old charge with newly refunded NP
                totalNpRefund /= 100.0f;
                if (totalNpRefund > 300.0f)
                {
                    totalNpRefund = 300.0f; // set max charge
                }
                partyMember.NpCharge = (float)Math.Floor(totalNpRefund);
                partyMember.NpChainOrder = NpChainOrderEnum.None;

                enemyMobs.RemoveAll(e => e.Health <= 0.0f); // remove dead enemies in preparation for next NP
                npChainPosition++;
            }

            // Check if any servants that fired off their NPs have any self "force/instantDeath" debuffs (i.e. Arash & Oda Nobukatsu)
            List<PartyMember> martyred = npChainList
                .FindAll(d => d
                    .NoblePhantasm
                    .Functions
                    .Any(f => (f.FuncType == "instantDeath" || f.FuncType == "forceInstantDeath") && f.FuncTargetType == "self"));

            foreach (PartyMember martyr in martyred)
            {
                party.Remove(martyr);
                // TODO: Move new units into their respective positions on the field
            }

            // Check if any NPs have "forceInstantDeath" and "ptSelfAnotherFirst" (i.e. Chen Gong)
            // TODO: Check elsewhere if there is another servant on the field prior to allowing this NP to execute
            PartyMember servantButcher = npChainList.Find(d => d.NoblePhantasm.Functions.Any(f => f.FuncType == "forceInstantDeath" && f.FuncTargetType == "ptSelfAnotherFirst"));
            if (servantButcher != null)
            {
                // remove the first (left-most) party member that isn't the butcher
                PartyMember scapegoat = party.Where(s => s.Id != servantButcher.Id).First();
                party.Remove(scapegoat);
            }

            return;
        }

        /// <summary>
        /// Find special damage up buff versus an enemy trait
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public float SpecialAttackUp(PartyMember partyMember, EnemyMob enemy)
        {
            float powerModifier = 0.0f;
            const float STATUS_EFFECT_DENOMINATOR = 1000.0f;

            foreach (ActiveStatus activeStatus in partyMember.ActiveStatuses)
            {
                Buff buff = GetBuff(activeStatus);
                if (buff == null)
                {
                    return 0.0f;
                }

                if (buff.Type == "upDamage" 
                    && buff.Vals.Any(f => f.Name == "buffPowerModStrUp") 
                    && buff.CkOpIndv.Any(f => f.Name == buff.Tvals.First().Name) 
                    && enemy.Traits.Contains(buff.Tvals.First().Name))
                {
                    powerModifier += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                }
            }

            return powerModifier;
        }

        /// <summary>
        /// Set necessary party member status effects for NP damage
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="cardNpTypeUp"></param>
        /// <param name="attackUp"></param>
        /// <param name="powerModifier"></param>
        /// <param name="npGainUp"></param>
        public Tuple<float, float, float, float> SetStatusEffects(PartyMember partyMember, float cardNpTypeUp, float attackUp, float powerModifier, float npGainUp)
        {
            const float STATUS_EFFECT_DENOMINATOR = 1000.0f;

            foreach (ActiveStatus activeStatus in partyMember.ActiveStatuses)
            {
                Buff buff = GetBuff(activeStatus);
                if (buff != null)
                {
                    if (buff.Type == "upCommandall" && buff.CkSelfIndv.Any(f => f.Name == ($"card{partyMember.NoblePhantasm.Card.ToUpperFirstChar()}")))
                    {
                        // Calculate card buff for NP if same card type
                        cardNpTypeUp += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                    }
                    else if (buff.Type == "upAtk")
                    {
                        attackUp += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                    }
                    else if (buff.Type == "upDropnp")
                    {
                        npGainUp += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                    }
                    else if (buff.Type == "upNpdamage")
                    {
                        powerModifier += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                    }
                }
            }

            return new Tuple<float, float, float, float>(cardNpTypeUp, attackUp, powerModifier, npGainUp);
        }

        /// <summary>
        /// Set necessary enemy status effects for NP damage
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="partyMember"></param>
        /// <param name="defenseDownModifier"></param>
        /// <param name="cardDefenseDownModifier"></param>
        public Tuple<float, float> SetStatusEffects(EnemyMob enemy, PartyMember partyMember, float defenseDownModifier, float cardDefenseDownModifier)
        {
            const float STATUS_EFFECT_DENOMINATOR = 1000.0f;

            foreach (ActiveStatus activeStatus in enemy.ActiveStatuses)
            {
                Buff buff = GetBuff(activeStatus);
                if (buff != null)
                {
                    if (buff.Type == "downDefence")
                    {
                        defenseDownModifier += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                    }
                    else if (buff.Type == "downDefencecommandall" && buff.CkOpIndv.Any(f => f.Name == ($"card{partyMember.NoblePhantasm.Card.ToUpperFirstChar()}")))
                    {
                        cardDefenseDownModifier += activeStatus.StatusEffect.Svals[activeStatus.AppliedSkillLevel - 1].Value / STATUS_EFFECT_DENOMINATOR;
                    }
                }
            }

            return new Tuple<float, float>(defenseDownModifier, cardDefenseDownModifier);
        }

        public float NpGainedFromEnemy(PartyMember partyMember, EnemyMob enemyMob, float npGainUp, float cardNpTypeUp,
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

                float calcNpPerHit = CalculatedNpPerHit(partyMember, enemyMob, cardNpTypeUp, npGainUp);
                npRefund += (float)Math.Floor(effectiveHitModifier * calcNpPerHit);
            }

            return npRefund;
        }

        public float CalculatedNpPerHit(PartyMember partyMember, EnemyMob enemyMob, float cardNpTypeUp, float npGainUp)
        {
            return EnemyClassModifier(enemyMob.ClassName.ToString())
                * partyMember.NoblePhantasm.NpGain.Np[partyMember.Servant.NpLevel - 1]
                * (1.0f + cardNpTypeUp)
                * (1.0f + npGainUp);
        }

        public List<float> NpDistributionPercentages(PartyMember partyMember)
        {
            float perc, lastNpHitPerc = 0.0f;
            List<float> percNpHitDistribution = new List<float>();

            /* Get NP distribution values and percentages */
            foreach (int npHit in partyMember.NoblePhantasm.NpDistribution)
            {
                perc = npHit + lastNpHitPerc;
                percNpHitDistribution.Add(perc);
                lastNpHitPerc = perc;
            }

            return percNpHitDistribution;
        }

        public float AttemptToKillEnemy(EnemyMob enemyMob, float npDamageForEnemyMob)
        {
            float healthRemaining = enemyMob.Health - (npDamageForEnemyMob * 0.9f);
            if (healthRemaining < 0.0f)
            {
                healthRemaining = 0.0f;
            }

            return healthRemaining;
        }

        public async Task<float> BaseNpDamage(PartyMember partyMember, EnemyMob enemy, int npChainPosition, Function npFunction)
        {
            Sval svalNp = Overcharge(partyMember.NpCharge, npChainPosition) switch
            {
                5 => npFunction.Svals5[partyMember.Servant.NpLevel - 1],
                4 => npFunction.Svals4[partyMember.Servant.NpLevel - 1],
                3 => npFunction.Svals3[partyMember.Servant.NpLevel - 1],
                2 => npFunction.Svals2[partyMember.Servant.NpLevel - 1],
                _ => npFunction.Svals[partyMember.Servant.NpLevel - 1],
            };

            int npValue = svalNp.Value;
            int targetBonusNpDamage = 0;

            // Add additional NP damage if there is a special target ID
            if (svalNp.Target > 0)
            {
                JObject traits = await _aaClient.GetTraitEnumInfo();
                string traitName = traits.Property(svalNp.Target.ToString())?.Value.ToString() ?? "";
                if (enemy.Traits.Contains(traitName))
                {
                    targetBonusNpDamage = svalNp.Correction;
                }
            }

            float constantAttackRate = await _constantRate.GetAttackMultiplier("ATTACK_RATE").ConfigureAwait(false);
            float classModifier = await _classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName).ConfigureAwait(false);
            float npTypeModifier = await _constantRate.GetAttackMultiplier($"ENEMY_ATTACK_RATE_{partyMember.NoblePhantasm.Card}").ConfigureAwait(false);

            // Base NP damage = ATTACK_RATE * Servant total attack * Class modifier * NP type modifier * NP damage
            float baseNpDamage = constantAttackRate
                * partyMember.TotalAttack
                * classModifier
                * npTypeModifier
                * (npValue / 1000.0f);

            if (targetBonusNpDamage != 0)
            {
                baseNpDamage *= targetBonusNpDamage / 1000.0f;
            }

            return baseNpDamage;
        }

        /// <summary>
        /// Return the overcharge in decimal (non-percent) format based on NP charge and the position in the NP chain
        /// </summary>
        /// <param name="npCharge"></param>
        /// <param name="npChainPosition"></param>
        /// <returns></returns>
        public int Overcharge(float npCharge, int npChainPosition)
        {
            int overcharge = 0;

            // Set overcharge based on the level of NP charge
            if (npCharge == 300.0f)
            {
                overcharge = 3;
            }
            else if (npCharge >= 200.0f)
            {
                overcharge = 2;
            }

            // Add additional overcharge depending on the position in the NP chain
            if (npChainPosition == 1) // 2nd NP in the chain
            {
                overcharge += 1;
            }
            else if (npChainPosition == 2) // 3rd NP in the chain
            {
                overcharge += 2;
            }

            return overcharge;
        }

        /// <summary>
        /// Multiply the modified NP damage passed in and the attribute and class modifiers between the party member and enemy
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <param name="modifiedNpDamage"></param>
        /// <returns></returns>
        public async Task<float> AverageNpDamage(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage)
        {
            return modifiedNpDamage 
                * await AttributeModifier(partyMember, enemyMob).ConfigureAwait(false) 
                * await ClassModifier(partyMember, enemyMob).ConfigureAwait(false);
        }

        /// <summary>
        /// Return the chance to kill an enemy based on 90% and 110% NP damage RNG
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <param name="modifiedNpDamage"></param>
        /// <returns></returns>
        public async Task<float> ChancesToKill(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage)
        {
            if (0.9f * await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage).ConfigureAwait(false) > enemyMob.Health)
            {
                return 100.0f; // perfect clear, even with the worst RNG
            }
            else if (1.1f * await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage).ConfigureAwait(false) < enemyMob.Health)
            {
                return 0.0f; // never clear, even with the best RNG
            }
            else // show chance of success
            {
                return (1.0f - ((enemyMob.Health / await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage).ConfigureAwait(false) - 0.9f) / 0.2f)) * 100.0f;
            }
        }

        /// <summary>
        /// Check if party member has enough NP charge for an attack. If so, add them to the queue
        /// </summary>
        /// <param name="partyMember"></param>
        public void AddPartyMemberToNpChain(List<PartyMember> party, PartyMember partyMember)
        {
            if (partyMember.NpCharge < 100.0f)
            {
#if DEBUG
                Console.WriteLine($"{partyMember.Servant.ServantInfo.Name} only has {partyMember.NpCharge}% charge");
#endif
            }
            else
            {
                if (!party.Exists(p => p.NpChainOrder == NpChainOrderEnum.First))
                {
                    partyMember.NpChainOrder = NpChainOrderEnum.First;
                }
                else if (!party.Exists(p => p.NpChainOrder == NpChainOrderEnum.Second))
                {
                    partyMember.NpChainOrder = NpChainOrderEnum.Second;
                }
                else if (!party.Exists(p => p.NpChainOrder == NpChainOrderEnum.Third))
                {
                    partyMember.NpChainOrder = NpChainOrderEnum.Third;
                }
                else
                {
#if DEBUG
                    Console.WriteLine("Error: Max NP chain limit is 3");
#endif
                }
            }
        }

        /// <summary>
        /// Add a servant from the player's Chaldea to the battle party and equip the specified CE (if available)
        /// </summary>
        /// <param name="party"></param>
        /// <param name="chaldeaServant"></param>
        /// <param name="chaldeaCraftEssence"></param>
        /// <returns></returns>
        public PartyMember AddPartyMember(List<PartyMember> party, Servant chaldeaServant, CraftEssence chaldeaCraftEssence = null)
        {
            int servantTotalAtk = chaldeaServant.ServantInfo.AtkGrowth[chaldeaServant.ServantLevel - 1] + chaldeaServant.FouAttack;
            int servantTotalHp = chaldeaServant.ServantInfo.HpGrowth[chaldeaServant.ServantLevel - 1] + chaldeaServant.FouHealth;

            if (chaldeaCraftEssence != null)
            {
                servantTotalAtk += chaldeaCraftEssence.CraftEssenceInfo.AtkGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1];
                servantTotalHp += chaldeaCraftEssence.CraftEssenceInfo.HpGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1];
            }

            return new PartyMember
            {
                Id = party.Count,
                Servant = chaldeaServant,
                EquippedCraftEssence = chaldeaCraftEssence,
                TotalAttack = servantTotalAtk,
                TotalHealth = servantTotalHp,
                NoblePhantasm = chaldeaServant  // Set NP for party member at start of fight
                    .ServantInfo                // (assume highest upgraded NP by priority)
                    .NoblePhantasms
                    .Aggregate((agg, next) =>
                        next.Priority >= agg.Priority ? next : agg)
            };
        }

        public void ApplyCraftEssenceEffects(PartyMember partyMember)
        {
            if (partyMember.EquippedCraftEssence == null)
            {
                return;
            }

            int priority = 1;
            if (partyMember.EquippedCraftEssence.Mlb)
            {
                priority = 2;
            }

            List<Skill> skills = partyMember.EquippedCraftEssence.CraftEssenceInfo.Skills.FindAll(s => s.Priority == priority);

            foreach (Skill skill in skills)
            {
                foreach (Function function in skill.Functions)
                {
                    if (function.FuncType == "gainNp")
                    {
                        partyMember.NpCharge += function.Svals[0].Value / 100.0f;
                    }
                    else
                    {
                        partyMember.ActiveStatuses.Add(new ActiveStatus
                        {
                            StatusEffect = function,
                            AppliedSkillLevel = 1,
                            ActiveTurnCount = function.Svals[0].Turn
                        });
                    }
                }
            }

            return;
        }

        #region Private Methods
        private async Task<float> DamagePhase(PartyMember partyMember, EnemyMob enemyMob, float powerModifier, int npChainPosition, float attackUp, 
            float cardNpTypeUp, float npGainUp, Function npFunction)
        {
            powerModifier += SpecialAttackUp(partyMember, enemyMob);

            // Determine enemy debuffs
            float defenseDownModifier = 0.0f, cardDefenseDebuffModifier = 0.0f;
            Tuple<float, float> enemyEffects = SetStatusEffects(enemyMob, partyMember, defenseDownModifier, cardDefenseDebuffModifier);
            defenseDownModifier = enemyEffects.Item1;
            cardDefenseDebuffModifier = enemyEffects.Item2;

            float baseNpDamage = await BaseNpDamage(partyMember, enemyMob, npChainPosition, npFunction).ConfigureAwait(false);
            if (baseNpDamage == 0.0f)
            {
                return 0.0f; // skip unnecessary calculations
            }

            float totalPowerDamageModifier = (1.0f + attackUp + defenseDownModifier + cardDefenseDebuffModifier)
                * (1.0f + cardNpTypeUp)
                * (1.0f + powerModifier);

            float modifiedNpDamage = baseNpDamage * totalPowerDamageModifier;
            float npDamageForEnemyMob = await AverageNpDamage(partyMember, enemyMob, modifiedNpDamage).ConfigureAwait(false);

            List<float> npDistributionPercentages = NpDistributionPercentages(partyMember);

            float npRefund = NpGainedFromEnemy(partyMember, enemyMob, npGainUp, cardNpTypeUp, npDamageForEnemyMob, npDistributionPercentages);

            await ChancesToKill(partyMember, enemyMob, modifiedNpDamage).ConfigureAwait(false);
            enemyMob.Health = AttemptToKillEnemy(enemyMob, npDamageForEnemyMob);

            return npRefund;
        }

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

        /// <summary>
        /// Check a servant's active status for any buffs
        /// </summary>
        /// <param name="activeStatus">Active status of a servant</param>
        /// <returns></returns>
        private Buff GetBuff(ActiveStatus activeStatus)
        {
            List<Buff> buffs = activeStatus.StatusEffect.Buffs;
            if (buffs == null || buffs.Count == 0)
            {
                return null;
            }

            return buffs[0];
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
