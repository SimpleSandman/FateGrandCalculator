using System;
using System.Collections.Generic;
using System.Threading;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademyJson;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC
{
    public class Program
    {
        /* Servants */
        const string ARTORIA_PENDRAGON_SABER = "100100";
        const string MHX_ASSASSIN = "601800";
        const string TOMOE_GOZEN_ARCHER = "202100";
        const string SUMMER_USHI_ASSASSIN = "603400";
        const string KIARA_ALTER_EGO = "1000300";
        const string ABBY_FOREIGNER = "2500100";
        const string MHXX_FOREIGNER = "2500300";

        /* Craft Essences */
        const string KSCOPE_CE = "9400340";
        const string AERIAL_DRIVE_CE = "9402750";
        const string BLACK_GRAIL_CE = "9400480";

        static void Main()
        {
            #region Commented Code
            //Console.WriteLine(">>>>>>>> Defending Servant <<<<<<<<");
            //ServantNiceJson defendingServant = PrintRelevantServantInfo(MHXX_FOREIGNER);
            #endregion

            Console.WriteLine(">>>>>>>> Craft Essence <<<<<<<<");
            ChaldeaCraftEssence chaldeaBlackGrail = new ChaldeaCraftEssence
            {
                CraftEssenceLevel = 100,
                Mlb = true,
                CraftEssenceInfo = PrintRelevantCraftEssenceInfo(BLACK_GRAIL_CE, 100)
            };

            Console.WriteLine(">>>>>>>> Attacking Servant <<<<<<<<");
            ChaldeaServant chaldeaAttackServant = new ChaldeaServant
            {
                ServantLevel = 100,
                NpLevel = 5,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevel1 = 10,
                SkillLevel2 = 10,
                SkillLevel3 = 10,
                ServantInfo = PrintRelevantServantInfo(TOMOE_GOZEN_ARCHER, 100)
            };

            PartyMember partyMember = PrintRelevantPartyMemberInfo(chaldeaAttackServant, chaldeaBlackGrail);

            EnemyMob normalArcherPirate = new EnemyMob
            {
                Name = "Pirate",
                ClassName = ClassRelationEnum.Archer,
                AttributeName = AttributeRelationEnum.Human,
                Gender = GenderRelationEnum.Male,
                Health = 3136,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Human", "Humanoid", "Male"
                }
            };

            AttributeRelation attributeRelation = new AttributeRelation();
            ClassRelation classRelation = new ClassRelation();
            ClassAttackRate classAttackRate = new ClassAttackRate();

            Console.WriteLine(">>>>>>>> Stats <<<<<<<<");
            Console.WriteLine($"Attribute Multiplier: {attributeRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.Attribute, normalArcherPirate.AttributeName.ToString())}x");
            Console.WriteLine($"Class Advantage Multiplier: {classRelation.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName, normalArcherPirate.ClassName.ToString())}x");

            partyMember.NpCharge = 100;

            if (partyMember.NpCharge < 100)
            {
                Console.WriteLine($"{partyMember.Servant.ServantInfo.Name} only has {partyMember.NpCharge}% charge");

                Console.ReadKey(); // end program
                return;
            }

            // Base NP damage = Servant total attack * Class modifier * NP type modifier * NP damage
            float baseNpDamage = partyMember.Attack 
                * classAttackRate.GetAttackMultiplier(partyMember.Servant.ServantInfo.ClassName)
                * 0.8f // NP type --> partyMember.Servant.ServantInfo.NoblePhantasms[^1].Type
                       // Use --> https://api.atlasacademy.io/export/NA/NiceConstant.json
                           // and grab "ENEMY_ATTACK_RATE_" constant according to NP type
                * partyMember.Servant.ServantInfo.NoblePhantasms[^1].Functions.Find(f => f.FuncType.Contains("np")).Svals[partyMember.Servant.NpLevel - 1].Value / 10.0f;

            Console.ReadKey(); // end program
        }

        #region Private Methods
        private static PartyMember PrintRelevantPartyMemberInfo(ChaldeaServant chaldeaServant, ChaldeaCraftEssence chaldeaCraftEssence)
        {
            int servantTotalAtk = chaldeaServant.ServantInfo.AtkGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.AtkGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouAttack;

            int servantTotalHp = chaldeaServant.ServantInfo.HpGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.HpGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouHealth;

            Console.WriteLine($"Servant ATK w/ CE: {servantTotalAtk}");
            Console.WriteLine($"Servant HP w/ CE: {servantTotalHp}");
            Console.WriteLine();

            return new PartyMember
            {
                Servant = chaldeaServant,
                EquippedCraftEssence = chaldeaCraftEssence,
                Attack = servantTotalAtk,
                Health = servantTotalHp
            };
        }

        private static EquipNiceJson PrintRelevantCraftEssenceInfo(string craftEssenceId, int level = 1)
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

        private static ServantNiceJson PrintRelevantServantInfo(string servantId, int level = 1)
        {
            ServantNiceJson servant = AtlasAcademyRequest.GetServantInfo(servantId);
            NoblePhantasm np = servant.NoblePhantasms[^1]; // ToDo: Give user choice of available NPs

            Console.WriteLine($"Servant Name: {servant.Name}");
            Console.WriteLine($"Servant Level: {level}");
            Console.WriteLine($"Attribute: {servant.Attribute}");
            Console.WriteLine($"Class: {servant.ClassName}");
            //Console.WriteLine($"NP Name: {np.Name} {np.Rank}");
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

        

        private static void ServantStats(string servantId)
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
