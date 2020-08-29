using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Calculations;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC
{
    public class Program
    {
        private readonly AttributeRelation _attributeRelation = new AttributeRelation();
        private readonly ClassRelation _classRelation = new ClassRelation();
        private readonly ClassAttackRate _classAttackRate = new ClassAttackRate();
        private readonly ConstantRate _constantRate = new ConstantRate();

        /* Servants */
        const string ARTORIA_PENDRAGON_SABER = "100100";
        const string MHX_ASSASSIN = "601800";
        const string TOMOE_GOZEN_ARCHER = "202100";
        const string SUMMER_USHI_ASSASSIN = "603400";
        const string KIARA_ALTER_EGO = "1000300";
        const string ABBY_FOREIGNER = "2500100";
        const string MHXX_FOREIGNER = "2500300";
        const string LANCELOT_BERSERKER = "700200";

        /* Craft Essences */
        const string KSCOPE_CE = "9400340";
        const string AERIAL_DRIVE_CE = "9402750";
        const string BLACK_GRAIL_CE = "9400480";

        static void Main()
        {
            Program program = new Program();
            program.Calculations();
        }

        private void Calculations()
        {
            #region Party Member
            Console.WriteLine(">>>>>>>> Craft Essence <<<<<<<<");
            ChaldeaCraftEssence chaldeaKscope = new ChaldeaCraftEssence
            {
                CraftEssenceLevel = 100,
                Mlb = true,
                CraftEssenceInfo = PrintRelevantCraftEssenceInfo(KSCOPE_CE, 100)
            };

            Console.WriteLine(">>>>>>>> Attacking Servant <<<<<<<<");
            ChaldeaServant chaldeaAttackServant = new ChaldeaServant
            {
                ServantLevel = 80,
                NpLevel = 3,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevel1 = 10,
                SkillLevel2 = 10,
                SkillLevel3 = 10,
                ServantInfo = PrintRelevantServantInfo(LANCELOT_BERSERKER, 80)
            };

            PartyMember partyMember = PrintRelevantPartyMemberInfo(chaldeaAttackServant, chaldeaKscope);
            #endregion

            #region Enemy Mob
            EnemyMob enemyMob1 = new EnemyMob
            {
                Name = "Roman Soldier 1",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Human,
                Gender = GenderRelationEnum.Male,
                Health = 23152,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Human", "Humanoid", "Male", "Roman"
                }
            };

            EnemyMob enemyMob2 = new EnemyMob
            {
                Name = "Roman Soldier 2",
                ClassName = ClassRelationEnum.Lancer,
                AttributeName = AttributeRelationEnum.Human,
                Gender = GenderRelationEnum.Male,
                Health = 18818,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Human", "Humanoid", "Male", "Roman"
                }
            };

            EnemyMob enemyMob3 = new EnemyMob
            {
                Name = "Vesuvias Dragon",
                ClassName = ClassRelationEnum.Lancer,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Unknown,
                Health = 85591,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Dragon", "Super Large"
                }
            };

            Console.WriteLine(">>>>>>>> Stats <<<<<<<<");
            Console.WriteLine($"Attribute Multiplier ({enemyMob1.Name}): {AttributeModifier(partyMember, enemyMob1)}x");
            Console.WriteLine($"Attribute Multiplier ({enemyMob2.Name}): {AttributeModifier(partyMember, enemyMob2)}x");
            Console.WriteLine($"Attribute Multiplier ({enemyMob3.Name}): {AttributeModifier(partyMember, enemyMob3)}x");
            Console.WriteLine($"Class Advantage Multiplier ({enemyMob1.Name}): {ClassModifier(partyMember, enemyMob1)}x");
            Console.WriteLine($"Class Advantage Multiplier ({enemyMob2.Name}): {ClassModifier(partyMember, enemyMob2)}x");
            Console.WriteLine($"Class Advantage Multiplier ({enemyMob3.Name}): {ClassModifier(partyMember, enemyMob3)}x");
            Console.WriteLine();
            #endregion

            #region NP Charge
            partyMember.NpCharge = 100; // ToDo: Make this based on skills/CEs/etc

            if (partyMember.NpCharge < 100)
            {
                Console.WriteLine($"{partyMember.Servant.ServantInfo.Name} only has {partyMember.NpCharge}% charge");

                Console.ReadKey(); // end program
                return;
            }
            #endregion

            float baseNpDamage = BaseNpDamage(partyMember);

            Console.WriteLine($"{partyMember.Servant.ServantInfo.Name}'s base NP damage: {baseNpDamage}");

            // Servant power modifiers
            float npGainUp = 0.0f, attackUp = 0.0f, cardNpTypeUp = 0.0f, powerModifier = 0.0f;

            npGainUp += 0.50f; // 2004 3rd skill
            attackUp += 0.20f; // Lancelot's NP effect (activates first)
            cardNpTypeUp += 0.50f; // Skadi's quick effectiveness
            cardNpTypeUp += 0.50f; // Skadi's quick effectiveness
            powerModifier += 0.0f;

            float totalPowerDamageModifier = (1.0f + attackUp) * (1.0f + cardNpTypeUp) * (1.0f + powerModifier);
            Console.WriteLine($"Total power damage modifier: {totalPowerDamageModifier}");
            float modifiedNpDamage = baseNpDamage * totalPowerDamageModifier;
            Console.WriteLine($"Modified NP damage: {modifiedNpDamage}\n");

            Console.WriteLine($"Average NP damage towards {enemyMob1.Name}: {AverageNpDamage(partyMember, enemyMob1, modifiedNpDamage)}");
            Console.WriteLine($"Average NP damage towards {enemyMob2.Name}: {AverageNpDamage(partyMember, enemyMob2, modifiedNpDamage)}");
            Console.WriteLine($"Average NP damage towards {enemyMob3.Name}: {AverageNpDamage(partyMember, enemyMob3, modifiedNpDamage)}\n");

            Console.WriteLine($"Chance to kill {enemyMob1.Name}: {ChanceToKill(partyMember, enemyMob1, modifiedNpDamage)}%");
            Console.WriteLine($"Chance to kill {enemyMob2.Name}: {ChanceToKill(partyMember, enemyMob2, modifiedNpDamage)}%");
            Console.WriteLine($"Chance to kill {enemyMob3.Name}: {ChanceToKill(partyMember, enemyMob3, modifiedNpDamage)}%");

            Console.ReadKey(); // end program
        }

        #region Calculation Methods
        private float BaseNpDamage(PartyMember partyMember)
        {
            NoblePhantasm highestPriorityNp = partyMember.Servant.ServantInfo.NoblePhantasms
                .Aggregate((agg, next) =>
                    next.Priority >= agg.Priority ? next : agg);

            int npValue = highestPriorityNp
                .Functions.Find(f => f.FuncType.Contains("damageNp"))
                .Svals[partyMember.Servant.NpLevel - 1].Value;

            Console.WriteLine($"Total attack: {partyMember.TotalAttack}");
            Console.WriteLine($"Class modifier: {_classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)}");
            Console.WriteLine($"NP type modifier: {_constantRate.GetAttackMultiplier("attackrate" + highestPriorityNp.Card)}");
            Console.WriteLine($"NP value: {(npValue / 1000.0f)}");

            // Base NP damage = ATTACK_RATE * Servant total attack * Class modifier * NP type modifier * NP damage
            return _constantRate.GetAttackMultiplier("ATTACK_RATE")
                * partyMember.TotalAttack
                * _classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)
                * _constantRate.GetAttackMultiplier($"ENEMY_ATTACK_RATE_{highestPriorityNp.Card}")
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
                return 100.0f;
            }
            else if (1.1f * AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) < enemyMob.Health)
            {
                return 0.0f;
            }
            else
            {
                return (1.0f - ((enemyMob.Health / AverageNpDamage(partyMember, enemyMob, modifiedNpDamage) - 0.9f) / 0.2f)) * 100.0f;
            }
        }

        /// <summary>
        /// Compares the relationship between two object's attributes and shows the resulting modifier
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <returns></returns>
        private float AttributeModifier(PartyMember partyMember, EnemyMob enemyMob)
        {
            return _attributeRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.Attribute, enemyMob.AttributeName.ToString());
        }

        /// <summary>
        /// Compares the relationship between two object's classes and shows the resulting modifier
        /// </summary>
        /// <param name="partyMember"></param>
        /// <param name="enemyMob"></param>
        /// <returns></returns>
        private float ClassModifier(PartyMember partyMember, EnemyMob enemyMob)
        {
            return _classRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName, enemyMob.ClassName.ToString());
        }
        #endregion

        #region Informative Methods
        private PartyMember PrintRelevantPartyMemberInfo(ChaldeaServant chaldeaServant, ChaldeaCraftEssence chaldeaCraftEssence)
        {
            int servantTotalAtk = chaldeaServant.ServantInfo.AtkGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.AtkGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouAttack;

            int servantTotalHp = chaldeaServant.ServantInfo.HpGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.HpGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouHealth;

            Console.WriteLine($"Servant ATK w/ CE: {servantTotalAtk}");
            Console.WriteLine($"Servant HP w/ CE: {servantTotalHp}");
            Console.WriteLine($"NP Level: {chaldeaServant.NpLevel}");
            Console.WriteLine();

            return new PartyMember
            {
                Servant = chaldeaServant,
                EquippedCraftEssence = chaldeaCraftEssence,
                TotalAttack = servantTotalAtk,
                TotalHealth = servantTotalHp
            };
        }

        private EquipNiceJson PrintRelevantCraftEssenceInfo(string craftEssenceId, int level = 1)
        {
            EquipNiceJson craftEssence = AtlasAcademyRequest.GetCraftEssenceInfo(craftEssenceId);

            Console.WriteLine($"CE Name: {craftEssence.Name}");
            Console.WriteLine($"CE Level: {level}");
            if (level > 0 && level <= 100)
            {
                Console.WriteLine($"Current ATK: {craftEssence.AtkGrowth[level - 1]}");
                Console.WriteLine($"Current HP: {craftEssence.HpGrowth[level - 1]}");
            }
            Console.WriteLine($"Max HP: {craftEssence.HpMax}");
            Console.WriteLine($"Max ATK: {craftEssence.AtkMax}");
            Console.WriteLine();

            return craftEssence;
        }

        private ServantNiceJson PrintRelevantServantInfo(string servantId, int level = 1)
        {
            ServantNiceJson servant = AtlasAcademyRequest.GetServantInfo(servantId);
            NoblePhantasm np = servant.NoblePhantasms[^1]; // ToDo: Give user choice of available NPs

            Console.WriteLine($"Servant Name: {servant.Name}");
            Console.WriteLine($"Servant Level: {level}");
            Console.WriteLine($"Attribute: {servant.Attribute}");
            Console.WriteLine($"Class: {servant.ClassName}");
            Console.WriteLine($"NP Name: {np.Name} {np.Rank}");
            //Console.WriteLine($"NP Card Type: {np.Card}");
            //if (level > 0 && level <= 100)
            //{
            //    Console.WriteLine($"Current ATK: {servant.AtkGrowth[level - 1]}");
            //    Console.WriteLine($"Current HP: {servant.HpGrowth[level - 1]}");
            //}
            //Console.WriteLine($"Attack max (before grail): {servant.AtkMax}");
            //Console.WriteLine($"Health max (before grail): {servant.HpMax}");
            //Console.WriteLine($"Attack absolute max: {servant.AtkGrowth[^1]}");
            //Console.WriteLine($"Health absolute max: {servant.HpGrowth[^1]}");
            //Console.WriteLine($"Skill 1 Name: {servant.Skills[0].Name}");
            //Console.WriteLine($"Skill 1 First Buff Type: {servant.Skills[0].Functions[0].Buffs[0].Type}");
            //Console.WriteLine($"Skill 1 Target Type: {servant.Skills[0].Functions[0].FuncTargetType}");
            Console.WriteLine();

            return servant;
        }

        private void ServantStats(string servantId)
        {
            ServantNiceJson servantJson = AtlasAcademyRequest.GetServantInfo(servantId);
            
            // retry if no result
            if (servantJson == null)
            {
                Console.WriteLine($"\nI wasn't able to get any info on Servant ID {servantId}. Give me 5 seconds to retry...\n\n");
                Thread.Sleep(5000); // give API a breather
                servantJson = ApiRequest.GetDesearlizeObjectAsync<ServantNiceJson>("https://api.atlasacademy.io/nice/NA/servant/" + servantId + "?lang=en").Result;

                // skip to next servant lookup
                if (servantJson == null)
                {
                    Console.WriteLine($"\nUnable to pull info on Servant ID {servantId}. Moving onto next servant...\n\n");
                    return;
                }
            }

            Console.WriteLine($"ID: {servantJson.Id}");
            Console.WriteLine($"Servant Name: {servantJson.Name}");
            Console.WriteLine($"Class Name: {servantJson.ClassName}");
            Console.WriteLine($"Rarity: {servantJson.Rarity}");
            Console.WriteLine($"Cost: {servantJson.Cost}");
            Console.WriteLine($"Gender: {servantJson.Gender}");
            Console.WriteLine($"Attribute: {servantJson.Attribute}");
            //Console.WriteLine($"Cards: {string.Join(", ", servantJson.Cards)}");
            //Console.WriteLine($"First Ascension 1st Item Name: {servantJson.AscensionMaterials?.FirstAsc?.Items[0].Amount.ToString() ?? "N/A"} {servantJson.AscensionMaterials?.FirstAsc?.Items[0].ItemObject.Name ?? ""}");
            
            //if (servantJson.AscensionMaterials?.SecondAsc?.Items.Count > 1)
            //    Console.WriteLine($"Second Ascension 2nd Item Name: {servantJson.AscensionMaterials?.SecondAsc?.Items[1].Amount.ToString() ?? "N/A"} {servantJson.AscensionMaterials?.SecondAsc?.Items[1].ItemObject.Name ?? ""}");
            //else
            //    Console.WriteLine($"Second Ascension Only Item Name: {servantJson.AscensionMaterials?.SecondAsc?.Items[0].Amount.ToString() ?? "N/A"} {servantJson.AscensionMaterials?.SecondAsc?.Items[0].ItemObject.Name ?? ""}");
            
            //Console.WriteLine($"Eigth Skill Level 1st Item Name: {servantJson.SkillMaterials?.EigthSkill?.Items[0].Amount.ToString() ?? "N/A"} {servantJson.SkillMaterials?.EigthSkill?.Items[0].ItemObject.Name ?? ""}");
            
            if (servantJson?.NoblePhantasms.Count > 0)
                Console.WriteLine($"Latest NP Name and Rank: {servantJson?.NoblePhantasms[^1].Name ?? "N/A"} {servantJson?.NoblePhantasms[^1].Rank ?? ""}");
            else
                Console.WriteLine("Latest NP Name and Rank: N/A");

            Console.WriteLine();
        }
        #endregion
    }
}
