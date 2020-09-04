using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC
{
    public class Program
    {
        private readonly CombatFormula _combatFormula = new CombatFormula();

        /* Servants */
        const string ARTORIA_PENDRAGON_SABER = "100100";
        const string MHX_ASSASSIN = "601800";
        const string TOMOE_GOZEN_ARCHER = "202100";
        const string SUMMER_USHI_ASSASSIN = "603400";
        const string KIARA_ALTER_EGO = "1000300";
        const string ABBY_FOREIGNER = "2500100";
        const string MHXX_FOREIGNER = "2500300";
        const string LANCELOT_BERSERKER = "700200";
        const string DANTES_AVENGER = "1100200";

        /* Craft Essences */
        const string KSCOPE_CE = "9400340";
        const string AERIAL_DRIVE_CE = "9402750";
        const string BLACK_GRAIL_CE = "9400480";

        static void Main()
        {
            Program program = new Program();
            program.Calculations();
        }

        /// <summary>
        /// Main calculations
        /// </summary>
        private void Calculations()
        {
            #region Party Member
            ChaldeaCraftEssence chaldeaKscope = new ChaldeaCraftEssence
            {
                CraftEssenceLevel = 100,
                Mlb = true,
                CraftEssenceInfo = AtlasAcademyRequest.GetCraftEssenceInfo(KSCOPE_CE)
            };

            ChaldeaServant chaldeaAttackServant = new ChaldeaServant
            {
                ServantLevel = 90,
                NpLevel = 1,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevel1 = 10,
                SkillLevel2 = 10,
                SkillLevel3 = 10,
                ServantInfo = AtlasAcademyRequest.GetServantInfo(DANTES_AVENGER)
            };

            PartyMember partyMember = AddPartyMember(chaldeaAttackServant, chaldeaKscope);
            #endregion

            /* Node */
            #region First Wave
            EnemyMob enemyMob = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                Health = 13933,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob1 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                Health = 14786,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob2 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                Health = 23456,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };
            #endregion

            #region Second Wave
            EnemyMob enemyMob3 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                Health = 25554,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };

            EnemyMob enemyMob4 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                Health = 19047,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob5 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                Health = 26204,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };
            #endregion

            #region Third Wave
            EnemyMob enemyMob6 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                Health = 42926,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob7 = new EnemyMob
            {
                Name = "Walkure",
                ClassName = ClassRelationEnum.Rider,
                AttributeName = AttributeRelationEnum.Sky,
                Gender = GenderRelationEnum.Female,
                Health = 180753,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
            };

            EnemyMob enemyMob8 = new EnemyMob
            {
                Name = "Muspell",
                ClassName = ClassRelationEnum.Berserker,
                AttributeName = AttributeRelationEnum.Earth,
                Gender = GenderRelationEnum.Male,
                Health = 61289,
                IsSpecial = false,
                Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
            };
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

            // Set NP for party member at start of fight (assume highest upgraded NP by priority)
            partyMember.NoblePhantasm = partyMember
                .Servant
                .ServantInfo
                .NoblePhantasms
                .Aggregate((agg, next) =>
                    next.Priority >= agg.Priority ? next : agg);

            /* Simulate node combat */
            // TODO: Specify defense down buffs to specific enemy mobs instead of always assuming AOE.
            //       Possibly add a debuff list property for the EnemyMob object
            Console.WriteLine(">>>>>> Fight 1/3 <<<<<<\n");
            _combatFormula.WaveBattleSimulator(partyMember, 0.50f, 0.00f, 1.00f, 0.00f, enemyMob, enemyMob1, enemyMob2);

            Console.WriteLine("\n>>>>>> Fight 2/3 <<<<<<\n");
            _combatFormula.WaveBattleSimulator(partyMember, 0.50f, 0.00f, 1.00f, 0.00f, enemyMob3, enemyMob4, enemyMob5);

            Console.WriteLine("\n>>>>>> Fatal 3/3 <<<<<<\n");
            _combatFormula.WaveBattleSimulator(partyMember, 0.50f, 1.36f, 1.00f, 0.13f, enemyMob6, enemyMob7, enemyMob8);

            Console.WriteLine("Battle ended! ^.^");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey(); // end program
            }
        }

        #region Private Methods
        private PartyMember AddPartyMember(ChaldeaServant chaldeaServant, ChaldeaCraftEssence chaldeaCraftEssence)
        {
            int servantTotalAtk = chaldeaServant.ServantInfo.AtkGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.AtkGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouAttack;

            int servantTotalHp = chaldeaServant.ServantInfo.HpGrowth[chaldeaServant.ServantLevel - 1]
                + chaldeaCraftEssence.CraftEssenceInfo.HpGrowth[chaldeaCraftEssence.CraftEssenceLevel - 1]
                + chaldeaServant.FouHealth;

            return new PartyMember
            {
                Servant = chaldeaServant,
                EquippedCraftEssence = chaldeaCraftEssence,
                TotalAttack = servantTotalAtk,
                TotalHealth = servantTotalHp
            };
        }
        #endregion
    }
}
